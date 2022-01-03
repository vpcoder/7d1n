
namespace Engine.Data
{

    public interface ICloth : ICraftableItem
    {
        /// <summary>
        /// Защита  от 0 до 100 * 1000
        /// </summary>
        int Protection { get; set; }
    }

}
