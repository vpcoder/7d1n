using System.Collections.Generic;
using Engine.Logic.Locations.Generator;
using UnityEngine;

namespace Engine.Scenes.Loader
{
    
    public class LoadContext
    {
        public GameObject TopPanel { get; set; }
        public GameObject ScanUI { get; set; }
        public GameObject LocationGUI { get; set; }

        
        public List<EnemyPointInfo> EnemyListInfo { get; set; }
    }
    
}
