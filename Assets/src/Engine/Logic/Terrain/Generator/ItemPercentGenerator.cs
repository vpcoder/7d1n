using Engine.Data;
using Engine.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic
{

    public class ItemPercentInfo
    {
        public int Percent;
        public long ID;

        public int MinCount;
        public int MaxCount;
    }

    public class ItemPercentContext
    {
        /// <summary>
        /// Процентный вес
        /// </summary>
        public int MaxWeight;

        /// <summary>
        /// Предметы с процентами
        /// </summary>
        public List<ItemPercentInfo> Items;
    }

    public class ItemPercentGenerator
    {

        #region Singleton

        private static Lazy<ItemPercentGenerator> instance = new Lazy<ItemPercentGenerator>(() => new ItemPercentGenerator());
        public static ItemPercentGenerator Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private ItemPercentGenerator()
        {

        }

        #endregion

        public ItemPercentInfo Create(long id, int minCount, int maxCount, int percent)
        {
            return new ItemPercentInfo()
            {
                Percent = percent,
                ID = id,
                MinCount = minCount,
                MaxCount = maxCount,
            };
        }

        public ItemPercentInfo Create(long id, int count, int percent)
        {
            return new ItemPercentInfo()
            {
                Percent = percent,
                ID = id,
                MinCount = count,
                MaxCount = count,
            };
        }

        public ItemPercentContext CreateContext(IEnumerable<ItemPercentInfo> items)
        {
            var context = new ItemPercentContext();
            context.Items = items.ToList();
            context.Items.Sort((o0, o1) =>
            {
                return o1.Percent - o0.Percent;
            });
            context.MaxWeight = context.Items.Select(o => o.Percent).Sum();
            return context;
        }

        public List<ItemInfo> GetRandom(ItemPercentContext context, int count)
        {
            var list = new List<ItemInfo>(count);
            for (int i = 0; i < count; i++)
            {
                var item = GetRandom(context);
                if (item != null)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public ItemInfo GetRandom(ItemPercentContext context)
        {
            if(Lists.IsEmpty(context.Items))
                throw new NotSupportedException("Контекст не может быть с пустым списком!");

            int value = UnityEngine.Random.Range(0, context.MaxWeight);
            int sum = 0;
            foreach(var info in context.Items)
            {
                sum += info.Percent;
                if(value < sum)
                    return Create(info);
            }
            return Create(context.Items.LastOrDefault());
        }

        private ItemInfo Create(ItemPercentInfo info)
        {
            var count = UnityEngine.Random.Range(info.MinCount, info.MaxCount + 1);
            if (count <= 0)
                return null;

            var item = ItemFactory.Instance.Create(info.ID, count);
            return ItemSerializator.Convert(item);
        }

    }

}
