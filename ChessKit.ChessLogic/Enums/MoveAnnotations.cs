using System;

namespace ChessKit.ChessLogic.Enums
{
    /// Non-critical error that can be noticed about the move during
    /// the legality check
    [Flags]
    public enum MoveWarnings
    {
        None = 0,
        /// <summary>It was annotated as Promotion but no PromoteTo
        /// piece type was assigned. Queens was used by default</summary>
        MissingPromotionHint = 0x08000000,
        /// <summary>It was not a promotion move, but PromoteTo
        /// contained non-empty piece type, that was ignored</summary>
        PromotionHintIsNotNeeded = 0x10000000,

        All = MissingPromotionHint
            | PromotionHintIsNotNeeded
    }

    [Flags]
    public enum Castlings
    {
        None = 0,
        BK = 0x080,
        BQ = 0x100,
        WK = 0x200,
        WQ = 0x400,
        All = BK | BQ | WK | WQ
    }

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

    [Flags]
    public enum MoveAnnotations
    {
        None = 0x00000000,

        Black = 0x00000001,

        Pawn = PieceType.Pawn,
        Bishop = PieceType.Bishop,
        Knight = PieceType.Knight,
        Rook = PieceType.Rook,
        Queen = PieceType.Queen,
        King = PieceType.King,
        AllPieces = PieceType.All,

        BK = Castlings.BK,
        BQ = Castlings.BQ,
        WK = Castlings.WK,
        WQ = Castlings.WQ,
        AllCastlings = Castlings.All,

        Promotion = MoveInfo.Promotion,
        Capture = MoveInfo.Capture,
        EnPassant = MoveInfo.EnPassant,
        PawnDoublePush = MoveInfo.DoublePush,
        AllInfos = MoveInfo.All,

        MoveToCheck = MoveErrors.MoveToCheck,
        EmptyCell = MoveErrors.EmptyCell,
        WrongSideToMove = MoveErrors.WrongSideToMove,
        HasNoCastling = MoveErrors.HasNoCastling,
        ToOccupiedCell = MoveErrors.ToOccupiedCell,
        HasNoEnPassant = MoveErrors.HasNoEnPassant,
        DoesNotJump = MoveErrors.DoesNotJump,
        OnlyCapturesThisWay = MoveErrors.OnlyCapturesThisWay,
        DoesNotCaptureThisWay = MoveErrors.DoesNotCaptureThisWay,
        CastleThroughCheck = MoveErrors.CastleThroughCheck,
        DoesNotMoveThisWay = MoveErrors.DoesNotMoveThisWay,
        CastleFromCheck = MoveErrors.CastleFromCheck,
        AllErrors = MoveErrors.All,

        MissingPromotionHint = MoveWarnings.MissingPromotionHint,
        PromotionHintIsNotNeeded = MoveWarnings.PromotionHintIsNotNeeded,
        AllWarnings = MoveWarnings.All,
    }
}
