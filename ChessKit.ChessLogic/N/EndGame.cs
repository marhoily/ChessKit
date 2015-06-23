using ChessKit.ChessLogic.Primitives;
using static ChessKit.ChessLogic.Primitives.MoveAnnotations;

namespace ChessKit.ChessLogic.N
{
    public static class EndGame
    {
        public static Position ToPosition(this LegalMove legalMove)
        {
            var core = legalMove.ResultPosition;
            var prev = legalMove.OriginalPosition;
            var piece = legalMove.Piece;
            var obs = legalMove.Annotations;
            var color = prev.Core.ActiveColor;

            var newHalfMoveClock = 
                piece == PieceType.Pawn || (obs & Capture) != 0
                ? 0 : prev.HalfMoveClock + 1;

            var newMoveNumber =
                prev.FullMoveNumber + (color == Color.Black ? 1 : 0);
            return new Position(core, newHalfMoveClock, 
                newMoveNumber, GameStates.None, legalMove);
        }
    }
}
