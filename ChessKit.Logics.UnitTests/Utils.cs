using System;
using System.Linq;

namespace ChessKit.ChessLogic.UnitTests
{
    public static class Utils
    {
        public static UInt64 GetMask(params int[] squares)
        {
            return squares.Aggregate<int, ulong>(0, 
                (current, square) => current | (1ul << square));
        }
    }
}