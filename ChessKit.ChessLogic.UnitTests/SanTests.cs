using ChessKit.ChessLogic.Algorithms;
using FluentAssertions;
using Xunit;

namespace ChessKit.ChessLogic.UnitTests
{
 public sealed class SanTests
 {
        private static void Check(string fen, string sanMove, string cnmMove)
        {
            fen.ParseFen()
                .ParseMoveFromSan(sanMove).Move
                .ToString().Should().Be(cnmMove);
        }

        [Fact] public void white_O_O() =>
            Check("r3k2r/8/8/8/8/8/8/R3K2R w KQkq - 0 12", "O-O", "e1-g1");
        [Fact] public void black_O_O() =>
            Check("r3k2r/8/8/8/8/8/8/R3K2R b KQkq - 0 12", "O-O", "e8-g8");
        [Fact] public void white_O_O_O() =>
            Check("r3k2r/8/8/8/8/8/8/R3K2R w KQkq - 0 12", "O-O-O", "e1-c1");
        [Fact] public void black_O_O_O() =>
            Check("r3k2r/8/8/8/8/8/8/R3K2R b KQkq - 0 12", "O-O-O", "e8-c8");
        [Fact] public void pawn_push() =>
            Check("8/8/8/8/8/8/P7/8 w - - 0 12", "a3", "a2-a3");
        [Fact] public void pawn_push_and_promote() =>
            Check("8/P7/8/8/8/8/8/8 w - 0 12", "a8=R", "a7-a8=R");

        //[Fact] public void pawn_capture_and_promote() =>
        //    Check("1n6/P7/8/8/8/8/8/8 w - 0 12" 
        // |> warn "axb8=Q", "a7_b8=Q", "DisambiguationIsExcessive");

        [Fact] public void pawn_double_push() =>
            Check("8/8/8/8/8/8/P7/8 w - 0 12", "a4", "a2_a4");

        //[Fact] public void pawn_3_squares_push() =>
        //    Check("8/8/8/8/8/8/P7/8 w - 0 12" 
        // |> illegal "a5", "a2_a5", "Pawn | DoesNotMoveThisWay");

        //[Fact] public void pawn_captures() =>
        //    Check("8/8/8/8/8/1p6/P7/8 w - 0 12" 
        // |> warn "axb3", "a2_b3", "DisambiguationIsExcessive");

        [Fact] public void Two_pawns_can_capture() =>
            Check("8/8/8/8/8/1p6/P1P5/8 w - 0 12", "cxb3", "c2-b3");

        //[Fact] public void pawn_move_does_not_make_sense() =>
        //    Check("8/8/8/8/8/8/P7/8 w - 0 12" 
        // |> nonsense "axb4", "PieceNotFound WhitePawn");

        //[Fact] public void can_t_push_pawn_cause_it_s_pinned() =>
        //    Check("8/8/8/3rP1K1/8/8/8/8 w f6 0 1" |> illegal "e6", "MoveToCheck");

        //[Fact] public void pawn_can_t_capture_cause_it_s_pinned() =>
        //    Check("8/6K1/3r4/4P3/8/2b5/8/2k5 w f6 0 1" |> illegal "exd6", "MoveToCheck");

        [Fact] public void Nf3() =>
            Check("8/8/8/8/8/8/8/6N1 w - - 0 12", "Nf3", "g1-f3");

        //[Fact] public void over_disambiguate_N1f3() =>
        //    Check("8/8/8/8/8/8/8/6N1 w 0 12" 
        // |> warn "N1f3", "g1_f3", "DisambiguationIsExcessive");

        //[Fact] public void under_disambiguate_Nf3() =>
        //    Check("8/8/8/6N1/8/8/8/6N1 w - 0 12" 
        // |> nonsense "Nf3", "AmbiguousChoice [g1_f3; g5_f3]");

        [Fact] public void disambiguate_N1f3() =>
            Check("8/8/8/6N1/8/8/8/6N1 w 0 12", "N1f3", "g1-f3");

        //[Fact] public void no_candidates_found_Nf3() =>
        //    Check("8/8/8/8/8/8/8/8 w - 0 12" 
        // |> nonsense "Nf3", "PieceNotFound WhiteKnight");

        //[Fact] public void wrong_disambiguation_N1f3() =>
        //    Check("8/8/8/6N1/8/8/8/8 w - 0 12" 
        // |> nonsense "N1f3", "PieceNotFound WhiteKnight");

        [Fact] public void one_of_the_knights_is_pinned() =>
            Check("8/6K1/8/4N3/8/2b5/8/2k1N3 w f6 0 1", "Nd3+", "e1-d3");

        [Fact] public void file_and_rank_disambiguation() =>
            Check("8/5N2/8/6q1/8/5N1N/8/2k1K3 w - 0 2", "Nf3xg5", "f3-g5");

        //[Fact] public void no_check_when_there_should_be() =>
        //   Check("Q7/8/8/8/8/8/8/8 w - 0 2" |> warn "Qh1+", "a8_h1", "IsNotCheck");

        //[Fact] public void check_is_not_marked() =>
        //    Check("Q7/8/8/8/8/8/8/k7 w - 0 2" |> warn "Qh1", "a8_h1", "IsCheck");

        //[Fact] public void it_is_not_capture_when_it_should_be() =>
        //    Check("Q7/8/8/8/8/8/8/7n w - 0 2" |> warn "Qh1", "a8_h1", "IsCapture");

        //[Fact] public void it_is_not_mate_when_it_should_be() =>
        //    Check("8/Q7/8/8/8/8/8/5K1k w - 0 1" |> warn "Qh7", "a7_h7", "IsMate");

        [Fact] public void it_is_mate_when_it_should_be() =>
            Check("8/Q7/8/8/8/8/8/5K1k w - 0 1", "Qh7#", "a7_h7");

        //[Fact] public void it_is_check_not_mate() =>
        //    Check("8/Q7/8/8/8/8/8/5K1k w - 0 1" |> warn "Qa8#", "a7_a8", "IsCheck IsNotMate");

        //[Fact] public void it_not_marked_capture_when_it_should_be() =>
        //    Check("Q7/8/8/8/8/8/8/8 w - 0 2" |> warn "Qxh1", "a8_h1", "IsNotCapture");

        //[Fact] public void 2_candidates_0_valid_moves() =>
        //    Check("8/8/2B3B1/3n1n2/4k3/8/8/8 b - 0 2" 
        // |> nonsense "Ne7" 
        // "ChoiceOfIllegalMoves [d5_e7 (MoveToCheck); f5_e7 (MoveToCheck)]");

        //[Fact] public void disambiguation_is_excessive_only_after_validation() =>
        //    Check("4b3/5N2/8/6nK/8/5N1N/8/2k4r - w 0 2" 
        // |> warn "Nf3xg5", "f3_g5", "DisambiguationIsExcessive");

        //[Fact] public void invalid_castling() =>
        //    Check("rn2k2r/ppp2ppp/3B1n2/8/3P2b1/6P1/PPP1N2P/RN1QKB1q - b Qkq - 0 9" 
        // |> illegal "O-O", "e8-g8", "King | BK | CastleThroughCheck");
 
 }
}
