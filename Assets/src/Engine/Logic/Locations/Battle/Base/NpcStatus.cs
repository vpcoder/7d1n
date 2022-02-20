using System;

namespace Engine.Logic.Locations
{

    public enum NpcStateMind : byte
    {
        Sleep,
        Normal,
        Agression,
    };

    [Serializable]
    public class NpcStatus
    {
        public NpcStateMind State = NpcStateMind.Normal;
        public bool IsDead = false;
    }

}
