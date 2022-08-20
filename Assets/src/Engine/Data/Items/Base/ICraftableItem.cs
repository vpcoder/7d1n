
namespace Engine.Data
{

    /// <summary>
    /// 
    /// Предмет, который можно создать вручную
    /// ---
    /// An item that can be created manually
    /// 
    /// </summary>
    public interface ICraftableItem : IItem
    {

        /// <summary>
        ///     Уровень качества созданного предмета
        ///     ---
        ///     The level of quality of the item created
        /// </summary>
        long Level { get; set; }

        /// <summary>
        ///     Автор-создатель предмета
        ///     ---
        ///     Author-creator of the item
        /// </summary>
        string Author { get; set; }

    }

}
