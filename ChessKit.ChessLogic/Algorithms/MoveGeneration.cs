using System.Collections.Generic;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.Algorithms
{
    static partial class MoveGeneration
    {
        static void GenerateWhiteCastlingMoves(byte[] _cells, int fromSquare, Castlings castlings, List<Move> collector)
        {
            if (fromSquare != Cells.E1) return;

            if ((castlings & Castlings.WQ) != 0)
                if (_cells[Cells.D1] == 0 && _cells[Cells.C1] == 0 && _cells[Cells.B1] == 0)
                    if (!Scanning.IsAttackedByBlack(_cells, Cells.E1) && !Scanning.IsAttackedByBlack(_cells, Cells.D1) && !Scanning.IsAttackedByBlack(_cells, Cells.C1))
                        collector.Add(new Move(Cells.E1, Cells.C1, MoveAnnotations.WQ));

            if ((castlings & Castlings.WK) != 0)
                if (_cells[Cells.F1] == 0 && _cells[Cells.G1] == 0)
                    if (!Scanning.IsAttackedByBlack(_cells, Cells.E1) && !Scanning.IsAttackedByBlack(_cells, Cells.F1) && !Scanning.IsAttackedByBlack(_cells, Cells.G1))
                        collector.Add(new Move(Cells.E1, Cells.G1, MoveAnnotations.WK));
        }
        static void GenerateBlackCastlingMoves(byte[] _cells, int fromSquare, Castlings castlings, List<Move> collector)
        {
            if (fromSquare != Cells.E8) return;

            if ((castlings & Castlings.BQ) != 0)
                if (_cells[Cells.D8] == 0 && _cells[Cells.C8] == 0 && _cells[Cells.B8] == 0)
                    if (!Scanning.IsAttackedByWhite(_cells, Cells.E8) && !Scanning.IsAttackedByWhite(_cells, Cells.D8) && !Scanning.IsAttackedByWhite(_cells, Cells.C8))
                        collector.Add(new Move(Cells.E8, Cells.C8, MoveAnnotations.BQ));

            if ((castlings & Castlings.BK) != 0)
                if (_cells[Cells.F8] == 0 && _cells[Cells.G8] == 0)
                    if (!Scanning.IsAttackedByWhite(_cells, Cells.E8) && !Scanning.IsAttackedByWhite(_cells, Cells.F8) && !Scanning.IsAttackedByWhite(_cells, Cells.G8))
                        collector.Add(new Move(Cells.E8, Cells.G8, MoveAnnotations.BK));
        }
        static void GenerateWhitePawnMoves(byte[] _cells, int _whiteKingPosition, int fromSquare, int? enPassantFile, List<Move> collector)
        {
            {
                var to = fromSquare + 16;
                if ((to & 0x88) == 0)
                    if (_cells[to] == 0)
                    {
                        _cells[fromSquare] = 0;
                        var toPiece = _cells[to];
                        _cells[to] = (byte)Piece.WhitePawn;
                        if (!Scanning.IsAttackedByBlack(_cells, _whiteKingPosition))
                            collector.Add(new Move(fromSquare, to,
                                to / 16 != 7 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion));
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
                        if (!Scanning.IsAttackedByBlack(_cells, _whiteKingPosition))
                            collector.Add(new Move(fromSquare, to, MoveAnnotations.Pawn | MoveAnnotations.DoublePush));
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
                            if (!Scanning.IsAttackedByBlack(_cells, _whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    to / 16 != 7
                                        ? MoveAnnotations.Pawn | MoveAnnotations.Capture
                                        : MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
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
                            if (!Scanning.IsAttackedByBlack(_cells, _whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
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
                            if (!Scanning.IsAttackedByBlack(_cells, _whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    to / 16 != 7 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
                                        MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
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
                            if (!Scanning.IsAttackedByBlack(_cells, _whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
                            _cells[fromSquare] = (byte)Piece.WhitePawn;
                            _cells[to] = (byte)Piece.EmptyCell;
                            _cells[to - 16] = (byte)Piece.BlackPawn;
                        }
                    }
                }
            }
        }
        static void GenerateBlackPawnMoves(byte[] _cells, int _blackKingPosition, int fromSquare, int? enPassantFile, List<Move> collector)
        {
            {
                var to = fromSquare - 16;
                if ((to & 0x88) == 0)
                    if (_cells[to] == 0)
                    {
                        _cells[fromSquare] = 0;
                        var toPiece = _cells[to];
                        _cells[to] = (byte)Piece.BlackPawn;
                        if (!Scanning.IsAttackedByWhite(_cells, _blackKingPosition))
                            collector.Add(new Move(fromSquare, to,
                                to / 16 != 0 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion));
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
                        if (!Scanning.IsAttackedByWhite(_cells, _blackKingPosition))
                            collector.Add(new Move(fromSquare, to,
                                MoveAnnotations.Pawn | MoveAnnotations.DoublePush));
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
                            if (!Scanning.IsAttackedByWhite(_cells, _blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    to / 16 != 0 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
                                        MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
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
                            if (!Scanning.IsAttackedByWhite(_cells, _blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
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
                            if (!Scanning.IsAttackedByWhite(_cells, _blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    to / 16 != 0 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
                                        MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
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
                            if (!Scanning.IsAttackedByWhite(_cells, _blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
                            _cells[fromSquare] = (byte)Piece.BlackPawn;
                            _cells[to + 16] = (byte)Piece.WhitePawn;
                            _cells[to] = toPiece;
                        }
                    }
                }
            }
        }

    }
}