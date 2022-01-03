using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Factories.Xml
{

    /// <summary>
    /// Загрузчик фабрики факторов
    /// </summary>
    public class XmlFactoryLoaderFactor : XmlFactoryLoaderBase<IFactor>
    {

        /// <summary>
        /// Коллекция действий факторов
        /// </summary>
        private readonly IDictionary<string, IFactorAction> actions = new Dictionary<string, IFactorAction>();

        public XmlFactoryLoaderFactor()
        {
            FileNames = new[] { "Data/factors_data" };

            // TODO: заполнить actions
        }

        protected override IFactor ReadItem()
        {
            var factor = new Factor();
            factor.ID            = Lng("ID");
            factor.Name          = Str("Name");
            factor.Description   = Str("Description");
            factor.DurationCount = Int("DurationCount");
            factor.Iteration     = factor.DurationCount;
            factor.Sprite        = Resources.Load<Sprite>("Data/Factors/" + Str("Sprite"));
            factor.StartFactor   = GetFactorAction(Str("StartFactor"));
            factor.EndFactor     = GetFactorAction(Str("EndFactor"));
            factor.DoFactor      = GetFactorAction(Str("DoFactor"));
            return factor;
        }

        private FactorActionDelegate GetFactorAction(string factorActionName)
        {
            if (factorActionName == null || factorActionName.Length == 0)
                return null;

            if (!actions.TryGetValue(factorActionName, out IFactorAction action))
                return null;

            return action.Run;
        }

    }

}
