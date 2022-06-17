using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Generator;
using UnityEngine;

namespace Engine.Data.Generation
{

    /// <summary>
    /// 
    ///     Базовый класс фабрики для работы с элементами
    ///     ---
    ///     Basic factory class for working with elements
    ///     
    /// </summary>
    /// <typeparam name="T">
    ///     Тип элемента генерации
    ///     ---
    ///     Type of generation element
    /// </typeparam>
    /// <typeparam name="E">
    ///     Тип стиля генерации
    ///     ---
    ///     Type of generation style
    /// </typeparam>
    /// <typeparam name="TLoader">
    ///     Загрузчик элементов
    ///     ---
    ///     Element Loader
    /// </typeparam>
    public abstract class GenerationElementFactoryBase<T, E, TLoader> : IGenerationElementFactory<T, E>
                                                                      where TLoader : IElementLoader<T, E>
                                                                      where T : class, IElementIdentity<E>
                                                                      where E : struct
    {

        #region Hidden Fields

        /// <summary>
        ///     Словарь элементов
        ///         Стиль -> (ИД стиля -> Элемент)
        ///         ---
        ///      Elements Dictionary
        ///         Style -> (Style ID -> Element)
        /// </summary>
        private IDictionary<E, IDictionary<long, T>> data;

        /// <summary>
        ///     Тип локации с которым работает наша фабрика
        ///     ---
        ///     Type of location our factory works with
        /// </summary>
        public abstract LocationType LocationType { get; }

        #endregion

        #region Ctor

        public GenerationElementFactoryBase()
        {
            var loader = Activator.CreateInstance<TLoader>();
            this.data = ConvertToDictionary(loader.LoadAll());
        }

        #endregion

        #region Hidden Methods

        /// <summary>
        ///     Выполняет конвертацию простой коллекции элементов в словарь
        ///     ---
        ///     Converts a simple collection of items into a dictionary
        /// </summary>
        /// <param name="elements">
        ///     Все возможные элементы
        ///     ---
        ///     All possible elements
        /// </param>
        /// <returns>
        ///     Сформированный словарь
        ///     ---
        ///     Dictionary Formed
        /// </returns>
        private IDictionary<E, IDictionary<long, T>> ConvertToDictionary(ICollection<T> elements)
        {
            var dictionary = new Dictionary<E, IDictionary<long, T>>();

            foreach(var element in elements)
            {
                var type = element.Type;
                var id   = element.ID;
                dictionary.AddInToDictionary(type, id, element);
            }

            return dictionary;
        }

        #endregion

        #region Shared Methods

        /// <summary>
        ///     Выполняет подсчёт вариаций элементов определённого стиля
        ///     ---
        ///     Performs a count of variations of elements of a particular style
        /// </summary>
        /// <param name="type">
        ///     Проверяемый стиль
        ///     ---
        ///     Selectable style
        /// </param>
        /// <returns>
        ///     Количество доступных вариаций
        ///     ---
        ///     Number of variations available
        /// </returns>
        public int GetCount(E type)
        {
            return Get(type).Count;
        }

        /// <summary>
        ///     Получает все вариации элементов определённого стиля
        ///     ---
        ///     Gets all variations of elements of a certain style
        /// </summary>
        /// <param name="type">
        ///     Проверяемый стиль
        ///     ---
        ///     Selectable style
        /// </param>
        /// <returns>
        ///     Словарь вариаций по идентификатору
        ///     ---
        ///     Dictionary of variations by identifier
        /// </returns>
        public IDictionary<long, T> Get(E type)
        {
            if (!data.TryGetValue(type, out var elementByIdDictionary))
                throw new KeyNotFoundException("type '" + type.ToString() + "' isn't founded!");
            return elementByIdDictionary;
        }

        /// <summary>
        ///     Получает конкретную вариацию элемента по стилю и идентификатору
        ///     ---
        ///     Gets a specific variation of an element by style and identifier
        /// </summary>
        /// <param name="type">
        ///     Проверяемый стиль
        ///     ---
        ///     Selectable style
        /// </param>
        /// <param name="id">
        ///     Проверяемый идентификатор
        ///     ---
        ///     Verifiable identifier
        /// </param>
        /// <returns>
        ///     Конкретную вариацию элемента для определённого стиля и идентификатора
        ///     ---
        ///     A specific variation of an element for a specific style and identifier
        /// </returns>
        public T Get(E type, long id)
        {
            var dictionaryById = Get(type);
            if(!dictionaryById.TryGetValue(id, out var element))
                throw new KeyNotFoundException("type '" + type.ToString() + "', id '" + id + "' isn't founded!");
            return element;
        }

        #endregion

    }

}
