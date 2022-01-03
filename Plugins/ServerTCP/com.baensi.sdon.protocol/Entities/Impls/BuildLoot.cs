using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'build_loot'
    /// </summary>
    public class BuildLoot : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int BuildId { get; set; }
		public int Item { get; set; }
		public int Count { get; set; }

		#endregion

		#region Ctor

		public BuildLoot()
		{ }

		#endregion

    }
}
