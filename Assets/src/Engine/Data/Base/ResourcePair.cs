using System;

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
    public struct ResourcePair
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

    }

}
