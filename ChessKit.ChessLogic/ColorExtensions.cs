using System;

namespace ChessKit.ChessLogic
{
    /// <summary>Represents piece Type</summary>
    public static class ColorExtensions
    {
        public static Color Invert(this Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }
        public static Color ParseColor(this char symbol)
        {
            switch (symbol)
            {
                case 'W':
                case 'w':
                    return ChessLogic.Color.White;
                case 'B':
                case 'b':
                    return ChessLogic.Color.Black;
                default:
                    throw new FormatException("What color is that?");
            }
        }

    }
}