using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'build'
    /// </summary>
    public class Build : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int BuildId { get; set; }
		public long Timestamp { get; set; }

		#endregion

		#region Ctor

		public Build()
		{ }

		#endregion

    }
}
