using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories
{

    /// <summary>
    /// Базовый класс фабрики
    /// </summary>
    /// <typeparam name="T">Тип хранимого объекта в фабрике</typeparam>
    /// <typeparam name="TLoader">Загрузчик для этой фабрики</typeparam>
    public abstract class FactoryBase<T, TLoader> : IFactory<T> where T : class, IIdentity
                                                                where TLoader : class, IFactoryLoader<T>
    {

        private readonly IDictionary<long, T> dataByID = new Dictionary<long, T>();
        private readonly IFactoryLoader<T> loader;

        public IDictionary<long, T> Data
        {
            get
            {
                return this.dataByID;
            }
        }

        public IFactoryLoader<T> Loader
        {
            get
            {
                return this.loader;
            }
        }

        public FactoryBase()
        {
            this.loader = Activator.CreateInstance<TLoader>();
            this.ReloadFactory();
        }

        public void ReloadFactory()
        {
            if (loader == null)
            {
                throw new Exception("Hasn't setup loader!");
            }

            this.dataByID.Clear();

            foreach (T value in this.loader.Load())
            {
                this.dataByID.Add(value.ID, value);
            }
        }

        public T Get(long id)
        {
            T value = null;
            if (dataByID.TryGetValue(id, out value))
            {
                return value;
            }
#if UNITY_EDITOR
            Debug.LogError("T " + typeof(T).Name + " with id " + id + " not founded in factory " + GetType().Name + "!");
#endif
            return null;
        }

        public virtual T Create(long id)
        {
            return (T)Get(id).Copy();
        }

    }

}
