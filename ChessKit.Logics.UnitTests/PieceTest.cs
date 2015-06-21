using FluentAssertions;
using NUnit.Framework;

namespace ChessKit.ChessLogic.UnitTests
{
  [TestFixture]
  public class PieceTest
  {
    [Test]
    public void Pack()
    {
      Assert.AreEqual(CompactPiece.WhitePawn  , Piece.Pack(Piece.WhitePawn));
      Assert.AreEqual(CompactPiece.WhiteBishop, Piece.Pack(Piece.WhiteBishop));
      Assert.AreEqual(CompactPiece.WhiteKnight, Piece.Pack(Piece.WhiteKnight));
      Assert.AreEqual(CompactPiece.WhiteRook  , Piece.Pack(Piece.WhiteRook));
      Assert.AreEqual(CompactPiece.WhiteQueen , Piece.Pack(Piece.WhiteQueen));
      Assert.AreEqual(CompactPiece.WhiteKing  , Piece.Pack(Piece.WhiteKing));
      
      Assert.AreEqual(CompactPiece.BlackPawn  , Piece.Pack(Piece.BlackPawn));
      Assert.AreEqual(CompactPiece.BlackBishop, Piece.Pack(Piece.BlackBishop));
      Assert.AreEqual(CompactPiece.BlackKnight, Piece.Pack(Piece.BlackKnight));
      Assert.AreEqual(CompactPiece.BlackRook  , Piece.Pack(Piece.BlackRook));
      Assert.AreEqual(CompactPiece.BlackQueen , Piece.Pack(Piece.BlackQueen));
      Assert.AreEqual(CompactPiece.BlackKing  , Piece.Pack(Piece.BlackKing));
    }
    [Test]
    public void Unpack()
    {
      Assert.AreEqual(null, Piece.Unpack(CompactPiece.EmptyCell));

      Assert.AreEqual(Piece.WhitePawn, Piece.Unpack(CompactPiece.WhitePawn));
      Assert.AreEqual(Piece.WhiteBishop, Piece.Unpack(CompactPiece.WhiteBishop));
      Assert.AreEqual(Piece.WhiteKnight, Piece.Unpack(CompactPiece.WhiteKnight));
      Assert.AreEqual(Piece.WhiteRook, Piece.Unpack(CompactPiece.WhiteRook));
      Assert.AreEqual(Piece.WhiteQueen, Piece.Unpack(CompactPiece.WhiteQueen));
      Assert.AreEqual(Piece.WhiteKing, Piece.Unpack(CompactPiece.WhiteKing));

      Assert.AreEqual(Piece.BlackPawn, Piece.Unpack(CompactPiece.BlackPawn));
      Assert.AreEqual(Piece.BlackBishop, Piece.Unpack(CompactPiece.BlackBishop));
      Assert.AreEqual(Piece.BlackKnight, Piece.Unpack(CompactPiece.BlackKnight));
      Assert.AreEqual(Piece.BlackRook, Piece.Unpack(CompactPiece.BlackRook));
      Assert.AreEqual(Piece.BlackQueen, Piece.Unpack(CompactPiece.BlackQueen));
      Assert.AreEqual(Piece.BlackKing, Piece.Unpack(CompactPiece.BlackKing));
    }
    [Test]
    public void Same()
    {
      Assert.AreEqual(Piece.WhitePawn, Piece.WhitePawn);
      Assert.AreEqual(Piece.WhiteBishop, Piece.WhiteBishop);
      Assert.AreEqual(Piece.WhiteKnight, Piece.WhiteKnight);
      Assert.AreEqual(Piece.WhiteRook, Piece.WhiteRook);
      Assert.AreEqual(Piece.WhiteQueen, Piece.WhiteQueen);
      Assert.AreEqual(Piece.WhiteKing, Piece.WhiteKing);

      Assert.AreEqual(Piece.BlackPawn, Piece.BlackPawn);
      Assert.AreEqual(Piece.BlackBishop, Piece.BlackBishop);
      Assert.AreEqual(Piece.BlackKnight, Piece.BlackKnight);
      Assert.AreEqual(Piece.BlackRook, Piece.BlackRook);
      Assert.AreEqual(Piece.BlackQueen, Piece.BlackQueen);
      Assert.AreEqual(Piece.BlackKing, Piece.BlackKing);
    }
    [Test]
    public void Get()
    {
      Piece.Get(PieceType.Pawn  , PieceColor.White).Should().Be(Piece.WhitePawn);
      Piece.Get(PieceType.Bishop, PieceColor.White).Should().Be(Piece.WhiteBishop);
      Piece.Get(PieceType.Knight, PieceColor.White).Should().Be(Piece.WhiteKnight);
      Piece.Get(PieceType.Rook  , PieceColor.White).Should().Be(Piece.WhiteRook);
      Piece.Get(PieceType.Queen , PieceColor.White).Should().Be(Piece.WhiteQueen);
      Piece.Get(PieceType.King  , PieceColor.White).Should().Be(Piece.WhiteKing);

      Piece.Get(PieceType.Pawn  , PieceColor.Black).Should().Be(Piece.BlackPawn);
      Piece.Get(PieceType.Bishop, PieceColor.Black).Should().Be(Piece.BlackBishop);
      Piece.Get(PieceType.Knight, PieceColor.Black).Should().Be(Piece.BlackKnight);
      Piece.Get(PieceType.Rook  , PieceColor.Black).Should().Be(Piece.BlackRook);
      Piece.Get(PieceType.Queen , PieceColor.Black).Should().Be(Piece.BlackQueen);
      Piece.Get(PieceType.King  , PieceColor.Black).Should().Be(Piece.BlackKing);
    }
    [Test]
    public void PiecesContent()
    {
      Piece.WhitePawn    .Symbol.Should().Be("P"); 
      Piece.WhiteBishop  .Symbol.Should().Be("B");
      Piece.WhiteKnight  .Symbol.Should().Be("N");
      Piece.WhiteRook    .Symbol.Should().Be("R");
      Piece.WhiteQueen   .Symbol.Should().Be("Q");
      Piece.WhiteKing    .Symbol.Should().Be("K");
                                   
      Piece.BlackPawn    .Symbol.Should().Be("p");
      Piece.BlackBishop  .Symbol.Should().Be("b");
      Piece.BlackKnight  .Symbol.Should().Be("n");
      Piece.BlackRook    .Symbol.Should().Be("r");
      Piece.BlackQueen   .Symbol.Should().Be("q");
      Piece.BlackKing    .Symbol.Should().Be("k");

      // ------------------------------------------------------
      
      Piece.WhitePawn    .PieceType.Should().Be(PieceType.Pawn  ); 
      Piece.WhiteBishop  .PieceType.Should().Be(PieceType.Bishop);
      Piece.WhiteKnight  .PieceType.Should().Be(PieceType.Knight);
      Piece.WhiteRook    .PieceType.Should().Be(PieceType.Rook  );
      Piece.WhiteQueen   .PieceType.Should().Be(PieceType.Queen );
      Piece.WhiteKing    .PieceType.Should().Be(PieceType.King  );
                         
      Piece.BlackPawn    .PieceType.Should().Be(PieceType.Pawn  );
      Piece.BlackBishop  .PieceType.Should().Be(PieceType.Bishop);
      Piece.BlackKnight  .PieceType.Should().Be(PieceType.Knight);
      Piece.BlackRook    .PieceType.Should().Be(PieceType.Rook  );
      Piece.BlackQueen   .PieceType.Should().Be(PieceType.Queen );
      Piece.BlackKing    .PieceType.Should().Be(PieceType.King  );

      // ------------------------------------------------------
      
      Piece.WhitePawn    .Color.Should().Be(PieceColor.White); 
      Piece.WhiteBishop  .Color.Should().Be(PieceColor.White);
      Piece.WhiteKnight  .Color.Should().Be(PieceColor.White);
      Piece.WhiteRook    .Color.Should().Be(PieceColor.White);
      Piece.WhiteQueen   .Color.Should().Be(PieceColor.White);
      Piece.WhiteKing    .Color.Should().Be(PieceColor.White);
                                                 
      Piece.BlackPawn    .Color.Should().Be(PieceColor.Black);
      Piece.BlackBishop  .Color.Should().Be(PieceColor.Black);
      Piece.BlackKnight  .Color.Should().Be(PieceColor.Black);
      Piece.BlackRook    .Color.Should().Be(PieceColor.Black);
      Piece.BlackQueen   .Color.Should().Be(PieceColor.Black);
      Piece.BlackKing    .Color.Should().Be(PieceColor.Black);
    }
  }
}