using Engine.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic
{

    public abstract class LocationCellGeneratorBase : ILocationCellGenerator
    {

        /// <summary>
        /// Контекст геренации предметов при исследовании местности
        /// </summary>
        private ItemPercentContext InspectContext { get; }

        /// <summary>
        /// Контекст генерации предметов при мелкой охоте
        /// </summary>
        private ItemPercentContext HuntContext { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="inspectItems">Контекст генерации предметов при исследовании местности</param>
        /// <param name="huntItems">Контекст генерации предметов при мелкой охоте</param>
        public LocationCellGeneratorBase(IEnumerable<ItemPercentInfo> inspectItems, IEnumerable<ItemPercentInfo> huntItems)
        {
            InspectContext = ItemPercentGenerator.Instance.CreateContext(inspectItems);
            HuntContext = ItemPercentGenerator.Instance.CreateContext(huntItems);
        }

        /// <summary>
        /// Тип Биома местности
        /// </summary>
        public abstract BiomType Biom { get; }

        /// <summary>
        /// Осмотр местности
        /// </summary>
        /// <param name="info">Информация о местности</param>
        /// <returns>Сгенерированные предметы</returns>
        public List<ItemInfo> Inspect(LocalCellInfo info)
        {
            var count = Mathf.RoundToInt(SearshLootCalculationService.GetInspectItemsCount() * (info.Wealth / 100f));
            return ItemPercentGenerator.Instance.GetRandom(InspectContext, count);
        }

        /// <summary>
        /// Мелкая охота
        /// </summary>
        /// <param name="info">Информация о местности</param>
        /// <returns>Сгенерированные предметы</returns>
        public List<ItemInfo> Hunt(LocalCellInfo info)
        {
            int count = Mathf.RoundToInt(SearshLootCalculationService.GetHuntItemsCount() * (info.Wealth / 100f));
            return ItemPercentGenerator.Instance.GetRandom(HuntContext, count);
        }

    }

}
