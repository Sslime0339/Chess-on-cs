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
    
    public readonly PieceType type;
    
    
    public Piece(Vector2 position, PieceColor color, Chessboard board, PieceType type)
    {
      _position = position;
      this.color = color;
      _board = board;
      this.type = type;
    }
    
    
    public abstract List<Vector2> GetAllMove();
    
    
  }
}