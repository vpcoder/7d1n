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
    public abstract class EnemyNpcBehaviour : MonoBehaviour,
                                              IAttackObject,
                                              IMonoBehaviourOverrideStartEvent,
                                              IMonoBehaviourOverrideUpdateEvent
    {
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
        public Transform LookDirectionTransform => lookDirectionTransform;
        public CharacterContext CharacterContext { get; set; } = new CharacterContext();
        public IAiIterationAction CurrentIterationAction { get; set; }
        public NpcBaseActionContext CurrentAction { get; set; }
        public Animator Animator => animator;
        public NavMeshAgent Agent => agent;
        public Vector2Int Pos { get; set; }
		public IDamagedObject Target { get; set; }
        public CharacterBody CharacterBody { get; private set; }
        public GameObject WeaponObject { get; private set; }
        public bool IsEndStep { get; set; } = true;
        public virtual IWeapon Weapon { get; set; }
        public virtual Vector3 TargetAttackPos { get; set; }
        public virtual GameObject AttackCharacterObject => gameObject;
        public AudioSource AttackAudioSource => attackAudioSource;

        /// <summary>
        ///     Словарь предикторов по состоянию НПС
        ///     ---
        ///     
        /// </summary>
        protected IDictionary<NpcStateType, IPredictor> PredictorByState { get; set; }
        

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
            if (!PredictorByState.TryGetValue(CharacterContext.Status.State, out var predictor))
                throw new NotSupportedException("npc state '" + CharacterContext.Status.State + "' isn't supported!");
            return predictor;
        }

        public void EquipWeapon(IWeapon weapon)
        {
            var weaponPoint = CharacterBody?.WeaponPoint;
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
            this.CharacterBody = characterBody;
            this.animator.avatar = CharacterBody.Avatar;
            this.animator.runtimeAnimatorController = CharacterBody.Controller;

            //TODO: FIXME: Костыль, нужно переделать на сеть коллидеров, обернув конечности в box collider
            var collider = this.gameObject.GetComponent<Collider>();
            if (collider == null)
            {
                collider = this.gameObject.AddComponent<MeshCollider>();
                ((MeshCollider)collider).sharedMesh = CharacterBody.MeshRenderer.sharedMesh;
            }
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

            var manager = ObjectFinder.Find<BattleManager>();
            manager.RemoveEnemiesFromBattle(this);

            var dropController = ObjectFinder.Find<ItemsDropController>();
            dropController.Drop(transform.position, true, Character.Items); // Выкидываем предметы
            dropController.Drop(transform.position, true, Character.Weapons?.Where(weapon => !DataDictionary.Items.SYSTEM_ITEMS.Contains(weapon.ID)).ToArray());

            StopNPC();

            Animator.SetInteger(AnimationKey.DeadKey, 1);
        }

        public virtual void AddBattleExp(long value)
        {

        }

    }

}
