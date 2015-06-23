using ChessKit.ChessLogic.Algorithms;
using ChessKit.ChessLogic.Primitives;
using FluentAssertions;
using Xunit;

namespace ChessKit.ChessLogic.UnitTests
{
    public sealed class UpdatePositionTests
    {
        private static void Check(string startFen, string move, string expectedFen)
        {
            var validateLegal = startFen.ParseFen()
                .ValidateLegal(Move.Parse(move));
            new Position(validateLegal.ResultPosition, 0, 1, 
                GameStates.None, validateLegal)
                .PrintFen()
                .Should().Be(expectedFen);
        }

        [Fact]
        public void IsValidPawnMove() => Check(
            "r1bqkb1r/pppppppp/2n2n2/8/PP6/N7/2PPPPPP/R1BQKBNR b KQkq b3 0 3", "e7-e5",
            "r1bqkb1r/pppp1ppp/2n2n2/4p3/PP6/N7/2PPPPPP/R1BQKBNR w KQkq e6 0 1");

        [Fact]
        public void Black_Take_EnPassant() => Check(
            "Nnbqkbnr/ppp3pp/8/2P2p2/4pP2/3p4/PP1PP1PP/R1BQKBNR b KQk f3 0 7", "e4-f3",
            "Nnbqkbnr/ppp3pp/8/2P2p2/8/3p1p2/PP1PP1PP/R1BQKBNR w KQk - 0 1");

        [Fact]
        public void WhiteKingCastling() => Check(
            "r3k2r/p3bppp/np3n2/2p1B3/7P/1P1b3B/P1PNP2P/RNQ1K2R w KQkq - 2 14", "e1-g1",
            "r3k2r/p3bppp/np3n2/2p1B3/7P/1P1b3B/P1PNP2P/RNQ2RK1 b kq - 0 1");

        [Fact]
        public void WhiteQueenCastling() => Check(
            "rnbk1b1r/p1p1qpp1/3pn3/1N2p1BQ/2P4P/P2P4/1P2BPPN/R3K2R w KQ - 9 16", "e1-c1",
            "rnbk1b1r/p1p1qpp1/3pn3/1N2p1BQ/2P4P/P2P4/1P2BPPN/2KR3R b - - 0 1");

        [Fact]
        public void BlackKingCastling() => Check(
            "1r2k2r/p2n1p1p/np4p1/2p1B1b1/7P/1P1P4/P1PN3P/RNQ2RK1 b k - 0 18", "e8-g8",
            "1r3rk1/p2n1p1p/np4p1/2p1B1b1/7P/1P1P4/P1PN3P/RNQ2RK1 w - - 0 1");

        [Fact]
        public void Kill_WhiteKing_castling() => Check(
            "rnbqkb1r/p2ppp1p/6pn/1pp5/4N2P/7N/PPPPPPP1/R1BQKB1R w KQkq - 2 5", "h1-g1",
            "rnbqkb1r/p2ppp1p/6pn/1pp5/4N2P/7N/PPPPPPP1/R1BQKBR1 b Qkq - 0 1");

        [Fact]
        public void Kill_BlackQueen_castling() => Check(
            "r1b1kb1r/p2p3p/n6n/q1p1pp1P/1p2N1p1/PQP3PN/1P1PPP2/R1B1KBR1 b Qkq - 1 12", "a8-b8",
            "1rb1kb1r/p2p3p/n6n/q1p1pp1P/1p2N1p1/PQP3PN/1P1PPP2/R1B1KBR1 w Qk - 0 1");

        [Fact]
        public void EnPassant() => Check(
            "rnbqkb1r/pppppppp/7n/8/8/7N/PPPPPPPP/RNBQKB1R w KQkq - 2 2", "f2-f4",
            "rnbqkb1r/pppppppp/7n/8/5P2/7N/PPPPP1PP/RNBQKB1R b KQkq f3 0 1");

        [Fact]
        public void Castling1() => Check(
            "rnbqkbnr/ppp3pp/1N6/3ppp2/8/8/PPPPPPPP/R1BQKBNR w KQkq - 0 4", "b6-a8",
            "Nnbqkbnr/ppp3pp/8/3ppp2/8/8/PPPPPPPP/R1BQKBNR b KQk - 0 1");

        [Fact]
        public void Castling2() => Check(
            "r1bknb1r/pp1pp1pp/n1pB4/5p2/3q1P2/QP5N/P1PPP1PP/RN2KB1R b KQ - 10 10", "d4-a1",
            "r1bknb1r/pp1pp1pp/n1pB4/5p2/5P2/QP5N/P1PPP1PP/qN2KB1R w K - 0 1");

