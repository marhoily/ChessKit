using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    /// Represents legal move as returned by the legality check
    public sealed class LegalMove : AnalyzedMove
    {
        /// The core of the position gotten as a result of the move
        /// (use legalMove.ToPosition() method to get full position)
        public PositionCore ResultPosition { get; }

        internal LegalMove(Move move, Position originalPosition,
            PositionCore resultPosition, MoveAnnotations annotations)
            : base(annotations, originalPosition, move)
        {
            ResultPosition = resultPosition;
        }
    }
}