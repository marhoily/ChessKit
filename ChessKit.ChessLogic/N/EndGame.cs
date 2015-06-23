using ChessKit.ChessLogic.Primitives;
using static ChessKit.ChessLogic.Primitives.MoveAnnotations;
using static ChessKit.ChessLogic.Primitives.GameStates;

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
            if (isCheck && noMoves) newState |= Mate;
            if (isCheck && !noMoves) newState |= Check;
            if (!isCheck && noMoves) newState |= Stalemate;
            if (newHalfMoveClock >= 50) newState |= FiftyMoveRule;

            return new Position(core, newHalfMoveClock, 
                newMoveNumber, newState, legalMove);
        }
    }
}
