using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'camp'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class Camp : DbEntity
    {
		
		#region Fields

		public int UserId { get; set; }
		public float X { get; set; }
		public float Y { get; set; }
		public string Name { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.Camp Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.Camp()
				{
					Id = this.Id,
					UserId = this.UserId,
					X = this.X,
					Y = this.Y,
					Name = this.Name
				};
			}
		}

		#region Ctor

		public Camp()
		{ }

		#endregion

    }
}
