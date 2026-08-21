using System.Collections.Generic;



namespace Chess
{
  class Queen : Piece
  {
    
    
    public Queen(Vector2 position, PieceColor color, Chessboard board) : base(position, color, board, PieceType.Queen)
    {
      
    }
    
    /*
    public override List<Vector2> GetAllMove()
    {
      return null;
    }
    */
  }
}