using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessKit.ChessLogic
{
	partial class Board
	{
		private const int BytesCount = 128;

		private readonly byte[] _cells;
		private int _whiteKingPosition;
		private int _blackKingPosition;

		/// <summary>Deep copy</summary>
		private Board(Board source)
		{
			_whiteKingPosition = source._whiteKingPosition;
			_blackKingPosition = source._blackKingPosition;

			_cells = new byte[BytesCount];
			Buffer.BlockCopy(source._cells, 0, _cells, 0, BytesCount);
		}

		/// <summary>Empty board</summary>
		private Board()
		{
			_cells = new byte[BytesCount];
			_whiteKingPosition = -1;
			_blackKingPosition = -1;
		}

        internal Board(BoardBuilder boardBuilder)
	    {
            _cells = new byte[BytesCount];
            Buffer.BlockCopy(boardBuilder._cells, 0, _cells, 0, BytesCount);
            SideOnMove = boardBuilder.SideOnMove;
            EnPassantFile = boardBuilder.EnPassantFile;
            HalfMoveClock = boardBuilder.HalfMoveClock;
            MoveNumber = boardBuilder.MoveNumber;
            _whiteKingPosition = Position.All.SingleOrDefault(p => this[p] == Piece.WhiteKing).CompactValue;
            _blackKingPosition = Position.All.SingleOrDefault(p => this[p] == Piece.BlackKing).CompactValue;
        }
		public CompactPiece this[int compactPosition]
		{
			get
			{
				return (CompactPiece)_cells[compactPosition];
			}
			set
			{
				_cells[compactPosition] = (byte)value;

				switch (value)
				{
					case CompactPiece.WhiteKing:
						_whiteKingPosition = compactPosition;
						break;
					case CompactPiece.BlackKing:
						_blackKingPosition = compactPosition;
						break;
				}
			}
		}

		MoveAnnotations ValidateWhiteCastlingMove(int fromSquare, int to, CastlingAvailability castlingAvailability)
		{
			if (fromSquare != E1) return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
			switch (to)
			{
				case C1: // Queenside
					if (_cells[D1] != 0 || _cells[B1] != 0) return MoveAnnotations.DoesNotJump | MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling;
					if (_cells[C1] != 0) return MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling;
					if (IsAttackedByBlack(E1)) return MoveAnnotations.CastleFromCheck | MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling;
					if (IsAttackedByBlack(D1)) return MoveAnnotations.CastleThroughCheck | MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling;
					if ((castlingAvailability & CastlingAvailability.WhiteQueen) != 0) return MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling;
					return MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling | MoveAnnotations.HasNoCastling;
				case G1: // Kingside
					if (_cells[F1] != 0) return MoveAnnotations.DoesNotJump | MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling;
					if (_cells[G1] != 0) return MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling;
					if (IsAttackedByBlack(E1)) return MoveAnnotations.CastleFromCheck | MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling;
					if (IsAttackedByBlack(F1)) return MoveAnnotations.CastleThroughCheck | MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling;
					if ((castlingAvailability & CastlingAvailability.WhiteKing) != 0) return MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling;
					return MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling | MoveAnnotations.HasNoCastling;
			}
			return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
		}
		MoveAnnotations ValidateBlackCastlingMove(int fromSquare, int to, CastlingAvailability castlingAvailability)
		{
			if (fromSquare != E8) return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
			switch (to)
			{
				case C8: // Queenside
					if (_cells[D8] != 0 || _cells[B8] != 0) return MoveAnnotations.DoesNotJump | MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling;
					if (_cells[C8] != 0) return MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling;
					if (IsAttackedByWhite(E8)) return MoveAnnotations.CastleFromCheck | MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling;
					if (IsAttackedByWhite(D8)) return MoveAnnotations.CastleThroughCheck | MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling;
					if ((castlingAvailability & CastlingAvailability.BlackQueen) != 0) return MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling;
					return MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling | MoveAnnotations.HasNoCastling;
				case G8: // Kingside
					if (_cells[F8] != 0) return MoveAnnotations.DoesNotJump | MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling;
					if (_cells[G8] != 0) return MoveAnnotations.Capture | MoveAnnotations.DoesNotCaptureThisWay | MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling;
					if (IsAttackedByWhite(E8)) return MoveAnnotations.CastleFromCheck | MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling;
					if (IsAttackedByWhite(F8)) return MoveAnnotations.CastleThroughCheck | MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling;
					if ((castlingAvailability & CastlingAvailability.BlackKing) != 0) return MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling;
					return MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling | MoveAnnotations.HasNoCastling;
			}
			return MoveAnnotations.King | MoveAnnotations.DoesNotMoveThisWay;
		}
		MoveAnnotations ValidateWhitePawnMove(int fromSquare, int to, CompactPiece toPiece)
		{
			switch (to - fromSquare)
			{
				case 32:
					if (fromSquare / 16 != 1) return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
					if (toPiece != 0)
						return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
					return _cells[fromSquare + 16] != 0
							 ? MoveAnnotations.Pawn | MoveAnnotations.DoesNotJump
							 : MoveAnnotations.Pawn | MoveAnnotations.PawnDoublePush;
				case 16:
					if (toPiece != 0)
						return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
					return to / 16 != 7 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion;
				case 17:
				case 15:
					if (toPiece == CompactPiece.EmptyCell)
						return to / 16 == 5
						  ? MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant
						  : MoveAnnotations.Pawn | MoveAnnotations.OnlyCapturesThisWay;
					return to / 16 != 7 ? MoveAnnotations.Pawn | MoveAnnotations.Capture
					  : MoveAnnotations.Pawn | MoveAnnotations.Promotion | MoveAnnotations.Capture;
				default:
					return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
			}

		}
		MoveAnnotations ValidateBlackPawnMove(int fromSquare, int to, CompactPiece toPiece)
		{
			switch (fromSquare - to)
			{
				case 32:
					if (fromSquare / 16 != 6) return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
					if (toPiece != 0)
						return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
					return _cells[fromSquare - 16] != 0
							 ? MoveAnnotations.Pawn | MoveAnnotations.DoesNotJump
							 : MoveAnnotations.Pawn | MoveAnnotations.PawnDoublePush;
				case 16:
					if (toPiece != 0)
						return MoveAnnotations.Pawn | MoveAnnotations.DoesNotCaptureThisWay;
					return to / 16 != 0 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion;
				case 17:
				case 15:
					if (toPiece == CompactPiece.EmptyCell)
						return to / 16 == 2
						  ? MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant
						  : MoveAnnotations.Pawn | MoveAnnotations.OnlyCapturesThisWay;
					return to / 16 != 0
					  ? MoveAnnotations.Pawn | MoveAnnotations.Capture
					  : MoveAnnotations.Pawn | MoveAnnotations.Promotion | MoveAnnotations.Capture;
				default:
					return MoveAnnotations.Pawn | MoveAnnotations.DoesNotMoveThisWay;
			}
		}
		void GenerateWhiteCastlingMoves(int fromSquare, CastlingAvailability castlingAvailability, List<Move> collector)
		{
			if (fromSquare != E1) return;

			if ((castlingAvailability & CastlingAvailability.WhiteQueen) != 0)
				if (_cells[D1] == 0 && _cells[C1] == 0 && _cells[B1] == 0)
					if (!IsAttackedByBlack(E1) && !IsAttackedByBlack(D1) && !IsAttackedByBlack(C1))
						collector.Add(new Move((Position) E1, (Position) C1,
							MoveAnnotations.Castling | MoveAnnotations.WhiteQueensideCastling));

			if ((castlingAvailability & CastlingAvailability.WhiteKing) != 0)
				if (_cells[F1] == 0 && _cells[G1] == 0)
					if (!IsAttackedByBlack(E1) && !IsAttackedByBlack(F1) && !IsAttackedByBlack(G1))
						collector.Add(new Move((Position) E1, (Position) G1,
							MoveAnnotations.Castling | MoveAnnotations.WhiteKingsideCastling));
		}
		void GenerateBlackCastlingMoves(int fromSquare, CastlingAvailability castlingAvailability, List<Move> collector)
		{
			if (fromSquare != E8) return;

			if ((castlingAvailability & CastlingAvailability.BlackQueen) != 0)
				if (_cells[D8] == 0 && _cells[C8] == 0 && _cells[B8] == 0)
					if (!IsAttackedByWhite(E8) && !IsAttackedByWhite(D8) && !IsAttackedByWhite(C8))
						collector.Add(new Move((Position) E8, (Position) C8,
							MoveAnnotations.Castling | MoveAnnotations.BlackQueensideCastling));

			if ((castlingAvailability & CastlingAvailability.BlackKing) != 0)
				if (_cells[F8] == 0 && _cells[G8] == 0)
					if (!IsAttackedByWhite(E8) && !IsAttackedByWhite(F8) && !IsAttackedByWhite(G8))
						collector.Add(new Move((Position) E8, (Position) G8,
							MoveAnnotations.Castling | MoveAnnotations.BlackKingsideCastling));
		}
		void GenerateWhitePawnMoves(int fromSquare, int? enPassantFile, List<Move> collector)
		{
			{
				var to = fromSquare + 16;
				if ((to & 0x88) == 0)
					if (_cells[to] == 0)
					{
						if ((_pinMap & (1ul << fromSquare)) != 0)
						{
							_cells[fromSquare] = 0;
							var toPiece = _cells[to];
							_cells[to] = (byte)CompactPiece.WhitePawn;
							if (!IsAttackedByBlack(_whiteKingPosition))
								collector.Add(new Move((Position) fromSquare, (Position) to,
									to / 16 != 7 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion));
							_cells[fromSquare] = (byte)CompactPiece.WhitePawn;
							_cells[to] = toPiece;
						}
						else
						{
							collector.Add(new Move((Position) fromSquare, (Position) to,
								to / 16 != 7 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion));
						}
					}
			}
			if ((fromSquare / 16) == 1)
			{
				if (_cells[fromSquare + 16] == 0)
				{
					var to = fromSquare + 32;
					if (_cells[to] == 0)
					{
						if ((_pinMap & (1ul << fromSquare)) != 0)
						{
							_cells[fromSquare] = 0;
							var toPiece = _cells[to];
							_cells[to] = (byte)CompactPiece.WhitePawn;
							if (!IsAttackedByBlack(_whiteKingPosition))
								collector.Add(new Move((Position) fromSquare, (Position) to,
									MoveAnnotations.Pawn | MoveAnnotations.PawnDoublePush));
							_cells[fromSquare] = (byte)CompactPiece.WhitePawn;
							_cells[to] = toPiece;
						}
						else
						{
							collector.Add(new Move((Position) fromSquare, (Position) to,
								MoveAnnotations.Pawn | MoveAnnotations.PawnDoublePush));
						}
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
						if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
						{
							if ((_pinMap & (1ul << fromSquare)) != 0)
							{
								_cells[fromSquare] = 0;
								_cells[to] = (byte)CompactPiece.WhitePawn;
								if (!IsAttackedByBlack(_whiteKingPosition))
									collector.Add(new Move((Position) fromSquare, (Position) to,
										to / 16 != 7
										? MoveAnnotations.Pawn | MoveAnnotations.Capture
										: MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
								_cells[fromSquare] = (byte)CompactPiece.WhitePawn;
								_cells[to] = toPiece;
							}
							else
							{
								collector.Add(new Move((Position) fromSquare, (Position) to,
													   to / 16 != 7
														   ? MoveAnnotations.Pawn | MoveAnnotations.Capture
														   : MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));

							}
						}
					}
					else
					{
						if (enPassantFile == (to % 16) && to / 16 == 5)
						{
							if ((_pinMap & (1ul << fromSquare)) != 0)
							{
								_cells[fromSquare] = 0;
								_cells[to] = (byte)CompactPiece.WhitePawn;
								_cells[to - 16] = 0;
								if (!IsAttackedByBlack(_whiteKingPosition))
									collector.Add(new Move((Position) fromSquare, (Position) to,
										MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
								_cells[fromSquare] = (byte)CompactPiece.WhitePawn;
								_cells[to - 16] = (byte)CompactPiece.BlackPawn;
								_cells[to] = toPiece;
							}
							else
							{
								collector.Add(new Move((Position) fromSquare, (Position) to,
									MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));

							}
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
						if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.White)
						{
							if ((_pinMap & (1ul << fromSquare)) != 0)
							{
								_cells[fromSquare] = 0;
								_cells[to] = (byte)CompactPiece.WhitePawn;
								if (!IsAttackedByBlack(_whiteKingPosition))
									collector.Add(new Move((Position) fromSquare, (Position) to,
										to / 16 != 7 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
										MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
								_cells[fromSquare] = (byte)CompactPiece.WhitePawn;
								_cells[to] = toPiece;
							}
							else
							{

								collector.Add(new Move((Position) fromSquare, (Position) to,
									to / 16 != 7 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
									MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
							}
						}
					}
					else
					{
						if (enPassantFile == (to % 16) && to / 16 == 5)
						{
							if ((_pinMap & (1ul << fromSquare)) != 0)
							{
								_cells[fromSquare] = 0;
								_cells[to] = (byte)CompactPiece.WhitePawn;
								_cells[to - 16] = 0;
								if (!IsAttackedByBlack(_whiteKingPosition))
									collector.Add(new Move((Position) fromSquare, (Position) to,
										MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
								_cells[fromSquare] = (byte)CompactPiece.WhitePawn;
								_cells[to] = (byte)CompactPiece.EmptyCell;
								_cells[to - 16] = (byte)CompactPiece.BlackPawn;
							}
							else
							{

								collector.Add(new Move((Position) fromSquare, (Position) to,
									MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
							}
						}
					}
				}
			}
		}
		void GenerateBlackPawnMoves(int fromSquare, int? enPassantFile, List<Move> collector)
		{
			{
				var to = fromSquare - 16;
				if ((to & 0x88) == 0)
					if (_cells[to] == 0)
					{
						if ((_pinMap & (1ul << fromSquare)) != 0)
						{
							_cells[fromSquare] = 0;
							var toPiece = _cells[to];
							_cells[to] = (byte)CompactPiece.BlackPawn;
							if (!IsAttackedByWhite(_blackKingPosition))
								collector.Add(new Move((Position) fromSquare, (Position) to,
									to / 16 != 0 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion));
							_cells[fromSquare] = (byte)CompactPiece.BlackPawn;
							_cells[to] = toPiece;
						}
						else
						{

							collector.Add(new Move((Position) fromSquare, (Position) to,
		to / 16 != 0 ? MoveAnnotations.Pawn : MoveAnnotations.Pawn | MoveAnnotations.Promotion));
						}
					}
			}
			if ((fromSquare / 16) == 6)
			{
				if (_cells[fromSquare - 16] == 0)
				{
					var to = fromSquare - 32;
					if (_cells[to] == 0)
					{
						if ((_pinMap & (1ul << fromSquare)) != 0)
						{
							_cells[fromSquare] = 0;
							var toPiece = _cells[to];
							_cells[to] = (byte)CompactPiece.BlackPawn;
							if (!IsAttackedByWhite(_blackKingPosition))
								collector.Add(new Move((Position) fromSquare, (Position) to,
									MoveAnnotations.Pawn | MoveAnnotations.PawnDoublePush));
							_cells[fromSquare] = (byte)CompactPiece.BlackPawn;
							_cells[to] = toPiece;
						}
						else
						{

							collector.Add(new Move((Position) fromSquare, (Position) to,
								MoveAnnotations.Pawn | MoveAnnotations.PawnDoublePush));
						}
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
						if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
						{
							if ((_pinMap & (1ul << fromSquare)) != 0)
							{
								_cells[fromSquare] = 0;
								_cells[to] = (byte)CompactPiece.BlackPawn;
								if (!IsAttackedByWhite(_blackKingPosition))
									collector.Add(new Move((Position) fromSquare, (Position) to,
										to / 16 != 0 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
										MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
								_cells[fromSquare] = (byte)CompactPiece.BlackPawn;
								_cells[to] = toPiece;
							}
							else
							{
								collector.Add(new Move((Position) fromSquare, (Position) to,
									to / 16 != 0 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
									MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));

							}
						}
					}
					else
					{
						if (enPassantFile == (to % 16) && to / 16 == 2)
						{
							if ((_pinMap & (1ul << fromSquare)) != 0)
							{
								_cells[fromSquare] = 0;
								_cells[to + 16] = 0;
								_cells[to] = (byte)CompactPiece.BlackPawn;
								if (!IsAttackedByWhite(_blackKingPosition))
									collector.Add(new Move((Position) fromSquare, (Position) to,
										MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
								_cells[fromSquare] = (byte)CompactPiece.BlackPawn;
								_cells[to] = toPiece;
								_cells[to + 16] = (byte)CompactPiece.WhitePawn;
							}
							else
							{
								collector.Add(new Move((Position) fromSquare, (Position) to,
									MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));

							}
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
						if ((PieceColor)(toPiece & (byte)PieceColor.Black) != PieceColor.Black)
						{
							if ((_pinMap & (1ul << fromSquare)) != 0)
							{
								_cells[fromSquare] = 0;
								_cells[to] = (byte)CompactPiece.BlackPawn;
								if (!IsAttackedByWhite(_blackKingPosition))
									collector.Add(new Move((Position) fromSquare, (Position) to,
										to / 16 != 0 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
										MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));
								_cells[fromSquare] = (byte)CompactPiece.BlackPawn;
								_cells[to] = (byte)toPiece;
							}
							else
							{
								collector.Add(new Move((Position) fromSquare, (Position) to,
									to / 16 != 0 ? MoveAnnotations.Pawn | MoveAnnotations.Capture :
									MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.Promotion));

							}
						}
					}
					else
					{
						if (enPassantFile == (to % 16) && to / 16 == 2)
						{
							if ((_pinMap & (1ul << fromSquare)) != 0)
							{
								_cells[fromSquare] = 0;
								_cells[to] = (byte)CompactPiece.BlackPawn;
								_cells[to + 16] = 0;
								if (!IsAttackedByWhite(_blackKingPosition))
									collector.Add(new Move((Position) fromSquare, (Position) to,
										MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
								_cells[fromSquare] = (byte)CompactPiece.BlackPawn;
								_cells[to + 16] = (byte)CompactPiece.WhitePawn;
								_cells[to] = toPiece;
							}
							else
							{
								collector.Add(new Move((Position) fromSquare, (Position) to,
									MoveAnnotations.Pawn | MoveAnnotations.Capture | MoveAnnotations.EnPassant));
							}
						}
					}
				}
			}
		}

	    private bool IsUnderCheck(PieceColor kingColor)
		{
			return kingColor == PieceColor.White
			  ? IsAttackedByBlack(_whiteKingPosition)
			  : IsAttackedByWhite(_blackKingPosition);
		}
	}
}
