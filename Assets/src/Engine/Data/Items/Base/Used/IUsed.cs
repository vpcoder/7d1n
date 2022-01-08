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
        /// Действие при использовании предмета
        /// Если действие возвращает true - значит предмет использован, и его нужно удалить
        /// Если действие возвращает false - значит предмет остаётся в сумке, и его можно повторно использовать
        /// ---
        /// Action when an item is used
        /// If the action returns true, then the item has been used and should be removed
        /// If the action returns false - item is left in the bag and can be reused
        /// </summary>
        IUseItemAction UseAction { get; set; }

        /// <summary>
        /// Если true - вес не рассчитывается, а берётся из свойства (*data.xml) в описании предмета
        /// ---
        /// If true - the weight is not calculated, but taken from the property (*data.xml) in the item description
        /// </summary>
        bool StaticWeight { get; set; }

        /// <summary>
        /// Звук использования предмета
        /// ---
        /// The sound of using an item
        /// </summary>
        string UseSoundType { get; set; }

    }

}
