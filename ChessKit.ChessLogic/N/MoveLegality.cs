using System;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.N
{
    public static class MoveLegality
    {
        public static MoveAnnotations Validate(this Position position, MoveR move)
        {
            var move1 = new Move(move.From, move.To, move.ProposedPromotion);
            var makeMove = position.ToBoard().MakeMove(move1);
            var annotations = makeMove.PreviousMove.Annotations;

            return annotations;
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
                Castlings.All | (Castlings) flags,
                move.Annotations,
                MoveWarnings.All | (MoveWarnings) flags);

        }
        public static Position ValidateLegal(this Position position, MoveR move)
        {
            var move1 = new Move(move.From, move.To, move.ProposedPromotion);
            var makeMove = position.ToBoard().MakeMove(move1);
            var validateLegal = makeMove.FromBoard().Core;
            if ((makeMove.PreviousMove.Annotations & MoveAnnotations.AllErrors) != 0)
                throw new Exception(makeMove.PreviousMove.Annotations.ToString());
            return new Position(validateLegal, 0, 1, GameStates.None, null);
        }
    }
}