using Engine.EGUI;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic
{

    public class CharacterSkillController : Panel
    {

        [SerializeField] private List<SkillsTab> tabs;

        private SkillItemBehaviour selectedSkillItem;

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

        public void DoSelectSkillItem(SkillItemBehaviour skillItem)
        {
            if (skillItem == null || skillItem == selectedSkillItem)
            {
                HideSkillItemInfo();
                return;
            }
            
            if(selectedSkillItem != null)
                selectedSkillItem.DoUnselect();
            selectedSkillItem = skillItem;
            ShowSkillItemInfo();
        }

        public void ShowSkillItemInfo()
        {
            selectedSkillItem.DoSelect();
            ObjectFinder.Find<SkillInfoPanelController>().Show(selectedSkillItem);
        }
        
        public void HideSkillItemInfo()
        {
            if(selectedSkillItem != null)
                selectedSkillItem.DoUnselect();
            selectedSkillItem = null;
            ObjectFinder.Find<SkillInfoPanelController>().Hide();
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
