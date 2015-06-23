using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    /// Represents an illegal move as returned by the legality check
    public sealed class IllegalMove : AnalyzedMove
    {
        /// The move that was checked for the legality
        public Move Move { get; }

        /// The position in which the move was checked
        public Position OriginalPosition { get; }

        /// The piece type that was moved
        public PieceType Piece { get; }

        /// Non-empty set of the errors to the move
        public MoveErrors Errors { get; }

        internal IllegalMove(Move move, Position originalPosition,
            PieceType piece, MoveAnnotations annotations)
            : base(annotations)
        {
            Move = move;
            OriginalPosition = originalPosition;
            Piece = piece;
            Errors = MoveErrors.All & (MoveErrors) annotations;
        }
    }
}