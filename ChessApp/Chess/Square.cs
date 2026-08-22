



namespace Chess
{
  class Square
  {
    private Piece _piece;
    public Piece CurrentPiece => _piece;
    
    public bool IsEmpty => _piece == null;
    
    public bool IsAttacked { get; set; }
    
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