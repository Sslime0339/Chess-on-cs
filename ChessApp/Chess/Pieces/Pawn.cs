using System.Collections.Generic;



namespace Chess
{
  class Pawn : Piece
  {
    private bool _fistMove;
    
    public Pawn(Vector2 position, PieceColor color, Chessboard board, bool fistMove = true) : base(position, color, board, PieceType.Pawn)
    {
      _fistMove = fistMove;
    }
    
    public override List<Vector2> GetAllMove()
    {
      return null;
    }
    
  }
}