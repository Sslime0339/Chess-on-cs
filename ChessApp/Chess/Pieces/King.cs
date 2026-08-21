using System.Collections.Generic;



namespace Chess
{
  class King : Piece
  {
    private bool _fistMove;
    
    public King(Vector2 position, PieceColor color, Chessboard board, bool fistMove = true) : base(position, color, board, PieceType.King)
    {
      _fistMove = fistMove;
    }
    
    /*
    public override List<Vector2> GetAllMove()
    {
      return null;
    }
    */
    
  }
}