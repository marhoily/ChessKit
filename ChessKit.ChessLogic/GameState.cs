namespace ChessKit.ChessLogic
{
  public enum GameState
  {
    /// <summary>No flags set: game is not over yet</summary>
    None,
    /// <summary>Black won (either by mating white king, or white resign)</summary>
    BlackWin,
    /// <summary>White won (either by mating black king, or black resign)</summary>
    WhiteWin,
    /// <summary>Game ended with draw (either by repetition, impasse or an agreement)</summary>
    Draw,
    /// <summary>Game ended with draw by repetition</summary>
    DrawByRepetition,
    /// <summary>It's check to side on move</summary>
    Check,
    CheckToWhite, CheckToBlack,
  }
}
