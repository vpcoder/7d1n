using Engine.Data;
using Engine.Data.Repositories;
using Engine.Logic.Locations;
using System;
using Engine.Data.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{
    
    /// <summary>
    /// 
    /// Ячейка экипировки
    /// (сюда вешается одежда персонажа или оружие)
    /// ---
    /// The Equipment Box
    /// (this is where the character's clothes or weapons are hung)
    /// 
    /// </summary>
    public class CharacterClothCell : MonoBehaviour
    {

        [SerializeField] private GameObject buttonCancel;
        [SerializeField] private GameObject buttonDrop;

        /// <summary>
        ///     Ячейка для оружия?
        ///     ---
        ///     A gun cell?
        /// </summary>
        [SerializeField] private bool isWeapon = false;

        /// <summary>
        ///     Актуально если isWeapon == true
        ///     ---
        ///     Valid if isWeapon == true
        /// </summary>
        [SerializeField] private HandCellType hand;

        /// <summary>
        ///     Тип ячейки (что сюда можно одевать?)
        ///     ---
        ///     Cell type (what can you put on here?)
        /// </summary>
        [SerializeField] private GroupType type;

        /// <summary>
        ///     Иконка для размещения предмета в ячейке
        ///     ---
        ///     Icon for placing an item in a cell
        /// </summary>
        [SerializeField] private Image image;

        /// <summary>
        ///     Первичная информация о предмете (защита или урон)
        ///     ---
        ///     Primary information about the item (defense or damage)
        /// </summary>
        [SerializeField] private Text txtInfo;

        /// <summary>
        ///     Дополнительная информация о предмете (находится внутри ячейки, например, количество патронов в оружии)
        ///     ---
        ///     Additional information about the item (located inside the cell, for example, the number of rounds in the weapon)
        /// </summary>
        [SerializeField] private Text txtAdditionInfo;

        /// <summary>
        ///     Пустой спрайт, на случай, если в ячейке экипировки ничего нет
        ///     ---
        ///     Empty sprite, in case there is nothing in the equipment box
        /// </summary>
        [SerializeField] private Sprite empty;

        public GroupType Type { get { return type; } }

        public HandCellType Hand { get { return hand; } }

        public bool IsWeapon { get { return isWeapon; } }

        private void Start()
        {
            UpdateInfo();
        }

        private string CreateClothInfo(ICloth cloth)
        {
            if (cloth == null)
                return "";
            return "+" + BattleCalculationService.GetProtectionPercentText(cloth.Protection);
        }

        private string CreateWeaponInfo(IWeapon weapon)
        {
            if (weapon == null)
                weapon = (IWeapon)ItemFactory.Instance.Get(DataDictionary.Weapons.WEAPON_SYSTEM_HANDS);
            
            // Базовый урон
            // Basic damage
            var damage = weapon.Damage;
            // Минимальный рассчитанный урон
            // Minimum calculated damage
            var minDamage = BattleCalculationService.GetMinDamage(damage);
            // Максимальный рассчитанный урон
            // Maximum calculated damage
            var maxDamage = BattleCalculationService.GetMaxDamage(damage);
            
            // Сообщение показывающее разброс реактивного урона
            // Message showing the scatter of reactive damage
            return Mathf.RoundToInt(minDamage).ToString() + "-" + Mathf.RoundToInt(maxDamage).ToString() + " " + Localization.Instance.Get("msg_count");
        }

        private string CreateWeaponAdditionInfo(IWeapon weapon)
        {
            if (weapon == null)
                return "";

            if (weapon.Type == GroupType.WeaponFirearms)
            {
                IFirearmsWeapon firearms = (IFirearmsWeapon)weapon;
                return firearms.AmmoCount + "/" + firearms.AmmoStackSize;
            }
            else
            {
                return "";
            }
        }

        private ICloth GetCloth()
        {
            switch (Type)
            {
                case GroupType.ClothHead:
                    return Game.Instance.Character.Equipment.Head;
                case GroupType.ClothBody:
                    return Game.Instance.Character.Equipment.Body;
                case GroupType.ClothHand:
                    return Game.Instance.Character.Equipment.Hand;
                case GroupType.ClothLegs:
                    return Game.Instance.Character.Equipment.Legs;
                case GroupType.ClothBoot:
                    return Game.Instance.Character.Equipment.Boot;
                default:
                    throw new NotSupportedException();
            }
        }

        private IWeapon GetWeapon()
        {
            switch (Hand)
            {
                case HandCellType.LeftHand:
                    return Game.Instance.Character.Equipment.Use1;
                case HandCellType.RightHand:
                    return Game.Instance.Character.Equipment.Use2;
                default:
                    throw new NotSupportedException();
            }
        }

        /// <summary>
        ///     Выполняет обновление информации о текущей ячйке и отрисовывает информацию на UI
        ///     ---
        ///     Updates information about the current cell and draws the information on the UI
        /// </summary>
        public void UpdateInfo()
        {
            if(IsWeapon)
            {
                var weapon = GetWeapon();
                txtInfo.text = CreateWeaponInfo(weapon);
                txtAdditionInfo.text = CreateWeaponAdditionInfo(weapon);
                image.sprite = weapon == null || weapon.Sprite == null ? empty : weapon.Sprite;
            }
            else
            {
                var cloth = GetCloth();
                txtInfo.text = CreateClothInfo(cloth);
                image.sprite =  cloth == null || cloth.Sprite == null ? empty : cloth.Sprite;
            }
        }

        public void HideButtons()
        {
            buttonDrop.SetActive(false);
            buttonCancel.SetActive(false);
        }

        public void OnClick()
        {
            var viewer = ObjectFinder.Find<ItemSelectorView>();
            viewer.SelectedEquipCell = this;
            viewer.Show();

            IItem item;
            if (IsWeapon)
                item = GetWeapon();
            else
                item = GetCloth();
            
            buttonDrop.SetActive(item != null);
            buttonCancel.SetActive(true);
        }

        private void PlaySound(ICloth cloth)
        {
            if (cloth == null)
                return;
            switch (cloth.Type)
            {
                case GroupType.ClothHead:
                    // AudioController.Instance.PlaySound("firearms/equip");
                    break;
                case GroupType.ClothBody:
                    break;
                case GroupType.ClothHand:
                    break;
                case GroupType.ClothLegs:
                    break;
                case GroupType.ClothBoot:
                    break;
            }
        }

        private void PlaySound(IWeapon weapon)
        {
            if (weapon == null)
                return;
            switch (weapon.Type)
            {
                case GroupType.WeaponFirearms:
                    AudioController.Instance.PlaySound("firearms/equip");
                    break;
                case GroupType.WeaponEdged:
                    AudioController.Instance.PlaySound("edgeds/equip");
                    break;
                case GroupType.WeaponGrenade:
                    AudioController.Instance.PlaySound("grenades/equip");
                    break;
            }
        }

        /// <summary>
        ///     Экипирует одежду
        ///     ---
        ///     Outfits clothing
        /// </summary>
        /// <param name="cloth">
        ///     Одежда, которую надо надеть
        ///     ---
        ///     Clothes to wear
        /// </param>
        private void DoEquip(ICloth cloth)
        {
            switch (Type)
            {
                case GroupType.ClothHead:
                    Game.Instance.Character.Equipment.Head = cloth;
                    break;
                case GroupType.ClothBody:
                    Game.Instance.Character.Equipment.Body = cloth;
                    break;
                case GroupType.ClothHand:
                    Game.Instance.Character.Equipment.Hand = cloth;
                    break;
                case GroupType.ClothLegs:
                    Game.Instance.Character.Equipment.Legs = cloth;
                    break;
                case GroupType.ClothBoot:
                    Game.Instance.Character.Equipment.Boot = cloth;
                    break;
            }
        }

        /// <summary>
        ///     Экипирует оружие
        ///     ---
        ///     Equips weapons
        /// </summary>
        /// <param name="weapon">
        ///     Оружие которое надо взять в руку
        ///     ---
        ///     Weapon to take in hand
        /// </param>
        private void DoEquip(IWeapon weapon)
        {
            switch (Hand)
            {
                case HandCellType.LeftHand:
                    Game.Instance.Character.Equipment.Use1 = weapon;
                    if (Game.Instance.Runtime.Scene != Scenes.SceneName.Map && Game.Instance.Runtime.Scene != Scenes.SceneName.Menu)
                        ObjectFinder.Find<HandsController>().GetCell(0).Weapon = weapon;

                    // Нельзя брать одно и то же оружие дважды в обе руки
                    // Cannot take the same weapon twice in both hands
                    if (Game.Instance.Character.Equipment.Use2 == weapon)
                    {
                        Game.Instance.Character.Equipment.Use2 = null;
                        ObjectFinder.Find<HandsController>().GetCell(1).Weapon = null;
                        ObjectFinder.Find<CharacterClothCellController>().UpdateInfo();
                    }
                    break;
                case HandCellType.RightHand:
                    Game.Instance.Character.Equipment.Use2 = weapon;
                    if (Game.Instance.Runtime.Scene != Scenes.SceneName.Map && Game.Instance.Runtime.Scene != Scenes.SceneName.Menu)
                        ObjectFinder.Find<HandsController>().GetCell(1).Weapon = weapon;

                    // Нельзя брать одно и то же оружие дважды в обе руки
                    // Cannot take the same weapon twice in both hands
                    if (Game.Instance.Character.Equipment.Use1 == weapon)
                    {
                        Game.Instance.Character.Equipment.Use1 = null;
                        ObjectFinder.Find<HandsController>().GetCell(0).Weapon = null;
                        ObjectFinder.Find<CharacterClothCellController>().UpdateInfo();
                    }
                    break;
            }
        }

        public void DoEquip(IItem item)
        {
            // Обновляем картинку
            // Updating the picture
            image.sprite = item == null || item.Sprite == null ? empty : item.Sprite;

            if (IsWeapon)
            {
                var weapon = (IWeapon)item;
                PlaySound(weapon);
                DoEquip(weapon);
            }
            else
            {
                var cloth = (ICloth)item;
                PlaySound(cloth);
                DoEquip(cloth);
            }

            // Сбрасываем информацию на диск
            // Resetting the information to disk
            CharacterRepository.Instance.EquipmentRepository.Save(Game.Instance.Character.Equipment.CreateData());

            // Перерисовываем информацию ячейки
            // Redraw the cell information
            UpdateInfo();
        }

    }

}
