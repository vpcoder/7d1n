using System.Collections.Generic;

namespace Engine.Data
{



    ///<summary>
    /// Предмет/объект
    ///</summary>
    public interface IItem : IEntity
    {

        ///<summary>
        /// Части из которых состоит предмет
        ///</summary>
        List<Part> Parts { get; set; }

        ///<summary>
        /// Количество этого предмета
        ///</summary>
        long Count { get; set; }

        ///<summary>
        /// Максимальное количество прежметов в пачке
        ///</summary>
        long StackSize { get; set; }

    }

}
