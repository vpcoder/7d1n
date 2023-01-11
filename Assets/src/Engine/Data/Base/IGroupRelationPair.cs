namespace Engine.Data
{
    
    /// <summary>
    ///
    /// Различные параметры отношений между двумя группами.
    /// Здесь могут быть такие параметры как враждебность двух групп друг к другу.
    /// ---
    /// Different parameters of the relationship between the two groups.
    /// There may be parameters such as the hostility of the two groups toward each other.
    /// 
    /// </summary>
    public interface IGroupRelationPair
    {
        
        /// <summary>
        ///     Первая группа в паре
        ///     ---
        ///     The first group in the pairing
        /// </summary>
        EnemyGroup FirstGroup  { get; set; }
        
        /// <summary>
        ///     Вторая группа в паре
        ///     ---
        ///     The second group in the pairing
        /// </summary>
        EnemyGroup SecondGroup { get; set; }
        
        /// <summary>
        ///     Враждебность между группами
        ///     0 - Нейтрально
        ///    +1 - Дружба
        ///    -1 - Вражда
        ///     ---
        ///     Hostility between groups
        ///     0 - Neutral
        ///    +1 - Friendship
        ///    -1 - Enmity
        /// </summary>
        int Hostility { get; set; }

    }
    
}