using System;
using System.Linq;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Предмет, который можно создать вручную
    /// ---
    /// An item that can be created manually
    /// 
    /// </summary>
    [Serializable]
    public class CraftableItem : Item, ICraftableItem
    {
        
        /// <summary>
        ///     Уровень качества созданного предмета
        ///     ---
        ///     The level of quality of the item created
        /// </summary>
        public long Level { get; set; }
        
        /// <summary>
        ///     Автор-создатель предмета
        ///     ---
        ///     Author-creator of the item
        /// </summary>
        public string Author { get; set; }

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
            return new CraftableItem()
            {
                ID = ID,
                ToolType = ToolType?.ToSet(),
                Type = Type,
                Name = Name,
                Description = Description,
                Count = Count,
                StackSize = StackSize,
                StaticWeight = StaticWeight,
                Weight = Weight,
                Parts = Parts?.ToList(),
                Level = Level,
                Author = Author,
            };
        }

    }

}
