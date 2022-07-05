using Engine.Data.Items.Used;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Используемый предмет
    /// ---
    /// The item being used
    /// 
    /// </summary>
    public interface IUsed : ICraftableItem
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
        IUseItemAction UseAction { get; set; }

        /// <summary>
        ///     Звук использования предмета
        ///     ---
        ///     The sound of using an item
        /// </summary>
        string UseSoundType { get; set; }

    }

}
