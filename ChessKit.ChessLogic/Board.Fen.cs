using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ChessKit.ChessLogic.Enums;
using JetBrains.Annotations;

namespace ChessKit.ChessLogic
{
    partial class Board
    {
        /// <summary>Board with all pieces set into the start position</summary>
        [SuppressMessage("Microsoft.Security",
          "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes",
          Justification = "Board is immutable")]
        public static readonly Board StartPosition = FromFenString(
            "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

        /// <summary> Load position from FEN string </summary>
        /// <param name="fen">FEN string</param>
        /// <remarks>http://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation</remarks>
        public static Board FromFenString([NotNull] string fen)
        {
            if (fen == null) throw new ArgumentNullException(nameof(fen));
            var offset = 0;
            var board = new Board();
            try
            {
                board.LoadPiecePlacementSection(fen, ref offset);
                board.LoadActiveColorSection(fen, ref offset);
                board.LoadCastlingAvailabilitySection(fen, ref offset);
                board.LoadEnPassantSection(fen, ref offset);
                board.LoadHalfmoveClockSection(fen, ref offset);
                board.LoadFullmoveNumberSection(fen, ref offset);
                board.SetStatus();
            }
            catch (IndexOutOfRangeException x)
            {
                throw new FormatException("Unexpected end of FEN string", x);
            }
            return board;
        }

        private void LoadPiecePlacementSection(string fen, ref int i)
        {
            for (var sq = 63; ; i++)
            {
                if (fen[i] == ' ') break;
                if (fen[i] >= '1' && fen[i] <= '9') sq -= fen[i] - '0';
                else if ('/' == fen[i]) { }
                else
                {
                    var c = (sq / 8) * 8 + 7 - sq % 8;
                    this[c + (c & ~7)] = fen[i].Parse();
                    sq--;
                }
            }
            i++; // Skip the space
        }
        private void LoadActiveColorSection(string fen, ref int i)
        {
            SideOnMove = fen[i++].ParseColor();
            if (fen[i] != ' ')
                throw new FormatException("Unexpected symbol");
            i++; // skip the space 
        }
        private void LoadCastlingAvailabilitySection(string fen, ref int i)
        {
            var flags = default(Caslings);
            for (; ; i++)
            {
                if (fen[i] == '-') { }
                else if (fen[i] == 'K') flags |= Caslings.WhiteKing;
                else if (fen[i] == 'Q') flags |= Caslings.WhiteQueen;
                else if (fen[i] == 'k') flags |= Caslings.BlackKing;
                else if (fen[i] == 'q') flags |= Caslings.BlackQueen;
                else if (fen[i] == ' ') break;
                else throw new FormatException("illegal character");
            }
            _caslings = flags;
            i++; // Skip the space
        }
        private void LoadEnPassantSection(string fen, ref int i)
        {
            if (fen[i] != '-')
            {
                EnPassantFile = EnPassant.Parse(fen[i]);
            }
            i++; // Skip the rank, or skip the '-'
            i++; // Skip the space
        }
        private void LoadHalfmoveClockSection(string fen, ref int i)
        {
            var res = 0;
            for (; ; i++)
            {
                if (fen[i] == ' ') break;
                if (fen[i] >= '0' && fen[i] <= '9')
                    res = res * 10 + fen[i] - '0';
            }
            HalfMoveClock = res;
            i++; // Skip the space
        }
        private void LoadFullmoveNumberSection(string fen, ref int i)
        {
            var res = 0;
            for (; i < fen.Length; i++)
                if (fen[i] >= '0' && fen[i] <= '9')
                    res = res * 10 + fen[i] - '0';
            MoveNumber = res;
        }

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
            var castling = _caslings;
            if (castling == Caslings.None)
            {
                fen.Append('-');
            }
            else
            {
                if ((castling & Caslings.WhiteKing) != 0) fen.Append('K');
                if ((castling & Caslings.WhiteQueen) != 0) fen.Append('Q');
                if ((castling & Caslings.BlackKing) != 0) fen.Append('k');
                if ((castling & Caslings.BlackQueen) != 0) fen.Append('q');
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

       
        private static class EnPassant
        {
            private static readonly int[] Symbols;
            private const char Z = '-';
            private const int Illegal = 99;

            [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline",
              Justification = "Too complex for field initialize")]
            static EnPassant()
            {
                Symbols = new int['z' - Z];

                for (var i = 0; i < Symbols.Length; i++)
                    Symbols[i] = Illegal;

                Symbols['-' - Z] = -1;

                Symbols['a' - Z] = 0;
                Symbols['b' - Z] = 1;
                Symbols['c' - Z] = 2;
                Symbols['d' - Z] = 3;
                Symbols['e' - Z] = 4;
                Symbols['f' - Z] = 5;
                Symbols['g' - Z] = 6;
                Symbols['h' - Z] = 7;

                Symbols['A' - Z] = 0;
                Symbols['B' - Z] = 1;
                Symbols['C' - Z] = 2;
                Symbols['D' - Z] = 3;
                Symbols['E' - Z] = 4;
                Symbols['F' - Z] = 5;
                Symbols['G' - Z] = 6;
                Symbols['H' - Z] = 7;
            }

            public static int Parse(char symbol)
            {
                int res;
                try
                {
                    res = Symbols[symbol - Z];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new FormatException("illegal character");
                }
                if (res == Illegal)
                    throw new FormatException("illegal character");
                return res;
            }
        }
    }
}

