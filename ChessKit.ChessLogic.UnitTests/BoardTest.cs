using System;
using System.Collections.Generic;
using System.Linq;
using ChessKit.ChessLogic.Algorithms;
using ChessKit.ChessLogic.Primitives;
using FluentAssertions;
using Xunit;

namespace ChessKit.ChessLogic.UnitTests
{
	public class BoardTest
	{
		/*
		 * TODO: Test board statuses: check, mate
		 * TODO: Board/Compact board covered?
		 * 
		 * TODO: Take from Valil: Move -> EndSAN
		 * TODO: Take from Valil: Check the board for validity
		 * TODO: Take from Valil: zobristKeys
		 * TODO: Take from Valil: Concrete exceptions thrown in FEN 
		 * TODO: Take from Valil: PGN
		 * TODO: IsAttacked[pos] or CountAttackers[pos] -> public interface?
		 * 
		 * TODO: Optimize: 0x88
		 * 
		 * TODO: Castling is O-O, and not e8-g8 (in tests, csv files)
		 * TODO: Use PEX
		 * TODO: Code analysis
		 * TODO: Optimize: "is check" to check BEFORE making actual change to the board
		 * 
		 */


        [Theory]
		[InlineData("rnbqkb1r/p2ppp1p/6pn/1pp5/4N2P/7N/PPPPPPP1/R1BQKBR1 b Qkq - 3 5", "none")]
		[InlineData("rnb1kbnr/pppp1ppp/4p3/8/5P1q/3P4/PPP1P1PP/RNBQKBNR w KQkq - 0 3", "check")]
		[InlineData("rnb1kbnr/pppp1ppp/8/4p3/5PPq/8/PPPPP2P/RNBQKBNR w KQkq - 0 3", "mate")]
		public void TestCheckAndMate(string fen, string expected)
		{
			var board = Fen.ParseFen(fen);
			//board.IsCheck.Should().Be(expected == "check");
			//board.IsMate.Should().Be(expected == "mate");
		}

		[Fact]
		public void TestDumb()
		{
			Console.WriteLine(Fen.ParseFen(
			  "rnbqkbnr/pp1ppp1p/6p1/2p5/7P/2N5/PPPPPPP1/R1BQKBNR b KQkq c6 0 3")
			  .Dump());
		}

		[Fact]
		public void GetLegalMovesTimeout()
		{
			var board = Board.StartPosition;
			for (var i = 0; i < 180; i++)
			{
				var legalMoves = board.GetAllLegalMoves();
			    board = legalMoves[0].ToPosition();
			}
		}
		[Theory]
        [MemberData(nameof(TestMoveData.All), MemberType = typeof(TestMoveData))]
        public void GetLegalMoves(TestMoveData d)
		{
			Console.WriteLine(d.StartingFen);
			Console.WriteLine(d.Move);
			Console.WriteLine();

			var board = d.StartingFen.ParseFen();
			var expected = MoveR.Parse(d.Move);
            var legalMoves = board.GetLegalMovesFromSquare(expected.From);
			if (d.ExpectedToBeValid)
			{
			    legalMoves
                    .Select(m => m.Move.ToString())
                    .Should().Contain(d.Move);
			    var single = legalMoves.Single(m => m.Move.ToString() == d.Move);
				single.Annotations.Should().Be(d.ExpectedAnnotations);
			}
			else
			{
                legalMoves
                    .Select(m => m.Move.ToString())
                    .Should().NotContain(d.Move);
			}
		}
        [Theory]
        [MemberData(nameof(TestMoveData.All), MemberType = typeof(TestMoveData))]
        public void CanBeValidMove(TestMoveData d)
		{
			Console.WriteLine(d.StartingFen);
			Console.WriteLine(d.Move);
			Console.WriteLine();

			var board = d.StartingFen.ParseFen();
			var expected = MoveR.Parse(d.Move);
			if (!d.ExpectedToBeValid) return;
            CanBeValid.CanBeValidMove(
                board.Core.Squares,
                (Piece)board.Core.Squares[expected.From],
				expected.From,
				expected.To).Should().BeTrue();
		}
    }
}