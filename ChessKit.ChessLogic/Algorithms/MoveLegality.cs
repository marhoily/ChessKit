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

        static MoveAnnotations ValidateWhiteCastlingMove(byte[] _cells, int fromSquare, int to, Castlings castlings)
        {
            if (fromSquare != S.E1) return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
            switch (to)
            {
                case S.C1: // Queenside
                    if (_cells[S.D1] != 0 || _cells[S.B1] != 0) return MoveAnnotations.King | MoveAnnotations.DoesNotJump | MoveAnnotations.WQ;
                    if (_cells[S.C1] != 0) return MoveAnnotations.King | MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.WQ;
                    if ((castlings & Castlings.WQ) == 0) return MoveAnnotations.King | MoveAnnotations.WQ | MoveAnnotations.HasNoCastling;
                    if (Scanning.IsAttackedByBlack(_cells, S.E1)) return MoveAnnotations.King | MoveAnnotations.CastleFromCheck | MoveAnnotations.WQ;
                    if (Scanning.IsAttackedByBlack(_cells, S.D1)) return MoveAnnotations.King | MoveAnnotations.CastleThroughCheck | MoveAnnotations.WQ;
                    return MoveAnnotations.King | MoveAnnotations.WQ;
                case S.G1: // Kingside
                    if (_cells[S.F1] != 0) return MoveAnnotations.King | MoveAnnotations.DoesNotJump | MoveAnnotations.WK;
                    if (_cells[S.G1] != 0) return MoveAnnotations.King | MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.WK;
                    if ((castlings & Castlings.WK) == 0) return MoveAnnotations.King | MoveAnnotations.WK | MoveAnnotations.HasNoCastling;
                    if (Scanning.IsAttackedByBlack(_cells, S.E1)) return MoveAnnotations.King | MoveAnnotations.CastleFromCheck | MoveAnnotations.WK;
                    if (Scanning.IsAttackedByBlack(_cells, S.F1)) return MoveAnnotations.King | MoveAnnotations.CastleThroughCheck | MoveAnnotations.WK;
                    return MoveAnnotations.King | MoveAnnotations.WK;
            }
            return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
        }
        static MoveAnnotations ValidateBlackCastlingMove(byte[] _cells, int fromSquare, int to, Castlings castlings)
        {
            if (fromSquare != S.E8) return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
            switch (to)
            {
                case S.C8: // Queenside
                    if (_cells[S.D8] != 0 || _cells[S.B8] != 0) return MoveAnnotations.King | MoveAnnotations.DoesNotJump | MoveAnnotations.BQ;
                    if (_cells[S.C8] != 0) return MoveAnnotations.King | MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.BQ;
                    if ((castlings & Castlings.BQ) == 0) return MoveAnnotations.King | MoveAnnotations.BQ | MoveAnnotations.HasNoCastling;
                    if (Scanning.IsAttackedByWhite(_cells, S.E8)) return MoveAnnotations.King | MoveAnnotations.CastleFromCheck | MoveAnnotations.BQ;
                    if (Scanning.IsAttackedByWhite(_cells, S.D8)) return MoveAnnotations.King | MoveAnnotations.CastleThroughCheck | MoveAnnotations.BQ;
                    return MoveAnnotations.King | MoveAnnotations.BQ;
                case S.G8: // Kingside
                    if (_cells[S.F8] != 0) return MoveAnnotations.King | MoveAnnotations.DoesNotJump | MoveAnnotations.BK;
                    if (_cells[S.G8] != 0) return MoveAnnotations.King | MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.BK;
                    if ((castlings & Castlings.BK) == 0) return MoveAnnotations.King | MoveAnnotations.BK | MoveAnnotations.HasNoCastling;
                    if (Scanning.IsAttackedByWhite(_cells, S.E8)) return MoveAnnotations.King | MoveAnnotations.CastleFromCheck | MoveAnnotations.BK;
                    if (Scanning.IsAttackedByWhite(_cells, S.F8)) return MoveAnnotations.King | MoveAnnotations.CastleThroughCheck | MoveAnnotations.BK;
                    return MoveAnnotations.King | MoveAnnotations.BK;
            }
            return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
        }
        static MoveAnnotations ValidateWhitePawnMove(byte[] _cells, int fromSquare, int to, Piece toPiece)
        {
            switch (to - fromSquare)
            {
                case 32:
                    if (fromSquare / 16 != 1) return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
                    if (toPiece != 0)
                        return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
                    return _cells[fromSquare + 16] != 0
                             ? MoveAnnotations.Pawn | MoveAnnotations.DoesNotJump
                             : MoveAnnotations.Pawn | MoveAnnotations.DoublePush;
                case 16:
                    if (toPiece != 0)
                        return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
                    return to / 16 != 7 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion;
                case 17:
                case 15:
                    if (toPiece == Piece.EmptyCell)
                        return (to / 16 == 5)
                            && _cells[to] == (byte)MoveAnnotations.None
                            && (_cells[to - 16] & (byte)MoveAnnotations.AllPieces) == (byte)MoveAnnotations.Pawn
                          ? MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant
                          : MoveAnnotations.Pawn | MoveAnnotations.OnlyCapturesThisWay;
                    return to / 16 != 7 ? MoveAnnotations.Pawn | MoveAnnotations.Capture
                      : MoveAnnotations.Pawn | MoveAnnotations.Promotion | MoveAnnotations.Capture;
                default:
                    return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
            }

        }
        static MoveAnnotations ValidateBlackPawnMove(byte[] _cells, int fromSquare, int to, Piece toPiece)
        {
            switch (fromSquare - to)
            {
                case 32:
                    if (fromSquare / 16 != 6) return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
                    if (toPiece != 0)
                        return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
                    return _cells[fromSquare - 16] != 0
                             ? MoveAnnotations.Pawn | MoveAnnotations.DoesNotJump
                             : MoveAnnotations.Pawn | MoveAnnotations.DoublePush;
                case 16:
                    if (toPiece != 0)
                        return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
                    return to / 16 != 0 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion;
                case 17:
                case 15:
                    if (toPiece == Piece.EmptyCell)
                        return (to / 16 == 2)
                            && _cells[to] == (byte)MoveAnnotations.None
                            && (_cells[to + 16] & (byte)MoveAnnotations.AllPieces) == (byte)MoveAnnotations.Pawn
                          ? MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant
                          : MoveAnnotations.Pawn | MoveAnnotations.OnlyCapturesThisWay;
                    return to / 16 != 0
                      ? MoveAnnotations.Pawn | MoveAnnotations.Capture
                      : MoveAnnotations.Pawn | MoveAnnotations.Promotion | MoveAnnotations.Capture;
                default:
                    return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
            }
        }
    }
}
