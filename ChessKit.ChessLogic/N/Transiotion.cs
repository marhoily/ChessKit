namespace ChessKit.ChessLogic.N
{
    public static class Transiotion
    {
        public static Board ToBoard(this Position position)
        {
            return new Board(new BoardBuilder
            {
                _cells = position.Core.Squares,
                EnPassantFile = position.Core.EnPassant,
                HalfMoveClock = position.HalfMoveClock,
                MoveNumber = position.FullMoveNumber,
                SideOnMove = position.Core.ActiveColor,
                CastlingAvailability = position.Core.CastlingAvailability,
            });
        }
        public static Board ToBoard(this PositionCore core)
        {
            return new Board(new BoardBuilder
            {
                _cells = core.Squares,
                EnPassantFile = core.EnPassant,
                HalfMoveClock = 0,
                MoveNumber = 1,
                SideOnMove = core.ActiveColor,
                CastlingAvailability = core.CastlingAvailability,
            });
        }

        public static Position FromBoard(this Board b)
        {
            return new Position(
                new PositionCore(
                    b._cells, b.SideOnMove,
                    b.Castlings, b.EnPassantFile),
                b.HalfMoveClock, b.MoveNumber,
                b.GameState, null,
                b._whiteKingPosition,
                b._blackKingPosition);
        }

    }
}