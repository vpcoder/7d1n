using System.Collections.Generic;
using Engine.Data;

namespace Engine.Logic
{

    public class WoodenLocationCellGenerator : LocationCellGeneratorBase
    {

        /// <summary>
        /// Исследование местности
        /// </summary>
        private static ItemPercentInfo[] inspectItems = new[]
        {
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_WOOD, 50, 500, 70),
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_RAW_IRON, 1, 20, 20),
        };

        /// <summary>
        /// Мелкая охота
        /// </summary>
        private static ItemPercentInfo[] huntItems = new[]
        {
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_WOOD, 50, 250, 70),
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_RAW_IRON, 1, 10, 10),

            ItemPercentGenerator.Instance.Create(DataDictionary.Foods.FOOD_RAW_LARVAE, 1, 3, 30),
        };

        public WoodenLocationCellGenerator() : base(inspectItems, huntItems) { }

        public override BiomType Biom => BiomType.Wooden;

    }

}
