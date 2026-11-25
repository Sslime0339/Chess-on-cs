using System;
using System.Text;
using Sslime0339.Chess.Pieces;

namespace Sslime0339.Chess
{
    internal static class FenSerializer
    {
        public static string Serialize(Chessboard board, PieceColor currentTurn, int halfMoveClock = 0)
        {
            StringBuilder fen = new StringBuilder();

            // 1. Расстановка фигур (с 8-й горизонтали вниз до 1-й)
            for (int y = 7; y >= 0; y--)
            {
                int emptyCount = 0;
                for (int x = 0; x < 8; x++)
                {
                    Square square = board[x, y];
                    if (square.IsEmpty)
                    {
                        emptyCount++;
                    }
                    else
                    {
                        if (emptyCount > 0)
                        {
                            fen.Append(emptyCount);
                            emptyCount = 0;
                        }
                        fen.Append(GetPieceChar(square.CurrentPiece));
                    }
                }
                if (emptyCount > 0)
                    fen.Append(emptyCount);

                if (y > 0)
                    fen.Append('/');
            }

            // 2. Очередь хода
            fen.Append(currentTurn == PieceColor.White ? " w " : " b ");

            // 3. Права на рокировку (на основе FirstMove короля и ладей)
            StringBuilder castling = new StringBuilder();
            if (CanCastleSide(board, PieceColor.White, 0, 7)) castling.Append('K');
            if (CanCastleSide(board, PieceColor.White, 0, 0)) castling.Append('Q');
            if (CanCastleSide(board, PieceColor.Black, 7, 7)) castling.Append('k');
            if (CanCastleSide(board, PieceColor.Black, 7, 0)) castling.Append('q');
            fen.Append(castling.Length > 0 ? castling.ToString() : "-");

            // 4. Поле взятия на проходе
            fen.Append(' ');
            string enPassantSquare = "-";
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (board[x, y].EnPassantTarget)
                    {
                        enPassantSquare = $"{(char)('a' + x)}{y + 1}";
                        break;
                    }
                }
            }
            fen.Append(enPassantSquare);

            // 5. Счетчики полуходов и номера хода (заглушки по стандарту FEN)
            fen.Append($" {halfMoveClock} 1");

            return fen.ToString();
        }

        public static void Deserialize(string fen, out Chessboard board, out PieceColor currentTurn)
        {
            string[] parts = fen.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 4)
                throw new ArgumentException("Некорректный формат FEN-строки.");

            board = Chessboard.CreateEmpty();

            // 1. Расстановка фигур
            string[] ranks = parts[0].Split('/');
            if (ranks.Length != 8)
                throw new ArgumentException("В FEN должно быть 8 горизонталей.");

            for (int r = 0; r < 8; r++)
            {
                int y = 7 - r;
                int x = 0;
                foreach (char c in ranks[r])
                {
                    if (char.IsDigit(c))
                    {
                        x += c - '0';
                    }
                    else
                    {
                        Piece piece = CreatePieceFromChar(c, new Vector2(x, y), board);
                        board.AddPiece(piece);
                        x++;
                    }
                }
            }

            // 2. Очередь хода
            currentTurn = parts[1] == "b" ? PieceColor.Black : PieceColor.White;

            // 3. Восстановление флагов FirstMove для рокировки
            string castling = parts[2];
            SetCastlingRights(board, PieceColor.White, 0, castling.Contains('K'), castling.Contains('Q'));
            SetCastlingRights(board, PieceColor.Black, 7, castling.Contains('k'), castling.Contains('q'));

            // 4. Восстановление взятия на проходе
            string ep = parts[3];
            if (ep != "-" && ep.Length == 2)
            {
                int epX = ep[0] - 'a';
                int epY = ep[1] - '1';
                int pawnY = epY == 2 ? 3 : 4; // Пешка стоит на 4-й (индекс 3) или 5-й (индекс 4) горизонтали
                board[epX, epY].SetEnPassantTarget(new Vector2(epX, pawnY));
            }

            board.UpdateAttackedSquares();
        }

        private static char GetPieceChar(Piece piece)
        {
            char c = piece.type switch
            {
                PieceType.Pawn => 'p',
                PieceType.Knight => 'n',
                PieceType.Bishop => 'b',
                PieceType.Rook => 'r',
                PieceType.Queen => 'q',
                PieceType.King => 'k',
                _ => '?'
            };
            return piece.color == PieceColor.White ? char.ToUpper(c) : c;
        }

        private static Piece CreatePieceFromChar(char c, Vector2 pos, Chessboard board)
        {
            PieceColor color = char.IsUpper(c) ? PieceColor.White : PieceColor.Black;
            char lower = char.ToLower(c);

            // Для пешек FirstMove зависит от стартовой линии; для короля и ладей выставим позже по правам рокировки
            bool isPawnStart = (color == PieceColor.White && pos.Y == 1) || (color == PieceColor.Black && pos.Y == 6);

            return lower switch
            {
                'p' => new Pawn(pos, color, board, isPawnStart),
                'n' => new Knight(pos, color, board, false),
                'b' => new Bishop(pos, color, board, false),
                'r' => new Rook(pos, color, board, false),
                'q' => new Queen(pos, color, board, false),
                'k' => new King(pos, color, board, false),
                _ => throw new ArgumentException($"Неизвестный символ фигуры: {c}")
            };
        }

        private static bool CanCastleSide(Chessboard board, PieceColor color, int y, int rookX)
        {
            Piece king = board[4, y].CurrentPiece;
            Piece rook = board[rookX, y].CurrentPiece;

            return king is King && king.color == color && king.FirstMove &&
                   rook is Rook && rook.color == color && rook.FirstMove;
        }

        private static void SetCastlingRights(Chessboard board, PieceColor color, int y, bool kingSide, bool queenSide)
        {
            if (!kingSide && !queenSide) return;

            if (board[4, y].CurrentPiece is King king && king.color == color)
                king.SetFirstMove(true);

            if (kingSide && board[7, y].CurrentPiece is Rook rookK && rookK.color == color)
                rookK.SetFirstMove(true);

            if (queenSide && board[0, y].CurrentPiece is Rook rookQ && rookQ.color == color)
                rookQ.SetFirstMove(true);
        }
    }
}
