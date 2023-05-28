using System;

namespace Engine.Data
{

    [Serializable]
    public class ExpDataRepositoryObject : IRepositoryObject
    {
        public long ID { get { return IDValue; } set { } }
        public long IDValue;

        public ExpField MainExperience;
        public ExpField LootExperience;
        public ExpField ScrapExperience;
        public ExpField CraftExperience;
        public ExpField FightExperience;
    }

    public enum ExperienceType : int
    {
        Main,
        Loot,
        Scrap,
        Craft,
        Fight,
    };
    
	public class Exps : ICharacterStoredObjectSerializable<ExpDataRepositoryObject>
    {
        public ExpField MainExperience   { get; set; } = new ExpField(0,  70, 0, 0); // Общий опыт выживания

		public ExpField LootExperience   { get; set; } = new ExpField(0, 100, 0, 0); // Опыт поиска
        public ExpField ScrapExperience  { get; set; } = new ExpField(0, 100, 0, 2); // Опыт разборщика
        public ExpField CraftExperience  { get; set; } = new ExpField(0, 100, 0, 0); // Опыт сборщика
        public ExpField FightExperience  { get; set; } = new ExpField(0, 100, 0, 0); // Опыт битвы

        public ExpField GetByExperienceType(ExperienceType type)
        {
            switch (type)
            {
                case ExperienceType.Craft: return CraftExperience;
                case ExperienceType.Fight: return FightExperience;
                case ExperienceType.Loot:  return LootExperience;
                case ExperienceType.Main:  return MainExperience;
                case ExperienceType.Scrap: return ScrapExperience;
                default:
                    throw new NotSupportedException("exp type '" + type + "' isn't supported!");
            }
        }
        
        #region Serialization

        public ExpDataRepositoryObject CreateData()
        {
            var data = new ExpDataRepositoryObject
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

        public void LoadFromData(ExpDataRepositoryObject data)
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
