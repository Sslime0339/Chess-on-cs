using Sslime0339.Chess;
using System;


namespace ConsoleUI
{
    static class GameStart
    {
        public static void Start()
        {
            ChessGame game = new ChessGame();

            Console.Write("\n");
            while (!game.IsGameOver)
            {
                Draw.Chessboard(game.GetBoardData());
                Console.WriteLine($"Введите ход ({game.CurrentTurn})");




                string input = Console.ReadLine()?.Trim();

                if (input == "save")
                {
                    game.SaveToFile("save.txt");
                    Console.WriteLine($"Игра сохранена! FEN: {game.SaveToFen()}");
                    continue;
                }

                if (input == "load")
                {
                    if (System.IO.File.Exists("save.txt"))
                    {
                        game.LoadFromFile("save.txt");
                        Console.WriteLine("Игра загружена!");
                    }
                    else
                    {
                        Console.WriteLine("Файл сохранения не найден.");
                    }
                    continue;
                }




                //string input = Console.ReadLine();
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
                        Promotion(game);
                    }
                }
                else
                {
                    Console.WriteLine("Неправильный ввод");
                }
            }
            Draw.Chessboard(game.GetBoardData());

        }
        static void Promotion(ChessGame game)
        {
            Console.WriteLine("Напишите в какую фигуру превратиться пешка\n(Q - ферзь, R - ладья, B - слон, K - конь)\n");
            string input = Console.ReadLine();
            PromotionPiece promotionPiece;
            while (!ConsoleInputHandler.TryParsePromotionPiece(input, out promotionPiece))
            {
                Console.WriteLine("Неверный ввод");
                input = Console.ReadLine();
            }

            MoveResult result = game.ToPromote(promotionPiece);
            Console.WriteLine($"\n{result}");
        }


    }
}