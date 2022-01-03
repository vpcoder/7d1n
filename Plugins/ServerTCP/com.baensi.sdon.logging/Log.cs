using NLog;
using NLog.Config;
using NLog.Targets;

namespace com.baensi.sdon.logging
{

    public static class Log
    {

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private const string LAYOUT = "${longdate} ${uppercase:${level}} - ${logger} - ${message}";

        public static void Init()
        {
            var config = new LoggingConfiguration();
            
            var traceLogFile = new FileTarget("logfile")
            {
                FileName = "logs/trace_${shortdate}.log",
                Layout = LAYOUT
            };

            var traceLogConsole = new ConsoleTarget("logconsole")
            {
                Layout = LAYOUT
            };

            config.AddRule(LogLevel.Debug, LogLevel.Fatal, traceLogConsole);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, traceLogFile);
     
            LogManager.Configuration = config;

            logger.Debug("init logging...");
        }

    }

}
