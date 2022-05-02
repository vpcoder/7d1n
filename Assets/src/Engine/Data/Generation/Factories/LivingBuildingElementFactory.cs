using Engine.Data.Generation.Elements;
using Engine.Data.Generation.Xml.Impls;
using Engine.Generator;
using Engine.Logic.Locations.Generator;

namespace Engine.Data.Generation
{

    /// <summary>
    /// 
    /// Фабрика элементов генерации для жилых помещений
    /// ---
    /// Factory of generating elements for living spaces
    ///     
    /// </summary>
    public class LivingBuildingElementFactory : GenerationElementFactoryBase<BuildingElement, RoomType, BuildingElementXmlLoader>
    {
        public override LocationType LocationType => LocationType.Living;
    }

}
