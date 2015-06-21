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
        public PieceType PieceType { get; private set; }

        /// <summary>Piece color</summary>
        public PieceColor Color { get; private set; }

        /// <summary>Gets the latin piece symbol</summary>
        public string Symbol { get; private set; }

        /// <summary>Gets all directions of move piece can do</summary>
        public ReadOnlyCollection<MoveDirection> MoveDirections { get; private set; }

        public CompactPiece CompactValue { get; private set; }

        internal MetaPiece(CompactPiece compactValue, string symbol)
        {
            CompactValue = compactValue;
            PieceType = (PieceType)((MoveAnnotations)compactValue & MoveAnnotations.AllPieces);
            Symbol = symbol;
            Color = (PieceColor)((MoveAnnotations)compactValue & MoveAnnotations.Black);
        }

        public override string ToString()
        {
            return Symbol;
        }

        /// <summary>All Types pieces may have</summary>
        public static ReadOnlyCollection<MetaPiece> All { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline",
            Justification = "Too complex to initialize without ctor")]
        static MetaPiece()
        {
            //      _______________
            // ____/ Create Pieces \____________________________________________________________
            var whitePawn = new MetaPiece(CompactPiece.WhitePawn, "P");
            var whiteBishop = new MetaPiece(CompactPiece.WhiteBishop, "B");
            var whiteKnight = new MetaPiece(CompactPiece.WhiteKnight, "N");
            var whiteRook = new MetaPiece(CompactPiece.WhiteRook, "R");
            var whiteQueen = new MetaPiece(CompactPiece.WhiteQueen, "Q");
            var whiteKing = new MetaPiece(CompactPiece.WhiteKing, "K");

            var blackPawn = new MetaPiece(CompactPiece.BlackPawn, "p");
            var blackBishop = new MetaPiece(CompactPiece.BlackBishop, "b");
            var blackKnight = new MetaPiece(CompactPiece.BlackKnight, "n");
            var blackRook = new MetaPiece(CompactPiece.BlackRook, "r");
            var blackQueen = new MetaPiece(CompactPiece.BlackQueen, "q");
            var blackKing = new MetaPiece(CompactPiece.BlackKing, "k");

            //      _____________
            // ____/ Collections \______________________________________________________________
            All = new ReadOnlyCollection<MetaPiece>(new[]
            {
                whitePawn, whiteBishop, whiteKnight, whiteRook,
                whiteQueen, whiteKing, blackPawn, blackBishop,
                blackKnight, blackRook, blackQueen, blackKing
            });


            //      ________________
            // ____/ MoveDirections \___________________________________________________________
            whitePawn.MoveDirections = Join(Move(0, 1), Take(1, 1), Take(-1, 1), Special(0, 2));
            whiteBishop.MoveDirections = Join(Slide(1, 1), Slide(1, -1), Slide(-1, -1), Slide(-1, 1));
            whiteKnight.MoveDirections = Join(Take(1, 2), Take(-1, 2), Take(1, -2), Take(-1, -2), Take(2, 1),
                Take(-2, 1), Take(2, -1), Take(-2, -1));
            whiteRook.MoveDirections = Join(Slide(0, 1), Slide(1, 0), Slide(0, -1), Slide(-1, 0));
            whiteQueen.MoveDirections = Join(whiteRook.MoveDirections, whiteBishop.MoveDirections);
            whiteKing.MoveDirections = Join(Take(0, 1), Take(1, 1), Take(1, 0), Take(1, -1),
                Take(0, -1), Take(-1, -1), Take(-1, 0), Take(-1, 1),
                Special(2, 0), Special(-2, 0));

            blackPawn.MoveDirections = Join(Move(0, -1), Take(1, -1), Take(-1, -1), Special(0, -2));
            blackBishop.MoveDirections = whiteBishop.MoveDirections;
            blackKnight.MoveDirections = whiteKnight.MoveDirections;
            blackRook.MoveDirections = whiteRook.MoveDirections;
            blackQueen.MoveDirections = whiteQueen.MoveDirections;
            blackKing.MoveDirections = whiteKing.MoveDirections;
        }

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
    }
}