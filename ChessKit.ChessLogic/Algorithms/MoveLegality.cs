using System;
using ChessKit.ChessLogic.Primitives;
using S = ChessKit.ChessLogic.Cells;
using static ChessKit.ChessLogic.Primitives.MoveAnnotations;

namespace ChessKit.ChessLogic.Algorithms
{
    static partial class MoveLegality
    {
        public static AnalyzedMove Validate(this Position position, Move move)
        {
            return BoardUpdater.MakeMove(position, move);
        }

        public static LegalMove ValidateLegal(this Position position, Move move)
        {
            var analyzedMove = BoardUpdater.MakeMove(position, move);
            var legalMove = analyzedMove as LegalMove;
            if (legalMove == null)
                throw new Exception(analyzedMove.Annotations.ToString());
            return legalMove;
        }

        static MoveAnnotations ValidateWhiteCastlingMove(byte[] cells, int fromSquare, int toSquare, Castlings availableCastlings)
        {
            if (fromSquare != S.E1) return King | DoesNotMoveThisWay;
            switch (toSquare)
            {
                case S.C1: // Queenside
                    if (cells[S.D1] != 0 || cells[S.B1] != 0) return King | DoesNotJump | WQ;
                    if (cells[S.C1] != 0) return King | Capture | DoesNotCaptureThisWay | WQ;
                    if ((availableCastlings & Castlings.WQ) == 0) return King | WQ | HasNoCastling;
                    if (cells.IsSquareAttackedByBlack(S.E1)) return King | CastleFromCheck | WQ;
                    if (cells.IsSquareAttackedByBlack(S.D1)) return King | CastleThroughCheck | WQ;
                    return King | WQ;
                case S.G1: // Kingside
                    if (cells[S.F1] != 0) return King | DoesNotJump | WK;
                    if (cells[S.G1] != 0) return King | Capture | DoesNotCaptureThisWay | WK;
                    if ((availableCastlings & Castlings.WK) == 0) return King | WK | HasNoCastling;
                    if (cells.IsSquareAttackedByBlack(S.E1)) return King | CastleFromCheck | WK;
                    if (cells.IsSquareAttackedByBlack(S.F1)) return King | CastleThroughCheck | WK;
                    return King | WK;
            }
            return King | DoesNotMoveThisWay;
        }
        static MoveAnnotations ValidateBlackCastlingMove(byte[] cells, int fromSquare, int toSquare, Castlings availableCastlings)
        {
            if (fromSquare != S.E8) return King | DoesNotMoveThisWay;
            switch (toSquare)
            {
                case S.C8: // Queenside
                    if (cells[S.D8] != 0 || cells[S.B8] != 0) return King | DoesNotJump | BQ;
                    if (cells[S.C8] != 0) return King | Capture | DoesNotCaptureThisWay | BQ;
                    if ((availableCastlings & Castlings.BQ) == 0) return King | BQ | HasNoCastling;
                    if (cells.IsSquareAttackedByWhite(S.E8)) return King | CastleFromCheck | BQ;
                    if (cells.IsSquareAttackedByWhite(S.D8)) return King | CastleThroughCheck | BQ;
                    return King | BQ;
                case S.G8: // Kingside
                    if (cells[S.F8] != 0) return King | DoesNotJump | BK;
                    if (cells[S.G8] != 0) return King | Capture | DoesNotCaptureThisWay | BK;
                    if ((availableCastlings & Castlings.BK) == 0) return King | BK | HasNoCastling;
                    if (cells.IsSquareAttackedByWhite(S.E8)) return King | CastleFromCheck | BK;
                    if (cells.IsSquareAttackedByWhite(S.F8)) return King | CastleThroughCheck | BK;
                    return King | BK;
            }
            return King | DoesNotMoveThisWay;
        }
        static MoveAnnotations ValidateWhitePawnMove(byte[] cells, int fromSquare, int toSquare, Piece capture)
        {
            switch (toSquare - fromSquare)
            {
                case 32:
                    if (fromSquare / 16 != 1) return Pawn | DoesNotMoveThisWay;
                    if (capture != 0) return Pawn | DoesNotCaptureThisWay;
                    return cells[fromSquare + 16] != 0
                             ? Pawn | DoesNotJump : Pawn | DoublePush;
                case 16:
                    if (capture != 0) return Pawn | DoesNotCaptureThisWay;
                    return toSquare / 16 != 7 ? Pawn : Pawn | Promotion;
                case 17:
                case 15:
                    if (capture == Piece.EmptyCell)
                        return (toSquare / 16 == 5)
                            && cells[toSquare] == (byte)None
                            && (cells[toSquare - 16] & (byte)AllPieces) == (byte)Pawn
                          ? Pawn | Capture | EnPassant : Pawn | OnlyCapturesThisWay;
                    return toSquare / 16 != 7 ? Pawn | Capture
                      : Pawn | Promotion | Capture;
                default:
                    return Pawn | DoesNotMoveThisWay;
            }

        }
        static MoveAnnotations ValidateBlackPawnMove(byte[] cells, int fromSquare, int toSquare, Piece capture)
        {
            switch (fromSquare - toSquare)
            {
                case 32:
                    if (fromSquare / 16 != 6) return Pawn | DoesNotMoveThisWay;
                    if (capture != 0) return Pawn | DoesNotCaptureThisWay;
                    return cells[fromSquare - 16] != 0
                             ? Pawn | DoesNotJump : Pawn | DoublePush;
                case 16:
                    if (capture != 0) return Pawn | DoesNotCaptureThisWay;
                    return toSquare / 16 != 0 ? Pawn : Pawn | Promotion;
                case 17:
                case 15:
                    if (capture == Piece.EmptyCell)
                        return (toSquare / 16 == 2)
                            && cells[toSquare] == (byte)None
                            && (cells[toSquare + 16] & (byte)AllPieces) == (byte)Pawn
                          ? Pawn | Capture | EnPassant : Pawn | OnlyCapturesThisWay;
                    return toSquare / 16 != 0
                      ? Pawn | Capture : Pawn | Promotion | Capture;
                default:
                    return Pawn | DoesNotMoveThisWay;
            }
        }
    }
}
