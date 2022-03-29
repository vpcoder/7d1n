using Engine.Data;
using Engine.Logic.Locations;
using Engine.Logic.Locations.Generator;
using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;

public class TestGeneration : MonoBehaviour
{
    
    public void Make()
    {
        var list = GameObject.FindObjectsOfType<MarkerBase>(); // Маркеры
        Game.Instance.Runtime.GenerationInfo = LocationGenerateContex.Generate(Game.Instance.Runtime.Location); // Тестовые сведения о здании и этаже
        
        LocationGenerateContex.GenerateByMarkers(list);

        ObjectFinder.Find<NavMeshGenerator>().CreateNavMesh();
    }
    
}
