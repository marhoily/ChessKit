using System;
using System.Linq;
using ChessKit.ChessLogic.Enums;
using FluentAssertions;
using Xunit;

namespace ChessKit.ChessLogic.UnitTests
{
  public class PositionTest
  {
    [Fact]
    public void Ctor()
    {
      var position = new int();
      position.GetX().Should().Be(0);
      position.GetY().Should().Be(0);

      position = 3 + 16* 6;
      position.GetX().Should().Be(3);
      position.GetY().Should().Be(6);

      // BUG: Romoved assertions
//      new Action(() => new int(-1, 0)).ShouldThrow<ArgumentOutOfRangeException>();
//      new Action(() => new int(1, -1)).ShouldThrow<ArgumentOutOfRangeException>();
//      new Action(() => new int(8, 1)).ShouldThrow<ArgumentOutOfRangeException>();
//      new Action(() => new int(1, 8)).ShouldThrow<ArgumentOutOfRangeException>();
    }
    [Fact]
    public void FileAndRank()
    {
      const int position = 3 + 16 * 1;
      position.GetFile().Should().Be('d');
      position.GetRank().Should().Be(2);
      position.ToSquareString().Should().Be("d2");
    }
    [Fact]
    public void Parse()
    {
      var position = Coordinate.Parse("a1");
      position.GetX().Should().Be(0);
      position.GetY().Should().Be(0);

      position = Coordinate.Parse("h4");
      position.GetX().Should().Be(7);
      position.GetY().Should().Be(3);

      position = Coordinate.Parse("B7");
      position.GetX().Should().Be(1);
      position.GetY().Should().Be(6);

      new Action(() => Coordinate.Parse(null) ).ShouldThrow<ArgumentNullException      >();
      new Action(() => Coordinate.Parse("")   ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => Coordinate.Parse("1")  ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => Coordinate.Parse("a11")).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => Coordinate.Parse("!1") ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => Coordinate.Parse("a0") ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => Coordinate.Parse("1a") ).ShouldThrow<FormatException            >();
      new Action(() => Coordinate.Parse("1a") ).ShouldThrow<FormatException            >();
      new Action(() => Coordinate.Parse("a9") ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => Coordinate.Parse("i8") ).ShouldThrow<ArgumentOutOfRangeException>();
    }
    [Fact]
    public void Equality()
    {
      Assert.False(Coordinate.Parse("b2").Equals(Coordinate.Parse("a1")));
      Assert.True(Coordinate.Parse("b2").Equals((object)Coordinate.Parse("b2")));
      Assert.False(Coordinate.Parse("h1") == Coordinate.Parse("a1"));
      Assert.True(Coordinate.Parse("h1") != Coordinate.Parse("a1"));
      Assert.False(Coordinate.Parse("b2").Equals(new object()));
      Assert.False(Coordinate.Parse("d3").Equals(null));
    }

    [Fact]
    public void OnBoard()
    {
      Assert.Equal(64, Coordinate.All.Count());
    }
  }
}