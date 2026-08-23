using System.Collections.Generic;



namespace Chess
{
  class King : Piece
  {
    
    public King(Vector2 position, PieceColor color, Chessboard board, bool fistMove = true) : base(position, color, board, PieceType.King, fistMove)
    {
      
    }
    
    // TODO:
    public override List<Vector2> GetAllMoves()
    {
      return null;
    }
    
    // TODO:
    public override List<Vector2> GetAttackedPositions()
    {
      return null;
    }
    
  }
}