using System;
using FluentAssertions;
using NUnit.Framework;
using U64 = System.UInt64;

namespace ChessKit.ChessLogic.UnitTests
{
    public sealed partial class Bitboard
    {
        private readonly UInt64[] _boards = new UInt64[14];

        public BitType this[UInt64 cell]
        {
            get
            {
                for (var i = 0; i < _boards.Length; i++)
                    if ((_boards[i] & cell) == cell)
                        return (BitType)i;
                return BitType.Empty;
            }
            set
            {
                _boards[(int)value] |= cell;
                if (IsWhite(value))
                    _boards[(int) BitType.White] |= cell;
                else
                    _boards[(int)BitType.Black] |= cell;
            }
        }

        public bool IsLegalMove(U64 from, U64 to)
        {
            switch (this[from])
            {
                case BitType.WhitePawn:
                    return IsLegalWhitePawnMove(from, to);
            }
            throw new Exception();
        }
        public bool IsLegalMove(int fromSquare, int toSquare)
        {
            switch (this[1ul << fromSquare])
            {
                case BitType.WhiteBishop:
                    return IsLegalWhiteBishopMove(fromSquare, toSquare);
            }
            throw new Exception();
        }
    }
    [TestFixture]
    public class BitboardTest
    {
        [Test]
        public void Indexer_Should_Get_What_Was_Set()
        {
            var bitboard = new Bitboard();
            bitboard[Bitboard.E2] = BitType.WhitePawn;
            bitboard[Bitboard.E2].Should().Be(BitType.WhitePawn);
        }

        [Test]
        public void Indexer_When_Calling_Set_Second_Time_Should_Not_Erease_First()
        {
            var bitboard = new Bitboard();
            bitboard[Bitboard.E2] = BitType.WhitePawn;
            bitboard[Bitboard.E3] = BitType.WhitePawn;
            bitboard[Bitboard.E2].Should().Be(BitType.WhitePawn);
        }
        [Test]
        public void Indexer_When_Not_Set_Should_Get_Empty()
        {
            var bitboard = new Bitboard();
            bitboard[2].Should().Be(BitType.Empty);
        }
    }
}