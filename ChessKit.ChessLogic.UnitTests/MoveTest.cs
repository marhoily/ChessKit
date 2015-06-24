using System;
using ChessKit.ChessLogic.Primitives;
using FluentAssertions;
using Xunit;

namespace ChessKit.ChessLogic.UnitTests
{
    public class MoveTest
    {
        //    [Test]
        public void MoveFromEqualsTo()
        {
            Action action = () => Move.Parse("b1-b1");
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void UsualMove()
        {
            var move = new Move("e2".ParseCoordinate(), "e4".ParseCoordinate());
            

            move.FromCell.Should().Be("e2".ParseCoordinate());
            move.ToCell.Should().Be("e4".ParseCoordinate());

            move.PromoteTo.Should().Be(PieceType.None);
        }

        [Fact]
        public void PromotionMove()
        {
            var move = new Move("e2".ParseCoordinate(),
                "e4".ParseCoordinate(), PieceType.Queen);

            move.FromCell.Should().Be("e2".ParseCoordinate());
            move.ToCell.Should().Be("e4".ParseCoordinate());

            move.PromoteTo.Should().Be(PieceType.Queen);
        }

        [Fact]
        public void Parse()
        {
            var position = Move.Parse("a1-b1");
            position.FromCell.Should().Be("a1".ParseCoordinate());
            position.ToCell.Should().Be("b1".ParseCoordinate());

            position = Move.Parse("h4-g8");
            position.FromCell.Should().Be("h4".ParseCoordinate());
            position.ToCell.Should().Be("g8".ParseCoordinate());

            new Action(() => Move.Parse(null)).ShouldThrow<ArgumentException>();
            new Action(() => Move.Parse("")).ShouldThrow<ArgumentException>();
            new Action(() => Move.Parse("1-")).ShouldThrow<ArgumentOutOfRangeException>();
            new Action(() => Move.Parse("a-11")).ShouldThrow<ArgumentOutOfRangeException>();
            new Action(() => Move.Parse("!-1")).ShouldThrow<ArgumentOutOfRangeException>();
            new Action(() => Move.Parse("a0-b2")).ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact(Skip = "Not sure if the feature is needed yet")]
        public void Equality()
        {
            Assert.False(Move.Parse("b2-h1").Equals(Move.Parse("b3-h1")));
            Assert.True(Move.Parse("b2-h1").Equals((object) Move.Parse("b2-h1")));
            Assert.False(Move.Parse("h1-b2") == Move.Parse("a1-h1"));
            Assert.True(Move.Parse("h1-d3") != Move.Parse("a1-d3"));
            Assert.False(Move.Parse("b2-h1").Equals(new object()));

            // TODO:		Assert.IsFalse(Move.Parse("d3-h1").Equals(null));
        }

        [Fact(Skip = "Not sure if the feature is needed yet")]
        public void HashCode()
        {
            Assert.Equal(
                Move.Parse("a1-b1").GetHashCode(),
                Move.Parse("a1-b1").GetHashCode());

            Assert.NotEqual(
                Move.Parse("a1-b1").GetHashCode(),
                Move.Parse("b1-a1").GetHashCode());
        }
    }
}