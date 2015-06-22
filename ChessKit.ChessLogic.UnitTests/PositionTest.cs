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
      var position = CoordinateExtensions.Parse("a1");
      position.GetX().Should().Be(0);
      position.GetY().Should().Be(0);

      position = CoordinateExtensions.Parse("h4");
      position.GetX().Should().Be(7);
      position.GetY().Should().Be(3);

      position = CoordinateExtensions.Parse("B7");
      position.GetX().Should().Be(1);
      position.GetY().Should().Be(6);

      new Action(() => CoordinateExtensions.Parse(null) ).ShouldThrow<ArgumentNullException      >();
      new Action(() => CoordinateExtensions.Parse("")   ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => CoordinateExtensions.Parse("1")  ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => CoordinateExtensions.Parse("a11")).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => CoordinateExtensions.Parse("!1") ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => CoordinateExtensions.Parse("a0") ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => CoordinateExtensions.Parse("1a") ).ShouldThrow<FormatException            >();
      new Action(() => CoordinateExtensions.Parse("1a") ).ShouldThrow<FormatException            >();
      new Action(() => CoordinateExtensions.Parse("a9") ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => CoordinateExtensions.Parse("i8") ).ShouldThrow<ArgumentOutOfRangeException>();
    }
    [Fact]
    public void Equality()
    {
      Assert.False(CoordinateExtensions.Parse("b2").Equals(CoordinateExtensions.Parse("a1")));
      Assert.True(CoordinateExtensions.Parse("b2").Equals((object)CoordinateExtensions.Parse("b2")));
      Assert.False(CoordinateExtensions.Parse("h1") == CoordinateExtensions.Parse("a1"));
      Assert.True(CoordinateExtensions.Parse("h1") != CoordinateExtensions.Parse("a1"));
      Assert.False(CoordinateExtensions.Parse("b2").Equals(new object()));
      Assert.False(CoordinateExtensions.Parse("d3").Equals(null));
    }

    [Fact]
    public void OnBoard()
    {
      Assert.Equal(64, CoordinateExtensions.All.Count());
    }
  }
}