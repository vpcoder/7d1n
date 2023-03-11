using System;

namespace Engine.Data
{

    public enum GenderType
    {
        Male,
        Female,
    };

    [Serializable]
    public class AccountStoryObject : IStoryObject
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
    public class Account : ICharacterStoredObjectSerializable<AccountStoryObject>
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

        public GenderType Gender { get; set; } = GenderType.Male;

        #region Serialization

        public AccountStoryObject CreateData()
        {
            var data = new AccountStoryObject
            {
                IDValue  = Game.Instance.Runtime.PlayerID,
                SpriteID = SpriteID,
                Name     = Name,
                Gender   = Gender,
            };
            return data;
        }

        public void LoadFromData(AccountStoryObject data)
        {
            this.SpriteID = data.SpriteID;
            this.Name     = data.Name;
            this.Gender   = data.Gender;
        }

        #endregion

    }

}
