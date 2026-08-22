using System.Collections.Generic;



namespace Chess
{
  class Knight : Piece
  {
    
    
    public Knight(Vector2 position, PieceColor color, Chessboard board) : base(position, color, board, PieceType.Knight)
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