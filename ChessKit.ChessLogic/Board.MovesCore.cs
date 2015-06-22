using System;
using System.Linq;
using ChessKit.ChessLogic.Primitives;
using static ChessKit.ChessLogic.Scanning;

namespace ChessKit.ChessLogic
{
    partial class Board
    {
        private const int BytesCount = 128;

        internal readonly byte[] _cells;
        private int _whiteKingPosition;
        private int _blackKingPosition;

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
            set
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
              ? IsAttackedByBlack(_cells, _whiteKingPosition)
              : IsAttackedByWhite(_cells, _blackKingPosition);
        }
    }
}
