using Sslime0339.Chess;
using Sslime0339.Chess.Pieces;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;


namespace Sslime0339.Chess.Bot
{
    public class MinimaxBot : IChessPlayer
    {
        public string Name { get; }
        public int MaxDepth { get; set; }
        public int ThinkDelayMs { get; set; } // Искусственная задержка, чтобы ходы не скипались

        // Событие, на которое может подписаться UI для отображения размышлений в реальном времени
        public event Action<BotThought>? OnThoughtUpdated;

        public MinimaxBot(string name = "ChessBot", int maxDepth = 3, int thinkDelayMs = 500)
        {
            Name = name;
            MaxDepth = maxDepth;
            ThinkDelayMs = thinkDelayMs;
        }

        // Вариант 1: Просто получить итоговый ход (совместим с IChessPlayer)
        public async Task<BotMove> GetMoveAsync(ChessGame game, CancellationToken ct = default)
        {
            return await ThinkAsync(game, reportThoughts: OnThoughtUpdated != null, ct);
        }

        // Вариант 2: Явный вызов с передачей коллбэка размышлений
        public async Task<BotMove> GetMoveWithThoughtsAsync(
            ChessGame game,
            Action<BotThought> onThought,
            CancellationToken ct = default)
        {
            void Handler(BotThought t) => onThought(t);
            OnThoughtUpdated += Handler;
            try
            {
                return await ThinkAsync(game, reportThoughts: true, ct);
            }
            finally
            {
                OnThoughtUpdated -= Handler;
            }
        }

        private async Task<BotMove> ThinkAsync(ChessGame game, bool reportThoughts, CancellationToken ct)
        {
            // Запускаем тяжелые вычисления в фоновом потоке, чтобы не вешать UI
            return await Task.Run(async () =>
            {
                var startTime = DateTime.UtcNow;
                Chessboard rootBoard = game.GetBoardClone();
                PieceColor botColor = game.CurrentTurn;

                List<BotMove> rootMoves = GenerateAllMoves(rootBoard, botColor);
                if (rootMoves.Count == 0)
                    throw new InvalidOperationException("У бота нет доступных ходов.");

                BotMove bestMove = rootMoves[0];
                int bestScore = int.MinValue;

                // Перебираем каждый возможный первый ход и анализируем его
                for (int i = 0; i < rootMoves.Count; i++)
                {
                    ct.ThrowIfCancellationRequested();
                    BotMove candidate = rootMoves[i];

                    if (reportThoughts)
                    {
                        OnThoughtUpdated?.Invoke(new BotThought
                        {
                            CurrentCandidate = candidate,
                            BestMoveSoFar = bestMove,
                            Score = bestScore == int.MinValue ? 0 : bestScore,
                            Depth = MaxDepth,
                            AnalysisNote = $"Анализирую ход {i + 1}/{rootMoves.Count}: {candidate}..."
                        });
                    }

                    // Делаем ход на копии доски и запускаем оценку вглубь
                    Chessboard nextBoard = rootBoard.Clone();
                    ApplyMove(nextBoard, candidate);

                    int score = -Minimax(nextBoard, MaxDepth - 1, int.MinValue + 1, int.MaxValue - 1, GetOpposite(botColor));

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = candidate;

                        if (reportThoughts)
                        {
                            OnThoughtUpdated?.Invoke(new BotThought
                            {
                                CurrentCandidate = candidate,
                                BestMoveSoFar = bestMove,
                                Score = bestScore,
                                Depth = MaxDepth,
                                AnalysisNote = $"Нашел вариант лучше: {bestMove} (оценка: {bestScore / 100.0:+0.00;-0.00;0.00})"
                            });
                        }
                    }
                }

                // Обеспечиваем минимальную задержку, если бот подумал слишком быстро
                int elapsed = (int)(DateTime.UtcNow - startTime).TotalMilliseconds;
                if (elapsed < ThinkDelayMs)
                {
                    await Task.Delay(ThinkDelayMs - elapsed, ct);
                }

                return bestMove;
            }, ct);
        }

        private int Minimax(Chessboard board, int depth, int alpha, int beta, PieceColor color)
        {
            if (depth == 0)
                return EvaluateBoard(board, color);

            List<BotMove> moves = GenerateAllMoves(board, color);
            if (moves.Count == 0)
            {
                // Если нет ходов: мат (-100000) или пат (0)
                return board.IsKingCheck(color) ? -100000 - depth : 0;
            }

            int maxEval = int.MinValue + 1;
            foreach (var move in moves)
            {
                Chessboard clone = board.Clone();
                ApplyMove(clone, move);

                int eval = -Minimax(clone, depth - 1, -beta, -alpha, GetOpposite(color));
                maxEval = Math.Max(maxEval, eval);
                alpha = Math.Max(alpha, eval);

                if (alpha >= beta)
                    break; // Альфа-бета отсечение
            }
            return maxEval;
        }

        private int EvaluateBoard(Chessboard board, PieceColor perspective)
        {
            int score = 0;

            foreach (Piece p in board.GetPieces(PieceColor.White))
            {
                score += GetPieceValue(p.type);
                score += PieceSquareTables.GetPositionalBonus(p.type, PieceColor.White, p.Position);
            }

            foreach (Piece p in board.GetPieces(PieceColor.Black))
            {
                score -= GetPieceValue(p.type);
                score -= PieceSquareTables.GetPositionalBonus(p.type, PieceColor.Black, p.Position);
            }

            return perspective == PieceColor.White ? score : -score;
        }

        private int GetPieceValue(PieceType type) => type switch
        {
            PieceType.Pawn => 100,
            PieceType.Knight => 320,
            PieceType.Bishop => 330,
            PieceType.Rook => 500,
            PieceType.Queen => 900,
            PieceType.King => 20000,
            _ => 0
        };

        private List<BotMove> GenerateAllMoves(Chessboard board, PieceColor color)
        {
            var captures = new List<BotMove>();
            var quietMoves = new List<BotMove>();

            foreach (Piece piece in board.GetPieces(color))
            {
                foreach (Vector2 target in piece.GetValidMoves())
                {
                    bool isCapture = !board[target].IsEmpty || (piece.type == PieceType.Pawn && board[target].EnPassantTarget);
                    var targetList = isCapture ? captures : quietMoves;

                    if (piece.type == PieceType.Pawn && (target.Y == 7 || target.Y == 0))
                    {
                        targetList.Add(new BotMove(piece.Position, target, PromotionPiece.Queen));
                        targetList.Add(new BotMove(piece.Position, target, PromotionPiece.Knight));
                    }
                    else
                    {
                        targetList.Add(new BotMove(piece.Position, target));
                    }
                }
            }

            // Взятия ставим в начало списка для максимальной эффективности Альфа-Бета отсечения
            captures.AddRange(quietMoves);
            return captures;
        }

        private void ApplyMove(Chessboard board, BotMove move)
        {
            board.Move(move.From, move.To);
            if (move.Promotion.HasValue && board[move.To].CurrentPiece is Pawn pawn)
            {
                Piece promoted = move.Promotion.Value switch
                {
                    PromotionPiece.Rook => new Rook(move.To, pawn.color, board, false),
                    PromotionPiece.Bishop => new Bishop(move.To, pawn.color, board, false),
                    PromotionPiece.Knight => new Knight(move.To, pawn.color, board, false),
                    _ => new Queen(move.To, pawn.color, board, false)
                };
                board.AddPiece(promoted);
                board.UpdateAttackedSquares();
            }
        }

        private PieceColor GetOpposite(PieceColor color) =>
            color == PieceColor.White ? PieceColor.Black : PieceColor.White;
    }
}
