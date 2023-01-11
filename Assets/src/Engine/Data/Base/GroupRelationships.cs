using System.Collections.Generic;
using JetBrains.Annotations;

namespace Engine.Data
{
    
    /// <summary>
    ///
    /// Контекст взаимоотношений между группами
    /// ---
    /// Context of the relationship between the groups
    /// 
    /// </summary>
    public class GroupRelationships : IGroupRelationships
    {

        #region Hidden Fields

        /// <summary>
        ///     Набор отношений между парой групп
        ///     ---
        ///     A set of relationships between a pair of groups
        /// </summary>
        private readonly ICollection<IGroupRelationPair> relationPairs;

        #endregion
        
        #region Ctors
        
        public GroupRelationships()
        {
            relationPairs = new LinkedList<IGroupRelationPair>(new[]
            {
                // Зомби против всех
                Create(EnemyGroup.ZombieGroup, EnemyGroup.PlayerGroup, -100),
                Create(EnemyGroup.ZombieGroup, EnemyGroup.AnotherPlayerGroup, -100),
                Create(EnemyGroup.ZombieGroup, EnemyGroup.DeceasedGroup, -100),
                Create(EnemyGroup.ZombieGroup, EnemyGroup.MaraudersGroup, -100),
                Create(EnemyGroup.ZombieGroup, EnemyGroup.WildAnimalsGroup, -100),
                Create(EnemyGroup.ZombieGroup, EnemyGroup.ReconstructionistsGroup, -100),
                Create(EnemyGroup.ZombieGroup, EnemyGroup.ScythiansGroup, -100),
                Create(EnemyGroup.ZombieGroup, EnemyGroup.SprintersGroup, -100),
                Create(EnemyGroup.ZombieGroup, EnemyGroup.TechnocratsGroup, -100),
                Create(EnemyGroup.ZombieGroup, EnemyGroup.NewLightGroup, -100),
                
                // Дикие животные против всех
                Create(EnemyGroup.WildAnimalsGroup, EnemyGroup.PlayerGroup, -100),
                Create(EnemyGroup.WildAnimalsGroup, EnemyGroup.AnotherPlayerGroup, -100),
                Create(EnemyGroup.WildAnimalsGroup, EnemyGroup.DeceasedGroup, -100),
                Create(EnemyGroup.WildAnimalsGroup, EnemyGroup.MaraudersGroup, -100),
                Create(EnemyGroup.WildAnimalsGroup, EnemyGroup.ReconstructionistsGroup, -100),
                Create(EnemyGroup.WildAnimalsGroup, EnemyGroup.ScythiansGroup, -100),
                Create(EnemyGroup.WildAnimalsGroup, EnemyGroup.SprintersGroup, -100),
                Create(EnemyGroup.WildAnimalsGroup, EnemyGroup.TechnocratsGroup, -100),
                Create(EnemyGroup.WildAnimalsGroup, EnemyGroup.NewLightGroup, -100),
                
                // Усопшие - нейтральные с остальными
                Create(EnemyGroup.DeceasedGroup, EnemyGroup.PlayerGroup, 0),
                Create(EnemyGroup.DeceasedGroup, EnemyGroup.AnotherPlayerGroup, 0),
                Create(EnemyGroup.DeceasedGroup, EnemyGroup.MaraudersGroup, 0),
                Create(EnemyGroup.DeceasedGroup, EnemyGroup.ReconstructionistsGroup, 0),
                Create(EnemyGroup.DeceasedGroup, EnemyGroup.ScythiansGroup, 0),
                Create(EnemyGroup.DeceasedGroup, EnemyGroup.SprintersGroup, 0),
                Create(EnemyGroup.DeceasedGroup, EnemyGroup.TechnocratsGroup, 0),
                Create(EnemyGroup.DeceasedGroup, EnemyGroup.NewLightGroup, 0),
                
                // Мородёры против одиночек
                Create(EnemyGroup.MaraudersGroup, EnemyGroup.PlayerGroup, -1),
                Create(EnemyGroup.MaraudersGroup, EnemyGroup.AnotherPlayerGroup, -1),
                Create(EnemyGroup.MaraudersGroup, EnemyGroup.SprintersGroup, -1),
            });
        }
        
        #endregion
        
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
        public IGroupRelationPair GetRelation(EnemyGroup first, EnemyGroup second)
        {
            if (first == second)
                return null;
            
            foreach (var pair in relationPairs)
            {
                if ((pair.FirstGroup == first && pair.SecondGroup == second) ||
                    (pair.FirstGroup == second && pair.SecondGroup == first))
                    return pair;
            }

            var newRelation = Create(first, second, 0);
            relationPairs.Add(newRelation);
            return newRelation;
        }

        private IGroupRelationPair Create(EnemyGroup first, EnemyGroup second, int hostility)
        {
            return new GroupRelationPair()
            {
                FirstGroup = first,
                SecondGroup = second,
                Hostility = hostility,
            };
        }

        
        public bool IsEnemies(EnemyGroup first, EnemyGroup second)
        {
            return IsEnemies(GetRelation(first, second));
        }
        public bool IsEnemies([NotNull] IGroupRelationPair relationPair)
        {
            return IsEnemies(relationPair.Hostility);
        }
        public bool IsEnemies(int hostility)
        {
            return hostility < 0;
        }
        
        public bool IsFriends(EnemyGroup first, EnemyGroup second)
        {
            return IsFriends(GetRelation(first, second));
        }
        public bool IsFriends([NotNull] IGroupRelationPair relationPair)
        {
            return IsFriends(relationPair.Hostility);
        }
        public bool IsFriends(int hostility)
        {
            return hostility > 0;
        }

        public bool IsNeutral(EnemyGroup first, EnemyGroup second)
        {
            return IsNeutral(GetRelation(first, second));
        }
        public bool IsNeutral([NotNull] IGroupRelationPair relationPair)
        {
            return IsNeutral(relationPair.Hostility);
        }
        public bool IsNeutral(int hostility)
        {
            return hostility == 0;
        }
        
    }
}