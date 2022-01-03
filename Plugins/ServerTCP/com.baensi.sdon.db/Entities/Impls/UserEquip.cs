using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'user_equip'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class UserEquip : DbEntity
    {
		
		#region Fields

		public int UserId { get; set; }
		public int Item { get; set; }
		public UserEquipType Type { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.UserEquip Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.UserEquip()
				{
					Id = this.Id,
					UserId = this.UserId,
					Item = this.Item,
					Type = this.Type
				};
			}
		}

		#region Ctor

		public UserEquip()
		{ }

		#endregion

    }
}
