using System;
using System.Linq;

using ChessKit.ChessLogic;

using NUnit.Framework;

using FluentAssertions;

namespace UnitTests.ChessLogic
{
  [TestFixture]
  public class PositionTest
  {
    [Test]
    public void Ctor()
    {
      var position = new Position();
      position.X.Should().Be(0);
      position.Y.Should().Be(0);

      position = new Position(3, 6);
      position.X.Should().Be(3);
      position.Y.Should().Be(6);

      // BUG: Romoved assertions
//      new Action(() => new Position(-1, 0)).ShouldThrow<ArgumentOutOfRangeException>();
//      new Action(() => new Position(1, -1)).ShouldThrow<ArgumentOutOfRangeException>();
//      new Action(() => new Position(8, 1)).ShouldThrow<ArgumentOutOfRangeException>();
//      new Action(() => new Position(1, 8)).ShouldThrow<ArgumentOutOfRangeException>();
    }
    [Test]
    public void FileAndRank()
    {
      var position = new Position(3, 1);
      position.File.Should().Be("d");
      position.Rank.Should().Be(2);
      position.ToString().Should().Be("d2");
    }
    [Test]
    public void Parse()
    {
      var position = Position.Parse("a1");
      position.X.Should().Be(0);
      position.Y.Should().Be(0);

      position = Position.Parse("h4");
      position.X.Should().Be(7);
      position.Y.Should().Be(3);

      position = Position.Parse("B7");
      position.X.Should().Be(1);
      position.Y.Should().Be(6);

      new Action(() => Position.Parse(null) ).ShouldThrow<ArgumentNullException      >();
      new Action(() => Position.Parse("")   ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => Position.Parse("1")  ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => Position.Parse("a11")).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => Position.Parse("!1") ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => Position.Parse("a0") ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => Position.Parse("1a") ).ShouldThrow<FormatException            >();
      new Action(() => Position.Parse("1a") ).ShouldThrow<FormatException            >();
      new Action(() => Position.Parse("a9") ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => Position.Parse("i8") ).ShouldThrow<ArgumentOutOfRangeException>();
    }
    [Test]
    public void Equality()
    {
      Assert.IsFalse(Position.Parse("b2").Equals(Position.Parse("a1")));
      Assert.IsTrue(Position.Parse("b2").Equals((object)Position.Parse("b2")));
      Assert.IsFalse(Position.Parse("h1") == Position.Parse("a1"));
      Assert.IsTrue(Position.Parse("h1") != Position.Parse("a1"));
      Assert.IsFalse(Position.Parse("b2").Equals(new object()));
      Assert.IsFalse(Position.Parse("d3").Equals(null));
    }
    [Test]
    public void HashCode()
    {
      Assert.AreEqual(
        new Position(1, 3).GetHashCode(),
        new Position(1, 3).GetHashCode());

      Assert.AreNotEqual(
        new Position(1, 3).GetHashCode(),
        new Position(3, 1).GetHashCode());
    }

    [Test]
    public void OnBoard()
    {
      Assert.AreEqual(64, Position.All.Count());
    }
  }
}