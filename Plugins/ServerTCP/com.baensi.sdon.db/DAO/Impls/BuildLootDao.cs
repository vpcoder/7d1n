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

	// Autogen BuildLoot DAO
	/// <summary>
    /// DAO таблицы 'build_loot', для работы с сущностью BuildLoot
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class BuildLootDao : DbAccess<BuildLoot>
	{
		
		#region Ctors

		public BuildLootDao() : base("7d1n.build_loot")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.build_loot "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "build_id INT,"
						+ "item INT,"
						+ "count INT,"
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
			var sql = $"DELETE FROM 7d1n.build_loot WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность BuildLoot, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private BuildLoot Read(DbDataReader reader)
		{
			return new BuildLoot()
			{
				Id = reader.GetInt32(0),
				BuildId = reader.GetInt32(1),
				Item = reader.GetInt32(2),
				Count = reader.GetInt32(3),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей BuildLoot, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<BuildLoot> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.build_loot WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<BuildLoot>();

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

        public override BuildLoot Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<BuildLoot> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.build_loot";
			var list = new List<BuildLoot>();

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

        public override int[] Insert(IEnumerable<BuildLoot> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.build_loot")
					   .Append(" (7d1n.build_loot.BuildId,7d1n.build_loot.Item,7d1n.build_loot.Count)")
					   .Append(" VALUES (")
					   .Append(item.BuildId).Append(",")
					   .Append(item.Item).Append(",")
					   .Append(item.Count)
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(BuildLoot item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<BuildLoot> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.build_loot")
					   .Append(" SET ")
					   .Append("7d1n.build_loot.build_id=").Append(item.BuildId).Append(",")
					   .Append("7d1n.build_loot.item=").Append(item.Item).Append(",")
					   .Append("7d1n.build_loot.count=").Append(item.Count)
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(BuildLoot item)
        {
            Update(new[] { item });
        }

	}

	// Autogen BuildLoot Repository
	public class BuildLootRepository : RepositoryBase<BuildLoot>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public BuildLootRepository() : base(new BuildLootDao())
		{
			logger.Debug($"init BuildLoot repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityBuildLootAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.BuildLoot> ToTransport(this IEnumerable<BuildLoot> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.BuildLoot>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

