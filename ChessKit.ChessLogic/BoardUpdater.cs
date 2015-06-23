using System;
using ChessKit.ChessLogic.N;
using ChessKit.ChessLogic.Primitives;
using static ChessKit.ChessLogic.Primitives.MoveAnnotations;
using S = ChessKit.ChessLogic.Board.S;

namespace ChessKit.ChessLogic
{
    public static class BoardUpdater
    {
        private const int BytesCount = 128;

        public static AnalyzedMove MakeMove(Position src, MoveR move)
        {
            var whiteKingPosition = src.WhiteKing;
            var blackKingPosition = src.BlackKing;

            var cells = new byte[BytesCount];
            var sourceCells = src.Core.Squares;
            Buffer.BlockCopy(sourceCells, 0, cells, 0, BytesCount);

            MoveAnnotations notes;
            // Piece in the from cell?
            var moveFrom = move.From;
            var piece = (Piece)sourceCells[moveFrom];
            if (piece == Piece.EmptyCell)
            {
                notes = EmptyCell;
                return new IllegalMove(move, src, 
                    PieceType.None, notes);
            }

            // Side to move?
            var color = piece.Color();
            if (color != src.Core.ActiveColor)
            {
                notes = (MoveAnnotations)piece.PieceType() | WrongSideToMove;
                return new IllegalMove(move, src,
                    piece.PieceType(), notes); 
            }

            // Move to occupied cell?
            var moveTo = move.To;
            var toPiece = (Piece) sourceCells[moveTo];
            if (toPiece != Piece.EmptyCell && toPiece.Color() == color)
            {
                notes = (MoveAnnotations)piece.PieceType() | ToOccupiedCell;
                return new IllegalMove(move, src,
                    piece.PieceType(), notes);
            }
            notes = MoveLegality.ValidateMove(cells, piece,
                moveFrom, moveTo, toPiece, src.Core.CastlingAvailability);
            if (toPiece != Piece.EmptyCell) notes |= Capture;
            // ---------------- SetupBoard ---------------------
            if ((notes & AllErrors) != 0)
                return new IllegalMove(move, src,
                    piece.PieceType(), notes);
            if ((notes & EnPassant) != 0)
            {
                if (src.Core.EnPassant != moveTo % 16)
                {
                    notes |= HasNoEnPassant;
                    return new IllegalMove(move, src,
                        piece.PieceType(), notes);
                }
            }
            else if ((notes & Promotion) != 0)
            {
                var proposedPromotion = move.ProposedPromotion;
                if (move.ProposedPromotion == PieceType.None)
                {
                    notes |= MissingPromotionHint;
                    proposedPromotion = PieceType.Queen;
                }
                piece = proposedPromotion.With(color);
            }
            else if (move.ProposedPromotion != PieceType.None)
            {
                notes |= PromotionHintIsNotNeeded;
            }

            cells[moveTo] = (byte)piece;

            switch (piece)
            {
                case Piece.WhiteKing:
                    whiteKingPosition = moveTo;
                    break;
                case Piece.BlackKing:
                    blackKingPosition = moveTo;
                    break;
            }

            cells[moveFrom] = 0;
            var enPassantFile = new int?();
            if ((notes & (DoublePush | EnPassant | AllCastlings)) != 0)
            {
                if ((notes & DoublePush) != 0)
                {
                    enPassantFile = moveFrom % 16;
                }
                else if ((notes & EnPassant) != 0)
                {
                    cells[moveTo + (color == Color.White ? -16 : +16)] = 0;
                }
                else switch (notes)
                {
                    case (King | WK):
                        cells[S.H1] = (byte)Piece.EmptyCell;
                        cells[S.F1] = (byte)Piece.WhiteRook;
                        break;
                    case (King | WQ):
                        cells[S.A1] = (byte)Piece.EmptyCell;
                        cells[S.D1] = (byte)Piece.WhiteRook;
                        break;
                    case (King | BK):
                        cells[S.H8] = (byte)Piece.EmptyCell;
                        cells[S.F8] = (byte)Piece.BlackRook;
                        break;
                    case (King | BQ):
                        cells[S.A8] = (byte)Piece.EmptyCell;
                        cells[S.D8] = (byte)Piece.BlackRook;
                        break;
                }
            }
            var isUnderCheck = src.Core.ActiveColor == Color.White
                ? Scanning.IsAttackedByBlack(cells, whiteKingPosition)
                : Scanning.IsAttackedByWhite(cells, blackKingPosition);

            if (isUnderCheck)
            {
                notes |= MoveToCheck;
            }
            var castlings = src.Core.CastlingAvailability
              & ~KilledAvailability(moveTo)
              & ~KilledAvailability(moveFrom);

            var sideOnMove = color.Invert();

            // ---------------- ---------- ---------------------
            if ((notes & AllErrors) != 0)
                return new IllegalMove(move, src,
                    piece.PieceType(), notes);

            var positionCore = new PositionCore(cells, sideOnMove, castlings, enPassantFile);
            var legalMove = new LegalMove(
                move, src, positionCore, 
                piece.PieceType(), notes);
            return legalMove;
        }

        private static Castlings KilledAvailability(int pos)
        {
            switch (pos)
            {
                case S.A1: return Castlings.WQ;
                case S.E1: return Castlings.White;
                case S.H1: return Castlings.WK;
                case S.A8: return Castlings.BQ;
                case S.E8: return Castlings.Black;
                case S.H8: return Castlings.BK;
                default: return Castlings.None;
            }
        }
    }
}
