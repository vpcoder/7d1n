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

	// Autogen Build DAO
	/// <summary>
    /// DAO таблицы 'build', для работы с сущностью Build
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class BuildDao : DbAccess<Build>
	{
		
		#region Ctors

		public BuildDao() : base("7d1n.build")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.build "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "build_id INT,"
						+ "timestamp BIGINT,"
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
			var sql = $"DELETE FROM 7d1n.build WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность Build, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private Build Read(DbDataReader reader)
		{
			return new Build()
			{
				Id = reader.GetInt32(0),
				BuildId = reader.GetInt32(1),
				Timestamp = reader.GetInt64(2),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей Build, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<Build> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.build WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<Build>();

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

        public override Build Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<Build> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.build";
			var list = new List<Build>();

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

        public override int[] Insert(IEnumerable<Build> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.build")
					   .Append(" (7d1n.build.BuildId,7d1n.build.Timestamp)")
					   .Append(" VALUES (")
					   .Append(item.BuildId).Append(",")
					   .Append(item.Timestamp)
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(Build item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<Build> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.build")
					   .Append(" SET ")
					   .Append("7d1n.build.build_id=").Append(item.BuildId).Append(",")
					   .Append("7d1n.build.timestamp=").Append(item.Timestamp)
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(Build item)
        {
            Update(new[] { item });
        }

	}

	// Autogen Build Repository
	public class BuildRepository : RepositoryBase<Build>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public BuildRepository() : base(new BuildDao())
		{
			logger.Debug($"init Build repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityBuildAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.Build> ToTransport(this IEnumerable<Build> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.Build>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

