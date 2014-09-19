
using System;
using FluentAssertions;
using NUnit.Framework;

namespace ChessKit.ChessLogic.UnitTests
{
	partial class Bitboard
	{
		private bool IsLegalWhiteBishopMove(ulong fromBit, ulong toBit)
		{
			var pushOnce = NortOne(fromBit);
			if (this[pushOnce] != BitType.Empty) return false;
			if (pushOnce == toBit) return true;
			if ((fromBit & Row2) == 0) return false;
			var pushTwice = NortOne(pushOnce);
			if (this[pushTwice] != BitType.Empty) return false;
			return pushTwice == toBit;
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
     //   [Test]
      //  [TestCase(TestName="White bishop moves from E4 to ")]
        public void IsLegalMove_For_White_Bishop_From_E4_To_Every_Other_Cell_Then_B1_C2_D3_F5_G6_H7_A8_B7_C6_D5_F3_G2_H1_Should_Return_False()
        {
            _bitboard[Bitboard.E4] = BitType.WhiteBishop;
	
            _bitboard.AssertLegalMoves(Bitboard.E4, Bitboard.B1, Bitboard.C2, Bitboard.D3, Bitboard.F5, Bitboard.G6, Bitboard.H7, Bitboard.A8, Bitboard.B7, Bitboard.C6, Bitboard.D5, Bitboard.F3, Bitboard.G2, Bitboard.H1);
        }
    }
}
