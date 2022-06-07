using Engine.Logic.Locations.Generator.Environment.Building.Rooms;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Hall.Items
{
    
    public class ToasterProcessor : ItemPutBaseProcessor<KitchenItemType>
    {

        public override KitchenItemType Type => KitchenItemType.Toaster;

        public override bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {
            var objectsList = Parent.ArrangementContext.GetItems
            (
                KitchenItemType.CounterCorner,
                          KitchenItemType.Counter,
                          KitchenItemType.Table,
                          KitchenItemType.CounterWashing,
                          KitchenItemType.EmptyCounter
           );

            if (objectsList.Count == 0)
                return false;

            var randomObject = objectsList[context.RoomRandom.Next(0, objectsList.Count - 1)];
            
            var position = GetPositionOnSurface(currentInsertItem.ToObject, randomObject.ToObject,
                SurfacePositionAlighmentType.CenterCenter);

            var rotAngles = new Vector3(0f, Random.value * 360f, 0f);
            var rotation = Quaternion.Euler(rotAngles);
            return TryPutOnGlobalPos(currentInsertItem, randomObject.Context, position, rotation);
        }

    }
    
}