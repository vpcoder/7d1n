using Engine.Data;
using Engine.Data.Factories;
using Engine.Logic.Locations;
using System.Collections;
using System.Collections.Generic;
using Engine.Logic.Locations.Generator;
using Engine.Logic.Locations.Generator.Environment.Building;
using Engine.Logic.Locations.Generator.Markers;
using Engine.Scenes;
using Engine.Scenes.Loader;
using src.Engine.Scenes.Loader;
using UnityEngine;

namespace Engine.Logic.Load
{

    public class LocationLoadProcessor : SceneLoadProcessorBase
    {
        
        public override IEnumerator LoadProcess()
        {
            StartLoad();

            // Выполняем загрузку локации
            // ObjectFinder.Find<LocationLoader>().LoadLocation(Game.Instance.Runtime.Location);
            
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
            
            // Загружаем сцену
            LoadFactory.Instance.Get(SceneName.Location).PostLoad(new LoadContext()
            {
                EnemyListInfo = enemyPointsList
            });

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

    }

}
