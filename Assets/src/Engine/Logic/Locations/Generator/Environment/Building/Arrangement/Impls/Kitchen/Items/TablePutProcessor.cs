using Engine.Logic.Locations.Generator.Environment.Building.Rooms;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Kitchen.Items
{
    
    public class TablePutProcessor : ItemPutBaseProcessor<KitchenItemType>
    {
        
        public override KitchenItemType Type => KitchenItemType.Table;
        
        public override bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {
            var foundedSegments = TileService.GetFurnitureOnTheLayoutByTiles(TileLayoutType.Floor, context.TilesInfo.TilesData, Filter);
            return TryPutOnRandomSegment(context, foundedSegments, EdgeLayout.Floor, currentInsertItem, true);
        }

        private bool Filter(TileSegmentLink link)
        {
            if (!link.Tile.IsEmptyFurniture
                || link.Tile.HasDoor
                || CheckLinkedTilesHasDoor(link.Tile))
                return false; // Нас не интерисуют заполненные мебелью сегменты и сегменты у двери, так же не интерисуют тайлы которые находятся напротив двери
            
            // Остальные места нам подходят
            return true;
        }

        private bool CheckLinkedTilesHasDoor(TileItem tile)
        {
            foreach (var checkItem in checkSet)
            {
                var linkTile = tile.GetTile(checkItem);
                if (linkTile != null && linkTile.HasDoor)
                    return true;
            }
            return false;
        }
        
        private static readonly TileLayoutType[] checkSet = { TileLayoutType.WallBottom, TileLayoutType.WallTop, TileLayoutType.WallRight, TileLayoutType.WallLeft };
        
    }
    
}