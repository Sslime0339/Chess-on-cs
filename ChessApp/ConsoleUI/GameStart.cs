using ChessApp.ConsoleUI;
using Sslime0339.Chess;
using Sslime0339.Chess.Bot;
using System;


namespace ConsoleUI
{
    static class GameStart
    {
        public static async Task StartAsync()
        {
            Console.WriteLine("Выберите режим игры:");
            Console.WriteLine("1 - Человек против Бота");
            Console.WriteLine("2 - Бот против Бота");
            Console.WriteLine("3 - Человек против Человека");

            string choice = Console.ReadLine()?.Trim();
            IChessPlayer whitePlayer;
            IChessPlayer blackPlayer;

            if (choice == "2")
            {
                var botWhite = new MinimaxBot("Бот Белые", maxDepth: 2, thinkDelayMs: 1000);
                var botBlack = new MinimaxBot("Бот Чёрные", maxDepth: 3, thinkDelayMs: 1000);
                whitePlayer = botWhite;
                blackPlayer = botBlack;
            }
            else if (choice == "3")
            {
                whitePlayer = new ConsoleHumanPlayer("Белые");
                blackPlayer = new ConsoleHumanPlayer("Чёрные");
            }
            else
            {
                var human = new ConsoleHumanPlayer("Вы");
                var bot = new MinimaxBot("Stockfish-Lite", maxDepth: 3, thinkDelayMs: 800);

                // Подписываемся на мысли бота, чтобы видеть процесс анализа
                bot.OnThoughtUpdated += thought =>
                {
                    Console.WriteLine($"[Мысли бота] {thought.AnalysisNote}");
                };

                whitePlayer = human;
                blackPlayer = bot;
            }

            var match = new ChessMatch(whitePlayer, blackPlayer);

            // Подписываемся на обновление доски и совершенные ходы
            match.OnBoardUpdated += board => Draw.Chessboard(board);
            match.OnMoveMade += (player, move, res) =>
            {
                Console.WriteLine($"\n{player.Name} сходил: {move} ({res})\n");
            };

            // Запускаем игру
            await match.RunAsync();

            Console.WriteLine("Игра окончена!");
        }



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