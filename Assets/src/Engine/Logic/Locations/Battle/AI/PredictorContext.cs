using System.Collections.Generic;
using Engine.Data;

namespace Engine.Logic.Locations
{
    
    /// <summary>
    /// 
    /// Контекст предиктора, которые позволяет ориентироваться в окружении
    /// ---
    /// Predictor context, which allows you to navigate the environment
    /// 
    /// </summary>
    public struct PredictorContext
    {
        
        /// <summary>
        /// 
        /// </summary>
        public OrderGroup OrderGroup { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public CharacterNpcBehaviour Npc { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public ICollection<CharacterNpcBehaviour> CurrentGroupNpc { get; set; }
    }
    
}