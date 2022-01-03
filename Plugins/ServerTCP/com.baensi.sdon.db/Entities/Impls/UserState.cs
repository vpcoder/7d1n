using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'user_state'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class UserState : DbEntity
    {
		
		#region Fields

		public int UserId { get; set; }
		public int Health { get; set; }
		public int Stamina { get; set; }
		public int Hunger { get; set; }
		public int Infection { get; set; }
		public int Strength { get; set; }
		public int Agility { get; set; }
		public int Intellect { get; set; }
		public int Points { get; set; }
		public int Defance { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.UserState Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.UserState()
				{
					Id = this.Id,
					UserId = this.UserId,
					Health = this.Health,
					Stamina = this.Stamina,
					Hunger = this.Hunger,
					Infection = this.Infection,
					Strength = this.Strength,
					Agility = this.Agility,
					Intellect = this.Intellect,
					Points = this.Points,
					Defance = this.Defance
				};
			}
		}

		#region Ctor

		public UserState()
		{ }

		#endregion

    }
}
