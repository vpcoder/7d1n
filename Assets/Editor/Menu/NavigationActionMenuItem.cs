using Engine;
using Engine.Data.Factories;
using Engine.DB;
using GitIntegration.Items.Data;
using UnityEngine;
using UnityEngine.AI;

namespace UnityEditor.Menu
{
    
    public static class NavigationActionMenuItem
    {

        [MenuItem("7d1n/Navigation/Hide Nav Mesh Obstacle objects")]
        public static void ReloadDbAction()
        {
            foreach(NavMeshObstacle item in Object.FindObjectsOfTypeAll(typeof(NavMeshObstacle)))
                item.gameObject.SetActive(false);
        }
        
        [MenuItem("7d1n/Navigation/Show Nav Mesh Obstacle objects")]
        public static void ReloadItemsAction()
        {
            foreach(NavMeshObstacle item in Object.FindObjectsOfTypeAll(typeof(NavMeshObstacle)))
                item.gameObject.SetActive(true);
        }
        
    }
    
}