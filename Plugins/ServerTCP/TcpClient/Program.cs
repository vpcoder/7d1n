using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using com.baensi.sdon.clientapi;
using com.baensi.sdon.protocol;
using com.baensi.sdon.protocol.entities;
using com.baensi.sdon.protocol.services;
using com.baensi.sdon.protocol.transport;
using ServiceWire.TcpIp;

namespace TcpClient
{

    public class Program
    {

        public static void Main(string[] args)
        {
            try
            {
                //netsh http add urlacl url=http://+:9090/ user=kyctuk
                WebApiFactory.Instance.InitConnect("http://127.0.0.1:9090");
				var test = WebApiFactory.Instance.Authorization.TryAuthorization(new AuthorizationRequest()
				{
					Password = "test",
                    EMail = "t@mail.ru",
                    GUID = "123"
				});
                WebApiFactory.Instance.Authorization.TryAuthorizationPromise(new AuthorizationRequest()
                {
                    Password = "test",
                    EMail = "t@mail.ru",
                    GUID = "123"
                }, (result) =>
				{
					Console.WriteLine(result.ToString());
				}, (ex) =>
				{
					Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
				});
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            }
			Console.ReadKey();
        }

    }

}
