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
        public bool IsEndTurn { get; set; } = true;
        public virtual IWeapon Weapon { get; set; }
        public virtual Vector3 TargetAttackPos { get; set; }
        public virtual GameObject AttackCharacterObject => gameObject;

        public AudioSource AttackAudioSource
        {
            get { return attackAudioSource; }
            set { attackAudioSource = value; }
        }

        /// <summary>
        ///     Словарь предикторов для текущего НПС по состояниям
        ///     ---
        ///     Dictionary of predictors for the current NPC by state
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

        /// <summary>
        ///     Запускает алгоритм разбора запланированных действий у данного НПС
        ///     ---
        ///     Launches the algorithm of the planned actions of the given NPC
        /// </summary>
        public void StartNPC()
        {
#if UNITY_EDITOR && BATTLE_DEBUG
            Debug.Log("npc " + CharacterBody.Character.ID + " name: " + transform.name + " start actions");
#endif
            
            // Начинаем первое действие в очереди действий
            // Starting the first action in the action queue
            DoNextAction();
            
            // Стартуем логику разбора очереди действий
            // Let's start the logic of the action queue parsing
            IsEndTurn = false;
        }

        /// <summary>
        ///     Останавливает алгоритм разбора запланированных действий у данного НПС,
        ///     так же, выполняет сброс текщих запланированных действий
        ///     ---
        ///     Stops the algorithm of the planned actions of the given NPC,
        ///     Also, resets the current scheduled actions
        /// </summary>
        public void StopNPC()
        {
#if UNITY_EDITOR && BATTLE_DEBUG
            Debug.Log("npc " + CharacterBody.Character.ID + " name: " + transform.name + " stop actions");
#endif
            
            // Останавливаем логику разбора очереди действий
            // Stopping the logic of the parsing queue of actions
            IsEndTurn = true;
            
            // Помечаем в менеджере что NPC завершил ход
            // Mark in the manager that the NPC has completed his turn
            var manager = ObjectFinder.Find<BattleManager>();
            if(manager != null)
                manager.NpcTurnCompleted(this);

            // Сбрасываем все запланированные задачи
            // Resetting all scheduled tasks
            ClearActions();
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

        /// <summary>
        ///     Метод заставляет экипировать оружие в руки НПС
        ///      - Предмет появляется в руке
        ///      - Персонаж включает соответствующую анимацию для данного предмета в руках
        ///     ---
        ///     The method forces the weapon into the hands of the NPC
        ///      - The item appears in the hand
        ///      - The character turns on the appropriate animation for the item in his hand
        /// </summary>
        /// <param name="weapon">
        ///     Оружие, которое необходимо экипировать.
        ///     Если null - считается что необходимо использовать голые руки
        ///     ---
        ///     Weapon to equip.
        ///     If null - it is considered that it is necessary to use bare arms
        /// </param>
        /// <exception cref="ArgumentException">
        ///     Бросается если у персонажа не задан WeaponPoint
        ///     ---
        ///     Thrown if the character does not have WeaponPoint set
        /// </exception>
        public void EquipWeapon(IWeapon weapon)
        {
            Weapon = weapon;
            Animator.SetCharacterDoAttackType(AttackType.Empty);

            var weaponPoint = CharacterBody == null ? null : CharacterBody.WeaponPoint;
            if (weaponPoint == null)
            {
#if UNITY_EDITOR && DEBUG
                Debug.LogError("weapon point for '" + GetType().FullName + "' is empty");
                return;
#endif
                throw new ArgumentException("weapon point for '" + GetType().FullName + "' is empty");
            }
            
            // Убираем из рук предыдущие оружия, если они там были
            // Remove previous weapons from hands, if there were any
            weaponPoint.DestroyAllChilds();
            
            if (weapon == null || DataDictionary.Items.IsSystemHands(weapon.ID))
            {
                // Устанавливаем объект оружия - пустую руку. Это позволит отлавливать столкновения пустой руки с получателями урона
                // Set the weapon object - the empty hand. This will allow to catch collisions of the empty hand with damage recipients
                this.WeaponObject = weaponPoint == null ? null : weaponPoint.gameObject;
                Animator.SetCharacterEquipWeaponType(WeaponType.Hands);
                return;
            }
            
            var weaponBody = GameObject.Instantiate(weapon.Prefab, weaponPoint);
            var weaponBehaviour = weaponBody.GetComponent<IWeaponBehaviour>();
            weaponBody.transform.localPosition = weaponBehaviour.PositionOffset;
            weaponBody.transform.localRotation = Quaternion.Euler(weaponBehaviour.RotationOffset);
            
            // Тело нпс может быть отмасштабированно, в таком случае нужно рассчитать обратный масштаб предмета в его руках
            // The body of the NPC can be scaled, in which case you must calculate the inverse mastchab of the object in his hands
            var rootScale = CharacterBody.Root.localScale;
            weaponBody.transform.localScale = new Vector3(1f / rootScale.x,
                                                          1f / rootScale.y,
                                                          1f / rootScale.z);
            
            // Мы взяли в руки полноценный предмет локации, чтобы не плодить одинаковых объектов для локации и для рук,
            // просто убираем LocationDroppedItemBehaviour, чтобы предмет нельзя было забрать из рук, как это происходит
            // с предметами разбросанными по локации
            // we took a full-fledged location item in our hands, so that we don't have to multiply the same objects for the location and for the hands,
            // just remove LocationDroppedItemBehaviour, so the item can't be taken out of the hands, like it happens
            // with items scattered on the location
            Destroy(weaponBody.GetComponent<LocationDroppedItemBehaviour>());
            
            this.WeaponObject = weaponBody;
            Animator.SetCharacterEquipWeaponType(weapon.WeaponType);
        }

        /// <summary>
        ///     Просит выполнить расчёт пути от текущей точки до целевой targetPoint
        ///     ---
        ///     It asks to calculate the path from the current point to the targetPoint
        /// </summary>
        /// <param name="targetPoint">
        ///     Целевая точка пути, в которую необходимо прийти
        ///     ---
        ///     Target point of the path to be reached
        /// </param>
        /// <returns>
        ///     Последовательность точек, формирующих путь до целевой точки targetPoint
        ///     null - если путь недостижим (невозможно прийти в целевую точку, что то мешает)
        ///     ---
        ///     The sequence of points which form the path to the target point targetPoint
        ///     null - if the path is unreachable (it is impossible to reach the target point, something is blocking it)
        /// </returns>
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
            switch (Game.Instance.Runtime.Mode)
            {
                case Mode.Battle:
                    DoBattleIteration();
                    break;
                case Mode.Dialog:
                    DoDialogIteration();
                    break;
                case Mode.Game:
                    DoNormalIteration();
                    break;
            }
        }

        private void DoBattleIteration()
        {
            // Уже походили?
            // Have been turn yet?
            if (IsEndTurn)
                return;

            // Нет действий, заканчиваем ход
            // No action, end turn
            if (CurrentAction == null)
            {
                StopNPC();
                return;
            }

            // Сейчас ход NPC
            // It's the NPC's turn now
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
                return; // TODO: Обращение к предиктору за новой порцией действий!

            if (CurrentIterationAction.Iteration(this, CurrentAction, timestamp))
                DoNextAction();
        }
        
        private void DoDialogIteration()
        {
            if (CurrentAction == null)
                return;

            if (CurrentIterationAction.Iteration(this, CurrentAction, timestamp))
                DoNextAction();
        }

        private void DoNextAction()
        {
#if UNITY_EDITOR && BATTLE_DEBUG
            Debug.Log("npc " + CharacterBody.Character.ID + " name: " + transform.name + " next action [" + CharacterContext.Actions.Count + "]");
#endif
            
            if(CurrentIterationAction != null)
                CurrentIterationAction.End(this, CurrentAction, timestamp);

            if (CharacterContext.Actions.Count == 0)
            {
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
            if(manager != null)
                manager.RemoveEnemiesFromBattle(this);

            // Выкидываем предметы на карту
            // Throw items on the map
            DropNpcItems();

            // нпс принудительно заканчивает все свои действия
            // NPC forcibly ends all his actions
            StopNPC();

            Animator.SetCharacterDeadType(DeatType.Dead);
        }

        public virtual void AddBattleExp(long value)
        {

        }

        private void DropNpcItems()
        {
            var pos = transform.position;
            var dropController = ObjectFinder.Find<ItemsDropController>();
            dropController.Drop(pos, true, Character.Items);
            dropController.Drop(pos, true, Character.Weapons?
                .Where(weapon => DataDictionary.Items.IsNotSystemItem(weapon.ID))
                .Select(weapon => (IItem)weapon)
                .ToArray());
            Character.Items.Clear();
            Character.Weapons?.Clear();
        }

        private void ClearActions()
        {
            CharacterContext.Actions.Clear();
            CurrentAction = null;
            CurrentIterationAction = null;
        }

    }

}
