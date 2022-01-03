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

	// Autogen UserState DAO
	/// <summary>
    /// DAO таблицы 'user_state', для работы с сущностью UserState
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class UserStateDao : DbAccess<UserState>
	{
		
		#region Ctors

		public UserStateDao() : base("7d1n.user_state")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.user_state "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "user_id INT,"
						+ "health INT,"
						+ "stamina INT,"
						+ "hunger INT,"
						+ "infection INT,"
						+ "strength INT,"
						+ "agility INT,"
						+ "intellect INT,"
						+ "points INT,"
						+ "defance INT,"
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
			var sql = $"DELETE FROM 7d1n.user_state WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность UserState, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private UserState Read(DbDataReader reader)
		{
			return new UserState()
			{
				Id = reader.GetInt32(0),
				UserId = reader.GetInt32(1),
				Health = reader.GetInt32(2),
				Stamina = reader.GetInt32(3),
				Hunger = reader.GetInt32(4),
				Infection = reader.GetInt32(5),
				Strength = reader.GetInt32(6),
				Agility = reader.GetInt32(7),
				Intellect = reader.GetInt32(8),
				Points = reader.GetInt32(9),
				Defance = reader.GetInt32(10),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей UserState, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<UserState> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.user_state WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<UserState>();

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

        public override UserState Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<UserState> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.user_state";
			var list = new List<UserState>();

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

        public override int[] Insert(IEnumerable<UserState> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.user_state")
					   .Append(" (7d1n.user_state.UserId,7d1n.user_state.Health,7d1n.user_state.Stamina,7d1n.user_state.Hunger,7d1n.user_state.Infection,7d1n.user_state.Strength,7d1n.user_state.Agility,7d1n.user_state.Intellect,7d1n.user_state.Points,7d1n.user_state.Defance)")
					   .Append(" VALUES (")
					   .Append(item.UserId).Append(",")
					   .Append(item.Health).Append(",")
					   .Append(item.Stamina).Append(",")
					   .Append(item.Hunger).Append(",")
					   .Append(item.Infection).Append(",")
					   .Append(item.Strength).Append(",")
					   .Append(item.Agility).Append(",")
					   .Append(item.Intellect).Append(",")
					   .Append(item.Points).Append(",")
					   .Append(item.Defance)
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(UserState item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<UserState> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.user_state")
					   .Append(" SET ")
					   .Append("7d1n.user_state.user_id=").Append(item.UserId).Append(",")
					   .Append("7d1n.user_state.health=").Append(item.Health).Append(",")
					   .Append("7d1n.user_state.stamina=").Append(item.Stamina).Append(",")
					   .Append("7d1n.user_state.hunger=").Append(item.Hunger).Append(",")
					   .Append("7d1n.user_state.infection=").Append(item.Infection).Append(",")
					   .Append("7d1n.user_state.strength=").Append(item.Strength).Append(",")
					   .Append("7d1n.user_state.agility=").Append(item.Agility).Append(",")
					   .Append("7d1n.user_state.intellect=").Append(item.Intellect).Append(",")
					   .Append("7d1n.user_state.points=").Append(item.Points).Append(",")
					   .Append("7d1n.user_state.defance=").Append(item.Defance)
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(UserState item)
        {
            Update(new[] { item });
        }

	}

	// Autogen UserState Repository
	public class UserStateRepository : RepositoryBase<UserState>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public UserStateRepository() : base(new UserStateDao())
		{
			logger.Debug($"init UserState repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityUserStateAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.UserState> ToTransport(this IEnumerable<UserState> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.UserState>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

