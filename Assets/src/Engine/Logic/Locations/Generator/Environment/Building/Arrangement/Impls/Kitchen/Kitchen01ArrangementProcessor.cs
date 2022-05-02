using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Kitchen
{

    public class Kitchen01ArrangementProcessor : ArrangementProcessorBase<KitchenItemType>
    {

        #region Hidden Fields
        
        private readonly IDictionary<KitchenItemType, IItemPutProcessor<KitchenItemType>> putProcessorsData;
        
        #endregion
        
        #region Properties
        
        public override RoomKindType RoomType => RoomKindType.Kitchen;
        
        #endregion

        #region Ctor

        public Kitchen01ArrangementProcessor()
        {
            putProcessorsData = new Dictionary<KitchenItemType, IItemPutProcessor<KitchenItemType>>();
            foreach (var processor in AssembliesHandler.CreateImplementations<IItemPutProcessor<KitchenItemType>>())
            {
                processor.Parent = this;
                putProcessorsData.Add(processor.Type, processor);
            }
        }
        
        #endregion
        
        public override bool InsertItemIntoScene(GenerationRoomContext context, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {
            // FIXME: Для отладки
            foreach (var item in context.TilesInfo.TilesData)
            {
                item.Marker.Emission = Color.black;
                item.Marker.Segments.Clear();
            }

            // Добавление мебели через процессоры
            putProcessorsData.TryGetValue(currentInsertItem.Type, out var processor);
            return processor?.TryPutItem(context, currentInsertItem) ?? false;
        }

    }

}
