using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Data.Common;
using com.baensi.sdon.db.connection;
using com.baensi.sdon.db.dao.repository;
using com.baensi.sdon.db.dao.generator;
using com.baensi.sdon.db.entity;
using com.baensi.sdon.logic;
using NLog;

namespace com.baensi.sdon.db.dao
{

	// Autogen UserExp DAO
	/// <summary>
    /// DAO таблицы 'user_exp', для работы с сущностью UserExp
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class UserExpDao : DbAccess<UserExp>
	{
		
		#region Ctors

		public UserExpDao() : base("7d1n.user_exp")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.user_exp "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "user_id INT,"
						+ "attack_exp BIGINT,"
						+ "attack_lvl INT,"
						+ "loot_exp BIGINT,"
						+ "loot_lvl INT,"
						+ "walk_exp BIGINT,"
						+ "walk_lvl INT,"
						+ "scrap_exp BIGINT,"
						+ "scrap_lvl INT,"
						+ "craft_exp BIGINT,"
						+ "craft_lvl INT,"
						+ "PRIMARY KEY (id)"
					+ ") ENGINE=INNODB;";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
		}

		/// <summary>
        /// Удаляет записи с указанными идентификаторами из БД
        /// </summary>
        /// <param name="ids">Идентификаторы, по которым необходимо удалять записи</param>
		public override void Delete(params int[] ids)
        {
			var sql = $"DELETE FROM 7d1n.user_exp WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность UserExp, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private UserExp Read(DbDataReader reader)
		{
			return new UserExp()
			{
				Id = reader.GetInt32(0),
				UserId = reader.GetInt32(1),
				AttackExp = reader.GetInt64(2),
				AttackLvl = reader.GetInt32(3),
				LootExp = reader.GetInt64(4),
				LootLvl = reader.GetInt32(5),
				WalkExp = reader.GetInt64(6),
				WalkLvl = reader.GetInt32(7),
				ScrapExp = reader.GetInt64(8),
				ScrapLvl = reader.GetInt32(9),
				CraftExp = reader.GetInt64(10),
				CraftLvl = reader.GetInt32(11),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей UserExp, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<UserExp> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.user_exp WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<UserExp>();

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				using(var reader = connection.ExecuteReader(sql))
				{
					while(reader.Read())
					{
						entities.Add(Read(reader));
					}
					reader.Close();
				}
			});

			if(entities.Count!=ids.Length)
				throw new Exception("Select by ids exception");

			return entities;
        }

        public override UserExp Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<UserExp> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.user_exp";
			var list = new List<UserExp>();

            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				using(var reader = connection.ExecuteReader(sql))
				{
					for(;;)
					{
						if(!reader.Read())
							break;
						list.Add(Read(reader));
					}
					reader.Close();
				}
			});

			return list;
        }

        public override int[] Insert(IEnumerable<UserExp> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.user_exp")
					   .Append(" (7d1n.user_exp.UserId,7d1n.user_exp.AttackExp,7d1n.user_exp.AttackLvl,7d1n.user_exp.LootExp,7d1n.user_exp.LootLvl,7d1n.user_exp.WalkExp,7d1n.user_exp.WalkLvl,7d1n.user_exp.ScrapExp,7d1n.user_exp.ScrapLvl,7d1n.user_exp.CraftExp,7d1n.user_exp.CraftLvl)")
					   .Append(" VALUES (")
					   .Append(item.UserId).Append(",")
					   .Append(item.AttackExp).Append(",")
					   .Append(item.AttackLvl).Append(",")
					   .Append(item.LootExp).Append(",")
					   .Append(item.LootLvl).Append(",")
					   .Append(item.WalkExp).Append(",")
					   .Append(item.WalkLvl).Append(",")
					   .Append(item.ScrapExp).Append(",")
					   .Append(item.ScrapLvl).Append(",")
					   .Append(item.CraftExp).Append(",")
					   .Append(item.CraftLvl)
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(UserExp item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<UserExp> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.user_exp")
					   .Append(" SET ")
					   .Append("7d1n.user_exp.user_id=").Append(item.UserId).Append(",")
					   .Append("7d1n.user_exp.attack_exp=").Append(item.AttackExp).Append(",")
					   .Append("7d1n.user_exp.attack_lvl=").Append(item.AttackLvl).Append(",")
					   .Append("7d1n.user_exp.loot_exp=").Append(item.LootExp).Append(",")
					   .Append("7d1n.user_exp.loot_lvl=").Append(item.LootLvl).Append(",")
					   .Append("7d1n.user_exp.walk_exp=").Append(item.WalkExp).Append(",")
					   .Append("7d1n.user_exp.walk_lvl=").Append(item.WalkLvl).Append(",")
					   .Append("7d1n.user_exp.scrap_exp=").Append(item.ScrapExp).Append(",")
					   .Append("7d1n.user_exp.scrap_lvl=").Append(item.ScrapLvl).Append(",")
					   .Append("7d1n.user_exp.craft_exp=").Append(item.CraftExp).Append(",")
					   .Append("7d1n.user_exp.craft_lvl=").Append(item.CraftLvl)
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(UserExp item)
        {
            Update(new[] { item });
        }

	}

	// Autogen UserExp Repository
	public class UserExpRepository : RepositoryBase<UserExp>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public UserExpRepository() : base(new UserExpDao())
		{
			logger.Debug($"init UserExp repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityUserExpAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.UserExp> ToTransport(this IEnumerable<UserExp> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.UserExp>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

