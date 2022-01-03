using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'group_loot'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class GroupLoot : DbEntity
    {
		
		#region Fields

		public int GroupId { get; set; }
		public int Item { get; set; }
		public int Count { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.GroupLoot Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.GroupLoot()
				{
					Id = this.Id,
					GroupId = this.GroupId,
					Item = this.Item,
					Count = this.Count
				};
			}
		}

		#region Ctor

		public GroupLoot()
		{ }

		#endregion

    }
}
