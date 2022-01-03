using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'user_item'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class UserItem : DbEntity
    {
		
		#region Fields

		public int BagId { get; set; }
		public int Item { get; set; }
		public int Count { get; set; }
		public int X { get; set; }
		public int Y { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.UserItem Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.UserItem()
				{
					Id = this.Id,
					BagId = this.BagId,
					Item = this.Item,
					Count = this.Count,
					X = this.X,
					Y = this.Y
				};
			}
		}

		#region Ctor

		public UserItem()
		{ }

		#endregion

    }
}
