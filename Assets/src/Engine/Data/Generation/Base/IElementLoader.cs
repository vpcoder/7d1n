using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Data.Generation
{

    /// <summary>
    /// 
    /// Загрузчик элементов стилей
    /// ---
    /// Style Elements Loader
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
    public interface IElementLoader<T, E> where T : IElementIdentity<E>
                                          where E : struct
    {

        /// <summary>
        ///     Выполняет загрузку всех элементов
        ///     ---
        ///     Loads all elements
        /// </summary>
        /// <returns>
        ///     Коллекция прочитанных элементов
        ///     ---
        ///     Collection of read elements
        /// </returns>
        ICollection<T> LoadAll();

    }

}
