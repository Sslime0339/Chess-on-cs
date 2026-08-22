using System.Collections.Generic;



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
    
    
    public MoveResult Move(Vector2 from, Vector2 to)
    {
      if (_board[from].IsEmpty) return MoveResult.IllegalMove;
      
      Piece piece = _board[from].CurrentPiece;
      List<Vector2> validMoves = piece.GetValidMoves();
      
      if (validMoves.Contains(to))
        return MoveResult.Success;
      
      return MoveResult.PromotionRequired;
    }
    
    
    public SquareInfo[,] GetBoardData()
    {
      return _board.GetData();
    }
  }
}