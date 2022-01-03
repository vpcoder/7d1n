using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'camp_loot'
    /// </summary>
    public class CampLoot : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int CampId { get; set; }
		public int Item { get; set; }
		public int Count { get; set; }

		#endregion

		#region Ctor

		public CampLoot()
		{ }

		#endregion

    }
}
