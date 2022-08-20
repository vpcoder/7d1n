using Engine.Data;
using Engine.Data.Stories;
using Engine.DB;
using Engine.Logic.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Engine.Logic
{

    public class LocationSaver : MonoBehaviour
    {

        /// <summary>
        /// Сохраняем локацию в хранилище
        /// </summary>
        public void SaveLocation(Generator.LocationInfo location)
        {
            var locationDataSet = ReadLocationDataSetFromScene();
            var dataObject = new BuildLocation();
            dataObject.Timestamp = DateTime.Now.Ticks.ToString();
            dataObject.ID = location.ID;
            dataObject.Data = JsonUtility.ToJson(locationDataSet);
            BuildLocationStory.Instance.Save(dataObject);
        }

        /// <summary>
        /// Считывает данные локации из текущей сцены
        /// </summary>
        public LocationDataSet ReadLocationDataSetFromScene()
        {
            var data = new LocationDataSet();
            data.Enemies = ReadEnemies();
            data.Objects = ReadObjects();
            return data;
        }

        public List<EnemyDataSet> ReadEnemies()
        {
            var data = new List<EnemyDataSet>();
            foreach(var entry in NpcAISceneManager.Instance.GroupToNpcList)
            {
                if (entry.Key == EnemyGroup.PlayerGroup || entry.Key == EnemyGroup.AnotherPlayerGroup) // Не сохраняем персонажей реальных игроков
                    continue;

                foreach(var enemy in entry.Value)
                    data.Add(Read(enemy));
            }
            return data;
        }

        public List<ObjectDataSet> ReadObjects()
        {
            var data = new List<ObjectDataSet>();



            return data;
        }

        public EnemyDataSet Read(EnemyNpcBehaviour enemy)
        {
            return new EnemyDataSet()
            {
                ID = enemy.Enemy.ID,
                Health = enemy.Enemy.Health,
                Protection = enemy.Enemy.Protection,
                Items = enemy.Enemy.Items?.Select(item => Read(item)).ToList(),
                Weapons = enemy.Enemy.Weapons?.Select(weapon => Read(weapon)).ToList(),
                Position = enemy.transform.position.ToString(),
                Rotation = enemy.transform.rotation.eulerAngles.ToString(),
            };
        }

        public EnemyItemDataSet Read(IItem item)
        {
            return new EnemyItemDataSet()
            {
                ID = item.ID,
                Count = item.Count,
            };
        }

        public EnemyWeaponDataSet Read(IWeapon weapon)
        {
            return new EnemyWeaponDataSet()
            {
                ID = weapon.ID,
                AmmoCount = weapon.Type == GroupType.WeaponFirearms ? ((IFirearmsWeapon)weapon).AmmoCount : 0L,
            };
        }

    }

}
