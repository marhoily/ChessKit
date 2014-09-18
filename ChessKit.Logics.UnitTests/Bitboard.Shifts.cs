using System;
using U64 = System.UInt64;

namespace ChessKit.ChessLogic.UnitTests
{
   partial class Bitboard
    {
      
        const UInt64 NotAFile = 0xfefefefefefefefe; // ~0x0101010101010101
        const UInt64 NotHFile = 0x7f7f7f7f7f7f7f7f; // ~0x8080808080808080
        static U64 SoutOne(U64 b) { return b >> 8; }
        static U64 NortOne(U64 b) { return b << 8; }
        static U64 EastOne(U64 b) { return (b << 1) & NotAFile; }
        static U64 NoEaOne(U64 b) { return (b << 9) & NotAFile; }
        static U64 SoEaOne(U64 b) { return (b >> 7) & NotAFile; }
        static U64 WestOne(U64 b) { return (b >> 1) & NotHFile; }
        static U64 SoWeOne(U64 b) { return (b >> 9) & NotHFile; }
        static U64 NoWeOne(U64 b) { return (b << 7) & NotHFile; }
}
}
