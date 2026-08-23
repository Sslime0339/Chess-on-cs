using System.Collections.Generic;



namespace Chess
{
  class Rook : Piece
  {
    
    public Rook(Vector2 position, PieceColor color, Chessboard board, bool fistMove = true) : base(position, color, board, PieceType.Rook, fistMove)
    {
      
    }
    
    public override Piece Clone(Chessboard newBoard)
    {
      return new Rook(_position, color, newBoard, _fistMove);
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