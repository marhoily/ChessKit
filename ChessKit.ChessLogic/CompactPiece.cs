namespace ChessKit.ChessLogic
{
  public enum CompactPiece : byte
  {
    EmptyCell,

    WhitePawn   = PieceColor.White | MoveHints.Pawn  ,
    WhiteBishop = PieceColor.White | MoveHints.Bishop,
    WhiteKnight = PieceColor.White | MoveHints.Knight,
    WhiteRook   = PieceColor.White | MoveHints.Rook  ,
    WhiteQueen  = PieceColor.White | MoveHints.Queen ,
    WhiteKing   = PieceColor.White | MoveHints.King  ,
    
    BlackPawn   = PieceColor.Black | MoveHints.Pawn  ,
    BlackBishop = PieceColor.Black | MoveHints.Bishop,
    BlackKnight = PieceColor.Black | MoveHints.Knight,
    BlackRook   = PieceColor.Black | MoveHints.Rook  ,
    BlackQueen  = PieceColor.Black | MoveHints.Queen ,
    BlackKing   = PieceColor.Black | MoveHints.King  ,
  }
}                                                    