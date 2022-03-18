using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Engine.Data.Generation.Xml
{

    /// <summary>
    /// Базовый класс загрузчика в формате XML
    /// </summary>
    /// <typeparam name="T">Тип хранимых объектов в фабрике</typeparam>
    public abstract class XmlLoaderBase<T> where T : class, IElementIdentity
    {

        public string[] FileNames { get; set; }

        protected XmlElement current;

        public ICollection<T> Load()
        {
            var data = new HashSet<T>();

            foreach (var file in FileNames)
            {
                var asset = Resources.Load<TextAsset>(file);
                var document = new XmlDocument();
                document.LoadXml(asset.text);
                foreach (XmlElement element in document.GetElementsByTagName("Element"))
                {
                    try
                    {
                        current = element;
                        data.Add(ReadElement());
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

        protected abstract T ReadElement();

        protected string Str(string name)
        {
            return Str(current, name);
        }
        
        protected V Enm<V>(string name) where V : struct
        {
            return Enm<V>(current, name);
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
