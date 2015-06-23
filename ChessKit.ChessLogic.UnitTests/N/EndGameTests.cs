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
            Play(Board.StartPosition, "g1-f3")
                .HalfMoveClock.Should().Be(1);

        [Fact]
        public void Fifty_moves_rule_clock_resets_after_pawn_advance() =>
            Play(Board.StartPosition, "g1-f3", "e7-e5")
                .HalfMoveClock.Should().Be(0);

        [Fact]
        public void Fifty_moves_rule_clock_resets_after_pawn_capture() =>
            Play(Board.StartPosition, 
                "e2-e4", "d7-d5", "g1-f3","d5-e4")
                .HalfMoveClock.Should().Be(0);

        [Fact]
        public void Fifty_moves_rule_clock_resets_after_capture() =>
            Play(Board.StartPosition, 
                "e2-e4", "d7-d5", "e4-d5", "d8-d5")
                .HalfMoveClock.Should().Be(0);

        [Fact]
        public void Full_moves_clock_does_not_increment_after_white_move() =>
            Play(Board.StartPosition, "e2-e4")
                .FullMoveNumber.Should().Be(1);

        [Fact]
        public void Full_moves_clock_does_increment_after_black_move() =>
            Play("7K/5n2/4b3/8/8/8/7k/8 w - - 49 1", "h8-g7", "f7-g5")
                .Properties.ToString()
                .Should().Be("FiftyMoveRule");

        [Fact]
        public void GetHistory()
        {
            const int a = unchecked((int)0xa221200a);
            const int b = unchecked((int)0x1643801d);
            const int c = unchecked((int)0x35098444);
            const int d = unchecked((int)0x88813080);
            const int s = unchecked((int)0xa0948504); 
            Play("r3qr1k/p1p1b3/4pnQp/3p4/8/2NB4/PPP2PPP/R5K1 w - - 0 17",
                "g6-h6", "h8-g8", "h6-g5", "g8-h8",
                "g5-h6", "h8-g8", "h6-g5", "g8-h8",
                "g5-h6")
                .GetHistory().Select(p => p.Core.GetHashCode())
                .Should().Equal(a, b, c, d, a, b, c, d, s);
        }

        [Fact]
        public void Draw_By_Repetition() =>
            Play("r3qr1k/p1p1b3/4pnQp/3p4/8/2NB4/PPP2PPP/R5K1 w - - 0 17",
                "g6-h6", "h8-g8", "h6-g5", "g8-h8",
                "g5-h6", "h8-g8", "h6-g5", "g8-h8",
                "g5-h6")
                .Properties.ToString()
                .Should().Be("Check, Repetition");

        [Fact]
        public void PositionCore_Structural_Equality_Should_Work()
        {
            const string fen = "r2q1r1k/p1p1b3/4pnQp/3p4/8/2NB4/PPP2PPP/R5K1 b - - 3 16";
            var c1 = fen.ParseFen().Core;
            var c2 = fen.ParseFen().Core;
            c1.Should().Be(c2);
            c1.GetHashCode().Should().Be(c2.GetHashCode());
        }

        private static void CheckProperties(string fen, string move, string expectedProperties)
            => fen.ParseFen()
                .ValidateLegal(MoveR.Parse(move))
                .ToPosition()
                .Properties.ToString()
                .Should().Be(expectedProperties);

        private static Position Play(string startPositionFen, params string[] moves)
            => Play(startPositionFen.ParseFen(), moves);
        private static Position Play(Position startPosition, params string[] moves)
            => moves.Aggregate(
                startPosition,
                (position, nextMove) => position
                    .ValidateLegal(MoveR.Parse(nextMove))
                    .ToPosition());
    }
}
