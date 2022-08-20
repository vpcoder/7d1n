using Engine.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Engine.Logic
{
    
    public class SkillItemBehaviour : MonoBehaviour, IPointerDownHandler
    {
        
        #region Hidden Fields

        [SerializeField] private Color normalDisabledColor;
        [SerializeField] private Color normalEnabledColor;
        [SerializeField] private Color selectEnabledColor;
        [SerializeField] private Color selectDisabledColor;

        [SerializeField] private Color defaultSlotColor = new Color(0.1f, 0.15f, 0.17f, 1f);
        [SerializeField] private Color enabledSlotColor = new Color(0.1f, 0.15f, 0.17f, 1f);
        
        [SerializeField] private string skillID;
        [SerializeField] private Image imgIcon;
        [SerializeField] private Image imgFrame;

        [SerializeField] private ExperienceType experienceType;
        
        [SerializeField] private bool selected;
        #endregion

        public ExperienceType ExperienceType => experienceType;
        public string SkillID => skillID;
        public Sprite Icon => imgIcon.sprite;
        
        private void Start()
        {
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            var skillEnabled = Game.Instance.Character.Skills.HasSkill(skillID);
            imgFrame.color = skillEnabled ? enabledSlotColor : defaultSlotColor;
            
            if (selected)
            {
                imgIcon.color = skillEnabled ? selectEnabledColor : selectDisabledColor;
            }
            else
            {
                imgIcon.color = skillEnabled ? normalEnabledColor : normalDisabledColor;
            }
        }

        public void DoSelect()
        {
            selected = true;
            UpdateInfo();
        }

        public void DoUnselect()
        {
            selected = false;
            UpdateInfo();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            AudioController.Instance.PlaySound("ui/select_skill");
            ObjectFinder.Find<CharacterSkillController>().DoSelectSkillItem(this);
        }
        
    }
    
}