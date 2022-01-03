using System;
using System.Collections.Generic;

namespace Engine.Data
{

    /// <summary>
    /// Ресурс
    /// </summary>
    [Serializable]
    public class Resource : Entity, IResource
    {

        ///<summary>
        /// Группа Resource
        ///</summary>
        public override GroupType Type { get { return GroupType.Resource; } set { } }

        ///<summary>
        /// Не инструмент
        ///</summary>
        public override ToolType ToolType { get { return ToolType.None; } set { } }

        ///<summary>
        /// Части из которых состоит ресурс
        ///</summary>
        public List<Part> Parts { get { return null; } set { } }

        ///<summary>
        /// Количество этого ресурса
        ///</summary>
        public long Count { get; set; }

        ///<summary>
        /// Максимальное количество ресурсов в пачке
        ///</summary>
        public long StackSize { get; set; }

        /// <summary>
        /// Копирует текущую сущность в новый экземпляр
        /// </summary>
        /// <returns>Копия сущности</returns>
        public override IIdentity Copy()
        {
            return new Resource()
            {
                ID = ID,
                Name = Name,
                Description = Description,
                Count = Count,
                Weight = Weight,
                StackSize = StackSize,
            };
        }

    }

}
