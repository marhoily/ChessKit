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

        /// The castling, if the move was castling attempt, -or- None
        public Castlings Castling { get; }

        /// Warnings to the move
        public MoveWarnings Warnings { get; }

        /// Non-empty set of the errors to the move
        public MoveErrors Errors { get; }

        public IllegalMove(Move move, Position originalPosition,
            PieceType piece, MoveAnnotations annotations)
            : base(annotations)
        {
            Move = move;
            OriginalPosition = originalPosition;
            Piece = piece;
            Castling = Castlings.All & (Castlings) annotations;
            Warnings = MoveWarnings.All & (MoveWarnings) annotations;
            Errors = MoveErrors.All & (MoveErrors) annotations;
        }
    }
}