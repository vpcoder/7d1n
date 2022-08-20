using System.Collections.Generic;
using Engine.Data;
using Engine.Logic.Locations;
using Engine.Logic.Locations.Generator;
using Engine.Logic.Locations.Generator.Environment.Building;
using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;

public class TestGeneration : MonoBehaviour
{

    [SerializeField] private int seed;
    
    public void Make()
    {
        Clear();

        Game.Instance.Runtime.Location.ID = seed;
        Game.Instance.Runtime.GenerationInfo = LocationGenerateContex.Generate(Game.Instance.Runtime.Location); // Тестовые сведения о здании и этаже

        // Собираем все комнаты в сцене
        var rooms = FindObjectsOfType<RoomHierarchyBehaviour>();
        var markers = new List<IMarker>();
        foreach (var room in rooms) // Генерим помещения по комнатам
        {
            markers.AddRange(room.GetMarkers());
            LocationGenerateContex.GenerateRoomByMarkers(room.GetMarkers(), room.RoomType);
        }
        LocationGenerateContex.GenerateGlobalScene(markers);

        // Строим навмеш сцены
        ObjectFinder.Find<NavMeshGenerator>().CreateNavMesh();

        seed = Random.Range(0, 10000);
    }

    public void Clear()
    {
        var data = GameObject.Find("BuildData");
        if(data != null)
            DestroyImmediate(data);
        ObjectFinder.Find<NavMeshGenerator>().ClearNavMesh();
    }
    
}
