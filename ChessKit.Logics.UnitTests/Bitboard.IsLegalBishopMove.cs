
using System;
using FluentAssertions;
using NUnit.Framework;

namespace ChessKit.ChessLogic.UnitTests
{
	partial class Bitboard
	{
		private bool IsLegalWhiteBishopMove(int fromSquare, int toSquare)
		{
			var rect = RectangularMasks[fromSquare*64 + toSquare];
			if (rect == 0) return false;
			var attack = BishopAttackMasks[fromSquare];
			var ray = _boards[(int)BitType.Black] & rect & attack;
			return (ray & ~(1ul << toSquare)) == 0;
		}
		
 
	}
    [TestFixture]
    public class BitboardTest_BishopMoves
    {
        private Bitboard _bitboard;

        [SetUp]
        public void Initialize()
        {
            _bitboard = new Bitboard();
        }
        [Test]
        [TestCase(TestName="White bishop moves from E4 to ")]
        public void IsLegalMove_For_White_Bishop_From_E4_To_Every_Other_Cell_Then_B1_C2_D3_F5_G6_H7_A8_B7_C6_D5_F3_G2_H1_Should_Return_False()
        {
            _bitboard[Bitboard.E4] = BitType.WhiteBishop;
	
            _bitboard.AssertLegalMoves(Square.E4, Square.B1, Square.C2, Square.D3, Square.F5, Square.G6, Square.H7, Square.A8, Square.B7, Square.C6, Square.D5, Square.F3, Square.G2, Square.H1);
        }

	
		
    }
}
