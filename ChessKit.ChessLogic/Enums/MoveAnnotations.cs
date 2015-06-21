using System;

namespace ChessKit.ChessLogic.Enums
{
    [Flags]
    public enum MoveAnnotations
    {
        None = 0,

        Black = 0x1,

        Pawn   = 0x02,
        Bishop = 0x04,
        Knight = 0x08,
        Rook   = 0x10,
        Queen  = 0x20,
        King   = 0x40,

        Capture = 0x80,

        Castling               = 0x0100,
        BlackKingsideCastling  = 0x0200,
        BlackQueensideCastling = 0x0400,
        WhiteKingsideCastling  = 0x0800,
        WhiteQueensideCastling = 0x1000,

        EnPassant      = 0x2000,
        Promotion      = 0x4000,
        PawnDoublePush = 0x8000,

        TestedForConsequences = 0x10000,
        Check                 = 0x20000,
        Mate                  = 0x40000,

        MoveToCheck     = 0x80000,  // "move to the square that is under attack", "if you made that move king would be in check"
        EmptyCell       = 0x100000,
        WrongSideToMove = 0x200000, //"it's white's turn
        HasNoCastling   = 0x400000,
        ToOccupiedCell  = 0x800000,
        HasNoEnPassant  = 0x1000000,

        DoesNotJump           = 0x2000000, // NOTE: Could be "blocked"? Chess titans: " the path is blocked"
        OnlyCapturesThisWay   = 0x4000000,
        DoesNotCaptureThisWay = 0x8000000,
        CastleThroughCheck    = 0x10000000,
        DoesNotMoveThisWay    = 0x20000000,
        CastleFromCheck       = 0x40000000,

        AllErrors = MoveToCheck
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
                  | CastleFromCheck,

        AllPieces = Pawn
                  | Bishop
                  | Knight
                  | Rook
                  | Queen
                  | King
    }
}
