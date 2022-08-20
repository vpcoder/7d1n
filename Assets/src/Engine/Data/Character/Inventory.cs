using Engine.Data.Factories;
using Engine.Data.Stories;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

        public event Action Save;

        #region Properties
        
        /// <summary>
        ///     Предметы в сумках персонажа.
        ///     Не менять содержимое коллекции напрямую! Использовать только методы Add, Remove и прочие, находящиеся здесь в инвентаре!
        ///     ---
        ///     Items in character bags.
        ///     Do not change the contents of the collection directly! Use only the Add, Remove, and other methods found here in the inventory!
        /// </summary>
        public List<IItem> Items { get; } = new List<IItem>();

        /// <summary>
        ///     Общий вес всех предметов в сумках персонажа
        ///     ---
        ///     Total weight of all items in the character's bags
        /// </summary>
        public long Weight
        {
            get
            {
                return Items.Sum(item => item.Weight * item.Count);
            }
        }

        #endregion
        
        #region Ctor

        public Inventory()
        {
            Save += OnSave;
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
            if(data.Items != null)
                Items.AddRange(data.Items.Select(ItemSerializator.Convert));
        }

        #endregion

        private void OnSave()
        {
            CharacterStory.Instance.InventoryStory.Save(CreateData());
            CharacterStory.Instance.EquipmentStory.Save(Game.Instance.Character.Equipment.CreateData());
        }

        
        /// <summary>
        ///     В инвентаре есть хотябы один из перечисленных инструментов
        ///     ---
        ///     At least one of these tools is in the inventory
        /// </summary>
        /// <param name="tools">
        ///     Набор инструментов которые ищем в инвентаре
        ///     ---
        ///     A set of tools that we are looking for in the inventory
        /// </param>
        /// <returns>
        ///     true - если в инвентаре нашёлся хотябы один указанный инструмент
        ///     false - если в инвентаре нет ни одного указанного инструмента
        ///     ---
        ///     true - if at least one specified tool is found in the inventory
        ///     false - if no specified tool is in the inventory
        /// </returns>
        public bool HasAnyTools(ISet<ToolType> tools)
        {
            if (Sets.IsEmpty(tools))
                return true;
            
            if (Lists.IsEmpty(Items))
                return false;
            
            foreach (var item in Items)
            {
                if (Sets.HasIntersect(item.ToolType, tools))
                    return true;
            }
            return false;
        }

        /// <summary>
        ///     Выполняет поиск инструментов tools в инвентаре
        ///     ---
        ///     Searches for tools in the inventory
        /// </summary>
        /// <param name="tools">
        ///     Инструменты, которые мы пытаемся найти в инвентаре
        ///     ---
        ///     The tools we try to find in the inventory
        /// </param>
        /// <returns>
        ///     Множество найденных инструментов, которые соответствуют искомым tools
        ///     ---
        ///     The set of found tools that match the tools you are looking for
        /// </returns>
        public ISet<ToolType> HasTools(ISet<ToolType> tools)
        {
            var foundedTools = new HashSet<ToolType>();
            if (Sets.IsEmpty(tools) || Lists.IsEmpty(Items))
                return foundedTools;
            foreach (var item in Items)
                foundedTools.AddRange(Sets.Intersect(item.ToolType, tools));
            return foundedTools;
        }

        /// <summary>
        ///     Выполняет поиск первого попавшегося предмета в инвентаре, который может выполнять роль указанного инструмента
        ///     ---
        ///     Searches for the first found item in the inventory, which can serve as a specified tool
        /// </summary>
        /// <param name="toolType">
        ///     Тип инструмента, под который нужно искать предмет в инвентаре
        ///     ---
        ///     The type of tool for which you want to search for an item in the inventory
        /// </param>
        /// <returns>
        ///     Экземпляр предмета в инвентаре, который может быть использован в качестве инструмента указанного типа.
        ///     Если ничего подходящего не нашлось, вернёт null
        ///     ---
        ///     An item in the inventory that can be used as a tool of the specified type
        ///     If nothing suitable is found, he will return null
        /// </returns>
        public IItem GetFirstByToolType(ToolType toolType)
        {
            foreach (var item in Items)
            {
                if (Sets.IsEmpty(item.ToolType))
                    continue;
                if (item.ToolType.Contains(toolType))
                    return item;
            }
            return null;
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

        public IItem GetFirstById(long itemId)
        {
            return Items.Where(o => o.ID == itemId).FirstOrDefault();
        }

        public List<IItem> GetById(long itemId)
        {
            return Items.Where(o => o.ID == itemId).ToList();
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
            return DoAndSaveToDatabase(() =>
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
                        
                        count -= freeCount;
                        item.Count += freeCount; // Заполняем стек до конца

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
            return DoAndSaveToDatabase(() =>
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
                        
                        long delta = count - itemCount;
                        count -= delta;
                        
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
            return DoAndSaveToDatabase(() => Items.Remove(item));
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

            return DoAndSaveToDatabase(() => Items.Remove(remove));
        }

        private bool DoAndSaveToDatabase(Func<bool> action)
        {
            bool result = false;

            try
            {
                result = action.Invoke();
            }
            finally
            {
                if(result)
                    Save?.Invoke();
            }

            return result;
        }

    }

}
