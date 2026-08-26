using Chess;
using System;


namespace ConsoleUI
{
    static class GameStart
    {
        public static void Start()
        {
            ChessGame game = new ChessGame();
            while (!game.IsGameOver)
            {
                Draw.Chessboard(game.GetBoardData());
                Console.WriteLine($"Введите ход ({game.CurrentTurn})");
                string input = Console.ReadLine();
                Vector2 from;
                Vector2 to;
                bool isCorrect = ConsoleInputHandler.TryParseMove(input, out from, out to);
                // Console.WriteLine($"\nfrom {from}, to {to}, isCorrect {isCorrect}\n");
                if (isCorrect)
                {
                    MoveResult moveResult = game.Move(from, to);
                    Console.WriteLine($"\n{moveResult}");

                    if (game.PromotionRequired)
                    {
                        Promotion();
                    }
                }
                else
                {
                    Console.WriteLine("Неправильный ввод");
                }
            }
            Draw.Chessboard(game.GetBoardData());

            void Promotion()
            {
                Console.WriteLine("Напишите в какую фигуру превратиться пешка\n(Q - ферзь, R - ладья, B - слон, K - конь)\n");
                string input = Console.ReadLine();
                PromotionPiece promotionPiece;
                while (!ConsoleInputHandler.TryParsePromotionPiece(input, out promotionPiece))
                {
                    Console.WriteLine("Неверный ввод");
                    input = Console.ReadLine();
                }
                game.ToPromote(promotionPiece);
            }
        }

    }
}