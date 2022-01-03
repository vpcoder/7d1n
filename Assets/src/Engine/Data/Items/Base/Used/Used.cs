using Engine.Data.Items.Used;
using System.Linq;

namespace Engine.Data
{

    /// <summary>
    /// Используемый предмет
    /// </summary>
    public class Used : CraftableItem, IUsed
    {

        /// <summary>
        /// Действие при использовании предмета
        /// Если действие возвращает true - значит предмет использован, и его нужно удалить
        /// Если действие возвращает false - значит предмет остаётся в сумке, и его можно повторно использовать
        /// </summary>
        public IUseItemAction UseAction { get; set; }

        /// <summary>
        /// Если true - вес не рассчитывается, а берётся из свойства (*data.xml) в описании предмета
        /// </summary>
       
        public bool StaticWeight { get; set; }

        /// <summary>
        /// Звук использования предмета
        /// </summary>
        public string UseSoundType { get; set; }

        /// <summary>
        /// Копирует текущую сущность в новый экземпляр
        /// </summary>
        /// <returns>Копия сущности</returns>
        public override IIdentity Copy()
        {
            return new Used()
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
                Level = Level,
                Author = Author,

                UseAction = UseAction,
                StaticWeight = StaticWeight,

                UseSoundType = UseSoundType,
            };
        }

    }

}
