using System.Collections.Generic;



namespace Chess
{
  class King : Piece
  {
    
    public King(Vector2 position, PieceColor color, Chessboard board, bool firstMove = true) : base(position, color, board, PieceType.King, firstMove)
    {
      
    }
    
    public override Piece Clone(Chessboard newBoard)
    {
      return new King(_position, color, newBoard, _firstMove);
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