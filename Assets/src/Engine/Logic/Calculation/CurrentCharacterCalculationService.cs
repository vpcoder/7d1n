using Engine.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class CurrentCharacterCalculationService
    {

        public static int CurrentProtection() {
            return Game.Instance.Character.State.Protection + Game.Instance.Character.Equipment.Protection;
        }

    }
}
