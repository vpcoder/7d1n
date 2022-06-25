using System;
using System.Text;
using Engine.Data;
using Engine.EGUI;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{
    public class CharacterSkillController : Panel
    {
        
        #region Hidden Fields
        
        [SerializeField] private RectTransform scrapSkillsMap;
        [SerializeField] private RectTransform craftSkillsMap;
        [SerializeField] private RectTransform lootSkillsMap;
        [SerializeField] private RectTransform battleSkillsMap;
        [SerializeField] private RectTransform mainSkillsMap;

        [SerializeField] private RectTransform skillMapContent;
        [SerializeField] private Text txtSkillsInfo;

        private readonly StringBuilder builder = new StringBuilder(1024);
        private ExperienceType currentSkillMap;
        private SkillItemBehaviour selectedSkillItem;

        #endregion
        
        #region Properties
        
        public ExperienceType CurrentSkillMap => currentSkillMap;

        #endregion
        
        private RectTransform GetPrefabByMapType(ExperienceType skillMapType)
        {
            switch (skillMapType)
            {
                case ExperienceType.Scrap: return scrapSkillsMap;
                case ExperienceType.Fight: return battleSkillsMap;
                case ExperienceType.Craft: return craftSkillsMap;
                case ExperienceType.Loot: return lootSkillsMap;
                case ExperienceType.Main: return mainSkillsMap;
                default: throw new NotSupportedException();
            }
        }

        private void Switch(ExperienceType type)
        {
            currentSkillMap = type;
            skillMapContent.DestroyAllChilds();
            var map = Instantiate(GetPrefabByMapType(type), skillMapContent);
            skillMapContent.sizeDelta = map.sizeDelta;
            skillMapContent.position = Vector3.zero;
            UpdateAvailableSkillPoints();
            ObjectFinder.Find<SkillInfoPanelController>().Hide();
        }

        public void UpdateAvailableSkillPoints()
        {
            var field = Game.Instance.Character.Exps.GetByExperienceType(currentSkillMap);

            builder.Append(Localization.Instance.Get("ui/skills/current_level"));
            builder.Append("<color=\"#0f0\">");
            builder.Append(field.Level);
            builder.Append("</color>\n");
            builder.Append(Localization.Instance.Get("ui/skills/current_exp"));
            builder.Append(field.Experience);
            builder.Append("/");
            builder.Append(field.MaxExperience);
            builder.Append("\n");
            builder.Append(Localization.Instance.Get("ui/skills/available_points"));
            builder.Append("<color=\"#0f0\">");
            builder.Append(field.Points);
            builder.Append("</color>\n");

            txtSkillsInfo.text = builder.ToString();
            builder.Clear();
        }
        
        public override void Show()
        {
            base.Show();
            Switch(ExperienceType.Main);
        }

        public void DoSelectSkillItem(SkillItemBehaviour skillItem)
        {
            if (skillItem == null || skillItem == selectedSkillItem)
            {
                HideSkillItemInfo();
                return;
            }

            if (selectedSkillItem != null)
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
            if (selectedSkillItem != null)
                selectedSkillItem.DoUnselect();
            selectedSkillItem = null;
            ObjectFinder.Find<SkillInfoPanelController>().Hide();
        }

        public void OnBattleTabClick()
        {
            Switch(ExperienceType.Fight);
        }

        public void OnLootTabClick()
        {
            Switch(ExperienceType.Loot);
        }

        public void OnCraftTabClick()
        {
            Switch(ExperienceType.Craft);
        }

        public void OnScrapTabClick()
        {
            Switch(ExperienceType.Scrap);
        }

        public void OnMainTabClick()
        {
            Switch(ExperienceType.Main);
        }
    }
}