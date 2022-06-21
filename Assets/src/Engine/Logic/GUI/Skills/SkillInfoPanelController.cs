using System;
using Engine.Data;
using Engine.Data.Factories;
using Engine.EGUI;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{
    
    public class SkillInfoPanelController : Panel
    {

        #region Hidden Fields

        [SerializeField] private Text txtName;
        [SerializeField] private Text txtDescription;
        [SerializeField] private Image imgIcon;

        private SkillItemBehaviour skillItem;
        
        #endregion

        public void Show(SkillItemBehaviour skillItem)
        {
            this.skillItem = skillItem;
            base.Show();
            var skill = SkillFactory.Instance.Get(skillItem.SkillID);
            txtName.text = Localization.Instance.Get(skill.Name);
            txtDescription.text = Localization.Instance.Get(skill.Description);
            imgIcon.sprite = skillItem.Icon;
        }

        public void OnBuySkillClick()
        {
            var field = GetField();
            if (field.points < 1 || Game.Instance.Character.Skills.HasSkill(skillItem.SkillID))
            {
                AudioController.Instance.PlaySound("ui/cant_add_skill");
                return;
            }

            AudioController.Instance.PlaySound("ui/add_skill");
            field.points--;
            Game.Instance.Character.Skills.AddSkill(skillItem.SkillID);
            skillItem.UpdateInfo();

            Hide();
        }

        private ExpField GetField()
        {
            var exps = Game.Instance.Character.Exps;
            switch (skillItem.ExperienceType)
            {
                case ExperienceType.Craft: return exps.CraftExperience;
                case ExperienceType.Fight: return exps.FightExperience;
                case ExperienceType.Loot:  return exps.LootExperience;
                case ExperienceType.Main:  return exps.MainExperience;
                case ExperienceType.Scrap: return exps.ScrapExperience;
                default:
                    throw new NotSupportedException("exp type '" + skillItem.ExperienceType + "' isn't supported!");
            }
        }
        
    }
    
}