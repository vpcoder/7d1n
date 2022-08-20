using System.Collections.Generic;
using Engine.Logic.Locations.Generator.Environment.Building.Rooms;
using Mapbox.Map;
using UnityEngine;

namespace Engine.Logic.Locations.Generator.Environment.Building.Arrangement.Impls.Kitchen.Items
{
    
    public class ChairPutProcessor : ItemPutBaseProcessor<KitchenItemType>
    {
        
        public override KitchenItemType Type => KitchenItemType.Chair;
        
        public override bool TryPutItem(GenerationRoomContext context, IEnvironmentItem<KitchenItemType> currentInsertItem)
        {
            var foundedSegments = GetAroundTableSegments();
            return TryPutOnRandomSegment(context, foundedSegments, EdgeLayout.Floor, currentInsertItem);
        }

        public IList<TileSegmentLink> GetAroundTableSegments()
        {
            var tableList = Parent.ArrangementContext.GetItems(KitchenItemType.Table);
            var table = Lists.First(tableList);
            if (table == null)
                return null;
            var link = table.Context;
            var tile = link.Tile;
            var tiles = new List<TileItem>();
            if (tile.LeftOfThis != null)
            {
                tiles.Add(tile.LeftOfThis);
                PutIfExists(tiles, tile.LeftOfThis.TopOfThis);
                PutIfExists(tiles, tile.LeftOfThis.BottomOfThis);
            }
            if (tile.RightOfThis != null)
            {
                tiles.Add(tile.RightOfThis);
                PutIfExists(tiles, tile.RightOfThis.TopOfThis);
                PutIfExists(tiles, tile.RightOfThis.BottomOfThis);
            }
            if (tile.TopOfThis != null)
            {
                tiles.Add(tile.TopOfThis);
                PutIfExists(tiles, tile.TopOfThis.LeftOfThis);
                PutIfExists(tiles, tile.TopOfThis.RightOfThis);
            }
            if (tile.BottomOfThis != null)
            {
                tiles.Add(tile.BottomOfThis);
                PutIfExists(tiles, tile.BottomOfThis.LeftOfThis);
                PutIfExists(tiles, tile.BottomOfThis.RightOfThis);
            }
            
            var foundedSegments = TileService.GetFurnitureOnTheLayoutByTiles(TileLayoutType.Floor, tiles, linkItem =>
            {
                if (linkItem.Item != null
                    || linkItem.Tile.HasDoor
                    || CheckLinkedTilesHasDoor(linkItem.Tile))
                    return false; // Нас не интерисуют заполненные мебелью сегменты и сегменты у двери, так же не интерисуют тайлы которые находятся напротив двери

                if (linkItem.Tile.LeftOfThis == tile &&
                    linkItem.SegmentType.IsOneOf(TileSegmentType.S00, TileSegmentType.S01))
                    return true;
                
                if (linkItem.Tile.RightOfThis == tile &&
                    linkItem.SegmentType.IsOneOf(TileSegmentType.S10, TileSegmentType.S11))
                    return true;
                
                if (linkItem.Tile.TopOfThis == tile &&
                    linkItem.SegmentType.IsOneOf(TileSegmentType.S01, TileSegmentType.S11))
                    return true;
                
                if (linkItem.Tile.BottomOfThis == tile &&
                    linkItem.SegmentType.IsOneOf(TileSegmentType.S00, TileSegmentType.S10))
                    return true;
                
                // Остальные места нам не подходят
                return false;
            });

            if (Lists.IsEmpty(foundedSegments))
                return foundedSegments;
            
            var updatedList = new List<TileSegmentLink>();
            foreach (var segmentLink in foundedSegments)
            {
                
                // Поворачиваем стулья относительно положения стола
                var updatedLayout = EdgeLayout.LeftInside;
                if (segmentLink.Tile.LeftOfThis == tile)
                    updatedLayout = EdgeLayout.RightInside;
                if (segmentLink.Tile.RightOfThis == tile)
                    updatedLayout = EdgeLayout.LeftInside;
                if (segmentLink.Tile.TopOfThis == tile)
                    updatedLayout = EdgeLayout.BottomInside;
                if (segmentLink.Tile.BottomOfThis == tile)
                    updatedLayout = EdgeLayout.TopInside;
                
                updatedList.Add(new TileSegmentLink(segmentLink)
                {
                    EdgeLayout = updatedLayout,
                    EdgeType = segmentLink.Tile.GetEdge(updatedLayout),
                });
            }
            
            return updatedList;
        }

        private void PutIfExists(IList<TileItem> list, TileItem item)
        {
            if (item == null)
                return;
            if(!list.Contains(item))
                list.Add(item);
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