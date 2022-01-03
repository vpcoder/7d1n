using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Engine.DB.Migrations
{

	/// <summary>
    /// Фабрика миграций
    /// </summary>
	public class MigrationFactory
	{

        #region Singleton

        private static readonly Lazy<MigrationFactory> instance = new Lazy<MigrationFactory>(() => new MigrationFactory());
        public static MigrationFactory Instance { get { return instance.Value; } }
        private MigrationFactory() { }

        #endregion

        /// <summary>
        /// Список всех миграций до самой актуальной версии БД
        /// </summary>
        public IList<MigrationBase> Migrations
		{
			get
			{
				var list = new List<MigrationBase>();

				try
				{
					foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
					{
						if (typeof(MigrationBase) == type)
							continue;

						if (typeof(MigrationBase).IsAssignableFrom(type))
						{
							var tmp = (MigrationBase)Activator.CreateInstance(type);
							list.Add(tmp);
						}
					}
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}

				list.Sort();

				return list;
			}
		}

        /// <summary>
        /// Выполняет миграции локальной БД
        /// </summary>
        /// <param name="db">Ссылка на открытую БД</param>
		public void DoMigration(Db db)
		{
			Version dbVersion = db.CurrentDbVersion;
			db.Do(connection =>
			{
				foreach (var migration in Migrations)
				{
					try
					{
						migration.Migration(dbVersion, connection);

                        if (migration.IsError)
                            break;
					}
					catch (Exception ex)
					{
						Debug.LogWarning("migration '" + migration.GetType().FullName + "' exception");
						Debug.LogException(ex);
					}
				}
            });
		}

	}

}
