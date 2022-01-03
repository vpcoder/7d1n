using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'camp_build'
    /// </summary>
    public class CampBuild : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int CampId { get; set; }
		public int Build { get; set; }
		public int Level { get; set; }

		#endregion

		#region Ctor

		public CampBuild()
		{ }

		#endregion

    }
}
