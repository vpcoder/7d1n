using Newtonsoft.Json;
using NLog;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace com.baensi.sdon.server.rest.formatters
{

    public class JsonRestFormatter : JsonMediaTypeFormatter
    {

        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        #region JSON

        public static object Deserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string Serialize(object obj)
        {

            return JsonConvert.SerializeObject(obj);

        }

        public static string Serialize<T>(T obj)
        {
            return Serialize((object)obj);
        }

        #endregion

        public JsonRestFormatter() : base()
        {
            this.UseDataContractJsonSerializer = true;
            this.SerializerSettings.MaxDepth = 8;
            this.SerializerSettings.ObjectCreationHandling = ObjectCreationHandling.Replace;
            //this.SerializerSettings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
        }

        public override bool CanReadType(Type type) { return true; }
        public override bool CanWriteType(Type type) { return true; }

        public override object ReadFromStream(Type type, Stream readStream, Encoding effectiveEncoding, IFormatterLogger formatterLogger)
        {
            try
            {
                using (var stream = new StreamReader(new BufferedStream(readStream)))
                {
                    string json = stream.ReadToEnd();
                    var data = Deserialize(json, type);
                    return data;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public override void WriteToStream(Type type, object value, Stream writeStream, Encoding effectiveEncoding)
        {
            try
            {
                using (var stream = new StreamWriter(new BufferedStream(writeStream)))
                {
                    string json = Serialize(value);
                    stream.WriteLine(json);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
        }

        public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
        {
            return base.ReadFromStreamAsync(type, readStream, content, formatterLogger, cancellationToken);
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
        {
            return base.WriteToStreamAsync(type, value, writeStream, content, transportContext, cancellationToken);
        }

    }

}
