using System.Collections.Generic;

namespace ChessKit.ChessLogic
{
    partial class Board
    {
        private void GenerateMoves(CompactPiece piece, int fromSquare,
             int? enPassantFile, Caslings castlingAvailability, List<Move> collector)
        {
            switch (piece)
            {
                #region ' White Pawn '
                case CompactPiece.WhitePawn:
                    GenerateWhitePawnMoves(fromSquare, enPassantFile, collector);
                    break;
                #endregion

                #region ' White Bishop '
                case CompactPiece.WhiteBishop:
                    for (var to = fromSquare + 17; (to & 0x88) == 0; to += 17)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteBishop;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    break;
                #endregion

                #region ' White Knight '
                case CompactPiece.WhiteKnight:
                    {
                        var to = fromSquare + 33;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.WhiteKnight;
                                if (!IsAttackedByBlack(_whiteKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
                            }
                        }
                    }
                    break;
                #endregion

                #region ' White Rook '
                case CompactPiece.WhiteRook:
                    for (var to = fromSquare + 16; (to & 0x88) == 0; to += 16)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteRook;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    break;
                #endregion

                #region ' White Queen '
                case CompactPiece.WhiteQueen:
                    for (var to = fromSquare + 16; (to & 0x88) == 0; to += 16)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.WhiteQueen;
                            if (!IsAttackedByBlack(_whiteKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    break;
                #endregion

                #region ' White King '
                case CompactPiece.WhiteKing:
                    {
                        var to = fromSquare + 16;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.White)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByBlack(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
                            }
                        }
                    }
                    GenerateWhiteCastlingMoves(fromSquare, castlingAvailability, collector);
                    break;
                #endregion

                #region ' Black Pawn '
                case CompactPiece.BlackPawn:
                    GenerateBlackPawnMoves(fromSquare, enPassantFile, collector);
                    break;
                #endregion

                #region ' Black Bishop '
                case CompactPiece.BlackBishop:
                    for (var to = fromSquare + 17; (to & 0x88) == 0; to += 17)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop));
                            _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackBishop;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Bishop | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    break;
                #endregion

                #region ' Black Knight '
                case CompactPiece.BlackKnight:
                    {
                        var to = fromSquare + 33;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                _cells[to] = (byte)CompactPiece.BlackKnight;
                                if (!IsAttackedByWhite(_blackKingPosition))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.Knight | MoveAnnotations.Capture));
                                _cells[to] = toPiece;
                                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
                            }
                        }
                    }
                    break;
                #endregion

                #region ' Black Rook '
                case CompactPiece.BlackRook:
                    for (var to = fromSquare + 16; (to & 0x88) == 0; to += 16)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackRook;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackRook;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackRook;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook));
                            _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackRook;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Rook | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    break;
                #endregion

                #region ' Black Queen '
                case CompactPiece.BlackQueen:
                    for (var to = fromSquare + 16; (to & 0x88) == 0; to += 16)
                    {
                        var toPiece = _cells[to];
                        if (toPiece == 0)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
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
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                            _cells[to] = toPiece;
                        }
                        else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                        {
                            _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                            _cells[to] = (byte)CompactPiece.BlackQueen;
                            if (!IsAttackedByWhite(_blackKingPosition))
                                collector.Add(new Move(fromSquare, to,
                                    MoveAnnotations.Queen | MoveAnnotations.Capture));
                            _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                            _cells[to] = toPiece;
                            break;
                        }
                        else break;
                    }
                    break;
                #endregion

                #region ' Black King '
                case CompactPiece.BlackKing:
                    {
                        var to = fromSquare + 16;
                        if ((to & 0x88) == 0)
                        {
                            var toPiece = _cells[to];
                            if (toPiece == 0)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
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
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
                            }
                            else if ((ChessLogic.Color)(toPiece & (byte)ChessLogic.Color.Black) != ChessLogic.Color.Black)
                            {
                                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                                if (!IsAttackedByWhite(to))
                                    collector.Add(new Move(fromSquare, to,
                                        MoveAnnotations.King | MoveAnnotations.Capture));
                                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
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