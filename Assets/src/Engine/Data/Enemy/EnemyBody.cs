using Engine.Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Engine.Data
{

    [Serializable]
    public class Enemy
    {
        [SerializeField] public long ID = 0;
        [SerializeField] public int AP = 8;
        [SerializeField] public long Exp = 5;
        [SerializeField] public EnemyGroup EnemyGroup = EnemyGroup.ZombieGroup;
        [SerializeField] public int Protection = 1;
        [SerializeField] public int Health = 100;
        [SerializeField] public string ItemsForGeneration = "";
        [SerializeField] public int ItemsMaxCountForGeneration = 0;
        [SerializeField] public string WeaponsForGeneration = "";
        [SerializeField] public int WeaponsMaxCountForGeneration = 0;
    }

    public class EnemyBody : MonoBehaviour
    {

        [SerializeField] private RuntimeAnimatorController controller;
        [SerializeField] private Avatar avatar;
        [SerializeField] private Transform weaponPoint;
        [SerializeField] private Enemy enemy;
        [SerializeField] private SkinnedMeshRenderer meshRenderer;

        public RuntimeAnimatorController Controller { get { return controller; } }
        public Avatar Avatar { get { return avatar; } }
        public Transform WeaponPoint { get { return weaponPoint; } }
        public SkinnedMeshRenderer MeshRenderer { get { return meshRenderer; } }

        private IEnemy cachedEnemy;

        public IEnemy Enemy
        {
            get
            {
                if(cachedEnemy == null)
                    cachedEnemy = CreateEnemy();
                return cachedEnemy;
            }
            set
            {
                cachedEnemy = value;
            }
        }

        private IEnemy CreateEnemy()
        {
            var enemy = new NPC();

            enemy.ID = this.enemy.ID;
            enemy.AP = this.enemy.AP;
            enemy.Exp = this.enemy.Exp;
            enemy.EnemyGroup = this.enemy.EnemyGroup;
            enemy.Protection = this.enemy.Protection;
            enemy.Health = this.enemy.Health;

            var parts = new List<ResourcePair>();
            foreach (var itemData in this.enemy.ItemsForGeneration.Split(';'))
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
            enemy.ItemsForGeneration = parts;

            var weapons = this.enemy.WeaponsForGeneration.Split(';')
                .Where(item => !string.IsNullOrWhiteSpace(item))
                .Select(id => long.Parse(id))
                .ToList();
            enemy.WeaponsForGeneration = weapons;

            enemy.ItemsMaxCountForGeneration = this.enemy.ItemsMaxCountForGeneration;
            enemy.WeaponsMaxCountForGeneration = this.enemy.WeaponsMaxCountForGeneration;

            GenerateItems(enemy);
            return enemy;
        }

        private void GenerateItems(IEnemy enemy)
        {
            enemy.Weapons = new List<IWeapon>();
            enemy.Items = new List<IItem>();

            var genWeaponsCount = Random.Range(1, enemy.WeaponsMaxCountForGeneration + 1);
            var genItemsCount = Random.Range(1, enemy.ItemsMaxCountForGeneration + 1);

            if (enemy.WeaponsForGeneration.Count > 0)
            {
                for (int i = 0; i < genWeaponsCount; i++)
                {
                    int index = Random.Range(0, enemy.WeaponsForGeneration.Count);
                    IWeapon weapon = (IWeapon)ItemFactory.Instance.Create(enemy.WeaponsForGeneration[index], 1);
                    IFirearmsWeapon firearms = weapon.Type == GroupType.WeaponFirearms ? (IFirearmsWeapon)weapon : null;
                    if (firearms != null)
                        firearms.AmmoCount = firearms.AmmoStackSize;

                    enemy.Weapons.Add(weapon);
                }
            }
            if (enemy.EnemyGroup == EnemyGroup.ZombieGroup)
            {
                enemy.Weapons.Add((IWeapon)ItemFactory.Instance.Get(DataDictionary.Weapons.WEAPON_SYSTEM_ZOMBIE_HANDS)); // Руки зомби
                enemy.Weapons.Add((IWeapon)ItemFactory.Instance.Get(DataDictionary.Weapons.WEAPON_SYSTEM_TOOTHS)); // Зубы зомби
            }
            else
            {
                enemy.Weapons.Add((IWeapon)ItemFactory.Instance.Get(DataDictionary.Weapons.WEAPON_SYSTEM_HANDS)); // Руки
            }

            if (enemy.Weapons.Count > 0)
            {
                enemy.Weapons.Sort((o1, o2) =>
                {
                    return o1.Damage - o2.Damage;
                });
            }
            if (enemy.ItemsForGeneration != null && enemy.ItemsForGeneration.Count > 0)
            {
                for (int i = 0; i < genItemsCount; i++)
                {
                    int index = Random.Range(0, enemy.ItemsForGeneration.Count);
                    var part = enemy.ItemsForGeneration[index];
                    IItem item = ItemFactory.Instance.Create(part.ResourceID, part.ResourceCount);
                    enemy.Items.Add(item);
                }
            }
        }

    }

}
