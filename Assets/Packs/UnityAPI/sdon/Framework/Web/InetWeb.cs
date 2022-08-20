using System;
using System.IO;
using System.Net;
using System.Text;

namespace UnityEngine
{

    public class InetWeb
    {

        private void Do(string url, Action<HttpWebResponse> callback, Action<Exception> exceptionCallback = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    if ((int)response.StatusCode < 299 && (int)response.StatusCode >= 200)
                        callback?.Invoke(response);
                    else
                        callback?.Invoke(null);
                }
            }
            catch (Exception ex)
            {
                if (exceptionCallback != null)
                    exceptionCallback.Invoke(ex);
                else
                    throw ex;
            }
        }

        public bool Ping(string resource)
        {
            bool result = false;
            Do(resource, response =>
            {
                if (response == null)
                {
                    result = false;
                    return;
                }
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    char[] cs = new char[8];
                    result = reader.Read(cs, 0, cs.Length) > 0;
                }
            });
            return result;
        }

        public string Get(string url)
        {
            string xml = null;
            Do(url, response =>
            {
                if (response == null)
                {
                    xml = null;
                    return;
                }
                var encoding = ASCIIEncoding.UTF8;
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    xml = reader.ReadToEnd();
                }
            });
            return xml;
        }

    }

}
