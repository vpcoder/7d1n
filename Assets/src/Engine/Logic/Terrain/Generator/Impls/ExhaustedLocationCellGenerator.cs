using System.Collections.Generic;
using Engine.Data;

namespace Engine.Logic
{

    public class ExhaustedLocationCellGenerator : LocationCellGeneratorBase
    {

        /// <summary>
        /// Исследование местности
        /// </summary>
        private static ItemPercentInfo[] inspectItems = new[]
        {
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_STONE, 50, 500, 70),
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_RAW_COOPER, 1, 20, 20),
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_RAW_IRON, 1, 20, 20),
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_RAW_LEAD, 1, 20, 20),
        };

        /// <summary>
        /// Мелкая охота
        /// </summary>
        private static ItemPercentInfo[] huntItems = new[]
        {
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_STONE, 50, 250, 70),
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_RAW_COOPER, 1, 10, 10),
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_RAW_IRON, 1, 10, 10),
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_RAW_LEAD, 1, 10, 10),

            ItemPercentGenerator.Instance.Create(DataDictionary.Foods.FOOD_RAW_ROACH, 1, 4, 30),
        };

        public ExhaustedLocationCellGenerator() : base(inspectItems, huntItems) { }

        public override BiomType Biom => BiomType.Exhausted;

    }

}
