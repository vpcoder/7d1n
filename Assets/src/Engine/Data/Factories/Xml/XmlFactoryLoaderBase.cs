using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;

namespace Engine.Data.Factories.Xml
{

    /// <summary>
    /// Базовый класс загрузчика в формате XML
    /// </summary>
    /// <typeparam name="T">Тип хранимых объектов в фабрике</typeparam>
    public abstract class XmlFactoryLoaderBase<T> : FactoryLoaderBase<T> where T : class, IIdentity
    {

        public string[] FileNames { get; set; }

        protected XmlElement current;

        public override ICollection<T> Load()
        {
            var data = new HashSet<T>();

            foreach (var file in FileNames)
            {
                var asset = Resources.Load<TextAsset>(file);
                var document = new XmlDocument();
                document.LoadXml(asset.text);
                foreach (XmlElement element in document.GetElementsByTagName("Item"))
                {
                    try
                    {
                        //Debug.Log("file: " + file + ", item: " + element.GetAttribute("ID"));
                        current = element;
                        data.Add(ReadItem());
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(element.OuterXml);
                        Debug.LogError(ex);
                    }
                }
            }
            return data;
        }

        protected abstract T ReadItem();

        protected int Int(string name)
        {
            return Int(current, name);
        }

        protected float Flt(string name)
        {
            return Flt(current, name);
        }

        protected long Lng(string name)
        {
            return Lng(current, name);
        }

        protected byte Byt(string name)
        {
            return Byt(current, name);
        }

        protected bool Bol(string name)
        {
            return Bol(current, name);
        }

        protected string Str(string name)
        {
            return Str(current, name);
        }

        protected List<string> Splt(string name)
        {
            var data = Str(name);
            if (data == null || data.Length == 0)
                return new List<string>();

            return data.Split(';').ToList();
        }

        protected List<List<string>> DblSplt(string name)
        {
            var result = new List<List<string>>();
            var data = Str(name);
            if (data == null || data.Length == 0)
                return result;

            foreach(var line in data.Split(';'))
            {
                if (line == null || line.Length == 0)
                    continue;

                result.Add(new List<string>(line.Split(',')));
            }

            return result;
        }

        protected V Enm<V>(string name) where V : struct
        {
            return Enm<V>(current, name);
        }

        protected int Int(XmlElement element, string name)
        {
            return Get(element, name, int.Parse);
        }

        protected float Flt(XmlElement element, string name)
        {
            return Get(element, name, float.Parse);
        }

        protected long Lng(XmlElement element, string name)
        {
            return Get(element, name, long.Parse);
        }

        protected bool Bol(XmlElement element, string name)
        {
            return Get(element, name, bool.Parse);
        }

        protected byte Byt(XmlElement element, string name)
        {
            return Get(element, name, byte.Parse);
        }

        protected string Str(XmlElement element, string name)
        {
            return Get(element, name);
        }

        protected V Enm<V>(XmlElement element, string name) where V : struct
        {
            return Get(element, name, Enums<V>.Parse);
        }

        private V Get<V>(XmlElement element, string name, Func<string, V> parse)
        {
            try
            {
                return parse(Get(element, name));
            }
            catch
            {
                return default;
            }
        }

        private string Get(XmlElement element, string name)
        {
            try
            {
                return element.GetAttribute(name);
            }
            catch
            {
                return null;
            }
        }

    }

}
