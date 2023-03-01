namespace Engine.Data
{
    
    /// <summary>
    ///
    /// Контекст взаимоотношений между группами
    /// ---
    /// Context of the relationship between the groups
    /// 
    /// </summary>
    public interface IGroupRelationships
    {

        /// <summary>
        ///     Находит параметры отношений между группами first и second
        ///     ---
        ///     Finds the parameters of the relationship between groups first and second
        /// </summary>
        /// <param name="first">
        ///     Первая группа, для которой нужно найти параметры отношений со второй группой second
        ///     ---
        ///     The first group, for which you need to find the parameters of the relationship with the second group
        /// </param>
        /// <param name="second">
        ///     Вторая группа, для которой нужно найти параметры отношений с первой группой first
        ///     ---
        ///     The second group, for which you need to find the parameters of the relationship with the first group
        /// </param>
        /// <returns>
        ///     Объект с параметрами отношений между группами
        ///     ---
        ///     Object with parameters of relations between groups
        /// </returns>
        IGroupRelationPair GetRelation(OrderGroup first, OrderGroup second);

        
        /// <summary>
        ///     Определяет, являются ли указанные групы first и second враждебными друг к другу
        ///     ---
        ///     Determines whether the specified groups are first and secondarily hostile to each other
        /// </summary>
        /// <param name="first">
        ///     Первая группа, у которой проверяется враждебность ко второй группе second
        ///     ---
        ///     The first group, in which hostility to the second group is tested
        /// </param>
        /// <param name="second">
        ///     Вторая группа, у которой проверяется враждебность к первой группе first
        ///     ---
        ///     The second group, in which hostility to the first group is tested
        /// </param>
        /// <returns>
        ///     true - если группы враждуют между собой,
        ///     иначе - false
        ///     ---
        ///     true - if groups are feuding with each other,
        ///     otherwise - false
        /// </returns>
        bool IsEnemies(OrderGroup first, OrderGroup second);
        bool IsEnemies(IGroupRelationPair relationPair);
        bool IsEnemies(int hostility);
        
        /// <summary>
        ///     Определяет, являются ли указанные групы first и second дружественными друг к другу
        ///     ---
        ///     Determines whether the specified groups are first and second friendly to each other
        /// </summary>
        /// <param name="first">
        ///     Первая группа, у которой проверяется дружественность ко второй группе second
        ///     ---
        ///     The first group, in which the friendliness of the second group is tested
        /// </param>
        /// <param name="second">
        ///     Вторая группа, у которой проверяется дружественность к первой группе first
        ///     ---
        ///     The second group, which checks the friendliness of the first group
        /// </param>
        /// <returns>
        ///     true - если группы дружат между собой,
        ///     иначе - false
        ///     ---
        ///     true - if the groups are friends with each other,
        ///     otherwise - false
        /// </returns>
        bool IsFriends(OrderGroup first, OrderGroup second);
        bool IsFriends(IGroupRelationPair relationPair);
        bool IsFriends(int hostility);
        
        /// <summary>
        ///     Определяет, являются ли указанные групы first и second нейтральными друг к другу
        ///     ---
        ///     Determines whether the specified groups are first and second neutral to each other
        /// </summary>
        /// <param name="first">
        ///     Первая группа, у которой проверяется нейтральность ко второй группе second
        ///     ---
        ///     The first group, in which neutrality to the second group is tested
        /// </param>
        /// <param name="second">
        ///     Вторая группа, у которой проверяется нейтральность к первой группе first
        ///     ---
        ///     The second group, which tests neutrality to the first group
        /// </param>
        /// <returns>
        ///     true - если группы нейтральны друг к другу,
        ///     иначе - false
        ///     ---
        ///     true - if the groups are neutral to each other,
        ///     otherwise - false
        /// </returns>
        bool IsNeutral(OrderGroup first, OrderGroup second);
        bool IsNeutral(IGroupRelationPair relationPair);
        bool IsNeutral(int hostility);

    }
    
}