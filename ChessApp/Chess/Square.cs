



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
    
    public SquareInfo GetSquareInfo()
    {
      if (_piece == null)
        return SquareInfo.EmptySquare();
      else
        return new SquareInfo(_piece.type, _piece.color);
      
    }
  }
}