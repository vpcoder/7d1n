namespace Engine.Logic.Locations
{
    
    /// <summary>
    /// 
    /// Объект, способный получить урон и перенаправить на базовый IDamagedObject объект
    /// ---
    /// An object capable of taking damage and redirecting to a basic IDamagedObject object
    /// 
    /// </summary>
    public interface IFragmentDamaged
    {
        
        /// <summary>
        ///     Объект, который в конечном итоге будет агрегировать урон со всех частей
        ///     ---
        ///     The object that will eventually aggregate damage from all parts
        /// </summary>
        IDamagedObject Damaged { get; }
        
    }
    
}