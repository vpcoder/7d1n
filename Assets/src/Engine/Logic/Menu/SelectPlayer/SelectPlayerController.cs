using System.Collections.Generic;
using Engine.Data;
using Engine.Data.Stories;
using Engine.DB;
using Engine.EGUI;
using UnityEngine;

namespace Engine.Logic
{

    /// <summary>
    /// Интерфейс выбора текущего персонажа
    /// </summary>
    public class SelectPlayerController : ListView<SelectPlayerItem, Player>
    {

        #region Hidden Fields
#pragma warning disable 0649, IDE0044
        
        [SerializeField] private AddNewPlayerController addNewPlayerController;
        [SerializeField] private DeletePlayerController deletePlayerController;

#pragma warning restore 0649, IDE0044
        #endregion
        
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
            foreach(var item in Items)
            {
                if (item == selected) // Это тот элемент на который нажали
                    item.DoSelect(); // Выделяем его
                else
                    item.DoUnselect(); // Снимаем выделение
            }
            // Запоминаем выбранного персонажа
            this.Selected = selected.Player;
        }
        
        public override void InitItem(Player model, SelectPlayerItem item, int index)
        {
            item.Player = model;
        }

        public override void DisposeItem(SelectPlayerItem item)
        { }

        public override ICollection<Player> ProvideModels()
        {
            return new[]
            {
                PlayerFactory.Instance.GetAll()[0],
                new Player()
                {
                    BodyID = 0,
                    ID = 2,
                    Name = "test"
                },
                new Player()
                {
                    BodyID = 0,
                    ID = 3,
                    Name = "dsf"
                },
                new Player()
                {
                    BodyID = 0,
                    ID = 4,
                    Name = "ghf"
                },
            };
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
