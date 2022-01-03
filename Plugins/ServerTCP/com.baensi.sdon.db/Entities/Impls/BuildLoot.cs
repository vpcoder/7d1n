using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'build_loot'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class BuildLoot : DbEntity
    {
		
		#region Fields

		public int BuildId { get; set; }
		public int Item { get; set; }
		public int Count { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.BuildLoot Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.BuildLoot()
				{
					Id = this.Id,
					BuildId = this.BuildId,
					Item = this.Item,
					Count = this.Count
				};
			}
		}

		#region Ctor

		public BuildLoot()
		{ }

		#endregion

    }
}
