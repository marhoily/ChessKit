using System;
using System.Linq;
using ChessKit.ChessLogic.Algorithms;
using ChessKit.ChessLogic.Primitives;
using FluentAssertions;
using Xunit;

namespace ChessKit.ChessLogic.UnitTests
{
    public class GetLegalMovesTests
    {
        static void Check(string fen, string moveFrom, string[] expected)
        {
            try
            {
                var f = moveFrom.ParseCoordinate();
                var position = fen.ParseFen();
                var actual = position
                    .GetLegalMovesFromSquare(f)
                    .Select(m => m.Move.ToCell.ToCoordinateString())
                    .OrderBy(s => s);
                actual.Should().Equal(expected.OrderBy(s => s));
                Coordinates.All
                    .Select(t => position.Validate(new Move(f, t)))
                    .OfType<LegalMove>()
                    .Select(m => m.Move.ToCell.ToCoordinateString())
                    .OrderBy(s => s)
                    .Should().Equal(actual);
            }
            finally
            {
                Console.WriteLine(fen.ParseFen().Dump());
            }
        }

        static void CheckAll(string fen, string[] expected)
        {
            try
            {
                var position = fen.ParseFen();
                var actual = position
                    .GetAllLegalMoves()
                    .Select(m => m.Move.ToCell.ToCoordinateString())
                    .OrderBy(s => s);
                actual.Should().Equal(expected.OrderBy(s => s));
                Coordinates.All
                    .Select(f => Coordinates.All.Select(t => new Move(f, t)))
                    .SelectMany(r => r)
                    .Select(moveR => position.Validate(moveR))
                    .OfType<LegalMove>()
                    .Select(m => m.Move.ToCell.ToCoordinateString())
                    .OrderBy(s => s)
                    .Should().Equal(actual);
            }
            finally
            {
                Console.WriteLine(fen.ParseFen().Dump());
            }

        }

        [Fact]
        public void empty_square() => Check(
            "8/8/8/8/8/8/8/8 w - - 0 1", "e4", new string[0]);

        [Fact]
        public void Rook() => Check(
            "8/2N5/8/8/8/2R2q2/8/8 w - - 0 1", "c3",
            new[] {"a3", "b3", "c1", "c2", "c4", "c5",
                "c6", "d3", "e3", "f3"});

        [Fact]
        public void Queen() => Check(
            "8/8/2n5/4P3/8/2q2n2/8/8 b - - 0 1", "c3",
            new[] { "a3", "b3", "c1", "c2", "c4", "c5", "d3", "e3",
                "a1", "b2", "d4", "e5", "a5", "b4", "d2", "e1" });

        [Fact]
        public void King() => Check(
            "8/8/8/8/4K3/8/8/8 w - - 0 1", "e4",
            new[] { "d3", "d4", "d5", "e3", "e5", "f3", "f4", "f5" });

        [Fact]
        public void Knight() => Check(
            "8/8/8/8/4n3/8/8/8 b - - 0 1", "e4",
            new[] { "d2", "c3", "c5", "d6", "f6", "g5", "g3", "f2" });

        [Fact]
        public void All() => CheckAll(
            "8/8/8/3pp3/3PP3/8/8/8 w - - 0 2", new[] { "e5", "d5" });

        // ---------------- White Pawn ----------------
        [Fact]
        public void white_pawn_h6_h7() => Check(
            "8/8/7P/8/8/8/8/8 w - - 0 1", "h6", new[] { "h7" });

        [Fact]
        public void white_pawn_h7_h8_Q() => Check(
            "8/7P/8/8/8/8/8/8 w - - 0 1", "h7", new[] { "h8" });

        [Fact]
        public void white_pawn_e2_e3_e2_e4() => Check(
            "8/8/8/8/8/8/4P3/8 w - - 0 1", "e2", new[] { "e3", "e4" });

        [Fact]
        public void white_pawn_e7_d8_Q_capture() => Check(
            "3qr3/4P3/8/8/8/8/8/8 w - - 0 1", "e7", new[] { "d8" });

        [Fact]
        public void white_pawn_e7_f8_Q_capture() => Check(
            "4rr2/4P3/8/8/8/8/8/8 w - - 0 1", "e7", new[] { "f8" });

        [Fact]
        public void white_pawn_e2_d3_capture() => Check(
            "8/8/8/8/8/3qr3/4P3/8 w - - 0 1", "e2", new[] { "d3" });

        [Fact]
        public void white_pawn_e2_f3_capture() => Check(
            "8/8/8/8/8/4rr2/4P3/8 w - - 0 1", "e2", new[] { "f3" });

        // ---------------- Black Pawn ----------------
        [Fact]
        public void black_pawn_h3_h2() => Check(
            "8/8/8/8/8/7p/8/8 b - - 0 1", "h3", new[] { "h2" });

        [Fact]
        public void black_pawn_h2_h1_Q() => Check(
            "8/8/8/8/8/8/7p/8 b - - 0 1", "h2", new[] { "h1" });

        [Fact]
        public void black_pawn_e7_e6_e7_e5() => Check(
            "8/4p3/8/8/8/8/8/8 b - - 0 1", "e7", new[] { "e6", "e5" });

        [Fact]
        public void black_pawn_e2_d1_Q_capture() => Check(
            "8/8/8/8/8/8/4p3/3RB3 b - - 0 1", "e2", new[] { "d1" });

        [Fact]
        public void black_pawn_e2_f1_Q_capture() => Check(
            "8/8/8/8/8/8/4p3/4NQ2 b - - 0 1", "e2", new[] { "f1" });

        [Fact]
        public void black_pawn_e7_d6_capture() => Check(
            "8/4p3/3BN3/8/8/8/8/8 b - - 0 1", "e7", new[] { "d6" });

        [Fact]
        public void black_pawn_e7_f6_capture() => Check(
            "8/4p3/4RR2/8/8/8/8/8 b - - 0 1", "e7", new[] { "f6" });

        // ---------------- Bishop ----------------
        [Fact]
        public void bishop_a1_b2()
            => Check("8/8/8/8/8/2P5/8/B7 w - - 0 1", "a1", new[] { "b2" });

        [Fact]
        public void bishop_a8_b7()
            => Check("b7/8/2p5/8/8/8/8/8 b - - 0 1", "a8", new[] { "b7" });

        [Fact]
        public void bishop_h8_g7()
            => Check("7b/8/5p2/8/8/8/8/8 b - - 0 1", "h8", new[] { "g7" });

        [Fact]
        public void bishop_h1_g2()
            => Check("8/8/8/8/8/5P2/8/7B w - - 0 1", "h1", new[] { "g2" });

        [Fact]
        public void bishop_h1_f3() => Check(
            "8/8/8/8/4p3/8/8/7b b - - 0 1", "h1", new[] { "g2", "f3" });

        [Fact]
        public void bishop_g1_h2_g1_f2() => Check(
            "8/8/8/8/8/4p3/8/6b1 b - - 0 1", "g1", new[] { "h2", "f2" });

        [Fact]
        public void bishop_captures_h1_g2() => Check(
            "8/8/8/8/8/8/6B1/7b b - - 0 1", "h1", new[] { "g2" });
    }
}