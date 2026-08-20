using System.Collections.Generic;


namespace Chess
{
  abstract class Piece
  {
    protected Vector2 _position;
    public Vector2 Position => _position;
    
    protected Chessboard _board;
    public Chessboard Board => _board;
    
    public readonly PieceColor color;
    
    
    public Piece(Vector2 position, PieceColor color, Chessboard board)
    {
      _position = position;
      this.color = color;
      _board = board;
    }
    
    
    public abstract List<Vector2> GetAllMove();
    
    
  }
}