using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'group_loot'
    /// </summary>
    public class GroupLoot : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int GroupId { get; set; }
		public int Item { get; set; }
		public int Count { get; set; }

		#endregion

		#region Ctor

		public GroupLoot()
		{ }

		#endregion

    }
}
