using System;
using System.Linq;
using System.Net;
using com.baensi.sdon.logging;
using NLog;
using ServiceWire.TcpIp;
using com.baensi.sdon.db.dao;
using com.baensi.sdon.protocol;

namespace TcpServer
{

    public class Test : ITest
    {
        public string Version => "1.0.0.0";

        public void test()
        {
            var users = DaoFactory.Instance.User.GetAll();
            Console.WriteLine("all users:");
            Console.WriteLine($"{string.Join("\r\n", users.Select(o => o.Nick).ToArray())}");
            Console.WriteLine("test::ok!");
        }
    }

    class Program
    {

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private static TcpHost host;

        static void Main(string[] args)
        {
            Log.Init();

            var users = DaoFactory.Instance.User.GetAll();

            host = new TcpHost(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5050));
            host.AddService<ITest>(new Test());
            host.Open();

            for(;;)
            {
                Console.ReadLine();
            }
        }
    }
}
