using System;
using ChessKit.ChessLogic.Primitives;
using S = ChessKit.ChessLogic.Cells;

namespace ChessKit.ChessLogic.Algorithms
{
    public static class BoardUpdater
    {
        private const int BytesCount = 128;

        public static AnalyzedMove MakeMove(Position source, Move move)
        {
            var whiteKingSquare = source.Core.WhiteKing;
            var blackKingSquare = source.Core.BlackKing;

            var cells = new byte[BytesCount];
            var sourceCells = source.Core.Squares;
            Buffer.BlockCopy(sourceCells, 0, cells, 0, BytesCount);

            MoveAnnotations notes;
            // Piece in the from cell?
            var moveFrom = move.FromCell;
            var piece = (Piece)sourceCells[moveFrom];
            if (piece == Piece.EmptyCell)
            {
                notes = MoveAnnotations.EmptyCell;
                return new IllegalMove(move, source, notes);
            }

            // Side to move?
            var color = piece.Color();
            if (color != source.Core.ActiveColor)
            {
                notes = (MoveAnnotations)piece.PieceType() | MoveAnnotations.WrongSideToMove;
                return new IllegalMove(move, source, notes); 
            }

            // Move to occupied cell?
            var moveTo = move.ToCell;
            var toPiece = (Piece) sourceCells[moveTo];
            if (toPiece != Piece.EmptyCell && toPiece.Color() == color)
            {
                notes = (MoveAnnotations)piece.PieceType() | MoveAnnotations.ToOccupiedCell;
                return new IllegalMove(move, source, notes);
            }
            notes = MoveLegality.ValidateMove(cells, piece,
                moveFrom, moveTo, toPiece, source.Core.CastlingAvailability);
            if (toPiece != Piece.EmptyCell) notes |= MoveAnnotations.Capture;
            // ---------------- SetupBoard ---------------------
            if ((notes & MoveAnnotations.AllErrors) != 0)
                return new IllegalMove(move, source, notes);
            if ((notes & MoveAnnotations.EnPassant) != 0)
            {
                if (source.Core.EnPassant != moveTo % 16)
                {
                    notes |= MoveAnnotations.HasNoEnPassant;
                    return new IllegalMove(move, source, notes);
                }
            }
            else if ((notes & MoveAnnotations.Promotion) != 0)
            {
                var proposedPromotion = move.PromoteTo;
                if (move.PromoteTo == PieceType.None)
                {
                    notes |= MoveAnnotations.MissingPromotionHint;
                    proposedPromotion = PieceType.Queen;
                }
                piece = proposedPromotion.With(color);
            }
            else if (move.PromoteTo != PieceType.None)
            {
                notes |= MoveAnnotations.PromotionHintIsNotNeeded;
            }

            cells[moveTo] = (byte)piece;

            switch (piece)
            {
                case Piece.WhiteKing:
                    whiteKingSquare = moveTo;
                    break;
                case Piece.BlackKing:
                    blackKingSquare = moveTo;
                    break;
            }

            cells[moveFrom] = 0;
            var enPassantFile = new int?();
            if ((notes & (MoveAnnotations.DoublePush | MoveAnnotations.EnPassant | MoveAnnotations.AllCastlings)) != 0)
            {
                if ((notes & MoveAnnotations.DoublePush) != 0)
                {
                    enPassantFile = moveFrom % 16;
                }
                else if ((notes & MoveAnnotations.EnPassant) != 0)
                {
                    cells[moveTo + (color == Color.White ? -16 : +16)] = 0;
                }
                else switch (notes)
                {
                    case (MoveAnnotations.King | MoveAnnotations.WK):
                        cells[S.H1] = (byte)Piece.EmptyCell;
                        cells[S.F1] = (byte)Piece.WhiteRook;
                        break;
                    case (MoveAnnotations.King | MoveAnnotations.WQ):
                        cells[S.A1] = (byte)Piece.EmptyCell;
                        cells[S.D1] = (byte)Piece.WhiteRook;
                        break;
                    case (MoveAnnotations.King | MoveAnnotations.BK):
                        cells[S.H8] = (byte)Piece.EmptyCell;
                        cells[S.F8] = (byte)Piece.BlackRook;
                        break;
                    case (MoveAnnotations.King | MoveAnnotations.BQ):
                        cells[S.A8] = (byte)Piece.EmptyCell;
                        cells[S.D8] = (byte)Piece.BlackRook;
                        break;
                }
            }
            var isUnderCheck = source.Core.ActiveColor == Color.White
                ? cells.IsSquareAttackedByBlack(whiteKingSquare)
                : cells.IsSquareAttackedByWhite(blackKingSquare);

            if (isUnderCheck)
            {
                notes |= MoveAnnotations.MoveToCheck;
            }
            var castlings = source.Core.CastlingAvailability
              & ~KilledAvailability(moveTo)
              & ~KilledAvailability(moveFrom);

            var sideOnMove = color.Invert();

            // ---------------- ---------- ---------------------
            if ((notes & MoveAnnotations.AllErrors) != 0)
                return new IllegalMove(move, source, notes);

            var positionCore = new PositionCore(
                cells, sideOnMove, castlings, enPassantFile, 
                whiteKingSquare, blackKingSquare);
            var legalMove = new LegalMove(
                move, source, positionCore, notes);
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
