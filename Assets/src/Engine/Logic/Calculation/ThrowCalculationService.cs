using Engine.Data;

namespace Engine
{
    
    public class ThrowCalculationService
    {

        public static float GetThrowDistance(IEdgedWeapon weapon)
        {
            var weaponDistance = weapon.ThrowDistance;

            return weaponDistance;
        }
        
    }
    
}