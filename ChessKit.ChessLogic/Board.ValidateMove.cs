using ChessKit.ChessLogic.Enums;
using A = ChessKit.ChessLogic.Enums.MoveAnnotations;
using static ChessKit.ChessLogic.Enums.MoveAnnotations;

namespace ChessKit.ChessLogic
{
  partial class Board
  {
    private A ValidateWhiteBishopMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      if (dx % 17 == 0) 
      {
        var steps = dx / 17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
            if (i == toSquare) return A.Bishop;
            else if (this[i] != Piece.EmptyCell) return A.Bishop | A.DoesNotJump;
      }
      if (dx % -15 == 0) 
      {
        var steps = dx / -15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
            if (i == toSquare) return A.Bishop;
            else if (this[i] != Piece.EmptyCell) return A.Bishop | A.DoesNotJump;
      }
      if (dx % -17 == 0) 
      {
        var steps = dx / -17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
            if (i == toSquare) return A.Bishop;
            else if (this[i] != Piece.EmptyCell) return A.Bishop | A.DoesNotJump;
      }
      if (dx % 15 == 0) 
      {
        var steps = dx / 15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
            if (i == toSquare) return A.Bishop;
            else if (this[i] != Piece.EmptyCell) return A.Bishop | A.DoesNotJump;
      }
          return A.Bishop | A.DoesNotMoveThisWay;
    }
    private static A ValidateWhiteKnightMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      switch (dx)
      {      
        case 33: return A.Knight;
        case 31: return A.Knight;
        case -31: return A.Knight;
        case -33: return A.Knight;
        case 18: return A.Knight;
        case 14: return A.Knight;
        case -14: return A.Knight;
        case -18: return A.Knight;
      }
	  return A.Knight | A.DoesNotMoveThisWay;
    }
    private A ValidateWhiteRookMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      if (dx % 16 == 0) 
      {
        var steps = dx / 16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
            if (i == toSquare) return A.Rook;
            else if (this[i] != Piece.EmptyCell) return A.Rook | A.DoesNotJump;
      }
      if (dx % 1 == 0) 
      {
        var steps = dx / 1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
            if (i == toSquare) return A.Rook;
            else if (this[i] != Piece.EmptyCell) return A.Rook | A.DoesNotJump;
      }
      if (dx % -16 == 0) 
      {
        var steps = dx / -16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
            if (i == toSquare) return A.Rook;
            else if (this[i] != Piece.EmptyCell) return A.Rook | A.DoesNotJump;
      }
      if (dx % -1 == 0) 
      {
        var steps = dx / -1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
            if (i == toSquare) return A.Rook;
            else if (this[i] != Piece.EmptyCell) return A.Rook | A.DoesNotJump;
      }
          return A.Rook | A.DoesNotMoveThisWay;
    }
    private A ValidateWhiteQueenMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      if (dx % 16 == 0) 
      {
        var steps = dx / 16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % 1 == 0) 
      {
        var steps = dx / 1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % -16 == 0) 
      {
        var steps = dx / -16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % -1 == 0) 
      {
        var steps = dx / -1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % 17 == 0) 
      {
        var steps = dx / 17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % -15 == 0) 
      {
        var steps = dx / -15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % -17 == 0) 
      {
        var steps = dx / -17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % 15 == 0) 
      {
        var steps = dx / 15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
          return A.Queen | A.DoesNotMoveThisWay;
    }
    private static A ValidateWhiteKingMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      switch (dx)
      {      
        case 16: return A.King;
        case 17: return A.King;
        case 1: return A.King;
        case -15: return A.King;
        case -16: return A.King;
        case -17: return A.King;
        case -1: return A.King;
        case 15: return A.King;
      }
	  return A.King | A.DoesNotMoveThisWay;
    }
    private A ValidateBlackBishopMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      if (dx % 17 == 0) 
      {
        var steps = dx / 17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
            if (i == toSquare) return A.Bishop;
            else if (this[i] != Piece.EmptyCell) return A.Bishop | A.DoesNotJump;
      }
      if (dx % -15 == 0) 
      {
        var steps = dx / -15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
            if (i == toSquare) return A.Bishop;
            else if (this[i] != Piece.EmptyCell) return A.Bishop | A.DoesNotJump;
      }
      if (dx % -17 == 0) 
      {
        var steps = dx / -17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
            if (i == toSquare) return A.Bishop;
            else if (this[i] != Piece.EmptyCell) return A.Bishop | A.DoesNotJump;
      }
      if (dx % 15 == 0) 
      {
        var steps = dx / 15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
            if (i == toSquare) return A.Bishop;
            else if (this[i] != Piece.EmptyCell) return A.Bishop | A.DoesNotJump;
      }
          return A.Bishop | A.DoesNotMoveThisWay;
    }
    private static A ValidateBlackKnightMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      switch (dx)
      {      
        case 33: return A.Knight;
        case 31: return A.Knight;
        case -31: return A.Knight;
        case -33: return A.Knight;
        case 18: return A.Knight;
        case 14: return A.Knight;
        case -14: return A.Knight;
        case -18: return A.Knight;
      }
	  return A.Knight | A.DoesNotMoveThisWay;
    }
    private A ValidateBlackRookMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      if (dx % 16 == 0) 
      {
        var steps = dx / 16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
            if (i == toSquare) return A.Rook;
            else if (this[i] != Piece.EmptyCell) return A.Rook | A.DoesNotJump;
      }
      if (dx % 1 == 0) 
      {
        var steps = dx / 1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
            if (i == toSquare) return A.Rook;
            else if (this[i] != Piece.EmptyCell) return A.Rook | A.DoesNotJump;
      }
      if (dx % -16 == 0) 
      {
        var steps = dx / -16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
            if (i == toSquare) return A.Rook;
            else if (this[i] != Piece.EmptyCell) return A.Rook | A.DoesNotJump;
      }
      if (dx % -1 == 0) 
      {
        var steps = dx / -1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
            if (i == toSquare) return A.Rook;
            else if (this[i] != Piece.EmptyCell) return A.Rook | A.DoesNotJump;
      }
          return A.Rook | A.DoesNotMoveThisWay;
    }
    private A ValidateBlackQueenMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      if (dx % 16 == 0) 
      {
        var steps = dx / 16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % 1 == 0) 
      {
        var steps = dx / 1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % -16 == 0) 
      {
        var steps = dx / -16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % -1 == 0) 
      {
        var steps = dx / -1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % 17 == 0) 
      {
        var steps = dx / 17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % -15 == 0) 
      {
        var steps = dx / -15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % -17 == 0) 
      {
        var steps = dx / -17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
      if (dx % 15 == 0) 
      {
        var steps = dx / 15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
            if (i == toSquare) return A.Queen;
            else if (this[i] != Piece.EmptyCell) return A.Queen | A.DoesNotJump;
      }
          return A.Queen | A.DoesNotMoveThisWay;
    }
    private static A ValidateBlackKingMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      switch (dx)
      {      
        case 16: return A.King;
        case 17: return A.King;
        case 1: return A.King;
        case -15: return A.King;
        case -16: return A.King;
        case -17: return A.King;
        case -1: return A.King;
        case 15: return A.King;
      }
	  return A.King | A.DoesNotMoveThisWay;
    }
  
    private A ValidateMove(Piece piece, int fromSquare, int toSquare, Piece toPiece, Castlings castlingAvailability)
    {
	  switch (piece)
      {
        case Piece.WhitePawn:
          return ValidateWhitePawnMove(fromSquare, toSquare, toPiece);

        case Piece.WhiteBishop:
          return ValidateWhiteBishopMove(fromSquare, toSquare);

        case Piece.WhiteKnight:
          return ValidateWhiteKnightMove(fromSquare, toSquare);

        case Piece.WhiteRook:
          return ValidateWhiteRookMove(fromSquare, toSquare);

        case Piece.WhiteQueen:
          return ValidateWhiteQueenMove(fromSquare, toSquare);

        case Piece.WhiteKing:
          if (ValidateWhiteKingMove(fromSquare, toSquare) == A.King)
            return A.King;
		  return ValidateWhiteCastlingMove(fromSquare, toSquare, castlingAvailability);

        case Piece.BlackPawn:
          return ValidateBlackPawnMove(fromSquare, toSquare, toPiece);

        case Piece.BlackBishop:
          return ValidateBlackBishopMove(fromSquare, toSquare);

        case Piece.BlackKnight:
          return ValidateBlackKnightMove(fromSquare, toSquare);

        case Piece.BlackRook:
          return ValidateBlackRookMove(fromSquare, toSquare);

        case Piece.BlackQueen:
          return ValidateBlackQueenMove(fromSquare, toSquare);

        case Piece.BlackKing:
          if (ValidateBlackKingMove(fromSquare, toSquare) == A.King)
            return A.King;
		  return ValidateBlackCastlingMove(fromSquare, toSquare, castlingAvailability);

        default: throw new System.InvalidOperationException("Unknown piece: " + piece);
	  }
    }  
  }
}