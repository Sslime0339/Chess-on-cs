using Chess;
using System;


namespace ConsoleUI
{
  static class Draw
  {
    public static void Chessboard(SquareInfo[,] boardData)
    {
      
      Console.BackgroundColor = ConsoleColor.White;
      Console.ForegroundColor = ConsoleColor.Black;
      Console.Write("   A B C D E F G H   ");
      Console.ResetColor();
      Console.Write("\n");
      
      for (int j = 7; j >= 0; j--)
      {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write($" {j+1} ");
        
        for (int i = 0; i < 8; i++)
        {
          DrawSquare(boardData[i, j], i, j);
          
          
          
          Console.Write(" ");
        }
        Console.Write($"{j+1} ");
        Console.ResetColor();
        Console.Write("\n");
      }
      
      Console.BackgroundColor = ConsoleColor.White;
      Console.ForegroundColor = ConsoleColor.Black;
      Console.Write("   A B C D E F G H   ");
      Console.ResetColor();
      Console.Write("\n");
    }
    
    private static void DrawSquare(SquareInfo square, int x, int y)
    {
      if (square.IsEmpty == true)
      {
        if ((x + y) % 2 != 0)
          Console.Write("◻"); // белый квадрат
        else
          Console.Write("◼"); // чёрный квадрат
      }
      else if (square.Color == PieceColor.White)
      {
        /* белые фигуры
        ♔
        ♕
        ♖
        ♗
        ♘
        ♙
        */
        switch (square.Piece)
        {
          case PieceType.Pawn:
            Console.Write("♙");
            break;
          case PieceType.Knight:
            Console.Write("♘");
            break;
          case PieceType.Bishop:
            Console.Write("♗");
            break;
          case PieceType.Rook:
            Console.Write("♖");
            break;
          case PieceType.Queen:
            Console.Write("♕");
            break;
          case PieceType.King:
            Console.Write("♔");
            break;
        }
      }
      else // чёрные фигуры
      {
        /*
        ♚
        ♛
        ♜
        ♝
        ♞
        ♟
        */
        switch (square.Piece)
        {
          case PieceType.Pawn:
            Console.Write("♟");
            break;
          case PieceType.Knight:
            Console.Write("♞");
            break;
          case PieceType.Bishop:
            Console.Write("♝");
            break;
          case PieceType.Rook:
            Console.Write("♜");
            break;
          case PieceType.Queen:
            Console.Write("♛");
            break;
          case PieceType.King:
            Console.Write("♚");
            break;
        }
      }
      
    }
  }
}