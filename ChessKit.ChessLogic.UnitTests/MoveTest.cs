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
            Action action = () => MoveR.Parse("b1-b1");
            action.ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void UsualMove()
        {
            var move = new MoveR("e2".ParseCoordinate(), "e4".ParseCoordinate());
            

            move.From.Should().Be("e2".ParseCoordinate());
            move.To.Should().Be("e4".ParseCoordinate());

            move.ProposedPromotion.Should().Be(PieceType.None);
        }

        [Fact]
        public void PromotionMove()
        {
            var move = new MoveR("e2".ParseCoordinate(),
                "e4".ParseCoordinate(), PieceType.Queen);

            move.From.Should().Be("e2".ParseCoordinate());
            move.To.Should().Be("e4".ParseCoordinate());

            move.ProposedPromotion.Should().Be(PieceType.Queen);
        }

        [Fact]
        public void Parse()
        {
            var position = MoveR.Parse("a1-b1");
            position.From.Should().Be("a1".ParseCoordinate());
            position.To.Should().Be("b1".ParseCoordinate());

            position = MoveR.Parse("h4-g8");
            position.From.Should().Be("h4".ParseCoordinate());
            position.To.Should().Be("g8".ParseCoordinate());

            new Action(() => MoveR.Parse(null)).ShouldThrow<ArgumentException>();
            new Action(() => MoveR.Parse("")).ShouldThrow<ArgumentException>();
            new Action(() => MoveR.Parse("1-")).ShouldThrow<ArgumentOutOfRangeException>();
            new Action(() => MoveR.Parse("a-11")).ShouldThrow<ArgumentOutOfRangeException>();
            new Action(() => MoveR.Parse("!-1")).ShouldThrow<ArgumentOutOfRangeException>();
            new Action(() => MoveR.Parse("a0-b2")).ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact(Skip = "Not sure if the feature is needed yet")]
        public void Equality()
        {
            Assert.False(MoveR.Parse("b2-h1").Equals(MoveR.Parse("b3-h1")));
            Assert.True(MoveR.Parse("b2-h1").Equals((object) MoveR.Parse("b2-h1")));
            Assert.False(MoveR.Parse("h1-b2") == MoveR.Parse("a1-h1"));
            Assert.True(MoveR.Parse("h1-d3") != MoveR.Parse("a1-d3"));
            Assert.False(MoveR.Parse("b2-h1").Equals(new object()));

            // TODO:		Assert.IsFalse(Move.Parse("d3-h1").Equals(null));
        }

        [Fact(Skip = "Not sure if the feature is needed yet")]
        public void HashCode()
        {
            Assert.Equal(
                MoveR.Parse("a1-b1").GetHashCode(),
                MoveR.Parse("a1-b1").GetHashCode());

            Assert.NotEqual(
                MoveR.Parse("a1-b1").GetHashCode(),
                MoveR.Parse("b1-a1").GetHashCode());
        }
    }
}