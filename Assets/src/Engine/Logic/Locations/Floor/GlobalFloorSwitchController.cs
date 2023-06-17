using System.Collections.Generic;
using System.Linq;
using Engine.Logic.Locations.Floor;
using UnityEngine;

namespace Engine.Logic.Locations
{

    public class GlobalFloorSwitchController : MonoBehaviour
    {
        
        [SerializeField] private List<FloorData> floorDatas;
        [SerializeField] private int currentFloor = 1;

        public int CurrentFloor => currentFloor;

        public int MaxFloor
        {
            get
            {
                if (Lists.IsEmpty(floorDatas))
                    return 1;
                return floorDatas.Max(floor => floor.FloorIndex);
            }
        }
        
        public void SetMaxFloor()
        {
            SetFloor(MaxFloor);
        }
        
        public void SetFloor(int floor)
        {
            if (floor < 1)
                floor = 1;

            var max = MaxFloor;
            if (floor > max)
                floor = max;

            foreach (var floorData in floorDatas)
            {
                var visible = floor >= floorData.FloorIndex;
                foreach (var item in floorData.ObjectList)
                    item.SetActive(visible);
            }
        }

    }
    
}