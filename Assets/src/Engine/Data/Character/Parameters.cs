using System;

namespace Engine.Data
{

    [Serializable]
    public class ParametersRepositoryData : IRepositoryObject
    {
        public long ID { get { return IDValue; } set { } }
        public long IDValue;

        public int Intellect;
        public int Strength;
        public int Agility;
        public int Endurance;
    }

    public class Parameters : ICharacterStoredObjectSerializable<ParametersRepositoryData>
    {
        public int Intellect { get; set; } = 0; // Интеллект
        public int Strength  { get; set; } = 0; // Сила
        public int Agility   { get; set; } = 0; // Ловкость
        public int Endurance { get; set; } = 0; // Выносливость

        #region Serialization

        public ParametersRepositoryData CreateData()
        {
            var data = new ParametersRepositoryData
            {
                IDValue = Game.Instance.Runtime.PlayerID,
                Intellect = Intellect,
                Strength = Strength,
                Agility = Agility,
                Endurance = Endurance
            };
            return data;
        }

        public void LoadFromData(ParametersRepositoryData data)
        {
            this.Intellect = data.Intellect;
            this.Strength  = data.Strength;
            this.Agility   = data.Agility;
            this.Endurance = data.Endurance;
        }

        #endregion

    }

}
