using System.Collections.Generic;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.Algorithms
{
    static partial class MoveGeneration
    {
        static void GenerateWhiteCastlingMoves(byte[] cells, int fromSquare, Castlings castlings, List<GeneratedMove> collector)
        {
            if (fromSquare != Cells.E1) return;

            if ((castlings & Castlings.WQ) != 0)
                if (cells[Cells.D1] == 0 && cells[Cells.C1] == 0 && cells[Cells.B1] == 0)
                    if (!Attacks.IsAttackedByBlack(cells, Cells.E1) && !Attacks.IsAttackedByBlack(cells, Cells.D1) && !Attacks.IsAttackedByBlack(cells, Cells.C1))
                        collector.Add(new GeneratedMove(Cells.E1, Cells.C1, MoveAnnotations.WQ));

            if ((castlings & Castlings.WK) != 0)
                if (cells[Cells.F1] == 0 && cells[Cells.G1] == 0)
                    if (!Attacks.IsAttackedByBlack(cells, Cells.E1) && !Attacks.IsAttackedByBlack(cells, Cells.F1) && !Attacks.IsAttackedByBlack(cells, Cells.G1))
                        collector.Add(new GeneratedMove(Cells.E1, Cells.G1, MoveAnnotations.WK));
        }
        static void GenerateBlackCastlingMoves(byte[] cells, int fromSquare, Castlings castlings, List<GeneratedMove> collector)
        {
            if (fromSquare != Cells.E8) return;

            if ((castlings & Castlings.BQ) != 0)
                if (cells[Cells.D8] == 0 && cells[Cells.C8] == 0 && cells[Cells.B8] == 0)
                    if (!Attacks.IsAttackedByWhite(cells, Cells.E8) && !Attacks.IsAttackedByWhite(cells, Cells.D8) && !Attacks.IsAttackedByWhite(cells, Cells.C8))
                        collector.Add(new GeneratedMove(Cells.E8, Cells.C8, MoveAnnotations.BQ));

            if ((castlings & Castlings.BK) != 0)
                if (cells[Cells.F8] == 0 && cells[Cells.G8] == 0)
                    if (!Attacks.IsAttackedByWhite(cells, Cells.E8) && !Attacks.IsAttackedByWhite(cells, Cells.F8) && !Attacks.IsAttackedByWhite(cells, Cells.G8))
                        collector.Add(new GeneratedMove(Cells.E8, Cells.G8, MoveAnnotations.BK));
        }
        static void GenerateWhitePawnMoves(byte[] cells, int whiteKing, int fromSquare, int? enPassantFile, List<GeneratedMove> collector)
        {
            {
                var to = fromSquare + 16;
                if ((to & 0x88) == 0)
                    if (cells[to] == 0)
                    {
                        cells[fromSquare] = 0;
                        var toPiece = cells[to];
                        cells[to] = (byte)Piece.WhitePawn;
                        if (!Attacks.IsAttackedByBlack(cells, whiteKing))
                            collector.Add(new GeneratedMove(fromSquare, to,
                                to / 16 != 7 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion));
                        cells[fromSquare] = (byte)Piece.WhitePawn;
                        cells[to] = toPiece;
                    }
            }
            if ((fromSquare / 16) == 1)
            {
                if (cells[fromSquare + 16] == 0)
                {
                    var to = fromSquare + 32;
                    if (cells[to] == 0)
                    {
                        cells[fromSquare] = 0;
                        var toPiece = cells[to];
                        cells[to] = (byte)Piece.WhitePawn;
                        if (!Attacks.IsAttackedByBlack(cells, whiteKing))
                            collector.Add(new GeneratedMove(fromSquare, to, MoveAnnotations.Pawn | MoveAnnotations.DoublePush));
                        cells[fromSquare] = (byte)Piece.WhitePawn;
                        cells[to] = toPiece;
                    }
                }
            }
            {
                var to = fromSquare + 17;
                if ((to & 0x88) == 0)
                {
                    var toPiece = cells[to];
                    if (toPiece != 0)
                    {
                        if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            cells[fromSquare] = 0;
                            cells[to] = (byte)Piece.WhitePawn;
                            if (!Attacks.IsAttackedByBlack(cells, whiteKing))
                                collector.Add(new GeneratedMove(fromSquare, to,
                                    to / 16 != 7
                                        ? MoveAnnotations.Pawn | MoveAnnotations.Capture
                                        : MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
                            cells[fromSquare] = (byte)Piece.WhitePawn;
                            cells[to] = toPiece;
                        }
                    }
                    else
                    {
                        if (enPassantFile == (to % 16) && to / 16 == 5)
                        {
                            cells[fromSquare] = 0;
                            cells[to] = (byte)Piece.WhitePawn;
                            cells[to - 16] = 0;
                            if (!Attacks.IsAttackedByBlack(cells, whiteKing))
                                collector.Add(new GeneratedMove(fromSquare, to,
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
                            cells[fromSquare] = (byte)Piece.WhitePawn;
                            cells[to - 16] = (byte)Piece.BlackPawn;
                            cells[to] = toPiece;
                        }
                    }
                }
            }
            {
                var to = fromSquare + 15;
                if ((to & 0x88) == 0)
                {
                    var toPiece = cells[to];
                    if (toPiece != 0)
                    {
                        if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            cells[fromSquare] = 0;
                            cells[to] = (byte)Piece.WhitePawn;
                            if (!Attacks.IsAttackedByBlack(cells, whiteKing))
                                collector.Add(new GeneratedMove(fromSquare, to,
                                    to / 16 != 7 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
                                        MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
                            cells[fromSquare] = (byte)Piece.WhitePawn;
                            cells[to] = toPiece;
                        }
                    }
                    else
                    {
                        if (enPassantFile == (to % 16) && to / 16 == 5)
                        {
                            cells[fromSquare] = 0;
                            cells[to] = (byte)Piece.WhitePawn;
                            cells[to - 16] = 0;
                            if (!Attacks.IsAttackedByBlack(cells, whiteKing))
                                collector.Add(new GeneratedMove(fromSquare, to,
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
                            cells[fromSquare] = (byte)Piece.WhitePawn;
                            cells[to] = (byte)Piece.EmptyCell;
                            cells[to - 16] = (byte)Piece.BlackPawn;
                        }
                    }
                }
            }
        }
        static void GenerateBlackPawnMoves(byte[] cells, int blackKing, int fromSquare, int? enPassantFile, List<GeneratedMove> collector)
        {
            {
                var to = fromSquare - 16;
                if ((to & 0x88) == 0)
                    if (cells[to] == 0)
                    {
                        cells[fromSquare] = 0;
                        var toPiece = cells[to];
                        cells[to] = (byte)Piece.BlackPawn;
                        if (!Attacks.IsAttackedByWhite(cells, blackKing))
                            collector.Add(new GeneratedMove(fromSquare, to,
                                to / 16 != 0 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion));
                        cells[fromSquare] = (byte)Piece.BlackPawn;
                        cells[to] = toPiece;
                    }
            }
            if ((fromSquare / 16) == 6)
            {
                if (cells[fromSquare - 16] == 0)
                {
                    var to = fromSquare - 32;
                    if (cells[to] == 0)
                    {
                        cells[fromSquare] = 0;
                        var toPiece = cells[to];
                        cells[to] = (byte)Piece.BlackPawn;
                        if (!Attacks.IsAttackedByWhite(cells, blackKing))
                            collector.Add(new GeneratedMove(fromSquare, to,
                                MoveAnnotations.Pawn | MoveAnnotations.DoublePush));
                        cells[fromSquare] = (byte)Piece.BlackPawn;
                        cells[to] = toPiece;
                    }
                }
            }
            {
                var to = fromSquare - 17;
                if ((to & 0x88) == 0)
                {
                    var toPiece = cells[to];
                    if (toPiece != 0)
                    {
                        if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            cells[fromSquare] = 0;
                            cells[to] = (byte)Piece.BlackPawn;
                            if (!Attacks.IsAttackedByWhite(cells, blackKing))
                                collector.Add(new GeneratedMove(fromSquare, to,
                                    to / 16 != 0 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
                                        MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
                            cells[fromSquare] = (byte)Piece.BlackPawn;
                            cells[to] = toPiece;
                        }
                    }
                    else
                    {
                        if (enPassantFile == (to % 16) && to / 16 == 2)
                        {
                            cells[fromSquare] = 0;
                            cells[to + 16] = 0;
                            cells[to] = (byte)Piece.BlackPawn;
                            if (!Attacks.IsAttackedByWhite(cells, blackKing))
                                collector.Add(new GeneratedMove(fromSquare, to,
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
                            cells[fromSquare] = (byte)Piece.BlackPawn;
                            cells[to] = toPiece;
                            cells[to + 16] = (byte)Piece.WhitePawn;
                        }
                    }
                }
            }
            {
                var to = fromSquare - 15;
                if ((to & 0x88) == 0)
                {
                    var toPiece = cells[to];
                    if (toPiece != 0)
                    {
                        if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            cells[fromSquare] = 0;
                            cells[to] = (byte)Piece.BlackPawn;
                            if (!Attacks.IsAttackedByWhite(cells, blackKing))
                                collector.Add(new GeneratedMove(fromSquare, to,
                                    to / 16 != 0 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
                                        MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
                            cells[fromSquare] = (byte)Piece.BlackPawn;
                            cells[to] = toPiece;
                        }
                    }
                    else
                    {
                        if (enPassantFile == (to % 16) && to / 16 == 2)
                        {
                            cells[fromSquare] = 0;
                            cells[to] = (byte)Piece.BlackPawn;
                            cells[to + 16] = 0;
                            if (!Attacks.IsAttackedByWhite(cells, blackKing))
                                collector.Add(new GeneratedMove(fromSquare, to,
                                    MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
                            cells[fromSquare] = (byte)Piece.BlackPawn;
                            cells[to + 16] = (byte)Piece.WhitePawn;
                            cells[to] = toPiece;
                        }
                    }
                }
            }
        }

    }
}