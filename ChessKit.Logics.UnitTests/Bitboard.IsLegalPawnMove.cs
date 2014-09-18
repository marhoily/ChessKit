using System;
using FluentAssertions;
using NUnit.Framework;

namespace ChessKit.ChessLogic.UnitTests
{
	partial class Bitboard
	{
		private bool IsLegalWhitePawnMove(ulong fromBit, ulong toBit)
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
    public class BitboardTest_PawnMoves
    {
        [Test]
        public void IsLegalMove_For_White_Pawn_From_E3_To_Every_Other_Cell_Then_E4_Should_Return_False()
        {
            var bitboard = new Bitboard();
            bitboard[Bitboard.E3] = BitType.WhitePawn;
			for (int i = 0; i < 64; i++)
			{
				var pos = (1ul << i);
				if (pos == Bitboard.E4) continue;
				bitboard.IsLegalMove(Bitboard.E3, pos).Should().BeFalse();
			}
        }
        [Test]
        public void IsLegalMove_For_White_Pawn_When_DoublePush_From_Third_Row_Should_Return_False()
        {
            var bitboard = new Bitboard();
            bitboard[Bitboard.E3] = BitType.WhitePawn;
            bitboard.IsLegalMove(Bitboard.E3, Bitboard.E5).Should().BeFalse();
        }
        [Test]
        public void IsLegalMove_For_White_Pawn_From_Second_To_Fourth_Row_When_Blocked_On_Fourth_Row_Should_Return_False()
        {
            var bitboard = new Bitboard();
            bitboard[Bitboard.E2] = BitType.WhitePawn;
            bitboard[Bitboard.E4] = BitType.WhitePawn;
            bitboard.IsLegalMove(Bitboard.E2, Bitboard.E4).Should().BeFalse();
        }
        [Test]
        public void IsLegalMove_For_White_Pawn_From_Second_To_Fourth_Row_When_Blocked_On_Third_Row_Should_Return_False()
        {
            var bitboard = new Bitboard();
            bitboard[Bitboard.E2] = BitType.WhitePawn;
            bitboard[Bitboard.E3] = BitType.WhitePawn;
            bitboard.IsLegalMove(Bitboard.E2, Bitboard.E4).Should().BeFalse();
        }
        [Test]
        public void IsLegalMove_For_White_Pawn_From_Second_To_Fourth_Row_Should_Return_True()
        {
            var bitboard = new Bitboard();
            bitboard[Bitboard.E2] = BitType.WhitePawn;
            bitboard.IsLegalMove(Bitboard.E2, Bitboard.E4).Should().BeTrue();
        }
        [Test]
        public void IsLegalMove_For_White_Pawn_From_Second_To_Third_Row_Should_Return_True()
        {
            var bitboard = new Bitboard();
            bitboard[Bitboard.E2] = BitType.WhitePawn;
            bitboard.IsLegalMove(Bitboard.E2, Bitboard.E3).Should().BeTrue();
        }
        [Test]
        public void IsLegalMove_For_White_Pawn_From_Second_To_Fifth_Row_Should_Return_False()
        {
            var bitboard = new Bitboard();
            bitboard[Bitboard.E2] = BitType.WhitePawn;
            bitboard.IsLegalMove(Bitboard.E2, 36).Should().BeFalse();
        }
		
    }
}
