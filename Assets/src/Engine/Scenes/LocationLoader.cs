using System.Collections.Generic;
using Engine.Data;
using Engine.Logic.Load;
using Engine.Logic.Locations.Generator;
using Engine.Logic.Locations.Generator.Environment.Building;
using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;

namespace Engine.Logic
{

    public class LocationLoader : MonoBehaviour
    {

        /// <summary>
        /// Загружает локацию из хранилища
        /// </summary>
        public void LoadLocation(ISceneLoadProcessor loader, Generator.LocationInfo location)
        {
            //var dataObject = BuildLocationStory.Instance.Get(location.ID);
            //var json = dataObject.Data;
            //var locationDataSet = JsonUtility.FromJson<LocationDataSet>(json);
            
            GenerateNew(loader);
        }

        private void GenerateNew(ISceneLoadProcessor loader)
        {
            // FIXME: тестовые данные
            Game.Instance.Runtime.Location.ID = Random.Range(0, 10000);
            Game.Instance.Runtime.GenerationInfo = LocationGenerateContex.Generate(Game.Instance.Runtime.Location); // Тестовые сведения о здании и этаже

            var enemyPointsList = new List<EnemyPointInfo>();
            loader?.SetDescription(Localization.Instance.Get("ui_location_load_rooms"));
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
            var context = LocationGenerateContex.GenerateGlobalScene(markers);
            Game.Instance.Runtime.GenerationInfo.GlobalInfo = context;
        }

    }

}
