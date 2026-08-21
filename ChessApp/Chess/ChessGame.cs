



namespace Chess
{
  class ChessGame
  {
    private Chessboard _board;
    private PieceColor _currentTurn;
    
    
    public ChessGame()
    {
      _board = new Chessboard();
      _currentTurn = PieceColor.White;
    }
    
    
    
    
    
    public SquareInfo[,] GetBoardDrawingData()
    {
      return _board.GetDrawingData();
    }
  }
}