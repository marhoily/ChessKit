using System;
using ChessKit.ChessLogic.Enums;
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
            var move = new Move(CoordinateExtensions.Parse("e2"), CoordinateExtensions.Parse("e4"))
            {
                Annotations = MoveAnnotations.Rook
            };

            move.From.Should().Be(CoordinateExtensions.Parse("e2"));
            move.To.Should().Be(CoordinateExtensions.Parse("e4"));

            move.ProposedPromotion.Should().Be(PieceType.Queen);

            move.Annotations.Should().Be(MoveAnnotations.Rook);
        }

        [Fact]
        public void PromotionMove()
        {
            var move = new Move(CoordinateExtensions.Parse("e2"), CoordinateExtensions.Parse("e4"), PieceType.Queen)
            {
                Annotations = MoveAnnotations.Promotion
            };


            move.From.Should().Be(CoordinateExtensions.Parse("e2"));
            move.To.Should().Be(CoordinateExtensions.Parse("e4"));

            move.ProposedPromotion.Should().Be(PieceType.Queen);

            move.Annotations.Should().Be(MoveAnnotations.Promotion);
        }

        [Fact]
        public void Parse()
        {
            var position = Move.Parse("a1-b1");
            position.From.Should().Be(CoordinateExtensions.Parse("a1"));
            position.To.Should().Be(CoordinateExtensions.Parse("b1"));

            position = Move.Parse("h4-g8");
            position.From.Should().Be(CoordinateExtensions.Parse("h4"));
            position.To.Should().Be(CoordinateExtensions.Parse("g8"));

            new Action(() => Move.Parse(null)).ShouldThrow<ArgumentException>();
            new Action(() => Move.Parse("")).ShouldThrow<ArgumentException>();
            new Action(() => Move.Parse("1-")).ShouldThrow<ArgumentOutOfRangeException>();
            new Action(() => Move.Parse("a-11")).ShouldThrow<ArgumentOutOfRangeException>();
            new Action(() => Move.Parse("!-1")).ShouldThrow<ArgumentOutOfRangeException>();
            new Action(() => Move.Parse("a0-b2")).ShouldThrow<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Equality()
        {
            Assert.False(Move.Parse("b2-h1").Equals(Move.Parse("b3-h1")));
            Assert.True(Move.Parse("b2-h1").Equals((object) Move.Parse("b2-h1")));
            Assert.False(Move.Parse("h1-b2") == Move.Parse("a1-h1"));
            Assert.True(Move.Parse("h1-d3") != Move.Parse("a1-d3"));
            Assert.False(Move.Parse("b2-h1").Equals(new object()));

            // TODO:		Assert.IsFalse(Move.Parse("d3-h1").Equals(null));
        }

        [Fact]
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