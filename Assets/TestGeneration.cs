using Engine.Logic.Locations;
using Engine.Logic.Locations.Generator;
using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;

public class TestGeneration : MonoBehaviour
{

    private void Start()
    {
        var list = GameObject.FindObjectsOfType<MarkerBase>();
        LocationGenerateContex.GenerateByMarkers(list);

        foreach (var item in list)
            GameObject.Destroy(item.gameObject);

        ObjectFinder.Find<NavMeshGenerator>().CreateNavMesh();
    }

}
