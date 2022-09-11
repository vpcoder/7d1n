namespace Engine.Data
{

    /// <summary>
    ///
    /// Эмоциональное состояние NPC персонажа
    /// ---
    /// Emotional state of the NPC character
    /// 
    /// </summary>
    public enum NpcStateType
    {
        
        /// <summary>
        ///     Сонный, ничего не подозревающий, находится в расслабленном состоянии
        ///     ---
        ///     Sleepy, unsuspecting, in a relaxed state
        /// </summary>
        Unsuspecting,
        
        /// <summary>
        ///     Обычное состояние, в меру активен, не особо готов к бою
        ///     ---
        ///     Normal condition, moderately active, not really ready to fight
        /// </summary>
        Normal,
        
        /// <summary>
        ///     Настороженное состояние, сильно активен, может принять бой, но с незначительными проблемами
        ///     ---
        ///     Cautionary state, strongly active, can take a fight, but with little trouble
        /// </summary>
        Nervous,
        
        /// <summary>
        ///     В боевом состоянии, но не видит цели, может вступить в бой, без проблем
        ///     ---
        ///     In combat, but does not see the target, can engage, no problem
        /// </summary>
        Searching,
        
        /// <summary>
        ///     Состояние максимальной готовности к любой ситуации, ведёт бой
        ///     ---
        ///     A state of maximum readiness for any situation, leads the fight
        /// </summary>
        Fighting,

    };

}