




namespace Chess
{
  class Chessboard
  {
    private Square[,] _board;
    
    
    public Chessboard()
    {
      _board = new Square[8, 8];
      
      AddPiece(new Pawn(new Vector2(0, 1), PieceColor.White, this));
    }
    
    
    private void AddPiece(Piece piece)
    {
      _board[piece.Position.X, piece.Position.Y].AddPiece(piece);
    }
  }
}