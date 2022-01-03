using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'build_enemy'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class BuildEnemy : DbEntity
    {
		
		#region Fields

		public int BuildId { get; set; }
		public int Enemy { get; set; }
		public int Health { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.BuildEnemy Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.BuildEnemy()
				{
					Id = this.Id,
					BuildId = this.BuildId,
					Enemy = this.Enemy,
					Health = this.Health
				};
			}
		}

		#region Ctor

		public BuildEnemy()
		{ }

		#endregion

    }
}
