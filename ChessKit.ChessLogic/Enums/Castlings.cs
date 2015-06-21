using System;

namespace ChessKit.ChessLogic.Enums
{
    [Flags]
    public enum Castlings
    {
        None = 0,
        BK = 0x080,
        BQ = 0x100,
        WK = 0x200,
        WQ = 0x400,

        White = WK | WQ,
        Black = BK | BQ,

        All = BK | BQ | WK | WQ
    }
}