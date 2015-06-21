using System;

namespace ChessKit.ChessLogic.Enums
{
    [Flags]
    public enum Caslings
    {
        None,

        WhiteKing = MoveAnnotations.WhiteKingsideCastling,
        BlackKing = MoveAnnotations.BlackKingsideCastling,
        WhiteQueen = MoveAnnotations.WhiteQueensideCastling,
        BlackQueen = MoveAnnotations.BlackQueensideCastling,

        White = WhiteKing | WhiteQueen,
        Black = BlackKing | BlackQueen
    }
}