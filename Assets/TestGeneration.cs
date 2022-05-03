using Engine.Data;
using Engine.Logic.Locations;
using Engine.Logic.Locations.Generator;
using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;

public class TestGeneration : MonoBehaviour
{

    [SerializeField] private int seed;
    
    public void Make()
    {
        Clear();
        var list = FindObjectsOfType<MarkerBase>(); // Маркеры
        Game.Instance.Runtime.Location.ID = seed;
        Game.Instance.Runtime.GenerationInfo = LocationGenerateContex.Generate(Game.Instance.Runtime.Location); // Тестовые сведения о здании и этаже
        LocationGenerateContex.GenerateByMarkers(list);
        ObjectFinder.Find<NavMeshGenerator>().CreateNavMesh();
    }

    public void Clear()
    {
        var data = GameObject.Find("BuildData");
        if(data != null)
            DestroyImmediate(data);
        ObjectFinder.Find<NavMeshGenerator>().ClearNavMesh();
    }
    
}
