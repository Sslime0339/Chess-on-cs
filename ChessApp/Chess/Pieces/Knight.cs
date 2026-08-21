using System.Collections.Generic;



namespace Chess
{
  class Knight : Piece
  {
    
    
    public Knight(Vector2 position, PieceColor color, Chessboard board) : base(position, color, board, PieceType.Knight)
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