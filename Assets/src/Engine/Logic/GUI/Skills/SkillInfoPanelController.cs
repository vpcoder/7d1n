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
            var field = Game.Instance.Character.Exps.GetByExperienceType(skillItem.ExperienceType);
            if (field.points < 1 || Game.Instance.Character.Skills.HasSkill(skillItem.SkillID))
            {
                AudioController.Instance.PlaySound("ui/cant_add_skill");
                return;
            }

            AudioController.Instance.PlaySound("ui/add_skill");
            field.points--;
            Game.Instance.Character.Skills.AddSkill(skillItem.SkillID);
            skillItem.UpdateInfo();
            
            ObjectFinder.Find<CharacterSkillController>().UpdateAvailableSkillPoints();
            Hide();
        }

    }
    
}