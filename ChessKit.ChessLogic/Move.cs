using System;
using ChessKit.ChessLogic.Enums;

// ReSharper disable IntroduceOptionalParameters.Global

namespace ChessKit.ChessLogic
{
    // TODO: Should Move be struct (con: references board)?
    // TODO: Crazy ctors! Public setters. Not immutable!
    public sealed class Move : IEquatable<Move>
    {
        public Move(int from, int to)
            : this(from, to, PieceType.Queen)
        {
        }

        public Move(int from, int to, PieceType promotion)
        {
            // TODO: if (from == to) throw new ArgumentException("to equals from!", "to");
            From = from;
            To = to;
            ProposedPromotion = promotion;
        }

        public Move(int from, int to, MoveAnnotations annotations)
        {
            To = to;
            From = from;
            Annotations = annotations;
            ProposedPromotion = PieceType.Queen;
        }

        public int From { get; }
        public int To { get; }
        public MoveAnnotations Annotations { get; set; }
        public PieceType ProposedPromotion { get; set; }
        public bool IsValid => (Annotations & MoveAnnotations.AllErrors) == 0;

        public bool IsKingsideCastling
            => (Annotations & (MoveAnnotations.BK | MoveAnnotations.WK)) != 0;

        public bool IsQueensideCastling
            => (Annotations & (MoveAnnotations.BQ | MoveAnnotations.WQ)) != 0;

        public bool IsProposedPromotion => (Annotations & (MoveAnnotations.Promotion)) != 0;

        public override string ToString() => $"{From}-{To}";

        public static Move Parse(string canString)
        {
            if (string.IsNullOrEmpty(canString))
                throw new ArgumentException("should not be null or empty", "canString");
            if (canString.Length == 5)
                return new Move(
                    Coordinate.Parse(canString.Substring(0, 2)),
                    Coordinate.Parse(canString.Substring(3, 2)));
            if (canString.Length != 7) throw new ArgumentOutOfRangeException("canString");
            Piece piece;
            if (!canString[6].TryParse(out piece))
                throw new ArgumentOutOfRangeException(nameof(canString));
            return new Move(
                Coordinate.Parse(canString.Substring(0, 2)),
                Coordinate.Parse(canString.Substring(3, 2)),
                piece.PieceType());
        }

        #region ' Equality '

        public bool Equals(Move other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return From.Equals(other.From)
                   && To.Equals(other.To)
                   && ProposedPromotion == other.ProposedPromotion;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Move) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = From.GetHashCode();
                hashCode = (hashCode*397) ^ To.GetHashCode();
                hashCode = (hashCode*397) ^ (int) ProposedPromotion;
                return hashCode;
            }
        }

        public static bool operator ==(Move left, Move right) => Equals(left, right);

        public static bool operator !=(Move left, Move right) => !Equals(left, right);

        #endregion
    }
}