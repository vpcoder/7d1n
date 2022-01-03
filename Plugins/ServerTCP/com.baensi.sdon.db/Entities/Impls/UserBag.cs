using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'user_bag'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class UserBag : DbEntity
    {
		
		#region Fields

		public int UserId { get; set; }
		public UserBagType Type { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.UserBag Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.UserBag()
				{
					Id = this.Id,
					UserId = this.UserId,
					Type = this.Type
				};
			}
		}

		#region Ctor

		public UserBag()
		{ }

		#endregion

    }
}
