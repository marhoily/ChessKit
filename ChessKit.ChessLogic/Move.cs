using System;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    public sealed class Move
    {
        public int FromCell { get; }
        public int ToCell { get; }
        public PieceType ProposedPromotion { get; }

        public Move(int fromCell, int toCell, PieceType promoteTo = PieceType.None)
        {
            FromCell = fromCell;
            ToCell = toCell;
            ProposedPromotion = promoteTo;
        }
        public override string ToString() 
            => $"{FromCell.ToCoordinateString()}-{ToCell.ToCoordinateString()}";

        public static Move Parse(string canString)
        {
            if (string.IsNullOrEmpty(canString))
                throw new ArgumentException(
                    "should not be null or empty", 
                    nameof(canString));

            if (canString.Length == 5)
                return new Move(
                    canString.Substring(0, 2).ParseCoordinate(),
                    canString.Substring(3, 2).ParseCoordinate());
            if (canString.Length != 7)
                throw new ArgumentOutOfRangeException(nameof(canString));
            Piece piece;
            if (!canString[6].TryParsePiece(out piece))
                throw new ArgumentOutOfRangeException(nameof(canString));
            return new Move(
                canString.Substring(0, 2).ParseCoordinate(),
                canString.Substring(3, 2).ParseCoordinate(),
                piece.PieceType());
        }

        public static implicit operator Move(GeneratedMove move) 
            => new Move(move.From, move.To);
    }
}