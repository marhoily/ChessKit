using System;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    public sealed class MoveR
    {
        public int From { get; }
        public int To { get; }
        public PieceType ProposedPromotion { get; }

        public MoveR(int @from, int to, PieceType promoteTo = PieceType.None)
        {
            From = @from;
            To = to;
            ProposedPromotion = promoteTo;
        }
        public override string ToString() 
            => $"{From.ToCoordinateString()}-{To.ToCoordinateString()}";

        public static MoveR Parse(string canString)
        {
            if (string.IsNullOrEmpty(canString))
                throw new ArgumentException("should not be null or empty", "canString");
            if (canString.Length == 5)
                return new MoveR(
                    canString.Substring(0, 2).ParseCoordinate(),
                    canString.Substring(3, 2).ParseCoordinate());
            if (canString.Length != 7) throw new ArgumentOutOfRangeException("canString");
            Piece piece;
            if (!canString[6].TryParsePiece(out piece))
                throw new ArgumentOutOfRangeException(nameof(canString));
            return new MoveR(
                canString.Substring(0, 2).ParseCoordinate(),
                canString.Substring(3, 2).ParseCoordinate(),
                piece.PieceType());
        }

        public static implicit operator MoveR(Move move) 
            => new MoveR(move.From, move.To);
    }
}