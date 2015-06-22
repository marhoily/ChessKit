using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.N
{
    public static class Scanning
    {
        public static bool IsAttackedBy(this PositionCore core, Color side, int square)
        {
            return core.ToBoard().IsAttackedBy(side, square);
        }
        public static bool IsInCheck(this PositionCore core, Color side)
        {
            return core.ToBoard().IsInCheck(side);
        }
    }
}
