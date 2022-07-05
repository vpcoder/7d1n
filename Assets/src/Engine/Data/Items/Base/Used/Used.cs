using Engine.Data.Items.Used;
using System.Linq;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Используемый предмет
    /// ---
    /// The item being used
    /// 
    /// </summary>
    public class Used : CraftableItem, IUsed
    {

        /// <summary>
        ///     Действие при использовании предмета
        ///     Если действие возвращает true - значит предмет использован, и его нужно удалить
        ///     Если действие возвращает false - значит предмет остаётся в сумке, и его можно повторно использовать
        ///     ---
        ///     Action when an item is used
        ///     If the action returns true, then the item has been used and should be removed
        ///     If the action returns false - item is left in the bag and can be reused
        /// </summary>
        public IUseItemAction UseAction { get; set; }

        /// <summary>
        ///     Звук использования предмета
        ///     ---
        ///     The sound of using an item
        /// </summary>
        public string UseSoundType { get; set; }

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
            return new Used()
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
                Level = Level,
                Author = Author,

                UseAction = UseAction,
                UseSoundType = UseSoundType,
            };
        }

    }

}
