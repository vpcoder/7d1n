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

            character.Inventory.Add(1000L, 1);
            character.Inventory.Add(2000L, 20);

            character.Inventory.Add(3000L, 1);
            character.Inventory.Add(4000L, 1);
            character.Inventory.Add(4001L, 1);

            var pm = (IFirearmsWeapon)character.Inventory.GetFirstById(1000L);
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
