using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    public abstract class AnalyzedMove
    {
        internal AnalyzedMove(MoveAnnotations annotations)
        {
            Annotations = annotations;
        }

        /// Annotations (capture, promotion attempt, etc.) to the move
        internal MoveAnnotations Annotations { get; }

        /// The castling, if the move was castling attempt, -or- None
        public Castlings Castling => Castlings.All & (Castlings)Annotations;

        /// Warnings to the move
        public MoveWarnings Warnings => MoveWarnings.All & (MoveWarnings)Annotations;

    }
}