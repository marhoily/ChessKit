using static ChessKit.ChessLogic.Enums.Color;
using static ChessKit.ChessLogic.Enums.PieceType;

namespace ChessKit.ChessLogic.Enums
{
  public enum Piece : byte
  {
    EmptyCell,

    WhitePawn   = White | Pawn,
    WhiteBishop = White | Bishop,
    WhiteKnight = White | Knight,
    WhiteRook   = White | Rook,
    WhiteQueen  = White | Queen,
    WhiteKing   = White | King,
    
    BlackPawn   = Black | Pawn,
    BlackBishop = Black | Bishop,
    BlackKnight = Black | Knight,
    BlackRook   = Black | Rook,
    BlackQueen  = Black | Queen,
    BlackKing   = Black | King
  }
}                                                    