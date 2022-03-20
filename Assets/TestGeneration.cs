using Engine.Data;
using Engine.Logic.Locations.Generator;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;
using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;

public class TestGeneration : MonoBehaviour
{

    private void Start()
    {
        //foreach (var item in list)
        //    GameObject.Destroy(item.gameObject);
        //
        //ObjectFinder.Find<NavMeshGenerator>().CreateNavMesh();
    }

    public void Make()
    {
        var list = GameObject.FindObjectsOfType<MarkerBase>();
        Game.Instance.Runtime.GenerationInfo = LocationGenerateContex.Generate(Game.Instance.Runtime.Location);
        LocationGenerateContex.GenerateByMarkers(list);
    }

}
