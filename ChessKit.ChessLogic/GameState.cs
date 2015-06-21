namespace ChessKit.ChessLogic
{
  public enum GameState
  {
    /// <summary>No flags set: game is not over yet</summary>
    None,
    /// <summary></summary>
    Mate,
    /// <summary>Game ended with draw (either by repetition, impasse or an agreement)</summary>
    Draw,
    /// <summary>Game ended with draw by repetition</summary>
    DrawByRepetition,
    /// <summary>It's check to side on move</summary>
    Check,
    CheckToWhite, CheckToBlack,
  }
}
