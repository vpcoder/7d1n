using Engine.Data;
using Engine.Data.Factories;
using Engine.DB;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    /// <summary>
    /// Выбираемый персонаж
    /// </summary>
    public class SelectPlayerItem : MonoBehaviour
    {

#pragma warning disable 0649, IDE0044

        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unselectedColor;
        [SerializeField] private Image background;
        [SerializeField] private Text  txtName;
        [SerializeField] private Image imgSprite;
        [SerializeField] private bool  selected = false;

#pragma warning restore 0649, IDE0044

        private Player player;
        
        public Player Player
        {
            get
            {
                return this.player;
            }
            set
            {
                this.player = value;
                UpdateInfo();
            }
        }

        private void UpdateInfo()
        {
            txtName.text = Player.Name;
            //imgSprite.sprite = EnemyFactory.Instance.GetBody(Player.BodyID);
            selected = GameSettings.Instance.Settings.PlayerID == Player.ID;
            if (selected)
                DoSelect();
        }

        public void DoSelect()
        {
            background.color = selectedColor;
        }

        public void DoUnselect()
        {
            background.color = unselectedColor;
        }

        public void OnSelect()
        {
            ObjectFinder.Find<SelectPlayerController>().DoSelect(this);
        }

    }

}
