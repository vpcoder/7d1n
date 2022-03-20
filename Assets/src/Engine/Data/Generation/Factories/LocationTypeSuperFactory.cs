using Engine.Data.Generation.Elements;
using Engine.Generator;
using Engine.Logic.Locations.Generator;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Engine.Data.Generation.Factories
{

    /// <summary>
    /// 
    /// Фабрика фабрик - супер фабрика. Хранит связи типов локации с фабриками генерации элементов
    /// ---
    /// Factory Factory is a super factory. Stores links of location types with factories of element generation
    /// 
    /// </summary>
    public class LocationTypeSuperFactory
    {

        #region Singleton

        private static readonly Lazy<LocationTypeSuperFactory> instance = new Lazy<LocationTypeSuperFactory>(() => new LocationTypeSuperFactory());
        public static LocationTypeSuperFactory Instance { get { return instance.Value; } }
        private LocationTypeSuperFactory()
        {
            foreach(var factory in AssembliesHandler.CreateImplementations<IGenerationElementFactory<BuildingElement, RoomType>>())
                elementsFactories.Add(factory.LocationType, factory);
        }

        #endregion

        #region Hidden Fields

        /// <summary>
        /// 
        /// Фабрика всех известных фабрик, для работы с элементами генерации зданий
        /// ---
        /// Factory of all known factories, to work with the elements of the generation of buildings
        /// 
        /// </summary>
        private IDictionary<LocationType, IGenerationElementFactory<BuildingElement, RoomType>> elementsFactories = new Dictionary<LocationType, IGenerationElementFactory<BuildingElement, RoomType>>();

        #endregion

        #region Shared Methods

        /// <summary>
        ///     Получает фабрику, для указанного типа локации
        ///     ---
        ///     Gets the factory, for the specified location type
        /// </summary>
        /// <param name="locationType">
        ///     Тип локации, для которого нужно получить фабрику генерации элементов
        ///     ---
        ///     The type of location for which you want to get the element generation factory
        /// </param>
        /// <returns>
        ///     Фабрика генерации элементов
        ///     ---
        ///     Element generation factory
        /// </returns>
        public IGenerationElementFactory<BuildingElement, RoomType> Get(LocationType locationType)
        {
            return elementsFactories[locationType];
        }

        #endregion

    }

}
