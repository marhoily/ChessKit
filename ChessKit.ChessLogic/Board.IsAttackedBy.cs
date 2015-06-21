namespace ChessKit.ChessLogic
{
    partial class Board
    {
        private bool IsAttackedByWhite(int cell)
        {
            {
                var square = cell - 17;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhitePawn)
                        return true;
            }
            {
                var square = cell - 15;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhitePawn)
                        return true;
            }
            for (var i = cell + 17; (i & 0x88) == 0; i += 17)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteBishop) return true;
                break;
            }
            for (var i = cell + -15; (i & 0x88) == 0; i += -15)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteBishop) return true;
                break;
            }
            for (var i = cell + -17; (i & 0x88) == 0; i += -17)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteBishop) return true;
                break;
            }
            for (var i = cell + 15; (i & 0x88) == 0; i += 15)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteBishop) return true;
                break;
            }
            {
                var square = cell - 33;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKnight)
                        return true;
            }
            {
                var square = cell - 31;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKnight)
                        return true;
            }
            {
                var square = cell - -31;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKnight)
                        return true;
            }
            {
                var square = cell - -33;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKnight)
                        return true;
            }
            {
                var square = cell - 18;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKnight)
                        return true;
            }
            {
                var square = cell - 14;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKnight)
                        return true;
            }
            {
                var square = cell - -14;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKnight)
                        return true;
            }
            {
                var square = cell - -18;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKnight)
                        return true;
            }
            for (var i = cell + 16; (i & 0x88) == 0; i += 16)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteRook) return true;
                break;
            }
            for (var i = cell + 1; (i & 0x88) == 0; i += 1)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteRook) return true;
                break;
            }
            for (var i = cell + -16; (i & 0x88) == 0; i += -16)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteRook) return true;
                break;
            }
            for (var i = cell + -1; (i & 0x88) == 0; i += -1)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteRook) return true;
                break;
            }
            for (var i = cell + 16; (i & 0x88) == 0; i += 16)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteQueen) return true;
                break;
            }
            for (var i = cell + 1; (i & 0x88) == 0; i += 1)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteQueen) return true;
                break;
            }
            for (var i = cell + -16; (i & 0x88) == 0; i += -16)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteQueen) return true;
                break;
            }
            for (var i = cell + -1; (i & 0x88) == 0; i += -1)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteQueen) return true;
                break;
            }
            for (var i = cell + 17; (i & 0x88) == 0; i += 17)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteQueen) return true;
                break;
            }
            for (var i = cell + -15; (i & 0x88) == 0; i += -15)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteQueen) return true;
                break;
            }
            for (var i = cell + -17; (i & 0x88) == 0; i += -17)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteQueen) return true;
                break;
            }
            for (var i = cell + 15; (i & 0x88) == 0; i += 15)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.WhiteQueen) return true;
                break;
            }
            {
                var square = cell - 16;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKing)
                        return true;
            }
            {
                var square = cell - 17;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKing)
                        return true;
            }
            {
                var square = cell - 1;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKing)
                        return true;
            }
            {
                var square = cell - -15;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKing)
                        return true;
            }
            {
                var square = cell - -16;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKing)
                        return true;
            }
            {
                var square = cell - -17;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKing)
                        return true;
            }
            {
                var square = cell - -1;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKing)
                        return true;
            }
            {
                var square = cell - 15;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.WhiteKing)
                        return true;
            }
            return false;
        }
        private bool IsAttackedByBlack(int cell)
        {
            {
                var square = cell - -15;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackPawn)
                        return true;
            }
            {
                var square = cell - -17;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackPawn)
                        return true;
            }
            for (var i = cell + 17; (i & 0x88) == 0; i += 17)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackBishop) return true;
                break;
            }
            for (var i = cell + -15; (i & 0x88) == 0; i += -15)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackBishop) return true;
                break;
            }
            for (var i = cell + -17; (i & 0x88) == 0; i += -17)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackBishop) return true;
                break;
            }
            for (var i = cell + 15; (i & 0x88) == 0; i += 15)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackBishop) return true;
                break;
            }
            {
                var square = cell - 33;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKnight)
                        return true;
            }
            {
                var square = cell - 31;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKnight)
                        return true;
            }
            {
                var square = cell - -31;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKnight)
                        return true;
            }
            {
                var square = cell - -33;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKnight)
                        return true;
            }
            {
                var square = cell - 18;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKnight)
                        return true;
            }
            {
                var square = cell - 14;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKnight)
                        return true;
            }
            {
                var square = cell - -14;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKnight)
                        return true;
            }
            {
                var square = cell - -18;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKnight)
                        return true;
            }
            for (var i = cell + 16; (i & 0x88) == 0; i += 16)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackRook) return true;
                break;
            }
            for (var i = cell + 1; (i & 0x88) == 0; i += 1)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackRook) return true;
                break;
            }
            for (var i = cell + -16; (i & 0x88) == 0; i += -16)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackRook) return true;
                break;
            }
            for (var i = cell + -1; (i & 0x88) == 0; i += -1)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackRook) return true;
                break;
            }
            for (var i = cell + 16; (i & 0x88) == 0; i += 16)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackQueen) return true;
                break;
            }
            for (var i = cell + 1; (i & 0x88) == 0; i += 1)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackQueen) return true;
                break;
            }
            for (var i = cell + -16; (i & 0x88) == 0; i += -16)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackQueen) return true;
                break;
            }
            for (var i = cell + -1; (i & 0x88) == 0; i += -1)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackQueen) return true;
                break;
            }
            for (var i = cell + 17; (i & 0x88) == 0; i += 17)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackQueen) return true;
                break;
            }
            for (var i = cell + -15; (i & 0x88) == 0; i += -15)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackQueen) return true;
                break;
            }
            for (var i = cell + -17; (i & 0x88) == 0; i += -17)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackQueen) return true;
                break;
            }
            for (var i = cell + 15; (i & 0x88) == 0; i += 15)
            {
                var piece = _cells[i];
                if (piece == 0) continue;
                if (piece == (byte) Piece.BlackQueen) return true;
                break;
            }
            {
                var square = cell - 16;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKing)
                        return true;
            }
            {
                var square = cell - 17;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKing)
                        return true;
            }
            {
                var square = cell - 1;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKing)
                        return true;
            }
            {
                var square = cell - -15;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKing)
                        return true;
            }
            {
                var square = cell - -16;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKing)
                        return true;
            }
            {
                var square = cell - -17;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKing)
                        return true;
            }
            {
                var square = cell - -1;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKing)
                        return true;
            }
            {
                var square = cell - 15;
                if ((square & 0x88) == 0)
                    if (_cells[square] == (byte) Piece.BlackKing)
                        return true;
            }
            return false;
        }
    }
}