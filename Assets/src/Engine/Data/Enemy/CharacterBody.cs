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

        [SerializeField] public bool IsRandomItemsGeneration = true;
        [SerializeField] public string ItemsForGeneration = "";
        [SerializeField] public int ItemsMaxCountForGeneration = 0;
        
        [SerializeField] public bool IsRandomWeaponsGeneration = true;
        [SerializeField] public string WeaponsForGeneration = "";
        [SerializeField] public int WeaponsMaxCountForGeneration = 0;
    }

    public class CharacterBody : MonoBehaviour
    {

        [SerializeField] private RuntimeAnimatorController controller;
        [SerializeField] private Avatar avatar;
        [SerializeField] private Transform weaponPoint;
        [SerializeField] private CharData charData;
        [SerializeField] private Transform root;

        public RuntimeAnimatorController Controller
        {
            get { return controller; }
            set { controller = value; }
        }

        public Avatar Avatar
        {
            get { return avatar; }
            set { avatar = value; }
        }
        public Transform WeaponPoint
        {
            get { return weaponPoint; }
            set { weaponPoint = value; }
        }

        public Transform Root
        {
            get { return root; }
            set { root = value; }
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
            character.GenerationInfo = new CharacterLootGeneration();
            
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
                parts.Add(new ResourcePair() { ResourceID = id, ResourceCount = count});
            }

            var genInfo = character.GenerationInfo;
            genInfo.ItemsForGeneration = parts;
            genInfo.ItemsMaxCountForGeneration = this.charData.ItemsMaxCountForGeneration;
            genInfo.IsRandomItemsGeneration = this.charData.IsRandomItemsGeneration;
            if(genInfo.IsRandomItemsGeneration)
                GenerateRandomItems(character);
            else
                GenerateItems(character);
            
            var weapons = this.charData.WeaponsForGeneration.Split(';')
                .Where(item => !string.IsNullOrWhiteSpace(item))
                .Select(id => long.Parse(id))
                .ToList();
            genInfo.WeaponsForGeneration = weapons;
            genInfo.WeaponsMaxCountForGeneration = this.charData.WeaponsMaxCountForGeneration;
            genInfo.IsRandomWeaponsGeneration = this.charData.IsRandomWeaponsGeneration;
            if(genInfo.IsRandomWeaponsGeneration)
                GenerateRandomWeapons(character);
            else
                GenerateWeapons(character);
            PostProcessWeapons(character);
            
            return character;
        }

        private void GenerateItems(ICharacter character)
        {
            var genInfo = character.GenerationInfo;
            character.Items = new List<IItem>();
            
            foreach (var part in genInfo.ItemsForGeneration)
            {
                IItem item = ItemFactory.Instance.Create(part.ResourceID, part.ResourceCount);
                character.Items.Add(item);
            }
        }
        
        private void GenerateRandomItems(ICharacter character)
        {
            var genInfo = character.GenerationInfo;
            character.Items = new List<IItem>();

            var genItemsCount = Random.Range(1, genInfo.ItemsMaxCountForGeneration + 1);
            
            if (genInfo.ItemsForGeneration != null && genInfo.ItemsForGeneration.Count > 0)
            {
                for (int i = 0; i < genItemsCount; i++)
                {
                    int index = Random.Range(0, genInfo.ItemsForGeneration.Count);
                    var part = genInfo.ItemsForGeneration[index];
                    IItem item = ItemFactory.Instance.Create(part.ResourceID, part.ResourceCount);
                    character.Items.Add(item);
                }
            }
        }

        private void GenerateWeapons(ICharacter character)
        {
            var genInfo = character.GenerationInfo;
            character.Weapons = new List<IWeapon>();

            foreach (var weaponID in genInfo.WeaponsForGeneration)
            {
                IWeapon weapon = (IWeapon)ItemFactory.Instance.Create(weaponID, 1);
                IFirearmsWeapon firearms = weapon.Type == GroupType.WeaponFirearms ? (IFirearmsWeapon)weapon : null;
                if (firearms != null)
                    firearms.AmmoCount = firearms.AmmoStackSize;

                character.Weapons.Add(weapon);
            }
        }
        
        private void GenerateRandomWeapons(ICharacter character)
        {
            var genInfo = character.GenerationInfo;
            character.Weapons = new List<IWeapon>();

            var genWeaponsCount = Random.Range(1, genInfo.WeaponsMaxCountForGeneration + 1);

            if (genInfo.WeaponsForGeneration.Count > 0)
            {
                for (int i = 0; i < genWeaponsCount; i++)
                {
                    int index = Random.Range(0, genInfo.WeaponsForGeneration.Count);
                    IWeapon weapon = (IWeapon)ItemFactory.Instance.Create(genInfo.WeaponsForGeneration[index], 1);
                    IFirearmsWeapon firearms = weapon.Type == GroupType.WeaponFirearms ? (IFirearmsWeapon)weapon : null;
                    if (firearms != null)
                        firearms.AmmoCount = firearms.AmmoStackSize;

                    character.Weapons.Add(weapon);
                }
            }
        }

        private void PostProcessWeapons(ICharacter character)
        {
            if (character.Weapons == null)
                character.Weapons = new List<IWeapon>();
            
            if (character.OrderGroup == OrderGroup.ZombieGroup)
            {
                character.Weapons.Add((IWeapon)ItemFactory.Instance.Get(DataDictionary.Weapons.WEAPON_SYSTEM_ZOMBIE_HANDS));
                character.Weapons.Add((IWeapon)ItemFactory.Instance.Get(DataDictionary.Weapons.WEAPON_SYSTEM_TOOTHS));
            }
            else
            {
                character.Weapons.Add((IWeapon)ItemFactory.Instance.Get(DataDictionary.Weapons.WEAPON_SYSTEM_HANDS));
            }

            if (character.Weapons.Count > 1)
            {
                character.Weapons.Sort((o1, o2) =>
                {
                    return o1.Damage - o2.Damage;
                });
            }
        }

    }

}
