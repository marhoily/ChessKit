using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ChessKit.ChessLogic.Enums;

namespace ChessKit.ChessLogic
{
    public sealed partial class Board
    {
        private GameState _gameState;

        /// <summary>The side which is moving next (white or black)</summary>
        public Color SideOnMove { get; private set; }
        /// <summary>Gets the file at which en passant move is available</summary>
        [SuppressMessage("Microsoft.Naming",
          "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "En",
          Justification = "'En-passant' is borrowed from french I guess")]
        public int? EnPassantFile { get; set; }

        /// <summary>Gets sides players can castle to</summary>
        private Caslings _caslings;
        /// <summary>This is the number of halfmoves since the last pawn advance or capture. </summary>
        /// <remarks>This is used to determine if a draw can be claimed under the fifty-move rule.</remarks>
        public int HalfMoveClock { get; set; }
        /// <summary>The number of the full move. It starts at 1, and is incremented after Black's move</summary>
        public int MoveNumber { get; private set; }

        public Board Previous { get; private set; }
        public Move PreviousMove { get; }

        public bool IsCheck => _gameState == GameState.Check;
        public bool IsMate => _gameState == GameState.Mate;

        #region ' MakeMove '

        [SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists"), SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate",
          Justification = "Takes considerable amount of time")]
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
                GenerateMoves(piece, moveFromSq, EnPassantFile, _caslings, res);
            }
            return res;
        }
        public List<Move> GetLegalMoves(int moveFrom)
        {
            var piece = this[moveFrom];
            if (piece == Piece.EmptyCell) return new List<Move>();
            if (piece.Color() != SideOnMove) return new List<Move>();
            var res = new List<Move>(28);
            GenerateMoves(piece, moveFrom, EnPassantFile, _caslings, res);
            return res;
        }


        private void SetStatus()
        {
            if (!IsUnderCheck(SideOnMove)) return;
            _gameState = GetLegalMoves().Count > 0
                ? GameState.Check : GameState.Mate;
        }

        public Board MakeMove(Move move)
        {
            return new Board(this, move);
        }

        private Board(Board src, Move move)
            : this(src)
        {
            PreviousMove = move;
            Previous = src;

            // Piece in the from cell?
            var moveFrom = (int)move.From;
            var piece = src[moveFrom];
            if (piece == Piece.EmptyCell)
            {
                PreviousMove.Annotations = MoveAnnotations.EmptyCell;
                return;
            }

            // Side to move?
            var color = piece.Color();
            if (color != src.SideOnMove)
            {
                PreviousMove.Annotations = (MoveAnnotations)piece | MoveAnnotations.WrongSideToMove;
                return;
            }

            // Move to occupied cell?
            var moveTo = (int)move.To;
            var toPiece = src[moveTo];
            if (toPiece != Piece.EmptyCell && toPiece.Color() == color)
            {
                PreviousMove.Annotations = (MoveAnnotations)piece | MoveAnnotations.ToOccupiedCell;
                return;
            }
            PreviousMove.Annotations = src.ValidateMove(piece,
              moveFrom, moveTo, toPiece, src._caslings);
            if (toPiece != Piece.EmptyCell) PreviousMove.Annotations |= MoveAnnotations.Capture;
            SetupBoard(src, piece, moveFrom, moveTo, move.ProposedPromotion, color);
            if ((PreviousMove.Annotations & MoveAnnotations.AllErrors) != 0) return;
            if (IsUnderCheck(SideOnMove))
            {
                _gameState = GameState.Check;
            }
           // PreviousMove.Annotations |= MoveAnnotations.TestedForConsequences;
        }
        private void SetupBoard(Board src, Piece piece,
          int moveFrom, int moveTo, PieceType proposedPromotion,
          Color color)
        {
            if ((PreviousMove.Annotations & MoveAnnotations.AllErrors) != 0) return;
            if ((PreviousMove.Annotations & MoveAnnotations.EnPassant) != 0)
            {
                if (src.EnPassantFile != moveTo % 16)
                {
                    PreviousMove.Annotations |= MoveAnnotations.HasNoEnPassant;
                    return;
                }
            }
            else if ((PreviousMove.Annotations & MoveAnnotations.Promotion) != 0)
            {
                piece = proposedPromotion.With(color);
            }

            this[moveTo] = piece;
            _cells[moveFrom] = 0;

            if ((PreviousMove.Annotations & (MoveAnnotations.PawnDoublePush | MoveAnnotations.EnPassant | MoveAnnotations.AllCastlings)) != 0)
            {
                if ((PreviousMove.Annotations & MoveAnnotations.PawnDoublePush) != 0)
                {
                    EnPassantFile = moveFrom % 16;
                }
                else if ((PreviousMove.Annotations & MoveAnnotations.EnPassant) != 0)
                {
                    _cells[moveTo + (color == Color.White ? -16 : +16)] = 0;
                }
                else if (PreviousMove.Annotations == MoveAnnotations.WK) // TODO: Move it up?
                {
                    _cells[S.H1] = (byte)Piece.EmptyCell;
                    _cells[S.F1] = (byte)Piece.WhiteRook;
                }
                else if (PreviousMove.Annotations == MoveAnnotations.WQ)
                {
                    _cells[S.A1] = (byte)Piece.EmptyCell;
                    _cells[S.D1] = (byte)Piece.WhiteRook;
                }
                else if (PreviousMove.Annotations == MoveAnnotations.BK)
                {
                    _cells[S.H8] = (byte)Piece.EmptyCell;
                    _cells[S.F8] = (byte)Piece.BlackRook;
                }
                else if (PreviousMove.Annotations == MoveAnnotations.BQ)
                {
                    _cells[S.A8] = (byte)Piece.EmptyCell;
                    _cells[S.D8] = (byte)Piece.BlackRook;
                }
            }
            if (IsUnderCheck(src.SideOnMove))
            {
                PreviousMove.Annotations |= MoveAnnotations.MoveToCheck;
            }
            _caslings = src._caslings
              & ~KilledAvailability(moveTo)
              & ~KilledAvailability(moveFrom);

            SideOnMove = color.Invert();

            HalfMoveClock =
              (PreviousMove.Annotations & (MoveAnnotations.Capture | MoveAnnotations.Pawn)) != 0
              ? 0 : src.HalfMoveClock + 1;

            MoveNumber = src.MoveNumber + (color == Color.Black ? 1 : 0);
        }

        private static Caslings KilledAvailability(int pos)
        {
            switch (pos)
            {
                case S.A1: return Caslings.WhiteQueen;
                case S.E1: return Caslings.White;
                case S.H1: return Caslings.WhiteKing;
                case S.A8: return Caslings.BlackQueen;
                case S.E8: return Caslings.Black;
                case S.H8: return Caslings.BlackKing;
                default: return Caslings.None;
            }
        }
        #endregion

        public Piece this[string index] => this[Coordinate.Parse(index)];
    }
}

