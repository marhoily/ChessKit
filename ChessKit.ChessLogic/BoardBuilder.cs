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

        [SuppressMessage("Microsoft.Design", "CA1043:UseIntegralOrStringArgumentForIndexers",
              Justification = "It just seems right to use Position as a natural indexer")]
        public Piece this[Position index]
        {
            get { return Piece.Unpack((CompactPiece)_cells[(int)index]); }
            set { _cells[(int)index] = (byte)Piece.Pack(value); }
        }

        public Piece this[string index]
        {
            get { return this[Position.Parse(index)]; }
            set { this[Position.Parse(index)] = value; }
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