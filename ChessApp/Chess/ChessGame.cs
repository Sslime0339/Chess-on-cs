



namespace Chess
{
  class ChessGame
  {
    private Chessboard _board;
    
    private PieceColor _currentTurn;
    public PieceColor CurrentTurn => _currentTurn;
    
    private bool _isGameOver;
    public bool IsGameOver => _isGameOver;
    
    
    public ChessGame()
    {
      _board = new Chessboard();
      _currentTurn = PieceColor.White;
      _isGameOver = false;
    }
    
    
    public MoveResult Move()
    {
      return MoveResult.IllegalMove;
    }
    
    
    public SquareInfo[,] GetBoardData()
    {
      return _board.GetData();
    }
  }
}