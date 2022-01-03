using Engine.DB.Migrations;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.DB
{

    public class Db
    {

        #region Singleton

        private static readonly Lazy<Db> instance = new Lazy<Db>(() => new Db());
        public static Db Instance { get { return instance.Value; } }

        #endregion

        #region Ctor

        /// <summary>
        /// Список доступных языков
        /// </summary>
        private static readonly ISet<string> i18nList = new HashSet<string>()
        {
            "ru_ru", // Русский язык
        };

        private Db()
        {
            Debug.Log("DB: " + DbFileName);
            CheckFolders();
            CheckDb();
            MigrationFactory.Instance.DoMigration(this);
        }

        private void CheckFolders()
        {
            if (!System.IO.Directory.Exists(Application.persistentDataPath + "/Database"))
                System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Database");
        }

        public void DoFillDB()
        {
            // Главная проливка структуры БД
            DoSqlScript("init_script");

            // Проливаем языки
            foreach (var langItem in i18nList)
                DoSqlScript("i18n/" + langItem);
        }

        private void DoSqlScript(string scriptName)
        {
            var sqlAsset = Resources.Load("Database/" + scriptName) as TextAsset;
            if (sqlAsset == null)
            {
                Debug.LogError("Не удалось найти проливку '" + scriptName + "'!");
                throw new Exception("script '" + scriptName + "' not founded!");
            }

            var sqlData = sqlAsset.text;
            var datas = sqlData.Split(new string[] { ";\r\n" }, StringSplitOptions.None);
            Exception e = null;

            Debug.Log("reload sql script '" + scriptName + "'...");
            Do(connect =>
            {
                int counter = 0;

                foreach (var text in datas)
                {
                    string sql = text;

                    if (string.IsNullOrEmpty(sql))
                        continue;

                    sql = sql.Trim();

                    if (string.IsNullOrEmpty(sql) || sql.StartsWith("#"))
                        continue;

                    try
                    {
                        counter += connect.Execute(sql);
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("sql: " + sql);
                        e = ex;
                        throw ex;
                    }
                }

                Debug.Log("reload sql counter: " + counter);
            }, ex =>
            {
                Debug.LogException(ex);
                e = ex;
            });

#if UNITY_EDITOR
            if (e != null)
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
#endif
        }

        #endregion

        #region Properties

        public bool IsDbExists
        {
            get
            {
                return System.IO.File.Exists(DbFileName);
            }
        }

        public Version CurrentDbVersion
        {
            get
            {
                Meta meta = null;
                Do(connection =>
                {
                    meta = connection.QueryFirst<Meta>(0);
                });
                return new Version(meta.Version);
            }
        }

        public string DbFileName
        {
            get
            {
                return Application.persistentDataPath + "/Database/user.db";
            }
        }

        private byte[] DbEmptyData
        {
            get
            {
                var asset = Resources.Load("Database/empty") as TextAsset;
                if (asset == null)
                    Debug.LogError("db file 'empty' not founded!");
                return asset.bytes;
            }
        }

        #endregion

        public bool CheckDb()
        {
            var exists = IsDbExists;
            if (!exists)
            {
                CreateEmptyDb();
                DoFillDB();
            }
            return exists;
        }

        public void CreateEmptyDb()
        {
            try
            {
                System.IO.File.WriteAllBytes(DbFileName, DbEmptyData);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
                throw ex;
            }
        }

        private object connectionLocker = new object();

        protected virtual SQLiteConnection MakeConnection()
        {
#if UNITY_EDITOR
            if (!IsDbExists)
            {
                Debug.LogError("db file name '" + DbFileName + "' - not exists!");
            }
#endif
            return new SQLiteConnection(DbFileName);
        }

        public virtual void Do(Action<SQLiteConnection> callback, Action<Exception> exceptionCallback = null, bool throwable = false)
        {
            if (callback == null)
                return;

            try
            {
                using (var db = MakeConnection())
                {

                    lock (connectionLocker)
                    {
                        try
                        {
                            callback.Invoke(db);
                        }
                        catch (Exception ex)
                        {
                            if (throwable)
                                throw ex;

                            try
                            {
                                if (exceptionCallback != null)
                                    exceptionCallback.Invoke(ex);
                            }
                            catch
                            { }
                            Debug.LogException(ex);
                        }
                        finally
                        {
                            try
                            {
                                db.Close();
                            }
                            catch { }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (throwable)
                    throw ex;

                try
                {
                    if (exceptionCallback != null)
                        exceptionCallback.Invoke(ex);
                }
                catch
                { }
                Debug.LogException(ex);
            }
        }

    }

}
