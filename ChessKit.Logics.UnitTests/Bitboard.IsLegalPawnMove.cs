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
        private Bitboard _bitboard;

        [SetUp]
        public void Initialize()
        {
            _bitboard = new Bitboard();
        }
        [Test]
        [TestCase(TestName="White pawn moves from e2 to e3 and e4 only")]
        public void IsLegalMove_For_White_Pawn_From_E2_To_Every_Other_Cell_Then_E3_E4_Should_Return_False()
        {
            _bitboard[Bitboard.E2] = BitType.WhitePawn;
	
            _bitboard.AssertLegalMoves(Bitboard.E2, Bitboard.E3, Bitboard.E4);
        }

        [Test]
        [TestCase(TestName="White pawn doesn't move from e2 if blocked on e3")]
        public void IsLegalMove_For_White_Pawn_From_E2_To_Every_Other_Cell_Then__Should_Return_False()
        {
            _bitboard[Bitboard.E2] = BitType.WhitePawn;
            _bitboard[Bitboard.E3] = BitType.WhitePawn;
	
            _bitboard.AssertLegalMoves(Bitboard.E2);
        }

        [Test]
        [TestCase(TestName="White pawn only moves from e2 to e3 if blocked on e4")]
        public void IsLegalMove_For_White_Pawn_From_E2_To_Every_Other_Cell_Then_E3_Should_Return_False()
        {
            _bitboard[Bitboard.E2] = BitType.WhitePawn;
            _bitboard[Bitboard.E4] = BitType.WhitePawn;
	
            _bitboard.AssertLegalMoves(Bitboard.E2, Bitboard.E3);
        }

        [Test]
        [TestCase(TestName="White pawn only moves from e3 to e4")]
        public void IsLegalMove_For_White_Pawn_From_E3_To_Every_Other_Cell_Then_E4_Should_Return_False()
        {
            _bitboard[Bitboard.E3] = BitType.WhitePawn;
	
            _bitboard.AssertLegalMoves(Bitboard.E3, Bitboard.E4);
        }
    }
}
