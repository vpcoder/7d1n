using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'user'
    /// </summary>
    public class User : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public string Nick { get; set; }
		public string Mail { get; set; }
		public string Pass { get; set; }
		public long LastUpdate { get; set; }

		#endregion

		#region Ctor

		public User()
		{ }

		#endregion

    }
}
