using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Data
{

    [Serializable]
    public class CraftableItem : Item, ICraftableItem
    {
        public long Level { get; set; } = 0;
        public string Author { get; set; } = null;

        /// <summary>
        /// Копирует текущую сущность в новый экземпляр
        /// </summary>
        /// <returns>Копия сущности</returns>
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
                Weight = Weight,
                Parts = Parts?.ToList(),
                Level = Level,
                Author = Author,
            };
        }

    }

}
