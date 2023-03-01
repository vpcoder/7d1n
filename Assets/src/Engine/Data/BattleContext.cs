using System.Collections.Generic;
using System.Linq;

namespace Engine.Data
{

    /// <summary>
    ///
    /// Контекст боя
    /// ---
    /// Combat Context
    /// 
    /// </summary>
    public class BattleContext
    {

        #region Properties

        /// <summary>
        ///     ОД у нашего игрока
        ///     ---
        ///     Our player's AP
        /// </summary>
        public int CurrentCharacterAP { get; set; }

        /// <summary>
        ///     Индекс группы текущего хода
        ///     ---
        ///     Index of the current stroke group
        /// </summary>
        public OrderGroup OrderIndex = OrderGroup.PlayerGroup;

        /// <summary>
        ///     Всего групп в бою
        ///     ---
        ///     Total groups in battle
        /// </summary>
        public readonly IList<OrderGroup> Order = new List<OrderGroup>();

        /// <summary>
        ///     Отношения между группами
        ///     ---
        ///     Relationships between groups
        /// </summary>
        public readonly IGroupRelationships Relations = new GroupRelationships();

        #endregion
        
        public void SetOrder(IEnumerable<OrderGroup> values)
        {
            Order.Clear();
            Order.AddRange(values.ToList());
            OrderIndex = Order[0];
        }

    }

}
