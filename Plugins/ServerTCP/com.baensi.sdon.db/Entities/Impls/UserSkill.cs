using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'user_skill'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class UserSkill : DbEntity
    {
		
		#region Fields

		public int UserId { get; set; }
		public int Skill { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.UserSkill Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.UserSkill()
				{
					Id = this.Id,
					UserId = this.UserId,
					Skill = this.Skill
				};
			}
		}

		#region Ctor

		public UserSkill()
		{ }

		#endregion

    }
}
