using UnityEngine;

namespace Engine.DB
{

    /// <summary>
    /// Конфигуратор БД для отладки, в релиз не пойдёт
    /// </summary>
	public class DbConfigurator
    {

        public static string CreateMeta()
        {
            var emptyAssets = Resources.Load("Database/empty") as TextAsset;

            var db = "db file: " + Db.Instance.DbFileName + "\n";
            var empty = "empty: " + (emptyAssets?.name ?? "<null>") + "\n";
            var meta = "meta: " + Db.Instance.CurrentDbVersion + "\n";

            return db + empty + meta;
        }

        public static void DoResetDB()
        {
            System.IO.File.Delete(Db.Instance.DbFileName);
            Db.Instance.CheckDb();
        }

    }

}
