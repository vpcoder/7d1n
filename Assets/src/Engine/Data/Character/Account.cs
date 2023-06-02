using System;
using UnityEngine;

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
        
        /// <summary>
        ///     Рейтинг того, как персонаж относится к вирусу.
        ///     Чем выше рейтинг, тем персонаж более благосклонен к вирусу, и поддерживает его,
        ///     Чем ниже рейтинг, тем более персонаж ненавидит вирус
        ///     ---
        ///     The rating of how the character treats the virus.
        ///     The higher the rating, the more favorable the character is to the virus and supports it,
        ///     The lower the rating, the more the character hates the virus
        /// </summary>
        public int VirusRating { get; set; }
        
        /// <summary>
        ///     Рейтинг того, как персонаж относится к игроку.
        ///     Чем выше рейтинг, тем персонаж более благосклонен к игроку, и поддерживает его,
        ///     Чем ниже рейтинг, тем более персонаж ненавидит игрока, и тем больше он сопротивляется действиям игрока
        ///     ---
        ///     The rating of how the character treats the player.
        ///     The higher the rating, the more the character favors and supports the player,
        ///     The lower the rating, the more the character hates the player, and the more he resists the player's actions
        /// </summary>
        public int PlayerRating { get; set; }



        public void AddVirusRating(int value)
        {
            if(value == 0)
                return;

            var res = VirusRating + value;
            
            if (res < -10)
                res = -10;
            
            if (res > 10)
                res = 10;

            VirusRating = res;
        }
        
        public void AddPlayerRating(int value)
        {
            if(value == 0)
                return;

            var res = PlayerRating + value;
            
            if (res < -10)
                res = -10;
            
            if (res > 10)
                res = 10;

            PlayerRating = res;
        }
        
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
