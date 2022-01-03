using Engine.Data.Items.Used;
using System;


namespace Engine.Data
{

    /// <summary>
    /// Используемый предмет
    /// </summary>
    public interface IUsed : ICraftableItem
    {

        /// <summary>
        /// Действие при использовании предмета
        /// Если действие возвращает true - значит предмет использован, и его нужно удалить
        /// Если действие возвращает false - значит предмет остаётся в сумке, и его можно повторно использовать
        /// </summary>
        IUseItemAction UseAction { get; set; }

        /// <summary>
        /// Если true - вес не рассчитывается, а берётся из свойства (*data.xml) в описании предмета
        /// </summary>
        bool StaticWeight { get; set; }

        /// <summary>
        /// Звук использования предмета
        /// </summary>
        string UseSoundType { get; set; }

    }

}
