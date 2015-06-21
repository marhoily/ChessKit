using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
