using System;

namespace Engine.Data
{
	
	[Serializable]
	public class StateRepositoryObject : IRepositoryObject
    {
        public long ID { get { return IDValue; } set { } }
        public long IDValue;

        public int  Health;
        public int  MaxHealth;
		public int  Protection;
		public int  Thirst;
		public int  Hunger;
		public int  Infection;
		public long MaxWeight;
        public int  MaxAP;
    }
    
	public class State : ICharacterStoredObjectSerializable<StateRepositoryObject>
    {

        #region Properties

        public int  Health      { get; set; } = 100;

        public int  MaxHealth   { get; set; } = 100;

		public int  Protection  { get; set; } = 0;
		
		public int  Thirst      { get; set; } = 0;

        public int  Hunger      { get; set; } = 0;
		
		public int  Infection   { get; set; } = 0;
		
		
		public long MaxWeight   { get; set; } = WeightCalculationService.GetMass(45, WeightUnitType.KILOGRAMS); // Максимально переносимый вес 45кг

        public int MaxAP { get; set; } = 8;

#endregion

#region Serialization

        public StateRepositoryObject CreateData()
        {
            var data = new StateRepositoryObject
            {
                IDValue = Game.Instance.Runtime.PlayerID,
                Health = Health,
                MaxHealth = MaxHealth,
                Protection = Protection,
                Thirst = Thirst,
                Hunger = Hunger,
                Infection = Infection,
                MaxWeight = MaxWeight,
                MaxAP = MaxAP
            };
            return data;
        }

        public void LoadFromData(StateRepositoryObject data)
        {
            this.Health     = data.Health;
            this.MaxHealth  = data.MaxHealth;
            this.Protection = data.Protection;
            this.Thirst     = data.Thirst;
            this.Hunger     = data.Hunger;
            this.Infection  = data.Infection;
            this.MaxWeight  = data.MaxWeight;
            this.MaxAP      = data.MaxAP;
        }

#endregion

    }
	
}
