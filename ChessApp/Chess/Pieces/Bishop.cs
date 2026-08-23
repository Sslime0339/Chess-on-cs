using System.Collections.Generic;



namespace Chess
{
  class Bishop : Piece
  {
    
    
    public Bishop(Vector2 position, PieceColor color, Chessboard board, bool fistMove = true) : base(position, color, board, PieceType.Bishop, fistMove)
    {
      
    }
    
    public override Piece Clone(Chessboard newBoard)
    {
      return new Bishop(_position, color, newBoard, _fistMove);
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