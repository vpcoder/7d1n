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

	// Autogen UserSkill DAO
	/// <summary>
    /// DAO таблицы 'user_skill', для работы с сущностью UserSkill
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class UserSkillDao : DbAccess<UserSkill>
	{
		
		#region Ctors

		public UserSkillDao() : base("7d1n.user_skill")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.user_skill "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "user_id INT,"
						+ "skill INT,"
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
			var sql = $"DELETE FROM 7d1n.user_skill WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность UserSkill, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private UserSkill Read(DbDataReader reader)
		{
			return new UserSkill()
			{
				Id = reader.GetInt32(0),
				UserId = reader.GetInt32(1),
				Skill = reader.GetInt32(2),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей UserSkill, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<UserSkill> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.user_skill WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<UserSkill>();

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

        public override UserSkill Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<UserSkill> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.user_skill";
			var list = new List<UserSkill>();

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

        public override int[] Insert(IEnumerable<UserSkill> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.user_skill")
					   .Append(" (7d1n.user_skill.UserId,7d1n.user_skill.Skill)")
					   .Append(" VALUES (")
					   .Append(item.UserId).Append(",")
					   .Append(item.Skill)
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(UserSkill item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<UserSkill> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.user_skill")
					   .Append(" SET ")
					   .Append("7d1n.user_skill.user_id=").Append(item.UserId).Append(",")
					   .Append("7d1n.user_skill.skill=").Append(item.Skill)
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(UserSkill item)
        {
            Update(new[] { item });
        }

	}

	// Autogen UserSkill Repository
	public class UserSkillRepository : RepositoryBase<UserSkill>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public UserSkillRepository() : base(new UserSkillDao())
		{
			logger.Debug($"init UserSkill repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityUserSkillAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.UserSkill> ToTransport(this IEnumerable<UserSkill> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.UserSkill>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

