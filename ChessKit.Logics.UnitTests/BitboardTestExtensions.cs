using System;
using FluentAssertions;

namespace ChessKit.ChessLogic.UnitTests
{
    public static class BitboardTestExtensions
    {
        public static void AssertLegalMoves(this Bitboard bitboard, ulong @from, params ulong[] legalMoves)
        {
            for (var i = 0; i < 64; i++)
            {
                var pos = (1ul << i);
                bitboard.IsLegalMove(@from, pos)
                    .Should().Be(Array.IndexOf(legalMoves, pos) != -1);
            }
        }
    }
}