using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'group_build'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class GroupBuild : DbEntity
    {
		
		#region Fields

		public int GroupId { get; set; }
		public int Build { get; set; }
		public int Level { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.GroupBuild Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.GroupBuild()
				{
					Id = this.Id,
					GroupId = this.GroupId,
					Build = this.Build,
					Level = this.Level
				};
			}
		}

		#region Ctor

		public GroupBuild()
		{ }

		#endregion

    }
}
