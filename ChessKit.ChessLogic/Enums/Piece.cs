namespace ChessKit.ChessLogic.Enums
{
  public enum Piece : byte
  {
    EmptyCell,

    WhitePawn   = Color.White | PieceType.Pawn  ,
    WhiteBishop = Color.White | PieceType.Bishop,
    WhiteKnight = Color.White | PieceType.Knight,
    WhiteRook   = Color.White | PieceType.Rook  ,
    WhiteQueen  = Color.White | PieceType.Queen ,
    WhiteKing   = Color.White | PieceType.King  ,
    
    BlackPawn   = Color.Black | PieceType.Pawn  ,
    BlackBishop = Color.Black | PieceType.Bishop,
    BlackKnight = Color.Black | PieceType.Knight,
    BlackRook   = Color.Black | PieceType.Rook  ,
    BlackQueen  = Color.Black | PieceType.Queen ,
    BlackKing   = Color.Black | PieceType.King  ,
  }
}                                                    