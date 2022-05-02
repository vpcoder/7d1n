using UnityEngine;

namespace Engine.Logic.Locations
{

    public abstract class DamagedBase : MonoBehaviour, IDamagedObject
    {

        #region Hidden Fields

        [SerializeField] protected AudioSource damageAudioSource;

        #endregion

        #region Properties

        /// <summary>
        ///     Источник воспроизведения звуков, связанный с этой целью
        ///     ---
        ///     The source of sound reproduction associated with this target
        /// </summary>
        public virtual AudioSource DamageAudioSource { get { return damageAudioSource; } }

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
        ///     Здоровье/Состояние цели
        ///     ---
        ///     Health/target state
        /// </summary>
        public abstract int Health { get; set; }

        /// <summary>
        ///     Защита цели
        ///     ---
        ///     Protecting the target
        /// </summary>
        public abstract int Protection { get; }

        /// <summary>
        ///     Опыт, который выдаётся за уничтожение этого существа или объекта
        ///     ---
        ///     The experience given for destroying that creature or object
        /// </summary>
        public abstract long Exp { get; }

        /// <summary>
        ///     Ссылка на этот объект
        ///     ---
        ///     Link to this object
        /// </summary>
        public GameObject ToObject { get { return this.gameObject; } }

        #endregion

        #region Methods

        /// <summary>
        ///     Вызывается когда цель получает урон
        ///     ---
        ///     Called when the target takes damage
        /// </summary>
        public abstract void TakeDamage();

        #endregion

    }

}
