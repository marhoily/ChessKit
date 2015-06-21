using System;

namespace ChessKit.ChessLogic.Enums
{
    /// Non-critical error that can be noticed about the move during
    /// the legality check
    [Flags]
    public enum MoveWarnings : uint
    {
        None = 0,
        /// <summary>It was annotated as Promotion but no PromoteTo
        /// piece type was assigned. Queens was used by default</summary>
      //  MissingPromotionHint = MoveAnnotations.MissingPromotionHint,
        /// <summary>It was not a promotion move, but PromoteTo
        /// contained non-empty piece type, that was ignored</summary>
        //PromotionHintIsNotNeeded = MoveAnnotations.PromotionHintIsNotNeeded
    }

    [Flags]
    public enum MoveAnnotations 
    {
        None                     = 0x00000000,
        Black                    = 0x00000001,
        Pawn                     = PieceType.Pawn  ,
        Bishop                   = PieceType.Bishop,
        Knight                   = PieceType.Knight,
        Rook                     = PieceType.Rook  ,
        Queen                    = PieceType.Queen ,
        King                     = PieceType.King,
        BlackKingsideCastling    = 0x00000080,
        BlackQueensideCastling   = 0x00000100,
        WhiteKingsideCastling    = 0x00000200,
        WhiteQueensideCastling   = 0x00000400,
        Capture                  = 0x00000800,
        Castling                 = 0x00001000,
        EnPassant                = 0x00002000,
        Promotion                = 0x00004000,
        PawnDoublePush           = 0x00008000,
        MoveToCheck              = 0x00010000,
        EmptyCell                = 0x00020000,
        WrongSideToMove          = 0x00040000,
        HasNoCastling            = 0x00080000,  // "move to the square that is under attack", "if you made that move king would be in check"
        ToOccupiedCell           = 0x00100000,
        HasNoEnPassant           = 0x00200000, //"it's white's turn
        DoesNotJump              = 0x00400000,
        OnlyCapturesThisWay      = 0x00800000,
        DoesNotCaptureThisWay    = 0x01000000,
        CastleThroughCheck       = 0x02000000, // NOTE: Could be "blocked"? Chess titans: " the path is blocked"
        DoesNotMoveThisWay       = 0x04000000,
        CastleFromCheck          = 0x08000000,
        //  = 0x10000000,
        //  = 0x20000000,
        //  = 0x40000000,
        //TestedForConsequences
        //MissingPromotionHint     = 0x80000000,
        //PromotionHintIsNotNeeded = 0x100000000,

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
