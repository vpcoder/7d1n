using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Engine.Logic.Locations
{
    
    /// <summary>
    ///
    /// Предиктор, который будет формировать список действий для NPC в зависимости от его состояния
    /// ---
    /// Predictor that will generate a list of actions for an NPC depending on its state
    /// 
    /// </summary>
    public abstract class AbstractPredictorBase : IPredictor
    {

        /// <summary>
        ///     Название предиктора
        ///		Уникальное в рамках всех предикторов!
        ///     ---
        ///     Predictor name
        ///		Unique within all predictors!
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        ///     Метод должен сформировать список действий для NPC
        ///     ---
        ///     The method should generate a list of actions for NPCs
        /// </summary>
        /// <param name="context">
        ///     Контекст предиктора, в котором находятся все необходимые для анализа действий NPC данные
        ///     ---
        ///     Predictor context, which contains all the data needed to analyze NPC actions
        /// </param>
        public void CreateStrategyForNpc(PredictorContext context)
        {
            // TODO: ... Proxy Call
            CreateStrategyForNpcInner(context);
        }

        /// <summary>
        ///     Метод должен сформировать список действий для NPC
        ///     ---
        ///     The method should generate a list of actions for NPCs
        /// </summary>
        /// <param name="context">
        ///     Контекст предиктора, в котором находятся все необходимые для анализа действий NPC данные
        ///     ---
        ///     Predictor context, which contains all the data needed to analyze NPC actions
        /// </param>
        public abstract void CreateStrategyForNpcInner(PredictorContext context);
        
        #region Protected Handlers
        
        protected NpcBaseActionContext CreateWait(float delay)
        {
            return new NpcWaitActionContext()
            {
                Action = NpcActionType.Wait,
                WaitDelay = delay,
            };
        }

        protected NpcBaseActionContext CreateWait(float delayMin, float delayMax)
        {
            return CreateWait(Random.Range(delayMin, delayMax));
        }
        
        protected NpcBaseActionContext CreateMove(CharacterNpcBehaviour character, List<Vector3> path, float speed)
        {
            return new NpcMoveActionContext()
            {
                Action = NpcActionType.Move,
                Path = path,
                Speed = speed,
            };
        }

        protected NpcBaseActionContext CreatePickWeapon(CharacterNpcBehaviour character, IWeapon weapon)
        {
            return new NpcPickWeaponActionContext()
            {
                Action = NpcActionType.PickWeapon,
                Weapon = weapon,
            };
        }

        protected NpcBaseActionContext CreateAttack(CharacterNpcBehaviour character, IWeapon weapon)
        {
            return new NpcAttackActionContext()
            {
                Action = NpcActionType.Attack,
                Weapon = weapon,
            };
        }

        protected NpcBaseActionContext CreateReload(CharacterNpcBehaviour character, IFirearmsWeapon weapon, IItem ammo)
        {
            return new NpcReloadActionContext()
            {
                Action = NpcActionType.Reload,
                FirearmsWeapon = weapon,
                Ammo = ammo,
            };
        }

        protected NpcBaseActionContext CreateLook(IDamagedObject target, float speed)
        {
            return new NpcLookActionContext()
            {
                Action = NpcActionType.Look,
                Speed = speed,
                LookPoint = target.ToObject.transform.position,
            };
        }
        
        #endregion
        
        /// <summary>
        ///     Поиск потенциальных противников из списка allAiItems для указанного персонажа npc
        ///     ---
        ///     
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="allAiItems"></param>
        /// <returns></returns>
        protected virtual List<PotentialTarget> FindPotentialEnemies(CharacterNpcBehaviour npc, IDictionary<OrderGroup, List<CharacterNpcBehaviour>> allAiItems)
        {
            var group = npc.CharacterBody.Character.OrderGroup;
            var potentialTargetList = new List<PotentialTarget>();
            var relations = Game.Instance.Runtime.BattleContext.Relations;
            foreach(var entry in allAiItems)
            {
                if(entry.Key == group || !relations.IsEnemies(group, entry.Key))
                    continue; // Это не враги или они не могут быть потенциальными врагами

                foreach(var potentialEnemy in entry.Value)
                {
                    var path = npc.CalculatePath(potentialEnemy.transform.position);
                    var rangedWeapon = TryFindRangedWeapon(potentialEnemy.Character.AP, potentialEnemy.Character.Weapons, potentialEnemy.Character.Items);
                    if(path != null || (rangedWeapon != null && NpcCalculationService.CanSeeTarget(npc, potentialEnemy)))
                    {
                        potentialTargetList.Add(new PotentialTarget()
                        {
                            Target = potentialEnemy,
                            Path = path,
                            PathLength = PathHelper.CalcLength(path),
                            FirearmsWeapon = rangedWeapon,
                            PriorityWeapon = rangedWeapon ?? TryFindWeaponByPredicate(potentialEnemy.Character.AP, potentialEnemy.Character.Weapons, potentialEnemy.Character.Items),
                        });
                    }
                }
            }
            return potentialTargetList;
        }
        
        protected PotentialTarget FindTargetByDistancePriority(CharacterNpcBehaviour npc, IDictionary<OrderGroup, List<CharacterNpcBehaviour>> allAiItems)
        {
            var potentialTargetList = FindPotentialEnemies(npc, allAiItems);
			
            if(potentialTargetList.Count == 0)
                return null;

            potentialTargetList.Sort((o1, o2) => // Сортируем по расстоянию до цели
            {
                if (o1.PathLength != 0 || o2.PathLength != 0)
                {
                    return Mathf.RoundToInt((o1.PathLength - o2.PathLength) * 100);
                }
                else
                {
                    var v1 = Vector3.Distance(npc.transform.position, o1.Target.transform.position);
                    var v2 = Vector3.Distance(npc.transform.position, o2.Target.transform.position);
                    return Mathf.RoundToInt((v1 - v2) * 100);
                }
            });

            return potentialTargetList.FirstOrDefault(); // Берём в качестве цели самого ближнего противника
        }
        
        /// <summary>
        ///     Выполняет поиск подходящих патронов/снарядов в инвентаре персонажа для оружия
        ///     ---
        ///     Searches for appropriate ammo/bullet in a character's weapon inventory
        /// </summary>
        /// <param name="weapon">
        ///     Оружие, для которого выполняем поиск патронов/снарядов
        ///     ---
        ///     Weapon for which we are searching for ammo/bullet
        /// </param>
        /// <param name="items">
        ///     Набор предметов в инвентаре персонажа
        ///     ---
        ///     A set of items in a character's inventory
        /// </param>
        /// <returns>
        ///     Объект патронов/снарядов, который нашёлся в инвентаря для указанного оружия weapon
        ///     ---
        ///     The object of the ammo/bullet that was found in the inventory for the specified weapon
        /// </returns>
        protected virtual IItem TryFindAmmo(IFirearmsWeapon weapon, List<IItem> items)
        {
            if (weapon == null || Lists.IsEmpty(items))
                return null;
            return items.Where(item => item.ID == weapon.AmmoID).FirstOrDefault();
        }

        /// <summary>
        ///     Ищет первое попавшееся оружие по предикату, на использование которого хватает ОД
        ///     а так же, если это оружие требует патронов - чтобы патроны были в в стволе или в
        ///     списке предметов
        ///     ---
        ///     Searches for the first found weapon by predicate, which has enough APs to use
        ///     also, if this weapon requires ammo - so that the ammo is in the barrel or in the
        ///     the list of items
        /// </summary>
        /// <param name="ap">
        ///     Число ОД у персонажа
        ///     ---
        ///     The number of APs a character has
        /// </param>
        /// <param name="weapons">
        ///     Арсенал оружия, который имеется у персонажа
        ///     ---
        ///     The arsenal of weapons that the character has
        /// </param>
        /// <param name="items">
        ///     Набор предметов в инвентаре персонажа
        ///     ---
        ///     A set of items in a character's inventory
        /// </param>
        /// <param name="predicate">
        ///     Кастомные условия выборки оружия
        ///     ---
        ///     Custom Weapon Selection Conditions
        /// </param>
        /// <returns>
        ///     Найденное оружие, подходящее под все критерии выборки,
        ///     null - если ничего подходящего не нашлось
        ///     ---
        ///     Weapon found that matches all the criteria of the sample,
        ///     null - if nothing suitable was found
        /// </returns>
        protected virtual IWeapon TryFindWeaponByPredicate(int ap, List<IWeapon> weapons, List<IItem> items, Func<IWeapon, bool> predicate = null)
        {
            if(Lists.IsEmpty(weapons))
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
        
        protected virtual IFirearmsWeapon TryFindRangedWeapon(int ap, List<IWeapon> weapons, List<IItem> items)
        {
            return (IFirearmsWeapon)TryFindWeaponByPredicate(ap, weapons, items, (w) =>  w.Type == GroupType.WeaponFirearms);			
        }
        
    }
    
}