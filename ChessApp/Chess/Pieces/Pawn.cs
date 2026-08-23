using System.Collections.Generic;



namespace Chess
{
  class Pawn : Piece
  {
    
    
    public Pawn(Vector2 position, PieceColor color, Chessboard board, bool fistMove = true) : base(position, color, board, PieceType.Pawn, fistMove)
    {
      
    }
    
    // TODO:
    public override List<Vector2> GetAllMoves()
    {
      List<Vector2> result = new List<Vector2>();
      
      Vector2 forward;
      if (color == PieceColor.White)
        forward = new Vector2(0, 1);
      else 
        forward = new Vector2(0, -1);
      
      
      if (CheckEmptyAndAdd(_position + forward, result))
      {
        
        // проверено две клетки перед пешкой
        if (_fistMove && CheckEmpty(_position + forward * 2))
        {
          result.Add(_position + forward * 2);
        }
      }
      
      CheckEnemyAndAdd(_position + forward + new Vector2(1, 0), result);
      
      CheckEnemyAndAdd(_position + forward + new Vector2(-1, 0), result);
      
      return result;
    }
    
    // TODO:
    public override List<Vector2> GetAttackedPositions()
    {
      List<Vector2> result = new List<Vector2>();
      
      Vector2 forward;
      if (color == PieceColor.White)
        forward = new Vector2(0, 1);
      else 
        forward = new Vector2(0, -1);
      
      result.Add(_position + forward + new Vector2(1, 0));
      
      result.Add(_position + forward + new Vector2(-1, 0));
      
      return result;
    }
    
  }
}