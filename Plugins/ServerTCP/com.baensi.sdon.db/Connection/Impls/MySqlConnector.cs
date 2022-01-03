using MySql.Data.MySqlClient;

namespace com.baensi.sdon.db.connection
{

    public class MySqlConnector : ConnectorBase<MySqlConnection, MySqlCommand>
    {

        #region Singleton

        private static MySqlConnector _instance;

        public static MySqlConnector Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MySqlConnector();

                return _instance;
            }
        }

        #endregion

        protected override string ConnectionString
        {
            get
            {
                return $"Server={_settings.Host};" +
                       $"Port={_settings.Port};" +
                       $"Database={_settings.DbName};" +
                       $"Uid={_settings.UserName};" +
                       $"Pwd={_settings.UserPassword};" +
                       $"SSLMode={_settings.SslMode};";
            }
        }

    }

}
