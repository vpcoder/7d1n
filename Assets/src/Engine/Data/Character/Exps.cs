using System;

namespace Engine.Data
{

    [Serializable]
    public class ExpDataStoryObject : IStoryObject
    {
        public long ID { get { return IDValue; } set { } }
        public long IDValue;

        public ExpField MainExperience;
        public ExpField LootExperience;
        public ExpField ScrapExperience;
        public ExpField CraftExperience;
        public ExpField FightExperience;
    }

	public class Exps : ICharacterStoredObjectSerializable<ExpDataStoryObject>
    {
        public ExpField MainExperience   { get; set; } = new ExpField(0,  70, 0, 0); // Общий опыт выживания

		public ExpField LootExperience   { get; set; } = new ExpField(0, 100, 0, 0); // Опыт поиска
        public ExpField ScrapExperience  { get; set; } = new ExpField(0, 100, 0, 0); // Опыт разборщика
        public ExpField CraftExperience  { get; set; } = new ExpField(0, 100, 0, 0); // Опыт сборщика
        public ExpField FightExperience  { get; set; } = new ExpField(0, 100, 0, 0); // Опыт битвы

        #region Serialization

        public ExpDataStoryObject CreateData()
        {
            var data = new ExpDataStoryObject
            {
                IDValue = Game.Instance.Runtime.PlayerID,
                MainExperience = MainExperience,
                LootExperience = LootExperience,
                ScrapExperience = ScrapExperience,
                CraftExperience = CraftExperience,
                FightExperience = FightExperience
            };
            return data;
        }

        public void LoadFromData(ExpDataStoryObject data)
        {
            this.MainExperience  = data.MainExperience;
            this.LootExperience  = data.LootExperience;
            this.ScrapExperience = data.ScrapExperience;
            this.CraftExperience = data.CraftExperience;
            this.FightExperience = data.FightExperience;
        }

        #endregion

    }
	
}
