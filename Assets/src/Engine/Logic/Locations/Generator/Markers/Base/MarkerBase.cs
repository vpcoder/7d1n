using UnityEngine;

namespace Engine.Logic.Locations.Generator.Markers
{

    public abstract class MarkerBase : MonoBehaviour, IMarker
    {
        [SerializeField]
        private bool isTwoSide = true;

        public GameObject ToObject
        {
            get
            {
                return gameObject;
            }
        }

        public bool IsTwoSide
        {
            get
            {
                return isTwoSide;
            }
        }

        public Vector3 Rotation
        {
            get
            {
                return transform.rotation.eulerAngles;
            }
            set
            {
                transform.rotation = Quaternion.Euler(value);
            }
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }

        public abstract Vector3 Bounds { get; }

    }

}
