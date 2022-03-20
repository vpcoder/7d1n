using System.Collections.Generic;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement
{

    public class ArrangementSuperFactory
    {

        #region Singleton

        private static Lazy<ArrangementSuperFactory> instance = new Lazy<ArrangementSuperFactory>(() => new ArrangementSuperFactory());

        public static ArrangementSuperFactory Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private ArrangementSuperFactory()
        {
            foreach (var processor in AssembliesHandler.CreateImplementations<IArrangementProcessor>())
                data.AddInToList(processor.RoomType, processor);
        }

        #endregion

        private IDictionary<RoomKindType, IList<IArrangementProcessor>> data = new Dictionary<RoomKindType, IList<IArrangementProcessor>>();




    }

}
