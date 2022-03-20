using System;
using System.Collections.Generic;

namespace Engine.Data.Factories
{

    /// <summary>
    /// Фабрика факторов
    /// </summary>
    public class FactorFactory
    {

        #region Singleton

        private static readonly Lazy<FactorFactory> instance = new Lazy<FactorFactory>(() => new FactorFactory());
        public static FactorFactory Instance { get { return instance.Value; } }
        private FactorFactory()
        {
            LoadFactors();
        }

        #endregion

        private IDictionary<long, IFactor> factors = new Dictionary<long, IFactor>();

        private void LoadFactors()
        {

        }

        private IFactor Load(long id)
        {
            return null;
        }

    }

}
