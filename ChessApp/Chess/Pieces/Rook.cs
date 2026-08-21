using System.Collections.Generic;



namespace Chess
{
  class Rook : Piece
  {
    private bool _fistMove;
    
    public Rook(Vector2 position, PieceColor color, Chessboard board, bool fistMove = true) : base(position, color, board, PieceType.Rook)
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