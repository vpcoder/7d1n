using Engine.Data.Factories;
using Engine.Data.Stories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Data
{

    [Serializable]
    public class InventoryStoryObject : IStoryObject
    {
        public long ID { get { return IDValue; } set { } }
        public long IDValue;

        public List<ItemInfo> Items;
    }

    public class Inventory : ICharacterStoredObjectSerializable<InventoryStoryObject>
    {

        public event Action Update;

        /// <summary>
        /// Предметы в сумках персонажа
        /// </summary>
        public List<IItem> Items { get; } = new List<IItem>();

        /// <summary>
        /// Вес всех предметов в сумках персонажа
        /// </summary>
        public long Weight
        {
            get
            {
                return Items.Sum(item => item.Weight * item.Count);
            }
        }

        #region Ctor

        public Inventory()
        {
            Update += OnUpdate;
        }

        #endregion

        #region Serialization

        public InventoryStoryObject CreateData()
        {
            var data = new InventoryStoryObject
            {
                IDValue = Game.Instance.Runtime.PlayerID,
                Items = Items.Select(ItemSerializator.Convert).ToList()
            };
            return data;
        }

        public void LoadFromData(InventoryStoryObject data)
        {
            Items.Clear();
            Items.AddRange(data.Items.Select(ItemSerializator.Convert));
        }

        #endregion

        private void OnUpdate()
        {
            CharacterStory.Instance.InventoryStory.Save(CreateData());
            CharacterStory.Instance.EquipmentStory.Save(Game.Instance.Character.Equipment.CreateData());
        }

        /// <summary>
        /// Ищет индекс предмета в сумке
        /// </summary>
        /// <param name="item">Искомый предмет</param>
        /// <returns>Индекс предмета если он найден, или -1 если предмет не найден</returns>
        public int TryFindIndex(IItem item)
        {
            if (item == null)
                return -1;
            return Items.IndexOf(item);
        }

        /// <summary>
        /// Получает предмет из сумки по индексу
        /// </summary>
        /// <param name="index">Индекс предмета</param>
        /// <returns>Предмет, если он найден по индексу, или null если предмет не найден</returns>
        public IItem GetByIndex(int index)
        {
            if (index < 0 || index >= Items.Count)
                return null;
            return Items[index];
        }

        /// <summary>
        /// Определяет, сколько указанных предметов есть в сумке
        /// </summary>
        /// <param name="itemId">Искомый предмет</param>
        /// <returns>Количество искомых предметов в сумке</returns>
        public long HasCount(long itemId)
        {
            return Items.ToList()
                .Where(o => o.ID == itemId)
                .Select(o => o.Count)
                .Sum();
        }

        public bool HasWithCount(IItem item)
        {
            return Has(item.ID, item.Count);
        }

        public bool HasWithoutCount(IItem item)
        {
            return Has(item.ID);
        }

        public bool Has(long itemId, long count = 1)
        {
            long hasCount = 0;
            foreach(var item in Items.ToList())
            {
                if(item.ID == itemId)
                {
                    if(item.Count < count)
                    {
                        hasCount += item.Count;
                    }
                    else
                    {
                        return true;
                    }
                    if(hasCount >= count)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool AddByAddress(IItem addItem)
        {
            return DoInUpdate(() =>
            {
                if (addItem.StackSize == 1) // Предмет без стека (поштучный)
                {
                    Items.Add(addItem); // Сразу добавляем его новым элементом в сумку
                    return true;
                }

                long count = addItem.Count; // Сколько предметов нам нужно разложить в сумке?
                foreach (var item in Items.ToList())
                {
                    if (item.ID == addItem.ID && item.Count < item.StackSize) // Нашли похожий предмет, у которого стек ещё не заполнен
                    {
                        long freeCount = item.StackSize - item.Count; // Вычисляем сколько свободных элементов в стеке
                        if (freeCount >= count) // Можно вместить все предметы?
                        {
                            item.Count += count;
                            return true;
                        }
                        else
                        {
                            count -= freeCount;
                            item.Count += freeCount; // Заполняем стек до конца
                        }

                        if (count == 0) // Разобрали все предметы?
                            return true;
                    }
                }

                addItem.Count = count; // Остались излишки, добавляем их отдельным предметом в сумке
                Items.Add(addItem);
                return true;
            });
        }

        public void Add(long itemId, long count)
        {
            var item = ItemFactory.Instance.Create(itemId, count);
            this.AddByAddress(item);
        }

        public void Add(IItem item)
        {
            this.AddByAddress((IItem)item.Copy());
        }

        public bool Remove(long itemId, long count = 1)
        {
            return DoInUpdate(() =>
            {
                foreach (var item in Items.ToList())
                {
                    if (item.ID == itemId)
                    {
                        long itemCount = item.Count;
                        if (itemCount >= count)
                        {
                            item.Count -= count;
                            if (item.Count == 0)
                            {
                                RemoveByAddress(item);
                            }
                            return true;
                        }
                        else
                        {
                            long delta = count - itemCount;
                            count -= delta;
                        }
                        if (count == 0)
                        {
                            return true;
                        }
                    }
                }
                return false;
            });
        }

        public bool RemoveByAddress(IItem item)
        {
            return DoInUpdate(() => Items.Remove(item));
        }
        
        /// <summary>
        /// Находит другой, точно такой же предмет как этот, и удаляет его из сумки
        /// </summary>
        /// <param name="item">Предмет по которому нужно искать другой предмет</param>
        /// <returns>true - если другой предмет удалось найти и удалить, false - иначе</returns>
        public bool RemoveOtherById(IItem item)
        {
            var remove = Items.Where(other => other != item && other.ID == item.ID).FirstOrDefault();
            if (remove == null)
                return false;

            return DoInUpdate(() => Items.Remove(remove));
        }

        private bool DoInUpdate(Func<bool> action)
        {
            bool result = false;

            try
            {
                result = action.Invoke();
            }
            finally
            {
                if(result)
                    Update?.Invoke();
            }

            return result;
        }

    }

}
