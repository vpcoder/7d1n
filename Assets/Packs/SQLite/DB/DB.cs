using Engine.Data;
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

        MigrationScriptExecuter executor = new MigrationScriptExecuter();
        
        #region Ctor

        /// <summary>
        ///     Список доступных языков, которыми будет набиваться БД при инициализации
        ///     ---
        ///     List of available languages, which will be filled in the database during initialization
        /// </summary>
        private static readonly ISet<string> i18nList = new HashSet<string>()
        {
            "ru_ru", // Русский язык
        };

        private Db()
        {
            Debug.Log("DB: " + DbFileName);
            CheckFolders();
        }

        private void CheckFolders()
        {
            if (!System.IO.Directory.Exists(Application.persistentDataPath + "/Database"))
                System.IO.Directory.CreateDirectory(Application.persistentDataPath + "/Database");
        }

        public void DoFillDB()
        {
            
            // Главная проливка структуры БД
            executor.ExecuteScriptFile("init_script");

            // Обновляем версию БД
            Do(connect =>
            {
                var version = Game.Instance.Buildtime.Version.ToString();
                connect.Execute("INSERT INTO `meta` ( `id`, `version` ) VALUES( 0, '" + version + "' )");
            });

            // Проливаем языки
            foreach (var langItem in i18nList)
                executor.ExecuteScriptFile("i18n/" + langItem);
        }
        
        #endregion

        #region Hidden Fields

        private object connectionLocker = new object();

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
                try
                {
                    Meta meta = null;
                    Do(connection =>
                    {
                        meta = connection.QueryFirst<Meta>(0);
                    });
                    return new Version(meta.Version);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    return new Version("0.0.0.0");
                }
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
            if (!exists || CurrentDbVersion < Game.Instance.Buildtime.Version) // FIXME: убрать перепроливку с нуля, если БД уже существует
            {
                CreateEmptyDb();
                DoFillDB();
            }
            //MigrationFactory.Instance.DoMigration(this);
            return exists;
        }

        public void CreateEmptyDb()
        {
            try
            {
                System.IO.File.Delete(DbFileName);
            }
            catch { }
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


        protected virtual SQLiteConnection MakeConnection()
        {
#if UNITY_EDITOR && DEBUG
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
