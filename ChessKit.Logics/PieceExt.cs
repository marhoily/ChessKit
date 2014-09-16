namespace ChessKit.ChessLogic
{
  /// <summary>Represents piece Type</summary>
  public static class PieceExt
  {
    public static PieceColor Invert(this PieceColor color)
    {
      return color == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }
  }
}