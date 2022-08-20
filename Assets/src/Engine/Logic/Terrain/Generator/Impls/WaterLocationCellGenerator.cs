using Engine.Data;

namespace Engine.Logic
{

    /// <summary>
    /// Генерация предметов в водном биоме
    /// </summary>
    public class WaterLocationCellGenerator : LocationCellGeneratorBase
    {

        /// <summary>
        /// Исследование местности
        /// </summary>
        private static ItemPercentInfo[] inspectItems = new[]
        {
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_DIRTY_WATER, 50, 500, 70),
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_RAW_LEAD, 1, 20, 10),
        };

        /// <summary>
        /// Мелкая охота
        /// </summary>
        private static ItemPercentInfo[] huntItems = new[]
        {
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_DIRTY_WATER, 50, 250, 70),
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_RAW_LEAD, 1, 10, 10),

            ItemPercentGenerator.Instance.Create(DataDictionary.Foods.FOOD_RAW_FRY_FISH, 1, 3, 30),
        };

        public WaterLocationCellGenerator() : base(inspectItems, huntItems) { }

        public override BiomType Biom => BiomType.Water;
    }

}
