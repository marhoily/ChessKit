using System;
using System.Collections.Generic;
using System.Linq;
using ChessKit.ChessLogic.Enums;
using static ChessKit.ChessLogic.Enums.MoveAnnotations;

namespace ChessKit.ChessLogic
{
    partial class Board
    {
        private const int BytesCount = 128;

        internal readonly byte[] _cells;
        private int _whiteKingPosition;
        private int _blackKingPosition;

        /// <summary>Deep copy</summary>
        private Board(Board source)
        {
            _whiteKingPosition = source._whiteKingPosition;
            _blackKingPosition = source._blackKingPosition;

            _cells = new byte[BytesCount];
            Buffer.BlockCopy(source._cells, 0, _cells, 0, BytesCount);
        }

        /// <summary>Empty board</summary>
        private Board()
        {
            _cells = new byte[BytesCount];
            _whiteKingPosition = -1;
            _blackKingPosition = -1;
        }

        internal Board(BoardBuilder boardBuilder)
        {
            _cells = new byte[BytesCount];
            Buffer.BlockCopy(boardBuilder._cells, 0, _cells, 0, BytesCount);
            SideOnMove = boardBuilder.SideOnMove;
            EnPassantFile = boardBuilder.EnPassantFile;
            HalfMoveClock = boardBuilder.HalfMoveClock;
            MoveNumber = boardBuilder.MoveNumber;
            _whiteKingPosition = CoordinateExtensions.All.SingleOrDefault(p => this[p] == Piece.WhiteKing);
            _blackKingPosition = CoordinateExtensions.All.SingleOrDefault(p => this[p] == Piece.BlackKing);
            Castlings = boardBuilder.CastlingAvailability;
        }
        public Piece this[int compactPosition]
        {
            get
            {
                return (Piece)_cells[compactPosition];
            }
            set
            {
                _cells[compactPosition] = (byte)value;

                switch (value)
                {
                    case Piece.WhiteKing:
                        _whiteKingPosition = compactPosition;
                        break;
                    case Piece.BlackKing:
                        _blackKingPosition = compactPosition;
                        break;
                }
            }
        }

