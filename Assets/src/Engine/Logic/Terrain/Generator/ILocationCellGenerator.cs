using Engine.Data;
using System.Collections.Generic;

namespace Engine.Logic
{

    public interface ILocationCellGenerator
    {

        /// <summary>
        /// Тип Биома местности
        /// </summary>
        BiomType Biom { get; }

        /// <summary>
        /// Осмотр местности
        /// </summary>
        /// <param name="info">Информация о местности</param>
        /// <returns>Сгенерированные предметы</returns>
        List<ItemInfo> Inspect(LocalCellInfo info);

        /// <summary>
        /// Мелкая охота
        /// </summary>
        /// <param name="info">Информация о местности</param>
        /// <returns>Сгенерированные предметы</returns>
        List<ItemInfo> Hunt(LocalCellInfo info);

    }

}
