using System.Collections.Generic;
using System.Linq;
using Engine.Logic.Locations.Floor;
using src.Engine.Logic.Locations.Floor;
using UnityEngine;

namespace Engine.Logic.Locations
{

    /// <summary>
    ///
    /// Контроллер управляющий этажностью в сцене
    /// ---
    /// Controller controlling the floors in the scene
    /// 
    /// </summary>
    public class GlobalFloorSwitchController : MonoBehaviour
    {
        
        [SerializeField] private List<FloorData> floorDatas;
        [SerializeField] private int currentFloor = 1;
        
        /// <summary>
        ///     Текущая зона этажа, в которой нахоится персонаж игрока
        ///     ---
        ///     The current zone of the floor in which the player's character is located
        /// </summary>
        public FloorZoneSwitchBehaviour CurrentZone { get; set; }

        public int CurrentFloor => currentFloor;

        
        /// <summary>
        ///     Рассчитывает и возвращает максимальный уровень этажа в сцене
        ///     ---
        ///     Calculates and returns the maximum floor level in the scene
        /// </summary>
        public int MaxFloor
        {
            get
            {
                if (Lists.IsEmpty(floorDatas))
                    return 1;
                return floorDatas.Max(floor => floor.FloorIndex);
            }
        }
        
        /// <summary>
        ///     Устанавливает максимальный доступный уровень этажа
        ///     ---
        ///     Sets the maximum available floor level
        /// </summary>
        public void SetMaxFloor()
        {
            SetFloor(MaxFloor);
        }
        
        /// <summary>
        ///     Устанеавливает указанный уровень этажа.
        ///     Если этаж некорректный, выставится наиболее близкий.
        ///     ---
        ///     Sets the specified floor level.
        ///     If the floor is incorrect, the closest one is set.
        /// </summary>
        /// <param name="floor"></param>
        public void SetFloor(int floor)
        {
            if (floor < 1)
                floor = 1;

            var max = MaxFloor;
            if (floor > max)
                floor = max;

            if(currentFloor == floor)
                return;
            currentFloor = floor;
            
            foreach (var floorData in floorDatas)
            {
                var visible = floor >= floorData.FloorIndex;
                foreach (var item in floorData.ObjectList)
                    item.SetActive(visible);
            }
        }

        /// <summary>
        ///     Обновляет информацию о необходимости переключения текущего этажа
        ///     ---
        ///     Updates information about the need to switch the current floor
        /// </summary>
        /// <param name="characterLink">
        ///     Ссылка на персонажа игрока
        ///     ---
        ///     Link to player character
        /// </param>
        public void UpdateFloor(LocationCharacter characterLink)
        {
            var count = Physics.OverlapSphereNonAlloc(characterLink.transform.position, characterLink.PickUpDistance, tmpColliderList);
            var floorIndex = MaxFloor;
            for(int i = 0; i < count; i++)
            {
                var zone = tmpColliderList[i].gameObject.GetComponent<FloorZoneSwitchBehaviour>();
                if(zone == null)
                    continue;

                if (floorIndex > zone.EnterFloorIndex)
                    floorIndex = zone.EnterFloorIndex;
            }
            SetFloor(floorIndex);
        }
        private static Collider[] tmpColliderList = new Collider[20];

    }
    
}