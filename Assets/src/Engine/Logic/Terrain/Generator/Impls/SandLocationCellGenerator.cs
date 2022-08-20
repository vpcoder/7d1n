using Engine.Data;

namespace Engine.Logic
{

    public class SandLocationCellGenerator : LocationCellGeneratorBase
    {

        /// <summary>
        /// Исследование местности
        /// </summary>
        private static ItemPercentInfo[] inspectItems = new[]
        {
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_DIRTY_WATER, 50, 500, 70),
        };

        /// <summary>
        /// Мелкая охота
        /// </summary>
        private static ItemPercentInfo[] huntItems = new[]
        {
            ItemPercentGenerator.Instance.Create(DataDictionary.Resources.RES_DIRTY_WATER, 50, 250, 70),
        };

        public SandLocationCellGenerator() : base(inspectItems, huntItems) { }

        public override BiomType Biom => BiomType.Sand;

    }

}
