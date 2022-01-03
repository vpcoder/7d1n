using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'camp'
    /// </summary>
    public class Camp : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int UserId { get; set; }
		public float X { get; set; }
		public float Y { get; set; }
		public string Name { get; set; }

		#endregion

		#region Ctor

		public Camp()
		{ }

		#endregion

    }
}
