using UnityEngine;
using UnityEngine.AI;

namespace Engine.Logic.Locations
{

    public class NavMeshGenerator : MonoBehaviour
    {

        public void CreateNavMesh()
        {
            var baker = ObjectFinder.Find<NavMeshBaker>();
            var surfaces = FindObjectsOfType<NavMeshSurface>();
            baker.Bake(surfaces);
        }

        public void ClearNavMesh()
        {
            var baker = ObjectFinder.Find<NavMeshBaker>();
            var surfaces = FindObjectsOfType<NavMeshSurface>();
            baker.Clear(surfaces);
        }
        
    }

}
