using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    /// Represents an illegal move as returned by the legality check
    public sealed class IllegalMove : AnalyzedMove
    {
        /// Errors to the move
        public MoveErrors Errors => MoveErrors.All & (MoveErrors)Annotations;

        internal IllegalMove(Move move, Position originalPosition, MoveAnnotations annotations)
            : base(annotations, originalPosition, move)
        {
        }
    }
}