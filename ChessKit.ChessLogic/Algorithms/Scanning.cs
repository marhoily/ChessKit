using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.Algorithms
{
    public static partial class Attacks
    {
        public static bool IsAttackedBy(this PositionCore core, Color side, int square)
        {
            return side == Color.White
                ? IsAttackedByWhite(core.Squares, square)
                : IsAttackedByBlack(core.Squares, square);
        }

        public static bool IsInCheck(this PositionCore core, Color side)
        {
            return side == Color.White
                ? IsAttackedByBlack(core.Squares, core.WhiteKing)
                : IsAttackedByWhite(core.Squares, core.BlackKing);
        }
    }
}
