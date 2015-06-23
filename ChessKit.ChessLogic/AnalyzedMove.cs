using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    public abstract class AnalyzedMove
    {
        /// The move that was checked for the legality
        public Move Move { get; }

        /// The position in which the move was checked
        public Position OriginalPosition { get; }

        internal AnalyzedMove(MoveAnnotations annotations, Position originalPosition, Move move)
        {
            Annotations = annotations;
            OriginalPosition = originalPosition;
            Move = move;
        }

        /// Annotations (capture, promotion attempt, etc.) to the move
        internal MoveAnnotations Annotations { get; }

        /// The castling, if the move was castling attempt, -or- None
        public Castlings Castling => Castlings.All & (Castlings)Annotations;

        /// Warnings to the move
        public MoveWarnings Warnings => MoveWarnings.All & (MoveWarnings)Annotations;
        /// The piece type that was moved
        public PieceType Piece => PieceType.All & (PieceType)Annotations;

    }
}