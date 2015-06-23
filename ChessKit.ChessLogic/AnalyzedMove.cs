using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    public abstract class AnalyzedMove
    {
        protected AnalyzedMove(MoveAnnotations annotations)
        {
            Annotations = annotations;
        }

        /// Annotations (capture, promotion attempt, etc.) to the move
        public MoveAnnotations Annotations { get; }
    }
}