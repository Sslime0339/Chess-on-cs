using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

using Sslime0339.Chess;



namespace Sslime0339.Chess.Bot
{
    public struct BotMove
    {
        public Vector2 From { get; }
        public Vector2 To { get; }
        public PromotionPiece? Promotion { get; }

        public BotMove(Vector2 from, Vector2 to, PromotionPiece? promotion = null)
        {
            From = from;
            To = to;
            Promotion = promotion;
        }

        public override string ToString()
        {
            char fX = (char)('a' + From.X);
            char tX = (char)('a' + To.X);
            string promo = Promotion.HasValue ? $"={Promotion.Value}" : "";
            return $"{fX}{From.Y + 1}->{tX}{To.Y + 1}{promo}";
        }
    }

    // Информация о текущем размышлении бота
    public class BotThought
    {
        public BotMove CurrentCandidate { get; set; } // Какой ход сейчас анализирует
        public BotMove BestMoveSoFar { get; set; }    // Лучший найденный ход на данный момент
        public int Score { get; set; }                // Оценка позиции (в пешках * 100)
        public int Depth { get; set; }                // Глубина просчета
        public string AnalysisNote { get; set; }      // Текстовое пояснение для UI
    }
}
