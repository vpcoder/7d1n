using System.Collections.Generic;
using Engine.Logic.Locations;
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
                // Zombies Against All
                Create(OrderGroup.ZombieGroup, OrderGroup.PlayerGroup, -100),
                Create(OrderGroup.ZombieGroup, OrderGroup.AnotherPlayerGroup, -100),
                Create(OrderGroup.ZombieGroup, OrderGroup.DeceasedGroup, -100),
                Create(OrderGroup.ZombieGroup, OrderGroup.MaraudersGroup, -100),
                Create(OrderGroup.ZombieGroup, OrderGroup.WildAnimalsGroup, -100),
                Create(OrderGroup.ZombieGroup, OrderGroup.ReconstructionistsGroup, -100),
                Create(OrderGroup.ZombieGroup, OrderGroup.ScythiansGroup, -100),
                Create(OrderGroup.ZombieGroup, OrderGroup.TechnocratsGroup, -100),
                Create(OrderGroup.ZombieGroup, OrderGroup.NewLightGroup, -100),
                
                // Дикие животные против всех
                // Wild Animals Against All
                Create(OrderGroup.WildAnimalsGroup, OrderGroup.PlayerGroup, -100),
                Create(OrderGroup.WildAnimalsGroup, OrderGroup.AnotherPlayerGroup, -100),
                Create(OrderGroup.WildAnimalsGroup, OrderGroup.DeceasedGroup, -100),
                Create(OrderGroup.WildAnimalsGroup, OrderGroup.MaraudersGroup, -100),
                Create(OrderGroup.WildAnimalsGroup, OrderGroup.ReconstructionistsGroup, -100),
                Create(OrderGroup.WildAnimalsGroup, OrderGroup.ScythiansGroup, -100),
                Create(OrderGroup.WildAnimalsGroup, OrderGroup.TechnocratsGroup, -100),
                Create(OrderGroup.WildAnimalsGroup, OrderGroup.NewLightGroup, -100),
                
                // Усопшие - нейтральные с остальными
                // The deceased are neutral with the rest
                Create(OrderGroup.DeceasedGroup, OrderGroup.PlayerGroup, 0),
                Create(OrderGroup.DeceasedGroup, OrderGroup.AnotherPlayerGroup, 0),
                Create(OrderGroup.DeceasedGroup, OrderGroup.ReconstructionistsGroup, 0),
                Create(OrderGroup.DeceasedGroup, OrderGroup.ScythiansGroup, 0),
                Create(OrderGroup.DeceasedGroup, OrderGroup.TechnocratsGroup, 0),
                Create(OrderGroup.DeceasedGroup, OrderGroup.NewLightGroup, 0),
                
                // Мородёры против остальных
                // The Morochers Against Rest of Us
                Create(OrderGroup.MaraudersGroup, OrderGroup.PlayerGroup, -100),
                Create(OrderGroup.MaraudersGroup, OrderGroup.AnotherPlayerGroup, -100),
                Create(OrderGroup.MaraudersGroup, OrderGroup.DeceasedGroup, -100),
                Create(OrderGroup.MaraudersGroup, OrderGroup.ReconstructionistsGroup, -100),
                Create(OrderGroup.MaraudersGroup, OrderGroup.TechnocratsGroup, -100),
                Create(OrderGroup.MaraudersGroup, OrderGroup.NewLightGroup, -100),
                // К скифам относятся нейтрально
                // The Scythians are treated neutrally
                Create(OrderGroup.MaraudersGroup, OrderGroup.ScythiansGroup, 0),
                
                // Новый свет против мародёров и технократов
                // New Light Against Morochers and Technocrats
                Create(OrderGroup.NewLightGroup, OrderGroup.TechnocratsGroup, -100),
                Create(OrderGroup.NewLightGroup, OrderGroup.MaraudersGroup, -100),
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
        public IGroupRelationPair GetRelation(OrderGroup first, OrderGroup second)
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

        private IGroupRelationPair Create(OrderGroup first, OrderGroup second, int hostility)
        {
            return new GroupRelationPair()
            {
                FirstGroup = first,
                SecondGroup = second,
                Hostility = hostility,
            };
        }
        
        public bool IsEnemies(OrderGroup first, OrderGroup second)
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
        
        public bool IsFriends(OrderGroup first, OrderGroup second)
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

        public bool IsNeutral(OrderGroup first, OrderGroup second)
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