using Sslime0339.Chess;
using Sslime0339.Chess.Pieces;
using System;

/*
белые фигуры
♔
♕
♖
♗
♘
♙
*/

/*
чёрные фигуры
♚
♛
♜
♝
♞
♟
*/

/*
белый квадрат
◻

чёрный квадрат
◼
*/

namespace ConsoleUI
{
    static class Draw
    {
        public static void Chessboard(SquareInfo[,] boardData)
        {
            DrawHeader();

            for (int j = 7; j >= 0; j--)
            {
                // Левая рамка с номером строки
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($" {j + 1} ");

                for (int i = 0; i < 8; i++)
                {
                    // Чередуем фон самой клетки (светло-серый и тёмно-серый)
                    bool isLightSquare = (i + j) % 2 != 0;
                    Console.BackgroundColor = isLightSquare ? ConsoleColor.Gray : ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.Black;

                    DrawPiece(boardData[i, j]);
                }

                // Правая рамка с номером строки
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write($" {j + 1} ");
                Console.ResetColor();
                Console.Write("\n");
            }

            DrawHeader();
        }

        private static void DrawHeader()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            // По два пробела на каждый столбец, чтобы совпало с шириной клеток
            Console.Write("    A  B  C  D  E  F  G  H    ");
            Console.ResetColor();
            Console.Write("\n");
        }

        private static void DrawPiece(SquareInfo square)
        {
            if (square.IsEmpty)
            {
                // Клетка шириной в 3 пробела делает пропорции доски почти квадратными
                Console.Write("   ");
                return;
            }

            char symbol = square.Color == PieceColor.White
                ? square.Piece switch
                {
                    PieceType.Pawn => '♙',
                    PieceType.Knight => '♘',
                    PieceType.Bishop => '♗',
                    PieceType.Rook => '♖',
                    PieceType.Queen => '♕',
                    PieceType.King => '♔',
                    _ => ' '
                }
                : square.Piece switch
                {
                    PieceType.Pawn => '♟',
                    PieceType.Knight => '♞',
                    PieceType.Bishop => '♝',
                    PieceType.Rook => '♜',
                    PieceType.Queen => '♛',
                    PieceType.King => '♚',
                    _ => ' '
                };

            if (square.Piece == PieceType.Pawn && square.Color == PieceColor.Black)
                Console.Write($" {symbol}");
            else
                // Пробел слева и справа центрирует фигуру внутри клетки
                Console.Write($" {symbol} ");
        }
    }

}