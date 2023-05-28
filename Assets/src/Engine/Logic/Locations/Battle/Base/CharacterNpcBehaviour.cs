using System;
using Engine.Data;
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
    public abstract class CharacterNpcBehaviour : MonoBehaviour,
                                              IAttackObject,
                                              IMonoBehaviourOverrideStartEvent,
                                              IMonoBehaviourOverrideUpdateEvent
    {
        [SerializeField] protected DamagedBase damaged;
        [SerializeField] protected CharacterBody characterBody;
        [SerializeField] protected Animator animator;
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected AudioSource attackAudioSource;
        [SerializeField] protected Transform lookDirectionTransform;
        [SerializeField] protected long id;
        [SerializeField] private Transform eye;
        [SerializeField] private CharacterMeshSwitcher meshSwitcher;
        
        private float timestamp;

        #region Events

        public event Action DeadEvent;

        #endregion
        
        #region Shared Properties

        public IDamagedObject Damaged
        {
            get { return damaged; }
            set { damaged = (DamagedBase)value; }
        }
        
        public CharacterMeshSwitcher MeshSwitcher
        {
            get { return meshSwitcher; }
            set { meshSwitcher = value; }
        }
        public Transform Eye
        {
            get { return eye;}
            set { eye = value; }
        }

        public Transform LookDirectionTransform
        {
            get { return lookDirectionTransform; }
            set { lookDirectionTransform = value; }
        }
        public CharacterContext CharacterContext { get; set; } = new CharacterContext();
        public IAiIterationAction CurrentIterationAction { get; set; }
        public NpcBaseActionContext CurrentAction { get; set; }

        public Animator Animator
        {
            get { return animator; }
            set { animator = value; }
        }
        public NavMeshAgent Agent
        {
            get { return agent; }
            set { agent = value; }
        }
        public Vector2Int Pos { get; set; }
		public IDamagedObject Target { get; set; }

        public CharacterBody CharacterBody
        {
            get { return characterBody; }
            set { characterBody = value; }
        }
        public GameObject WeaponObject { get; set; }
        public bool IsEndStep { get; set; } = true;
        public virtual IWeapon Weapon { get; set; }
        public virtual Vector3 TargetAttackPos { get; set; }
        public virtual GameObject AttackCharacterObject => gameObject;

        public AudioSource AttackAudioSource
        {
            get { return attackAudioSource; }
            set { attackAudioSource = value; }
        }

        /// <summary>
        ///     Словарь предикторов по состоянию НПС
        ///     ---
        ///     
        /// </summary>
        protected IDictionary<CharacterStateType, IPredictor> PredictorByState { get; set; }
        

        /// <summary>
        ///     Параметры врага
        ///     ---
        ///     
        /// </summary>
        public ICharacter Character
        {
            get
            {
                if(CharacterBody == null)
                {
                    UpdateBody();
                }
                return CharacterBody.Character;
            }
            protected set
            {
                if (CharacterBody == null)
                {
                    UpdateBody();
                }
                CharacterBody.Character = value;
            }
        }

        #endregion

        public void StartNPC()
        {
            // Начинаем первое действие в очереди действий
            // Starting the first action in the action queue
            DoNextAction();
            
            // Стартуем логику разбора очереди действий
            // Let's start the logic of the action queue parsing
            IsEndStep = false;
        }

        public void StopNPC()
        {
            // Останавливаем логику разбора очереди действий
            // Stopping the logic of the parsing queue of actions
            IsEndStep = true;
            
            // Помечаем в менеджере что NPC завершил ход
            // Mark in the manager that the NPC has completed his turn
            var manager = ObjectFinder.Find<BattleManager>();
            if(manager != null)
                manager.EnemyStepCompleted(this);
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
            if (!PredictorByState.TryGetValue(CharacterContext.Status.State, out var predictor))
                throw new NotSupportedException("npc state '" + CharacterContext.Status.State + "' isn't supported!");
            return predictor;
        }

        public void EquipWeapon(IWeapon weapon)
        {
            var weaponPoint = CharacterBody?.WeaponPoint;
            weaponPoint.DestroyAllChilds();

            Weapon = weapon;
            Animator.SetCharacterDoAttackType(AttackType.Empty);

            if (weapon == null || DataDictionary.Items.IsSystemHands(weapon.ID))
            {
                this.WeaponObject = weaponPoint?.gameObject;
                Animator.SetCharacterEquipWeaponType(WeaponType.Hands);
                return;
            }

            if (weaponPoint != null)
            {
                var weaponBody = GameObject.Instantiate(weapon.Prefab, weaponPoint);
                var weaponBehaviour = weaponBody.GetComponent<IWeaponBehaviour>();
                weaponBody.transform.localPosition = weaponBehaviour.PositionOffset;
                weaponBody.transform.localRotation = Quaternion.Euler(weaponBehaviour.RotationOffset);
                var rootScale = CharacterBody.Root.localScale;
                weaponBody.transform.localScale = new Vector3(1f / rootScale.x,
                                                              1f / rootScale.z,
                                                              1f / rootScale.z);
                Destroy(weaponBody.GetComponent<LocationDroppedItemBehaviour>());
                this.WeaponObject = weaponBody;
            }

            Animator.SetCharacterEquipWeaponType(weapon.WeaponType);
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
#if UNITY_EDITOR
            if (Animator == null)
                Debug.LogError("character animator can't be null! " + transform.name);
            if(CharacterBody == null)
                Debug.LogError("character body can't be null! " + transform.name);
            if (Animator == null || CharacterBody == null)
                return;
#endif
            Animator.avatar = CharacterBody.Avatar;
            Animator.runtimeAnimatorController = CharacterBody.Controller;
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
            if (Game.Instance.Runtime.BattleContext.OrderIndex != OrderGroup.PlayerGroup &&
                Game.Instance.Runtime.BattleContext.OrderIndex != OrderGroup.AnotherPlayerGroup)
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

            if (CharacterContext.Actions.Count == 0)
            {
                CurrentAction = null;
                CurrentIterationAction = null;
                StopNPC();
                return;
            }

            CurrentAction = CharacterContext.Actions[0];
            CurrentIterationAction = AiIteratorFactory.Instance.GetIterationAction(CurrentAction.Action);

            if(CurrentIterationAction != null)
                CurrentIterationAction.Start(this, CurrentAction);

            timestamp = Time.time;
            CharacterContext.Actions.RemoveAt(0);
        }

        public virtual void Died()
        {
            if (CharacterContext.Status.IsDead)
                return;

            DeadEvent?.Invoke();
            CharacterContext.Status.IsDead = true;
            CharacterContext.Status.IsEnabledAI = false;
            
            var manager = ObjectFinder.Find<BattleManager>();
            manager.RemoveEnemiesFromBattle(this);

            var pos = transform.position;
            var dropController = ObjectFinder.Find<ItemsDropController>();
            dropController.Drop(pos, true, Character.Items); // Выкидываем предметы
            dropController.Drop(pos, true, Character.Weapons?
                .Where(weapon => DataDictionary.Items.IsNotSystemItem(weapon.ID))
                .Select(weapon => (IItem)weapon)
                .ToArray());

            StopNPC();

            Animator.SetCharacterDeadType(DeatType.Dead);
        }

        public virtual void AddBattleExp(long value)
        {

        }

    }

}
