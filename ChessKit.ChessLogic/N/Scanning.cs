using ChessKit.ChessLogic.Enums;

namespace ChessKit.ChessLogic.N
{
    public static class Scanning
    {
        public static bool IsAttackedBy(this PositionCore core, Color side, int square)
        {
            return core.ToBoard().IsAttackedBy(side, square);
        }
    }
}
