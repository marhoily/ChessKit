using System;
using System.Linq;
using ChessKit.ChessLogic.Primitives;
using FluentAssertions;
using Xunit;

namespace ChessKit.ChessLogic.UnitTests
{
  public class CoordinateTests
  {
    [Fact]
    public void Ctor()
    {
      var coordinate = new int();
      coordinate.GetX().Should().Be(0);
      coordinate.GetY().Should().Be(0);

      coordinate = 3 + 16* 6;
      coordinate.GetX().Should().Be(3);
      coordinate.GetY().Should().Be(6);

      // BUG: Romoved assertions
//      new Action(() => new int(-1, 0)).ShouldThrow<ArgumentOutOfRangeException>();
//      new Action(() => new int(1, -1)).ShouldThrow<ArgumentOutOfRangeException>();
//      new Action(() => new int(8, 1)).ShouldThrow<ArgumentOutOfRangeException>();
//      new Action(() => new int(1, 8)).ShouldThrow<ArgumentOutOfRangeException>();
    }
    [Fact]
    public void FileAndRank()
    {
      const int coordinate = 3 + 16 * 1;
      coordinate.GetFile().Should().Be('d');
      coordinate.GetRank().Should().Be(2);
      coordinate.ToCoordinateString().Should().Be("d2");
    }
    [Fact]
    public void Parse()
    {
      var coordinate = "a1".ParseCoordinate();
      coordinate.GetX().Should().Be(0);
      coordinate.GetY().Should().Be(0);

      coordinate = "h4".ParseCoordinate();
      coordinate.GetX().Should().Be(7);
      coordinate.GetY().Should().Be(3);

      coordinate = "B7".ParseCoordinate();
      coordinate.GetX().Should().Be(1);
      coordinate.GetY().Should().Be(6);

      new Action(() => Coordinates.ParseCoordinate(null) ).ShouldThrow<ArgumentNullException      >();
      new Action(() => "".ParseCoordinate()   ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => "1".ParseCoordinate()  ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => "a11".ParseCoordinate()).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => "!1".ParseCoordinate() ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => "a0".ParseCoordinate() ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => "1a".ParseCoordinate() ).ShouldThrow<FormatException            >();
      new Action(() => "1a".ParseCoordinate() ).ShouldThrow<FormatException            >();
      new Action(() => "a9".ParseCoordinate() ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => "i8".ParseCoordinate() ).ShouldThrow<ArgumentOutOfRangeException>();
    }
    [Fact]
    public void Equality()
    {
      Assert.False("b2".ParseCoordinate().Equals("a1".ParseCoordinate()));
      Assert.True("b2".ParseCoordinate().Equals((object)"b2".ParseCoordinate()));
      Assert.False("h1".ParseCoordinate() == "a1".ParseCoordinate());
      Assert.True("h1".ParseCoordinate() != "a1".ParseCoordinate());
      Assert.False("b2".ParseCoordinate().Equals(new object()));
      Assert.False("d3".ParseCoordinate().Equals(null));
    }

    [Fact]
    public void OnBoard()
    {
      Assert.Equal(64, Coordinates.All.Count());
    }
  }
}