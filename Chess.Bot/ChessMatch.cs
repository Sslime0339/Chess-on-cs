using System;
using System.Collections.Generic;
using System.Text;

using Sslime0339.Chess;


namespace Sslime0339.Chess.Bot
{
    public class ChessMatch
    {
        public ChessGame Game { get; }
        public IChessPlayer WhitePlayer { get; }
        public IChessPlayer BlackPlayer { get; }

        // События для обновления интерфейса
        public event Action<IChessPlayer, BotMove, MoveResult>? OnMoveMade;
        public event Action<SquareInfo[,]>? OnBoardUpdated;

        public ChessMatch(IChessPlayer whitePlayer, IChessPlayer blackPlayer)
        {
            Game = new ChessGame();
            WhitePlayer = whitePlayer;
            BlackPlayer = blackPlayer;
        }

        public IChessPlayer CurrentPlayer =>
            Game.CurrentTurn == PieceColor.White ? WhitePlayer : BlackPlayer;

        public async Task RunAsync(CancellationToken ct = default)
        {
            OnBoardUpdated?.Invoke(Game.GetBoardData());

            while (!Game.IsGameOver && !ct.IsCancellationRequested)
            {
                IChessPlayer player = CurrentPlayer;

                // Запрашиваем ход у текущего игрока (человека или бота)
                BotMove move = await player.GetMoveAsync(Game, ct);

                MoveResult result = Game.Move(move.From, move.To);

                // Если требуется превращение пешки
                if (result == MoveResult.PromotionRequired)
                {
                    PromotionPiece promo = move.Promotion ?? PromotionPiece.Queen;
                    result = Game.ToPromote(promo);
                }

                if (result == MoveResult.IllegalMove)
                {
                    // Если человек ввел неверный ход, цикл повторится и спросит снова
                    continue;
                }

                OnMoveMade?.Invoke(player, move, result);
                OnBoardUpdated?.Invoke(Game.GetBoardData());
            }
        }
    }
}
