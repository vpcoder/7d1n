using UnityEngine;

namespace Engine.Logic.Locations.Objects
{

    /// <summary>
    /// 
    /// Базовый неживой объект, который может быть уничтожен
    /// ---
    /// Basic inanimate object that can be destroyed
    /// 
    /// </summary>
    public abstract class DestroyedBase : MonoBehaviour, IDestroyedObject
    {

        #region Hidden Fields

        /// <summary>
        ///     Текущий уровень состояния объекта
        ///     ---
        ///     Current state level of the object
        /// </summary>
        [SerializeField] private int health = 10;

        /// <summary>
        ///     Текущий уровень защиты объекта
        ///     ---
        ///     Current level of object protection
        /// </summary>
        [SerializeField] private int protection = 0;

        /// <summary>
        ///     Количество опыта, получаемого игроками за уничтожение этого объекта
        ///     ---
        ///     The amount of experience players get for destroying this object
        /// </summary>
        [SerializeField] private long exp = 0;

        /// <summary>
        ///     Флаг определяющий, был ли объект уничтожен или нет
        ///     ---
        ///     Flag determining whether the object was destroyed or not
        /// </summary>
        protected bool isDestroyed = false;

        #endregion

        #region Properties

        /// <summary>
        ///     Состояние объекта. Если состояние уменьшится до 0, считается что объект уничтожен
        ///     ---
        ///     The state of the object. If the state decreases to 0, the object is considered destroyed
        /// </summary>
        public virtual int Health
        {
            get
            {
                return health;
            }
            set
            {
                this.health = value;
            }
        }

        /// <summary>
        ///     Защита объекта
        ///     ---
        ///     Object protection
        /// </summary>
        public virtual int Protection { get { return protection; } }

        /// <summary>
        ///     Опыт, получаемый за уничтожение объекта
        ///     ---
        ///     Experience gained for destroying an object
        /// </summary>
        public virtual long Exp => exp;

        /// <summary>
        ///     Источник воспроизведения звуков, связанный с этой целью
        ///     ---
        ///     The source of sound reproduction associated with this target
        /// </summary>
        public virtual AudioSource DamageAudioSource { get { return null; } }

        /// <summary>
        ///     Кем то уже получен опыт за него?
        ///     ---
        ///     Is someone already experienced for him?
        /// </summary>
        public bool ExpGeted
        {
            get;
            set;
        } = false;

        /// <summary>
        ///     Ссылка на этот объект
        ///     ---
        ///     Link to this object
        /// </summary>
        public GameObject ToObject { get { return this.gameObject; } }

        #endregion

        #region Methods

        /// <summary>
        ///     Вызывается когда необходимо уничтожить объект
        ///     ---
        ///     Called when it is necessary to destroy an object
        /// </summary>
        public virtual void DoDestroy()
        {
            GameObject.Destroy(gameObject);
        }

        /// <summary>
        ///     Вызывает проверку состояния объекта, может спровоцировать уничтожение этого объекта
        ///     ---
        ///     Causes a check of the object's state, can provoke the destruction of this object
        /// </summary>
        public virtual void CheckDestroy()
        {
            if (Health > 0 || isDestroyed)
                return;

            isDestroyed = false;
            DoDestroy();
        }

        /// <summary>
        ///     Вызывается когда цель получает урон
        ///     ---
        ///     Called when the target takes damage
        /// </summary>
        public virtual void TakeDamage()
        {
            // По умолчанию ничего не делаем, в дальнейшем, тут можно наносить повреждения объекту
        }

        #endregion

    }

}
