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
    }
}