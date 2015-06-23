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
            var tempPosition = new Position(core, 0, 0, 0, legalMove);

            var newHalfMoveClock = 
                piece == PieceType.Pawn || (obs & Capture) != 0
                ? 0 : prev.HalfMoveClock + 1;

            var newMoveNumber =
                prev.FullMoveNumber + (color == Color.Black ? 1 : 0);

            var isCheck = core.IsInCheck(core.ActiveColor);
            var noMoves = tempPosition.GetAllLegalMoves().Count == 0;

            var newState = default(GameStates);
            if (isCheck && noMoves) newState |= GameStates.Mate;
            if (isCheck && !noMoves) newState |= GameStates.Check;
            if (!isCheck && noMoves) newState |= GameStates.Stalemate;

            return new Position(core, newHalfMoveClock, 
                newMoveNumber, newState, legalMove);
        }
    }
}
