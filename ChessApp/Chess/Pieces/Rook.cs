using System.Collections.Generic;



namespace Chess
{
  class Rook : Piece
  {
    
    public Rook(Vector2 position, PieceColor color, Chessboard board, bool fistMove = true) : base(position, color, board, PieceType.Rook, fistMove)
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