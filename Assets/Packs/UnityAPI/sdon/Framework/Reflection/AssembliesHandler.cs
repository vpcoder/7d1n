using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine
{

    /// <summary>
    /// 
    /// Работает со сборками и типами в сборках
    /// ---
    /// Works with assemblies and types in assemblies
    /// 
    /// </summary>
    public static class AssembliesHandler
    {

        /// <summary>
        ///     Выполняет поиск реализаций, соответствующих шаблону T
        ///     ---
        ///     Searches for implementations that match the pattern T
        /// </summary>
        /// <typeparam name="T">
        ///     Искомый тип, для которого нужно найти реализации
        ///     ---
        ///     The type you are looking for, for which you want to find implementations
        /// </typeparam>
        /// <returns>
        ///     Коллекцию найденных типов-реализаций
        ///     ---
        ///     A collection of found types-realizations
        /// </returns>
        public static ICollection<Type> FindImplementationTypes<T>() where T : class
        {
            var list = new List<Type>();
            try
            {
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    try
                    {
                        foreach (var type in assembly.GetTypes())
                        {
                            if (!typeof(T).IsAssignableFrom(type) || type.IsAbstract || type.IsNotPublic)
                                continue;
                            list.Add(type);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.LogException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            return list;
        }

        /// <summary>
        ///     Находит реализации указанного типа T, создаёт экземпляры этих классов
        ///     ---
        ///     Finds implementations of the specified T type, creates instances of these classes
        /// </summary>
        /// <typeparam name="T">
        ///     Искомый тип, для которого нужно найти реализации
        ///     ---
        ///     The type you are looking for, for which you want to find implementations
        /// </typeparam>
        /// <returns>
        ///     Сформированная коллекция экземпляров классов-реализаций для типа T, которые были построены конструктором по умолчанию
        ///     ---
        ///     A generated collection of instances of class-realizations for type T that were constructed by the default constructor
        /// </returns>
        public static ICollection<T> CreateImplementations<T>() where T : class
        {
            return FindImplementationTypes<T>() // Находим Типы реализаций
                .Select(o => (T)Activator.CreateInstance(o)) // Создаём экземпляры классов с конструктором по умолчанию
                .ToList(); // Формируем список
        }

    }

}
