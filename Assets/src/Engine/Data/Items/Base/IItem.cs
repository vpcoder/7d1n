using System.Collections.Generic;

namespace Engine.Data
{

    ///<summary>
    ///
    /// Предмет/объект
    /// ---
    /// Item/object
    /// 
    ///</summary>
    public interface IItem : IEntity
    {

        ///<summary>
        ///     Части из которых состоит предмет
        ///     ---
        ///     The parts that make up the item
        ///</summary>
        List<Part> Parts { get; set; }

        ///<summary>
        ///     Количество этого предмета в текущей пачке
        ///     ---
        ///     The amount of this item in the current pack
        ///</summary>
        long Count { get; set; }

        ///<summary>
        ///     Максимальное количество предметов в пачке
        ///     ---
        ///     Maximum number of items in one pack
        ///</summary>
        long StackSize { get; set; }

    }

}
