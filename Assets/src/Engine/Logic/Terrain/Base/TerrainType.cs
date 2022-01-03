
namespace Engine.Logic
{

    /// <summary>
    /// Тип местности
    /// </summary>
    public enum TerrainType : int
    {
        /// <summary>
        /// Крупный город
        /// </summary>
        City,

        /// <summary>
        /// Небольшой город
        /// </summary>
        Town,

        /// <summary>
        /// Отдельно стоящее здание
        /// </summary>
        OneHouse,

        /// <summary>
        /// Дорога у небольшого города
        /// </summary>
        TownRoad,

        /// <summary>
        /// Дорога, рядом нет домов
        /// </summary>
        Road,

        /// <summary>
        /// Дорога, и один дом у дороги
        /// </summary>
        OneHouseRoad,

        /// <summary>
        /// Чистое поле
        /// </summary>
        Field,
    };

}
