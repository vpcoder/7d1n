using Engine.Data.Factories.Xml;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    public class EnemyFactory : FactoryBase<IEnemy, XmlFactoryLoaderEnemy>
    {

        private IDictionary<long, GameObject> bodyByID = new Dictionary<long, GameObject>();

        #region Singleton

        private static readonly Lazy<EnemyFactory> instance = new Lazy<EnemyFactory>(() => new EnemyFactory());
        public static EnemyFactory Instance { get { return instance.Value; } }
        private EnemyFactory() { }

        #endregion

        public GameObject GetBody(long id)
        {
            GameObject body = null;
            if (!bodyByID.TryGetValue(id, out body))
            {
                var enemy = Get(id);
                var path = "Data/Enemies/" + enemy.SpritePath;
                body = Resources.Load<GameObject>(path);
#if UNITY_EDITOR
                if (body == null)
                {
                    Debug.LogError("body '" + path + "' not founded!");
                    return null;
                }
#endif
                bodyByID.Add(id, body);
            }
            return body;
        }

        public IEnemy GenerateEnemy(long id)
        {
            return GenerateEnemy(Create(id));
        }

        public IEnemy GenerateEnemy(IEnemy enemy)
        {
            enemy.Weapons = new List<IWeapon>();
            enemy.Items   = new List<IItem>();

            var genWeaponsCount = Random.Range(1, enemy.WeaponsMaxCountForGeneration + 1);
            var genItemsCount   = Random.Range(1, enemy.ItemsMaxCountForGeneration   + 1);

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
                enemy.Weapons.Add((IWeapon)ItemFactory.Instance.Get(991L)); // Руки зомби
                enemy.Weapons.Add((IWeapon)ItemFactory.Instance.Get(992L)); // Зубы зомби
            }
            else
            {
                enemy.Weapons.Add((IWeapon)ItemFactory.Instance.Get(990L)); // Руки
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

            return enemy;
        }

    }

}
