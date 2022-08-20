using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Data
{

    ///<summary>
    ///
    /// Предмет/объект
    /// ---
    /// Item/object
    /// 
    ///</summary>
    [Serializable]
    public class Item : Entity, IItem
    {

        ///<summary>
        ///     Части из которых состоит предмет
        ///     ---
        ///     The parts that make up the item
        ///</summary>
        public List<Part> Parts { get; set; } = new List<Part>();

        ///<summary>
        ///     Количество этого предмета в текущей пачке
        ///     ---
        ///     The amount of this item in the current pack
        ///</summary>
        public long Count { get; set; }

        ///<summary>
        ///     Максимальное количество предметов в пачке
        ///     ---
        ///     Maximum number of items in one pack
        ///</summary>
        public long StackSize { get; set; } = 1;

        /// <summary>
        ///     Копирует текущую сущность в новый экземпляр
        ///     ---
        ///     Copies the current entity into a new instance
        /// </summary>
        /// <returns>
        ///     Копия сущности
        ///     ---
        ///     Entity Copy
        /// </returns>
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
                StaticWeight = StaticWeight,
                Weight = Weight,
                Parts = Parts?.ToList(),
            };
        }

    }

}
