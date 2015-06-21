using System;
using System.Diagnostics.CodeAnalysis;
using ChessKit.ChessLogic.Enums;

namespace ChessKit.ChessLogic
{
    public sealed class BoardBuilder
    {
        private const int BytesCount = 128;

        internal byte[] _cells;
        public Color SideOnMove { get; set; }
        /// <summary>Gets the file at which en passant move is available</summary>
        public int? EnPassantFile { get; set; }
        public Castlings CastlingAvailability { get; set; }
        /// <summary>This is the number of halfmoves since the last pawn advance or capture. </summary>
        /// <remarks>This is used to determine if a draw can be claimed under the fifty-move rule.</remarks>
        public int HalfMoveClock { get; set; }
        /// <summary>The number of the full move. It starts at 1, and is incremented after Black's move</summary>
        public int MoveNumber { get; set; }

        public Piece this[int index]
        {
            get { return (Piece)_cells[index]; }
            set { _cells[index] = (byte) value; }
        }

        public Piece this[string index]
        {
            get { return (Piece)_cells[Coordinate.Parse(index)]; }
            set { _cells[Coordinate.Parse(index)] = (byte) value; }
        }

        public BoardBuilder()
        {
            _cells = new byte[BytesCount];
        }

        public Board ToBoard()
        {
            return new Board(this);
        }
    }
}