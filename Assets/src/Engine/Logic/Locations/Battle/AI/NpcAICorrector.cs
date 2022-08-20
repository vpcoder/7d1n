using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Logic.Locations
{

    public class NpcAICorrector
    {
        private static readonly Lazy<NpcAICorrector> instance = new Lazy<NpcAICorrector>(() => new NpcAICorrector());
        public static NpcAICorrector Instance { get { return instance.Value; } }
        private NpcAICorrector() { }


    }

}
