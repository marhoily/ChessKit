using System;

namespace ChessKit.ChessLogic.Enums
{
    [Flags]
    public enum MoveInfo 
    {
        None = 0,

        /// <summary> It was a pawn move to the last rank </summary>
        Promotion = MoveAnnotations.Promotion,

        /// <summary> It captured the opponents piece </summary>
        Capture = MoveAnnotations.Capture,

        /// <summary> It was a pawn move to capture en passant </summary>
        EnPassant = MoveAnnotations.EnPassant,

        /// <summary> It was the pawn move 2 squares ahead from the original rank </summary>
        DoublePush = MoveAnnotations.PawnDoublePush
    }
}