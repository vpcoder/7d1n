using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Data
{

    ///<summary>
    /// Базовый класс предмета/объекта
    ///</summary>
    [Serializable]
    public class Item : Entity, IItem
    {

        ///<summary>
        /// Части из которых состоит предмет
        ///</summary>
        public List<Part> Parts { get; set; } = new List<Part>();

        ///<summary>
        /// Количество этого предмета
        ///</summary>
        public long Count { get; set; }

        ///<summary>
        /// Максимальное количество прежметов в пачке
        ///</summary>
        public long StackSize { get; set; } = 1;

        /// <summary>
        /// Копирует текущую сущность в новый экземпляр
        /// </summary>
        /// <returns>Копия сущности</returns>
        public override IIdentity Copy()
        {
            return new Item()
            {
                ID = ID,
                ToolType = ToolType,
                Type = Type,
                Name = Name,
                Description = Description,
                Count = Count,
                StackSize = StackSize,
                Weight = Weight,
                Parts = Parts?.ToList(),
            };
        }

    }

}
