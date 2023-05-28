using System;

namespace Engine.Data
{

    [Serializable]
    public class FactorInfo
    {
        public long ID;

    }

    [Serializable]
    public class FactorRepositoryObject : IRepositoryObject
    {
        public long ID { get { return IDValue; } set { } }
        public long IDValue;

        
    }

    public class Factors : ICharacterStoredObjectSerializable<FactorRepositoryObject>
    {

        #region Serialization

        public FactorRepositoryObject CreateData()
        {
            var data = new FactorRepositoryObject
            {
                IDValue = Game.Instance.Runtime.PlayerID,
            };
            return data;
        }

        public void LoadFromData(FactorRepositoryObject data)
        {

        }

        #endregion



    }

}
