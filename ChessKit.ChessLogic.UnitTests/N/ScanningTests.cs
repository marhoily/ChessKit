using ChessKit.ChessLogic.Enums;
using ChessKit.ChessLogic.N;
using FluentAssertions;
using Xunit;

namespace ChessKit.ChessLogic.UnitTests.N
{
    public sealed class ScanningTests
    {
        private static void IsAttackedBy(string fen, string square, Color color, bool expected)
            => fen.ParseFen().Core
                .IsAttackedBy(color, square.ParseCoordinate())
                .Should().Be(expected);

        [Fact]
        public void C6_is_attacked_by_black_pawn_on_b7()
            => IsAttackedBy("8/1p6/8/8/8/8/8/8 w - - 0 1", "c6", Color.Black, true);

        [Fact]
        public void a6_is_attacked_by_black_pawn_on_b7()
            => IsAttackedBy("8/1p6/8/8/8/8/8/8 w - - 0 1", "a6", Color.Black, true);

        [Fact]
        public void when_check_if_a8_is_attacked_it_does_not_overflow()
            => IsAttackedBy("8/8/8/8/8/8/8/8 w - - 0 1", "a8", Color.Black, false);

        [Fact]
        public void h8_is_attacked_by_black_bishop_on_d4()
            => IsAttackedBy("8/8/8/8/3b4/8/8/8 w - - 0 1", "h8", Color.Black, true);

        [Fact]
        public void h8_is_NOT_attacked_by_black_bishop_on_d4_because_its_masked_by_the_pawn_on_f6()
            => IsAttackedBy("8/8/5P2/8/3b4/8/8/8 w - - 0 1", "h8", Color.Black, false);

        [Fact]
        public void a7_is_attacked_by_black_bishop_on_d4()
            => IsAttackedBy("8/8/8/8/3b4/8/8/8 w - - 0 1", "a7", Color.Black, true);

        [Fact]
        public void a1_is_attacked_by_black_bishop_on_d4()
            => IsAttackedBy("8/8/8/8/3b4/8/8/8 w - - 0 1", "a1", Color.Black, true);

        [Fact]
        public void f2_is_attacked_by_black_bishop_on_d4()
            => IsAttackedBy("8/8/8/8/3b4/8/8/8 w - - 0 1", "f2", Color.Black, true);

        [Fact]
        public void c2_is_attacked_by_black_knight_on_d4()
            => IsAttackedBy("8/8/8/8/3n4/8/8/8 w - - 0 1", "c2", Color.Black, true);

        [Fact]
        public void b3_is_attacked_by_black_knight_on_d4()
            => IsAttackedBy("8/8/8/8/3n4/8/8/8 w - - 0 1", "b3", Color.Black, true);

        [Fact]
        public void b5_is_attacked_by_black_knight_on_d4()
            => IsAttackedBy("8/8/8/8/3n4/8/8/8 w - - 0 1", "b5", Color.Black, true);

        [Fact]
        public void c6_is_attacked_by_black_knight_on_d4()
            => IsAttackedBy("8/8/8/8/3n4/8/8/8 w - - 0 1", "c6", Color.Black, true);

        [Fact]
        public void e6_is_attacked_by_black_knight_on_d4()
            => IsAttackedBy("8/8/8/8/3n4/8/8/8 w - - 0 1", "e6", Color.Black, true);

        [Fact]
        public void f5_is_attacked_by_black_knight_on_d4()
            => IsAttackedBy("8/8/8/8/3n4/8/8/8 w - - 0 1", "f5", Color.Black, true);

        [Fact]
        public void f3_is_attacked_by_black_knight_on_d4()
            => IsAttackedBy("8/8/8/8/3n4/8/8/8 w - - 0 1", "f3", Color.Black, true);

        [Fact]
        public void e2_is_attacked_by_black_knight_on_d4()
            => IsAttackedBy("8/8/8/8/3n4/8/8/8 w - - 0 1", "e2", Color.Black, true);

        [Fact]
        public void d1_is_attacked_by_black_rook_on_d4()
            => IsAttackedBy("8/8/8/8/3r4/8/8/8 w - - 0 1", "d1", Color.Black, true);

        [Fact]
        public void d6_is_attacked_by_black_rook_on_d4()
            => IsAttackedBy("8/8/8/8/3r4/8/8/8 w - - 0 1", "d6", Color.Black, true);

        [Fact]
        public void f4_is_attacked_by_black_rook_on_d4()
            => IsAttackedBy("8/8/8/8/3r4/8/8/8 w - - 0 1", "f4", Color.Black, true);

