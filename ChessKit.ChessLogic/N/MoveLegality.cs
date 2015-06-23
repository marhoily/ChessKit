using System;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.N
{
    public static class MoveLegality
    {
        public static AnalyzedMove Validate(this Position position, MoveR move)
        {
            return BoardUpdater.MakeMove(position.ToBoard(), move);
            /*var move1 = new Move(move.From, move.To, move.ProposedPromotion);
            var prevBoard = position.ToBoard();
            var makeMove = prevBoard.MakeMove(move1);
            var annotations = makeMove.PreviousMove.Annotations;
            var flags = (int)annotations;
            if ((annotations & MoveAnnotations.AllErrors) != 0)
                return new IllegalMove(move, position,
                    PieceType.None,
                    annotations);

            return new LegalMove(move, position,
                position.Core, PieceType.None,
                annotations);*/
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