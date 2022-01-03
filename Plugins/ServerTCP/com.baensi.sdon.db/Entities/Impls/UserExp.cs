using System;
using System.Collections.Generic;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.entity
{

	// Autogen Entity
	/// <summary>
    /// Сущность таблицы 'user_exp'
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
    public class UserExp : DbEntity
    {
		
		#region Fields

		public int UserId { get; set; }
		public long AttackExp { get; set; }
		public int AttackLvl { get; set; }
		public long LootExp { get; set; }
		public int LootLvl { get; set; }
		public long WalkExp { get; set; }
		public int WalkLvl { get; set; }
		public long ScrapExp { get; set; }
		public int ScrapLvl { get; set; }
		public long CraftExp { get; set; }
		public int CraftLvl { get; set; }

		#endregion

		public virtual com.baensi.sdon.protocol.entities.UserExp Transport
		{
			get
			{
				return new com.baensi.sdon.protocol.entities.UserExp()
				{
					Id = this.Id,
					UserId = this.UserId,
					AttackExp = this.AttackExp,
					AttackLvl = this.AttackLvl,
					LootExp = this.LootExp,
					LootLvl = this.LootLvl,
					WalkExp = this.WalkExp,
					WalkLvl = this.WalkLvl,
					ScrapExp = this.ScrapExp,
					ScrapLvl = this.ScrapLvl,
					CraftExp = this.CraftExp,
					CraftLvl = this.CraftLvl
				};
			}
		}

		#region Ctor

		public UserExp()
		{ }

		#endregion

    }
}
