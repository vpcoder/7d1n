using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Reflection;

namespace com.baensi.sdon.db.entity
{

    public static class EntityHandler
    {

        class FieldsHandler
        {
            public IEnumerable<PropertyInfo> Properties { get; }

            public FieldsHandler(Type type)
            {
                this.Properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            }
        }

        private static IDictionary<Type, FieldsHandler> _handlers = new Dictionary<Type, FieldsHandler>();

        private static FieldsHandler GetFieldsHandler(Type type)
        {
            FieldsHandler result = null;

            if(!_handlers.TryGetValue(type, out result))
            {
                result = new FieldsHandler(type);
                _handlers.Add(type, result);
            }

            return result;
        }

        /// <summary>
        /// Копирует содержимое полей из экземпляра item0 в экземпляр item1 типа T
        /// </summary>
        /// <typeparam name="T">Тип экземпляра, поля которого надо скопировать</typeparam>
        /// <param name="item0">Откуда копировать</param>
        /// <param name="item1">Куда копировать</param>
        public static void Copy<T>(T item0, T item1) where T : class, IEntity, new()
        {
            var handler = GetFieldsHandler(typeof(T));

            foreach(var property in handler.Properties)
            {
                var value = property.GetGetMethod().Invoke(item0, new object[] { });
                property.GetSetMethod().Invoke(item1, new object[] { value });
            }
        }

    }

}
