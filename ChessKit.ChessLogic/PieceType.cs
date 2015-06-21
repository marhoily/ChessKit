using System.Diagnostics.CodeAnalysis;

namespace ChessKit.ChessLogic
{
    [SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags",
        Justification = "It really is not flags")]
    public enum PieceType
    {
        None,
        Pawn = MoveAnnotations.Pawn,
        Bishop = MoveAnnotations.Bishop,
        Knight = MoveAnnotations.Knight,
        Rook = MoveAnnotations.Rook,
        Queen = MoveAnnotations.Queen,
        King = MoveAnnotations.King
    }
}