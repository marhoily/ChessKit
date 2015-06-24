using System;
using ChessKit.ChessLogic.Primitives;
using S = ChessKit.ChessLogic.Cells;

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

            if (legalMove != null)
            {
                return legalMove;
            }
            throw new Exception(analyzedMove.Annotations.ToString());
        }

        static MoveAnnotations ValidateWhiteCastlingMove(byte[] cells, int fromSquare, int toSquare, Castlings availableCastlings)
        {
            if (fromSquare != S.E1) return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
            switch (toSquare)
            {
                case S.C1: // Queenside
                    if (cells[S.D1] != 0 || cells[S.B1] != 0) return MoveAnnotations.King | MoveAnnotations.DoesNotJump | MoveAnnotations.WQ;
                    if (cells[S.C1] != 0) return MoveAnnotations.King | MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.WQ;
                    if ((availableCastlings & Castlings.WQ) == 0) return MoveAnnotations.King | MoveAnnotations.WQ | MoveAnnotations.HasNoCastling;
                    if (cells.IsSquareAttackedByBlack(S.E1)) return MoveAnnotations.King | MoveAnnotations.CastleFromCheck | MoveAnnotations.WQ;
                    if (cells.IsSquareAttackedByBlack(S.D1)) return MoveAnnotations.King | MoveAnnotations.CastleThroughCheck | MoveAnnotations.WQ;
                    return MoveAnnotations.King | MoveAnnotations.WQ;
                case S.G1: // Kingside
                    if (cells[S.F1] != 0) return MoveAnnotations.King | MoveAnnotations.DoesNotJump | MoveAnnotations.WK;
                    if (cells[S.G1] != 0) return MoveAnnotations.King | MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.WK;
                    if ((availableCastlings & Castlings.WK) == 0) return MoveAnnotations.King | MoveAnnotations.WK | MoveAnnotations.HasNoCastling;
                    if (cells.IsSquareAttackedByBlack(S.E1)) return MoveAnnotations.King | MoveAnnotations.CastleFromCheck | MoveAnnotations.WK;
                    if (cells.IsSquareAttackedByBlack(S.F1)) return MoveAnnotations.King | MoveAnnotations.CastleThroughCheck | MoveAnnotations.WK;
                    return MoveAnnotations.King | MoveAnnotations.WK;
            }
            return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
        }
        static MoveAnnotations ValidateBlackCastlingMove(byte[] cells, int fromSquare, int toSquare, Castlings availableCastlings)
        {
            if (fromSquare != S.E8) return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
            switch (toSquare)
            {
                case S.C8: // Queenside
                    if (cells[S.D8] != 0 || cells[S.B8] != 0) return MoveAnnotations.King | MoveAnnotations.DoesNotJump | MoveAnnotations.BQ;
                    if (cells[S.C8] != 0) return MoveAnnotations.King | MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.BQ;
                    if ((availableCastlings & Castlings.BQ) == 0) return MoveAnnotations.King | MoveAnnotations.BQ | MoveAnnotations.HasNoCastling;
                    if (cells.IsSquareAttackedByWhite(S.E8)) return MoveAnnotations.King | MoveAnnotations.CastleFromCheck | MoveAnnotations.BQ;
                    if (cells.IsSquareAttackedByWhite(S.D8)) return MoveAnnotations.King | MoveAnnotations.CastleThroughCheck | MoveAnnotations.BQ;
                    return MoveAnnotations.King | MoveAnnotations.BQ;
                case S.G8: // Kingside
                    if (cells[S.F8] != 0) return MoveAnnotations.King | MoveAnnotations.DoesNotJump | MoveAnnotations.BK;
                    if (cells[S.G8] != 0) return MoveAnnotations.King | MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.BK;
                    if ((availableCastlings & Castlings.BK) == 0) return MoveAnnotations.King | MoveAnnotations.BK | MoveAnnotations.HasNoCastling;
                    if (cells.IsSquareAttackedByWhite(S.E8)) return MoveAnnotations.King | MoveAnnotations.CastleFromCheck | MoveAnnotations.BK;
                    if (cells.IsSquareAttackedByWhite(S.F8)) return MoveAnnotations.King | MoveAnnotations.CastleThroughCheck | MoveAnnotations.BK;
                    return MoveAnnotations.King | MoveAnnotations.BK;
            }
            return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
        }
        static MoveAnnotations ValidateWhitePawnMove(byte[] cells, int fromSquare, int toSquare, Piece capture)
        {
            switch (toSquare - fromSquare)
            {
                case 32:
                    if (fromSquare / 16 != 1) return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
                    if (capture != 0) return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
                    return cells[fromSquare + 16] != 0
                             ? MoveAnnotations.Pawn | MoveAnnotations.DoesNotJump
                             : MoveAnnotations.Pawn | MoveAnnotations.DoublePush;
                case 16:
                    if (capture != 0) return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
                    return toSquare / 16 != 7 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion;
                case 17:
                case 15:
                    if (capture == Piece.EmptyCell)
                        return (toSquare / 16 == 5)
                            && cells[toSquare] == (byte)MoveAnnotations.None
                            && (cells[toSquare - 16] & (byte)MoveAnnotations.AllPieces) == (byte)MoveAnnotations.Pawn
                          ? MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant
                          : MoveAnnotations.Pawn | MoveAnnotations.OnlyCapturesThisWay;
                    return toSquare / 16 != 7 ? MoveAnnotations.Pawn | MoveAnnotations.Capture
                      : MoveAnnotations.Pawn | MoveAnnotations.Promotion | MoveAnnotations.Capture;
                default:
                    return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
            }

        }
        static MoveAnnotations ValidateBlackPawnMove(byte[] cells, int fromSquare, int toSquare, Piece capture)
        {
            switch (fromSquare - toSquare)
            {
                case 32:
                    if (fromSquare / 16 != 6) return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
                    if (capture != 0) return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
                    return cells[fromSquare - 16] != 0
                             ? MoveAnnotations.Pawn | MoveAnnotations.DoesNotJump
                             : MoveAnnotations.Pawn | MoveAnnotations.DoublePush;
                case 16:
                    if (capture != 0) return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
                    return toSquare / 16 != 0 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion;
                case 17:
                case 15:
                    if (capture == Piece.EmptyCell)
                        return (toSquare / 16 == 2)
                            && cells[toSquare] == (byte)MoveAnnotations.None
                            && (cells[toSquare + 16] & (byte)MoveAnnotations.AllPieces) == (byte)MoveAnnotations.Pawn
                          ? MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant
                          : MoveAnnotations.Pawn | MoveAnnotations.OnlyCapturesThisWay;
                    return toSquare / 16 != 0
                      ? MoveAnnotations.Pawn | MoveAnnotations.Capture
                      : MoveAnnotations.Pawn | MoveAnnotations.Promotion | MoveAnnotations.Capture;
                default:
                    return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
            }
        }
    }
}
