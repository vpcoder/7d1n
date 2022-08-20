using Engine.Logic.Locations.Generator.Environment.Building.Rooms;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Hall.Items
{
    
    public class TVSetProcessor : ItemPutBaseProcessor<HallItemType>
    {

        public override HallItemType Type => HallItemType.TVSet;

        public override bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<HallItemType> currentInsertItem)
        {
            var tvCabinetList = Parent.ArrangementContext.GetItems(HallItemType.TVCabinet);
            var tvCabinet = Lists.First(tvCabinetList);
            if (tvCabinet == null)
                return false;

            var position = GetPositionOnSurface(currentInsertItem.ToObject, tvCabinet.ToObject,
                SurfacePositionAlighmentType.CenterCenter);
            var rotation = GetRotationBySegmentLink(tvCabinet.Context);
            
            return TryPutOnGlobalPos(currentInsertItem, tvCabinet.Context, position, rotation);
        }

    }
    
}