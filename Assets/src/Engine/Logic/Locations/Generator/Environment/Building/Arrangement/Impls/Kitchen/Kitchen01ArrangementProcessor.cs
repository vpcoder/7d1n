using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Kitchen
{

    public class Kitchen01ArrangementProcessor : ArrangementProcessorBase<KitchenItemType>
    {

        private readonly IDictionary<KitchenItemType, IItemPutProcessor<KitchenItemType>> putProcessorsData =
            new Dictionary<KitchenItemType, IItemPutProcessor<KitchenItemType>>();
        
        public override RoomKindType RoomType => RoomKindType.Kitchen;
        
        public Kitchen01ArrangementProcessor()
        {
            foreach (var processor in AssembliesHandler.CreateImplementations<IItemPutProcessor<KitchenItemType>>())
                putProcessorsData.Add(processor.Type, processor);
        }
        
        public override bool InsertItemIntoScene(GenerationRoomContext context, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {
            // FIXME: Для отладки
            foreach (var item in context.TilesInfo.TilesData)
            {
                item.Marker.Emission = Color.black;
                item.Marker.Segments.Clear();
            }

            putProcessorsData.TryGetValue(currentInsertItem.Type, out var processor);
            return processor?.TryPutItem(context, currentInsertItem) ?? false;
        }

    }

}
