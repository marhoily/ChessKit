using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

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
        public static void AssertLegalMoves(this Bitboard bitboard, ulong fromBit, params ulong[] legalMoves)
        {
            var actual = new HashSet<string>();
            for (var i = 0; i < 64; i++)
                if (bitboard.IsLegalMove(fromBit, 1ul << i))
                    actual.Add(i.ToBitboardPositionString());
            var expected = legalMoves.Select(m => m.FindFirstBit().ToBitboardPositionString());
            actual.Should().BeEquivalentTo(expected);
        }
        public static void AssertLegalMoves(this Bitboard bitboard, int fromSquare, params int[] legalMoves)
        {
            var actual = new HashSet<int>();
            for (var i = 0; i < 64; i++)
                if (bitboard.IsLegalMove(fromSquare, i))
                    actual.Add(i);
            if (!actual.SetEquals(legalMoves))
            {
                Console.WriteLine("expected:");
                Console.WriteLine();
                Print(new HashSet<int>(legalMoves));
                Console.WriteLine();
                Console.WriteLine("Actual:");
                Console.WriteLine();
                Print(actual);
                Assert.Fail();

            }
            
        }

        private static void Print(HashSet<int> actual)
        {

            for (var i = 7; i >= 0; i--)
            {
                Console.WriteLine(string.Join(" ",
                    Enumerable.Range(0, 8) 
                    .Select(j => actual.Contains(i*8 + j) ? "*" : ".")));
            }
        }
    }
}