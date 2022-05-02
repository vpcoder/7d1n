using System;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Набор информации о предмете для сериализации
    /// ---
    /// Set of information about the object for serialization
    /// 
    /// </summary>
    [Serializable]
    public class ItemInfo
    {
        public long ID;
        public long Count;
        public GroupType Type;

        public FirearmsInfo Firearms;
        public CraftableInfo Craftable;
    }

}
