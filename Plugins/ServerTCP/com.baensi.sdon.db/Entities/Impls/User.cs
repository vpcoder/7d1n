using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'user'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class User : DbEntity
    {
		
		#region Fields

		public string Nick { get; set; }
		public string Mail { get; set; }
		public string Pass { get; set; }
		public long LastUpdate { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.User Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.User()
				{
					Id = this.Id,
					Nick = this.Nick,
					Mail = this.Mail,
					Pass = this.Pass,
					LastUpdate = this.LastUpdate
				};
			}
		}

		#region Ctor

		public User()
		{ }

		#endregion

    }
}
