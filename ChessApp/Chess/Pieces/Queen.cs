using System.Collections.Generic;



namespace Chess
{
  class Queen : Piece
  {
    
    
    public Queen(Vector2 position, PieceColor color, Chessboard board, bool fistMove = true) : base(position, color, board, PieceType.Queen, fistMove)
    {
      
    }
    
    public override Piece Clone(Chessboard newBoard)
    {
      return new Queen(_position, color, newBoard, _fistMove);
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