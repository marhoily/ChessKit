using System;
using System.Collections.Generic;
using System.Linq;
using ChessKit.ChessLogic.Primitives;
using static ChessKit.ChessLogic.Primitives.MoveAnnotations;
using static ChessKit.ChessLogic.Scanning;

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
            _whiteKingPosition = Coordinates.All.SingleOrDefault(p => this[p] == Piece.WhiteKing);
            _blackKingPosition = Coordinates.All.SingleOrDefault(p => this[p] == Piece.BlackKing);
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

        void GenerateWhiteCastlingMoves(int fromSquare, Castlings castlings, List<Move> collector)
        {
            if (fromSquare != S.E1) return;

            if ((castlings & Castlings.WQ) != 0)
                if (_cells[S.D1] == 0 && _cells[S.C1] == 0 && _cells[S.B1] == 0)
                    if (!IsAttackedByBlack(_cells, S.E1) && !IsAttackedByBlack(_cells, S.D1) && !IsAttackedByBlack(_cells, S.C1))
                        collector.Add(new Move(S.E1, S.C1, WQ));

            if ((castlings & Castlings.WK) != 0)
                if (_cells[S.F1] == 0 && _cells[S.G1] == 0)
                    if (!IsAttackedByBlack(_cells, S.E1) && !IsAttackedByBlack(_cells, S.F1) && !IsAttackedByBlack(_cells, S.G1))
                        collector.Add(new Move(S.E1, S.G1, WK));
        }
        void GenerateBlackCastlingMoves(int fromSquare, Castlings castlings, List<Move> collector)
        {
            if (fromSquare != S.E8) return;

            if ((castlings & Castlings.BQ) != 0)
                if (_cells[S.D8] == 0 && _cells[S.C8] == 0 && _cells[S.B8] == 0)
                    if (!IsAttackedByWhite(_cells, S.E8) && !IsAttackedByWhite(_cells, S.D8) && !IsAttackedByWhite(_cells, S.C8))
                        collector.Add(new Move(S.E8, S.C8, BQ));

            if ((castlings & Castlings.BK) != 0)
                if (_cells[S.F8] == 0 && _cells[S.G8] == 0)
                    if (!IsAttackedByWhite(_cells, S.E8) && !IsAttackedByWhite(_cells, S.F8) && !IsAttackedByWhite(_cells, S.G8))
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
                        if (!IsAttackedByBlack(_cells, _whiteKingPosition))
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
                        if (!IsAttackedByBlack(_cells, _whiteKingPosition))
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
                            if (!IsAttackedByBlack(_cells, _whiteKingPosition))
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
                            if (!IsAttackedByBlack(_cells, _whiteKingPosition))
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
                            if (!IsAttackedByBlack(_cells, _whiteKingPosition))
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
                            if (!IsAttackedByBlack(_cells, _whiteKingPosition))
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
                        if (!IsAttackedByWhite(_cells, _blackKingPosition))
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
                        if (!IsAttackedByWhite(_cells, _blackKingPosition))
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
                            if (!IsAttackedByWhite(_cells, _blackKingPosition))
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
                            if (!IsAttackedByWhite(_cells, _blackKingPosition))
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
                            if (!IsAttackedByWhite(_cells, _blackKingPosition))
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
                            if (!IsAttackedByWhite(_cells, _blackKingPosition))
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
              ? IsAttackedByBlack(_cells, _whiteKingPosition)
              : IsAttackedByWhite(_cells, _blackKingPosition);
        }
    }
}
