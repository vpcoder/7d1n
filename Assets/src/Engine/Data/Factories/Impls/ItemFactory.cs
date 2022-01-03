using Engine.Data.Factories.Xml;

namespace Engine.Data.Factories
{

    public class ItemFactory : FactoryBase<IItem, XmlFactoryLoaderItem>
    {

        #region Singleton

        private static readonly Lazy<ItemFactory> instance = new Lazy<ItemFactory>(() => new ItemFactory());
        public static ItemFactory Instance { get { return instance.Value; } }
        private ItemFactory() { }

        #endregion

        public IItem Create(long id, long count)
        {
            var item = Create(id);
            item.Weight = item.GetWeight();
            item.Count = count;
            return item;
        }

    }

}
