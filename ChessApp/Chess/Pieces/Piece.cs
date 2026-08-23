using System.Collections.Generic;



namespace Chess
{
  abstract class Piece
  {
    protected Vector2 _position;
    public Vector2 Position => _position;
    
    protected Chessboard _board;
    public Chessboard Board => _board;
    
    protected bool _fistMove;
    public bool FistMove => _fistMove;
    
    public readonly PieceColor color;
    
    public readonly PieceType type;
    
    
    public Piece(Vector2 position, PieceColor color, Chessboard board, PieceType type, bool fistMove = true)
    {
      _position = position;
      this.color = color;
      _board = board;
      this.type = type;
      _fistMove = fistMove;
    }
    
    public abstract Piece Clone(Chessboard newBoard);
    
    
    public abstract List<Vector2> GetAllMoves();
    
    public abstract List<Vector2> GetAttackedPositions();
    
    
    public List<Vector2> GetValidMoves()
    {
      List<Vector2> allMoves = GetAllMoves();
      List<Vector2> validMoves = new List<Vector2>();
      
      foreach (Vector2 move in allMoves)
      {
        if (!_board.WouldLeaveKingInCheck(_position, move))
        {
          validMoves.Add(move);
        }
      }
      
      return validMoves;
    }
    
    
    
    
    
    protected bool CheckEmpty(Vector2 position)
    {
      return OnBoard(position) && _board[position].IsEmpty;
    }
    
    protected bool CheckEmptyAndAdd(Vector2 position, List<Vector2> list)
    {
      if (CheckEmpty(position))
      {
        list.Add(position);
        return true;
      }
      return false;
    }
    
    protected bool CheckEnemy(Vector2 position)
    {
      return OnBoard(position) && !_board[position].IsEmpty && _board[position].CurrentPiece.color != color;
    }
    
    protected bool CheckEnemyAndAdd(Vector2 position, List<Vector2> list)
    {
      if (CheckEnemy(position))
      {
        list.Add(position);
        return true;
      }
      return false;
    }
    
    protected bool CheckEmptyOrEnemyAndAdd(Vector2 position, List<Vector2> list)
    {
      if (CheckEmpty(position) || CheckEnemy(position))
      {
        list.Add(position);
        return true;
      }
      return false;
    }
    
    protected bool OnBoard(Vector2 position)
    {
      return (position.X >= 0 && position.X < 8 &&
        position.Y >= 0 && position.Y < 8);
    }
    
  }
}