        [Fact]
        public void Castling3() => Check(
            "rnbqkb1r/ppp2ppp/2N5/3pp3/8/8/PPPPPnPP/R1BQKBNR b KQkq - 0 6", "f2-h1",
            "rnbqkb1r/ppp2ppp/2N5/3pp3/8/8/PPPPP1PP/R1BQKBNn w Qkq - 0 1");

        [Fact]
        public void Piece_take_should_reset_halfmove_clock() => Check(
            "1r2kbnr/pb1p3p/nq4N1/2p1pp1P/1p2N1p1/PQPP2P1/1P2PPB1/R1B1K1R1 b Qk - 6 16", "b7-e4",
            "1r2kbnr/p2p3p/nq4N1/2p1pp1P/1p2b1p1/PQPP2P1/1P2PPB1/R1B1K1R1 w Qk - 0 1");

        [Fact]
        public void Rook_take_should_remove_castling_option() => Check(
            "1r2kbnr/p2p3p/nq4N1/2pQp2P/1p3pp1/PPPP2P1/4PPb1/R1B1K1R1 w Qk - 0 19", "g6-h8",
            "1r2kbnN/p2p3p/nq6/2pQp2P/1p3pp1/PPPP2P1/4PPb1/R1B1K1R1 b Q - 0 1");

        [Fact]
        public void King_move_should_kill_castling() => Check(
            "1r2kb1N/p1Qpn3/nq5p/4p2P/1p1p1pp1/PPP3P1/4PP2/R1B1KbR1 w Q - 2 23", "e1-f1",
            "1r2kb1N/p1Qpn3/nq5p/4p2P/1p1p1pp1/PPP3P1/4PP2/R1B2KR1 b - - 0 1");

        [Fact]
        public void King_move_should_kill_castling1() => Check(
            "2bqkbnr/rp2Bppp/pn1p4/2p5/1PP4P/N7/P2PPPP1/R2QKBNR b KQk c3 0 8", "e8-d7",
            "2bq1bnr/rp1kBppp/pn1p4/2p5/1PP4P/N7/P2PPPP1/R2QKBNR w KQ - 0 1");

        [Fact]
        public void Rook_move_should_kill_castling2() => Check(
            "rnbqkbnr/p1ppp1p1/1p3p2/1N5Q/4P3/P6p/1PPP1PPP/1RB1KBNR b Kkq - 1 6", "h8-h5",
            "rnbqkbn1/p1ppp1p1/1p3p2/1N5r/4P3/P6p/1PPP1PPP/1RB1KBNR w Kq - 0 1");

        [Fact]
        public void Promotion_with_take_This_test_fails_on_Valil() => Check(
            "rnbqk1nr/1p1p1pPp/p7/2p1p3/8/NPb2P2/P1PPP1PR/R1BQKBN1 w Qkq - 1 9", "g7-h8=R",
            "rnbqk1nR/1p1p1p1p/p7/2p1p3/8/NPb2P2/P1PPP1PR/R1BQKBN1 b Qq - 0 1");

        [Fact]
        public void Take_enPassant_removes_check() => Check(
            "rnb1nrk1/pp3p2/1q2p3/3p1PpP/2pbPK2/PB5P/1PPP4/RNBQ2NR w - g6 0 22", "f5-g6",
            "rnb1nrk1/pp3p2/1q2p1P1/3p3P/2pbPK2/PB5P/1PPP4/RNBQ2NR b - - 0 1");

        [Fact]
        public void Promotion() => Check(
            "8/3kbN2/1nr4p/1q1p3P/pPR3P1/2Bp2p1/1Kp5/6R1 b - - 4 64", "c2-c1=Q",
            "8/3kbN2/1nr4p/1q1p3P/pPR3P1/2Bp2p1/1K6/2q3R1 w - - 0 1");

        [Fact]
        public void Promote_to_Q_by_default() => Check(
            "8/3kbN2/1nr4p/1q1p3P/pPR3P1/2Bp2p1/1Kp5/6R1 b - - 4 64", "c2-c1",
            "8/3kbN2/1nr4p/1q1p3P/pPR3P1/2Bp2p1/1K6/2q3R1 w - - 0 1");

        [Fact]
        public void IsValidPawnMove2() => Check(
            "8/1P4k1/8/8/3N4/1n1b2B1/1K6/8 w - - 51 130", "b7-b8=B",
            "1B6/6k1/8/8/3N4/1n1b2B1/1K6/8 b - - 0 1");


    }
}
