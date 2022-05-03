using System;
using UnityEngine;

namespace Engine.Logic.Locations.Generator
{
    
    /// <summary>
    ///
    /// Сегменты маленьких тайлов
    /// ---
    /// Segments of small tiles
    /// 
    /// </summary>
    public enum TileSegmentType
    {
        
        /// <summary>
        /// -----------
        /// |    |    |
        /// |----|----|
        /// |  * |    |
        /// -----------
        /// </summary>
        S00 = 0x00,
        
        /// <summary>
        /// -----------
        /// |  * |    |
        /// |----|----|
        /// |    |    |
        /// -----------
        /// </summary>
        S01 = 0x01,
        
        /// <summary>
        /// -----------
        /// |    |    |
        /// |----|----|
        /// |    |  * |
        /// -----------
        /// </summary>
        S10 = 0x02,
        
        /// <summary>
        /// -----------
        /// |    |  * |
        /// |----|----|
        /// |    |    |
        /// -----------
        /// </summary>
        S11 = 0x03,
        
    };

    public static class TileSegmentTypeAdditions
    {

        public static TileSegmentType MoveByDirection(this TileSegmentType segment, TileLayoutType layout, TileItem currentTile, TileFindDirection direction, out TileItem nextTile)
        {
            switch (layout)
            {
                case TileLayoutType.Ceiling:
                case TileLayoutType.Floor:
                    return MoveOnFloorByDirection(segment, currentTile, direction, out nextTile);
                default:
                        throw new NotSupportedException();
            }
        }

        private static TileSegmentType MoveOnFloorByDirection(TileSegmentType segment,  TileItem currentTile, TileFindDirection direction, out TileItem nextTile)
        {
            switch (segment)
            {
                case TileSegmentType.S00:
                    switch (direction)
                    {
                        case TileFindDirection.Left:
                            nextTile = currentTile.LeftOfThis; // Уходим на левый тайл
                            return TileSegmentType.S10;
                        case TileFindDirection.Right:
                            nextTile = currentTile; // Остаёмся на текущем тайле
                            return TileSegmentType.S10;
                        case TileFindDirection.Top:
                            nextTile = currentTile;
                            return TileSegmentType.S01;
                        case TileFindDirection.Bottom:
                            nextTile = currentTile.BottomOfThis;
                            return TileSegmentType.S01;
                        default:
                            throw new NotSupportedException();
                    }
                case TileSegmentType.S01:
                    switch (direction)
                    {
                        case TileFindDirection.Left:
                            nextTile = currentTile.LeftOfThis; // Уходим на левый тайл
                            return TileSegmentType.S11;
                        case TileFindDirection.Right:
                            nextTile = currentTile; // Остаёмся на текущем тайле
                            return TileSegmentType.S11;
                        case TileFindDirection.Top:
                            nextTile = currentTile.TopOfThis;
                            return TileSegmentType.S00;
                        case TileFindDirection.Bottom:
                            nextTile = currentTile;
                            return TileSegmentType.S00;
                        default:
                            throw new NotSupportedException();
                    }
                case TileSegmentType.S10:
                    switch (direction)
                    {
                        case TileFindDirection.Left:
                            nextTile = currentTile; // Остаёмся на текущем тайле
                            return TileSegmentType.S00;
                        case TileFindDirection.Right:
                            nextTile = currentTile.RightOfThis; // Уходим на правый тайл
                            return TileSegmentType.S00;
                        case TileFindDirection.Top:
                            nextTile = currentTile;
                            return TileSegmentType.S11;
                        case TileFindDirection.Bottom:
                            nextTile = currentTile.BottomOfThis;
                            return TileSegmentType.S11;
                        default:
                            throw new NotSupportedException();
                    }
                case TileSegmentType.S11:
                    switch (direction)
                    {
                        case TileFindDirection.Left:
                            nextTile = currentTile; // Остаёмся на текущем тайле
                            return TileSegmentType.S01;
                        case TileFindDirection.Right:
                            nextTile = currentTile.RightOfThis; // Уходим на правый тайл
                            return TileSegmentType.S01;
                        case TileFindDirection.Top:
                            nextTile = currentTile.TopOfThis;
                            return TileSegmentType.S10;
                        case TileFindDirection.Bottom:
                            nextTile = currentTile;
                            return TileSegmentType.S10;
                        default:
                            throw new NotSupportedException();
                    }
                default:
                    throw new NotSupportedException();
            }
        }
        
    }
    
}