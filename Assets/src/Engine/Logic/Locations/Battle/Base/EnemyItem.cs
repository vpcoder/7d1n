using Engine.Data;
using Engine.Data.Factories;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using UnityEngine.AI;

namespace Engine.Logic.Locations
{

    public enum NpcActionType : byte
    {
        None,
        Move,
        Attack,
        Wait,
        Reload,
    };

    [Serializable]
    public class NpcAction
    {
        [SerializeField]
        public NpcActionType Action;
        [SerializeField]
        public List<Vector3> Path { get; set; }
        public IFirearmsWeapon FirearmsWeapon;
        public IEdgedWeapon EdgedWeapon;
        public IItem Ammo;
        public float WaitDelay = 0f;
        public float Speed = 1f;
    }

    /// <summary>
    /// Текущий контекст NPC
    /// </summary>
    [Serializable]
    public class NpcContext
    {
        [SerializeField]
        public List<NpcAction> Actions;
    }

    /// <summary>
    /// Противник игрока и его группы
    /// </summary>
    public class EnemyItem : MonoBehaviour, IAttackCharacter
    {

        [SerializeField] protected Animator animator;
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected GameObject body;
        [SerializeField] protected AudioSource attackAudioSource;
        [SerializeField] protected long id;

        public Animator Animator { get { return animator; } }
        public NavMeshAgent Agent { get { return this.agent; } }
        public Vector2Int Pos { get; set; }
		public IDamagedObject Target { get; set; }
		
        protected IEnemy enemy;

        /// <summary>
        /// Параметры врага
        /// </summary>
        public IEnemy Enemy
        {
            get
            {
                if (this.enemy == null)
                {
                    this.enemy = EnemyFactory.Instance.GenerateEnemy(id);
                    UpdateBody();
                }
                return this.enemy;
            }
        }

        public List<Vector3> GetPath(Vector3 targetPoint)
        {
            var path = new NavMeshPath();
            if (!Agent.CalculatePath(targetPoint, path))
                return null;
            return path.corners.ToList();
        }

        protected virtual void UpdateBody()
        {
            this.body.transform.DestroyAllChilds();
            var body = GameObject.Instantiate<GameObject>(EnemyFactory.Instance.GetBody(id), this.body.transform);
            var enemyBody = body.GetComponent<EnemyBody>();
            this.animator.avatar = enemyBody.Avatar;
            this.animator.runtimeAnimatorController = enemyBody.Controller;
        }

        public AudioSource AttackAudioSource
        {
            get
            {
                return this.attackAudioSource;
            }
        }

        private void Awake()
        {
            OnStart();
        }

        public void OnMouseUpAsButton()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Game.Instance.Runtime.BattleContext.OrderIndex != EnemyGroup.PlayerGroup) // Не ход игрока?
                return;


        }

        public bool IsEndStep { get; set; } = true;

        public virtual IWeapon Weapon
        {
            get;
            set;
        }

        public virtual Vector3 TargetAttackPos
        {
            get;
            set;
        }

        public virtual GameObject ToObject
        {
            get
            {
                return this.gameObject;
            }
        }

        public float Timestamp = 0f;
        public float WaitDelay = 0f;
        public Vector3 LocalPosition;

        [SerializeField]
        public NpcAction CurrentAction;
        [SerializeField]
        public NpcContext NpcContext;

        public virtual void OnStart()
        {
            this.enemy = EnemyFactory.Instance.GenerateEnemy(id);
            UpdateBody();
        }

        public virtual void OnUpdate()
        {
            if (enemy.Health <= 0)
                Died();

            if (enemy == null || IsEndStep || CurrentAction == null) // Уже походили
                return;

            // Ход NPC
            if (Game.Instance.Runtime.BattleContext.OrderIndex != EnemyGroup.PlayerGroup && Game.Instance.Runtime.BattleContext.OrderIndex != EnemyGroup.AnotherPlayerGroup)
            {
                switch (CurrentAction.Action)
                {
                    case NpcActionType.Wait:
                        if (Time.time - Timestamp < WaitDelay)
                            return;
                        DoNextAction(); // Конец этого действия
                        break;
                    case NpcActionType.Move:
                        var nextPoint = CurrentAction.Path[0];
                        var distance = Vector3.Distance(LocalPosition, nextPoint);
                        var progress = Mathf.Min((Time.time - Timestamp) * CurrentAction.Speed / distance, 1f);
                        transform.position = Vector3.Lerp(LocalPosition, nextPoint, progress);
                        if (progress >= 1f)
                        {
                            CurrentAction.Path.RemoveAt(0); // Следующая точка
                            LocalPosition = transform.position;
                            Timestamp = Time.time;
                            if (CurrentAction.Path.Count == 0) // Достигли конца
                            {
                                DoNextAction(); // Конец этого действия
                            }
                        }
                        break;
                    case NpcActionType.Attack:

                        if (Target == null) // Потеряли цель
                        {
                            DoNextAction(); // Конец этого действия
                            break;
                        }

                        if (CurrentAction.FirearmsWeapon != null) // Атака на расстоянии
                        {
                            Weapon = CurrentAction.FirearmsWeapon;
                            TargetAttackPos = Target.ToObject.transform.position;
                            BattleCalculationService.DoFirearmsAttack(this);
                        }
                        else // Атака вблизи
                        {
                            Weapon = CurrentAction.EdgedWeapon;
                            BattleCalculationService.DoEdgedAttack(this, Target);
                        }
                        
                        DoNextAction(); // Конец этого действия
                        break;

                    case NpcActionType.Reload: // Перезарядка оружия
                        var weapon = CurrentAction.FirearmsWeapon;
                        var ammo = CurrentAction.Ammo;
                        var ammoCount = Math.Min(ammo.Count, weapon.AmmoStackSize);
                        weapon.AmmoCount += ammoCount;
                        ammo.Count -= ammoCount;
                        if (ammo.Count == 0)
                        {
                            enemy.Items.Remove(ammo);
                        }
                        AudioController.Instance.PlaySound(AttackAudioSource, weapon.ReloadSoundType);
                        DoNextAction(); // Конец этого действия
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private void Update()
        {
            OnUpdate();
        }

        public void DoNextAction()
        {
            if (NpcContext.Actions.Count == 0)
            {
                CurrentAction = null;
                DoEndStep();
                return;
            }

            CurrentAction = NpcContext.Actions[0];

            Timestamp = Time.time;
            WaitDelay = CurrentAction.WaitDelay;
            LocalPosition = transform.position;

            NpcContext.Actions.RemoveAt(0);
        }

        private void DoEndStep()
        {
            IsEndStep = true;
            var manager = ObjectFinder.Find<BattleManager>();
            manager.EnemyStepCompleted(this);
        }

        public virtual void Died()
        {
            var manager = ObjectFinder.Find<BattleManager>();
            manager.RemoveEnemiesFromBattle(this);

            var dropController = ObjectFinder.Find<ItemsDropController>();
            dropController.Drop(transform.position, Enemy.Items); // Выкидываем предметы
            dropController.Drop(transform.position, Enemy.Weapons?.Where(weapon => !DataDictionary.Items.SYSTEM_ITEMS.Contains(weapon.ID)).ToArray());

            DoEndStep();
            GameObject.Destroy(this.gameObject);
        }

        public virtual void AddBattleExp(long value)
        {

        }

    }

}
