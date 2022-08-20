using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{
    
    /// <summary>
    /// 
    /// Якорь и границы объекта, необходимые для вычисления расположений одного объекта на другом
    /// ---
    /// Anchor and object boundaries needed to calculate the locations of one object on another
    /// 
    /// </summary>
    [RequireComponent(typeof(BoxCollider))]
    public class SurfaceLocalAnchorBehaviour : MonoBehaviour
    {

        #region Hidden Fields
        
        /// <summary>
        ///     Точка локальной оси координат снизу, слева, на себя
        ///     ---
        ///     The point of the local coordinate axis from below, to the left, on itself
        /// </summary>
        [SerializeField] private Vector3 anchorPos;
        
        #endregion

        #region Properties
        
        /// <summary>
        ///     Общие границы текущего объекта
        ///     ---
        ///     General boundaries of the current object
        /// </summary>
        public Bounds Bounds => GetComponent<BoxCollider>().bounds;
        
        /// <summary>
        ///     Точка локальной оси координат снизу, слева, на себя
        ///     ---
        ///     The point of the local coordinate axis from below, to the left, on itself
        /// </summary>
        public Vector3 AnchorPos => anchorPos; // TODO: Учитывать вращение в SurfaceLocalAnchorBehaviour

        public Vector3 TopSurfaceAnchorPos => transform.position + anchorPos + new Vector3(0f, Bounds.size.y, 0f);
        
        public Vector3 TopSurfaceCenterPos
        {
            get
            {
                var bounds = Bounds;
                return transform.position + anchorPos + new Vector3(-bounds.extents.x, bounds.size.y, -bounds.extents.z);
            }
        }
        
        public Vector3 BottomSurfaceAnchorPos => transform.position + anchorPos;
        
        public Vector3 BottomSurfaceCenterPos
        {
            get
            {
                var bounds = Bounds;
                return transform.position + anchorPos + new Vector3(-bounds.extents.x, 0f, -bounds.extents.z);
            }
        }

        #endregion
        
    }
    
}