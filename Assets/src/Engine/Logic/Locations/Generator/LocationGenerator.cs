using UnityEngine;
using UnityEngine.AI;

namespace Engine.Logic.Locations
{

    public class LocationGenerator : MonoBehaviour
    {

        private void Awake()
        {
            CreateNavMesh();
            Destroy(this);
        }

        private void CreateNavMesh()
        {
            var baker = ObjectFinder.Find<NavMeshBaker>();
            var surfaces = GameObject.FindObjectsOfType<NavMeshSurface>();
            baker.Bake(surfaces);
        }

    }

}
