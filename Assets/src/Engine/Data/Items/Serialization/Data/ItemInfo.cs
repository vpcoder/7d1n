using System;

namespace Engine.Data
{

    /// <summary>
    /// Набор информации о предмете для сериализации
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
