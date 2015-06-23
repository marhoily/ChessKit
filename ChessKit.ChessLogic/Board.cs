using System;
using System.Linq;
using ChessKit.ChessLogic.N;
using ChessKit.ChessLogic.Primitives;

namespace ChessKit.ChessLogic
{
    public sealed partial class Board
    {
        /// <summary>Board with all pieces set into the start position</summary>
        public static readonly Position StartPosition = 
            "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"
            .ParseFen();

        /// <summary>The side which is moving next (white or black)</summary>
        public Color SideOnMove { get; private set; }
        /// <summary>Gets the file at which en passant move is available</summary>
        public int? EnPassantFile { get; private set; }

        /// <summary>Gets sides players can castle to</summary>
        internal Castlings Castlings { get; private set; }
        /// <summary>This is the number of halfmoves since the last pawn advance or capture. </summary>
        /// <remarks>This is used to determine if a draw can be claimed under the fifty-move rule.</remarks>
        public int HalfMoveClock { get;  }
        /// <summary>The number of the full move. It starts at 1, and is incremented after Black's move</summary>
        public int MoveNumber { get; private set; }



        private const int BytesCount = 128;

        internal byte[] Cells { get;  }
        internal int WhiteKingPosition { get; }
        internal int BlackKingPosition { get; }

        public Board(byte[] cells, Color sideOnMove,
            int? enPassantFile, int halfMoveClock,
            int moveNumber, Castlings castlings)
        {
            Cells = cells;
            SideOnMove = sideOnMove;
            EnPassantFile = enPassantFile;
            HalfMoveClock = halfMoveClock;
            MoveNumber = moveNumber;
            Castlings = castlings;
            WhiteKingPosition = Coordinates.All.SingleOrDefault(p => this[p] == Piece.WhiteKing);
            BlackKingPosition = Coordinates.All.SingleOrDefault(p => this[p] == Piece.BlackKing);
        }

        public Piece this[int compactPosition] => (Piece)Cells[compactPosition];
    }
}

