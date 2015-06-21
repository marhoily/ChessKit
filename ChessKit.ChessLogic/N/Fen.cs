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
                    b._Castlings, b.EnPassantFile),
                b.HalfMoveClock, b.MoveNumber,
                b._gameState, null);
        }

    }
    public static class Fen
    {
        public static string PrintFen(this Position position)
        {
            return position.ToBoard().ToFenString();
        }

        public static Position ParseFen(this string fenString)
        {
            return Board.FromFenString(fenString).FromBoard();
        }
    }
}
