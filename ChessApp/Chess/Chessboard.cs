




namespace Chess
{
  class Chessboard
  {
    private Square[,] _board;
    
    //public 
    
    public Chessboard()
    {
      _board = new Square[8, 8];
      
      for (int i = 0; i < 8; i++)
      {
        for (int j = 0; j < 8; j++)
        {
          _board[i, j] = new Square();
        }
      }
      
      AddPiece(new Pawn(new Vector2(0, 1), PieceColor.White, this));
    }
    
    
    public void AddPiece(Piece piece)
    {
      _board[piece.Position.X, piece.Position.Y].AddPiece(piece);
    }
    
    public SquareInfo[,] GetDrawingData()
    {
      SquareInfo[,] data = new SquareInfo[8, 8];
      for (int i = 0; i < 8; i++)
      {
        for (int j = 0; j < 8; j++)
        {
          data[i, j] = _board[i, j].GetSquareInfo();
        }
      }
      return data;
    }
    
  }
}