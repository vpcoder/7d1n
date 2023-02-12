using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations
{
    
    public class FloorSwitchController : MonoBehaviour
    {
        
        [SerializeField] private List<GameObject> floor2;
        [SerializeField] private List<GameObject> floor3;
        [SerializeField] private List<GameObject> floor4;
        [SerializeField] private List<GameObject> floor5;

        [SerializeField] private int currentFloor = 1;
        [SerializeField] private int maxFloor = 1;

        private IDictionary<int, List<GameObject>> floorToList;

        public int CurrentFloor => currentFloor;

        public void SetMaxFloor()
        {
            SetFloor(maxFloor);
        }
        
        public void SetFloor(int floor)
        {
            if (floor < 1)
                floor = 1;
            if (floor > maxFloor)
                floor = maxFloor;

            if(floorToList == null)
                InitCahce();
            
            foreach (var entry in floorToList)
            {
                var switchFloor = entry.Key;
                var visible = floor >= switchFloor;
                foreach (var item in entry.Value)
                    item.SetActive(visible);
            }
        }

        private void InitCahce()
        {
            floorToList = new Dictionary<int, List<GameObject>>();
            floorToList.Add(2, floor2);
            floorToList.Add(3, floor3);
            floorToList.Add(4, floor4);
            floorToList.Add(5, floor5);
        }
        
    }
    
}