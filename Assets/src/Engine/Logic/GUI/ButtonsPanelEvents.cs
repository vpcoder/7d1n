using Engine.Data;
using Engine.EGUI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    public class ButtonsPanelEvents : MonoBehaviour
    {

        [SerializeField] private CharacterBag inventory;
        [SerializeField] private CharacterEquipController equip;
        [SerializeField] private CharacterSkillController skills;
        [SerializeField] private CraftController craft;
        [SerializeField] private SettingsMenuController menu;

        [SerializeField] private Image imgInventory;
        [SerializeField] private Image imgEquip;
        [SerializeField] private Image imgSkills;
        [SerializeField] private Image imgCraft;
        [SerializeField] private Image imgMenu;

        private IDictionary<IPanel, Image> panels;

        private void Start()
        {
            panels = new Dictionary<IPanel, Image>();
            panels.Add(inventory, imgInventory);
            panels.Add(skills, imgSkills);
            panels.Add(equip, imgEquip);
            panels.Add(craft, imgCraft);
            panels.Add(menu, imgMenu);
        }

        private void Switch(IPanel panel)
        {
            if (panel.Visible)
            {
                panels[panel].color = new Color(0.35f, 1f, 0f);
                panel.Hide();
                Game.Instance.Runtime.Mode = Game.Instance.Runtime.BattleFlag ? Mode.Battle : Mode.Game;
            }
            else
            {
                Game.Instance.Runtime.Mode = Mode.GUI;
                panel.Show();
                panels[panel].color = new Color(1f, 1f, 0f);
                foreach (var pair in panels)
                {
                    var other = pair.Key;
                    if (other != panel)
                    {
                        other.Hide();
                        pair.Value.color = new Color(0.35f, 1f, 0f);
                    }
                }
            }
        }

        public void SwitchInventory()
        {
            Switch(inventory);
        }

        public void SwitchSkills()
        {
            Switch(skills);
        }
        
        public void SwitchCraft()
        {
            Switch(craft);
        }

        public void SwitchEquip()
        {
            Switch(equip);
        }

        public void ShwitchSettings()
        {
            Switch(menu);
        }

    }

}
