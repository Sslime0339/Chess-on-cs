



namespace Chess
{
  class Square
  {
    private Piece _piece;
    
    // хз вроде не нужен
    //public readonly Vector2 position;
    
    public void AddPiece(Piece piece)
    {
      _piece = piece;
    }
    
  }
}