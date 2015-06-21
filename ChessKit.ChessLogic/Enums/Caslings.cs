using System;

namespace ChessKit.ChessLogic.Enums
{
    [Flags]
    public enum Caslings
    {
        None,

        WhiteKing = MoveAnnotations.WK,
        BlackKing = MoveAnnotations.BK,
        WhiteQueen = MoveAnnotations.WQ,
        BlackQueen = MoveAnnotations.BQ,

        White = WhiteKing | WhiteQueen,
        Black = BlackKing | BlackQueen
    }
}