using System;
using System.Linq;

namespace Engine.Data
{

    [Serializable]
    public class Cloth : CraftableItem, ICloth
    {
       /// <summary>
       /// Защита от 0 до 100 * 1000
       /// </summary>
        public int Protection { get; set; }

        /// <summary>
        /// Копирует текущую сущность в новый экземпляр
        /// </summary>
        /// <returns>Копия сущности</returns>
        public override IIdentity Copy()
        {
            return new Cloth()
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
                Protection = Protection,
            };
        }

    }

}
