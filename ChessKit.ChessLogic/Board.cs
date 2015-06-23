using System;
using System.Collections.Generic;
using System.Linq;
using ChessKit.ChessLogic.Primitives;
using static ChessKit.ChessLogic.Primitives.MoveAnnotations;

namespace ChessKit.ChessLogic
{
    public sealed partial class Board
    {
        /// <summary>Board with all pieces set into the start position</summary>
        public static readonly Board StartPosition = Fen.ParseFen(
            "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

        internal GameStates GameState;

        /// <summary>The side which is moving next (white or black)</summary>
        public Color SideOnMove { get; private set; }
        /// <summary>Gets the file at which en passant move is available</summary>
        public int? EnPassantFile { get; set; }

        /// <summary>Gets sides players can castle to</summary>
        internal Castlings Castlings;
        /// <summary>This is the number of halfmoves since the last pawn advance or capture. </summary>
        /// <remarks>This is used to determine if a draw can be claimed under the fifty-move rule.</remarks>
        public int HalfMoveClock { get; set; }
        /// <summary>The number of the full move. It starts at 1, and is incremented after Black's move</summary>
        public int MoveNumber { get; private set; }

        public Move PreviousMove { get; }

        public bool IsCheck => GameState == GameStates.Check;
        public bool IsMate => GameState == GameStates.Mate;

        #region ' MakeMove '
        #endregion

        public Piece this[string index] => this[index.ParseCoordinate()];

        public bool IsAttackedBy(Color side, int square)
        {
            return side == Color.White
                ? Scanning.IsAttackedByWhite(_cells, square)
                : Scanning.IsAttackedByBlack(_cells, square);
        }

        public bool IsInCheck(Color side)
        {
            return side == Color.White
                ? Scanning.IsAttackedByBlack(_cells, _whiteKingPosition)
                : Scanning.IsAttackedByWhite(_cells, _blackKingPosition);
        }

        private const int BytesCount = 128;

        internal readonly byte[] _cells;
        internal int _whiteKingPosition;
        internal int _blackKingPosition;

        /// <summary>Deep copy</summary>
        private Board(Board source)
        {
            _whiteKingPosition = source._whiteKingPosition;
            _blackKingPosition = source._blackKingPosition;

            _cells = new byte[BytesCount];
            Buffer.BlockCopy(source._cells, 0, _cells, 0, BytesCount);
        }

        /// <summary>Empty board</summary>
        private Board()
        {
            _cells = new byte[BytesCount];
            _whiteKingPosition = -1;
            _blackKingPosition = -1;
        }

        internal Board(BoardBuilder boardBuilder)
        {
            _cells = new byte[BytesCount];
            Buffer.BlockCopy(boardBuilder._cells, 0, _cells, 0, BytesCount);
            SideOnMove = boardBuilder.SideOnMove;
            EnPassantFile = boardBuilder.EnPassantFile;
            HalfMoveClock = boardBuilder.HalfMoveClock;
            MoveNumber = boardBuilder.MoveNumber;
            _whiteKingPosition = Coordinates.All.SingleOrDefault(p => this[p] == Piece.WhiteKing);
            _blackKingPosition = Coordinates.All.SingleOrDefault(p => this[p] == Piece.BlackKing);
            Castlings = boardBuilder.CastlingAvailability;
        }

        public Board(byte[] cells, Color sideOnMove,
            int? enPassantFile, int halfMoveClock,
            int moveNumber, Castlings castlings)
        {
            _cells = cells;
            SideOnMove = sideOnMove;
            EnPassantFile = enPassantFile;
            HalfMoveClock = halfMoveClock;
            MoveNumber = moveNumber;
            Castlings = castlings;
            _whiteKingPosition = Coordinates.All.SingleOrDefault(p => this[p] == Piece.WhiteKing);
            _blackKingPosition = Coordinates.All.SingleOrDefault(p => this[p] == Piece.BlackKing);
        }

        public Piece this[int compactPosition]
        {
            get
            {
                return (Piece)_cells[compactPosition];
            }
            private set
            {
                _cells[compactPosition] = (byte)value;

                switch (value)
                {
                    case Piece.WhiteKing:
                        _whiteKingPosition = compactPosition;
                        break;
                    case Piece.BlackKing:
                        _blackKingPosition = compactPosition;
                        break;
                }
            }
        }


        private bool IsUnderCheck(Color kingColor)
        {
            return kingColor == Color.White
              ? Scanning.IsAttackedByBlack(_cells, _whiteKingPosition)
              : Scanning.IsAttackedByWhite(_cells, _blackKingPosition);
        }

    }
}

