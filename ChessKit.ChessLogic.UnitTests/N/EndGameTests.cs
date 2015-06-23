using System.Linq;
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
        [Fact]
        public void GivesStalemate() => CheckProperties(
            "7k/7P/8/7K/8/8/8/8 w - - 0 1", "h5-h6", "Stalemate");

        [Fact]
        public void Fifty_moves_rule_clock_increments_after_move() =>
            Play(Board.StartPosition.FromBoard(), "g1-f3")
                .HalfMoveClock.Should().Be(1);

        [Fact]
        public void Fifty_moves_rule_clock_resets_after_pawn_advance() =>
            Play(Board.StartPosition.FromBoard(), "g1-f3", "e7-e5")
                .HalfMoveClock.Should().Be(0);

        [Fact]
        public void Fifty_moves_rule_clock_resets_after_pawn_capture() =>
            Play(Board.StartPosition.FromBoard(), 
                "e2-e4", "d7-d5", "g1-f3","d5-e4")
                .HalfMoveClock.Should().Be(0);

        [Fact]
        public void Fifty_moves_rule_clock_resets_after_capture() =>
            Play(Board.StartPosition.FromBoard(), 
                "e2-e4", "d7-d5", "e4-d5", "d8-d5")
                .HalfMoveClock.Should().Be(0);

        private static void CheckProperties(string fen, string move, string expectedProperties)
            => fen.ParseFen().FromBoard()
                .ValidateLegal(MoveR.Parse(move))
                .ToPosition()
                .Properties.ToString()
                .Should().Be(expectedProperties);

        private static Position Play(Position startPosition, params string[] moves)
            => moves.Aggregate(
                startPosition,
                (position, nextMove) => position
                    .ValidateLegal(MoveR.Parse(nextMove))
                    .ToPosition());
    }
}
