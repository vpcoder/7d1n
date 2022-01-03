using System;
using UnityEngine;

namespace Engine
{

    [Serializable]
    public class MoveContext
    {
        public Vector3 NextPosition;
        public Vector3 StartPosition;
        public Quaternion NextRotation;
        public Quaternion StartRotation;
        public float Distance;
        public float ChangePositionTimestamp;
    }

}
