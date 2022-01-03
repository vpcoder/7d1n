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

	// Autogen BuildEnemy DAO
	/// <summary>
    /// DAO таблицы 'build_enemy', для работы с сущностью BuildEnemy
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class BuildEnemyDao : DbAccess<BuildEnemy>
	{
		
		#region Ctors

		public BuildEnemyDao() : base("7d1n.build_enemy")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.build_enemy "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "build_id INT,"
						+ "enemy INT,"
						+ "health INT,"
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
			var sql = $"DELETE FROM 7d1n.build_enemy WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность BuildEnemy, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private BuildEnemy Read(DbDataReader reader)
		{
			return new BuildEnemy()
			{
				Id = reader.GetInt32(0),
				BuildId = reader.GetInt32(1),
				Enemy = reader.GetInt32(2),
				Health = reader.GetInt32(3),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей BuildEnemy, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<BuildEnemy> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.build_enemy WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<BuildEnemy>();

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

        public override BuildEnemy Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<BuildEnemy> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.build_enemy";
			var list = new List<BuildEnemy>();

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

        public override int[] Insert(IEnumerable<BuildEnemy> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.build_enemy")
					   .Append(" (7d1n.build_enemy.BuildId,7d1n.build_enemy.Enemy,7d1n.build_enemy.Health)")
					   .Append(" VALUES (")
					   .Append(item.BuildId).Append(",")
					   .Append(item.Enemy).Append(",")
					   .Append(item.Health)
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(BuildEnemy item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<BuildEnemy> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.build_enemy")
					   .Append(" SET ")
					   .Append("7d1n.build_enemy.build_id=").Append(item.BuildId).Append(",")
					   .Append("7d1n.build_enemy.enemy=").Append(item.Enemy).Append(",")
					   .Append("7d1n.build_enemy.health=").Append(item.Health)
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(BuildEnemy item)
        {
            Update(new[] { item });
        }

	}

	// Autogen BuildEnemy Repository
	public class BuildEnemyRepository : RepositoryBase<BuildEnemy>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public BuildEnemyRepository() : base(new BuildEnemyDao())
		{
			logger.Debug($"init BuildEnemy repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityBuildEnemyAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.BuildEnemy> ToTransport(this IEnumerable<BuildEnemy> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.BuildEnemy>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

