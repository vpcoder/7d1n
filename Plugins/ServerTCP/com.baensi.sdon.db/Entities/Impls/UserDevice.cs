using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'user_device'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class UserDevice : DbEntity
    {
		
		#region Fields

		public int UserId { get; set; }
		public string Guid { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.UserDevice Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.UserDevice()
				{
					Id = this.Id,
					UserId = this.UserId,
					Guid = this.Guid
				};
			}
		}

		#region Ctor

		public UserDevice()
		{ }

		#endregion

    }
}
