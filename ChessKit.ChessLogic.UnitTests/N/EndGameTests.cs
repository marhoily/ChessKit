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
