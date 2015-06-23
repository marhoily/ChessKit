using System;
using System.Linq;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.UnitTests
{
    public class TestMoveData
    {
        private string Name { get; }
        public string StartingFen { get; }
        public string Move { get; }
        private string Result { get; }

        public bool ExpectedToBeValid => (ExpectedAnnotations & MoveAnnotations.AllErrors) == 0;

        public MoveAnnotations ExpectedAnnotations
        {
            get
            {
                MoveAnnotations result = 0;
                foreach (var s in Result.Split(new[] {" | "}, StringSplitOptions.None))
                    result |= (MoveAnnotations) Enum.Parse(typeof (MoveAnnotations), s);
                return result;
            }
        }

        private TestMoveData(string name, string startingFen, string move, string result)
        {
            Name = name;
            StartingFen = startingFen;
            Move = move;
            Result = result;
        }

        public override string ToString() => Name;

        public static TestMoveData[][] All()
        {
            return new[] {
                // ReSharper disable StringLiteralTypo
                new [] {new TestMoveData("IsValidRook", "4k2r/2r1np1p/1p5b/p2p2p1/qPb2PP1/P1P1P2P/1B1P1KN1/R5R1 b k - 2 25", "a4-b4", "Queen | Capture") },
                new [] {new TestMoveData("Long castling doesn't care if B file square is under attack", "r3k1r1/3bbpp1/2q5/2pN3p/pQ4n1/1P1K4/n2P1PBP/R5NR b q - 0 30", "e8-c8", "King | BQ") },
                new [] {new TestMoveData("No stack overflow", "8/5k2/R6K/8/8/8/8/8 w - - 7 143", "h6-h7", " King") },
                new [] {new TestMoveData("There is no check 2", "7b/P4r2/1R6/7k/8/8/6KB/8 w - - 5 108", "g2-h1", " King") },
                new [] {new TestMoveData("There is no check 1", "3N4/2n2r2/k7/1q3Pb1/2Q4p/4RK1P/4p3/8 b - - 7 85", "a6-a5", " King ") },
                new [] {new TestMoveData("There is no check 3", "1k1K4/8/8/8/8/8/8/B7 b - - 3 100", "b8-a8", " King ") },
                new [] {new TestMoveData("King Captures the piece that gives it check", "8/1k6/4R3/1NP4p/p2Q3P/4K1n1/3q4/8 w - - 5 73", "e3-d2", " King | Capture") },
                new [] {new TestMoveData("Rook move2", "rnbqkbnr/pp1ppp1p/6p1/2p5/7P/2N5/PPPPPPP1/R1BQKBNR w KQkq c6 0 3", "a1-b1", " Rook") },
                new [] {new TestMoveData("IsValidRook", "1n1qkbnr/rpp1pppp/p2pb3/8/8/NP2P1PN/P1PP1P1P/R1BQKB1R b KQk - 0 5", "a7-a8", " Rook") },
                new [] {new TestMoveData("IsValidRook", "r2qkbnr/1pp2ppp/p2pb3/8/6Q1/PP2P1PN/2PP1PBP/R1B1K2R b KQk - 0 10", "a8-c8", " Rook") },
                new [] {new TestMoveData("IsValidKingCastlingMove 1", "1rbqkb1r/p1pp1ppp/1p6/2n1p3/4nP1P/3PPN2/PPPNB1P1/R1BQK2R w KQk - 1 8", "e1-g1", "King | WK") },
                new [] {new TestMoveData("BishopMove2", "rnbqkb1r/p2ppp1p/6pn/1pp5/4N2P/7N/PPPPPPP1/R1BQKBR1 b Qkq - 3 5", "c8-b7", "Bishop") },
                new [] {new TestMoveData("Custle from under check", "rn1qk2r/1b2p1b1/2ppPp1n/p5pQ/p1PP1N2/6P1/PP3P1P/1RB1KB1R b kq - 0 15", "e8-g8", "King | BK | CastleFromCheck") },
                new [] {new TestMoveData("Custle from under check", "r3k2r/ppp2ppp/n2q1n2/1Bbppb2/4P3/P1N2N2/1PPP1PPP/R1B1QRK1 b kq - 0 8", "e8-c8", "King | BQ | CastleFromCheck") },
                new [] {new TestMoveData("Custle from under check", "rnbqk2r/ppp2ppp/5n2/1Bbpp3/4P3/2N2N2/PPPP1PPP/R1BQK2R b KQkq - 0 5", "e8-g8", "King | BK | CastleFromCheck") },
                new [] {new TestMoveData("Castling cannot Capture a piece", "r1B1k1r1/p2p1p2/1p1b3p/2pqp1p1/PPP2n2/B1N2NP1/3P3P/1R1K3n b q - 0 35", "e8-c8", "King | BQ | Capture | DoesNotCaptureThisWay | BQ") },
                new [] {new TestMoveData("DoesNotMoveThisWay", "rnbqk1nr/pppp1ppp/3bp3/8/4P3/7P/PPPP1PP1/RNBQKBNR w KQkq - 1 3", "a2-e2", "Pawn | DoesNotMoveThisWay") },
                new [] {new TestMoveData("BishopMove", "rnbqkbnr/pp1ppp1p/6p1/2p5/7P/2N5/PPPPPPP1/R1BQKBNR w KQkq c6 0 3", "c1-b1", "Bishop | DoesNotMoveThisWay") },
                new [] {new TestMoveData("Knight move", "rnbqkbnr/ppp2p1p/3p2P1/4p3/4P3/8/PPPP2PP/RNBQKBNR b KQkq - 0 4", "b8-b6", "Knight | DoesNotMoveThisWay") },
                new [] {new TestMoveData("Queen move", "rnbqkbnr/pp1ppp1p/6p1/2p5/7P/2N5/PPPPPPP1/R1BQKBNR w KQkq c6 0 3", "d1-b1", "Queen | DoesNotJump") },
                new [] {new TestMoveData("Queen | DoesNotJump", "rnbqkbnr/1pppppp1/p7/7p/8/3P1N2/PPP1PPPP/RNBQKB1R w KQkq - 0 3", "d1-g4", "Queen | DoesNotJump") },
                new [] {new TestMoveData("IsValidBishopMove1", "1N1b4/8/2P5/1k3K2/8/p7/P7/8 b - - 16 140", "d8-d1", "Bishop | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidBishopMove2", "rnbqkbnr/1pp1pppp/8/p2p4/3PP3/8/PPP2PPP/RNBQKBNR w KQkq d6 0 3", "c1-a3", "Bishop | DoesNotJump") },
                new [] {new TestMoveData("IsValidKnightMove3", "r1b2k1r/2n1qpbp/2p3p1/1p1Pp2n/1PBP3P/PQ6/2P2PP1/RNBK2N1 b - - 6 20", "h5-a4", "Knight | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidKnightMove4", "r1bqkb2/2ppp2r/p4Qpn/5p1p/1p6/N2PBN2/PPP1PPPP/R3KB1R w Qq - 2 13", "a3-g1", "Knight | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidKnightMove5", "8/2r2r2/2bb2k1/N2Pp3/1p2PBnp/p1PK1B2/PP6/R6R w - - 4 48", "a5-h2", "Knight | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidKnightMove6", "r1bqkb2/1ppppr2/p5pn/5p1p/3Q4/N2PBN2/PPP1PPPP/R3KBR1 b Qq - 3 10", "h6-b8", "Knight | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidKnightMove7", "r1bqkb2/1ppppp1r/p5pn/7p/3Q4/N2P1N2/PPP1PPPP/R1B1KBR1 w Qq - 0 8", "a3-h4", "Knight | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidKingCastlingMove 2", "rn1qkbn1/p3p1pr/2p2p2/1p1p3p/3P2b1/2P5/PP1BPPPP/RN2KBNR w KQq - 2 8", "e1-c1", "King | WQ | DoesNotJump") },
                new [] {new TestMoveData("IsValidKingCastlingMove 4", "rn1qkb2/p5pr/2p2p1n/1p1pp1Bp/3P2b1/1PP2P2/P3P1PP/RN2KBNR b KQq - 1 10", "e8-c8", "King | BQ | DoesNotJump") },
                new [] {new TestMoveData("IsValidKingCastlingMove 6", "rnb1k3/2q1r1np/p1pp1p2/p1b1Pp2/1P2P3/4QNP1/P1PK1PRP/R1B5 w q - 3 23", "d2-b2", "King | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidKingCastlingMove 7", "rnbqkbn1/pppppppr/8/6Np/8/8/PPPPPPPP/RNBQKB1R w KQq - 2 3", "e1-g1", "King | WK | DoesNotJump") },
                new [] {new TestMoveData("IsValidKingCastlingMove 8", "2b1k2r/r2p1ppp/2p1p3/p7/4P1nq/1P3PP1/P3K2P/3RbBNR w k - 7 23", "e2-g2", "King | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidKingCastlingMove 9", "r1bqkbnr/pppp1ppp/n7/4p3/5P2/3P4/PPP1P1PP/RNBQKBNR w KQkq - 1 3", "e1-a3", "King | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidKing", "8/5K2/k7/8/3N4/1P4Q1/8/8 b - - 6 110", "a6-h5", "King | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidKing", "1rk3r1/1b5p/1pp5/p3R1PP/1PP2p2/1N3B2/1P5R/2B4K w - - 0 33", "h1-a2", "King | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidPawn1", "r1bk4/2q3n1/p1p1rp2/2p2p1p/p3QPnP/2K3P1/P1P3R1/B3N1R1 w - - 1 38", "a2-a4", "Pawn | Capture | DoesNotCaptureThisWay") },
                new [] {new TestMoveData("IsValidPawn2", "rnb1k2r/3p1pnp/pqp3p1/p1b1p3/3PP2N/6P1/PPP2P1P/RNBQK1R1 w kq - 1 13", "e4-e5", "Pawn | Capture | DoesNotCaptureThisWay") },
                new [] {new TestMoveData("IsValidPawn3", "r1bk4/2q3n1/p1p1rp2/2p2p1p/p3QPnP/2K3P1/P1P3R1/B3N1R1 w - - 1 38", "h4-a6", "Pawn | Capture | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidPawn4", "rnbqkbnr/1p1ppppp/B1p5/p7/4P3/8/PPPP1PPP/RNBQK1NR w KQkq - 0 3", "e4-e6", "Pawn | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidPawn5", "rnb1k3/2q1r1np/p1pp1p2/p1b1Pp2/1P2P3/4QNP1/P1PK1PRP/R1B5 w q - 3 23", "f2-f4", "Pawn | DoesNotJump") },
                new [] {new TestMoveData("IsValidPawn6", "rnbqkbnr/p3ppp1/2p5/1p1p3p/3P4/2P5/PPQ1PPPP/RNB1KBNR b KQkq - 1 5", "b5-b3", "Pawn | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidPawn7", "rn1qkb2/p5pr/2p2p1n/1p1pp1Bp/3P2b1/1PP2P2/P3P1PP/RN2KBNR b KQq - 1 10", "g7-g5", "Pawn | Capture | DoesNotCaptureThisWay") },
                new [] {new TestMoveData("IsValidPawn8", "r1bqkb1r/pppp1ppp/2n5/4p3/PPP1n3/N7/R2PPPPP/2BQKBNR b Kkq c3 0 5", "c7-c5", "Pawn | DoesNotJump") },
                new [] {new TestMoveData("IsValidPawn9", "r1bq1b1r/1pppnkpp/8/p3ppP1/PPP1n3/N4P2/R2PP2P/2BQKBNR b K - 3 10", "a5-a4", "Pawn | Capture | DoesNotCaptureThisWay") },
                new [] {new TestMoveData("IsValidPawn10", "r1bqkb1r/pppp1ppp/2n5/4p3/PPP1n3/N7/R2PPPPP/2BQKBNR b Kkq c3 0 5", "e5-a1", "Pawn | DoesNotMoveThisWay") },
                new [] {new TestMoveData("Cant short castle through occupied square", "8/4k3/8/8/8/8/8/4Kb1R w K - 0 1", "e1-g1", "King | WK | DoesNotJump") },
                new [] {new TestMoveData("Cant long castle through occupied square", "8/4k3/8/6b1/8/8/8/R2bK3 w Q - 0 1", "e1-c1", "King | WQ | DoesNotJump") },
                new [] {new TestMoveData("Cannot move where your piece is", "rnbqkbnr/pppppppp/8/8/8/5N2/PPPPPPPP/RNBQKB1R b KQkq - 1 1", "f6-g8", "EmptyCell") },
                new [] {new TestMoveData("Cannot move from empty cell", "rnbqk1nr/pppp1ppp/3bp3/8/4P3/7P/PPPP1PP1/RNBQKBNR w KQkq - 1 3", "e2-e3", "EmptyCell") },
                new [] {new TestMoveData("Promotion", "4kb1N/p2nn3/1rq4p/3p3P/PP2p1p1/2ppKPp1/1B2P3/2R4R b - - 0 40", "e8-c8", "HasNoCastling | BQ") },
                new [] {new TestMoveData("Move to check", "rnbqkb1r/ppp2ppp/8/3pp1N1/1P3PnP/2P5/P2PP1P1/RNBQKBR1 w Qkq - 1 1", "e1-f2", "King | MoveToCheck") },
                new [] {new TestMoveData("Move to check from pawn", "r2q2n1/p1p1kp1r/b5p1/1p1Pn2p/8/bP1P1P1P/PR1BB1P1/3QK1NR b K - 0 1", "e7-e6", "King | MoveToCheck") },
                new [] {new TestMoveData("There is check from bishop 1", "rnb1kb1r/1pp1pppp/p4n2/2qp4/5P2/3P2PP/PPPNP1B1/R1BQK1NR w KQkq - 3 8", "e1-f2", "King | MoveToCheck") },
                new [] {new TestMoveData("Knight 1", "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "b1-a3", "Knight") },
                new [] {new TestMoveData("Knight 2", "rnbqkbnr/pppppppp/8/8/7P/8/PPPPPPP1/RNBQKBNR b KQkq h3 0 1", "b8-c6", "Knight") },
                new [] {new TestMoveData("Knight 3", "rnbqkbnr/pp1ppp1p/6p1/2p5/7P/2N5/PPPPPPP1/R1BQKBNR w KQkq c6 0 1", "c3-a4", "Knight") },
                new [] {new TestMoveData("Knight 4", "rnbqkbnr/pp1ppp1p/6p1/2p5/7P/2N5/PPPPPPP1/R1BQKBNR w KQkq c6 0 1", "c3-e4", "Knight") },
                new [] {new TestMoveData("Knight 4", "rnbqkbnr/p2ppp1p/6p1/1pp5/4N2P/8/PPPPPPP1/R1BQKBNR w KQkq b6 0 1", "e4-c3", "Knight") },
                new [] {new TestMoveData("Knight 5", "rnbqkbnr/p2ppp1p/6p1/1pp5/4N2P/8/PPPPPPP1/R1BQKBNR w KQkq b6 0 1", "e4-g3", "Knight") },
                new [] {new TestMoveData("Knight | DoesNotMoveThisWay", "rnbqkb1r/p2ppp1p/6pn/1pp5/4N2P/7N/PPPPPPP1/R1BQKB1R w KQkq - 2 1", "h3-a6", "Knight | DoesNotMoveThisWay") },
                new [] {new TestMoveData("Castling accross attacked square1", "r1bqk2r/pppp4/1P4pp/4pP2/n4PN1/B1n5/P2PP1BP/RN1QK2R b KQk - 1 15", "e8-g8", "King | BK | CastleThroughCheck") },
                new [] {new TestMoveData("Castling accross attacked square2", "rn2kbn1/p4p1r/2p4q/1p1Bp1pp/PP1pPP2/2N4b/2PP3P/R1BQK2R w KQq - 0 13", "e1-g1", "King | WK | CastleThroughCheck") },
                new [] {new TestMoveData("Castling accross attacked square3", "1n2kb1r/q3p1p1/3p1Bpp/p1pN4/1p2P3/2PP1N1b/PP3P1P/R3K2R w Kk - 2 23", "e1-g1", "King | WK | CastleThroughCheck") },
                new [] {new TestMoveData("Castling accross the square attacked by a pawn", "1n3kn1/rpPb1p2/1P6/p1p3p1/P1B1PP2/2P4r/R5p1/1N2K2R w K - 1 28", "e1-g1", "King | WK | CastleThroughCheck") },
                new [] {new TestMoveData("IsValidKingCastlingMove 3", "1nb1kb2/3p1Np1/rpp4r/p1P1pp2/1Q2P1p1/PP2n3/4PP2/R3KBR1 w Q - 1 28", "e1-c1", "King | WQ | CastleThroughCheck") },
                new [] {new TestMoveData("IsValidKingCastlingMove 5", "r3kbn1/p2nBNp1/r7/1b1P4/RP1P1P1P/3q2p1/3N2BR/4K3 b q - 1 30", "e8-c8", "King | BQ | CastleThroughCheck") },
                new [] {new TestMoveData("IsValidPawn", "r1bq1b1r/1pppnkpp/8/p3ppP1/PPP1n3/N4P2/R2PP2P/2BQKBNR b K - 3 10", "a5-b4", "Pawn | Capture") },
                new [] {new TestMoveData("En-passant Capture is allowed but for the wrong side", "rnbqkbnr/ppp2p1p/3p4/4pPp1/4P3/8/PPPP2PP/RNBQKBNR w KQkq g3 0 4", "f5-g6", "Pawn | Capture | EnPassant") },
                new [] {new TestMoveData("Pawn doesn't Capture en-passant", "rnbqkb1r/1pp2ppp/p7/3pp1NP/1P3Pn1/2P5/P2PP1P1/RNBQKBR1 w Qkq - 0 1", "h5-g6", "Pawn | Capture | EnPassant | HasNoEnPassant") },
                new [] {new TestMoveData("Wrong file to Capture en-passant", "rnbqkbnr/ppp2p1p/8/3ppPp1/4P3/8/PPPP2PP/RNBQKBNR w KQkq g6 0 4", "f5-e6", "Pawn | Capture | EnPassant | HasNoEnPassant") },
                new [] {new TestMoveData("There is check from pawn 1", "2r1kb1r/2p1ppp1/pn3n2/7p/PpPp1P1N/BP1P2PN/3KP1qR/1R1Q1B2 w k - 1 23", "e2-e3", "Pawn | MoveToCheck") },
                new [] {new TestMoveData("Wrong side to move", "rnbqk1nr/pppp1ppp/3bp3/8/4P3/7P/PPPP1PP1/RNBQKBNR b KQkq - 1 3", "e4-e5", "Pawn | WrongSideToMove") },
                new [] {new TestMoveData("Queen move2", "r1b1kb1r/p2pp2p/n6n/qpp2p1P/4N1p1/PQP4N/1P1PPPP1/R1B1KB1R b Qkq - 0 10", "a5-a4", "Queen") },
                new [] {new TestMoveData("Queen move3", "rnbqkb1r/p2ppp1p/6pn/1pp5/4N2P/7N/PPPPPPP1/R1BQKBR1 b Qkq - 3 5", "d8-c7", "Queen") },
                new [] {new TestMoveData("Move to discovered check", "2r4r/p1Bk1p2/b1n1qnp1/1p5p/5P1P/1P1P2PQ/Pb2BK2/6NR b - - 10 1", "e6-b3", "Queen | Capture | MoveToCheck") },
                new [] {new TestMoveData("IsValidRook1", "8/2rnk2p/1p5b/pP1p3P/P5p1/2PK4/7q/5R2 b - - 9 65", "h2-a2", "Queen") },
                new [] {new TestMoveData("IsValidRook2", "2r1kn1r/5p1p/1pq4b/p2p2p1/1P2PPP1/P1P1K2P/1B4N1/2R2bR1 w - - 7 33", "g1-f1", "Rook | Capture") },
                new [] {new TestMoveData("Rook move", "rnbqkbnr/pp1ppp1p/6p1/2p5/7P/2N5/PPPPPPP1/R1BQKBNR w KQkq c6 0 3", "a1-h2", "Rook | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidRook3", "r2qkbnr/1ppn1ppp/p2pb3/8/8/1P2P1PN/P1PP1P1P/R1BQKB1R w KQk - 0 8", "a1-a3", "Rook | DoesNotJump") },
                new [] {new TestMoveData("IsValidRook4", "2r1kn1r/5p1p/1pq4b/p2p2p1/1P2PPP1/P1P1K2P/1B4N1/2R2bR1 w - - 7 33", "c1-c4", "Rook | DoesNotJump") },
                new [] {new TestMoveData("IsValidRook5", "1n1qkbnr/rpp1pppp/p2pb3/8/8/NP2P1PN/P1PP1P1P/R1BQKB1R b KQk - 0 5", "a7-f6", "Rook | DoesNotMoveThisWay") },
                new [] {new TestMoveData("IsValidRook6", "1n1qkbnr/rpp1pppp/p2pb3/8/8/NP2P1PN/P1PP1P1P/R1BQKB1R b KQk - 0 5", "a7-d7", "Rook | DoesNotJump") },
                new [] {new TestMoveData("Move to occupied cell", "rnbqk1nr/pppp1ppp/3bp3/8/4P3/7P/PPPP1PP1/RNBQKBNR w KQkq - 1 3", "a1-a2", "Rook | ToOccupiedCell") },
                new [] {new TestMoveData("Move to occupied cell2", "rnbqk1nr/pppp1ppp/3bp3/8/4P3/7P/PPPP1PP1/RNBQKBNR w KQkq - 1 3", "a1-a2", "Rook | ToOccupiedCell") },
                new [] {new TestMoveData("New check series", "1r1nkb1N/p3n3/q2p3p/7P/Pp2p1p1/RPPp1Pp1/1B2PK2/5R2 w - - 0 33", "f2-e3", "King") },
                new [] {new TestMoveData("DoublePush", "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "a2-a4", "Pawn | DoublePush") },
                new [] {new TestMoveData("Castling again", "4k3/8/1P4qP/2P2N2/8/P5r1/4n3/1K6 b - - 2 90", "e8-g8", "King | BK | HasNoCastling") },
                new [] {new TestMoveData("Black pawn double move", "rnbqkbnr/pppppppp/8/8/8/N7/PPPPPPPP/R1BQKBNR b KQkq - 1 1", "a7-a5", "Pawn | DoublePush") },
                new [] {new TestMoveData("White pawn only takes this way1", "rnbqkbnr/ppppppp1/8/7p/8/N7/PPPPPPPP/R1BQKBNR w KQkq h6 0 2", "g2-h3", "Pawn | OnlyCapturesThisWay ") },
                new [] {new TestMoveData("Black pawn only takes this way2", "rnbqkbnr/ppppp1p1/5p2/7p/2P5/N4P2/PP1PP1PP/R1BQKBNR b KQkq c3 0 3", "b7-c6", "Pawn | OnlyCapturesThisWay ") },
                new [] {new TestMoveData("Black pawn move to check", "rnbqkbnr/p1pp2p1/4ppQ1/1p6/2P5/N4Pp1/PP1PP2P/R1B1KBNR b KQkq - 1 7", "g3-g2", "Pawn | MoveToCheck ") },
                new [] {new TestMoveData("Black pawn move to check2", "rnbqkbnr/p1pp2p1/4ppQ1/1p6/2P5/N4Pp1/PP1PP2P/R1B1KBNR b KQkq - 1 7", "g3-h2", "Pawn | Capture | MoveToCheck") },
                new [] {new TestMoveData("Valid pawn move towards pinning piece", "rnb1kbnr/1p2pp2/p1p4p/3p3q/4R2N/5P2/1PPPP1PR/1NBQKB2 b q - 1 11", "e7-e6", "Pawn") },
                new [] {new TestMoveData("Valid bishop move towards pinning piece", "1r1qk3/2ppnp2/bp2p1Pb/p2P4/3nPB2/PPPQ3R/3K1PP1/RN3BN1 w - - 5 20", "f4-h6", "Bishop | Capture") },
                new [] {new TestMoveData("Knight takes piece that gives check", "4kb1r/r1p3q1/4b2p/1Bnpp3/1P2P1pP/P1P4n/3P1PP1/RNBR3K b k - 0 20", "c5-d7", "Knight ") },
                new [] {new TestMoveData("King moves along attack line", "rn5r/1b4q1/3p4/1k3p1p/p1NK1PPb/3R4/1B1P4/R3N3 w - - 3 44", "d4-c3", "King | MoveToCheck") },
                new [] {new TestMoveData("Knight takes attacking piece", "1nb2b2/3p2pQ/P1pk2n1/1B2Pp2/p6R/3P4/PPN1P1q1/R1B1K1N1 b - - 0 25", "g6-e5", "Knight | Capture") },
                new [] {new TestMoveData("Black pawn takes en-passant", "1nb2b2/3p2pQ/P1p3nB/1B1k1p2/pP2R3/3P4/P1N1P1q1/R3K1N1 b - b3 0 28", "a4-b3", "Pawn | Capture | EnPassant") },
                new [] {new TestMoveData("Pawn takes attacking piece", "2b1qb1r/1pppkppp/r7/p2np1N1/2B1P1P1/4K3/PPPPQP1P/RNB2R2 w - - 1 11", "e4-d5", "Pawn | Capture") },
                new [] {new TestMoveData("Black pawn takes en-passant", "7r/B2b2pp/3qpbk1/pN2n3/1Pp1NP1P/7K/P5P1/R1Q2BR1 b - b3 0 32", "c4-b3", "Pawn | Capture | EnPassant") },
                new [] {new TestMoveData("Pawn advances towards pinning piece", "r1q4b/6kr/8/pP1p1pP1/R1P2pnB/NPK1p2P/2b1B2R/6N1 w - - 2 52", "c4-c5", "Pawn") },
                new [] {new TestMoveData("Pawn takes towards pinning piece", "r4bnr/2p1p1pp/ppn2p2/2k5/P7/1PN1P1Rb/3PBPPP/2B1QKNR w - - 1 19", "g2-h3", "Pawn | Capture") },
                new [] {new TestMoveData("Pawn advances", "1rn2k2/3p2qQ/1p1p3b/4p3/P3P3/P1P1R3/1R2KPPB/1N4N1 w - - 9 37", "f2-f3", "Pawn") },
                new [] {new TestMoveData("Pawn advances x2 towards pinning piece", "r3k1n1/B5b1/n3q1pr/2p4p/PpN4P/1P1P2P1/R1P1P1b1/1N1QK3 w - - 4 33", "e2-e4", "Pawn | DoublePush") },
                new [] {new TestMoveData("Pawn takes attacking piece#2", "rn3k2/p7/1p5n/1p1prb1p/1N2p2R/P1P2q2/4K1P1/R4B2 w - - 8 43", "g2-f3", "Pawn | Capture") },
                new [] {new TestMoveData("Pawn takes piece that gives check", "3k3r/r1p5/4n3/pp3pp1/P1K5/N4P1B/1R3B1p/5R2 w - - 0 42", "a4-b5", "Pawn | Capture") },
                new [] {new TestMoveData("Pawn takes piece that gives check en-passant1", "q3kbr1/1rpb2n1/2P4p/p3ppP1/2p1KB1P/1PP4R/R3P1P1/3Q1BN1 w - f6 0 26", "g5-f6", "Pawn | Capture | EnPassant") },
                new [] {new TestMoveData("Pawn takes piece that gives check en-passant2", "8/8/6b1/2k2NP1/p1pP4/P7/6K1/8 b - d3 0 96", "c4-d3", "Pawn | Capture | EnPassant") },
                new [] {new TestMoveData("Pawn takes piece that gives check en-passant3", "k7/8/1P4B1/1P2Pp1P/2pPK1QN/7Q/8/8 w - f6 0 93", "e5-f6", "Pawn | Capture | EnPassant") },
                new [] {new TestMoveData("White pawn takes en-passant and block check", "1r6/2pk1p2/p2b4/PprN1b2/4n2p/4RP1p/3p4/1K6 w - b6 0 48", "a5-b6", "Pawn | Capture | EnPassant") },
                new [] {new TestMoveData("Pawn takes piece that gives check en-passant4", "r4bn1/5p2/pp1Nq2Q/n1kppb2/PPp5/2P1P3/3P2P1/R1B1K1NR b - b3 0 30", "c4-b3", "Pawn | Capture | EnPassant") },
                // ReSharper restore StringLiteralTypo
            };
        }

    }
}