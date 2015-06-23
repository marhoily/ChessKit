using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.N
{
    public static class Scanning2
    {
        public static bool IsAttackedBy(this PositionCore core, Color side, int square)
        {
            return side == Color.White
                ? Scanning.IsAttackedByWhite(core.Squares, square)
                : Scanning.IsAttackedByBlack(core.Squares, square);
        }

        public static bool IsInCheck(this PositionCore core, Color side)
        {
            return side == Color.White
                ? Scanning.IsAttackedByBlack(core.Squares, -1)
                : Scanning.IsAttackedByWhite(core.Squares, -1);
        }
    }
}
