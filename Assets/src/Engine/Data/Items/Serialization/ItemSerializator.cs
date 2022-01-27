
using Engine.Data.Factories;

namespace Engine.Data
{

    /// <summary>
    /// Класс-сериализатор объектов
    /// </summary>
    public class ItemSerializator
    {

        /// <summary>
        /// Выполняет конвертацию предмета в формат, пригодный для сериализации
        /// </summary>
        /// <param name="item">Предмет, который надо конвертировать</param>
        /// <returns>Объект для сериализации</returns>
        public static ItemInfo Convert(IItem item)
        {
            var data = new ItemInfo();

            if (item == null)
                return data;

            data.ID    = item.ID;
            data.Count = item.Count;
            data.Type  = item.Type;

            var craftable = item as ICraftableItem;
            if(craftable != null)
            {
                data.Craftable = new CraftableInfo();
                data.Craftable.Level = craftable.Level;
                data.Craftable.Author = craftable.Author;
            }

            var firearms = item as IFirearmsWeapon;
            if (firearms != null)
            {
                data.Firearms = new FirearmsInfo();
                data.Firearms.AmmoCount = firearms.AmmoCount;
            }

            return data;
        }

        /// <summary>
        /// Выполняет конвертацию объекта для сериализации в игровой предмет
        /// </summary>
        /// <param name="info">Объект для сериализации</param>
        /// <returns>Предмет</returns>
        public static IItem Convert(ItemInfo info)
        {
            if (info == null)
                return null;

            var item = ItemFactory.Instance.Create(info.ID, info.Count);
            var craftable = item as ICraftableItem;
            var firearms = item as IFirearmsWeapon;

            if(craftable != null)
            { // Блок крафтового предмета
                craftable.Author = info.Craftable.Author;
                craftable.Level = info.Craftable.Level;
            }

            if(firearms != null)
            { // Блок предмета огнестрельного оружия
                firearms.AmmoCount = info.Firearms.AmmoCount;
            }
            return item;
        }

    }

}
