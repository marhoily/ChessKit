using System;

using ChessKit.ChessLogic;

using NUnit.Framework;

using FluentAssertions;

namespace UnitTests.ChessLogic
{
  [TestFixture]
  public class MoveTest
  {
//    [Test]
    public void MoveFromEqualsTo()
    {
      Action action = () => Move.Parse("b1-b1");
      action.ShouldThrow<ArgumentException>();
    }
    [Test]
    public void ResignMove()
    {
      var move = new Move(MoveType.Resign);
      move.Kind.Should().Be(MoveType.Resign);
      move.Hints.Should().Be(MoveHints.None);
    }
    [Test]
    public void UsualMove()
    {
      var move = new Move(Position.Parse("e2"), Position.Parse("e4"))
                   {
                     Hints = MoveHints.Rook,
                   };
      move.Kind.Should().Be(MoveType.Usual);

      move.From.Should().Be(Position.Parse("e2"));
      move.To.Should().Be(Position.Parse("e4"));

      move.ProposedPromotion.Should().Be(PieceType.Queen);

      move.Hints.Should().Be(MoveHints.Rook);
    }
    [Test]
    public void PromotionMove()
    {
      var move = new Move(Position.Parse("e2"), Position.Parse("e4"), PieceType.Queen)
                   {
                     Hints = MoveHints.Promotion,
                   };

      move.Kind.Should().Be(MoveType.Usual);

      move.From.Should().Be(Position.Parse("e2"));
      move.To.Should().Be(Position.Parse("e4"));

      move.ProposedPromotion.Should().Be(PieceType.Queen);

      move.Hints.Should().Be(MoveHints.Promotion);
    }

	[Test]
	public void Parse()
	{
		var position = Move.Parse("a1-b1");
		position.From.Should().Be(Position.Parse("a1"));
		position.To.Should().Be(Position.Parse("b1"));

		position = Move.Parse("h4-g8");
		position.From.Should().Be(Position.Parse("h4"));
		position.To.Should().Be(Position.Parse("g8"));

		new Action(() => Move.Parse(null)).ShouldThrow<ArgumentException>();
    new Action(() => Move.Parse("")).ShouldThrow<ArgumentException>();
		new Action(() => Move.Parse("1-")).ShouldThrow<ArgumentOutOfRangeException>();
		new Action(() => Move.Parse("a-11")).ShouldThrow<ArgumentOutOfRangeException>();
		new Action(() => Move.Parse("!-1")).ShouldThrow<ArgumentOutOfRangeException>();
		new Action(() => Move.Parse("a0-b2")).ShouldThrow<ArgumentOutOfRangeException>();
	}
	[Test]
	public void Equality()
	{
		Assert.IsFalse(Move.Parse("b2-h1").Equals(Move.Parse("b3-h1")));
		Assert.IsTrue(Move.Parse("b2-h1").Equals((object)Move.Parse("b2-h1")));
		Assert.IsFalse(Move.Parse("h1-b2") == Move.Parse("a1-h1"));
		Assert.IsTrue(Move.Parse("h1-d3") != Move.Parse("a1-d3"));
		Assert.IsFalse(Move.Parse("b2-h1").Equals(new object()));

// TODO:		Assert.IsFalse(Move.Parse("d3-h1").Equals(null));
	}								  
	[Test]							  
	public void HashCode()
	{
		Assert.AreEqual(
		  Move.Parse("a1-b1").GetHashCode(),
		  Move.Parse("a1-b1").GetHashCode());

		Assert.AreNotEqual(
		  Move.Parse("a1-b1").GetHashCode(),
		  Move.Parse("b1-a1").GetHashCode());
	}

  }
}