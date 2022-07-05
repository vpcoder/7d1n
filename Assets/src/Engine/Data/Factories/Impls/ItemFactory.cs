using Engine.Data.Factories.Xml;
using System;

namespace Engine.Data.Factories
{

    /// <summary>
    /// 
    /// Фабрика предметов.
    /// Кеширует и хранит все предметы в игре, сдесь можно получать как предметы в ОЗУ,
    /// так и сощдавать новые экземпляры предметов
    /// ---
    /// Item Factory.
    /// Caches and stores all items in the game, here you can get both items in RAM,
    /// or create new items.
    /// 
    /// </summary>
    public class ItemFactory : FactoryBase<IItem, XmlFactoryLoaderItem>
    {

        #region Singleton

        private static readonly Lazy<ItemFactory> instance = new Lazy<ItemFactory>(() => new ItemFactory());
        public static ItemFactory Instance { get { return instance.Value; } }
        private ItemFactory() { }

        #endregion

        /// <summary>
        ///     Создаёт новый экземпляр предмета
        ///     ---
        ///     Creates a new instance of the item
        /// </summary>
        /// <param name="id">
        ///     Идентификатор создаваемого предмета
        ///     ---
        ///     The identifier of the item to be created
        /// </param>
        /// <param name="count">
        ///     Количество штук предметов в пачке
        ///     ---
        ///     Number of pieces of items in a pack
        /// </param>
        /// <returns>
        ///     Возвращает созданный экземпляр предмета
        ///     ---
        ///     Returns the created instance of the item
        /// </returns>
        public IItem Create(long id, long count)
        {
            var item = Create(id);
            item.Count = count;
            if(!item.StaticWeight)
                item.Weight = item.GetWeight();
            return item;
        }

    }

}
