using com.baensi.sdon.db.connection;
using System.Threading;

namespace com.baensi.sdon.db.dao.generator
{

    public class DefaultIdGenerator : IGenerator
    {

        #region Hidden Fields

        private string path;

        #endregion

        #region Shared Fields

        private object locker = new object();

        private int id;

        public int Id { get { return id; } }

        public int Next
        {
            get
            {
                Interlocked.Increment(ref id);
                return id;
            }
        }

        #endregion

        #region Ctors

        public DefaultIdGenerator(string path)
        {
            this.path = path;
            this.Update();
        }

        #endregion

        #region Update

        public void Update()
        {
            var sql = "SELECT MAX(Id) FROM " + path;
            MySqlConnector.Instance.OpenConnection((connection) =>
            {
                var value = connection.ExecuteScalar(sql).ToString();

                if (string.IsNullOrEmpty(value))
                {
                    this.id = 1;
                    return;
                }

                this.id = int.Parse(value);
            });
        }

        #endregion

    }

}
