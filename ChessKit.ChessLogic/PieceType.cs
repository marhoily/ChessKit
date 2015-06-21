using System.Diagnostics.CodeAnalysis;

namespace ChessKit.ChessLogic
{
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