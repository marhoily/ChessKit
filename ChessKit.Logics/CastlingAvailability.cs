using System;

namespace ChessKit.ChessLogic
{
  [Flags]
  public enum CastlingAvailability
  {
    None,

    WhiteKing = MoveHints.WhiteKingsideCastling,
    BlackKing = MoveHints.BlackKingsideCastling,
    WhiteQueen = MoveHints.WhiteQueensideCastling,
    BlackQueen = MoveHints.BlackQueensideCastling,

    White = WhiteKing | WhiteQueen,
    Black = BlackKing | BlackQueen,
  }
}