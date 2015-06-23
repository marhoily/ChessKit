﻿using System;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic.N
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

        public PositionCore(byte[] squares, Color activeColor, Castlings castlingAvailability, int? enPassant)
        {
            Squares = squares;
            ActiveColor = activeColor;
            CastlingAvailability = castlingAvailability;
            EnPassant = enPassant;
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

    /// Represents legal move as returned by the legality check
    public sealed class LegalMove : AnalyzedMove
    {
        /// The move that was checked for the legality
        public MoveR Move { get; }

        /// The position in which the move was checked
        public Position OriginalPosition { get; }

        /// The core of the position gotten as a result of the move
        /// (use legalMove.ToPosition() method to get full position)
        public PositionCore ResultPosition { get; }

        /// The piece type that was moved
        public PieceType Piece { get; }

        /// The castling, if the move was castling, -or- None
        public Castlings Castling { get; }

        /// Warnings to the move
        public MoveWarnings Warnings { get; }

        public LegalMove(MoveR move, Position originalPosition, PositionCore resultPosition, PieceType piece,
            MoveAnnotations annotations)
            : base(annotations)
        {
            Move = move;
            OriginalPosition = originalPosition;
            ResultPosition = resultPosition;
            Piece = piece;
            Castling = Castlings.All | (Castlings) annotations;
            Warnings = MoveWarnings.All | (MoveWarnings) annotations;
        }
    }

    /// Represents a position in the game
    /// (adding Properties to the position is a bit CPU consuming)
    public sealed class Position
    {
        public int WhiteKing { get; }
        public int BlackKing { get; }
        /// Stuff that was calculated immediately with the legality check
        public PositionCore Core { get; }

        /// 50 moves rule counter
        public int HalfMoveClock { get; }

        /// Number of full moves (white, then black) counted
        public int FullMoveNumber { get; }

        /// Properties of the position like check, mate and stalemate
        public GameStates Properties { get; }

        /// The previous legal move if the position derives from
        /// some other position, -or- ...
        public LegalMove Move { get; }

        public Position(PositionCore core, int halfMoveClock, int fullMoveNumber, GameStates properties, LegalMove move, int whiteKing, int blackKing)
        {
            Core = core;
            HalfMoveClock = halfMoveClock;
            FullMoveNumber = fullMoveNumber;
            Properties = properties;
            Move = move;
            WhiteKing = whiteKing;
            BlackKing = blackKing;
        }
    }

    /// Represents an illegal move as returned by the legality check
    public sealed class IllegalMove : AnalyzedMove
    {
        /// The move that was checked for the legality
        public MoveR Move { get; }

        /// The position in which the move was checked
        public Position OriginalPosition { get; }

        /// The piece type that was moved
        public PieceType Piece { get; }

        /// The castling, if the move was castling attempt, -or- None
        public Castlings Castling { get; }

        /// Warnings to the move
        public MoveWarnings Warnings { get; }

        /// Non-empty set of the errors to the move
        public MoveErrors Errors { get; }

        public IllegalMove(MoveR move, Position originalPosition,
            PieceType piece, MoveAnnotations annotations)
            : base(annotations)
        {
            Move = move;
            OriginalPosition = originalPosition;
            Piece = piece;
            Castling = Castlings.All & (Castlings) annotations;
            Warnings = MoveWarnings.All & (MoveWarnings) annotations;
            Errors = MoveErrors.All & (MoveErrors) annotations;
        }
    }

    public abstract class AnalyzedMove
    {
        protected AnalyzedMove(MoveAnnotations annotations)
        {
            Annotations = annotations;
        }

        /// Annotations (capture, promotion attempt, etc.) to the move
        public MoveAnnotations Annotations { get; }
    }
}