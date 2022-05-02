using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Engine.Data.Generation.Xml
{

    /// <summary>
    /// 
    /// Базовый загрузчик элемента на базе XML
    /// ---
    /// Basic XML-based element loader
    /// 
    /// </summary>
    /// <typeparam name="T">
    ///     Тип хранимого элемента
    ///     ---
    ///     Stored element type
    /// </typeparam>
    /// <typeparam name="E">
    ///     Группа стилей
    ///     ---
    ///     Style group
    /// </typeparam>
    public abstract class XmlLoaderBase<T, E> : IElementLoader<T, E>
                                              where T : class, IElementIdentity<E>
                                              where E : struct
    {

        #region Hidden Fields

        /// <summary>
        ///     Список файлов, подлежащих чтению.
        ///     Вся информация из файлов будет собрана в одну единую коллекцию для LoadAll
        ///     ---
        ///     The list of files to be read.
        ///     All information from the files will be gathered into one single collection for LoadAll
        /// </summary>
        protected abstract string[] FileNames { get; }

        /// <summary>
        ///     Текущий элемент, который читаем из какого то файла
        ///     ---
        ///     The current item, which is read from some file
        /// </summary>
        private XmlElement current;

        #endregion

        /// <summary>
        ///     Выполняет загрузку всех элементов из файлов, и представляет их в виде коллекции
        ///     ---
        ///     Loads all items from files, and presents them as a collection
        /// </summary>
        /// <returns>
        ///     Коллекция загруженных элементов
        ///     ---
        ///     Collection of loaded items
        /// </returns>
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

        protected string Str(string name)
        {
            return Str(current, name);
        }
        
        protected V Enm<V>(string name) where V : struct
        {
            return Enm<V>(current, name);
        }

        #endregion

        #region Hidden Methods
        
        private long Lng(XmlElement element, string name)
        {
            return Get(element, name, long.Parse);
        }
        
        private string Str(XmlElement element, string name)
        {
            return Get(element, name);
        }

        private V Enm<V>(XmlElement element, string name) where V : struct
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

        #endregion

    }

}
