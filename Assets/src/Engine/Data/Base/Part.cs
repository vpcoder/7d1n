using System;
using System.Collections.Generic;

namespace Engine.Data
{

    ///<summary>
    /// 
    /// Часть предмета
    /// часть содержит ресурс и его количество
    /// Все предметы состаят из каких либо частей (ресурсов)
    /// ---
    /// Part of the item
    /// The part contains a resource and its quantity
    /// All items are composed of some parts (resources)
    /// 
    ///</summary>
    [Serializable]
    public class Part
    {

        ///<summary>
        ///     Идентификатор ресурса
        ///     ---
        ///     Resource ID
        ///</summary>
        public long ResourceID;

        ///<summary>
        ///     Количество ресурса
        ///     ---
        ///     Amount of resource
        ///</summary>
        public long ResourceCount;

        /// <summary>
        ///     Сложность сбора/разбора этой группы ресурсов
        ///     ---
        ///     The complexity of collecting/disassembling this group of resources
        /// </summary>
        public long Difficulty;

        /// <summary>
        ///     Инструменты, которыми можно извлечь эту часть предмета.
        ///     Достаточно одного предмета из списка.
        ///     ---
        ///     The tools with which this part of the object can be extracted.
        ///     One item from the list is enough.
        /// </summary>
        public ISet<ToolType> NeededTools;
        
    }

}
