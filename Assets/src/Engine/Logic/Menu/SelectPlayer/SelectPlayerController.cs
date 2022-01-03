using Engine.Data;
using Engine.Data.Stories;
using Engine.DB;
using Engine.EGUI;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic
{

    /// <summary>
    /// Интерфейс выбора текущего персонажа
    /// </summary>
    public class SelectPlayerController : Panel
    {

        #region Hidden Fields
#pragma warning disable 0649, IDE0044

        [SerializeField] private SelectPlayerItem playerItemPrefab;
        [SerializeField] private GameObject content;
        [SerializeField] private AddNewPlayerController addNewPlayerController;
        [SerializeField] private DeletePlayerController deletePlayerController;

#pragma warning restore 0649, IDE0044
        #endregion

        /// <summary>
        /// Персонажи, которые были загружены из БД
        /// </summary>
        private List<Player> players;

        /// <summary>
        /// Все созданные UI элементы, которые сейчас есть в этом интерфейсе
        /// </summary>
        private readonly List<SelectPlayerItem> items = new List<SelectPlayerItem>();

        /// <summary>
        /// Персонаж, на которого нажимали в последний раз
        /// </summary>
        private Player Selected { get; set; }

        /// <summary>
        /// Игрок выделил конкретного персонажа
        /// </summary>
        /// <param name="selected">Персонаж, на которого нажал игрок</param>
        public void DoSelect(SelectPlayerItem selected)
        {
            foreach(var item in items)
            {
                if (item == selected) // Это тот элемент на который нажали
                    item.DoSelect(); // Выделяем его
                else
                    item.DoUnselect(); // Снимаем выделение
            }
            // Запоминаем выбранного персонажа
            this.Selected = selected.Player;
        }

        public override void Show()
        {
            items.Clear();
            content.transform.DestroyAllChilds();

            var rect = (RectTransform)content.transform;
            var height = ((RectTransform)playerItemPrefab.transform).rect.height;

            // Загружаем персонажей из БД
            players = PlayerFactory.Instance.GetAll();

            // Рассчитываем высоту контента, основываясь на высоте одного элемента персонажа и количестве персонажей в БД
            rect.sizeDelta = new Vector2(rect.sizeDelta.x, (height + 10) * players.Count);

            base.Show();

            // Создаём элементы под каждого персонажа
            foreach (var player in players)
                items.Add(CreateItem(player));
        }

        /// <summary>
        /// Создаёт и возвращает UI элемент персонажа
        /// </summary>
        /// <param name="player">Данные о персонаже</param>
        /// <returns>Созданный UI элемент персонажа</returns>
        private SelectPlayerItem CreateItem(Player player)
        {
            var index = items.Count;
            var height = ((RectTransform)playerItemPrefab.transform).rect.height;

            var item = GameObject.Instantiate<SelectPlayerItem>(playerItemPrefab, content.transform);
            item.Player = player;

            var rect = (RectTransform)item.transform;
            rect.localPosition = new Vector3(rect.localPosition.x, -index * (height + 10));

            return item;
        }

        /// <summary>
        /// Создание нового персонажа
        /// </summary>
        /// <param name="player">Создаваемый персонаж</param>
        public void CreateNewPlayer(Player player)
        {// Устанавливаем текущего песронажа в рантайме
            Game.Instance.Runtime.PlayerID = player.ID;

            // Заводим информацию о новом персонаже в хранилище
            Game.Instance.Character.Account.SpriteID = player.BodyID;
            Game.Instance.Character.Account.Name     = player.Name;
            CharacterStory.Instance.AccountStory.Save(Game.Instance.Character.Account.CreateData());

            // Добавляем персонажа в БД
            PlayerFactory.Instance.Save(player);

            // Выбираем созданного персонажа
            SelectPlayer(player);
        }

        /// <summary>
        /// Выбор персонажа, из списка уже имеющихся у игрока
        /// </summary>
        /// <param name="player">Персонаж</param>
        public void SelectPlayer(Player player)
        {
            // Устанавливаем текущего песронажа в рантайме
            Game.Instance.Runtime.PlayerID = player.ID;

            // Сохраняем выбранного персонажа в настройки
            GameSettings.Instance.Settings.PlayerID = player.ID;
            GameSettings.Instance.SaveSettings();

            // Подтягиваем информацию о персонаже
            CharacterStory.Instance.LoadAll(Game.Instance.Character);
        }

        public void OnSelectClick()
        {
            if(Selected != null)
                SelectPlayer(Selected);
            Hide();
        }

        public void OnDeleteClick()
        {
            if (Selected == null)
                return; // Ничего не выбирали, нечего удалять

            // Отображаем интерфейс удаления персонажа
            deletePlayerController.Player = Selected;
            deletePlayerController.Show();
            Hide();
        }

        public void OnAddClick()
        {
            // Отображаем интерфейс добавления нового персонажа
            addNewPlayerController.Show();
            Hide();
        }

    }

}