        [Fact]
        public void a4_is_attacked_by_black_rook_on_d4()
            => IsAttackedBy("8/8/8/8/3r4/8/8/8 w - - 0 1", "a4", Color.Black, true);

        [Fact]
        public void c4_is_attacked_by_black_queen_on_d4()
            => IsAttackedBy("8/8/8/8/3q4/8/8/8 w - - 0 1", "c4", Color.Black, true);

        [Fact]
        public void c3_is_attacked_by_black_queen_on_d4()
            => IsAttackedBy("8/8/8/8/3q4/8/8/8 w - - 0 1", "c3", Color.Black, true);

        [Fact]
        public void d3_is_attacked_by_black_queen_on_d4()
            => IsAttackedBy("8/8/8/8/3q4/8/8/8 w - - 0 1", "d3", Color.Black, true);

        [Fact]
        public void e3_is_attacked_by_black_queen_on_d4()
            => IsAttackedBy("8/8/8/8/3q4/8/8/8 w - - 0 1", "e3", Color.Black, true);

        [Fact]
        public void e4_is_attacked_by_black_queen_on_d4()
            => IsAttackedBy("8/8/8/8/3q4/8/8/8 w - - 0 1", "e4", Color.Black, true);

        [Fact]
        public void e5_is_attacked_by_black_queen_on_d4()
            => IsAttackedBy("8/8/8/8/3q4/8/8/8 w - - 0 1", "e5", Color.Black, true);

        [Fact]
        public void d5_is_attacked_by_black_queen_on_d4()
            => IsAttackedBy("8/8/8/8/3q4/8/8/8 w - - 0 1", "d5", Color.Black, true);

        [Fact]
        public void c5_is_attacked_by_black_queen_on_d4()
            => IsAttackedBy("8/8/8/8/3q4/8/8/8 w - - 0 1", "c5", Color.Black, true);

        // =============== King ================

        [Fact]
        public void c4_is_attacked_by_black_king_on_d4()
            => IsAttackedBy("8/8/8/8/3k4/8/8/8 w - - 0 1", "c4", Color.Black, true);

        [Fact]
        public void c3_is_attacked_by_black_king_on_d4()
            => IsAttackedBy("8/8/8/8/3k4/8/8/8 w - - 0 1", "c3", Color.Black, true);

        [Fact]
        public void d3_is_attacked_by_black_king_on_d4()
            => IsAttackedBy("8/8/8/8/3k4/8/8/8 w - - 0 1", "d3", Color.Black, true);

        [Fact]
        public void e3_is_attacked_by_black_king_on_d4()
            => IsAttackedBy("8/8/8/8/3k4/8/8/8 w - - 0 1", "e3", Color.Black, true);

        [Fact]
        public void e4_is_attacked_by_black_king_on_d4()
            => IsAttackedBy("8/8/8/8/3k4/8/8/8 w - - 0 1", "e4", Color.Black, true);

        [Fact]
        public void e5_is_attacked_by_black_king_on_d4()
            => IsAttackedBy("8/8/8/8/3k4/8/8/8 w - - 0 1", "e5", Color.Black, true);

        [Fact]
        public void d5_is_attacked_by_black_king_on_d4()
            => IsAttackedBy("8/8/8/8/3k4/8/8/8 w - - 0 1", "d5", Color.Black, true);

        [Fact]
        public void c5_is_attacked_by_black_king_on_d4()
            => IsAttackedBy("8/8/8/8/3k4/8/8/8 w - - 0 1", "c5", Color.Black, true);

        // =============== Color.White ===============

        [Fact]
        public void c8_is_attacked_by_white_pawn_on_b7()
            => IsAttackedBy("8/1P6/8/8/8/8/8/8 w - - 0 1", "c8", Color.White, true);

        [Fact]
        public void a8_is_attacked_by_white_pawn_on_b7()
            => IsAttackedBy("8/1P6/8/8/8/8/8/8 w - - 0 1", "a8", Color.White, true);

        // =============== Board.IsInCheck ===============

        private static void IsInCheck(string fen, Color color, bool expected)
            => fen.ParseFen().Core
                .IsInCheck(color)
                .Should().Be(expected);

        [Fact]
        public void black_is_in_check_when_their_king_on_c8_is_attacked_by_pawn_on_b7()
            => IsInCheck("2k5/1P6/8/8/8/8/8/8 w - - 0 1", Color.Black, true);

        [Fact]
        public void black_is_not_in_check_when_there_is_no_white_pieces_on_the_board()
            => IsInCheck("2k5/8/8/8/8/8/8/8 w - - 0 1", Color.Black, false);
    }
}