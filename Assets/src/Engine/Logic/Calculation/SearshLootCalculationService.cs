using Engine.Data;

namespace Engine
{

    /// <summary>
    /// Сервис расчётов поиска лута
    /// </summary>
    public static class SearshLootCalculationService
    {

        /// <summary>
        /// Богатство ячейки индекса в процентах
        /// </summary>
        /// <param name="value">Значение которое сгенерировали (от 0 до 100)</param>
        /// <returns>Результирующее значение богатства в процентах (от 0 до 255)</returns>
        public static byte GetWealth(byte value)
        {
            return value;
        }

        /// <summary>
        /// Число предметов в ячейке, которые мы сейчас будим искать
        /// </summary>
        public static int GetInspectItemsCount()
        {
            int level = (int)(Game.Instance.Character.Exps.LootExperience.Level / 10);
            if(level > 0)
                level = UnityEngine.Random.Range(level / 2, level + 1);
            int count = UnityEngine.Random.Range(4, 9);
            return level + count;
        }

        /// <summary>
        /// Число предметов в ячейке, которые мы сейчас будим добывать мелкой охотой
        /// </summary>
        public static int GetHuntItemsCount()
        {
            int level = (int)(Game.Instance.Character.Exps.LootExperience.Level / 10);
            if (level > 0)
                level = UnityEngine.Random.Range(level / 2, level + 1);
            int count = UnityEngine.Random.Range(2, 6);
            return level + count;
        }

    }

}
