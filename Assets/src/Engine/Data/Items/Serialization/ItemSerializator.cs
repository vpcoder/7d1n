using Engine.Data.Factories;

namespace Engine.Data
{

    /// <summary>
    /// 
    /// Класс-сериализатор предметов
    /// ---
    /// Item serializer class
    /// 
    /// </summary>
    public class ItemSerializator
    {

        /// <summary>
        ///     Выполняет конвертацию игрового предмета в информацию о предмете (сериализуемый объект)
        ///     ---
        ///     Converts the game object into information about the object (serializable object)
        /// </summary>
        /// <param name="item">
        ///     Игровой предмет, который надо конвертировать
        ///     ---
        ///     Game item to be converted
        /// </param>
        /// <returns>
        ///     Информация о предмете
        ///     ---
        ///     Item information
        /// </returns>
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
        ///     Выполняет конвертацию информации о предмете в игровой предмет
        ///     ---
        ///     Converts information about the item into a game item
        /// </summary>
        /// <param name="info">
        ///     Информация о предмете
        ///     ---
        ///     Item information
        /// </param>
        /// <returns>
        ///     Предмет
        ///     ---
        ///     Item
        /// </returns>
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
