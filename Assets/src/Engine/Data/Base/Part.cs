using System;

namespace Engine.Data
{

    ///<summary>
    /// Часть предмета
    /// часть содержит ресурс и его количество
    /// Все предметы состаят из каких либо частей (ресурсов)
    ///</summary>
    [Serializable]
    public struct Part
    {
        ///<summary>
        /// Идентификатор ресурса
        ///</summary>
        public long ResourceID;

        ///<summary>
        /// Количество ресурса
        ///</summary>
        public long ResourceCount;

        /// <summary>
        /// Сложность сбора/разбора этой группы ресурсов
        /// </summary>
        public long Difficulty;
    }

}
