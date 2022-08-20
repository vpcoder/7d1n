using System;
using System.Linq;
using System.Collections.Generic;

namespace Engine.Logic
{

    public class LocationCellGenerationFactory
    {

        private IDictionary<BiomType, ILocationCellGenerator> data;

        #region Singleton

        private static Lazy<LocationCellGenerationFactory> instance = new Lazy<LocationCellGenerationFactory>(() => new LocationCellGenerationFactory());
        public static LocationCellGenerationFactory Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private LocationCellGenerationFactory()
        {
            var list = new[]
            {
                typeof(WaterLocationCellGenerator),
                typeof(SandLocationCellGenerator),
                typeof(ExhaustedLocationCellGenerator),
                typeof(WoodenLocationCellGenerator),
            };
            data = list.Select(item => (ILocationCellGenerator)Activator.CreateInstance(item))
                       .ToDictionary(item => item.Biom, item => item);
        }

        #endregion

        public ILocationCellGenerator GetGenerator(BiomType biom)
        {
            ILocationCellGenerator result;
            if (!data.TryGetValue(biom, out result))
                throw new NotSupportedException();
            return result;
        }


    }

}
