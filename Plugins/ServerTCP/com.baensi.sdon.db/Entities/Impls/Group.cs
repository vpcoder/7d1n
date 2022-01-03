using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'group'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class Group : DbEntity
    {
		
		#region Fields

		public string Name { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.Group Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.Group()
				{
					Id = this.Id,
					Name = this.Name
				};
			}
		}

		#region Ctor

		public Group()
		{ }

		#endregion

    }
}
