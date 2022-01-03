using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;

namespace com.baensi.sdon.protocol.entities
{

	// Autogen Transport Entity
	/// <summary>
    /// Транспортная сущность 'user_equip'
    /// </summary>
    public class UserEquip : TransportEntity
    {
		
		#region Fields

		public int Id { get; set; }
		public int UserId { get; set; }
		public int Item { get; set; }
		public UserEquipType Type { get; set; }

		#endregion

		#region Ctor

		public UserEquip()
		{ }

		#endregion

    }
}
