using Chess;


namespace ConsoleUI
{
  static class GameStart
  {
    public static void Start()
    {
      ChessGame game = new ChessGame();
      Draw.Chessboard(game.GetBoardDrawingData());
    }
  }
}