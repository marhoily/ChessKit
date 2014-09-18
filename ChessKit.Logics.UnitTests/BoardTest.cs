using System;
using System.Linq;
using ChessKit.ChessLogic;

using FluentAssertions;
using NUnit.Framework;

namespace UnitTests.ChessLogic
{
    [TestFixture]
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


		[TestCase("rnbqkb1r/p2ppp1p/6pn/1pp5/4N2P/7N/PPPPPPP1/R1BQKBR1 b Qkq - 3 5", "none")]
		[TestCase("rnb1kbnr/pppp1ppp/4p3/8/5P1q/3P4/PPP1P1PP/RNBQKBNR w KQkq - 0 3", "check")]
		[TestCase("rnb1kbnr/pppp1ppp/8/4p3/5PPq/8/PPPPP2P/RNBQKBNR w KQkq - 0 3", "mate")]
		public void TestCheckAndMate(string fen, string expected)
		{
			var board = Board.FromFenString(fen);
			board.IsCheck.Should().Be(expected == "check");
			board.IsMate.Should().Be(expected == "mate");
		}

		[Test]
		public void TestDumb()
		{
			Console.WriteLine(Board.FromFenString(
			  "rnbqkbnr/pp1ppp1p/6p1/2p5/7P/2N5/PPPPPPP1/R1BQKBNR b KQkq c6 0 3")
			  .Dump());
		}

		[Test]
		public void BoardShouldKeepReferenceToThePrevBoard()
		{
			Board.StartPosition.MakeMove(Move.Parse("e2-e4"))
			  .Previous.Should().Be(Board.StartPosition);
		}
		[Test, Timeout(900)]
		public void GetLegalMovesTimeout()
		{
			var board = Board.StartPosition;
			for (var i = 0; i < 180; i++)
			{
				var legalMoves = board.GetLegalMoves();
				board = board.MakeMove(legalMoves[0]);
			}
		}
		[Test, TestCaseSource(typeof(TestMoveData), "All")]
		public void GetLegalMoves(TestMoveData d)
		{
			Console.WriteLine(d.StartingFen);
			Console.WriteLine(d.Move);
			Console.WriteLine();

			var board = Board.FromFenString(d.StartingFen);
			var expected = Move.Parse(d.Move);
			if (d.ExpectedToBeValid)
			{
				board.GetLegalMoves(expected.From).Should().Contain(expected);
				var single = board.GetLegalMoves(expected.From).Single(m => m == expected);
				single.Hints.Should().Be(d.ExpectedHints);
			}
			else
			{
				board.GetLegalMoves(expected.From).Should().NotContain(expected);
			}
		}
		[Test, TestCaseSource(typeof(TestMoveData), "All")]
		public void CanBeValidMove(TestMoveData d)
		{
			Console.WriteLine(d.StartingFen);
			Console.WriteLine(d.Move);
			Console.WriteLine();

			var board = Board.FromFenString(d.StartingFen);
			var expected = Move.Parse(d.Move);
			if (!d.ExpectedToBeValid) return;
			board.CanBeValidMove(
				board[expected.From].CompactValue,
				(int)expected.From,
				(int)expected.To).Should().BeTrue();
		}
	}
}