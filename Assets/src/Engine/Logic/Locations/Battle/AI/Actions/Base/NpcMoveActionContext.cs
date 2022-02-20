using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Engine.Logic.Locations
{

    public class NpcMoveActionContext : NpcBaseActionContext
    {

        public Vector3 StartPosition { get; set; }

        public float Timestamp { get; set; } = 0f;

        public List<Vector3> Path { get; set; }

        public float Speed { get; set; } = 1f;

    }

}
