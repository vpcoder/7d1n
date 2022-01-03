using Engine.Data;
using Engine.Data.Factories;
using System.Linq;

namespace Engine
{

    /// <summary>
    /// Сервис расчёта параметров предметов
    /// </summary>
    public static class EntityCalculationService
    {

        public static long GetWeight(this IEntity entity)
        {
            if(entity is IResource)
                return GetWeight((IResource)entity);

            if (entity is IUsed)
                return GetWeight((IUsed)entity);

            if (entity is IItem)
                return GetWeight((IItem)entity);

            return 0L;
        }

        public static long GetWeight(this IItem item)
        {
            if (item is IResource)
                return GetWeight((IResource)item);

            if (item is IUsed)
                return GetWeight((IUsed)item);

            var parts = item.Parts;

            if (parts == null || parts.Count == 0)
                return 0L;

            return parts.Select(o => GetWeight(o.ResourceID) * o.ResourceCount).Sum();
        }

        public static long GetWeight(this IResource res)
        {
            return res.Weight;
        }

        public static long GetWeight(this IUsed used)
        {
            if(used.StaticWeight)
                return used.Weight;

            var parts = used.Parts;

            if (parts == null || parts.Count == 0)
                return 0L;

            return parts.Select(o => GetWeight(o.ResourceID) * o.ResourceCount).Sum();
        }

        public static long GetWeight(long itemId)
        {
            return GetWeight(ItemFactory.Instance.Get(itemId));
        }

        /// <summary>
        /// Вес в формате вывода для пользователя
        /// </summary>
        /// <param name="weight">Вес предмета</param>
        /// <returns>
        /// Минимальную единицу размера веса
        /// Обозначение единицы веса
        /// </returns>
        public static string GetWeightFormat(long weight)
        {
            if (weight < 1000L)
                return weight + " " + Localization.Instance.Get("msg_weight_mlg");

            if (weight >= 1000L && weight < 1000000L)
                return (weight / 1000L) + " " + Localization.Instance.Get("msg_weight_g");

            return (weight / 1000000L) + " " + Localization.Instance.Get("msg_weight_kg");
        }

    }

}
