using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessKit.ChessLogic
{
    partial class Board
    {
        private const int BytesCount = 128;

        private readonly byte[] _cells;
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
            _whiteKingPosition = Position.All.SingleOrDefault(p => this[p] == CompactPiece.WhiteKing).CompactValue;
            _blackKingPosition = Position.All.SingleOrDefault(p => this[p] == CompactPiece.BlackKing).CompactValue;
        }
        public CompactPiece this[int compactPosition]
        {
            get
            {
                return (CompactPiece)_cells[compactPosition];
            }
            set
            {
                _cells[compactPosition] = (byte)value;

                switch (value)
                {
                    case CompactPiece.WhiteKing:
                        _whiteKingPosition = compactPosition;
                        break;
                    case CompactPiece.BlackKing:
                        _blackKingPosition = compactPosition;
                        break;
                }
            }
        }

        MoveAnnotations ValidateWhiteCastlingMove(int fromSquare, int to, Caslings caslings)
        {
            if (fromSquare != S.E1) return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
            switch (to)
            {
                case S.C1: // Queenside
                    if (_cells[S.D1] != 0 || _cells[S.B1] != 0) return MoveAnnotations.DoesNotJump | MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling;
                    if (_cells[S.C1] != 0) return MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling;
                    if (IsAttackedByBlack(S.E1)) return MoveAnnotations.CastleFromCheck | MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling;
                    if (IsAttackedByBlack(S.D1)) return MoveAnnotations.CastleThroughCheck | MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling;
                    if ((caslings & Caslings.WhiteQueen) != 0) return MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling;
                    return MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling | MoveAnnotations.HasNoCastling;
                case S.G1: // Kingside
                    if (_cells[S.F1] != 0) return MoveAnnotations.DoesNotJump | MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling;
                    if (_cells[S.G1] != 0) return MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling;
                    if (IsAttackedByBlack(S.E1)) return MoveAnnotations.CastleFromCheck | MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling;
                    if (IsAttackedByBlack(S.F1)) return MoveAnnotations.CastleThroughCheck | MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling;
                    if ((caslings & Caslings.WhiteKing) != 0) return MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling;
                    return MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling | MoveAnnotations.HasNoCastling;
            }
            return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
        }
        MoveAnnotations ValidateBlackCastlingMove(int fromSquare, int to, Caslings caslings)
        {
            if (fromSquare != S.E8) return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
            switch (to)
            {
                case S.C8: // Queenside
                    if (_cells[S.D8] != 0 || _cells[S.B8] != 0) return MoveAnnotations.DoesNotJump | MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling;
                    if (_cells[S.C8] != 0) return MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling;
                    if (IsAttackedByWhite(S.E8)) return MoveAnnotations.CastleFromCheck | MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling;
                    if (IsAttackedByWhite(S.D8)) return MoveAnnotations.CastleThroughCheck | MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling;
                    if ((caslings & Caslings.BlackQueen) != 0) return MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling;
                    return MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling | MoveAnnotations.HasNoCastling;
                case S.G8: // Kingside
                    if (_cells[S.F8] != 0) return MoveAnnotations.DoesNotJump | MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling;
                    if (_cells[S.G8] != 0) return MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling;
                    if (IsAttackedByWhite(S.E8)) return MoveAnnotations.CastleFromCheck | MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling;
                    if (IsAttackedByWhite(S.F8)) return MoveAnnotations.CastleThroughCheck | MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling;
                    if ((caslings & Caslings.BlackKing) != 0) return MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling;
                    return MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling | MoveAnnotations.HasNoCastling;
            }
            return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
        }
        MoveAnnotations ValidateWhitePawnMove(int fromSquare, int to, CompactPiece toPiece)
        {
            switch (to - fromSquare)
            {
                case 32:
                    if (fromSquare / 16 != 1) return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
                    if (toPiece != 0)
                        return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
                    return _cells[fromSquare + 16] != 0
                             ? MoveAnnotations.Pawn | MoveAnnotations.DoesNotJump
                             : MoveAnnotations.Pawn | MoveAnnotations.PawnDoublePush;
                case 16:
                    if (toPiece != 0)
                        return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
                    return to / 16 != 7 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion;
                case 17:
                case 15:
                    if (toPiece == CompactPiece.EmptyCell)
                        return to / 16 == 5
                          ? MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant
                          : MoveAnnotations.Pawn | MoveAnnotations.OnlyCapturesThisWay;
                    return to / 16 != 7 ? MoveAnnotations.Pawn | MoveAnnotations.Capture
                      : MoveAnnotations.Pawn | MoveAnnotations.Promotion | MoveAnnotations.Capture;
                default:
                    return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
            }

        }
        MoveAnnotations ValidateBlackPawnMove(int fromSquare, int to, CompactPiece toPiece)
        {
            switch (fromSquare - to)
            {
                case 32:
                    if (fromSquare / 16 != 6) return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
                    if (toPiece != 0)
                        return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
                    return _cells[fromSquare - 16] != 0
                             ? MoveAnnotations.Pawn | MoveAnnotations.DoesNotJump
                             : MoveAnnotations.Pawn | MoveAnnotations.PawnDoublePush;
                case 16:
                    if (toPiece != 0)
                        return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
                    return to / 16 != 0 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion;
                case 17:
                case 15:
                    if (toPiece == CompactPiece.EmptyCell)
                        return to / 16 == 2
                          ? MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant
                          : MoveAnnotations.Pawn | MoveAnnotations.OnlyCapturesThisWay;
                    return to / 16 != 0
                      ? MoveAnnotations.Pawn | MoveAnnotations.Capture
                      : MoveAnnotations.Pawn | MoveAnnotations.Promotion | MoveAnnotations.Capture;
                default:
                    return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
            }
        }
        void GenerateWhiteCastlingMoves(int fromSquare, Caslings caslings, List<Move> collector)
        {
            if (fromSquare != S.E1) return;

            if ((caslings & Caslings.WhiteQueen) != 0)
                if (_cells[S.D1] == 0 && _cells[S.C1] == 0 && _cells[S.B1] == 0)
                    if (!IsAttackedByBlack(S.E1) && !IsAttackedByBlack(S.D1) && !IsAttackedByBlack(S.C1))
                        collector.Add(new Move(S.E1, S.C1,
                            MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling));

            if ((caslings & Caslings.WhiteKing) != 0)
                if (_cells[S.F1] == 0 && _cells[S.G1] == 0)
                    if (!IsAttackedByBlack(S.E1) && !IsAttackedByBlack(S.F1) && !IsAttackedByBlack(S.G1))
                        collector.Add(new Move(S.E1, S.G1,
                            MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling));
        }
        void GenerateBlackCastlingMoves(int fromSquare, Caslings caslings, List<Move> collector)
        {
            if (fromSquare != S.E8) return;

            if ((caslings & Caslings.BlackQueen) != 0)
                if (_cells[S.D8] == 0 && _cells[S.C8] == 0 && _cells[S.B8] == 0)
                    if (!IsAttackedByWhite(S.E8) && !IsAttackedByWhite(S.D8) && !IsAttackedByWhite(S.C8))
                        collector.Add(new Move(S.E8, S.C8,
                            MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling));

            if ((caslings & Caslings.BlackKing) != 0)
                if (_cells[S.F8] == 0 && _cells[S.G8] == 0)
                    if (!IsAttackedByWhite(S.E8) && !IsAttackedByWhite(S.F8) && !IsAttackedByWhite(S.G8))
                        collector.Add(new Move(S.E8, S.G8,
                            MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling));
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
                        _cells[to] = (byte)CompactPiece.WhitePawn;
                        if (!IsAttackedByBlack(_whiteKingPosition))
                            collector.Add(new Move(fromSquare, to,
                                to / 16 != 7 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion));
                        _cells[fromSquare] = (byte)CompactPiece.WhitePawn;
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
                        _cells[to] = (byte)CompactPiece.WhitePawn;
                        if (!IsAttackedByBlack(_whiteKingPosition))
                            collector.Add(new Move(fromSquare, to,
                                MoveAnnotations.Pawn | MoveAnnotations.PawnDoublePush));
                        _cells[fromSquare] = (byte)CompactPiece.WhitePawn;
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
                        if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)CompactPiece.WhitePawn;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    to / 16 != 7
                                    ? MoveAnnotations.Pawn | MoveAnnotations.Capture
                                    : MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
                            _cells[fromSquare] = (byte)CompactPiece.WhitePawn;
                            _cells[to] = toPiece;
                        }
                    }
                    else
                    {
                        if (enPassantFile == (to % 16) && to / 16 == 5)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)CompactPiece.WhitePawn;
                            _cells[to - 16] = 0;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
                            _cells[fromSquare] = (byte)CompactPiece.WhitePawn;
                            _cells[to - 16] = (byte)CompactPiece.BlackPawn;
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
                        if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)CompactPiece.WhitePawn;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    to / 16 != 7 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
                            _cells[fromSquare] = (byte)CompactPiece.WhitePawn;
                            _cells[to] = toPiece;
                        }
                    }
                    else
                    {
                        if (enPassantFile == (to % 16) && to / 16 == 5)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)CompactPiece.WhitePawn;
                            _cells[to - 16] = 0;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
                            _cells[fromSquare] = (byte)CompactPiece.WhitePawn;
                            _cells[to] = (byte)CompactPiece.EmptyCell;
                            _cells[to - 16] = (byte)CompactPiece.BlackPawn;
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
                        _cells[to] = (byte)CompactPiece.BlackPawn;
                        if (!IsAttackedByWhite(_blackKingPosition))
                            collector.Add(new Move(fromSquare, to,
                                to / 16 != 0 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion));
                        _cells[fromSquare] = (byte)CompactPiece.BlackPawn;
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
                        _cells[to] = (byte)CompactPiece.BlackPawn;
                        if (!IsAttackedByWhite(_blackKingPosition))
                            collector.Add(new Move(fromSquare, to,
                                MoveAnnotations.Pawn | MoveAnnotations.PawnDoublePush));
                        _cells[fromSquare] = (byte)CompactPiece.BlackPawn;
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
                        if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)CompactPiece.BlackPawn;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    to / 16 != 0 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
                            _cells[fromSquare] = (byte)CompactPiece.BlackPawn;
                            _cells[to] = toPiece;
                        }
                    }
                    else
                    {
                        if (enPassantFile == (to % 16) && to / 16 == 2)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to + 16] = 0;
                            _cells[to] = (byte)CompactPiece.BlackPawn;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
                            _cells[fromSquare] = (byte)CompactPiece.BlackPawn;
                            _cells[to] = toPiece;
                            _cells[to + 16] = (byte)CompactPiece.WhitePawn;
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
                        if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)CompactPiece.BlackPawn;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    to / 16 != 0 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
                            _cells[fromSquare] = (byte)CompactPiece.BlackPawn;
                            _cells[to] = (byte)toPiece;
                        }
                    }
                    else
                    {
                        if (enPassantFile == (to % 16) && to / 16 == 2)
                        {
                            _cells[fromSquare] = 0;
                            _cells[to] = (byte)CompactPiece.BlackPawn;
                            _cells[to + 16] = 0;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
                            _cells[fromSquare] = (byte)CompactPiece.BlackPawn;
                            _cells[to + 16] = (byte)CompactPiece.WhitePawn;
                            _cells[to] = toPiece;
                        }
                    }
                }
            }
        }

        private bool IsUnderCheck(PieceColor kingColor)
        {
            return kingColor == PieceColor.White
              ? IsAttackedByBlack(_whiteKingPosition)
              : IsAttackedByWhite(_blackKingPosition);
        }
    }
}
