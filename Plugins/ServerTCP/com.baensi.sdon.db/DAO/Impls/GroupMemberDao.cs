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

	// Autogen GroupMember DAO
	/// <summary>
    /// DAO таблицы 'group_member', для работы с сущностью GroupMember
	/// Дата: 31.12.2019 15:45:20
    /// </summary>
	public class GroupMemberDao : DbAccess<GroupMember>
	{
		
		#region Ctors

		public GroupMemberDao() : base("7d1n.group_member")
		{ }

		#endregion

		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		/// <summary>
        /// Создаёт пустую таблицу для указанной сущности, если такой ещё не существует
        /// </summary>
		public override void CreateTable()
		{
			var sql = $"CREATE TABLE IF NOT EXISTS 7d1n.group_member "
					+ "("
						+ "id INT AUTO_INCREMENT,"
						+ "group_id INT,"
						+ "user_id INT,"
						+ "role INT,"
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
			var sql = $"DELETE FROM 7d1n.group_member WHERE id IN ({string.Join(",",ids)});";
            MySqlConnector.Instance.OpenConnection((connection) =>
			{
				connection.Execute(sql);
			});
        }

		/// <summary>
        /// Читает запись из БД, и формирует сущность GroupMember, на основании этой записи
        /// </summary>
        /// <param name="reader">Поток чтения из БД</param>
		private GroupMember Read(DbDataReader reader)
		{
			return new GroupMember()
			{
				Id = reader.GetInt32(0),
				GroupId = reader.GetInt32(1),
				UserId = reader.GetInt32(2),
				Role = reader.GetInt32(3),
			 };
		}

		/// <summary>
        /// Запрашивает из базы экземпляры конкретных сущностей GroupMember, по их идентификаторам
        /// </summary>
        /// <param name="ids">Идентификаторы запрашиваемых сущностей</param>
		public List<GroupMember> Get(params int[] ids)
        {
			var sql = $"SELECT * FROM 7d1n.group_member WHERE id IN ({string.Join(",",ids)})";
			var entities = new List<GroupMember>();

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

        public override GroupMember Get(int id)
        {
			return Get(new[] { id })[0];
        }

        public override IEnumerable<GroupMember> GetAll()
        {
            var sql = $"SELECT * FROM 7d1n.group_member";
			var list = new List<GroupMember>();

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

        public override int[] Insert(IEnumerable<GroupMember> items)
        {
			var list = items.ToList();
			var sql = new StringBuilder(4096);
			var ids = new int[list.Count];

			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				var i = 0;
				foreach(var item in list)
				{
					sql.Append("INSERT INTO 7d1n.group_member")
					   .Append(" (7d1n.group_member.GroupId,7d1n.group_member.UserId,7d1n.group_member.Role)")
					   .Append(" VALUES (")
					   .Append(item.GroupId).Append(",")
					   .Append(item.UserId).Append(",")
					   .Append(item.Role)
					   .Append(");");
				
					connection.Execute(sql.ToString());

					ids[i++] = _generator.Next;

					sql.Clear();
				}
			});
			return ids;
        }

        public override int Insert(GroupMember item)
        {
            return Insert(new[] { item })[0];
        }

        public override void Update(IEnumerable<GroupMember> items)
        {
			var sql = new StringBuilder(4096);
			MySqlConnector.Instance.OpenConnection((connection) =>
			{
				foreach(var item in items)
				{
					sql.Append("UPDATE 7d1n.group_member")
					   .Append(" SET ")
					   .Append("7d1n.group_member.group_id=").Append(item.GroupId).Append(",")
					   .Append("7d1n.group_member.user_id=").Append(item.UserId).Append(",")
					   .Append("7d1n.group_member.role=").Append(item.Role)
					   .Append(" WHERE id=").Append(item.Id).Append(")");
					connection.Execute(sql.ToString());
					sql.Clear();
				}
			});
        }

        public override void Update(GroupMember item)
        {
            Update(new[] { item });
        }

	}

	// Autogen GroupMember Repository
	public class GroupMemberRepository : RepositoryBase<GroupMember>
	{
		
		private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

		public GroupMemberRepository() : base(new GroupMemberDao())
		{
			logger.Debug($"init GroupMember repository with {_cache.Count} items");
		}

	}

}

public static class DbEntityGroupMemberAdditions
{

    public static IEnumerable<com.baensi.sdon.protocol.entities.GroupMember> ToTransport(this IEnumerable<GroupMember> entities)
	{
		var list = new List<com.baensi.sdon.protocol.entities.GroupMember>();

		foreach(var entity in entities)
			list.Add(entity.Transport);

		return list;
	}

}

