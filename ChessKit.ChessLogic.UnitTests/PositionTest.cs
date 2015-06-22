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
      position.ToCoordinateString().Should().Be("d2");
    }
    [Fact]
    public void Parse()
    {
      var position = "a1".ParseCoordinate();
      position.GetX().Should().Be(0);
      position.GetY().Should().Be(0);

      position = "h4".ParseCoordinate();
      position.GetX().Should().Be(7);
      position.GetY().Should().Be(3);

      position = "B7".ParseCoordinate();
      position.GetX().Should().Be(1);
      position.GetY().Should().Be(6);

      new Action(() => CoordinateExtensions.ParseCoordinate(null) ).ShouldThrow<ArgumentNullException      >();
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
      Assert.Equal(64, CoordinateExtensions.All.Count());
    }
  }
}