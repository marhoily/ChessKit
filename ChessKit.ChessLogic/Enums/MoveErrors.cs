using System;

namespace ChessKit.ChessLogic.Enums
{
    [Flags]
    public enum MoveErrors
    {
        None = 0,
        /// Your king can be captured the next move
        MoveToCheck = 0x00008000,
        /// Your move originates in the empty cell
        EmptyCell = 0x00010000,
        /// It's not your turn to move (not your piece to move)
        WrongSideToMove = 0x00020000,
        /// You can't castle because the King or the Rook had moved
        HasNoCastling = 0x00040000,
        /// The move destinates in the cell occupied by the piece of your own
        ToOccupiedCell = 0x00080000,
        /// You can't capture en passant because it's been a while since
        /// the opponent's pawn moved
        HasNoEnPassant = 0x00100000,
        /// Your sliding or castling move jumps over other pieces,
        /// only knights can do that
        DoesNotJump = 0x00200000,
        /// Pawn can't move the way it can capture
        OnlyCapturesThisWay = 0x00400000,
        /// Pawn can't capture the way it moves
        DoesNotCaptureThisWay = 0x00800000,
        /// You can't castle if the square, the King transits through
        /// is under attack right now
        CastleThroughCheck = 0x01000000,
        /// The piece doesn't move that way (e.g. bishop cannot move a1-a2)
        DoesNotMoveThisWay = 0x02000000,
        /// You can't casle if your king is under check right now
        CastleFromCheck = 0x04000000,

        All = MoveToCheck
              | EmptyCell
              | WrongSideToMove
              | HasNoCastling
              | ToOccupiedCell
              | HasNoEnPassant
              | DoesNotJump
              | OnlyCapturesThisWay
              | DoesNotCaptureThisWay
              | CastleThroughCheck
              | DoesNotMoveThisWay
              | CastleFromCheck
    }
}