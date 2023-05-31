namespace UnityEngine
{
    
    public static class MaterialAdditions
    {
        
        private const string FIELD_EMISSION_COLOR = "_EmissionColor";
        
        public static void SetEmissionColor(this Material material, Color color)
        {
            material.SetColor(FIELD_EMISSION_COLOR, color);
        }
        
    }
    
}