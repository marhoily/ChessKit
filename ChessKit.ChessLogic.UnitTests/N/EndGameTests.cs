using ChessKit.ChessLogic.N;
using FluentAssertions;
using Xunit;

namespace ChessKit.ChessLogic.UnitTests.N
{
    public sealed class EndGameTests
    {
        [Fact]
        public void GivesCheck() => CheckProperties(
            "8/2Rk4/1q4BP/8/8/6K1/8/8 b - - 24 119", "b6-c7", "Check");
        [Fact]
        public void GivesMate() => CheckProperties(
            "2K5/8/2k4r/8/8/8/8/8 b - - 0 9", "h6-h8", "Mate");

        private static void CheckProperties(string fen, string move, string expectedProperties)
        {
            fen.ParseFen().FromBoard()
                .ValidateLegal(MoveR.Parse(move))
                .ToPosition()
                .Properties.ToString()
                .Should().Be(expectedProperties);
        }
    }
}
