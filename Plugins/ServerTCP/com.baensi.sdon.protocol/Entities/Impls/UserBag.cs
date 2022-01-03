using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'user_bag'
    /// </summary>
    public class UserBag : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int UserId { get; set; }
		public UserBagType Type { get; set; }

		#endregion

		#region Ctor

		public UserBag()
		{ }

		#endregion

    }
}
