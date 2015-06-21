using ChessKit.ChessLogic.Enums;

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
    }
}