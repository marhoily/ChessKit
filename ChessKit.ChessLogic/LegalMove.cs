using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    /// Represents legal move as returned by the legality check
    public sealed class LegalMove : AnalyzedMove
    {
        /// The move that was checked for the legality
        public Move Move { get; }

        /// The position in which the move was checked
        public Position OriginalPosition { get; }

        /// The core of the position gotten as a result of the move
        /// (use legalMove.ToPosition() method to get full position)
        public PositionCore ResultPosition { get; }

        /// The piece type that was moved
        public PieceType Piece { get; }

        /// The castling, if the move was castling, -or- None
        public Castlings Castling { get; }

        /// Warnings to the move
        public MoveWarnings Warnings { get; }

        internal LegalMove(Move move, 
            Position originalPosition, PositionCore resultPosition, 
            PieceType piece, MoveAnnotations annotations)
            : base(annotations)
        {
            Move = move;
            OriginalPosition = originalPosition;
            ResultPosition = resultPosition;
            Piece = piece;
            Castling = Castlings.All | (Castlings) annotations;
            Warnings = MoveWarnings.All | (MoveWarnings) annotations;
        }
    }
}