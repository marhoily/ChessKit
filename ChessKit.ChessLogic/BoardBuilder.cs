using System;
using System.Diagnostics.CodeAnalysis;

namespace ChessKit.ChessLogic
{
    public sealed class BoardBuilder
    {
        private const int BytesCount = 128;

        internal readonly byte[] _cells;
        public PieceColor SideOnMove { get; private set; }
        /// <summary>Gets the file at which en passant move is available</summary>
        [SuppressMessage("Microsoft.Naming",
          "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "En",
          Justification = "'En-passant' is borrowed from french I guess")]
        public int? EnPassantFile { get; private set; }
        /// <summary>This is the number of halfmoves since the last pawn advance or capture. </summary>
        /// <remarks>This is used to determine if a draw can be claimed under the fifty-move rule.</remarks>
        public int HalfMoveClock { get; private set; }
        /// <summary>The number of the full move. It starts at 1, and is incremented after Black's move</summary>
        public int MoveNumber { get; private set; }

        public CompactPiece this[int index]
        {
            get { return (CompactPiece)_cells[index]; }
            set { _cells[index] = (byte) value; }
        }

        public CompactPiece this[string index]
        {
            get { return (CompactPiece)_cells[Coordinate.Parse(index)]; }
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