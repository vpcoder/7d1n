using System.Collections.Generic;

namespace Engine.Logic.Locations.Generator.Environment.Building
{

    /// <summary>
    /// 
    /// Загрузчик объектов окружения в помещениях
    /// ---
    /// Loader of indoor environment objects
    /// 
    /// </summary>
    /// <typeparam name="E">
    ///     Объект
    ///     ---
    ///     Object
    /// </typeparam>
    public interface IEnvironmentItemLoader<E> where E : struct
    {

        /// <summary>
        ///     Выполняет загрузку всех объектов окружения
        ///     ---
        ///     Loads all environment objects
        /// </summary>
        /// <returns>
        ///     Коллекция прочитанных объектов
        ///     ---
        ///     Collection of read object
        /// </returns>
        ICollection<IEnvironmentItem<E>> LoadAll();

    }

}
