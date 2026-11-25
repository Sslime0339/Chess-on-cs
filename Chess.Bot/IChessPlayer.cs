using System;
using System.Collections.Generic;
using System.Text;

using Sslime0339.Chess;

namespace Sslime0339.Chess.Bot
{
    public interface IChessPlayer
    {
        string Name { get; }
        Task<BotMove> GetMoveAsync(ChessGame game, CancellationToken ct = default);
    }
}
