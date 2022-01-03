using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'group_member'
    /// </summary>
    public class GroupMember : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int GroupId { get; set; }
		public int UserId { get; set; }
		public int Role { get; set; }

		#endregion

		#region Ctor

		public GroupMember()
		{ }

		#endregion

    }
}
