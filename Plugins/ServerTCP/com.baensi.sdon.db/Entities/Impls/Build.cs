using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'build'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class Build : DbEntity
    {
		
		#region Fields

		public int BuildId { get; set; }
		public long Timestamp { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.Build Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.Build()
				{
					Id = this.Id,
					BuildId = this.BuildId,
					Timestamp = this.Timestamp
				};
			}
		}

		#region Ctor

		public Build()
		{ }

		#endregion

    }
}
