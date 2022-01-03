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

	// Autogen UserDevice DAO
	/// <summary>
    /// DAO таблицы 'user_device', для работы с сущностью UserDevice
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class UserDeviceDao : DbAccess<UserDevice>
	{
		
		#region Ctors

		public UserDeviceDao() : base("7d1n.user_device")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.user_device "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "user_id INT,"
						+ "guid TEXT,"
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
			var sql = $"DELETE FROM 7d1n.user_device WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность UserDevice, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private UserDevice Read(DbDataReader reader)
		{
			return new UserDevice()
			{
				Id = reader.GetInt32(0),
				UserId = reader.GetInt32(1),
				Guid = reader.GetString(2),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей UserDevice, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<UserDevice> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.user_device WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<UserDevice>();

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

        public override UserDevice Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<UserDevice> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.user_device";
			var list = new List<UserDevice>();

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

        public override int[] Insert(IEnumerable<UserDevice> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.user_device")
					   .Append(" (7d1n.user_device.UserId,7d1n.user_device.Guid)")
					   .Append(" VALUES (")
					   .Append(item.UserId).Append(",")
					   .Append("'"+item.Guid+"'")
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(UserDevice item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<UserDevice> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.user_device")
					   .Append(" SET ")
					   .Append("7d1n.user_device.user_id=").Append(item.UserId).Append(",")
					   .Append("7d1n.user_device.guid=").Append("'"+item.Guid+"'")
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(UserDevice item)
        {
            Update(new[] { item });
        }

	}

	// Autogen UserDevice Repository
	public class UserDeviceRepository : RepositoryBase<UserDevice>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public UserDeviceRepository() : base(new UserDeviceDao())
		{
			logger.Debug($"init UserDevice repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityUserDeviceAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.UserDevice> ToTransport(this IEnumerable<UserDevice> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.UserDevice>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

