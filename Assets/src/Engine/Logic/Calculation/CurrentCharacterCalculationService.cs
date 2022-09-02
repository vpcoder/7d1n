using Engine.Data;

namespace Engine
{
    public static class CurrentCharacterCalculationService
    {

        public static int CurrentProtection() {
            return Game.Instance.Character.State.Protection + Game.Instance.Character.Equipment.Protection;
        }

    }
}
