using System.Collections.Generic;



namespace Chess
{
  class Pawn : Piece
  {
    
    
    public Pawn(Vector2 position, PieceColor color, Chessboard board, bool firstMove = true) : base(position, color, board, PieceType.Pawn, firstMove)
    {
      
    }
    
    public override Piece Clone(Chessboard newBoard)
    {
      return new Pawn(_position, color, newBoard, _firstMove);
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
        if (_firstMove && CheckEmpty(_position + forward * 2))
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