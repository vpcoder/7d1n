using System.Collections.Generic;

namespace Engine.Data
{

    public class BattleContext
    {

        /// <summary>
        /// ОД у нашего игрока
        /// </summary>
        public int CurrentCharacterAP { get; set; } = 0;

        /// <summary>
        /// Индекс группы текущего хода
        /// </summary>
        public EnemyGroup OrderIndex = EnemyGroup.PlayerGroup;

        public int OrderIndexInt = 0;

        /// <summary>
        /// Всего групп в бою
        /// </summary>
        public readonly List<EnemyGroup> Order = new List<EnemyGroup>();

        public void SetOrder(IEnumerable<EnemyGroup> values)
        {
            Order.Clear();
            Order.AddRange(values);
            OrderIndex = Order[0];
        }

    }

}
