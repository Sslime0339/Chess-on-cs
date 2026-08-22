




namespace Chess
{
  class Chessboard
  {
    private Square[,] _board;
    
    public Square this[int x, int y]
    {
      get
      {
        return _board[x, y];
      }
    }
    
    public Square this[Vector2 vec]
    {
      get
      {
        return this[vec.X, vec.Y];
      }
    }
    
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
      
      
      for (int i = 0; i < 8; i++)
        AddPiece(new Pawn(new Vector2(i, 1), PieceColor.White, this));
      
      for (int i = 0; i < 8; i++)
        AddPiece(new Pawn(new Vector2(i, 6), PieceColor.Black, this));
      
      // тест
      // AddPiece(new Pawn(new Vector2(3, 2), PieceColor.Black, this));
      
      AddPiece(new Rook(new Vector2(0,0), PieceColor.White, this));
      AddPiece(new Rook(new Vector2(7,0), PieceColor.White, this));
      AddPiece(new Knight(new Vector2(1, 0), PieceColor.White, this));
      AddPiece(new Knight(new Vector2(6, 0), PieceColor.White, this));
      AddPiece(new Bishop(new Vector2(2, 0), PieceColor.White, this));
      AddPiece(new Bishop(new Vector2(5, 0), PieceColor.White, this));
      AddPiece(new Queen(new Vector2(3, 0), PieceColor.White, this));
      AddPiece(new King(new Vector2(4, 0), PieceColor.White, this));
      
      AddPiece(new Rook(new Vector2(0,7), PieceColor.Black, this));
      AddPiece(new Rook(new Vector2(7,7), PieceColor.Black, this));
      AddPiece(new Knight(new Vector2(1, 7), PieceColor.Black, this));
      AddPiece(new Knight(new Vector2(6, 7), PieceColor.Black, this));
      AddPiece(new Bishop(new Vector2(2, 7), PieceColor.Black, this));
      AddPiece(new Bishop(new Vector2(5, 7), PieceColor.Black, this));
      AddPiece(new Queen(new Vector2(3, 7), PieceColor.Black, this));
      AddPiece(new King(new Vector2(4, 7), PieceColor.Black, this));
    }
    
    
    public void AddPiece(Piece piece)
    {
      _board[piece.Position.X, piece.Position.Y].AddPiece(piece);
    }
    
    public SquareInfo[,] GetData()
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