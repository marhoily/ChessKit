using System;
using ChessKit.ChessLogic.Algorithms;
using FluentAssertions;
using Xunit;

namespace ChessKit.ChessLogic.UnitTests
{
    public sealed class FenTests
    {
        private static void Check(string fen)
        {
            fen.ParseFen().PrintFen().Should().Be(fen);
        }
        [Fact]
        public void Works_With_Starting_Position()
        {
            Check("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        }
        [Fact]
        public void Works_With_EnPassant_For_Black()
        {
            Check("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1");
        }
        [Fact]
        public void Fails_With_Invalid_String()
        {
            Assert.Throws<Exception>(() => "x w - e3 0 1".ParseFen());
        }
    }
}
