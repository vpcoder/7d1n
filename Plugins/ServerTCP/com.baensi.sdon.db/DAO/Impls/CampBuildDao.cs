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

	// Autogen CampBuild DAO
	/// <summary>
    /// DAO таблицы 'camp_build', для работы с сущностью CampBuild
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class CampBuildDao : DbAccess<CampBuild>
	{
		
		#region Ctors

		public CampBuildDao() : base("7d1n.camp_build")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.camp_build "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "camp_id INT,"
						+ "build INT,"
						+ "level INT,"
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
			var sql = $"DELETE FROM 7d1n.camp_build WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность CampBuild, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private CampBuild Read(DbDataReader reader)
		{
			return new CampBuild()
			{
				Id = reader.GetInt32(0),
				CampId = reader.GetInt32(1),
				Build = reader.GetInt32(2),
				Level = reader.GetInt32(3),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей CampBuild, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<CampBuild> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.camp_build WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<CampBuild>();

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

        public override CampBuild Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<CampBuild> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.camp_build";
			var list = new List<CampBuild>();

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

        public override int[] Insert(IEnumerable<CampBuild> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.camp_build")
					   .Append(" (7d1n.camp_build.CampId,7d1n.camp_build.Build,7d1n.camp_build.Level)")
					   .Append(" VALUES (")
					   .Append(item.CampId).Append(",")
					   .Append(item.Build).Append(",")
					   .Append(item.Level)
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(CampBuild item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<CampBuild> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.camp_build")
					   .Append(" SET ")
					   .Append("7d1n.camp_build.camp_id=").Append(item.CampId).Append(",")
					   .Append("7d1n.camp_build.build=").Append(item.Build).Append(",")
					   .Append("7d1n.camp_build.level=").Append(item.Level)
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(CampBuild item)
        {
            Update(new[] { item });
        }

	}

	// Autogen CampBuild Repository
	public class CampBuildRepository : RepositoryBase<CampBuild>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public CampBuildRepository() : base(new CampBuildDao())
		{
			logger.Debug($"init CampBuild repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityCampBuildAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.CampBuild> ToTransport(this IEnumerable<CampBuild> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.CampBuild>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

