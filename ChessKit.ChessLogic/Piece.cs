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
    public sealed class Piece_
    {
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "It is really immutable")]
        public static readonly IComparer<Piece_> TypeComparer =
                new PieceTypeComparer();

        #region ' Piece_ '

        private static readonly Piece_[] TypeColorMap;

        #endregion

        public static Piece_ Get(PieceType type, PieceColor color)
        {
            return TypeColorMap[(int)type | (int)color];
        }

        [Immutable]
        private class PieceTypeComparer : IComparer<Piece_>
        {
            public int Compare(Piece_ x, Piece_ y)
            {
                if (x == null) throw new ArgumentNullException("x");
                if (y == null) throw new ArgumentNullException("y");
                return (int)x.PieceType - (int)y.PieceType;
            }
        }

        #region ' Instance '

        public PieceType PieceType { get; private set; }

        /// <summary>Piece_ color</summary>
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

        internal Piece_(CompactPiece compactValue, string symbol,
            char chessChar, string english, string russian)
        {
            CompactValue = compactValue;
            PieceType = (PieceType)((MoveAnnotations)compactValue & MoveAnnotations.AllPieces);
            Symbol = symbol;
            ChessChar = chessChar;
            English = english;
            Russian = russian;
            Color = (PieceColor)((MoveAnnotations)compactValue & MoveAnnotations.Black);
        }

        public override string ToString()
        {
            return Symbol;
        }

        #endregion

        #region ' Parse '

        //		private static readonly Dictionary<string, Piece_> ParserDic;
        private static readonly Piece_[] PieceBySymbol;

        /// <summary>
        ///     Parses a symbol into a Piece_.
        ///     Recognizes japanese hieroglyphs as well as latin symbols
        /// </summary>
        public static Piece_ Parse(char symbol)
        {
            var idx = symbol - 'A';
            if (idx < 0 || idx >= PieceBySymbol.Length)
                throw new FormatException("illegal character: " + symbol);
            var piece = PieceBySymbol[idx];
            if (piece == null)
                throw new FormatException("illegal character: " + symbol);
            return piece;
        }

        /// <summary>
        ///     Tries to parse a symbol into a PieceType.
        ///     Recognizes japanese hieroglyphs as well as latin symbols
        /// </summary>
        /// <returns>false if it couldn't parse the symbol</returns>
        public static bool TryParse(char symbol, out Piece_ result)
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

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly Piece_ WhitePawn;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly Piece_ WhiteBishop;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly Piece_ WhiteKnight;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly Piece_ WhiteRook;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly Piece_ WhiteQueen;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly Piece_ WhiteKing;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly Piece_ BlackPawn;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly Piece_ BlackBishop;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly Piece_ BlackKnight;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly Piece_ BlackRook;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly Piece_ BlackQueen;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly Piece_ BlackKing;

        /// <summary>All Types pieces may have</summary>
        public static ReadOnlyCollection<Piece_> All { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline",
            Justification = "Too complex to initialize without ctor")]
        static Piece_()
        {
            //      _______________
            // ____/ Create Pieces \____________________________________________________________
            WhitePawn = new Piece_(CompactPiece.WhitePawn, "P", '♙', "White Pawn", "Белая пешка");
            WhiteBishop = new Piece_(CompactPiece.WhiteBishop, "B", '♗', "White Bishop", "Белый слон");
            WhiteKnight = new Piece_(CompactPiece.WhiteKnight, "N", '♘', "White Knight", "Белый конь");
            WhiteRook = new Piece_(CompactPiece.WhiteRook, "R", '♖', "White Rook", "Белая ладья");
            WhiteQueen = new Piece_(CompactPiece.WhiteQueen, "Q", '♕', "White Queen", "Белый ферзь");
            WhiteKing = new Piece_(CompactPiece.WhiteKing, "K", '♔', "White King", "Белый король");

            BlackPawn = new Piece_(CompactPiece.BlackPawn, "p", '♟', "Black Pawn", "Черная пешка");
            BlackBishop = new Piece_(CompactPiece.BlackBishop, "b", '♝', "Black Bishop", "Черный слон");
            BlackKnight = new Piece_(CompactPiece.BlackKnight, "n", '♞', "Black Knight", "Черный конь");
            BlackRook = new Piece_(CompactPiece.BlackRook, "r", '♜', "Black Rook", "Черная ладья");
            BlackQueen = new Piece_(CompactPiece.BlackQueen, "q", '♛', "Black Queen", "Черный ферзь");
            BlackKing = new Piece_(CompactPiece.BlackKing, "k", '♚', "Black King", "Черный король");

            //      _____________
            // ____/ Collections \______________________________________________________________
            All = new ReadOnlyCollection<Piece_>(new[]
            {
                WhitePawn, WhiteBishop, WhiteKnight, WhiteRook,
                WhiteQueen, WhiteKing, BlackPawn, BlackBishop,
                BlackKnight, BlackRook, BlackQueen, BlackKing
            });


            //      ______________
            // ____/ Dictionaries \_____________________________________________________________
            PieceBySymbol = new Piece_['z' - 'A'];
            foreach (var piece in All)
                PieceBySymbol[piece.Symbol[0] - 'A'] = piece;

            CompactPieceBySymbol = new CompactPiece['z' - 'A'];
            foreach (var piece in All)
                CompactPieceBySymbol[piece.Symbol[0] - 'A'] = piece.CompactValue;

            TypeColorMap = new Piece_[(int)BlackKing.CompactValue + 1];
            foreach (var piece in All)
                TypeColorMap[(int)piece.CompactValue] = piece;

            //      ________________
            // ____/ MoveDirections \___________________________________________________________
            WhitePawn.MoveDirections = Join(Move(0, 1), Take(1, 1), Take(-1, 1), Special(0, 2));
            WhiteBishop.MoveDirections = Join(Slide(1, 1), Slide(1, -1), Slide(-1, -1), Slide(-1, 1));
            WhiteKnight.MoveDirections = Join(Take(1, 2), Take(-1, 2), Take(1, -2), Take(-1, -2), Take(2, 1),
                Take(-2, 1), Take(2, -1), Take(-2, -1));
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

        public static CompactPiece Pack(Piece_ value)
        {
            return value == null ? CompactPiece.EmptyCell : value.CompactValue;
        }

        public static Piece_ Unpack(CompactPiece value)
        {
            return TypeColorMap[(int)value];
        }

        public static CompactPiece Pack(PieceType pieceType, PieceColor pieceColor)
        {
            return (CompactPiece)((MoveAnnotations)pieceType | (MoveAnnotations)pieceColor);
        }

        public static PieceColor UnpackColor(CompactPiece piece)
        {
            return (PieceColor)((MoveAnnotations)piece & (MoveAnnotations)PieceColor.Black);
        }

        public static PieceType UnpackType(CompactPiece piece)
        {
            return (PieceType)((MoveAnnotations)piece & ~(MoveAnnotations)PieceColor.Black);
        }

        #endregion
    }

    public static class Piece

    {
        public static CompactPiece Pack(PieceType pieceType, PieceColor pieceColor)
        {
            return (CompactPiece)((MoveAnnotations)pieceType | (MoveAnnotations)pieceColor);
        }

        public static PieceColor UnpackColor(CompactPiece piece)
        {
            return (PieceColor)((MoveAnnotations)piece & (MoveAnnotations)PieceColor.Black);
        }

        public static PieceType UnpackType(CompactPiece piece)
        {
            return (PieceType)((MoveAnnotations)piece & ~(MoveAnnotations)PieceColor.Black);
        }
        public static PieceType PieceType(this CompactPiece piece)
        {
            return (PieceType)((MoveAnnotations)piece & ~(MoveAnnotations)PieceColor.Black);
        }

        public static char GetSymbol(this CompactPiece piece)
        {
            switch (piece)
            {
                case CompactPiece.WhitePawn: return 'P';
                case CompactPiece.WhiteKnight: return 'N';
                case CompactPiece.WhiteBishop: return 'B';
                case CompactPiece.WhiteRook: return 'R';
                case CompactPiece.WhiteQueen: return 'Q';
                case CompactPiece.WhiteKing: return 'K';
                case CompactPiece.BlackPawn: return 'p';
                case CompactPiece.BlackKnight: return 'n';
                case CompactPiece.BlackBishop: return 'b';
                case CompactPiece.BlackRook: return 'r';
                case CompactPiece.BlackQueen: return 'q';
                case CompactPiece.BlackKing: return 'k';
                case CompactPiece.EmptyCell: return ' ';
                default: throw new Exception("Unexpected");
            }
        }

        public static char GetTypeSymbol(this CompactPiece piece)
        {
            switch (piece)
            {
                case CompactPiece.WhitePawn:   return 'P';
                case CompactPiece.WhiteKnight: return 'N';
                case CompactPiece.WhiteBishop: return 'B';
                case CompactPiece.WhiteRook:   return 'R';
                case CompactPiece.WhiteQueen:  return 'Q';
                case CompactPiece.WhiteKing:   return 'K';
                case CompactPiece.BlackPawn:   return 'P';
                case CompactPiece.BlackKnight: return 'N';
                case CompactPiece.BlackBishop: return 'B';
                case CompactPiece.BlackRook:   return 'R';
                case CompactPiece.BlackQueen:  return 'Q';
                case CompactPiece.BlackKing:   return 'K';
                default: throw new Exception("Unexpected");
            }
        }

        public static bool TryParse(char ch, out CompactPiece piece)
        {
            switch (ch)
            {
                case 'P': piece = CompactPiece.WhitePawn;   break;
                case 'N': piece = CompactPiece.WhiteKnight; break;
                case 'B': piece = CompactPiece.WhiteBishop; break;
                case 'R': piece = CompactPiece.WhiteRook;   break;
                case 'Q': piece = CompactPiece.WhiteQueen;  break;
                case 'K': piece = CompactPiece.WhiteKing;   break;
                case 'p': piece = CompactPiece.BlackPawn;   break;
                case 'n': piece = CompactPiece.BlackKnight; break;
                case 'b': piece = CompactPiece.BlackBishop; break;
                case 'r': piece = CompactPiece.BlackRook;   break;
                case 'q': piece = CompactPiece.BlackQueen;  break;
                case 'k': piece = CompactPiece.BlackKing;   break;
                default:
                    piece = CompactPiece.EmptyCell;
                    return false;
            }
            return true;
        }
        public static CompactPiece Parse(char ch)
        {
            switch (ch)
            {
                case 'P': return CompactPiece.WhitePawn; ;
                case 'N': return CompactPiece.WhiteKnight;
                case 'B': return CompactPiece.WhiteBishop;
                case 'R': return CompactPiece.WhiteRook;
                case 'Q': return CompactPiece.WhiteQueen;
                case 'K': return CompactPiece.WhiteKing;
                case 'p': return CompactPiece.BlackPawn;
                case 'n': return CompactPiece.BlackKnight;
                case 'b': return CompactPiece.BlackBishop;
                case 'r': return CompactPiece.BlackRook;
                case 'q': return CompactPiece.BlackQueen;
                case 'k': return CompactPiece.BlackKing;
                default: throw new Exception("Unexpected");
            }
        }
    }

    public class ImmutableAttribute : Attribute
    {
    }
}