﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ChessKit.ChessLogic
{
    public sealed partial class Board
	{
		private GameState _gameState;

		/// <summary>The side which is moving next (white or black)</summary>
		public PieceColor SideOnMove { get; private set; }
		/// <summary>Gets the file at which en passant move is available</summary>
		[SuppressMessage("Microsoft.Naming",
		  "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "En",
		  Justification = "'En-passant' is borrowed from french I guess")]
		public int? EnPassantFile { get; set; }

		/// <summary>Gets sides players can castle to</summary>
		private CastlingAvailability _castlingAvailability;
		/// <summary>This is the number of halfmoves since the last pawn advance or capture. </summary>
		/// <remarks>This is used to determine if a draw can be claimed under the fifty-move rule.</remarks>
		public int HalfMoveClock { get; set; }
		/// <summary>The number of the full move. It starts at 1, and is incremented after Black's move</summary>
		public int MoveNumber { get; private set; }

		public Board Previous { get; private set; }
		public Move PreviousMove { get; private set; }

		public bool IsCheck => _gameState == GameState.Check;
        public bool IsMate => _gameState == GameState.WhiteWin || _gameState == GameState.BlackWin;

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
				if (Piece.UnpackColor(piece) != sideOnMove) continue;
				GenerateMoves(piece, moveFromSq, EnPassantFile, _castlingAvailability, res);
			}
			return res;
		}
		public List<Move> GetLegalMoves(Position from)
		{
			return GetLegalMoves((int)@from);
		}
		public List<Move> GetLegalMoves(int moveFrom)
		{
			var piece = this[moveFrom];
			if (piece == CompactPiece.EmptyCell) return new List<Move>();
			if (Piece.UnpackColor(piece) != SideOnMove) return new List<Move>();
			var res = new List<Move>(28);
			GenerateMoves(piece, moveFrom, EnPassantFile, _castlingAvailability, res);
			return res;
		}


		private void SetStatus()
		{
			if (!IsUnderCheck(SideOnMove))
			{
				return;
			}
			if (GetLegalMoves().Count > 0)
				_gameState = GameState.Check;
			else _gameState = SideOnMove == PieceColor.White ?
			  GameState.BlackWin : GameState.WhiteWin;
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
			if (piece == CompactPiece.EmptyCell)
			{
				PreviousMove.Annotations = MoveAnnotations.EmptyCell;
				return;
			}

			// Side to move?
			var color = Piece.UnpackColor(piece);
			if (color != src.SideOnMove)
			{
				PreviousMove.Annotations = (MoveAnnotations)piece | MoveAnnotations.WrongSideToMove;
				return;
			}

			// Move to occupied cell?
			var moveTo = (int)move.To;
			var toPiece = src[moveTo];
			if (toPiece != CompactPiece.EmptyCell && Piece.UnpackColor(toPiece) == color)
			{
				PreviousMove.Annotations = (MoveAnnotations)piece | MoveAnnotations.ToOccupiedCell;
				return;
			}
			PreviousMove.Annotations = src.ValidateMove(piece,
			  moveFrom, moveTo, toPiece, src._castlingAvailability);
			if (toPiece != CompactPiece.EmptyCell) PreviousMove.Annotations |= MoveAnnotations.Capture;
			SetupBoard(src, piece, moveFrom, moveTo, move.ProposedPromotion, color);
			if ((PreviousMove.Annotations & MoveAnnotations.AllErrors) != 0) return;
			if (IsUnderCheck(SideOnMove))
			{
				PreviousMove.Annotations |= MoveAnnotations.Check;
				_gameState = GameState.Check;
			}
			PreviousMove.Annotations |= MoveAnnotations.TestedForConsequences;
		}
		private void SetupBoard(Board src, CompactPiece piece,
		  int moveFrom, int moveTo, PieceType proposedPromotion,
		  PieceColor color)
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
				piece = Piece.Pack(proposedPromotion, color);
			}

			this[moveTo] = piece;
			_cells[moveFrom] = 0;

			if ((PreviousMove.Annotations & (MoveAnnotations.PawnDoublePush | MoveAnnotations.EnPassant | MoveAnnotations.Castling)) != 0)
			{
				if ((PreviousMove.Annotations & MoveAnnotations.PawnDoublePush) != 0)
				{
					EnPassantFile = moveFrom % 16;
				}
				else if ((PreviousMove.Annotations & MoveAnnotations.EnPassant) != 0)
				{
					_cells[moveTo + (color == PieceColor.White ? -16 : +16)] = 0;
				}
				else if (PreviousMove.Annotations == (MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling)) // TODO: Move it up?
				{
					_cells[H1] = (byte)CompactPiece.EmptyCell;
					_cells[F1] = (byte)CompactPiece.WhiteRook;
				}
				else if (PreviousMove.Annotations == (MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling))
				{
					_cells[A1] = (byte)CompactPiece.EmptyCell;
					_cells[D1] = (byte)CompactPiece.WhiteRook;
				}
				else if (PreviousMove.Annotations == (MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling))
				{
					_cells[H8] = (byte)CompactPiece.EmptyCell;
					_cells[F8] = (byte)CompactPiece.BlackRook;
				}
				else if (PreviousMove.Annotations == (MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling))
				{
					_cells[A8] = (byte)CompactPiece.EmptyCell;
					_cells[D8] = (byte)CompactPiece.BlackRook;
				}
			}
			if (IsUnderCheck(src.SideOnMove))
			{
				PreviousMove.Annotations |= MoveAnnotations.MoveToCheck;
			}
			_castlingAvailability = src._castlingAvailability
			  & ~KilledAvailability(moveTo)
			  & ~KilledAvailability(moveFrom);

			SideOnMove = color.Invert();

			HalfMoveClock =
			  (PreviousMove.Annotations & (MoveAnnotations.Capture | MoveAnnotations.Pawn)) != 0
			  ? 0 : src.HalfMoveClock + 1;

			MoveNumber = src.MoveNumber + (color == PieceColor.Black ? 1 : 0);
		}

		private static CastlingAvailability KilledAvailability(int pos)
		{
			switch (pos)
			{
				case A1: return CastlingAvailability.WhiteQueen;
				case E1: return CastlingAvailability.White;
				case H1: return CastlingAvailability.WhiteKing;
				case A8: return CastlingAvailability.BlackQueen;
				case E8: return CastlingAvailability.Black;
				case H8: return CastlingAvailability.BlackKing;
				default: return CastlingAvailability.None;
			}
		}
		#endregion


		[SuppressMessage("Microsoft.Design", "CA1043:UseIntegralOrStringArgumentForIndexers",
		  Justification = "It just seems right to use Position as a natural indexer")]
		public Piece this[Position index]
		{
			get { return Piece.Unpack((CompactPiece)_cells[(int)index]); }
		}

		public Piece this[string index]
		{
			get { return this[Position.Parse(index)]; }
		}
	}
}

