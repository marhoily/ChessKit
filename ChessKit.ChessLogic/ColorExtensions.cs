namespace ChessKit.ChessLogic
{
    /// <summary>Represents piece Type</summary>
    public static class ColorExtensions
    {
        public static Color Invert(this Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }
    }
}