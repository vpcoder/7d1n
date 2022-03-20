
namespace Engine.Data.Generation
{

    /// <summary>
    /// 
    /// Элемент генерации
    /// (В нём нахогдятся все объекты, из которых составляется база сцены - остов)
    /// ---
    /// The generation element
    /// (This element contains all the objects that make up the base of the scene - the carcass)
    /// 
    /// </summary>
    /// <typeparam name="E">
    ///     Енум с типами стилей, для данного элемента
    ///     ---
    ///     Enum with style types, for this item
    /// </typeparam>
    public interface IElementIdentity<E> where E : struct
    {

        /// <summary>
        ///     ИД элемента стиля
        ///     (Уникален в рамках одного E)
        ///     ---
        ///     The ID of the style element
        ///     (Unique within one E)
        /// </summary>
        long ID { get; set; }

        /// <summary>
        ///     Стиль генерации
        ///     ---
        ///     Generation style
        /// </summary>
        E Type { get; set; }

    }

}
