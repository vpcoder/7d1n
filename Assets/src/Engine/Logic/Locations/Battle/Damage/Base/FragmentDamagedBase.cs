using UnityEngine;

namespace Engine.Logic.Locations
{
    
    /// <summary>
    /// 
    /// Объект, способный получить урон и перенаправить на базовый IDamagedObject объект
    /// ---
    /// An object capable of taking damage and redirecting to a basic IDamagedObject object
    /// 
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public abstract class FragmentDamagedBase : MonoBehaviour, IFragmentDamaged
    {
        
        [SerializeField] private DamagedBase damaged;

        /// <summary>
        ///     Объект, который в конечном итоге будет агрегировать урон со всех частей
        ///     ---
        ///     The object that will eventually aggregate damage from all parts
        /// </summary>
        public IDamagedObject Damaged
        {
            get { return damaged; }
            set { damaged = (DamagedBase)value; }
        }
        
    }
    
}