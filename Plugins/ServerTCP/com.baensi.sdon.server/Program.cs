using NLog;
using System;
using com.baensi.sdon.logging;
using com.baensi.sdon.server.rest;
using com.baensi.sdon.server.Properties;

namespace com.baensi.sdon.server
{

    public class Program
    {

        // Пример разрешения URL:
        // netsh http add urlacl url=http://+:9090/ user=kyctuk
        //
        // Пример снятия разрешения URL:
        // netsh http remove urlacl url=http://+:9090/
        // 

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private static Settings Settings { get; } = Settings.Default;

        static void Main(string[] args)
        {
            Log.Init();

            var rest = new ServerRest();
            rest.StartAsync(Settings.HostAddress).Wait();

            if (!rest.Status)
            {
                logger.Debug("server failed!");
                Console.ReadLine();
                return;
            }

            DoStart();
        }

        private static void DoStart()
        {
            logger.Debug("server started!");

            for (; ; )
                Console.ReadLine();
        }

    }

}
