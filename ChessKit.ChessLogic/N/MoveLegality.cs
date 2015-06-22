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
            return makeMove.PreviousMove.Annotations;
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