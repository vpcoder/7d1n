using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'group_build'
    /// </summary>
    public class GroupBuild : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int GroupId { get; set; }
		public int Build { get; set; }
		public int Level { get; set; }

		#endregion

		#region Ctor

		public GroupBuild()
		{ }

		#endregion

    }
}
