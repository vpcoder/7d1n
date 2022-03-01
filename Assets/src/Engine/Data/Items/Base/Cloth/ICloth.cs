
namespace Engine.Data
{

    /// <summary>
    /// 
    /// Элемент одежды, который можно экипировать
    /// ---
    /// An item of clothing that can be outfitted
    /// 
    /// </summary>
    public interface ICloth : ICraftableItem
    {
        /// <summary>
        ///     Защита  от 0 до 100 * 1000
        ///     ---
        ///     Protection from 0 to 100 * 1000
        /// </summary>
        int Protection { get; set; }
    }

}
