using Engine.Data;
using Engine.Data.Factories;
using Engine.Logic.Locations;
using System.Collections;
using System.Collections.Generic;
using Engine.Logic.Locations.Generator;
using Engine.Logic.Locations.Generator.Environment.Building;
using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;

namespace Engine.Logic.Load
{

    public class LocationLoadProcessor : SceneLoadProcessorBase
    {
        
        public override IEnumerator LoadProcess()
        {
            StartLoad();

            // FIXME: тестовые данные
            Game.Instance.Runtime.Location.ID = Random.Range(0, 10000);
            Game.Instance.Runtime.GenerationInfo = LocationGenerateContex.Generate(Game.Instance.Runtime.Location); // Тестовые сведения о здании и этаже


            var enemyPointsList = new List<EnemyPointInfo>();
            SetDescription(Localization.Instance.Get("ui_location_load_rooms"));
            // Собираем все комнаты в сцене
            var rooms = FindObjectsOfType<RoomHierarchyBehaviour>();
            var markers = new List<IMarker>();
            foreach (var room in rooms) // Генерим помещения по комнатам
            {
                markers.AddRange(room.GetMarkers());
                LocationGenerateContex.GenerateRoomByMarkers(room.GetMarkers(), room.RoomType);
                enemyPointsList.AddRange(Game.Instance.Runtime.GenerationInfo.EnemyInfo.EnemyStartPoints);
            }
            // Генерация всей сцены (стены, пол и прочее)
            LocationGenerateContex.GenerateGlobalScene(markers);

            SetTitle(Localization.Instance.Get("ui_loading"));
            yield return new WaitForSeconds(MIN_WAIT);

            // Генерируем навмеш, для работы с путями
            SetDescription(Localization.Instance.Get("ui_location_load_navmesh"));
            yield return new WaitForSeconds(MIN_WAIT);
            ObjectFinder.Find<NavMeshGenerator>().CreateNavMesh();
            
            Test(enemyPointsList);

            // Конец загрузки
            CompleteLoad();
        }

        private GameObject character;
        
        public override void OnCompleteLoad()
        {
            character.SetActive(true);

            ObjectFinder.Find<BattleManager>().EnterToBattle();

            Destroy(gameObject);
        }

        public override void OnStartLoad()
        {
            character = ObjectFinder.Find<LocationCharacter>().gameObject;
            character.SetActive(false);
        }
        
        private void Test(List<EnemyPointInfo> enemyPointsList)
        {
            var character = Game.Instance.Character;
            
            character.Inventory.Add(DataDictionary.Weapons.WEAPON_PISTOL_PM, 1);
            character.Inventory.Add(DataDictionary.Weapons.AMMO_9X18, 20);

            character.Inventory.Add(DataDictionary.Weapons.WEAPON_KNIFE_KITCHEN, 1);
            character.Inventory.Add(DataDictionary.Weapons.WEAPON_GRENADE_RGD5, 1);
            character.Inventory.Add(DataDictionary.Weapons.WEAPON_GRENADE_F1, 1);
            
            character.Inventory.Add(DataDictionary.Resources.RES_WOOD, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_SCRAP, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_IRON, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_LEAD, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_COOPER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_ALUMINUM, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_SILVER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_GOLD, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_TITANIUM, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_PLASTIC, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_PAPER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_ELECTRIC, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_STONE, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_CERAMICS, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_SAND, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_SALT, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_COAL, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_WATER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_ACID, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_LYE, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_BRIMSTONE, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_SALTPETRE, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_FUEL, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_FIBER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RUBBER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_POWDER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_EXPLOSIVE, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_DIRTY_WATER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_IRON, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_LEAD, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_COOPER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_ALUMINUM, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_SILVER, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_GOLD, 1);
            character.Inventory.Add(DataDictionary.Resources.RES_RAW_TITANIUM, 1);
            
            character.Inventory.Add(DataDictionary.Foods.FOOD_RAW_ROACH, 2);
            character.Inventory.Add(DataDictionary.Foods.FOOD_RAW_LARVAE, 3);
            character.Inventory.Add(DataDictionary.Foods.FOOD_RAW_FRY_FISH, 1);
            
            character.Inventory.Add(5000, 1);
            character.Inventory.Add(5001, 1);
            character.Inventory.Add(5002, 1);
            character.Inventory.Add(5003, 1);
            character.Inventory.Add(5004, 1);
            character.Inventory.Add(5005, 1);
            character.Inventory.Add(5006, 1);

            var pm = (IFirearmsWeapon)character.Inventory.GetFirstById(DataDictionary.Weapons.WEAPON_PISTOL_PM);
            pm.AmmoCount = 4;
            character.Equipment.Use1 = pm;
            ObjectFinder.Find<HandsController>().GetCell(0).Weapon = pm;

            for (int i = 0; i < 5; i++)
            {
                if (enemyPointsList.Count == 0)
                    break;

                var randomPoint = enemyPointsList[Random.Range(0, enemyPointsList.Count)];
                
                var behaviour = NpcFactory.Instance.GetBehaviour(100L);
                var pos = randomPoint.Position;

                var rot = Random.rotation.eulerAngles;
                rot.x = 0;
                rot.z = 0;

                var npc = Instantiate(behaviour, pos, Quaternion.Euler(rot));

                enemyPointsList.Remove(randomPoint);
            }

        }

    }

}
