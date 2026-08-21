using System.Collections.Generic;



namespace Chess
{
  class Bishop : Piece
  {
    
    
    public Bishop(Vector2 position, PieceColor color, Chessboard board) : base(position, color, board, PieceType.Bishop)
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