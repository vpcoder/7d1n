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

	// Autogen UserEquip DAO
	/// <summary>
    /// DAO таблицы 'user_equip', для работы с сущностью UserEquip
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class UserEquipDao : DbAccess<UserEquip>
	{
		
		#region Ctors

		public UserEquipDao() : base("7d1n.user_equip")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.user_equip "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "user_id INT,"
						+ "item INT,"
						+ "type INT,"
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
			var sql = $"DELETE FROM 7d1n.user_equip WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность UserEquip, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private UserEquip Read(DbDataReader reader)
		{
			return new UserEquip()
			{
				Id = reader.GetInt32(0),
				UserId = reader.GetInt32(1),
				Item = reader.GetInt32(2),
				Type = (UserEquipType)reader.GetInt32(3),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей UserEquip, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<UserEquip> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.user_equip WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<UserEquip>();

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

        public override UserEquip Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<UserEquip> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.user_equip";
			var list = new List<UserEquip>();

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

        public override int[] Insert(IEnumerable<UserEquip> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.user_equip")
					   .Append(" (7d1n.user_equip.UserId,7d1n.user_equip.Item,7d1n.user_equip.Type)")
					   .Append(" VALUES (")
					   .Append(item.UserId).Append(",")
					   .Append(item.Item).Append(",")
					   .Append(item.Type)
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(UserEquip item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<UserEquip> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.user_equip")
					   .Append(" SET ")
					   .Append("7d1n.user_equip.user_id=").Append(item.UserId).Append(",")
					   .Append("7d1n.user_equip.item=").Append(item.Item).Append(",")
					   .Append("7d1n.user_equip.type=").Append(item.Type)
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(UserEquip item)
        {
            Update(new[] { item });
        }

	}

	// Autogen UserEquip Repository
	public class UserEquipRepository : RepositoryBase<UserEquip>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public UserEquipRepository() : base(new UserEquipDao())
		{
			logger.Debug($"init UserEquip repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityUserEquipAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.UserEquip> ToTransport(this IEnumerable<UserEquip> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.UserEquip>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

