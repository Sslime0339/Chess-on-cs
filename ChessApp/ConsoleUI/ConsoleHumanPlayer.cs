using ConsoleUI;
using Sslime0339.Chess;
using System;
using System.Collections.Generic;
using System.Text;

using Sslime0339.Chess.Bot;

namespace ChessApp.ConsoleUI
{
    class ConsoleHumanPlayer : IChessPlayer
    {
        public string Name { get; }

        public ConsoleHumanPlayer(string name = "Игрок")
        {
            Name = name;
        }

        public Task<BotMove> GetMoveAsync(ChessGame game, CancellationToken ct = default)
        {
            while (true)
            {
                Console.WriteLine($"{Name} ({game.CurrentTurn}), введите ход:");
                string input = Console.ReadLine();

                if (ConsoleInputHandler.TryParseMove(input, out Vector2 from, out Vector2 to))
                {
                    // Если пешка идет на последнюю горизонталь, сразу спрашиваем превращение
                    SquareInfo sq = game.GetBoardData()[from.X, from.Y];
                    if (sq.Piece == PieceType.Pawn && (to.Y == 7 || to.Y == 0))
                    {
                        Console.WriteLine("Превратить в (Q/R/B/K):");
                        PromotionPiece promo;
                        while (!ConsoleInputHandler.TryParsePromotionPiece(Console.ReadLine(), out promo))
                            Console.WriteLine("Неверный ввод");

                        return Task.FromResult(new BotMove(from, to, promo));
                    }

                    return Task.FromResult(new BotMove(from, to));
                }

                Console.WriteLine("Неправильный ввод!");
            }
        }
    }
}
