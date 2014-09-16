 
 
namespace ChessKit.ChessLogic
{
  partial class Board
  {
    public bool CanBeValidMove(CompactPiece piece, int fromSquare, int toSquare)
    {
	    var dx = toSquare - fromSquare;
	    switch (piece)
      {
        #region ' White Pawn '
        case CompactPiece.WhitePawn:
          switch (dx)
          {      
            case 16:
              return this[toSquare] == CompactPiece.EmptyCell;
            case 17:
              return true;
            case 15:
              return true;
            case 32:
              return this[toSquare] == CompactPiece.EmptyCell;
          }
	      return false;
        #endregion

        #region ' White Bishop '
        case CompactPiece.WhiteBishop:
          if (dx % 17 == 0) 
          {
            var steps = dx / 17;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -15 == 0) 
          {
            var steps = dx / -15;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -17 == 0) 
          {
            var steps = dx / -17;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % 15 == 0) 
          {
            var steps = dx / 15;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          return false;
        #endregion

        #region ' White Knight '
        case CompactPiece.WhiteKnight:
          switch (dx)
          {      
            case 33:
              return true;
            case 31:
              return true;
            case -31:
              return true;
            case -33:
              return true;
            case 18:
              return true;
            case 14:
              return true;
            case -14:
              return true;
            case -18:
              return true;
          }
	      return false;
        #endregion

        #region ' White Rook '
        case CompactPiece.WhiteRook:
          if (dx % 16 == 0) 
          {
            var steps = dx / 16;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % 1 == 0) 
          {
            var steps = dx / 1;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -16 == 0) 
          {
            var steps = dx / -16;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -1 == 0) 
          {
            var steps = dx / -1;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          return false;
        #endregion

        #region ' White Queen '
        case CompactPiece.WhiteQueen:
          if (dx % 16 == 0) 
          {
            var steps = dx / 16;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % 1 == 0) 
          {
            var steps = dx / 1;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -16 == 0) 
          {
            var steps = dx / -16;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -1 == 0) 
          {
            var steps = dx / -1;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % 17 == 0) 
          {
            var steps = dx / 17;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -15 == 0) 
          {
            var steps = dx / -15;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -17 == 0) 
          {
            var steps = dx / -17;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % 15 == 0) 
          {
            var steps = dx / 15;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          return false;
        #endregion

        #region ' White King '
        case CompactPiece.WhiteKing:
          switch (dx)
          {      
            case 16:
              return true;
            case 17:
              return true;
            case 1:
              return true;
            case -15:
              return true;
            case -16:
              return true;
            case -17:
              return true;
            case -1:
              return true;
            case 15:
              return true;
            case 2:
              return this[toSquare] == CompactPiece.EmptyCell;
            case -2:
              return this[toSquare] == CompactPiece.EmptyCell;
          }
	      return false;
        #endregion

        #region ' Black Pawn '
        case CompactPiece.BlackPawn:
          switch (dx)
          {      
            case -16:
              return this[toSquare] == CompactPiece.EmptyCell;
            case -15:
              return true;
            case -17:
              return true;
            case -32:
              return this[toSquare] == CompactPiece.EmptyCell;
          }
	      return false;
        #endregion

        #region ' Black Bishop '
        case CompactPiece.BlackBishop:
          if (dx % 17 == 0) 
          {
            var steps = dx / 17;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -15 == 0) 
          {
            var steps = dx / -15;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -17 == 0) 
          {
            var steps = dx / -17;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % 15 == 0) 
          {
            var steps = dx / 15;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          return false;
        #endregion

        #region ' Black Knight '
        case CompactPiece.BlackKnight:
          switch (dx)
          {      
            case 33:
              return true;
            case 31:
              return true;
            case -31:
              return true;
            case -33:
              return true;
            case 18:
              return true;
            case 14:
              return true;
            case -14:
              return true;
            case -18:
              return true;
          }
	      return false;
        #endregion

        #region ' Black Rook '
        case CompactPiece.BlackRook:
          if (dx % 16 == 0) 
          {
            var steps = dx / 16;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % 1 == 0) 
          {
            var steps = dx / 1;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -16 == 0) 
          {
            var steps = dx / -16;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -1 == 0) 
          {
            var steps = dx / -1;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          return false;
        #endregion

        #region ' Black Queen '
        case CompactPiece.BlackQueen:
          if (dx % 16 == 0) 
          {
            var steps = dx / 16;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 16; (i & 0x88) == 0; i += 16)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % 1 == 0) 
          {
            var steps = dx / 1;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 1; (i & 0x88) == 0; i += 1)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -16 == 0) 
          {
            var steps = dx / -16;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -16; (i & 0x88) == 0; i += -16)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -1 == 0) 
          {
            var steps = dx / -1;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -1; (i & 0x88) == 0; i += -1)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % 17 == 0) 
          {
            var steps = dx / 17;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 17; (i & 0x88) == 0; i += 17)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -15 == 0) 
          {
            var steps = dx / -15;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -15; (i & 0x88) == 0; i += -15)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % -17 == 0) 
          {
            var steps = dx / -17;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + -17; (i & 0x88) == 0; i += -17)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          if (dx % 15 == 0) 
          {
            var steps = dx / 15;
            if (steps >= 0 && steps < 8) 
              for (var i = fromSquare + 15; (i & 0x88) == 0; i += 15)
                if (i == toSquare) return true;
                else if (this[i] != CompactPiece.EmptyCell) return false;
          }
          return false;
        #endregion

        #region ' Black King '
        case CompactPiece.BlackKing:
          switch (dx)
          {      
            case 16:
              return true;
            case 17:
              return true;
            case 1:
              return true;
            case -15:
              return true;
            case -16:
              return true;
            case -17:
              return true;
            case -1:
              return true;
            case 15:
              return true;
            case 2:
              return this[toSquare] == CompactPiece.EmptyCell;
            case -2:
              return this[toSquare] == CompactPiece.EmptyCell;
          }
	      return false;
        #endregion

        default: return false;
	    }
    }
  }
}