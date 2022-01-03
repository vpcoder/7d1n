using System;

namespace Engine.Data
{

    [Serializable]
    public class AccountStoryObject : IStoryObject
    {
        public long   ID { get { return IDValue; } set { } }
        public long   IDValue;
        public long   SpriteID;
        public string Name;
    }

    public class Account : ICharacterStoredObjectSerializable<AccountStoryObject>
    {

        public long   SpriteID { get; set; } = 0L;

        public string Name     { get; set; } = "Robert";

        #region Serialization

        public AccountStoryObject CreateData()
        {
            var data = new AccountStoryObject
            {
                IDValue  = Game.Instance.Runtime.PlayerID,
                SpriteID = SpriteID,
                Name     = Name
            };
            return data;
        }

        public void LoadFromData(AccountStoryObject data)
        {
            this.SpriteID = data.SpriteID;
            this.Name     = data.Name;
        }

        #endregion

    }

}
