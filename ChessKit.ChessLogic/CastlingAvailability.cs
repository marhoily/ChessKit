using System;

namespace ChessKit.ChessLogic
{
  [Flags]
  public enum CastlingAvailability
  {
    None,

    WhiteKing = MoveAnnotations.WhiteKingsideCastling,
    BlackKing = MoveAnnotations.BlackKingsideCastling,
    WhiteQueen = MoveAnnotations.WhiteQueensideCastling,
    BlackQueen = MoveAnnotations.BlackQueensideCastling,

    White = WhiteKing | WhiteQueen,
    Black = BlackKing | BlackQueen,
  }
}