using System;
using System.Data;
using System.Data.Common;
using NLog;
using com.baensi.sdon.db.Properties;

namespace com.baensi.sdon.db.connection
{

    public class ConnectionHandler<TConnection, TCommand>
        where TConnection : DbConnection
        where TCommand : DbCommand
    {

        #region Hidden Fields

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private TConnection connection;

        #endregion

        #region Ctors

        public ConnectionHandler(TConnection connection)
        {
            this.connection = connection;
        }

        #endregion

        protected TCommand MakeCmd(string sql)
        {
            logger.Trace($"make cmd...");
            var type = typeof(TCommand);
            var cmd = Activator.CreateInstance(type, sql, connection);
            return (TCommand)cmd;
        }

        #region Handlers

        public int Execute(string sql)
        {
            logger.Debug($"execute '{sql}'");
            return MakeCmd(sql).ExecuteNonQuery();
        }

        public object ExecuteScalar(string sql)
        {
            logger.Debug($"execute scalar '{sql}'");
            return MakeCmd(sql).ExecuteScalar();
        }

        public DbDataReader ExecuteReader(string sql, CommandBehavior behavior = CommandBehavior.Default)
        {
            logger.Debug($"execute reader '{sql}'");
            return MakeCmd(sql).ExecuteReader(behavior);
        }

        public void ReadLineByLine(string sql, Action<DbDataReader> rowReadCallback, Action<DataTable> schemaReadCallback = null)
        {
            using (var reader = MakeCmd(sql).ExecuteReader())
            {
                if (schemaReadCallback != null)
                {
                    var schema = reader.GetSchemaTable();
                    schemaReadCallback(schema);
                }

                for (; ; )
                {
                    if (!reader.Read())
                        break;

                    rowReadCallback(reader);
                }

                reader.Close();
            }
        }

        #endregion

    }

    public abstract class ConnectorBase<TConnection, TCommand>
        where TConnection : DbConnection
        where TCommand    : DbCommand
    {

        #region Hidden Fields

        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        protected Settings _settings;

        #endregion

        #region Ctors

        public ConnectorBase()
        {
            this._settings = Settings.Default;
        }

        #endregion

        #region Properties

        protected abstract string ConnectionString { get; }

        public string DbName
        {
            get
            {
                return _settings.DbName;
            }
        }

        #endregion

        public void OpenConnection(Action<ConnectionHandler<TConnection,TCommand>> callback)
        {
            _logger.Trace("create connection...");
            using (var connection = (TConnection)Activator.CreateInstance(typeof(TConnection), ConnectionString))
            {
                _logger.Trace("open connection...");
                connection.Open();

                var connectionHandler = new ConnectionHandler<TConnection, TCommand>(connection);

                try
                {
                    _logger.Trace($"callback...");
                    callback(connectionHandler);
                }
                catch (Exception ex)
                {
                    try
                    {
                        _logger.Trace($"close connection");
                        connection.Close();
                    }
                    catch { }

                    _logger.Error(ex);

                    throw ex;
                }

                _logger.Trace($"close connection");
                connection.Close();
            }
        }

    }

}
