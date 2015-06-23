using System;
using ChessKit.ChessLogic.Algorithms;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    /// The part of the chess position that can be compared
    /// to determine threefold repetitions
    public sealed class PositionCore : IEquatable<PositionCore>
    {
        /// An array of the 64 squares the chess board consists of
        /// Note that index 0 corresponds to a8, and NOT a1!
        /// Indexes read left to right, top to bottom!
        public byte[] Squares { get; }

        /// The color of the side that makes the next move
        public Color ActiveColor { get; }

        /// Castlings available to the both sides
        /// (one that changes when they move their kings/rooks)
        public Castlings CastlingAvailability { get; }

        /// The index is the file the opponent last
        /// made pawn double move -or- ...
        public int? EnPassant { get; }

        public int WhiteKing { get; }
        public int BlackKing { get; }

        public PositionCore(byte[] squares, Color activeColor, Castlings castlingAvailability, int? enPassant, int whiteKing, int blackKing)
        {
            Squares = squares;
            ActiveColor = activeColor;
            CastlingAvailability = castlingAvailability;
            EnPassant = enPassant;
            WhiteKing = whiteKing;
            BlackKing = blackKing;
        }

        #region ' Equality '

        public bool Equals(PositionCore other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            for (var i = 0; i < 64; i++)
            {
                var sq = i + (i & ~7);
                if (Squares[sq] != other.Squares[sq])
                    return false;
            }
            return ActiveColor == other.ActiveColor &&
                   CastlingAvailability == other.CastlingAvailability && 
                   EnPassant == other.EnPassant;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is PositionCore && Equals((PositionCore)obj);
        }

        public override int GetHashCode()
        {
            var hash = this.GetHash();
            return unchecked ((int) hash & (int) (hash >> 32));
        }

        public static bool operator ==(PositionCore left, PositionCore right) => Equals(left, right);
        public static bool operator !=(PositionCore left, PositionCore right) => !Equals(left, right);

        #endregion        
    }
}