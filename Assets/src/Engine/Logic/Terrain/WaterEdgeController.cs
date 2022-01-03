using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic
{

    public enum WaterEdgeDirection : byte
    {
        Forward,
        Back,
        Left,
        Right,
    };

    public class WaterEdgeController : MonoBehaviour
    {

        [SerializeField] private List<GameObject> forwardItems;
        [SerializeField] private List<GameObject> backItems;
        [SerializeField] private List<GameObject> leftItems;
        [SerializeField] private List<GameObject> rightItems;

        private List<GameObject> GetEdgeByDirection(WaterEdgeDirection direction)
        {
            switch (direction)
            {
                case WaterEdgeDirection.Forward: return forwardItems;
                case WaterEdgeDirection.Back:    return backItems;
                case WaterEdgeDirection.Left:    return leftItems;
                case WaterEdgeDirection.Right:   return rightItems;
                default:
                    throw new NotSupportedException();
            }
        }

        public void HideItems(WaterEdgeDirection direction)
        {
            foreach(var edge in GetEdgeByDirection(direction))
                edge.SetActive(false);
        }

    }

}
