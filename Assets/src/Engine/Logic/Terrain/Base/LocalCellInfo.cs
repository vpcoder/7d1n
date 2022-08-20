using System;

namespace Engine.Logic
{

    /// <summary>
    /// Информация о ячейке локации
    /// </summary>
    [Serializable]
    public struct LocalCellInfo
    {

        /// <summary>
        /// Положение ячейки по X
        /// </summary>
        public int PosX;

        /// <summary>
        /// Положение ячейки по Y
        /// </summary>
        public int PosY;

        /// <summary>
        /// Положение биома по X
        /// </summary>
        public int BiomPosX;

        /// <summary>
        /// Положение биома по Y
        /// </summary>
        public int BiomPosY;

        /// <summary>
        /// Биом, в котором находится эта ячейка
        /// </summary>
        public BiomType Biom;

        /// <summary>
        /// Богатство ячейки (от 0 до 100)
        /// </summary>
        public byte Wealth;

        /// <summary>
        /// Время когда ячейка была сгенерированна
        /// </summary>
        public long Timestamp;

    }

}
