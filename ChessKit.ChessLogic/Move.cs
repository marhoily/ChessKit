using System;
using System.Globalization;

namespace ChessKit.ChessLogic
{
  // TODO: Should Move be struct (con: references board)?
  // TODO: Crazy ctors! Public setters. Not immutable!
  public sealed class Move : IEquatable<Move>
  {
    public Position From { get; private set; }
    public Position To { get; private set; }
    public MoveHints Hints { get; set; }
    public PieceType ProposedPromotion { get; set; }
    public MoveType Kind { get; private set; }

    public bool IsValid { get { return (Hints & MoveHints.AllErrors) == 0; } }
    public bool IsKingsideCastling
    {
      get { return (Hints & (MoveHints.BlackKingsideCastling | MoveHints.WhiteKingsideCastling)) != 0; }
    }
    public bool IsQueensideCastling
    {
      get { return (Hints & (MoveHints.BlackQueensideCastling | MoveHints.WhiteQueensideCastling)) != 0; }
    }

    public bool IsProposedPromotion { get { return (Hints & (MoveHints.Promotion)) != 0; } }

    public Move(MoveType type)
    {
      Kind = type;
      ProposedPromotion = PieceType.Queen;
    }
    public Move(Position from, Position to)
      // ReSharper disable IntroduceOptionalParameters.Global
      : this(from, to, PieceType.Queen)
    // ReSharper restore IntroduceOptionalParameters.Global
    {
    }
    public Move(Position from, Position to, PieceType promotion)
    {
      // TODO: if (from == to) throw new ArgumentException("to equals from!", "to");
      From = from;
      To = to;
      ProposedPromotion = promotion;
    }

    public Move(Position from, Position to, MoveHints hints)
    {
      To = to;
      From = from;
      Hints = hints;
      ProposedPromotion = PieceType.Queen;
    }

    #region ' Equality '

    public bool Equals(Move other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return From.Equals(other.From)
        && To.Equals(other.To)
        && ProposedPromotion == other.ProposedPromotion
        && Kind == other.Kind;
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != GetType()) return false;
      return Equals((Move)obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        int hashCode = From.GetHashCode();
        hashCode = (hashCode * 397) ^ To.GetHashCode();
        hashCode = (hashCode * 397) ^ (int)ProposedPromotion;
        hashCode = (hashCode * 397) ^ (int)Kind;
        return hashCode;
      }
    }

    public static bool operator ==(Move left, Move right)
    {
      return Equals(left, right);
    }

    public static bool operator !=(Move left, Move right)
    {
      return !Equals(left, right);
    }

    #endregion

    public override string ToString()
    {
      return string.Format(
        CultureInfo.InvariantCulture, "{0}-{1}", From, To);
    }
    public static Move Parse(string canString)
    {
      if (string.IsNullOrEmpty(canString)) throw new ArgumentException("should not be null or empty", "canString");
      if (canString.Length == 5)
        return new Move(
          Position.Parse(canString.Substring(0, 2)),
          Position.Parse(canString.Substring(3, 2)));
      if (canString.Length != 7) throw new ArgumentOutOfRangeException("canString");
      Piece piece;
      if (!Piece.TryParse(canString[6], out piece))
        throw new ArgumentOutOfRangeException("canString");
      return new Move(
          Position.Parse(canString.Substring(0, 2)),
          Position.Parse(canString.Substring(3, 2)),
          piece.PieceType);
    }
  }
}