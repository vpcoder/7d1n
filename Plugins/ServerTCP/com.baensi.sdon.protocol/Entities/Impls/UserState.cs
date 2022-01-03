using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'user_state'
    /// </summary>
    public class UserState : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
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

		#region Ctor

		public UserState()
		{ }

		#endregion

    }
}
