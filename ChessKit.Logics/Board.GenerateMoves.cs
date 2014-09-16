using System.Collections.Generic;

namespace ChessKit.ChessLogic
{
  partial class Board
  {
    public void GenerateMoves(CompactPiece piece, int fromSquare, 
       int? enPassantFile, CastlingAvailability castlingAvailability, List<Move> collector)
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteBishop;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop));
                _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteBishop;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -15; (to & 0x88) == 0; to += -15)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteBishop;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop));
                _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteBishop;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -17; (to & 0x88) == 0; to += -17)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteBishop;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop));
                _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteBishop;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + 15; (to & 0x88) == 0; to += 15)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteBishop;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop));
                _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteBishop;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop | MoveHints.Capture));
              }
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteKnight;
                if (!IsAttackedByBlack(_whiteKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.WhiteKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteRook;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook));
                _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteRook;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + 1; (to & 0x88) == 0; to += 1)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteRook;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook));
                _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteRook;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -16; (to & 0x88) == 0; to += -16)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteRook;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook));
                _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteRook;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -1; (to & 0x88) == 0; to += -1)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteRook;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook));
                _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteRook;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook | MoveHints.Capture));
              }
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + 1; (to & 0x88) == 0; to += 1)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -16; (to & 0x88) == 0; to += -16)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -1; (to & 0x88) == 0; to += -1)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + 17; (to & 0x88) == 0; to += 17)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -15; (to & 0x88) == 0; to += -15)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -17; (to & 0x88) == 0; to += -17)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + 15; (to & 0x88) == 0; to += 15)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.WhiteQueen;
                if (!IsAttackedByBlack(_whiteKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByBlack(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.WhiteKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackBishop;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop));
                _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackBishop;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -15; (to & 0x88) == 0; to += -15)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackBishop;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop));
                _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackBishop;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -17; (to & 0x88) == 0; to += -17)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackBishop;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop));
                _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackBishop;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + 15; (to & 0x88) == 0; to += 15)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackBishop;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop));
                _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackBishop;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Bishop | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackBishop;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Bishop | MoveHints.Capture));
              }
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Knight));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackKnight;
                if (!IsAttackedByWhite(_blackKingPosition))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
                _cells[to] = toPiece;
                _cells[fromSquare] = (byte)CompactPiece.BlackKnight;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.Knight | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackRook;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook));
                _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackRook;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + 1; (to & 0x88) == 0; to += 1)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackRook;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook));
                _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackRook;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -16; (to & 0x88) == 0; to += -16)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackRook;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook));
                _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackRook;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -1; (to & 0x88) == 0; to += -1)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackRook;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook));
                _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackRook;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Rook | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackRook;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Rook | MoveHints.Capture));
              }
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + 1; (to & 0x88) == 0; to += 1)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -16; (to & 0x88) == 0; to += -16)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -1; (to & 0x88) == 0; to += -1)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + 17; (to & 0x88) == 0; to += 17)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -15; (to & 0x88) == 0; to += -15)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + -17; (to & 0x88) == 0; to += -17)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
              break;
            }
            else break;
          }
          for (var to = fromSquare + 15; (to & 0x88) == 0; to += 15)
          {
            var toPiece = _cells[to];
            if (toPiece == 0) 
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen));
              }
            }
            else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
            {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                _cells[to] = (byte)CompactPiece.BlackQueen;
                if (!IsAttackedByWhite(_blackKingPosition))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.Queen | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackQueen;
                _cells[to] = toPiece;
              }
              else
              {
                collector.Add(new Move((Position)fromSquare, (Position)to, 
                  MoveHints.Queen | MoveHints.Capture));
              }
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                  collector.Add(new Move((Position)fromSquare, (Position)to, 
                    MoveHints.King));
				}
              }
              else if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
              {
              if ((_pinMap & (1ul << fromSquare)) != 0)
              {
                _cells[fromSquare] = (byte)CompactPiece.EmptyCell;
                if (!IsAttackedByWhite(to))
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
                _cells[fromSquare] = (byte)CompactPiece.BlackKing;
				}
				else
				{
                 collector.Add(new Move((Position)fromSquare, (Position)to, 
                   MoveHints.King | MoveHints.Capture));
				}
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