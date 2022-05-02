using System;

namespace Engine.Logic.Locations.Generator
{

    /// <summary>
    /// 
    /// Направление поиска
    /// ---
    /// Search Direction
    /// 
    /// </summary>
    public enum TileFindDirection
    {

        /// <summary>
        ///     Поиск по горизонтали влево
        ///     ---
        ///     Search horizontally to the left
        /// </summary>
        Left = 0x00,

        /// <summary>
        ///     Поиск по горизонтали вправо
        ///     ---
        ///     Search horizontally to the right
        /// </summary>
        Right = 0x01,
        
        /// <summary>
        ///     Поиск по вертикали вверх
        ///     ---
        ///     Search vertically upwards
        /// </summary>
        Top = 0x02,
        
        /// <summary>
        ///     Поиск по вертикали вниз
        ///     ---
        ///     Search vertically downwards
        /// </summary>
        Bottom = 0x03,

    };

    public static class TileFindDirectionAdditions
    {

        /// <summary>
        ///     Инвертирует направление
        ///     ---
        ///     Inverts the direction
        /// </summary>
        /// <param name="direction">
        ///     Исходное направление
        ///     ---
        ///     Original direction
        /// </param>
        /// <returns>
        ///     Направление обратное к исходному
        ///     ---
        ///     The direction is the opposite of the original
        /// </returns>
        public static TileFindDirection Invert(this TileFindDirection direction)
        {
            switch (direction)
            {
                case TileFindDirection.Left: return TileFindDirection.Right;
                case TileFindDirection.Right: return TileFindDirection.Left;
                case TileFindDirection.Top: return TileFindDirection.Bottom;
                case TileFindDirection.Bottom: return TileFindDirection.Top;
                default:
                    throw new NotSupportedException();
            }
        }
        
    }

}