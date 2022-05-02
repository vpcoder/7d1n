using System;

namespace Engine.Generator
{

    public static class GeneratorAdditions
    {
        public static string ToLocalText(this LocationType type)
        {
            switch(type)
            {
                case LocationType.OpenSpace:   return Localization.Instance.Get("msg_location_open_space");
                case LocationType.Living:       return Localization.Instance.Get("msg_location_house");
                case LocationType.Stock:       return Localization.Instance.Get("msg_location_stock");
                case LocationType.SuperMarket: return Localization.Instance.Get("msg_location_super_market");
                case LocationType.TechStore:   return Localization.Instance.Get("msg_location_tech_store");
                case LocationType.Med:         return Localization.Instance.Get("msg_location_med");
                case LocationType.Hospital:    return Localization.Instance.Get("msg_location_hospital");
                case LocationType.Army:        return Localization.Instance.Get("msg_location_army");
                default:
                    return "?";
            }
        }

        public static string ToLocalText(this LocationSize type)
        {
            switch (type)
            {
                case LocationSize.Small:  return Localization.Instance.Get("msg_location_size_small");
                case LocationSize.Normal: return Localization.Instance.Get("msg_location_size_normal");
                case LocationSize.Large:  return Localization.Instance.Get("msg_location_size_large");
                default:
                    return "?";
            }
        }
    }

    public enum LocationSize : byte
    {
        Small, // Маленький размер
        Normal, // Средний размер
        Large, // Большой размер
    }

    public enum LocationType : int
    {
        OpenSpace   = 0x00, // Открытая местность

        Living      = 0x01, // Жилой дом
        Stock       = 0x02, // Склад
        SuperMarket = 0x03, // Продуктовый магазин/Супермаркет
        TechStore   = 0x04, // Магазин техники
        Med         = 0x05, // Аптека
        Hospital    = 0x06, // Больница
        Army        = 0x07, // Военное здание
    };

    public class LocationInfo
    {

        public long ID { get; set; }

        public int Height { get; set; }

        public LocationType Type { get; set; }

        public LocationSize Size { get; set; }

    }

    public static class LocationInfoGenerator
    {

        public const int MIN_BUILD_HEIGHT = 8;
        public const int MAX_BUILD_HEIGHT = 60;

        public static LocationInfo Generate(long id)
        {
            var random = new Random((int)id);
            var height = random.Next(MIN_BUILD_HEIGHT, MAX_BUILD_HEIGHT);
            var type = (LocationType)random.Next(1, UnityEngine.Enums<LocationType>.Count);
            var size = (LocationSize)random.Next(0, UnityEngine.Enums<LocationSize>.Count);

            return new LocationInfo()
            {
                ID = id,
                Height = height,
                Type = type,
                Size = size,
            };
        }

    }

}
