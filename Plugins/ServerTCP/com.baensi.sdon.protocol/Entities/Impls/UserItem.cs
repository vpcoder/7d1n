using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'user_item'
    /// </summary>
    public class UserItem : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int BagId { get; set; }
		public int Item { get; set; }
		public int Count { get; set; }
		public int X { get; set; }
		public int Y { get; set; }

		#endregion

		#region Ctor

		public UserItem()
		{ }

		#endregion

    }
}
