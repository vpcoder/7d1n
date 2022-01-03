using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'camp_loot'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class CampLoot : DbEntity
    {
		
		#region Fields

		public int CampId { get; set; }
		public int Item { get; set; }
		public int Count { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.CampLoot Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.CampLoot()
				{
					Id = this.Id,
					CampId = this.CampId,
					Item = this.Item,
					Count = this.Count
				};
			}
		}

		#region Ctor

		public CampLoot()
		{ }

		#endregion

    }
}
