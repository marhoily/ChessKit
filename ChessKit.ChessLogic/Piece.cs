using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using JetBrains.Annotations;

namespace ChessKit.ChessLogic
{
	/// <summary>Represents piece Type</summary>
	[Immutable, PublicAPI]
	public sealed class Piece
	{
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
		  Justification = "It is really immutable")]
		public static readonly IComparer<Piece> TypeComparer = new PieceTypeComparer();

		[Immutable]
		private class PieceTypeComparer : IComparer<Piece>
		{
			public int Compare(Piece x, Piece y)
			{
				if (x == null) throw new ArgumentNullException("x");
				if (y == null) throw new ArgumentNullException("y");
				return (int)x.PieceType - (int)y.PieceType;
			}
		}

		#region ' Instance '

		public PieceType PieceType { get; private set; }

		/// <summary>Piece color</summary>
		public PieceColor Color { get; private set; }

		/// <summary>Russian piece name</summary>
		public string Russian { get; private set; }

		/// <summary>Gets the latin piece symbol</summary>
		public string Symbol { get; private set; }

		public char ChessChar { get; private set; }

		/// <summary>English piece name</summary>
		public string English { get; private set; }

		/// <summary>Gets all directions of move piece can do</summary>
		public ReadOnlyCollection<MoveDirection> MoveDirections { get; private set; }

		public CompactPiece CompactValue { get; private set; }

		internal Piece(CompactPiece compactValue, string symbol,
		  char chessChar, string english, string russian)
		{
			CompactValue = compactValue;
			PieceType = (PieceType)((MoveHints)compactValue & MoveHints.AllPieces);
			Symbol = symbol;
			ChessChar = chessChar;
			English = english;
			Russian = russian;
			Color = (PieceColor)((MoveHints)compactValue & MoveHints.Black);
		}

		public override string ToString()
		{
			return Symbol;
		}

		#endregion

		#region ' Parse '

		//		private static readonly Dictionary<string, Piece> ParserDic;
		private static readonly Piece[] PieceBySymbol;

		/// <summary>Parses a symbol into a Piece.
		///   Recognizes japanese hieroglyphs as well as latin symbols</summary>
		public static Piece Parse(char symbol)
		{
			var idx = symbol - 'A';
			if (idx < 0 || idx >= PieceBySymbol.Length)
				throw new FormatException("illegal character: " + symbol);
			var piece = PieceBySymbol[idx];
			if (piece == null)
				throw new FormatException("illegal character: " + symbol);
			return piece;
		}
	
		/// <summary>Tries to parse a symbol into a PieceType.
		///   Recognizes japanese hieroglyphs as well as latin symbols</summary>
		/// <returns>false if it couldn't parse the symbol</returns>
		public static bool TryParse(char symbol, out Piece result)
		{
			var idx = symbol - 'A';
			if (idx < 0 || idx >= PieceBySymbol.Length)
			{
				result = null;
				return false;
			}
			result = PieceBySymbol[idx];
			return result != null;
		}


		#endregion

		#region ' Public Constants '

		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "In fact immutable")]
		public static readonly Piece WhitePawn;
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "In fact immutable")]
		public static readonly Piece WhiteBishop;
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "In fact immutable")]
		public static readonly Piece WhiteKnight;
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "In fact immutable")]
		public static readonly Piece WhiteRook;
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "In fact immutable")]
		public static readonly Piece WhiteQueen;
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "In fact immutable")]
		public static readonly Piece WhiteKing;

		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "In fact immutable")]
		public static readonly Piece BlackPawn;
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "In fact immutable")]
		public static readonly Piece BlackBishop;
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "In fact immutable")]
		public static readonly Piece BlackKnight;
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "In fact immutable")]
		public static readonly Piece BlackRook;
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "In fact immutable")]
		public static readonly Piece BlackQueen;
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "In fact immutable")]
		public static readonly Piece BlackKing;

		/// <summary>All Types pieces may have</summary>
		public static ReadOnlyCollection<Piece> All { get; private set; }

		[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline",
		  Justification = "Too complex to initialize without ctor")]
		static Piece()
		{
			//      _______________
			// ____/ Create Pieces \____________________________________________________________
			WhitePawn = new Piece(CompactPiece.WhitePawn,     "P", '♙', "White Pawn", "Белая пешка");
			WhiteBishop = new Piece(CompactPiece.WhiteBishop, "B", '♗', "White Bishop", "Белый слон");
			WhiteKnight = new Piece(CompactPiece.WhiteKnight, "N", '♘', "White Knight", "Белый конь");
			WhiteRook = new Piece(CompactPiece.WhiteRook,     "R", '♖', "White Rook", "Белая ладья");
			WhiteQueen = new Piece(CompactPiece.WhiteQueen,   "Q", '♕', "White Queen", "Белый ферзь");
			WhiteKing = new Piece(CompactPiece.WhiteKing,     "K", '♔', "White King", "Белый король");

			BlackPawn = new Piece(CompactPiece.BlackPawn,     "p", '♟', "Black Pawn", "Черная пешка");
			BlackBishop = new Piece(CompactPiece.BlackBishop, "b", '♝', "Black Bishop", "Черный слон");
			BlackKnight = new Piece(CompactPiece.BlackKnight, "n", '♞', "Black Knight", "Черный конь");
			BlackRook = new Piece(CompactPiece.BlackRook,     "r", '♜', "Black Rook", "Черная ладья");
			BlackQueen = new Piece(CompactPiece.BlackQueen,   "q", '♛', "Black Queen", "Черный ферзь");
			BlackKing = new Piece(CompactPiece.BlackKing,     "k", '♚', "Black King", "Черный король");

			//      _____________
			// ____/ Collections \______________________________________________________________
			All = new ReadOnlyCollection<Piece>(new[]
					{
						WhitePawn, WhiteBishop, WhiteKnight, WhiteRook, 
						WhiteQueen, WhiteKing, BlackPawn, BlackBishop, 
						BlackKnight, BlackRook, BlackQueen, BlackKing,
					});


			//      ______________
			// ____/ Dictionaries \_____________________________________________________________
			PieceBySymbol = new Piece['z' - 'A'];
			foreach (var piece in All)
				PieceBySymbol[piece.Symbol[0] - 'A'] = piece;

			CompactPieceBySymbol = new CompactPiece['z' - 'A'];
			foreach (var piece in All)
				CompactPieceBySymbol[piece.Symbol[0] - 'A'] = piece.CompactValue;

			TypeColorMap = new Piece[(int)BlackKing.CompactValue + 1];
			foreach (var piece in All)
				TypeColorMap[(int)piece.CompactValue] = piece;

			//      ________________
			// ____/ MoveDirections \___________________________________________________________
			WhitePawn.MoveDirections = Join(Move(0, 1), Take(1, 1), Take(-1, 1), Special(0, 2));
			WhiteBishop.MoveDirections = Join(Slide(1, 1), Slide(1, -1), Slide(-1, -1), Slide(-1, 1));
			WhiteKnight.MoveDirections = Join(Take(1, 2), Take(-1, 2), Take(1, -2), Take(-1, -2), Take(2, 1), Take(-2, 1), Take(2, -1), Take(-2, -1));
			WhiteRook.MoveDirections = Join(Slide(0, 1), Slide(1, 0), Slide(0, -1), Slide(-1, 0));
			WhiteQueen.MoveDirections = Join(WhiteRook.MoveDirections, WhiteBishop.MoveDirections);
			WhiteKing.MoveDirections = Join(Take(0, 1), Take(1, 1), Take(1, 0), Take(1, -1), 
				Take(0, -1), Take(-1, -1), Take(-1, 0), Take(-1, 1),
				Special(2, 0), Special(-2, 0));

			BlackPawn.MoveDirections = Join(Move(0, -1), Take(1, -1), Take(-1, -1), Special(0, -2));
			BlackBishop.MoveDirections = WhiteBishop.MoveDirections;
			BlackKnight.MoveDirections = WhiteKnight.MoveDirections;
			BlackRook.MoveDirections = WhiteRook.MoveDirections;
			BlackQueen.MoveDirections = WhiteQueen.MoveDirections;
			BlackKing.MoveDirections = WhiteKing.MoveDirections;
		}

		#endregion

		#region ' Piece '

		private static readonly Piece[] TypeColorMap;

		#endregion

		#region ' MoveDirection '

		private static ReadOnlyCollection<T> Join<T>(params IEnumerable<T>[] arr)
		{
			return new ReadOnlyCollection<T>(arr.SelectMany(e => e).ToList());
		}

		private static IEnumerable<MoveDirection> Slide(int dx, int dy)
		{
			yield return new MoveDirection(dx, dy, 8, true);
		}
		private static IEnumerable<MoveDirection> Take(int dx, int dy)
		{
			yield return new MoveDirection(dx, dy, 1, true);
		}
		private static IEnumerable<MoveDirection> Move(int dx, int dy)
		{
			yield return new MoveDirection(dx, dy, 1, false);
		}
		private static IEnumerable<MoveDirection> Special(int dx, int dy)
		{
			yield return new MoveDirection(dx, dy, 1, false, true);
		}

		#endregion

		public static Piece Get(PieceType type, PieceColor color)
		{
			return TypeColorMap[(int)type | (int)color];
		}

		#region ' Compact '

		private static readonly CompactPiece[] CompactPieceBySymbol;

		public static CompactPiece ParseCompact(char symbol)
		{
			var idx = symbol - 'A';
			if (idx < 0 || idx >= CompactPieceBySymbol.Length)
				throw new FormatException("illegal character: " + symbol);
			var piece = CompactPieceBySymbol[idx];
			if (piece == CompactPiece.EmptyCell)
				throw new FormatException("illegal character: " + symbol);
			return piece;
		}
		public static bool TryParseCompact(char symbol, out CompactPiece result)
		{
			var idx = symbol - 'A';
			if (idx < 0 || idx >= CompactPieceBySymbol.Length)
			{
				result = CompactPiece.EmptyCell;
				return false;
			}
			result = CompactPieceBySymbol[idx];
			return result != CompactPiece.EmptyCell;
		}
		public static CompactPiece Pack(Piece value)
		{
			return value == null ? CompactPiece.EmptyCell : value.CompactValue;
		}

		public static Piece Unpack(CompactPiece value)
		{
			return TypeColorMap[(int)value];
		}
		public static CompactPiece Pack(PieceType pieceType, PieceColor pieceColor)
		{
			return (CompactPiece)((MoveHints)pieceType | (MoveHints)pieceColor);
		}
		public static PieceColor UnpackColor(CompactPiece piece)
		{
			return (PieceColor)((MoveHints)piece & (MoveHints)PieceColor.Black);
		}
		public static PieceType UnpackType(CompactPiece piece)
		{
			return (PieceType)((MoveHints)piece & ~(MoveHints)PieceColor.Black);
		}

		#endregion
	}

    public class ImmutableAttribute : Attribute
    {
    }
}