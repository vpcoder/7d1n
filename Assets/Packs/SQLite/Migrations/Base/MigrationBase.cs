using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.DB.Migrations
{

    /// <summary>
    /// Базовый класс миграции
    /// </summary>
	public abstract class MigrationBase : IComparer<MigrationBase>
	{
        /// <summary>
        /// Версия миграции
        /// </summary>
		public abstract Version Version { get; }

        /// <summary>
        /// Флаг ошибки, при выполнении миграции
        /// </summary>
        public bool IsError { get; set; }

		public void Migration(Version dbVersion, SQLiteConnection connection)
		{
            // Сбрасываем флаг ошибки
            IsError = false;

            if (dbVersion > Version)
				return; // Делаем миграции только если БД устарела

            try
            {
                // Открываем транзакцию
                connection.BeginTransaction();

                // Выполняем миграцию
                DoMigration(connection);

                // Коммитим транзакцию
                connection.Commit();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
                try
                {
                    // Что то пошло не так, откатываем транзакцию
                    connection.Rollback();
                }
                catch (Exception ex2)
                {
                    Debug.LogError(ex2);
                }
                
                // Устанавливаем флаг ошибки
                IsError = true;
            }
		}

        /// <summary>
        /// Тело миграции (выполняется в транзакции)
        /// </summary>
        /// <param name="connection">Соединение БД</param>
		public abstract void DoMigration(SQLiteConnection connection);

        /// <summary>
        /// Сравнение двух миграций по их версии
        /// </summary>
        /// <param name="first">Первая сравниваемая миграция</param>
        /// <param name="second">Вторая сравниваемая миграция</param>
        /// <returns>Результат сравнения</returns>
		public int Compare(MigrationBase first, MigrationBase second)
		{
			return first.Version.CompareTo(second.Version);
		}
	}

}
