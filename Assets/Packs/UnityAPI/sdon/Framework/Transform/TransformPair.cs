using System;

namespace UnityEngine
{
    
    [Serializable]
    public struct TransformPair
    {
        [SerializeField]
        public Vector3 position;
        [SerializeField]
        public Quaternion rotation;
    }
    
}