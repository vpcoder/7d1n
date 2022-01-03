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

	// Autogen Group DAO
	/// <summary>
    /// DAO таблицы 'group', для работы с сущностью Group
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class GroupDao : DbAccess<Group>
	{
		
		#region Ctors

		public GroupDao() : base("7d1n.group")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.group "
					+ "("
						+ "id INT AUTO_INCREMENT,"
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
			var sql = $"DELETE FROM 7d1n.group WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность Group, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private Group Read(DbDataReader reader)
		{
			return new Group()
			{
				Id = reader.GetInt32(0),
				Name = reader.GetString(1),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей Group, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<Group> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.group WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<Group>();

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

        public override Group Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<Group> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.group";
			var list = new List<Group>();

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

        public override int[] Insert(IEnumerable<Group> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.group")
					   .Append(" (7d1n.group.Name)")
					   .Append(" VALUES (")
					   .Append("'"+item.Name+"'")
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(Group item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<Group> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.group")
					   .Append(" SET ")
					   .Append("7d1n.group.name=").Append("'"+item.Name+"'")
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(Group item)
        {
            Update(new[] { item });
        }

	}

	// Autogen Group Repository
	public class GroupRepository : RepositoryBase<Group>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public GroupRepository() : base(new GroupDao())
		{
			logger.Debug($"init Group repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityGroupAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.Group> ToTransport(this IEnumerable<Group> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.Group>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

