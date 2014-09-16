 
 
namespace ChessKit.ChessLogic
{
  partial class Board
  {
    private MoveHints ValidateWhiteBishopMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      if (dx % 17 == 0) 
      {
        var steps = dx / 17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
            if (i == toSquare) return MoveHints.Bishop;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Bishop | MoveHints.DoesNotJump;
      }
      if (dx % -15 == 0) 
      {
        var steps = dx / -15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
            if (i == toSquare) return MoveHints.Bishop;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Bishop | MoveHints.DoesNotJump;
      }
      if (dx % -17 == 0) 
      {
        var steps = dx / -17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
            if (i == toSquare) return MoveHints.Bishop;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Bishop | MoveHints.DoesNotJump;
      }
      if (dx % 15 == 0) 
      {
        var steps = dx / 15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
            if (i == toSquare) return MoveHints.Bishop;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Bishop | MoveHints.DoesNotJump;
      }
          return MoveHints.Bishop | MoveHints.DoesNotMoveThisWay;
    }
    private static MoveHints ValidateWhiteKnightMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      switch (dx)
      {      
        case 33:
          return MoveHints.Knight;
        case 31:
          return MoveHints.Knight;
        case -31:
          return MoveHints.Knight;
        case -33:
          return MoveHints.Knight;
        case 18:
          return MoveHints.Knight;
        case 14:
          return MoveHints.Knight;
        case -14:
          return MoveHints.Knight;
        case -18:
          return MoveHints.Knight;
      }
	  return MoveHints.Knight | MoveHints.DoesNotMoveThisWay;
    }
    private MoveHints ValidateWhiteRookMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      if (dx % 16 == 0) 
      {
        var steps = dx / 16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
            if (i == toSquare) return MoveHints.Rook;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Rook | MoveHints.DoesNotJump;
      }
      if (dx % 1 == 0) 
      {
        var steps = dx / 1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
            if (i == toSquare) return MoveHints.Rook;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Rook | MoveHints.DoesNotJump;
      }
      if (dx % -16 == 0) 
      {
        var steps = dx / -16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
            if (i == toSquare) return MoveHints.Rook;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Rook | MoveHints.DoesNotJump;
      }
      if (dx % -1 == 0) 
      {
        var steps = dx / -1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
            if (i == toSquare) return MoveHints.Rook;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Rook | MoveHints.DoesNotJump;
      }
          return MoveHints.Rook | MoveHints.DoesNotMoveThisWay;
    }
    private MoveHints ValidateWhiteQueenMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      if (dx % 16 == 0) 
      {
        var steps = dx / 16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % 1 == 0) 
      {
        var steps = dx / 1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % -16 == 0) 
      {
        var steps = dx / -16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % -1 == 0) 
      {
        var steps = dx / -1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % 17 == 0) 
      {
        var steps = dx / 17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % -15 == 0) 
      {
        var steps = dx / -15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % -17 == 0) 
      {
        var steps = dx / -17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % 15 == 0) 
      {
        var steps = dx / 15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
          return MoveHints.Queen | MoveHints.DoesNotMoveThisWay;
    }
    private static MoveHints ValidateWhiteKingMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      switch (dx)
      {      
        case 16:
          return MoveHints.King;
        case 17:
          return MoveHints.King;
        case 1:
          return MoveHints.King;
        case -15:
          return MoveHints.King;
        case -16:
          return MoveHints.King;
        case -17:
          return MoveHints.King;
        case -1:
          return MoveHints.King;
        case 15:
          return MoveHints.King;
      }
	  return MoveHints.King | MoveHints.DoesNotMoveThisWay;
    }
    private MoveHints ValidateBlackBishopMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      if (dx % 17 == 0) 
      {
        var steps = dx / 17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
            if (i == toSquare) return MoveHints.Bishop;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Bishop | MoveHints.DoesNotJump;
      }
      if (dx % -15 == 0) 
      {
        var steps = dx / -15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
            if (i == toSquare) return MoveHints.Bishop;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Bishop | MoveHints.DoesNotJump;
      }
      if (dx % -17 == 0) 
      {
        var steps = dx / -17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
            if (i == toSquare) return MoveHints.Bishop;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Bishop | MoveHints.DoesNotJump;
      }
      if (dx % 15 == 0) 
      {
        var steps = dx / 15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
            if (i == toSquare) return MoveHints.Bishop;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Bishop | MoveHints.DoesNotJump;
      }
          return MoveHints.Bishop | MoveHints.DoesNotMoveThisWay;
    }
    private static MoveHints ValidateBlackKnightMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      switch (dx)
      {      
        case 33:
          return MoveHints.Knight;
        case 31:
          return MoveHints.Knight;
        case -31:
          return MoveHints.Knight;
        case -33:
          return MoveHints.Knight;
        case 18:
          return MoveHints.Knight;
        case 14:
          return MoveHints.Knight;
        case -14:
          return MoveHints.Knight;
        case -18:
          return MoveHints.Knight;
      }
	  return MoveHints.Knight | MoveHints.DoesNotMoveThisWay;
    }
    private MoveHints ValidateBlackRookMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      if (dx % 16 == 0) 
      {
        var steps = dx / 16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
            if (i == toSquare) return MoveHints.Rook;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Rook | MoveHints.DoesNotJump;
      }
      if (dx % 1 == 0) 
      {
        var steps = dx / 1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
            if (i == toSquare) return MoveHints.Rook;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Rook | MoveHints.DoesNotJump;
      }
      if (dx % -16 == 0) 
      {
        var steps = dx / -16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
            if (i == toSquare) return MoveHints.Rook;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Rook | MoveHints.DoesNotJump;
      }
      if (dx % -1 == 0) 
      {
        var steps = dx / -1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
            if (i == toSquare) return MoveHints.Rook;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Rook | MoveHints.DoesNotJump;
      }
          return MoveHints.Rook | MoveHints.DoesNotMoveThisWay;
    }
    private MoveHints ValidateBlackQueenMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      if (dx % 16 == 0) 
      {
        var steps = dx / 16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % 1 == 0) 
      {
        var steps = dx / 1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % -16 == 0) 
      {
        var steps = dx / -16;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % -1 == 0) 
      {
        var steps = dx / -1;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % 17 == 0) 
      {
        var steps = dx / 17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % -15 == 0) 
      {
        var steps = dx / -15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % -17 == 0) 
      {
        var steps = dx / -17;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
      if (dx % 15 == 0) 
      {
        var steps = dx / 15;
        if (steps >= 0 && steps < 8) 
            for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
            if (i == toSquare) return MoveHints.Queen;
            else if (this[i] != CompactPiece.EmptyCell) 
			  return MoveHints.Queen | MoveHints.DoesNotJump;
      }
          return MoveHints.Queen | MoveHints.DoesNotMoveThisWay;
    }
    private static MoveHints ValidateBlackKingMove(int fromSquare, int toSquare)
    {
	  var dx = toSquare - fromSquare;
      switch (dx)
      {      
        case 16:
          return MoveHints.King;
        case 17:
          return MoveHints.King;
        case 1:
          return MoveHints.King;
        case -15:
          return MoveHints.King;
        case -16:
          return MoveHints.King;
        case -17:
          return MoveHints.King;
        case -1:
          return MoveHints.King;
        case 15:
          return MoveHints.King;
      }
	  return MoveHints.King | MoveHints.DoesNotMoveThisWay;
    }
  
    public MoveHints ValidateMove(CompactPiece piece, int fromSquare, int toSquare, CompactPiece toPiece, CastlingAvailability castlingAvailability)
    {
	  switch (piece)
      {
        case CompactPiece.WhitePawn:
          return ValidateWhitePawnMove(fromSquare, toSquare, toPiece);

        case CompactPiece.WhiteBishop:
          return ValidateWhiteBishopMove(fromSquare, toSquare);

        case CompactPiece.WhiteKnight:
          return ValidateWhiteKnightMove(fromSquare, toSquare);

        case CompactPiece.WhiteRook:
          return ValidateWhiteRookMove(fromSquare, toSquare);

        case CompactPiece.WhiteQueen:
          return ValidateWhiteQueenMove(fromSquare, toSquare);

        case CompactPiece.WhiteKing:
          if (ValidateWhiteKingMove(fromSquare, toSquare) == MoveHints.King)
            return MoveHints.King;
		  return ValidateWhiteCastlingMove(fromSquare, toSquare, castlingAvailability);

        case CompactPiece.BlackPawn:
          return ValidateBlackPawnMove(fromSquare, toSquare, toPiece);

        case CompactPiece.BlackBishop:
          return ValidateBlackBishopMove(fromSquare, toSquare);

        case CompactPiece.BlackKnight:
          return ValidateBlackKnightMove(fromSquare, toSquare);

        case CompactPiece.BlackRook:
          return ValidateBlackRookMove(fromSquare, toSquare);

        case CompactPiece.BlackQueen:
          return ValidateBlackQueenMove(fromSquare, toSquare);

        case CompactPiece.BlackKing:
          if (ValidateBlackKingMove(fromSquare, toSquare) == MoveHints.King)
            return MoveHints.King;
		  return ValidateBlackCastlingMove(fromSquare, toSquare, castlingAvailability);

        default: throw new System.InvalidOperationException("Unknown piece: " + piece);
	  }
    }  
  }
}