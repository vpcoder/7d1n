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

	// Autogen User DAO
	/// <summary>
    /// DAO таблицы 'user', для работы с сущностью User
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class UserDao : DbAccess<User>
	{
		
		#region Ctors

		public UserDao() : base("7d1n.user")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.user "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "nick TEXT,"
						+ "mail TEXT,"
						+ "pass TEXT,"
						+ "last_update BIGINT,"
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
			var sql = $"DELETE FROM 7d1n.user WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность User, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private User Read(DbDataReader reader)
		{
			return new User()
			{
				Id = reader.GetInt32(0),
				Nick = reader.GetString(1),
				Mail = reader.GetString(2),
				Pass = reader.GetString(3),
				LastUpdate = reader.GetInt64(4),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей User, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<User> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.user WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<User>();

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

        public override User Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<User> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.user";
			var list = new List<User>();

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

        public override int[] Insert(IEnumerable<User> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.user")
					   .Append(" (7d1n.user.Nick,7d1n.user.Mail,7d1n.user.Pass,7d1n.user.LastUpdate)")
					   .Append(" VALUES (")
					   .Append("'"+item.Nick+"'").Append(",")
					   .Append("'"+item.Mail+"'").Append(",")
					   .Append("'"+item.Pass+"'").Append(",")
					   .Append(item.LastUpdate)
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(User item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<User> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.user")
					   .Append(" SET ")
					   .Append("7d1n.user.nick=").Append("'"+item.Nick+"'").Append(",")
					   .Append("7d1n.user.mail=").Append("'"+item.Mail+"'").Append(",")
					   .Append("7d1n.user.pass=").Append("'"+item.Pass+"'").Append(",")
					   .Append("7d1n.user.last_update=").Append(item.LastUpdate)
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(User item)
        {
            Update(new[] { item });
        }

	}

	// Autogen User Repository
	public class UserRepository : RepositoryBase<User>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public UserRepository() : base(new UserDao())
		{
			logger.Debug($"init User repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityUserAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.User> ToTransport(this IEnumerable<User> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.User>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

