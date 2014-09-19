using System;
using System.Collections.Generic;

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

		MoveHints ValidateWhiteCastlingMove(int fromSquare, int to, CastlingAvailability castlingAvailability)
		{
			if (fromSquare != E1) return MoveHints.King | MoveHints.DoesNotMoveThisWay;
			switch (to)
			{
				case C1: // Queenside
					if (_cells[D1] != 0 || _cells[B1] != 0) return MoveHints.DoesNotJump | MoveHints.Castling | MoveHints.WhiteQueensideCastling;
					if (_cells[C1] != 0) return MoveHints.Capture | MoveHints.DoesNotCaptureThisWay | MoveHints.Castling | MoveHints.WhiteQueensideCastling;
					if (IsAttackedByBlack(E1)) return MoveHints.CastleFromCheck | MoveHints.Castling | MoveHints.WhiteQueensideCastling;
					if (IsAttackedByBlack(D1)) return MoveHints.CastleThroughCheck | MoveHints.Castling | MoveHints.WhiteQueensideCastling;
					if ((castlingAvailability & CastlingAvailability.WhiteQueen) != 0) return MoveHints.Castling | MoveHints.WhiteQueensideCastling;
					return MoveHints.Castling | MoveHints.WhiteQueensideCastling | MoveHints.HasNoCastling;
				case G1: // Kingside
					if (_cells[F1] != 0) return MoveHints.DoesNotJump | MoveHints.Castling | MoveHints.WhiteKingsideCastling;
					if (_cells[G1] != 0) return MoveHints.Capture | MoveHints.DoesNotCaptureThisWay | MoveHints.Castling | MoveHints.WhiteKingsideCastling;
					if (IsAttackedByBlack(E1)) return MoveHints.CastleFromCheck | MoveHints.Castling | MoveHints.WhiteKingsideCastling;
					if (IsAttackedByBlack(F1)) return MoveHints.CastleThroughCheck | MoveHints.Castling | MoveHints.WhiteKingsideCastling;
					if ((castlingAvailability & CastlingAvailability.WhiteKing) != 0) return MoveHints.Castling | MoveHints.WhiteKingsideCastling;
					return MoveHints.Castling | MoveHints.WhiteKingsideCastling | MoveHints.HasNoCastling;
			}
			return MoveHints.King | MoveHints.DoesNotMoveThisWay;
		}
		MoveHints ValidateBlackCastlingMove(int fromSquare, int to, CastlingAvailability castlingAvailability)
		{
			if (fromSquare != E8) return MoveHints.King | MoveHints.DoesNotMoveThisWay;
			switch (to)
			{
				case C8: // Queenside
					if (_cells[D8] != 0 || _cells[B8] != 0) return MoveHints.DoesNotJump | MoveHints.Castling | MoveHints.BlackQueensideCastling;
					if (_cells[C8] != 0) return MoveHints.Capture | MoveHints.DoesNotCaptureThisWay | MoveHints.Castling | MoveHints.BlackQueensideCastling;
					if (IsAttackedByWhite(E8)) return MoveHints.CastleFromCheck | MoveHints.Castling | MoveHints.BlackQueensideCastling;
					if (IsAttackedByWhite(D8)) return MoveHints.CastleThroughCheck | MoveHints.Castling | MoveHints.BlackQueensideCastling;
					if ((castlingAvailability & CastlingAvailability.BlackQueen) != 0) return MoveHints.Castling | MoveHints.BlackQueensideCastling;
					return MoveHints.Castling | MoveHints.BlackQueensideCastling | MoveHints.HasNoCastling;
				case G8: // Kingside
					if (_cells[F8] != 0) return MoveHints.DoesNotJump | MoveHints.Castling | MoveHints.BlackKingsideCastling;
					if (_cells[G8] != 0) return MoveHints.Capture | MoveHints.DoesNotCaptureThisWay | MoveHints.Castling | MoveHints.BlackKingsideCastling;
					if (IsAttackedByWhite(E8)) return MoveHints.CastleFromCheck | MoveHints.Castling | MoveHints.BlackKingsideCastling;
					if (IsAttackedByWhite(F8)) return MoveHints.CastleThroughCheck | MoveHints.Castling | MoveHints.BlackKingsideCastling;
					if ((castlingAvailability & CastlingAvailability.BlackKing) != 0) return MoveHints.Castling | MoveHints.BlackKingsideCastling;
					return MoveHints.Castling | MoveHints.BlackKingsideCastling | MoveHints.HasNoCastling;
			}
			return MoveHints.King | MoveHints.DoesNotMoveThisWay;
		}
		MoveHints ValidateWhitePawnMove(int fromSquare, int to, CompactPiece toPiece)
		{
			switch (to - fromSquare)
			{
				case 32:
					if (fromSquare / 16 != 1) return MoveHints.Pawn | MoveHints.DoesNotMoveThisWay;
					if (toPiece != 0)
						return MoveHints.Pawn | MoveHints.DoesNotCaptureThisWay;
					return _cells[fromSquare + 16] != 0
							 ? MoveHints.Pawn | MoveHints.DoesNotJump
							 : MoveHints.Pawn | MoveHints.PawnDoublePush;
				case 16:
					if (toPiece != 0)
						return MoveHints.Pawn | MoveHints.DoesNotCaptureThisWay;
					return to / 16 != 7 ? MoveHints.Pawn : MoveHints.Pawn | MoveHints.Promotion;
				case 17:
				case 15:
					if (toPiece == CompactPiece.EmptyCell)
						return to / 16 == 5
						  ? MoveHints.Pawn | MoveHints.Capture | MoveHints.EnPassant
						  : MoveHints.Pawn | MoveHints.OnlyCapturesThisWay;
					return to / 16 != 7 ? MoveHints.Pawn | MoveHints.Capture
					  : MoveHints.Pawn | MoveHints.Promotion | MoveHints.Capture;
				default:
					return MoveHints.Pawn | MoveHints.DoesNotMoveThisWay;
			}

		}
		MoveHints ValidateBlackPawnMove(int fromSquare, int to, CompactPiece toPiece)
		{
			switch (fromSquare - to)
			{
				case 32:
					if (fromSquare / 16 != 6) return MoveHints.Pawn | MoveHints.DoesNotMoveThisWay;
					if (toPiece != 0)
						return MoveHints.Pawn | MoveHints.DoesNotCaptureThisWay;
					return _cells[fromSquare - 16] != 0
							 ? MoveHints.Pawn | MoveHints.DoesNotJump
							 : MoveHints.Pawn | MoveHints.PawnDoublePush;
				case 16:
					if (toPiece != 0)
						return MoveHints.Pawn | MoveHints.DoesNotCaptureThisWay;
					return to / 16 != 0 ? MoveHints.Pawn : MoveHints.Pawn | MoveHints.Promotion;
				case 17:
				case 15:
					if (toPiece == CompactPiece.EmptyCell)
						return to / 16 == 2
						  ? MoveHints.Pawn | MoveHints.Capture | MoveHints.EnPassant
						  : MoveHints.Pawn | MoveHints.OnlyCapturesThisWay;
					return to / 16 != 0
					  ? MoveHints.Pawn | MoveHints.Capture
					  : MoveHints.Pawn | MoveHints.Promotion | MoveHints.Capture;
				default:
					return MoveHints.Pawn | MoveHints.DoesNotMoveThisWay;
			}
		}
		void GenerateWhiteCastlingMoves(int fromSquare, CastlingAvailability castlingAvailability, List<Move> collector)
		{
			if (fromSquare != E1) return;

			if ((castlingAvailability & CastlingAvailability.WhiteQueen) != 0)
				if (_cells[D1] == 0 && _cells[C1] == 0 && _cells[B1] == 0)
					if (!IsAttackedByBlack(E1) && !IsAttackedByBlack(D1) && !IsAttackedByBlack(C1))
						collector.Add(new Move((Position) E1, (Position) C1,
							MoveHints.Castling | MoveHints.WhiteQueensideCastling));

			if ((castlingAvailability & CastlingAvailability.WhiteKing) != 0)
				if (_cells[F1] == 0 && _cells[G1] == 0)
					if (!IsAttackedByBlack(E1) && !IsAttackedByBlack(F1) && !IsAttackedByBlack(G1))
						collector.Add(new Move((Position) E1, (Position) G1,
							MoveHints.Castling | MoveHints.WhiteKingsideCastling));
		}
		void GenerateBlackCastlingMoves(int fromSquare, CastlingAvailability castlingAvailability, List<Move> collector)
		{
			if (fromSquare != E8) return;

			if ((castlingAvailability & CastlingAvailability.BlackQueen) != 0)
				if (_cells[D8] == 0 && _cells[C8] == 0 && _cells[B8] == 0)
					if (!IsAttackedByWhite(E8) && !IsAttackedByWhite(D8) && !IsAttackedByWhite(C8))
						collector.Add(new Move((Position) E8, (Position) C8,
							MoveHints.Castling | MoveHints.BlackQueensideCastling));

			if ((castlingAvailability & CastlingAvailability.BlackKing) != 0)
				if (_cells[F8] == 0 && _cells[G8] == 0)
					if (!IsAttackedByWhite(E8) && !IsAttackedByWhite(F8) && !IsAttackedByWhite(G8))
						collector.Add(new Move((Position) E8, (Position) G8,
							MoveHints.Castling | MoveHints.BlackKingsideCastling));
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
									to / 16 != 7 ? MoveHints.Pawn : MoveHints.Pawn | MoveHints.Promotion));
							_cells[fromSquare] = (byte)CompactPiece.WhitePawn;
							_cells[to] = toPiece;
						}
						else
						{
							collector.Add(new Move((Position) fromSquare, (Position) to,
								to / 16 != 7 ? MoveHints.Pawn : MoveHints.Pawn | MoveHints.Promotion));
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
									MoveHints.Pawn | MoveHints.PawnDoublePush));
							_cells[fromSquare] = (byte)CompactPiece.WhitePawn;
							_cells[to] = toPiece;
						}
						else
						{
							collector.Add(new Move((Position) fromSquare, (Position) to,
								MoveHints.Pawn | MoveHints.PawnDoublePush));
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
										? MoveHints.Pawn | MoveHints.Capture
										: MoveHints.Pawn | MoveHints.Capture | MoveHints.Promotion));
								_cells[fromSquare] = (byte)CompactPiece.WhitePawn;
								_cells[to] = toPiece;
							}
							else
							{
								collector.Add(new Move((Position) fromSquare, (Position) to,
													   to / 16 != 7
														   ? MoveHints.Pawn | MoveHints.Capture
														   : MoveHints.Pawn | MoveHints.Capture | MoveHints.Promotion));

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
										MoveHints.Pawn | MoveHints.Capture | MoveHints.EnPassant));
								_cells[fromSquare] = (byte)CompactPiece.WhitePawn;
								_cells[to - 16] = (byte)CompactPiece.BlackPawn;
								_cells[to] = toPiece;
							}
							else
							{
								collector.Add(new Move((Position) fromSquare, (Position) to,
									MoveHints.Pawn | MoveHints.Capture | MoveHints.EnPassant));

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
										to / 16 != 7 ? MoveHints.Pawn | MoveHints.Capture :
										MoveHints.Pawn | MoveHints.Capture | MoveHints.Promotion));
								_cells[fromSquare] = (byte)CompactPiece.WhitePawn;
								_cells[to] = toPiece;
							}
							else
							{

								collector.Add(new Move((Position) fromSquare, (Position) to,
									to / 16 != 7 ? MoveHints.Pawn | MoveHints.Capture :
									MoveHints.Pawn | MoveHints.Capture | MoveHints.Promotion));
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
										MoveHints.Pawn | MoveHints.Capture | MoveHints.EnPassant));
								_cells[fromSquare] = (byte)CompactPiece.WhitePawn;
								_cells[to] = (byte)CompactPiece.EmptyCell;
								_cells[to - 16] = (byte)CompactPiece.BlackPawn;
							}
							else
							{

								collector.Add(new Move((Position) fromSquare, (Position) to,
									MoveHints.Pawn | MoveHints.Capture | MoveHints.EnPassant));
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
									to / 16 != 0 ? MoveHints.Pawn : MoveHints.Pawn | MoveHints.Promotion));
							_cells[fromSquare] = (byte)CompactPiece.BlackPawn;
							_cells[to] = toPiece;
						}
						else
						{

							collector.Add(new Move((Position) fromSquare, (Position) to,
		to / 16 != 0 ? MoveHints.Pawn : MoveHints.Pawn | MoveHints.Promotion));
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
									MoveHints.Pawn | MoveHints.PawnDoublePush));
							_cells[fromSquare] = (byte)CompactPiece.BlackPawn;
							_cells[to] = toPiece;
						}
						else
						{

							collector.Add(new Move((Position) fromSquare, (Position) to,
								MoveHints.Pawn | MoveHints.PawnDoublePush));
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
										to / 16 != 0 ? MoveHints.Pawn | MoveHints.Capture :
										MoveHints.Pawn | MoveHints.Capture | MoveHints.Promotion));
								_cells[fromSquare] = (byte)CompactPiece.BlackPawn;
								_cells[to] = toPiece;
							}
							else
							{
								collector.Add(new Move((Position) fromSquare, (Position) to,
									to / 16 != 0 ? MoveHints.Pawn | MoveHints.Capture :
									MoveHints.Pawn | MoveHints.Capture | MoveHints.Promotion));

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
										MoveHints.Pawn | MoveHints.Capture | MoveHints.EnPassant));
								_cells[fromSquare] = (byte)CompactPiece.BlackPawn;
								_cells[to] = toPiece;
								_cells[to + 16] = (byte)CompactPiece.WhitePawn;
							}
							else
							{
								collector.Add(new Move((Position) fromSquare, (Position) to,
									MoveHints.Pawn | MoveHints.Capture | MoveHints.EnPassant));

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
										to / 16 != 0 ? MoveHints.Pawn | MoveHints.Capture :
										MoveHints.Pawn | MoveHints.Capture | MoveHints.Promotion));
								_cells[fromSquare] = (byte)CompactPiece.BlackPawn;
								_cells[to] = (byte)toPiece;
							}
							else
							{
								collector.Add(new Move((Position) fromSquare, (Position) to,
									to / 16 != 0 ? MoveHints.Pawn | MoveHints.Capture :
									MoveHints.Pawn | MoveHints.Capture | MoveHints.Promotion));

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
										MoveHints.Pawn | MoveHints.Capture | MoveHints.EnPassant));
								_cells[fromSquare] = (byte)CompactPiece.BlackPawn;
								_cells[to + 16] = (byte)CompactPiece.WhitePawn;
								_cells[to] = toPiece;
							}
							else
							{
								collector.Add(new Move((Position) fromSquare, (Position) to,
									MoveHints.Pawn | MoveHints.Capture | MoveHints.EnPassant));
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
