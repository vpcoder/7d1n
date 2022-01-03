using Engine.EGUI;
using UnityEngine;

namespace Engine.Logic
{

    public enum SkillsTabType
    {
        BattleTab,
        CraftTab,
        SearshTab,
    }

    public class SkillsTab : Panel
    {

        [SerializeField] private SkillsTabType type;

        public SkillsTabType Type { get { return type; } }

    }

}
