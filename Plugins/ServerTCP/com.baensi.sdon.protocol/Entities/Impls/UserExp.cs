using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'user_exp'
    /// </summary>
    public class UserExp : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int UserId { get; set; }
		public long AttackExp { get; set; }
		public int AttackLvl { get; set; }
		public long LootExp { get; set; }
		public int LootLvl { get; set; }
		public long WalkExp { get; set; }
		public int WalkLvl { get; set; }
		public long ScrapExp { get; set; }
		public int ScrapLvl { get; set; }
		public long CraftExp { get; set; }
		public int CraftLvl { get; set; }

		#endregion

		#region Ctor

		public UserExp()
		{ }

		#endregion

    }
}
