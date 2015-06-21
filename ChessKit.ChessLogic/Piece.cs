namespace ChessKit.ChessLogic
{
  public enum Piece : byte
  {
    EmptyCell,

    WhitePawn   = Color.White | MoveAnnotations.Pawn  ,
    WhiteBishop = Color.White | MoveAnnotations.Bishop,
    WhiteKnight = Color.White | MoveAnnotations.Knight,
    WhiteRook   = Color.White | MoveAnnotations.Rook  ,
    WhiteQueen  = Color.White | MoveAnnotations.Queen ,
    WhiteKing   = Color.White | MoveAnnotations.King  ,
    
    BlackPawn   = Color.Black | MoveAnnotations.Pawn  ,
    BlackBishop = Color.Black | MoveAnnotations.Bishop,
    BlackKnight = Color.Black | MoveAnnotations.Knight,
    BlackRook   = Color.Black | MoveAnnotations.Rook  ,
    BlackQueen  = Color.Black | MoveAnnotations.Queen ,
    BlackKing   = Color.Black | MoveAnnotations.King  ,
  }
}                                                    