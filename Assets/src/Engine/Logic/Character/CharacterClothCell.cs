using Engine.Data;
using Engine.Data.Stories;
using Engine.Logic.Locations;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    /// <summary>
    /// Тип ячейки оружия (в какой руке?)
    /// </summary>
    public enum HandCellType : byte
    {
        LeftHand,
        RightHand
    };

    /// <summary>
    /// Ячейка экипировки
    /// (сюда вешается одежда персонажа или оружие)
    /// </summary>
    public class CharacterClothCell : MonoBehaviour
    {

        [SerializeField] private GameObject buttonCancel;
        [SerializeField] private GameObject buttonDrop;

        /// <summary>
        /// Ячейка для оружия?
        /// </summary>
        [SerializeField] private bool isWeapon = false;

        /// <summary>
        /// Актуально если isWeapon == true
        /// </summary>
        [SerializeField] private HandCellType hand;

        /// <summary>
        /// Тип ячейки (что сюда можно одевать?)
        /// </summary>
        [SerializeField] private GroupType type;

        /// <summary>
        /// Иконка для размещения предмета в ячейке
        /// </summary>
        [SerializeField] private Image image;

        /// <summary>
        /// Первичная информация о предмете (защита или урон)
        /// </summary>
        [SerializeField] private Text txtInfo;

        /// <summary>
        /// Дополнительная информация о предмете (находится внутри ячейки, например, количество патронов в оружии)
        /// </summary>
        [SerializeField] private Text txtAdditionInfo;

        /// <summary>
        /// Пустой спрайт, на случай, если в ячейке экипировки ничего нет
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
                return "";
            var damage = weapon.Damage; // Базовый урон
            var minDamage = BattleCalculationService.GetMinDamage(damage); // Минимальный рассчитанный урон
            var maxDamage = BattleCalculationService.GetMaxDamage(damage); // Максимальный рассчитанный урон
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
        /// Выполняет обновление информации о текущей ячйке и отрисовывает информацию на UI
        /// </summary>
        public void UpdateInfo()
        {
            if(IsWeapon)
            {
                var weapon = GetWeapon();
                txtInfo.text = CreateWeaponInfo(weapon);
                txtAdditionInfo.text = CreateWeaponAdditionInfo(weapon);
                image.sprite = weapon?.Sprite ?? empty;
            }
            else
            {
                var cloth = GetCloth();
                txtInfo.text = CreateClothInfo(cloth);
                image.sprite = cloth?.Sprite ?? empty;
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
        /// Экипирует одежду
        /// </summary>
        /// <param name="cloth">Одежда, которую надо надеть</param>
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
        /// Экипирует оружие
        /// </summary>
        /// <param name="weapon">Оружие которое надо взять в руку</param>
        private void DoEquip(IWeapon weapon)
        {
            switch (Hand)
            {
                case HandCellType.LeftHand:
                    Game.Instance.Character.Equipment.Use1 = weapon;
                    if (Game.Instance.Runtime.Scene == Scenes.SceneName.Location)
                        ObjectFinder.Find<HandsController>().GetCell(0).Weapon = weapon;

                    if (Game.Instance.Character.Equipment.Use2 == weapon) // Нельзя брать одно и то же оружие дважды в обе руки
                    {
                        Game.Instance.Character.Equipment.Use2 = null;
                        ObjectFinder.Find<HandsController>().GetCell(1).Weapon = null;
                        ObjectFinder.Find<CharacterClothCellController>().UpdateInfo();
                    }
                    break;
                case HandCellType.RightHand:
                    Game.Instance.Character.Equipment.Use2 = weapon;
                    if (Game.Instance.Runtime.Scene == Scenes.SceneName.Location)
                        ObjectFinder.Find<HandsController>().GetCell(1).Weapon = weapon;

                    if (Game.Instance.Character.Equipment.Use1 == weapon) // Нельзя брать одно и то же оружие дважды в обе руки
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
            image.sprite = item?.Sprite ?? empty;

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
            CharacterStory.Instance.EquipmentStory.Save(Game.Instance.Character.Equipment.CreateData());

            // Перерисовываем информацию ячейки
            UpdateInfo();
        }

    }

}
