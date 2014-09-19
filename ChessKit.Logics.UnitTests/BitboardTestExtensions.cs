using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace ChessKit.ChessLogic.UnitTests
{
    public static class BitboardTestExtensions
    {
        public static string ToBitboardPositionString(this int i)
        {
            return (char) ('A' + i%8) + (i/8 + 1).ToString();
        }
        public static int FindFirstBit(this UInt64 pos)
        {
            for (var i = 0; i < 64; i++)
                if ((pos >> i) == 1)
                    return i;
            return -1;
        }
        public static void AssertLegalMoves(this Bitboard bitboard, ulong @from, params ulong[] legalMoves)
        {
            var actual = new HashSet<string>();
            for (var i = 0; i < 64; i++)
                if (bitboard.IsLegalMove(@from, 1ul << i))
                    actual.Add(i.ToBitboardPositionString());
            var expected = legalMoves.Select(m => m.FindFirstBit().ToBitboardPositionString());
            actual.Should().BeEquivalentTo(expected);
        }
    }
}