using System;


namespace Chess
{
  struct SquareInfo
  {
    public bool IsEmpty { get; }
    
    private readonly PieceType? _piece;
    public PieceType? Piece => IsEmpty ? null : _piece;
    
    private readonly PieceColor? _color;
    public PieceColor? Color => IsEmpty ? null : _color;
    
    public SquareInfo(PieceType piece, PieceColor color)
    {
      this.IsEmpty = false;
      this._piece = piece;
      this._color = color;
    }
    
    //чтобы получить нужно использовать SquareInfo.EmptySquare()
    private SquareInfo(bool isEmpty)
    {
      this.IsEmpty = true;
      this._piece = null;
      this._color = null;
    }
    
    public static SquareInfo EmptySquare()
    {
      return new SquareInfo(true);
    }
    
  }
}