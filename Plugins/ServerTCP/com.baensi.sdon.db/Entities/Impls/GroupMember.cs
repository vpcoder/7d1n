using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'group_member'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class GroupMember : DbEntity
    {
		
		#region Fields

		public int GroupId { get; set; }
		public int UserId { get; set; }
		public int Role { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.GroupMember Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.GroupMember()
				{
					Id = this.Id,
					GroupId = this.GroupId,
					UserId = this.UserId,
					Role = this.Role
				};
			}
		}

		#region Ctor

		public GroupMember()
		{ }

		#endregion

    }
}
