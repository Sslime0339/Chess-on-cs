using System.Collections.Generic;



namespace Chess
{
  class Bishop : Piece
  {
    
    
    public Bishop(Vector2 position, PieceColor color, Chessboard board, bool fistMove = true) : base(position, color, board, PieceType.Bishop, fistMove)
    {
      
    }
    
    // TODO:
    public override List<Vector2> GetValidMoves()
    {
      return null;
    }
    
    // TODO:
    public override List<Vector2> GetAttackedSquares()
    {
      return null;
    }
  }
}