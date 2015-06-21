namespace ChessKit.ChessLogic.N
{
    public static class Fen
    {
        public static string Print(this Position position)
        {
            return new Board(new BoardBuilder
            {
                _cells = position.Core.Squares,
                EnPassantFile = position.Core.EnPassant,
                HalfMoveClock = position.HalfMoveClock,
                MoveNumber = position.FullMoveNumber,
                SideOnMove = position.Core.ActiveColor,
                CastlingAvailability = position.Core.CastlingAvailability,
            }).ToFenString();
        }

        public static Position ParseFen(this string fenString)
        {
            var b = Board.FromFenString(fenString);
            return new Position(
                new PositionCore(
                    b._cells, b.SideOnMove, 
                    b._Castlings, b.EnPassantFile), 
                b.HalfMoveClock, b.MoveNumber, 
                b._gameState, null);
        }
    }
}
