using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'user_skill'
    /// </summary>
    public class UserSkill : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int UserId { get; set; }
		public int Skill { get; set; }

		#endregion

		#region Ctor

		public UserSkill()
		{ }

		#endregion

    }
}
