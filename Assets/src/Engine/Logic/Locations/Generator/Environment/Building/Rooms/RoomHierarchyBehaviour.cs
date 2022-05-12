using System.Collections.Generic;
using System.Linq;
using Engine.Logic.Locations.Generator.Markers;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building
{
    
    public class RoomHierarchyBehaviour : MonoBehaviour
    {

        [SerializeField] private RoomKindType roomType;
        [SerializeField] private List<MarkerBase> markers;
        
        public RoomKindType RoomType
        {
            get { return roomType; }
        }
        
        public ICollection<IMarker> GetMarkers()
        {
            return markers.Select(marker => (IMarker)marker).ToList(); // Достаём маркеры
        }
        
    }
    
}
