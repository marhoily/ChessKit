namespace ChessKit.ChessLogic
{
  public enum CompactPiece : byte
  {
    EmptyCell,

    WhitePawn   = PieceColor.White | MoveAnnotations.Pawn  ,
    WhiteBishop = PieceColor.White | MoveAnnotations.Bishop,
    WhiteKnight = PieceColor.White | MoveAnnotations.Knight,
    WhiteRook   = PieceColor.White | MoveAnnotations.Rook  ,
    WhiteQueen  = PieceColor.White | MoveAnnotations.Queen ,
    WhiteKing   = PieceColor.White | MoveAnnotations.King  ,
    
    BlackPawn   = PieceColor.Black | MoveAnnotations.Pawn  ,
    BlackBishop = PieceColor.Black | MoveAnnotations.Bishop,
    BlackKnight = PieceColor.Black | MoveAnnotations.Knight,
    BlackRook   = PieceColor.Black | MoveAnnotations.Rook  ,
    BlackQueen  = PieceColor.Black | MoveAnnotations.Queen ,
    BlackKing   = PieceColor.Black | MoveAnnotations.King  ,
  }
}                                                    