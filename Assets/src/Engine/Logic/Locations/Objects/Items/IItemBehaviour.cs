using Engine.Data;

namespace Engine.Logic.Locations.Objects
{

    /// <summary>
    /// 
    /// Скрипт поведения объекта на локации
    /// ---
    /// The script of the object behavior on the location
    /// 
    /// </summary>
    public interface IItemBehaviour
    {

        /// <summary>
        ///     Экземпляр предмета
        ///     ---
        ///     A copy of the item
        /// </summary>
        IItem Item { get; set; }

        /// <summary>
        ///     Набор данных по которым будет создаваться экземпляр предмета Item
        ///     ---
        ///     The set of data by which an Item instance will be created
        /// </summary>
        ItemInfo ItemInfo { get; set; }

    }

}
