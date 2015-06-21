using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ChessKit.ChessLogic.UnitTests
{
  [TestFixture]
  public class PositionTest
  {
    [Test]
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
    [Test]
    public void FileAndRank()
    {
      const int position = 3 + 16 * 1;
      position.GetFile().Should().Be('d');
      position.GetRank().Should().Be(2);
      position.ToSquareString().Should().Be("d2");
    }
    [Test]
    public void Parse()
    {
      var position = X.Parse("a1");
      position.GetX().Should().Be(0);
      position.GetY().Should().Be(0);

      position = X.Parse("h4");
      position.GetX().Should().Be(7);
      position.GetY().Should().Be(3);

      position = X.Parse("B7");
      position.GetX().Should().Be(1);
      position.GetY().Should().Be(6);

      new Action(() => X.Parse(null) ).ShouldThrow<ArgumentNullException      >();
      new Action(() => X.Parse("")   ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => X.Parse("1")  ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => X.Parse("a11")).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => X.Parse("!1") ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => X.Parse("a0") ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => X.Parse("1a") ).ShouldThrow<FormatException            >();
      new Action(() => X.Parse("1a") ).ShouldThrow<FormatException            >();
      new Action(() => X.Parse("a9") ).ShouldThrow<ArgumentOutOfRangeException>();
      new Action(() => X.Parse("i8") ).ShouldThrow<ArgumentOutOfRangeException>();
    }
    [Test]
    public void Equality()
    {
      Assert.IsFalse(X.Parse("b2").Equals(X.Parse("a1")));
      Assert.IsTrue(X.Parse("b2").Equals((object)X.Parse("b2")));
      Assert.IsFalse(X.Parse("h1") == X.Parse("a1"));
      Assert.IsTrue(X.Parse("h1") != X.Parse("a1"));
      Assert.IsFalse(X.Parse("b2").Equals(new object()));
      Assert.IsFalse(X.Parse("d3").Equals(null));
    }

    [Test]
    public void OnBoard()
    {
      Assert.AreEqual(64, X.All.Count());
    }
  }
}