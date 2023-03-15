using System;
using UnityEngine;
using UnityEngine.AI;

namespace Engine.Logic.Locations
{

    public class NavMeshGenerator : MonoBehaviour
    {
        [SerializeField] private bool createNavMeshOnStart = false;
        
        public void Start()
        {
            if(createNavMeshOnStart)
                CreateNavMesh();
        }

        public void RefreshNavMesh()
        {
            ClearNavMesh();
            CreateNavMesh();
        }
        
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
