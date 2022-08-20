using System;
using System.Linq;
using Engine.Data;
using Engine.Data.Factories;
using UnityEngine;

namespace Engine
{

    /// <summary>
    ///     Единица конвертации массы объектов
    ///     ---
    ///     Unit of conversion of objects mass
    /// </summary>
    public enum WeightUnitType
    {
        /// <summary>
        ///     Милиграммы
        ///     ---
        ///     Miligrams
        /// </summary>
        MILIGRAMS,
        
        /// <summary>
        ///     Граммы
        ///     ---
        ///     Grams
        /// </summary>
        GRAMS,
        
        /// <summary>
        ///     Килограммы
        ///     ---
        ///     Kilograms
        /// </summary>
        KILOGRAMS,
    }
    
    public static class WeightCalculationService
    {

        public static long GetMass(long value, WeightUnitType weightUnitType)
        {
            var mul = 1;
            switch (weightUnitType)
            {
                case WeightUnitType.GRAMS:
                    mul = 1000;
                    break;
                case WeightUnitType.KILOGRAMS:
                    mul = 1000000;
                    break;
            }
            return value * mul;
        }
        
        public static long GetWeight(this IEntity entity)
        {
            if (entity.StaticWeight)
                return entity.Weight;
            
            if (entity is IItem)
                return GetWeight((IItem)entity);

            return 0L;
        }

        public static long GetWeight(this IItem item)
        {
            if (item.StaticWeight || item is IResource)
                return item.Weight;
            
            var parts = item.Parts;
            if (Lists.IsEmpty(parts))
                return 0L;

            return parts.Select(o => GetWeight(o.ResourceID) * o.ResourceCount).Sum();
        }

        public static long GetWeight(long itemId)
        {
            return GetWeight(ItemFactory.Instance.Get(itemId));
        }

        /// <summary>
        ///     Вес в формате вывода '0.00 кг' для пользователя
        ///     ---
        ///     Weight in output format '0.00 kg' for the user
        /// </summary>
        /// <param name="weightMiligrams">
        ///     Вес предмета в миллиграммах
        ///     ---
        ///     Item weight in milligrams
        /// </param>
        /// <returns>
        ///     Минимальную единицу размера веса
        ///     Обозначение единицы веса
        ///     ---
        ///     The minimum unit of weight
        ///     Weight unit designation
        /// </returns>
        public static string GetWeightFormat(long weightMiligrams)
        {
            if (weightMiligrams < 1000L)
                return weightMiligrams + Localization.Instance.Get("msg_weight_mlg");

            if (weightMiligrams < 1000000L)
            {
                var valueGrams = Math.Truncate(weightMiligrams / 10d) / 100d;
                return string.Format("{0:N2}", valueGrams) + Localization.Instance.Get("msg_weight_g");
            }
            var valueKilograms = Math.Truncate(weightMiligrams / 10000d) / 100d;
            return string.Format("{0:N2}", valueKilograms) + Localization.Instance.Get("msg_weight_kg");
        }
        
    }
    
}