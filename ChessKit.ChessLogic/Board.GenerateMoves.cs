using System.Collections.Generic;
using ChessKit.ChessLogic.Enums;

namespace ChessKit.ChessLogic
{
    partial class Board
    {
        private void GenerateMoves(Piece piece, int fromSquare,
             int? enPassantFile, Castlings castlingAvailability, List<Move> collector)
        {
            switch (piece)
            {
                #region ' White Pawn '
                case Piece.WhitePawn:
                    GenerateWhitePawnMoves(fromSquare, enPassantFile, collector);
                    break;
                #endregion

                #region ' White Bishop '
                case Piece.WhiteBishop:
                    for (var to = fromSquare + 17; (to & 0x88) == 0; to += 17)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)Piece.WhiteBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteBishop;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -15; (to & 0x88) == 0; to += -15)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)Piece.WhiteBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteBishop;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -17; (to & 0x88) == 0; to += -17)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)Piece.WhiteBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteBishop;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + 15; (to & 0x88) == 0; to += 15)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)Piece.WhiteBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteBishop;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    break;
                #endregion

                #region ' White Knight '
                case Piece.WhiteKnight:
                    {
                        var to = fromSquare + 33;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + 31;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -31;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -33;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + 18;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + 14;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -14;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -18;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.WhiteKnight;
                            }
                        }
                    }
                    break;
                #endregion

                #region ' White Rook '
                case Piece.WhiteRook:
                    for (var to = fromSquare + 16; (to & 0x88) == 0; to += 16)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)Piece.WhiteRook;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteRook;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + 1; (to & 0x88) == 0; to += 1)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)Piece.WhiteRook;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteRook;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -16; (to & 0x88) == 0; to += -16)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)Piece.WhiteRook;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteRook;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -1; (to & 0x88) == 0; to += -1)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)Piece.WhiteRook;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteRook;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    break;
                #endregion

                #region ' White Queen '
                case Piece.WhiteQueen:
                    for (var to = fromSquare + 16; (to & 0x88) == 0; to += 16)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + 1; (to & 0x88) == 0; to += 1)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -16; (to & 0x88) == 0; to += -16)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -1; (to & 0x88) == 0; to += -1)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + 17; (to & 0x88) == 0; to += 17)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -15; (to & 0x88) == 0; to += -15)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -17; (to & 0x88) == 0; to += -17)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + 15; (to & 0x88) == 0; to += 15)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.WhiteQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    break;
                #endregion

                #region ' White King '
                case Piece.WhiteKing:
                    {
                        var to = fromSquare + 16;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + 17;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + 1;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -15;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -16;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -17;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -1;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + 15;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.White)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.WhiteKing;
                            }
                        }
                    }
                    GenerateWhiteCastlingMoves(fromSquare, castlingAvailability, collector);
                    break;
                #endregion

                #region ' Black Pawn '
                case Piece.BlackPawn:
                    GenerateBlackPawnMoves(fromSquare, enPassantFile, collector);
                    break;
                #endregion

                #region ' Black Bishop '
                case Piece.BlackBishop:
                    for (var to = fromSquare + 17; (to & 0x88) == 0; to += 17)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)Piece.BlackBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackBishop;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -15; (to & 0x88) == 0; to += -15)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)Piece.BlackBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackBishop;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -17; (to & 0x88) == 0; to += -17)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)Piece.BlackBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackBishop;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + 15; (to & 0x88) == 0; to += 15)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)Piece.BlackBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackBishop;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    break;
                #endregion

                #region ' Black Knight '
                case Piece.BlackKnight:
                    {
                        var to = fromSquare + 33;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + 31;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -31;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -33;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + 18;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + 14;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -14;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -18;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                _cells[to] = (byte)Piece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)Piece.BlackKnight;
                            }
                        }
                    }
                    break;
                #endregion

                #region ' Black Rook '
                case Piece.BlackRook:
                    for (var to = fromSquare + 16; (to & 0x88) == 0; to += 16)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)Piece.BlackRook;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackRook;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + 1; (to & 0x88) == 0; to += 1)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)Piece.BlackRook;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackRook;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -16; (to & 0x88) == 0; to += -16)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)Piece.BlackRook;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackRook;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -1; (to & 0x88) == 0; to += -1)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)Piece.BlackRook;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackRook;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    break;
                #endregion

                #region ' Black Queen '
                case Piece.BlackQueen:
                    for (var to = fromSquare + 16; (to & 0x88) == 0; to += 16)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + 1; (to & 0x88) == 0; to += 1)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -16; (to & 0x88) == 0; to += -16)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -1; (to & 0x88) == 0; to += -1)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + 17; (to & 0x88) == 0; to += 17)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -15; (to & 0x88) == 0; to += -15)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + -17; (to & 0x88) == 0; to += -17)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    for (var to = fromSquare + 15; (to & 0x88) == 0; to += 15)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                        {
                            _cells[fromSquare] = (byte)Piece.EmptyCell;
                            _cells[to] = (byte)Piece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)Piece.BlackQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    break;
                #endregion

                #region ' Black King '
                case Piece.BlackKing:
                    {
                        var to = fromSquare + 16;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + 17;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + 1;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -15;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -16;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -17;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + -1;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                        }
                    }
                    {
                        var to = fromSquare + 15;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                            else if ((Color)(toPiece & (byte)Color.Black) != Color.Black)
                            {
                                _cells[fromSquare] = (byte)Piece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)Piece.BlackKing;
                            }
                        }
                    }
                    GenerateBlackCastlingMoves(fromSquare, castlingAvailability, collector);
                    break;
                #endregion

            }
        }
    }
}