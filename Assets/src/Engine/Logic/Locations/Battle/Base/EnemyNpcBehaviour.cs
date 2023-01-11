using System;
using Engine.Data;
using Engine.Data.Factories;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;
using Engine.Logic.Locations.Objects;
using Engine.Logic.Locations.Animation;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// 
    /// Любой NPC в игре, в том числе персонаж игрока, и его товарищи
    /// ---
    /// Any NPC in the game, including the player character, and his comrades
    /// 
    /// </summary>
    public abstract class EnemyNpcBehaviour : MonoBehaviour,
                                              IAttackObject,
                                              IMonoBehaviourOverrideStartEvent,
                                              IMonoBehaviourOverrideUpdateEvent
    {

        [SerializeField] protected Animator animator;
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected GameObject body;
        [SerializeField] protected AudioSource attackAudioSource;
        [SerializeField] protected Transform lookDirectionTransform;
        [SerializeField] protected long id;

        private float timestamp;

        #region Shared Properties

        public Transform LookDirectionTransform { get { return lookDirectionTransform; } }
        public NpcContext NpcContext { get; set; } = new NpcContext();
        public IAiIterationAction CurrentIterationAction { get; set; }
        public NpcBaseActionContext CurrentAction { get; set; }
        public Animator Animator { get { return animator; } }
        public NavMeshAgent Agent { get { return this.agent; } }
        public Vector2Int Pos { get; set; }
		public IDamagedObject Target { get; set; }
        public EnemyBody EnemyBody { get; private set; }
        public GameObject WeaponObject { get; private set; }
        public bool IsEndStep { get; set; } = true;
        public virtual IWeapon Weapon { get; set; }
        public virtual Vector3 TargetAttackPos { get; set; }
        public virtual GameObject AttackCharacterObject { get { return this.gameObject; } }
        public AudioSource AttackAudioSource { get { return this.attackAudioSource; } }
        
        /// <summary>
        ///     Словарь предикторов по состоянию НПС
        ///     ---
        ///     
        /// </summary>
        protected IDictionary<NpcStateType, IPredictor> PredictorByState { get; set; }
        

        /// <summary>
        /// Параметры врага
        /// </summary>
        public IEnemy Enemy
        {
            get
            {
                if(EnemyBody == null)
                {
                    UpdateBody();
                }
                return EnemyBody.Enemy;
            }
            protected set
            {
                if (EnemyBody == null)
                {
                    UpdateBody();
                }
                EnemyBody.Enemy = value;
            }
        }

        #endregion

        public void StartNPC()
        {
            DoNextAction(); // Начинаем первое действие
            IsEndStep = false; // Стартуем логику разбора очереди действий
        }

        public void StopNPC()
        {
            IsEndStep = true; // Останавливаем логику разбора очереди действий
            
            var manager = ObjectFinder.Find<BattleManager>();
            manager.EnemyStepCompleted(this); // Помечаем в менеджере что NPC завершил ход
        }

        /// <summary>
        ///     Выполняет поиск предиктора по состоянию
        ///     ---
        ///     Performs predictor search by state
        /// </summary>
        /// <returns>
        ///     Предиктор, формирующий стратегию поведения при указанном состоянии
        ///     ---
        ///     Predictor that forms the strategy of behavior under the specified condition
        /// </returns>
        /// <exception cref="NotSupportedException">
        ///     Не удалось найти предиктора по состоянию
        ///     ---
        ///     It was not possible to find a predictor by state
        /// </exception>
        public IPredictor TryFindPredictor()
        {
            if (!PredictorByState.TryGetValue(NpcContext.Status.State, out var predictor))
                throw new NotSupportedException("npc state '" + NpcContext.Status.State + "' isn't supported!");
            return predictor;
        }

        public void LoadPredictors(IDictionary<NpcStateType, string> stateToPredictorName)
        {
            PredictorByState?.Clear();
            PredictorByState = new Dictionary<NpcStateType, IPredictor>();
            foreach (var entry in stateToPredictorName)
                PredictorByState[entry.Key] = NpcAIPredictor.Instance.Get(entry.Value);
        }
        
        public void EquipWeapon(IWeapon weapon)
        {
            var weaponPoint = EnemyBody?.WeaponPoint;
            weaponPoint.DestroyAllChilds();

            Weapon = weapon;
            Animator.SetInteger(AnimationKey.AttackTypeKey, (int)AttackType.Empty);

            if (weapon == null || weapon.ID == DataDictionary.Weapons.WEAPON_SYSTEM_HANDS)
            {
                this.WeaponObject = weaponPoint?.gameObject;
                Animator.SetInteger(AnimationKey.WeaponEquipKey, (int)WeaponType.Hands);
                return;
            }

            if (weaponPoint != null)
            {
                var weaponBody = GameObject.Instantiate(weapon.Prefab, weaponPoint);
                var weaponBehaviour = weaponBody.GetComponent<IWeaponBehaviour>();
                weaponBody.transform.localPosition = weaponBehaviour.PositionOffset;
                weaponBody.transform.localRotation = Quaternion.Euler(weaponBehaviour.RotationOffset);
                Destroy(weaponBody.GetComponent<LocationDroppedItemBehaviour>());
                this.WeaponObject = weaponBody;
            }

            Animator.SetInteger(AnimationKey.WeaponEquipKey, (int)weapon.WeaponType);
        }

        public List<Vector3> CalculatePath(Vector3 targetPoint)
        {
            var path = new NavMeshPath();
            if (!Agent.CalculatePath(targetPoint, path))
                return null;
            return path.corners.ToList();
        }

        protected virtual void UpdateBody()
        {
            this.body.transform.DestroyAllChilds();
            var bodyBehaviour = GameObject.Instantiate(NpcFactory.Instance.GetBody(id), this.body.transform);
            this.EnemyBody = bodyBehaviour.GetComponent<EnemyBody>();
            this.animator.avatar = EnemyBody.Avatar;
            this.animator.runtimeAnimatorController = EnemyBody.Controller;

            //TODO: FIXME: Костыль, нужно переделать на сеть коллидеров, обернув конечности в box collider
            var collider = this.gameObject.AddComponent<MeshCollider>();
            collider.sharedMesh = EnemyBody.MeshRenderer.sharedMesh;
        }

        #region Unity Events

        private void Awake()
        {
            OnStart();
        }

        private void Update()
        {
            OnUpdate();
        }

        #endregion

        public virtual void OnStart()
        {
            UpdateBody();
        }

        public virtual void OnUpdate()
        {
            if(Game.Instance.Runtime.Mode == Mode.Battle)
            {
                DoBattleIteration();
                return;
            }
            DoNormalIteration();
        }

        private void DoBattleIteration()
        {
            if (IsEndStep) // Уже походили
                return;

            if (CurrentAction == null) // Нет действий, заканчиваем ход
            {
                StopNPC();
                return;
            }

            // Сейчас ход NPC
            if (Game.Instance.Runtime.BattleContext.OrderIndex != EnemyGroup.PlayerGroup &&
                Game.Instance.Runtime.BattleContext.OrderIndex != EnemyGroup.AnotherPlayerGroup)
            {
                if (CurrentIterationAction.Iteration(this, CurrentAction, timestamp))
                    DoNextAction(); // Конец этого действия
            }
        }

        private void DoNormalIteration()
        {
            if (CurrentAction == null)
                return;

            if (CurrentIterationAction.Iteration(this, CurrentAction, timestamp))
                DoNextAction(); // Конец действия
        }

        private void DoNextAction()
        {
            if(CurrentIterationAction != null)
                CurrentIterationAction.End(this, CurrentAction, timestamp);

            if (NpcContext.Actions.Count == 0)
            {
                CurrentAction = null;
                CurrentIterationAction = null;
                StopNPC();
                return;
            }

            CurrentAction = NpcContext.Actions[0];
            CurrentIterationAction = AiIteratorFactory.Instance.GetIterationAction(CurrentAction.Action);

            if(CurrentIterationAction != null)
                CurrentIterationAction.Start(this, CurrentAction);

            timestamp = Time.time;
            NpcContext.Actions.RemoveAt(0);
        }

        public virtual void Died()
        {
            if (NpcContext.Status.IsDead)
                return;

            NpcContext.Status.IsDead = true;

            var manager = ObjectFinder.Find<BattleManager>();
            manager.RemoveEnemiesFromBattle(this);

            var dropController = ObjectFinder.Find<ItemsDropController>();
            dropController.Drop(transform.position, true, Enemy.Items); // Выкидываем предметы
            dropController.Drop(transform.position, true, Enemy.Weapons?.Where(weapon => !DataDictionary.Items.SYSTEM_ITEMS.Contains(weapon.ID)).ToArray());

            StopNPC();

            Animator.SetInteger(AnimationKey.DeadKey, 1);
        }

        public virtual void AddBattleExp(long value)
        {

        }

    }

}
