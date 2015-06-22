using System;

namespace ChessKit.ChessLogic.Enums
{
    /// <summary>Represents chess piece/player's side color</summary>
    public enum Color
    {
        White = 0,
        Black = 1,

        All = White
            | Black
    }
}