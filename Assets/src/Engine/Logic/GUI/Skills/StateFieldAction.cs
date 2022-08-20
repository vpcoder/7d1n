using Engine.Data;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    public enum StateFieldType : int
    {
        Intellect = 0x00, // Интеллект
        Strength  = 0x01, // Сила
        Agility   = 0x02, // Ловкость
        Endurance = 0x03, // Выносливость
    };

    public class StateFieldAction : MonoBehaviour
    {

        [SerializeField] private StateFieldType type;
        [SerializeField] private Button button;
        [SerializeField] private Text txtValue;
        [SerializeField] private RectTransform progress;

        private void Start()
        {
            UpdateInfo();
        }

        public void OnClick()
        {
            ObjectFinder.Find<CharacterStateController>().OnStateFieldClick(type);
        }

        public void UpdateInfo()
        {
            txtValue.text = (Value / 10).ToString();
            var enabled = Game.Instance.Character.Exps.MainExperience.Points > 0; // Есть доступные очки?
            button.gameObject.SetActive(enabled);

            var progressValue = Value % 10;
            if (progressValue == 0)
            {
                progress.gameObject.SetActive(false);
            }
            else
            {
                progress.gameObject.SetActive(true);
                progress.sizeDelta = new Vector2(80 / 10 * progressValue, 0);
            }
        }

        private long Value
        {
            get
            {
                switch(type)
                {
                    case StateFieldType.Agility:   return Game.Instance.Character.Parameters.Agility;
                    case StateFieldType.Endurance: return Game.Instance.Character.Parameters.Endurance;
                    case StateFieldType.Intellect: return Game.Instance.Character.Parameters.Intellect;
                    case StateFieldType.Strength:  return Game.Instance.Character.Parameters.Strength;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

    }

}
