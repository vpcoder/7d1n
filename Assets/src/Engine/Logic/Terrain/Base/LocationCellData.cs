using Engine.Data;
using System;
using System.Collections.Generic;

namespace Engine.Logic
{

    [Serializable]
    public class LocationCellData
    {

        /// <summary>
        /// Информация о ячейке
        /// </summary>
        public LocalCellInfo Info;

        /// <summary>
        /// Предметы выкинутые в ячейке
        /// </summary>
        public List<ItemInfo> Items;

    }

}
