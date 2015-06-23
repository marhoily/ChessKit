using System;
using ChessKit.ChessLogic.N;
using ChessKit.ChessLogic.Primitives;
using static ChessKit.ChessLogic.Primitives.MoveAnnotations;
using S = ChessKit.ChessLogic.Cells;

namespace ChessKit.ChessLogic
{
    static partial class MoveLegality
    {
        public static AnalyzedMove Validate(this Position position, MoveR move)
        {
            return BoardUpdater.MakeMove(position, move);
        }

        public static LegalMove ValidateLegal(this Position position, MoveR move)
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
            if (fromSquare != S.E1) return King | DoesNotMoveThisWay;
            switch (to)
            {
                case S.C1: // Queenside
                    if (_cells[S.D1] != 0 || _cells[S.B1] != 0) return King | DoesNotJump | WQ;
                    if (_cells[S.C1] != 0) return King | Capture | DoesNotCaptureThisWay | WQ;
                    if ((castlings & Castlings.WQ) == 0) return King | WQ | HasNoCastling;
                    if (Scanning.IsAttackedByBlack(_cells, S.E1)) return King | CastleFromCheck | WQ;
                    if (Scanning.IsAttackedByBlack(_cells, S.D1)) return King | CastleThroughCheck | WQ;
                    return King | WQ;
                case S.G1: // Kingside
                    if (_cells[S.F1] != 0) return King | DoesNotJump | WK;
                    if (_cells[S.G1] != 0) return King | Capture | DoesNotCaptureThisWay | WK;
                    if ((castlings & Castlings.WK) == 0) return King | WK | HasNoCastling;
                    if (Scanning.IsAttackedByBlack(_cells, S.E1)) return King | CastleFromCheck | WK;
                    if (Scanning.IsAttackedByBlack(_cells, S.F1)) return King | CastleThroughCheck | WK;
                    return King | WK;
            }
            return King | DoesNotMoveThisWay;
        }
        static MoveAnnotations ValidateBlackCastlingMove(byte[] _cells, int fromSquare, int to, Castlings castlings)
        {
            if (fromSquare != S.E8) return King | DoesNotMoveThisWay;
            switch (to)
            {
                case S.C8: // Queenside
                    if (_cells[S.D8] != 0 || _cells[S.B8] != 0) return King | DoesNotJump | BQ;
                    if (_cells[S.C8] != 0) return King | Capture | DoesNotCaptureThisWay | BQ;
                    if ((castlings & Castlings.BQ) == 0) return King | BQ | HasNoCastling;
                    if (Scanning.IsAttackedByWhite(_cells, S.E8)) return King | CastleFromCheck | BQ;
                    if (Scanning.IsAttackedByWhite(_cells, S.D8)) return King | CastleThroughCheck | BQ;
                    return King | BQ;
                case S.G8: // Kingside
                    if (_cells[S.F8] != 0) return King | DoesNotJump | BK;
                    if (_cells[S.G8] != 0) return King | Capture | DoesNotCaptureThisWay | BK;
                    if ((castlings & Castlings.BK) == 0) return King | BK | HasNoCastling;
                    if (Scanning.IsAttackedByWhite(_cells, S.E8)) return King | CastleFromCheck | BK;
                    if (Scanning.IsAttackedByWhite(_cells, S.F8)) return King | CastleThroughCheck | BK;
                    return King | BK;
            }
            return King | DoesNotMoveThisWay;
        }
        static MoveAnnotations ValidateWhitePawnMove(byte[] _cells, int fromSquare, int to, Piece toPiece)
        {
            switch (to - fromSquare)
            {
                case 32:
                    if (fromSquare / 16 != 1) return Pawn | DoesNotMoveThisWay;
                    if (toPiece != 0)
                        return Pawn | DoesNotCaptureThisWay;
                    return _cells[fromSquare + 16] != 0
                             ? Pawn | DoesNotJump
                             : Pawn | DoublePush;
                case 16:
                    if (toPiece != 0)
                        return Pawn | DoesNotCaptureThisWay;
                    return to / 16 != 7 ? Pawn : Pawn | Promotion;
                case 17:
                case 15:
                    if (toPiece == Piece.EmptyCell)
                        return (to / 16 == 5)
                            && _cells[to] == (byte)None
                            && (_cells[to - 16] & (byte)AllPieces) == (byte)Pawn
                          ? Pawn | Capture | EnPassant
                          : Pawn | OnlyCapturesThisWay;
                    return to / 16 != 7 ? Pawn | Capture
                      : Pawn | Promotion | Capture;
                default:
                    return Pawn | DoesNotMoveThisWay;
            }

        }
        static MoveAnnotations ValidateBlackPawnMove(byte[] _cells, int fromSquare, int to, Piece toPiece)
        {
            switch (fromSquare - to)
            {
                case 32:
                    if (fromSquare / 16 != 6) return Pawn | DoesNotMoveThisWay;
                    if (toPiece != 0)
                        return Pawn | DoesNotCaptureThisWay;
                    return _cells[fromSquare - 16] != 0
                             ? Pawn | DoesNotJump
                             : Pawn | DoublePush;
                case 16:
                    if (toPiece != 0)
                        return Pawn | DoesNotCaptureThisWay;
                    return to / 16 != 0 ? Pawn : Pawn | Promotion;
                case 17:
                case 15:
                    if (toPiece == Piece.EmptyCell)
                        return (to / 16 == 2)
                            && _cells[to] == (byte)None
                            && (_cells[to + 16] & (byte)AllPieces) == (byte)Pawn
                          ? Pawn | Capture | EnPassant
                          : Pawn | OnlyCapturesThisWay;
                    return to / 16 != 0
                      ? Pawn | Capture
                      : Pawn | Promotion | Capture;
                default:
                    return Pawn | DoesNotMoveThisWay;
            }
        }
    }
}
