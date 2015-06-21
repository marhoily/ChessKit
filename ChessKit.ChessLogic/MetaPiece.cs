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
        #region ' Piece '

        private static readonly MetaPiece[] TypeColorMap;

        #endregion

        #region ' Instance '

        public PieceType PieceType { get; private set; }

        /// <summary>Piece color</summary>
        public PieceColor Color { get; private set; }

        /// <summary>Gets the latin piece symbol</summary>
        public string Symbol { get; private set; }

        /// <summary>Gets all directions of move piece can do</summary>
        public ReadOnlyCollection<MoveDirection> MoveDirections { get; private set; }

        public CompactPiece CompactValue { get; private set; }

        internal MetaPiece(CompactPiece compactValue, string symbol,
            char chessChar, string english, string russian)
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

        #endregion

        #region ' Parse '

        private static readonly MetaPiece[] PieceBySymbol;

        #endregion

        #region ' Public Constants '

        /// <summary>All Types pieces may have</summary>
        public static ReadOnlyCollection<MetaPiece> All { get; private set; }

        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline",
            Justification = "Too complex to initialize without ctor")]
        static MetaPiece()
        {
            //      _______________
            // ____/ Create Pieces \____________________________________________________________
            var whitePawn = new MetaPiece(CompactPiece.WhitePawn, "P", '♙', "White Pawn", "Белая пешка");
            var whiteBishop = new MetaPiece(CompactPiece.WhiteBishop, "B", '♗', "White Bishop", "Белый слон");
            var whiteKnight = new MetaPiece(CompactPiece.WhiteKnight, "N", '♘', "White Knight", "Белый конь");
            var whiteRook = new MetaPiece(CompactPiece.WhiteRook, "R", '♖', "White Rook", "Белая ладья");
            var whiteQueen = new MetaPiece(CompactPiece.WhiteQueen, "Q", '♕', "White Queen", "Белый ферзь");
            var whiteKing = new MetaPiece(CompactPiece.WhiteKing, "K", '♔', "White King", "Белый король");

            var blackPawn = new MetaPiece(CompactPiece.BlackPawn, "p", '♟', "Black Pawn", "Черная пешка");
            var blackBishop = new MetaPiece(CompactPiece.BlackBishop, "b", '♝', "Black Bishop", "Черный слон");
            var blackKnight = new MetaPiece(CompactPiece.BlackKnight, "n", '♞', "Black Knight", "Черный конь");
            var blackRook = new MetaPiece(CompactPiece.BlackRook, "r", '♜', "Black Rook", "Черная ладья");
            var blackQueen = new MetaPiece(CompactPiece.BlackQueen, "q", '♛', "Black Queen", "Черный ферзь");
            var blackKing = new MetaPiece(CompactPiece.BlackKing, "k", '♚', "Black King", "Черный король");

            //      _____________
            // ____/ Collections \______________________________________________________________
            All = new ReadOnlyCollection<MetaPiece>(new[]
            {
                whitePawn, whiteBishop, whiteKnight, whiteRook,
                whiteQueen, whiteKing, blackPawn, blackBishop,
                blackKnight, blackRook, blackQueen, blackKing
            });


            //      ______________
            // ____/ Dictionaries \_____________________________________________________________
            PieceBySymbol = new MetaPiece['z' - 'A'];
            foreach (var piece in All)
                PieceBySymbol[piece.Symbol[0] - 'A'] = piece;

            CompactPieceBySymbol = new CompactPiece['z' - 'A'];
            foreach (var piece in All)
                CompactPieceBySymbol[piece.Symbol[0] - 'A'] = piece.CompactValue;

            TypeColorMap = new MetaPiece[(int)blackKing.CompactValue + 1];
            foreach (var piece in All)
                TypeColorMap[(int)piece.CompactValue] = piece;

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