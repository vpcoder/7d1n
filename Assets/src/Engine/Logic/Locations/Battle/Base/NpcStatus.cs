using System;
using Engine.Data;

namespace Engine.Logic.Locations
{

    [Serializable]
    public class NpcStatus
    {
        public NpcStateType State = NpcStateType.Normal;
        public bool IsDead;
    }

}
