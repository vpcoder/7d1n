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

	// Autogen UserItem DAO
	/// <summary>
    /// DAO таблицы 'user_item', для работы с сущностью UserItem
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class UserItemDao : DbAccess<UserItem>
	{
		
		#region Ctors

		public UserItemDao() : base("7d1n.user_item")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.user_item "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "bag_id INT,"
						+ "item INT,"
						+ "count INT,"
						+ "x INT,"
						+ "y INT,"
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
			var sql = $"DELETE FROM 7d1n.user_item WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность UserItem, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private UserItem Read(DbDataReader reader)
		{
			return new UserItem()
			{
				Id = reader.GetInt32(0),
				BagId = reader.GetInt32(1),
				Item = reader.GetInt32(2),
				Count = reader.GetInt32(3),
				X = reader.GetInt32(4),
				Y = reader.GetInt32(5),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей UserItem, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<UserItem> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.user_item WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<UserItem>();

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

        public override UserItem Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<UserItem> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.user_item";
			var list = new List<UserItem>();

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

        public override int[] Insert(IEnumerable<UserItem> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.user_item")
					   .Append(" (7d1n.user_item.BagId,7d1n.user_item.Item,7d1n.user_item.Count,7d1n.user_item.X,7d1n.user_item.Y)")
					   .Append(" VALUES (")
					   .Append(item.BagId).Append(",")
					   .Append(item.Item).Append(",")
					   .Append(item.Count).Append(",")
					   .Append(item.X).Append(",")
					   .Append(item.Y)
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(UserItem item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<UserItem> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.user_item")
					   .Append(" SET ")
					   .Append("7d1n.user_item.bag_id=").Append(item.BagId).Append(",")
					   .Append("7d1n.user_item.item=").Append(item.Item).Append(",")
					   .Append("7d1n.user_item.count=").Append(item.Count).Append(",")
					   .Append("7d1n.user_item.x=").Append(item.X).Append(",")
					   .Append("7d1n.user_item.y=").Append(item.Y)
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(UserItem item)
        {
            Update(new[] { item });
        }

	}

	// Autogen UserItem Repository
	public class UserItemRepository : RepositoryBase<UserItem>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public UserItemRepository() : base(new UserItemDao())
		{
			logger.Debug($"init UserItem repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityUserItemAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.UserItem> ToTransport(this IEnumerable<UserItem> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.UserItem>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

