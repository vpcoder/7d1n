using System;

namespace Engine.Data
{

    [Serializable]
    public class AccountRepositoryObject : IRepositoryObject
    {
        public long         ID { get { return IDValue; } set { } }
        public long         IDValue;
        public long         SpriteID;
        public GenderType   Gender;
        public string       Name;
    }

    /// <summary>
    /// 
    /// Информация о учётной записи персонажа.
    /// Может содержать сведение о персонаже или игроке
    /// ---
    /// Character account information.
    /// May contain information about the character or player
    /// 
    /// </summary>
    public class Account : ICharacterStoredObjectSerializable<AccountRepositoryObject>
    {

        /// <summary>
        ///     Идентификатор изображения персонажа
        ///     Показывает какой аватар будет и игрока
        ///     ---
        ///     The character's image identifier
        ///     Shows which avatar will be and player
        /// </summary>
        public long   SpriteID { get; set; } = 0L;

        /// <summary>
        ///     Видимое имя персонажа
        ///     Это имя видят остальные игроки
        ///     ---
        ///     The visible name of the character
        ///     This is the name the other players can see
        /// </summary>
        public string     Name   { get; set; } = "Robert";

        /// <summary>
        ///     Пол персонажа
        ///     ---
        ///     Character's gender
        /// </summary>
        public GenderType Gender { get; set; } = GenderType.Male;

        #region Serialization

        public AccountRepositoryObject CreateData()
        {
            var data = new AccountRepositoryObject
            {
                IDValue  = Game.Instance.Runtime.PlayerID,
                SpriteID = SpriteID,
                Name     = Name,
                Gender   = Gender,
            };
            return data;
        }

        public void LoadFromData(AccountRepositoryObject data)
        {
            this.SpriteID = data.SpriteID;
            this.Name     = data.Name;
            this.Gender   = data.Gender;
        }

        #endregion

    }

}
