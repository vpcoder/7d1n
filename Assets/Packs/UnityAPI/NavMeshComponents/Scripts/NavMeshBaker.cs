using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    
    public void Bake(IEnumerable<NavMeshSurface> surfaces)
    {
        foreach (var surface in surfaces)
        {
            surface.BuildNavMesh();
        }
    }

    public void Clear(IEnumerable<NavMeshSurface> surfaces)
    {
        foreach (var surface in surfaces)
            surface.RemoveData();
    }
    
}

