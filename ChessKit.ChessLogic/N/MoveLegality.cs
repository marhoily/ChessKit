using System;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.N
{
    public static class MoveLegality
    {
        public static AnalyzedMove Validate(this Position position, MoveR move)
        {
            return BoardUpdater.MakeMove(position.ToBoard(), move);
        }

        public static LegalMove ValidateLegal(this Position position, MoveR move)
        {
            var analyzedMove = BoardUpdater.MakeMove(position.ToBoard(), move);
            var legalMove = analyzedMove as LegalMove;

            if (legalMove != null)
            {
                return legalMove;
            }
            throw new Exception(analyzedMove.Annotations.ToString());
        }

        public static LegalMove ToLegalMove(this Board nextBoard, Board prevBoard)
        {
            var validateLegal = nextBoard.FromBoard().Core;
            var move = nextBoard.PreviousMove;
            if ((move.Annotations & MoveAnnotations.AllErrors) != 0)
                throw new Exception(move.Annotations.ToString());
            var moveR = new MoveR(move.From, move.To, move.ProposedPromotion);
            var position = new Position(validateLegal, 0, 1, GameStates.None, null);
            var flags = (int) move.Annotations;
            return new LegalMove(moveR, prevBoard.FromBoard(),
                position.Core, PieceType.None,
                move.Annotations);

        }
    }
}