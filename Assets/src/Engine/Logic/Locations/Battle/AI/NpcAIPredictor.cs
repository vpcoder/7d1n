using Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Engine.Logic.Locations
{

    #pragma warning disable IDE0060

    public struct PredictorMoveResult
    {
        public IFirearmsWeapon Weapon;
        public bool Moved;
    }

    public class NpcAIPredictor
    {
        private static readonly Lazy<NpcAIPredictor> instance = new Lazy<NpcAIPredictor>(() => new NpcAIPredictor());
        public static NpcAIPredictor Instance { get { return instance.Value; } }

        private BattleManager battle;

        /// <summary>
        /// Единая точка рассчитывающая стратегию группы AI
        /// </summary>
        public void CreateStrategy()
        {
            this.battle = ObjectFinder.Find<BattleManager>();

            Debug.Log("create strategy...");

            var currentGroup = Game.Instance.Runtime.BattleContext.OrderIndex;
            var allAiItems = NpcAISceneManager.Instance.GroupToNpcList; // Все НПС в своих группах
            var enemyList = allAiItems[currentGroup]; // Группа, которая сейчас ходит

            battle.EnemyGroupCounter = enemyList.Count;

            foreach (var enemy in enemyList)
                CreateStrategyForEnemy(currentGroup, enemy, allAiItems);
        }

        public void CreateStrategyForEnemy(EnemyGroup group, EnemyItem enemy, IDictionary<EnemyGroup, List<EnemyItem>> allAiItems)
        {
            Debug.Log("create strategy for '" + enemy.transform.name + "'...");

            var target = enemy.Target ?? FindTarget(group, enemy, allAiItems); // Пытаемся найти цель
            if(target == null)
            {
                enemy.IsEndStep = true; // Не можем ходить, некого атаковать
                return;
            }

			enemy.Target = target;
            enemy.CurrentAction = null;
            enemy.NpcContext.Actions.Clear();
            enemy.NpcContext.Actions.Add(CreateWait(Random.Range(0.1f, 1f))); // Начинаем ход со случайной задержкой от 0.1 до 1 секунды, чтобы казалось что нпс более живые

            var ap = enemy.Enemy.AP;
            var moveResult = DoMoveIfNeeded(enemy, target, ref ap); // Движемся к цели, если нужно
            for(;;)
            {
                if (ap == 0)
                    break;

                if (moveResult.Weapon != null)
                {
                    if (!DoAttackOnlyRanged(enemy, target, moveResult.Weapon, ref ap)) // Атакуем дальнобойным пока есть ОД
                        break;
                }
                else
                {
                    if (!DoAttackIfNeeded(enemy, target, ref ap)) // Атакуем пока есть ОД
                        break;
                }
                enemy.NpcContext.Actions.Add(CreateWait(Random.Range(0.3f, 0.6f))); // 300mls-600mls
            }

            foreach(var action in enemy.NpcContext.Actions)
            {
                Debug.Log("+ " + enemy.transform.name + " action: " + action.Action.ToString());
            }

            enemy.DoNextAction(); // Начинаем первое действие
            enemy.IsEndStep = false; // Ходим
        }

        private IDamagedObject FindTarget(EnemyGroup group, EnemyItem enemy, IDictionary<EnemyGroup, List<EnemyItem>> allAiItems)
        {
        	var potentialTargetList = new List<IDamagedObject>();

			foreach(var entry in allAiItems)
			{
				if(entry.Key == group)
					continue;

				foreach(var item in entry.Value)
				{
					var startPos = enemy.transform.position;
                    var path = enemy.GetPath(item.transform.position);
					if(path != null || TryFindRangedWeapon(item.Enemy.AP, item.Enemy.Weapons, item.Enemy.Items) != null)
					{
						potentialTargetList.Add(item.transform.GetComponent<IDamagedObject>());
					}
				}
			}
			
			if(potentialTargetList.Count == 0)
            	return null;

            potentialTargetList.Sort((o1, o2) => // Сортируем по расстоянию до цели
            {
                var v1 = Vector3.Distance(enemy.transform.position, o1.ToObject.transform.position);
                var v2 = Vector3.Distance(enemy.transform.position, o2.ToObject.transform.position);
                return Mathf.RoundToInt((v1 - v2) * 100);
            });

            return potentialTargetList[0]; // Берём в качестве цели самого ближнего противника
        }

		private IWeapon TryFindWeaponByPredicate(int ap, List<IWeapon> weapons, List<IItem> items, Func<IWeapon, bool> predicate = null)
		{
			if(weapons == null || weapons.Count == 0)
				return null;
			
			foreach(IWeapon weapon in weapons)
			{
				if(weapon.UseAP > ap)
					continue;

				if(predicate !=null && !predicate(weapon))
					continue;

				if(weapon.Type == GroupType.WeaponFirearms)
				{
					var firearms = (IFirearmsWeapon)weapon;
					if(firearms.AmmoCount > 0 || TryFindAmmo(firearms, items) != null)
						return weapon;
				}
				else
				{
					return weapon;
				}
			}
			return null;
		}

		private IItem TryFindAmmo(IFirearmsWeapon weapon, List<IItem> items)
		{
            if (items == null || items.Count == 0)
                return null;
			return items.Where(item => item.ID == weapon.AmmoID).FirstOrDefault();
		}

		private IFirearmsWeapon TryFindRangedWeapon(int ap, List<IWeapon> weapons, List<IItem> items)
		{
			return (IFirearmsWeapon)TryFindWeaponByPredicate(ap, weapons, items, (w) =>  w.Type == GroupType.WeaponFirearms);			
		}

        private PredictorMoveResult DoMoveIfNeeded(EnemyItem enemy, IDamagedObject target, ref int ap)
        {
            var result = new PredictorMoveResult();

            if (ap <= 0)
                return result;

            var currentAp = ap;

            var weapon = TryFindRangedWeapon(ap, enemy.Enemy.Weapons, enemy.Enemy.Items);
            if (weapon != null) // FIXME: check raycast
            {
                result.Weapon = weapon;
                return result;
            }

			var startPos = enemy.transform.position;
			var endPos = target.ToObject.transform.position;
			var path = enemy.GetPath(endPos);

            if (path == null || path.Count == 0)
                return result;

			path.Reverse();

            if (path.Count == 0)
                return result;

            var pathFragment = new List<Vector3>();
			for(int i = 0; i < currentAp; i++)
			{
				if(path.Count <= i)
					break;

				ap--;
				//pathFragment.Add(matrix.GetCellToWorldPointCenter(path[i].Position));
			}

            if (pathFragment.Count == 0)
                return result;

			//var newPoint = matrix.GetCell(pathFragment[pathFragment.Count-1]);
			enemy.NpcContext.Actions.Add(CreateMove(enemy, pathFragment, 1f));

            result.Moved = true;

            return result;
        }

        private bool DoAttackIfNeeded(EnemyItem enemy, IDamagedObject target, ref int ap)
        {
        	if(ap <= 0)
        		return false;
        	
            var weapon = TryFindWeaponByPredicate(ap, enemy.Enemy.Weapons, enemy.Enemy.Items); // Оружия которые можно использовать (хватает ОД)
            if (weapon == null)
                return false;

            switch(weapon.Type)
            {
                case GroupType.WeaponFirearms:
                    enemy.NpcContext.Actions.Add(CreateAttack(enemy, (IFirearmsWeapon)weapon));
                    break;
                case GroupType.WeaponEdged:
                    enemy.NpcContext.Actions.Add(CreateAttack(enemy, (IEdgedWeapon)weapon));
                    break;
                default:
                    throw new NotSupportedException();
            }

            ap -= weapon.UseAP;
            return true;
        }

        private bool DoAttackOnlyRanged(EnemyItem enemy, IDamagedObject target, IFirearmsWeapon weapon, ref int ap)
        {
            if (weapon == null)
                return false;

            switch (weapon.Type)
            {
                case GroupType.WeaponFirearms:
                    if (weapon.AmmoCount > 0)
                    {
                        enemy.NpcContext.Actions.Add(CreateAttack(enemy, weapon));
                        weapon.AmmoCount--;
                    }
                    else
                    {
                        if (weapon.ReloadAP > ap)
                            return false;
                        var ammo = TryFindAmmo(weapon, enemy.Enemy.Items);
                        if (ammo != null)
                        {
                            ap -= weapon.ReloadAP;
                            enemy.NpcContext.Actions.Add(CreateReload(enemy, weapon, ammo));
                            return true;
                        }
                    }
                    ap -= weapon.UseAP;
                    return true;
                default:
                    throw new NotSupportedException();
            }
        }


        private NpcAction CreateWait(float delay)
        {
            return new NpcAction()
            {
                Action = NpcActionType.Wait,
                WaitDelay = delay,
            };
        }

        private NpcAction CreateMove(EnemyItem enemy, List<Vector3> path, float speed)
        {
            return new NpcAction()
            {
                Action = NpcActionType.Move,
                Path = path,
                Speed = speed,
            };
        }

        private NpcAction CreateAttack(EnemyItem enemy, IFirearmsWeapon weapon)
        {
            return new NpcAction()
            {
                Action = NpcActionType.Attack,
                FirearmsWeapon = weapon,
                EdgedWeapon = null,
            };
        }

        private NpcAction CreateAttack(EnemyItem enemy, IEdgedWeapon weapon)
        {
            return new NpcAction()
            {
                Action = NpcActionType.Attack,
                FirearmsWeapon = null,
                EdgedWeapon = weapon,
            };
        }

        private NpcAction CreateReload(EnemyItem enemy, IFirearmsWeapon weapon, IItem ammo)
        {
            return new NpcAction()
            {
                Action = NpcActionType.Reload,
                FirearmsWeapon = weapon,
                Ammo = ammo,
            };
        }

    }

}
