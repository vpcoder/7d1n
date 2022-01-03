using Engine.Data.Factories;
using Engine.DB;
using Engine.EGUI;
using UnityEngine;

namespace Engine.Logic
{

    /// <summary>
    /// Интерфейс добавления нового персонажа
    /// </summary>
    public class AddNewPlayerController : Panel
    {

        #region Hidden Fields
        #pragma warning disable 0649, IDE0044, CS0414, IDE0051

        [SerializeField] private UnityEngine.UI.Text txtName;
        [SerializeField] private UnityEngine.UI.Image imgAvatar;
        [SerializeField] private UnityEngine.UI.Text txtDescription;

        #pragma warning restore 0649, IDE0044, CS0414, IDE0051
        #endregion

        /// <summary>
        /// Текущий выбранный аватар (индекс из массива avatars)
        /// </summary>
        private int index = 0;

        /// <summary>
        /// Список аватаров, из которых игрок будет выбирать своего персонажа
        /// </summary>
        private readonly long[] bodies = new[] { 0L };

        /// <summary>
        /// Идентификатор спрайта нового персонажа
        /// </summary>
        public long BodyID
        {
            get
            {
                return bodies[index];
            }
        }

        /// <summary>
        /// Видимое имя нового персонажа
        /// </summary>
        public string Name
        {
            get
            {
                return txtName.text.Trim();
            }
        }

        /// <summary>
        /// Идентификатор нового персонажаа
        /// </summary>
        public long ID
        {
            get
            {
                return PlayerFactory.Instance.NextID;
            }
        }

        public override void Show()
        {
            base.Show();
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            //imgAvatar.sprite = EnemyFactory.Instance.GetBody(SpriteID);
        }

        public void DoNextAvatarClick()
        {
            // Следующий индекс (сразу создаём кольцо)
            index = (index + 1) % bodies.Length;
            UpdateInfo();
        }

        public void DoPreviousAvatarClick()
        {
            // Предыдущий индекс
            index--;
            if (index < 0) // Создаём кольцо
                index = bodies.Length - 1;
            UpdateInfo();
        }

        /// <summary>
        /// Нажали на добавление нового персонажа
        /// </summary>
        public void DoAddClick()
        {
            var player = new Player()
            {
                ID = ID,
                Name = Name,
                BodyID = BodyID,
            };

            if (player.Name == "")
                return;

            var controller = ObjectFinder.Find<SelectPlayerController>();
            controller.CreateNewPlayer(player);
            Hide();
            controller.Show();
        }

        /// <summary>
        /// Нажали на отмену
        /// </summary>
        public void DoCancelClick()
        {
            Hide();
            ObjectFinder.Find<SelectPlayerController>().Show();
        }

    }

}
