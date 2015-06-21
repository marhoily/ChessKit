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
    public sealed class MetaPiece
    {
        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "It is really immutable")]
        public static readonly IComparer<MetaPiece> TypeComparer =
            new PieceTypeComparer();

        #region ' Piece '

        private static readonly MetaPiece[] TypeColorMap;

        #endregion

        [Immutable]
        private class PieceTypeComparer : IComparer<MetaPiece>
        {
            public int Compare(MetaPiece x, MetaPiece y)
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

        internal MetaPiece(CompactPiece compactValue, string symbol,
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

        private static readonly MetaPiece[] PieceBySymbol;

        #endregion

        #region ' Public Constants '

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly MetaPiece WhitePawn;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly MetaPiece WhiteBishop;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly MetaPiece WhiteKnight;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly MetaPiece WhiteRook;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly MetaPiece WhiteQueen;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly MetaPiece WhiteKing;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly MetaPiece BlackPawn;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly MetaPiece BlackBishop;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly MetaPiece BlackKnight;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly MetaPiece BlackRook;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly MetaPiece BlackQueen;

        [SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
            Justification = "In fact immutable")]
        public static readonly MetaPiece BlackKing;

        /// <summary>All Types pieces may have</summary>
        public static ReadOnlyCollection<MetaPiece> All { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline",
            Justification = "Too complex to initialize without ctor")]
        static MetaPiece()
        {
            //      _______________
            // ____/ Create Pieces \____________________________________________________________
            WhitePawn = new MetaPiece(CompactPiece.WhitePawn, "P", '♙', "White Pawn", "Белая пешка");
            WhiteBishop = new MetaPiece(CompactPiece.WhiteBishop, "B", '♗', "White Bishop", "Белый слон");
            WhiteKnight = new MetaPiece(CompactPiece.WhiteKnight, "N", '♘', "White Knight", "Белый конь");
            WhiteRook = new MetaPiece(CompactPiece.WhiteRook, "R", '♖', "White Rook", "Белая ладья");
            WhiteQueen = new MetaPiece(CompactPiece.WhiteQueen, "Q", '♕', "White Queen", "Белый ферзь");
            WhiteKing = new MetaPiece(CompactPiece.WhiteKing, "K", '♔', "White King", "Белый король");

            BlackPawn = new MetaPiece(CompactPiece.BlackPawn, "p", '♟', "Black Pawn", "Черная пешка");
            BlackBishop = new MetaPiece(CompactPiece.BlackBishop, "b", '♝', "Black Bishop", "Черный слон");
            BlackKnight = new MetaPiece(CompactPiece.BlackKnight, "n", '♞', "Black Knight", "Черный конь");
            BlackRook = new MetaPiece(CompactPiece.BlackRook, "r", '♜', "Black Rook", "Черная ладья");
            BlackQueen = new MetaPiece(CompactPiece.BlackQueen, "q", '♛', "Black Queen", "Черный ферзь");
            BlackKing = new MetaPiece(CompactPiece.BlackKing, "k", '♚', "Black King", "Черный король");

            //      _____________
            // ____/ Collections \______________________________________________________________
            All = new ReadOnlyCollection<MetaPiece>(new[]
            {
                WhitePawn, WhiteBishop, WhiteKnight, WhiteRook,
                WhiteQueen, WhiteKing, BlackPawn, BlackBishop,
                BlackKnight, BlackRook, BlackQueen, BlackKing
            });


            //      ______________
            // ____/ Dictionaries \_____________________________________________________________
            PieceBySymbol = new MetaPiece['z' - 'A'];
            foreach (var piece in All)
                PieceBySymbol[piece.Symbol[0] - 'A'] = piece;

            CompactPieceBySymbol = new CompactPiece['z' - 'A'];
            foreach (var piece in All)
                CompactPieceBySymbol[piece.Symbol[0] - 'A'] = piece.CompactValue;

            TypeColorMap = new MetaPiece[(int)BlackKing.CompactValue + 1];
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

        #endregion
    }
}