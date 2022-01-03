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

	// Autogen CampLoot DAO
	/// <summary>
    /// DAO таблицы 'camp_loot', для работы с сущностью CampLoot
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class CampLootDao : DbAccess<CampLoot>
	{
		
		#region Ctors

		public CampLootDao() : base("7d1n.camp_loot")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.camp_loot "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "camp_id INT,"
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
			var sql = $"DELETE FROM 7d1n.camp_loot WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность CampLoot, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private CampLoot Read(DbDataReader reader)
		{
			return new CampLoot()
			{
				Id = reader.GetInt32(0),
				CampId = reader.GetInt32(1),
				Item = reader.GetInt32(2),
				Count = reader.GetInt32(3),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей CampLoot, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<CampLoot> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.camp_loot WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<CampLoot>();

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

        public override CampLoot Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<CampLoot> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.camp_loot";
			var list = new List<CampLoot>();

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

        public override int[] Insert(IEnumerable<CampLoot> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.camp_loot")
					   .Append(" (7d1n.camp_loot.CampId,7d1n.camp_loot.Item,7d1n.camp_loot.Count)")
					   .Append(" VALUES (")
					   .Append(item.CampId).Append(",")
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

        public override int Insert(CampLoot item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<CampLoot> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.camp_loot")
					   .Append(" SET ")
					   .Append("7d1n.camp_loot.camp_id=").Append(item.CampId).Append(",")
					   .Append("7d1n.camp_loot.item=").Append(item.Item).Append(",")
					   .Append("7d1n.camp_loot.count=").Append(item.Count)
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(CampLoot item)
        {
            Update(new[] { item });
        }

	}

	// Autogen CampLoot Repository
	public class CampLootRepository : RepositoryBase<CampLoot>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public CampLootRepository() : base(new CampLootDao())
		{
			logger.Debug($"init CampLoot repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityCampLootAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.CampLoot> ToTransport(this IEnumerable<CampLoot> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.CampLoot>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

