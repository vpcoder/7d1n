using com.baensi.sdon.tools.reflection;
using NLog;
using System;
using System.Collections.Generic;

namespace com.baensi.sdon.server.cache
{

    public class CacheFactory
    {

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private IDictionary<Type, IDataDictionary> _cacheData = new Dictionary<Type, IDataDictionary>();

        #region Singleton

        private static CacheFactory _instance;

        public static CacheFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CacheFactory();

                return _instance;
            }
        }

        #endregion

        #region Ctors

        public CacheFactory()
        {
            logger.Debug("init cache factory...");

            Domain.ForEachTypes((type) =>
            {
                if (!type.IsClass || type.IsAbstract)
                    return;

                if (!typeof(IDataDictionary).IsAssignableFrom(type))
                    return;

                var tmp = (IDataDictionary)Activator.CreateInstance(type);
                _cacheData.Add(type,tmp);
            });
        }

        #endregion

        public T Get<T>() where T : class, IDataDictionary
        {
            try
            {
                IDataDictionary tmp = null;

                if (!_cacheData.TryGetValue(typeof(T), out tmp))
                    logger.Error($"cache factory get method exception, type '{typeof(T).Name}' not found!");

                return (T)tmp;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
            }

            return null;
        }

    }

}
