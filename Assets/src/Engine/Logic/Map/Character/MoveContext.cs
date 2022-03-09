using System;
using UnityEngine;

namespace Engine
{

    [Serializable]
    public class MoveContext
    {
        public Vector3 NextPosition { get; set; }
        public Vector3 StartPosition { get; set; }
        public Quaternion NextRotation { get; set; }
        public Quaternion StartRotation { get; set; }
        public float Distance { get; set; }
        public float ChangePositionTimestamp { get; set; }
    }

}
