﻿using System;
using ChessKit.ChessLogic.Primitives;
using JetBrains.Annotations;

namespace ChessKit.ChessLogic
{
    public static class Fen
    {

        /// <summary> Load position from FEN string </summary>
        /// <param name="fen">FEN string</param>
        /// <remarks>http://en.wikipedia.org/wiki/Forsyth%E2%80%93Edwards_Notation</remarks>
        public static Board ParseFen([NotNull] this string fen)
        {
            if (fen == null) throw new ArgumentNullException(nameof(fen));
            var offset = 0;
            try
            {
                var cells = LoadPiecePlacementSection(fen, ref offset);
                var color = LoadActiveColorSection(fen, ref offset);
                var castling = LoadCastlingAvailabilitySection(fen, ref offset);
                var enPassant = LoadEnPassantSection(fen, ref offset);
                var halfmoveClock = LoadHalfmoveClockSection(fen, ref offset);
                var moveNumber = LoadFullmoveNumberSection(fen, ref offset);
                return new Board(cells, color, enPassant,
                    halfmoveClock, moveNumber, castling);
            }
            catch (IndexOutOfRangeException x)
            {
                throw new FormatException("Unexpected end of FEN string", x);
            }
        }

        static byte[] LoadPiecePlacementSection(string fen, ref int i)
        {
            var res = new byte[128];
            for (var sq = 63; ; i++)
            {
                if (fen[i] == ' ') break;
                if (fen[i] >= '1' && fen[i] <= '9') sq -= fen[i] - '0';
                else if ('/' == fen[i]) { }
                else
                {
                    var c = (sq / 8) * 8 + 7 - sq % 8;
                    res[c + (c & ~7)] = (byte)fen[i].ParsePiece();
                    sq--;
                }
            }
            i++; // Skip the space
            return res;
        }
        static Color LoadActiveColorSection(string fen, ref int i)
        {
            var res = fen[i++].ParseColor();
            if (fen[i] != ' ')
                throw new FormatException("Unexpected symbol");
            i++; // skip the space 
            return res;
        }
        static Castlings LoadCastlingAvailabilitySection(string fen, ref int i)
        {
            var flags = default(Castlings);
            for (; ; i++)
            {
                if (fen[i] == '-') { }
                else if (fen[i] == 'K') flags |= Castlings.WK;
                else if (fen[i] == 'Q') flags |= Castlings.WQ;
                else if (fen[i] == 'k') flags |= Castlings.BK;
                else if (fen[i] == 'q') flags |= Castlings.BQ;
                else if (fen[i] == ' ') break;
                else throw new FormatException("illegal character");
            }
            i++; // Skip the space
            return flags;
        }
        static int? LoadEnPassantSection(string fen, ref int i)
        {
            int? res = null;
            if (fen[i] != '-')
            {
                res = EnPassantParser.Parse(fen[i]);
            }
            i++; // Skip the rank, or skip the '-'
            i++; // Skip the space
            return res;
        }
        static int LoadHalfmoveClockSection(string fen, ref int i)
        {
            var res = 0;
            for (; ; i++)
            {
                if (fen[i] == ' ') break;
                if (fen[i] >= '0' && fen[i] <= '9')
                    res = res * 10 + fen[i] - '0';
            }
            i++; // Skip the space
            return res;
        }
        static int LoadFullmoveNumberSection(string fen, ref int i)
        {
            var res = 0;
            for (; i < fen.Length; i++)
                if (fen[i] >= '0' && fen[i] <= '9')
                    res = res * 10 + fen[i] - '0';
            return res;

        }

        static class EnPassantParser
        {
            static readonly int[] Symbols;
            const char Z = '-';
            const int Illegal = 99;

            static EnPassantParser()
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