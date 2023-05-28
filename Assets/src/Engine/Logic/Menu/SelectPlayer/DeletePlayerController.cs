using Engine.Data.Factories;
using Engine.Data.Repositories;
using Engine.DB;
using Engine.EGUI;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Engine.Logic
{

    /// <summary>
    /// Интерфейс удаления персонажа
    /// </summary>
    public class DeletePlayerController : Panel
    {

#pragma warning disable 0649, IDE0044

        [SerializeField] private Image imgAvatar;
        [SerializeField] private Text  txtName;
        [SerializeField] private Text  txtDescription;

#pragma warning restore 0649, IDE0044

        public Player Player { get; set; }

        public override void Show()
        {
            base.Show();
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            if (Player == null)
                return;

            //imgAvatar.sprite = EnemyFactory.Instance.GetBody(Player.BodyID);
            txtName.text = Player.Name;
            txtDescription.text = CreateDescriptionText();
        }

        private string CreateDescriptionText()
        {
            if (Player == null)
                return "";

            var character = CharacterRepository.Instance.ExpsRepository.Get(Player.ID);
            var builder = new StringBuilder();
            builder.Append(Localization.Instance.Get("msg_mainlevel"));
            builder.Append(": ");
            builder.AppendLine(character.MainExperience.Level.ToString());
            builder.Append(Localization.Instance.Get("msg_mainexp"));
            builder.Append(": ");
            builder.AppendLine(character.MainExperience.Experience.ToString());
            return builder.ToString();
        }

        public void OnDeleteClick()
        {
            Hide();
            CharacterRepository.Instance.Delete(Player.ID);
            PlayerFactory.Instance.Delete(Player.ID);
            ObjectFinder.Find<SelectPlayerController>().Show();
        }

        public void OnCancelClick()
        {
            Hide();
            ObjectFinder.Find<SelectPlayerController>().Show();
        }

    }

}
