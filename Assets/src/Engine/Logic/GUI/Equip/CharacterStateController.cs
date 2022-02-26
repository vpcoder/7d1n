using Engine.Data;
using Engine.Logic.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    public class CharacterStateController : MonoBehaviour
    {

        [SerializeField] private Text txtHeader1;
        [SerializeField] private Text txtHeader2;
        [SerializeField] private Text txtDescription;
        [SerializeField] private List<StateFieldAction> fields;
        private StringBuilder builder = new StringBuilder(2048);

        public void UpdateInfo()
        {
            txtHeader1.text = CreateHeader1Text();
            txtHeader2.text = CreateHeader2Text();
            txtDescription.text = CreateDescriptionText();
            foreach (var field in fields) // Обновляем кнопки
                field.UpdateInfo();
        }

        private string CreateHeader1Text()
        {
            builder.Clear();
            builder.Append(Localization.Instance.Get("msg_name")).Append(": ").Append(Game.Instance.Character.Account.Name).Append("\n");
            builder.Append(Localization.Instance.Get("msg_health")).Append(": ").Append(Game.Instance.Character.State.Health).Append("/").Append(Game.Instance.Character.State.MaxHealth).Append("\n");
            builder.Append(Localization.Instance.Get("msg_protection")).Append(": ").Append(BattleCalculationService.GetProtectionPercentText(CurrentCharacterCalculationService.CurrentProtection())).Append("\n");
            return builder.ToString();
        }

        private string CreateHeader2Text()
        {
            builder.Clear();
            builder.Append(Localization.Instance.Get("msg_mainlevel")).Append(": ").Append(Game.Instance.Character.Exps.MainExperience.Level).Append("\n");
            builder.Append(Localization.Instance.Get("msg_mainexp")).Append(" ").Append(Game.Instance.Character.Exps.MainExperience.Experience).Append("/").Append(Game.Instance.Character.Exps.MainExperience.MaxExperience).Append("\n");
            builder.Append(Localization.Instance.Get("msg_mainpoints")).Append(" ").Append(Game.Instance.Character.Exps.MainExperience.Points).Append("\n");
            return builder.ToString();
        }

        private string CreateDescriptionText()
        {
            builder.Clear();

            return builder.ToString();
        }

        /// <summary>
        /// Поднятие параметра
        /// </summary>
        /// <param name="type">Какой параметр повышают</param>
        public void OnStateFieldClick(StateFieldType type)
        {
            if (Game.Instance.Character.Exps.MainExperience.Points <= 0)
                return;

            switch (type)
            {
                case StateFieldType.Agility:
                    Game.Instance.Character.Parameters.Agility++;
                    if(Game.Instance.Character.Parameters.Agility % 100 == 0)
                        Game.Instance.Character.State.MaxAP++;
                    break;
                case StateFieldType.Endurance:
                    Game.Instance.Character.Parameters.Endurance++;
                    Game.Instance.Character.State.MaxHealth++;
                    Game.Instance.Character.State.Health++;
                    break;
                case StateFieldType.Intellect:
                    Game.Instance.Character.Parameters.Intellect++;
                    break;
                case StateFieldType.Strength:
                    Game.Instance.Character.Parameters.Strength++;
                    Game.Instance.Character.State.MaxWeight++;
                    break;
                default:
                    throw new NotSupportedException();
            }

            Game.Instance.Character.Exps.MainExperience.Points--;
            UpdateInfo();
        }

    }

}
