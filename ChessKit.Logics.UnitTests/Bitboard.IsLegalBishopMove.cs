
using System;
using FluentAssertions;
using NUnit.Framework;

namespace ChessKit.ChessLogic.UnitTests
{
	partial class Bitboard
	{
		private bool IsLegalWhiteBishopMove(int fromSquare, int toSquare)
		{
            // Get mask for the (from, to) reactangle 
		    var index = fromSquare*64 + toSquare; 
		    var boundaries = RectangularMasks[index]; 
			if (boundaries == 0) return false;
            // Get bishop attack pattern for the square
            var attackPattern = BishopAttackMasks[fromSquare];
            // Get attack ray that connects fromSquare and toSquare
            var attackRay = boundaries & attackPattern;
            if (attackRay == 0) return false;
            // The attack ray should not intercect 
            // with any pieces of the same color
            if ((~_occupanceWhite & attackRay) != attackRay) return false;
            // The attack ray may only intercect piece of the opposite 
            // color in exactly one square: the toSquare
            var hitTargets = _occupanceBlack & attackRay; 
		    var toBit = (1ul << toSquare); 
		    return (hitTargets | toBit) == toBit; 
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
        [TestCase(TestName="Validate white bishop moves from E4 on the empty board")]
        public void IsLegalMove_For_White_Bishop_From_E4_To_Every_Other_Cell_Then_B1_C2_D3_F5_G6_H7_A8_B7_C6_D5_F3_G2_H1_Should_Return_False()
        {
            _bitboard[Bitboard.E4] = BitType.WhiteBishop;
	
            _bitboard.AssertLegalMoves(Square.E4, Square.B1, Square.C2, Square.D3, Square.F5, Square.G6, Square.H7, Square.A8, Square.B7, Square.C6, Square.D5, Square.F3, Square.G2, Square.H1);
        }

        [Test]
        [TestCase(TestName="Validate white bishop moves from E4 on with obstacles")]
        public void IsLegalMove_For_White_Bishop_From_E4_To_Every_Other_Cell_Then_D3_F5_G6_D5_F3_G2_H1_Should_Return_False()
        {
            _bitboard[Bitboard.E4] = BitType.WhiteBishop;
            _bitboard[Bitboard.C2] = BitType.WhiteBishop;
            _bitboard[Bitboard.H7] = BitType.WhiteBishop;
            _bitboard[Bitboard.D5] = BitType.BlackBishop;
            _bitboard[Bitboard.H1] = BitType.BlackBishop;
	
            _bitboard.AssertLegalMoves(Square.E4, Square.D3, Square.F5, Square.G6, Square.D5, Square.F3, Square.G2, Square.H1);
        }

	
		
    }
}
