using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ChessKit.ChessLogic.Primitives;
using JetBrains.Annotations;

namespace ChessKit.ChessLogic
{
    partial class Board
    {
        /// <summary>Board with all pieces set into the start position</summary>
        [SuppressMessage("Microsoft.Security",
          "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
          Justification = "Board is immutable")]
        public static readonly Board StartPosition = Fen.ParseFen(
            "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

        public string ToFenString()
        {
            var fen = new StringBuilder(77);
            for (int empty = 0, sq = 63; sq >= 0; sq--)
            {
                var idx = (sq / 8) * 8 + 7 - sq % 8;
                idx = idx + (idx & ~7);
                if (_cells[idx] == 0)
                {
                    empty++;
                    if (0 == sq % 8)
                    {
                        if (empty != 0) fen.Append((char)('0' + empty));
                        if (sq != 0) fen.Append('/');
                        empty = 0;
                    }
                    continue;
                }

                if (empty != 0) fen.Append((char)('0' + empty));
                fen.Append(((Piece)_cells[idx]).GetSymbol());
                empty = 0;
                if (sq != 0 && sq % 8 == 0) fen.Append('/');
            }

            fen.Append(' ');
            fen.Append(SideOnMove == Color.White ? 'w' : 'b');

            fen.Append(' ');
            var castling = Castlings;
            if (castling == Castlings.None)
            {
                fen.Append('-');
            }
            else
            {
                if ((castling & Castlings.WK) != 0) fen.Append('K');
                if ((castling & Castlings.WQ) != 0) fen.Append('Q');
                if ((castling & Castlings.BK) != 0) fen.Append('k');
                if ((castling & Castlings.BQ) != 0) fen.Append('q');
            }

            fen.Append(' ');
            if (!EnPassantFile.HasValue)
            {
                fen.Append('-');
            }
            else
            {
                fen.Append("abcdefgh"[EnPassantFile.GetValueOrDefault()]);
                fen.Append(SideOnMove == Color.White ? '6' : '3');
            }

            fen.Append(' ');
            fen.Append(HalfMoveClock);

            fen.Append(' ');
            fen.Append(MoveNumber);

            return fen.ToString();
        }
    }
}

