using Engine.Data;
using Engine.Data.Factories;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic.Locations
{

    /// <summary>
    /// Ячейка быстрого слота оружия
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class HandCellItem : MonoBehaviour
    {

        #pragma warning disable 0649, IDE0044, CS0414, IDE0060, IDE0051

        [SerializeField] private bool selected = false;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color selectColor = new Color(0.39f, 1f, 0.5f);
        [SerializeField] private Image icon;
        [SerializeField] private HandCellType Hand;

        [SerializeField] private Text txtAmmo;

        private IWeapon weapon;

        private void Start()
        {
            switch(Hand)
            {
                case HandCellType.LeftHand:
                    Weapon = Game.Instance.Character.Equipment.Use1;
                    break;
                case HandCellType.RightHand:
                    Weapon = Game.Instance.Character.Equipment.Use2;
                    break;
            }
        }

        /// <summary>
        /// Оружие в этом слоте
        /// </summary>
        public IWeapon Weapon
        {
            get
            {
                return this.weapon;
            }
            set
            {
                this.weapon = value;
                this.UpdateCellInfo();
            }
        }

        public void UpdateCellInfo()
        {
            if (this.weapon == null)
                this.weapon = (IWeapon)ItemFactory.Instance.Get(DataDictionary.Weapons.WEAPON_SYSTEM_HANDS);

            icon.gameObject.SetActive(true);
            icon.sprite = weapon.Sprite;

            if (weapon.Type == GroupType.WeaponFirearms)
            {
                IFirearmsWeapon firearms = (IFirearmsWeapon)weapon;
                txtAmmo.gameObject.SetActive(true);
                txtAmmo.text = firearms.AmmoCount + "/" + firearms.AmmoStackSize;
            }
            else
            {
                txtAmmo.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Выбран ли этот слот
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return selected;
            }
        }

        public void DoSelect()
        {
            selected = true;
            GetComponent<Image>().color = selectColor;
        }

        public void DoUnselect()
        {
            selected = false;
            GetComponent<Image>().color = normalColor;
        }

        public void OnSelectClick()
        {
            ObjectFinder.Find<HandsController>().DoSelectHand(this);
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if(selected)
                GetComponent<Image>().color = selectColor;
            else
                GetComponent<Image>().color = normalColor;
        }

#endif

    }

}
