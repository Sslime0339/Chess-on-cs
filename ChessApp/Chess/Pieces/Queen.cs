using System.Collections.Generic;



namespace Chess
{
  class Queen : Piece
  {
    
    
    public Queen(Vector2 position, PieceColor color, Chessboard board, bool firstMove = true) : base(position, color, board, PieceType.Queen, firstMove)
    {
      
    }
    
    public override Piece Clone(Chessboard newBoard)
    {
      return new Queen(_position, color, newBoard, _firstMove);
    }
    
    // TODO:
    public override List<Vector2> GetAllMoves()
    {
      return GetAllQueenMove();
    }
    
    // TODO:
    public override List<Vector2> GetAttackedPositions()
    {
      return GetAllQueenMove();
    }
    
    private List<Vector2> GetAllQueenMove()
    {
      List<Vector2> result = GetOrthogonalLines(_position);
      result.AddRange(GetDiagonals(_position));
      return result;
    }
  }
}