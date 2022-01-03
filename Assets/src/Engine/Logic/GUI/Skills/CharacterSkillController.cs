using Engine.EGUI;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic
{

    public class CharacterSkillController : Panel
    {

        [SerializeField] private List<SkillsTab> tabs;

        private void Switch(SkillsTabType type)
        {
            foreach(var tab in tabs)
            {
                if (tab.Type == type)
                    tab.Show();
                else
                    tab.Hide();
            }
        }

        public override void Show()
        {
            base.Show();
            Switch(SkillsTabType.BattleTab);
        }

        public void OnBattleTabClick()
        {
            Switch(SkillsTabType.BattleTab);
        }

        public void OnSearshTabClick()
        {
            Switch(SkillsTabType.SearshTab);
        }

        public void OnCraftTabClick()
        {
            Switch(SkillsTabType.CraftTab);
        }

    }

}
