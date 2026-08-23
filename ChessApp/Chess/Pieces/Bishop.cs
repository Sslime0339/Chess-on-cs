using System.Collections.Generic;



namespace Chess
{
  class Bishop : Piece
  {
    
    
    public Bishop(Vector2 position, PieceColor color, Chessboard board, bool firstMove = true) : base(position, color, board, PieceType.Bishop, firstMove)
    {
      
    }
    
    public override Piece Clone(Chessboard newBoard)
    {
      return new Bishop(_position, color, newBoard, _firstMove);
    }
    
    
    // TODO:
    public override List<Vector2> GetAllMoves()
    {
      List<Vector2> result = new List<Vector2>();
      
    }
    
    // TODO:
    public override List<Vector2> GetAttackedPositions()
    {
      return null;
    }
  }
}