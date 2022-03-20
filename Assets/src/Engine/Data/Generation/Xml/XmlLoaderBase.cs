using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Engine.Data.Generation.Xml
{

    public abstract class XmlLoaderBase<T, E> : IElementLoader<T, E>
                                              where T : class, IElementIdentity<E>
                                              where E : struct
    {

        public abstract string[] FileNames { get; }

        public abstract string RootDirectory { get; }

        protected XmlElement current;

        private string GetPath(E type, long id)
        {
            return RootDirectory + type.ToString() + "/" + id + "/";
        }

        protected string GetResourcePath(T element, string attributeName)
        {
            return GetPath(element.Type, element.ID) + Str(attributeName);
        }

        public ICollection<T> LoadAll()
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

        /// <summary>
        ///     Выполняет чтение XML элемента, должен вернуть прочитанный элемент в виде объекта элемента
        ///     ---
        ///     Performs a read of an XML item, must return the read item as an element object
        /// </summary>
        /// <returns>
        ///     Элемент генерации в языке c#
        ///     ---
        ///     The generation element in c#
        /// </returns>
        protected abstract T ReadElement();



        #region Helper Methods

        protected long Lng(string name)
        {
            return Lng(current, name);
        }

        protected long Lng(XmlElement element, string name)
        {
            return Get(element, name, long.Parse);
        }

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

        #endregion

        #region Hidden Methods

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

        #endregion

    }

}
