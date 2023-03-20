using Engine.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Engine.Data
{

    [Serializable]
    public class CharData
    {
        [SerializeField] public long ID = 0;
        [SerializeField] public int AP = 8;
        [SerializeField] public long Exp = 5;
        [SerializeField] public OrderGroup orderGroup = OrderGroup.ZombieGroup;
        [SerializeField] public int Protection = 1;
        [SerializeField] public int Health = 100;
        [SerializeField] public string ItemsForGeneration = "";
        [SerializeField] public int ItemsMaxCountForGeneration = 0;
        [SerializeField] public string WeaponsForGeneration = "";
        [SerializeField] public int WeaponsMaxCountForGeneration = 0;
    }

    public class CharacterBody : MonoBehaviour
    {

        [SerializeField] private RuntimeAnimatorController controller;
        [SerializeField] private Avatar avatar;
        [SerializeField] private Transform weaponPoint;
        [SerializeField] private CharData charData;

        public RuntimeAnimatorController Controller { get { return controller; } }
        public Avatar Avatar { get { return avatar; } }
        public Transform WeaponPoint
        {
            get { return weaponPoint; }
            set { weaponPoint = value; }
        }

        private ICharacter cachedCharacter;

        public ICharacter Character
        {
            get
            {
                if(cachedCharacter == null)
                    cachedCharacter = CreateCharacter();
                return cachedCharacter;
            }
            set
            {
                cachedCharacter = value;
            }
        }

        public CharData Data => charData;

        private ICharacter CreateCharacter()
        {
            var character = new NPC();

            character.ID = this.charData.ID;
            character.AP = this.charData.AP;
            character.Exp = this.charData.Exp;
            character.OrderGroup = this.charData.orderGroup;
            character.Protection = this.charData.Protection;
            character.Health = this.charData.Health;

            var parts = new List<ResourcePair>();
            foreach (var itemData in this.charData.ItemsForGeneration.Split(';'))
            {
                if(string.IsNullOrWhiteSpace(itemData))
                {
                    continue;
                }
                var item = itemData.Split(',');
                if(string.IsNullOrWhiteSpace(item[0]))
                {
                    continue;
                }
                var id = long.Parse(item[0]);
                var count = long.Parse(item[1]);
                parts.Add(new ResourcePair() { ResourceID = long.Parse(item[0]), ResourceCount = long.Parse(item[1])});
            }
            character.ItemsForGeneration = parts;

            var weapons = this.charData.WeaponsForGeneration.Split(';')
                .Where(item => !string.IsNullOrWhiteSpace(item))
                .Select(id => long.Parse(id))
                .ToList();
            character.WeaponsForGeneration = weapons;

            character.ItemsMaxCountForGeneration = this.charData.ItemsMaxCountForGeneration;
            character.WeaponsMaxCountForGeneration = this.charData.WeaponsMaxCountForGeneration;

            GenerateItems(character);
            return character;
        }

        private void GenerateItems(ICharacter character)
        {
            character.Weapons = new List<IWeapon>();
            character.Items = new List<IItem>();

            var genWeaponsCount = Random.Range(1, character.WeaponsMaxCountForGeneration + 1);
            var genItemsCount = Random.Range(1, character.ItemsMaxCountForGeneration + 1);

            if (character.WeaponsForGeneration.Count > 0)
            {
                for (int i = 0; i < genWeaponsCount; i++)
                {
                    int index = Random.Range(0, character.WeaponsForGeneration.Count);
                    IWeapon weapon = (IWeapon)ItemFactory.Instance.Create(character.WeaponsForGeneration[index], 1);
                    IFirearmsWeapon firearms = weapon.Type == GroupType.WeaponFirearms ? (IFirearmsWeapon)weapon : null;
                    if (firearms != null)
                        firearms.AmmoCount = firearms.AmmoStackSize;

                    character.Weapons.Add(weapon);
                }
            }
            if (character.OrderGroup == OrderGroup.ZombieGroup)
            {
                character.Weapons.Add((IWeapon)ItemFactory.Instance.Get(DataDictionary.Weapons.WEAPON_SYSTEM_ZOMBIE_HANDS)); // Руки зомби
                character.Weapons.Add((IWeapon)ItemFactory.Instance.Get(DataDictionary.Weapons.WEAPON_SYSTEM_TOOTHS)); // Зубы зомби
            }
            else
            {
                character.Weapons.Add((IWeapon)ItemFactory.Instance.Get(DataDictionary.Weapons.WEAPON_SYSTEM_HANDS)); // Руки
            }

            if (character.Weapons.Count > 0)
            {
                character.Weapons.Sort((o1, o2) =>
                {
                    return o1.Damage - o2.Damage;
                });
            }
            if (character.ItemsForGeneration != null && character.ItemsForGeneration.Count > 0)
            {
                for (int i = 0; i < genItemsCount; i++)
                {
                    int index = Random.Range(0, character.ItemsForGeneration.Count);
                    var part = character.ItemsForGeneration[index];
                    IItem item = ItemFactory.Instance.Create(part.ResourceID, part.ResourceCount);
                    character.Items.Add(item);
                }
            }
        }

    }

}
