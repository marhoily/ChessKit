using System.Diagnostics.CodeAnalysis;

namespace ChessKit.ChessLogic
{
  [SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags",
    Justification = "It really is not flags")]
  public enum PieceType
  {
    None,
    Pawn   = MoveHints.Pawn,
    Bishop = MoveHints.Bishop ,
    Knight = MoveHints.Knight ,
    Rook   = MoveHints.Rook   ,
    Queen  = MoveHints.Queen  ,
    King   = MoveHints.King   ,
  }
}
