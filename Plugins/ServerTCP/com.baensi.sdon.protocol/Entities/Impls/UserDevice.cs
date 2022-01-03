using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'user_device'
    /// </summary>
    public class UserDevice : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int UserId { get; set; }
		public string Guid { get; set; }

		#endregion

		#region Ctor

		public UserDevice()
		{ }

		#endregion

    }
}
