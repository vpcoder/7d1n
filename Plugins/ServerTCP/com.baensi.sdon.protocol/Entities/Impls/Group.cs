using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'group'
    /// </summary>
    public class Group : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public string Name { get; set; }

		#endregion

		#region Ctor

		public Group()
		{ }

		#endregion

    }
}
