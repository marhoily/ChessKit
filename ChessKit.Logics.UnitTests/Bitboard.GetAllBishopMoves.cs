
using System;
using FluentAssertions;
using NUnit.Framework;

namespace ChessKit.ChessLogic.UnitTests
{
	partial class Bitboard
	{
		public UInt64 GetAllWhiteBishopMoves(int fromSquare)
		{
		    return 0; 
		}
		
 
	}
    [TestFixture]
    public class BitboardTest_GetAllBishopMoves
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
	
            _bitboard.GetAllWhiteBishopMoves(Square.E4)
				.ShouldBeSameMask(Utils.GetMask(Square.B1, Square.C2, Square.D3, Square.F5, Square.G6, Square.H7, Square.A8, Square.B7, Square.C6, Square.D5, Square.F3, Square.G2, Square.H1));
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
	
            _bitboard.GetAllWhiteBishopMoves(Square.E4)
				.ShouldBeSameMask(Utils.GetMask(Square.D3, Square.F5, Square.G6, Square.D5, Square.F3, Square.G2, Square.H1));
        }

	
		
    }
}
