using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.Algorithms
{
    public static partial class Attacks
    {
        public static bool IsSquareAttackedBySide(this PositionCore core, Color side, int square)
        {
            return side == Color.White
                ? IsSquareAttackedByWhite(core.Squares, square)
                : IsSquareAttackedByBlack(core.Squares, square);
        }

        public static bool IsInCheck(this PositionCore core, Color side)
        {
            return side == Color.White
                ? IsSquareAttackedByBlack(core.Squares, core.WhiteKing)
                : IsSquareAttackedByWhite(core.Squares, core.BlackKing);
        }
    }
}
