using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;

namespace ChessKit.ChessLogic
{
    /// <summary>Represents piece Type</summary>
    [Immutable, PublicAPI]
    public sealed class MetaPiece
    {
        /// <summary>Piece type</summary>
        public PieceType PieceType => _compactValue.PieceType();

        /// <summary>Piece color</summary>
        public PieceColor Color => _compactValue.Color();

        /// <summary>Gets all directions of move piece can do</summary>
        public ReadOnlyCollection<MoveDirection> MoveDirections { get; private set; }

        private CompactPiece _compactValue;

        internal MetaPiece(CompactPiece compactValue)
        {
            _compactValue = compactValue;
        }

        /// <summary>All Types pieces may have</summary>
        public static ReadOnlyCollection<MetaPiece> All { get; private set; }

        static MetaPiece()
        {
            //      _______________
            // ____/ Create Pieces \____________________________________________________________
            var whitePawn = new MetaPiece(CompactPiece.WhitePawn);
            var whiteBishop = new MetaPiece(CompactPiece.WhiteBishop);
            var whiteKnight = new MetaPiece(CompactPiece.WhiteKnight);
            var whiteRook = new MetaPiece(CompactPiece.WhiteRook);
            var whiteQueen = new MetaPiece(CompactPiece.WhiteQueen);
            var whiteKing = new MetaPiece(CompactPiece.WhiteKing);

            var blackPawn = new MetaPiece(CompactPiece.BlackPawn);
            var blackBishop = new MetaPiece(CompactPiece.BlackBishop);
            var blackKnight = new MetaPiece(CompactPiece.BlackKnight);
            var blackRook = new MetaPiece(CompactPiece.BlackRook);
            var blackQueen = new MetaPiece(CompactPiece.BlackQueen);
            var blackKing = new MetaPiece(CompactPiece.BlackKing);
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

        private static T Id<T>(T v) => v;
        private static ReadOnlyCollection<T> Join<T>(params IEnumerable<T>[] arr)
            => new ReadOnlyCollection<T>(arr.SelectMany(Id).ToList());

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