        MoveAnnotations ValidateWhiteCastlingMove(int fromSquare, int to, Castlings castlings)
        {
            if (fromSquare != S.E1) return King | DoesNotMoveThisWay;
            switch (to)
            {
                case S.C1: // Queenside
                    if (_cells[S.D1] != 0 || _cells[S.B1] != 0) return King | DoesNotJump | WQ;
                    if (_cells[S.C1] != 0) return King | Capture | DoesNotCaptureThisWay | WQ;
                    if ((castlings & Castlings.WQ) == 0) return King | WQ | HasNoCastling;
                    if (IsAttackedByBlack(S.E1)) return King | CastleFromCheck | WQ;
                    if (IsAttackedByBlack(S.D1)) return King | CastleThroughCheck | WQ;
                    return King | WQ;
                case S.G1: // Kingside
                    if (_cells[S.F1] != 0) return King | DoesNotJump | WK;
                    if (_cells[S.G1] != 0) return King | Capture | DoesNotCaptureThisWay | WK;
                    if ((castlings & Castlings.WK) == 0) return King | WK | HasNoCastling;
                    if (IsAttackedByBlack(S.E1)) return King | CastleFromCheck | WK;
                    if (IsAttackedByBlack(S.F1)) return King | CastleThroughCheck | WK;
                    return King | WK;
            }
            return King | DoesNotMoveThisWay;
        }
        MoveAnnotations ValidateBlackCastlingMove(int fromSquare, int to, Castlings castlings)
        {
            if (fromSquare != S.E8) return King | DoesNotMoveThisWay;
            switch (to)
            {
                case S.C8: // Queenside
                    if (_cells[S.D8] != 0 || _cells[S.B8] != 0) return King | DoesNotJump | BQ;
                    if (_cells[S.C8] != 0) return King | Capture | DoesNotCaptureThisWay | BQ;
                    if ((castlings & Castlings.BQ) == 0) return King | BQ | HasNoCastling;
                    if (IsAttackedByWhite(S.E8)) return King | CastleFromCheck | BQ;
                    if (IsAttackedByWhite(S.D8)) return King | CastleThroughCheck | BQ;
                    return King | BQ;
                case S.G8: // Kingside
                    if (_cells[S.F8] != 0) return King | DoesNotJump | BK;
                    if (_cells[S.G8] != 0) return King | Capture | DoesNotCaptureThisWay | BK;
                    if ((castlings & Castlings.BK) == 0) return King | BK | HasNoCastling;
                    if (IsAttackedByWhite(S.E8)) return King | CastleFromCheck | BK;
                    if (IsAttackedByWhite(S.F8)) return King | CastleThroughCheck | BK;
                    return King | BK;
            }
            return King | DoesNotMoveThisWay;
        }
        MoveAnnotations ValidateWhitePawnMove(int fromSquare, int to, Piece toPiece)
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
                            && _cells[to] == (byte) None
                            && (_cells[to - 16] & (byte)AllPieces) == (byte)Pawn
                          ? Pawn | Capture | EnPassant
                          : Pawn | OnlyCapturesThisWay;
                    return to / 16 != 7 ? Pawn | Capture
                      : Pawn | Promotion | Capture;
                default:
                    return Pawn | DoesNotMoveThisWay;
            }

        }
        MoveAnnotations ValidateBlackPawnMove(int fromSquare, int to, Piece toPiece)
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
        void GenerateWhiteCastlingMoves(int fromSquare, Castlings castlings, List<Move> collector)
        {
            if (fromSquare != S.E1) return;

            if ((castlings & Castlings.WQ) != 0)
                if (_cells[S.D1] == 0 && _cells[S.C1] == 0 && _cells[S.B1] == 0)
                    if (!IsAttackedByBlack(S.E1) && !IsAttackedByBlack(S.D1) && !IsAttackedByBlack(S.C1))
                        collector.Add(new Move(S.E1, S.C1, WQ));

            if ((castlings & Castlings.WK) != 0)
                if (_cells[S.F1] == 0 && _cells[S.G1] == 0)
                    if (!IsAttackedByBlack(S.E1) && !IsAttackedByBlack(S.F1) && !IsAttackedByBlack(S.G1))
                        collector.Add(new Move(S.E1, S.G1, WK));
        }
        void GenerateBlackCastlingMoves(int fromSquare, Castlings castlings, List<Move> collector)
        {
            if (fromSquare != S.E8) return;

            if ((castlings & Castlings.BQ) != 0)
                if (_cells[S.D8] == 0 && _cells[S.C8] == 0 && _cells[S.B8] == 0)
                    if (!IsAttackedByWhite(S.E8) && !IsAttackedByWhite(S.D8) && !IsAttackedByWhite(S.C8))
                        collector.Add(new Move(S.E8, S.C8, BQ));

            if ((castlings & Castlings.BK) != 0)
                if (_cells[S.F8] == 0 && _cells[S.G8] == 0)
                    if (!IsAttackedByWhite(S.E8) && !IsAttackedByWhite(S.F8) && !IsAttackedByWhite(S.G8))
                        collector.Add(new Move(S.E8, S.G8, BK));
        }
        void GenerateWhitePawnMoves(int fromSquare, int? enPassantFile, List<Move> collector)
        {
            {
                var to = fromSquare + 16;
                if ((to & 0x88) == 0)
                    if (_cells[to] == 0)
                    {
                        _cells[fromSquare] = 0;
                        var toPiece = _cells[to];
                        _cells[to] = (byte)Piece.WhitePawn;
                        if (!IsAttackedByBlack(_whiteKingPosition))
                            collector.Add(new Move(fromSquare, to,
                                to / 16 != 7 ? Pawn : Pawn | Promotion));
                        _cells[fromSquare] = (byte)Piece.WhitePawn;
                        _cells[to] = toPiece;
                    }
            }
            if ((fromSquare / 16) == 1)
            {
                if (_cells[fromSquare + 16] == 0)
                {
                    var to = fromSquare + 32;
                    if (_cells[to] == 0)
                    {
                        _cells[fromSquare] = 0;
                        var toPiece = _cells[to];
                        _cells[to] = (byte)Piece.WhitePawn;
                        if (!IsAttackedByBlack(_whiteKingPosition))
                            collector.Add(new Move(fromSquare, to, Pawn | DoublePush));
                        _cells[fromSquare] = (byte)Piece.WhitePawn;
                        _cells[to] = toPiece;
                    }
                }
            }
            {
                var to = fromSquare + 17;
                if ((to & 0x88) == 0)
                {
                    var toPiece = _cells[to];
                    if (toPiece != 0)
                    {
                        if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)Piece.WhitePawn;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    to / 16 != 7
                                    ? Pawn | Capture
                                    : Pawn | Capture | Promotion));
                            _cells[fromSquare] = (byte)Piece.WhitePawn;
                            _cells[to] = toPiece;
                        }
                    }
                    else
                    {
                        if (enPassantFile == (to % 16) && to / 16 == 5)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)Piece.WhitePawn;
                            _cells[to - 16] = 0;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    Pawn | Capture | EnPassant));
                            _cells[fromSquare] = (byte)Piece.WhitePawn;
                            _cells[to - 16] = (byte)Piece.BlackPawn;
                            _cells[to] = toPiece;
                        }
                    }
                }
            }
            {
                var to = fromSquare + 15;
                if ((to & 0x88) == 0)
                {
                    var toPiece = _cells[to];
                    if (toPiece != 0)
                    {
                        if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)Piece.WhitePawn;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    to / 16 != 7 ? Pawn | Capture :
                                    Pawn | Capture | Promotion));
                            _cells[fromSquare] = (byte)Piece.WhitePawn;
                            _cells[to] = toPiece;
                        }
                    }
                    else
                    {
                        if (enPassantFile == (to % 16) && to / 16 == 5)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)Piece.WhitePawn;
                            _cells[to - 16] = 0;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    Pawn | Capture | EnPassant));
                            _cells[fromSquare] = (byte)Piece.WhitePawn;
                            _cells[to] = (byte)Piece.EmptyCell;
                            _cells[to - 16] = (byte)Piece.BlackPawn;
                        }
                    }
                }
            }
        }
        void GenerateBlackPawnMoves(int fromSquare, int? enPassantFile, List<Move> collector)
        {
            {
                var to = fromSquare - 16;
                if ((to & 0x88) == 0)
                    if (_cells[to] == 0)
                    {
                        _cells[fromSquare] = 0;
                        var toPiece = _cells[to];
                        _cells[to] = (byte)Piece.BlackPawn;
                        if (!IsAttackedByWhite(_blackKingPosition))
                            collector.Add(new Move(fromSquare, to,
                                to / 16 != 0 ? Pawn : Pawn | Promotion));
                        _cells[fromSquare] = (byte)Piece.BlackPawn;
                        _cells[to] = toPiece;
                    }
            }
            if ((fromSquare / 16) == 6)
            {
                if (_cells[fromSquare - 16] == 0)
                {
                    var to = fromSquare - 32;
                    if (_cells[to] == 0)
                    {
                        _cells[fromSquare] = 0;
                        var toPiece = _cells[to];
                        _cells[to] = (byte)Piece.BlackPawn;
                        if (!IsAttackedByWhite(_blackKingPosition))
                            collector.Add(new Move(fromSquare, to,
                                Pawn | DoublePush));
                        _cells[fromSquare] = (byte)Piece.BlackPawn;
                        _cells[to] = toPiece;
                    }
                }
            }
            {
                var to = fromSquare - 17;
                if ((to & 0x88) == 0)
                {
                    var toPiece = _cells[to];
                    if (toPiece != 0)
                    {
                        if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)Piece.BlackPawn;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    to / 16 != 0 ? Pawn | Capture :
                                    Pawn | Capture | Promotion));
                            _cells[fromSquare] = (byte)Piece.BlackPawn;
                            _cells[to] = toPiece;
                        }
                    }
                    else
                    {
                        if (enPassantFile == (to % 16) && to / 16 == 2)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to + 16] = 0;
                            _cells[to] = (byte)Piece.BlackPawn;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    Pawn | Capture | EnPassant));
                            _cells[fromSquare] = (byte)Piece.BlackPawn;
                            _cells[to] = toPiece;
                            _cells[to + 16] = (byte)Piece.WhitePawn;
                        }
                    }
                }
            }
            {
                var to = fromSquare - 15;
                if ((to & 0x88) == 0)
                {
                    var toPiece = _cells[to];
                    if (toPiece != 0)
                    {
                        if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)Piece.BlackPawn;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    to / 16 != 0 ? Pawn | Capture :
                                    Pawn | Capture | Promotion));
                            _cells[fromSquare] = (byte)Piece.BlackPawn;
                            _cells[to] = toPiece;
                        }
                    }
                    else
                    {
                        if (enPassantFile == (to % 16) && to / 16 == 2)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)Piece.BlackPawn;
                            _cells[to + 16] = 0;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    Pawn | Capture | EnPassant));
                            _cells[fromSquare] = (byte)Piece.BlackPawn;
                            _cells[to + 16] = (byte)Piece.WhitePawn;
                            _cells[to] = toPiece;
                        }
                    }
                }
            }
        }

        private bool IsUnderCheck(Color kingColor)
        {
            return kingColor == Color.White
              ? IsAttackedByBlack(_whiteKingPosition)
              : IsAttackedByWhite(_blackKingPosition);
        }
    }
}
