using System.Collections.Generic;
using ChessKit.ChessLogic.Enums;
using static ChessKit.ChessLogic.Enums.MoveAnnotations;

namespace ChessKit.ChessLogic
{
    public sealed partial class Board
    {
        internal GameStates GameState;

        /// <summary>The side which is moving next (white or black)</summary>
        public Color SideOnMove { get; private set; }
        /// <summary>Gets the file at which en passant move is available</summary>
        public int? EnPassantFile { get; set; }

        /// <summary>Gets sides players can castle to</summary>
        internal Castlings Castlings;
        /// <summary>This is the number of halfmoves since the last pawn advance or capture. </summary>
        /// <remarks>This is used to determine if a draw can be claimed under the fifty-move rule.</remarks>
        public int HalfMoveClock { get; set; }
        /// <summary>The number of the full move. It starts at 1, and is incremented after Black's move</summary>
        public int MoveNumber { get; private set; }

        public Move PreviousMove { get; }

        public bool IsCheck => GameState == GameStates.Check;
        public bool IsMate => GameState == GameStates.Mate;

        #region ' MakeMove '

        public List<Move> GetLegalMoves()
        {
            // BUG: Actually creates boards, but only returns moves!
            var res = new List<Move>(50);
            var sideOnMove = SideOnMove;
            for (var moveFrom = 0; moveFrom < 64; moveFrom++)
            {
                var moveFromSq = moveFrom + (moveFrom & ~7);
                var piece = this[moveFromSq];
                if (piece == 0) continue;
                if (piece.Color() != sideOnMove) continue;
                GenerateMoves(piece, moveFromSq, EnPassantFile, Castlings, res);
            }
            return res;
        }
        public List<Move> GetLegalMoves(int moveFrom)
        {
            var piece = this[moveFrom];
            if (piece == Piece.EmptyCell) return new List<Move>();
            if (piece.Color() != SideOnMove) return new List<Move>();
            var res = new List<Move>(28);
            GenerateMoves(piece, moveFrom, EnPassantFile, Castlings, res);
            return res;
        }


        private void SetStatus()
        {
            if (!IsUnderCheck(SideOnMove)) return;
            GameState = GetLegalMoves().Count > 0
                ? GameStates.Check : GameStates.Mate;
        }

        public Board MakeMove(Move move)
        {
            return new Board(this, move);
        }

        private Board(Board src, Move move)
            : this(src)
        {
            PreviousMove = move;

            // Piece in the from cell?
            var moveFrom = move.From;
            var piece = src[moveFrom];
            if (piece == Piece.EmptyCell)
            {
                PreviousMove.Annotations = EmptyCell;
                return;
            }

            // Side to move?
            var color = piece.Color();
            if (color != src.SideOnMove)
            {
                PreviousMove.Annotations = (MoveAnnotations)piece.PieceType() | WrongSideToMove;
                return;
            }

            // Move to occupied cell?
            var moveTo = move.To;
            var toPiece = src[moveTo];
            if (toPiece != Piece.EmptyCell && toPiece.Color() == color)
            {
                PreviousMove.Annotations = (MoveAnnotations)piece.PieceType() | ToOccupiedCell;
                return;
            }
            PreviousMove.Annotations = src.ValidateMove(piece,
              moveFrom, moveTo, toPiece, src.Castlings);
            if (toPiece != Piece.EmptyCell) PreviousMove.Annotations |= Capture;
            SetupBoard(src, piece, moveFrom, moveTo, move.ProposedPromotion, color);
            if ((PreviousMove.Annotations & AllErrors) != 0) return;
            if (IsUnderCheck(SideOnMove))
            {
                GameState = GameStates.Check;
            }
        }
        private void SetupBoard(Board src, Piece piece,
          int moveFrom, int moveTo, PieceType proposedPromotion,
          Color color)
        {
            if ((PreviousMove.Annotations & AllErrors) != 0) return;
            if ((PreviousMove.Annotations & EnPassant) != 0)
            {
                if (src.EnPassantFile != moveTo % 16)
                {
                    PreviousMove.Annotations |= HasNoEnPassant;
                    return;
                }
            }
            else if ((PreviousMove.Annotations & Promotion) != 0)
            {
                if (proposedPromotion == PieceType.None)
                {
                    PreviousMove.Annotations |= MissingPromotionHint;
                 // BUG:   proposedPromotion = PieceType.Queen;
                }
                piece = proposedPromotion.With(color);
            }
            else if (proposedPromotion != PieceType.None)
            {
                PreviousMove.Annotations |= PromotionHintIsNotNeeded;
            }

            this[moveTo] = piece;
            _cells[moveFrom] = 0;

            if ((PreviousMove.Annotations & (DoublePush | EnPassant | AllCastlings)) != 0)
            {
                if ((PreviousMove.Annotations & DoublePush) != 0)
                {
                    EnPassantFile = moveFrom % 16;
                }
                else if ((PreviousMove.Annotations & EnPassant) != 0)
                {
                    _cells[moveTo + (color == Color.White ? -16 : +16)] = 0;
                }
                else if (PreviousMove.Annotations == WK) // TODO: Move it up?
                {
                    _cells[S.H1] = (byte)Piece.EmptyCell;
                    _cells[S.F1] = (byte)Piece.WhiteRook;
                }
                else if (PreviousMove.Annotations == WQ)
                {
                    _cells[S.A1] = (byte)Piece.EmptyCell;
                    _cells[S.D1] = (byte)Piece.WhiteRook;
                }
                else if (PreviousMove.Annotations == BK)
                {
                    _cells[S.H8] = (byte)Piece.EmptyCell;
                    _cells[S.F8] = (byte)Piece.BlackRook;
                }
                else if (PreviousMove.Annotations == BQ)
                {
                    _cells[S.A8] = (byte)Piece.EmptyCell;
                    _cells[S.D8] = (byte)Piece.BlackRook;
                }
            }
            if (IsUnderCheck(src.SideOnMove))
            {
                PreviousMove.Annotations |= MoveToCheck;
            }
            Castlings = src.Castlings
              & ~KilledAvailability(moveTo)
              & ~KilledAvailability(moveFrom);

            SideOnMove = color.Invert();

            HalfMoveClock =
              (PreviousMove.Annotations & (Capture | Pawn)) != 0
              ? 0 : src.HalfMoveClock + 1;

            MoveNumber = src.MoveNumber + (color == Color.Black ? 1 : 0);
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
        #endregion

        public Piece this[string index] => this[CoordinateExtensions.Parse(index)];

        public bool IsAttackedBy(Color side, int square)
        {
            return side == Color.White
                ? IsAttackedByWhite(square)
                : IsAttackedByBlack(square);
        }

        public bool IsInCheck(Color side)
        {
            return side == Color.White
                ? IsAttackedByBlack(_whiteKingPosition)
                : IsAttackedByWhite(_blackKingPosition);
        }
    }
}

