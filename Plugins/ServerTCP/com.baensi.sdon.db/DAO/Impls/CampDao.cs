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

	// Autogen Camp DAO
	/// <summary>
    /// DAO таблицы 'camp', для работы с сущностью Camp
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class CampDao : DbAccess<Camp>
	{
		
		#region Ctors

		public CampDao() : base("7d1n.camp")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.camp "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "user_id INT,"
						+ "x FLOAT,"
						+ "y FLOAT,"
						+ "name TEXT,"
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
			var sql = $"DELETE FROM 7d1n.camp WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность Camp, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private Camp Read(DbDataReader reader)
		{
			return new Camp()
			{
				Id = reader.GetInt32(0),
				UserId = reader.GetInt32(1),
				X = reader.GetFloat(2),
				Y = reader.GetFloat(3),
				Name = reader.GetString(4),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей Camp, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<Camp> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.camp WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<Camp>();

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

        public override Camp Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<Camp> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.camp";
			var list = new List<Camp>();

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

        public override int[] Insert(IEnumerable<Camp> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.camp")
					   .Append(" (7d1n.camp.UserId,7d1n.camp.X,7d1n.camp.Y,7d1n.camp.Name)")
					   .Append(" VALUES (")
					   .Append(item.UserId).Append(",")
					   .Append(item.X).Append(",")
					   .Append(item.Y).Append(",")
					   .Append("'"+item.Name+"'")
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(Camp item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<Camp> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.camp")
					   .Append(" SET ")
					   .Append("7d1n.camp.user_id=").Append(item.UserId).Append(",")
					   .Append("7d1n.camp.x=").Append(item.X).Append(",")
					   .Append("7d1n.camp.y=").Append(item.Y).Append(",")
					   .Append("7d1n.camp.name=").Append("'"+item.Name+"'")
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(Camp item)
        {
            Update(new[] { item });
        }

	}

	// Autogen Camp Repository
	public class CampRepository : RepositoryBase<Camp>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public CampRepository() : base(new CampDao())
		{
			logger.Debug($"init Camp repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityCampAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.Camp> ToTransport(this IEnumerable<Camp> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.Camp>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

