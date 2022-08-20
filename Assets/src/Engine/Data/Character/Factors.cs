using System;

namespace Engine.Data
{

    [Serializable]
    public class FactorInfo
    {
        public long ID;

    }

    [Serializable]
    public class FactorStoryObject : IStoryObject
    {
        public long ID { get { return IDValue; } set { } }
        public long IDValue;

        
    }

    public class Factors : ICharacterStoredObjectSerializable<FactorStoryObject>
    {

        #region Serialization

        public FactorStoryObject CreateData()
        {
            var data = new FactorStoryObject
            {
                IDValue = Game.Instance.Runtime.PlayerID,
            };
            return data;
        }

        public void LoadFromData(FactorStoryObject data)
        {

        }

        #endregion



    }

}
