using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'camp_build'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class CampBuild : DbEntity
    {
		
		#region Fields

		public int CampId { get; set; }
		public int Build { get; set; }
		public int Level { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.CampBuild Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.CampBuild()
				{
					Id = this.Id,
					CampId = this.CampId,
					Build = this.Build,
					Level = this.Level
				};
			}
		}

		#region Ctor

		public CampBuild()
		{ }

		#endregion

    }
}
