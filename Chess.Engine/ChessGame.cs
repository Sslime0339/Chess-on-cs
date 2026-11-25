using System.Collections.Generic;
using System;



using Sslime0339.Chess.Pieces;


// TODO: Сделать поддержку Portable Game Notation (PGN) (стандартная запись шахматной игры).

// TODO: Сделать сохранение игры.

namespace Sslime0339.Chess
{
    public class ChessGame
    {
        private Chessboard _board;

        private PieceColor _currentTurn;
        public PieceColor CurrentTurn => _currentTurn;
        //public PieceColor OppositeTurn => GetOppositeColor(_currentTurn);

        private bool _isGameOver;
        public bool IsGameOver => _isGameOver;

        private bool _promotionRequired;
        public bool PromotionRequired => _promotionRequired;

        private Pawn _pawnPromotionRequired;

        private int _halfMoveClock;
        public int HalfMoveClock => _halfMoveClock;

        private Dictionary<string, int> _positionHistory;


        public ChessGame()
        {
            _board = new Chessboard();
            _currentTurn = PieceColor.White;
            _isGameOver = false;
            _halfMoveClock = 0;

            _positionHistory = new Dictionary<string, int>();
            RecordCurrentPosition();
        }


        private int RecordCurrentPosition()
        {
            string fullFen = SaveToFen();
            // Отбрасываем счетчики полуходов и номера хода в конце FEN
            string[] parts = fullFen.Split(' ');
            string positionKey = $"{parts[0]} {parts[1]} {parts[2]} {parts[3]}";

            if (_positionHistory.TryGetValue(positionKey, out int count))
            {
                _positionHistory[positionKey] = count + 1;
                return count + 1;
            }

            _positionHistory[positionKey] = 1;
            return 1;
        }



        public MoveResult Move(Vector2 from, Vector2 to)
        {
            if (_isGameOver)
                return MoveResult.GameOver;

            if (_promotionRequired)
                return MoveResult.PromotionRequired;
            

            if (_board[from].IsEmpty)
                return MoveResult.IllegalMove;

            Piece piece = _board[from].CurrentPiece;

            if (piece.color != _currentTurn)
                return MoveResult.IllegalMove;

            List<Vector2> validMoves = piece.GetValidMoves();

            if (validMoves.Contains(to))
            {
                // Для правила 50 шагов
                bool isPawnMove = piece.type == PieceType.Pawn;
                bool isCapture = !_board[to].IsEmpty || (isPawnMove && _board[to].EnPassantTarget);

                if (isPawnMove || isCapture)
                {
                    _halfMoveClock = 0;
                    // Оптимизация: после взятия или хода пешкой старые позиции уже никогда не повторятся
                    _positionHistory.Clear();
                }
                else
                    _halfMoveClock++;




                //DrawEnPassantTargetSquare();
                _board.Move(from, to);



                // короче обращаюсь к доске на проверку какая пешка дошла до конца 
                // сдругой стороны почему доска должна заботиться о каких-то пешках
                // хотя она находит короля, то есть она уже реагирует на конкретные фигуры
                // ну так и доска конкретна

                // всё доска ищет пешки которые дошли до конца

                // главное сторона не меняеться, она поменяеться после преобразовании пешки
                if (_board.TryGetPawnRequiringPromotion(_currentTurn, out Pawn pawn))
                {
                    _pawnPromotionRequired = pawn;
                    _promotionRequired = true;
                    return MoveResult.PromotionRequired;
                }


                return FinalizeTurn();

            }

            if (validMoves.Count == 0)
            {
                if (_board.GetAllValidMoves(_currentTurn).Count != 0)
                    return MoveResult.IllegalMove;
                //else if (_board.IsKingCheck(_currentTurn))
                    //return MoveResult.
            }


            return MoveResult.IllegalMove;
            //return MoveResult.PromotionRequired;
        }


        
        private MoveResult FinalizeTurn()
        {
            ChangeCurrentTurn();


            // Записываем новую позицию после смены очереди хода
            int repetitions = RecordCurrentPosition();



            if (_board.GetAllValidMoves(_currentTurn).Count == 0)
            {
                _isGameOver = true;
                if (_board.IsKingCheck(_currentTurn))
                {
                    return MoveResult.Checkmate;
                }
                else
                {
                    return MoveResult.Stalemate;
                }
            }

            // для правила 50 шагов
            if (repetitions >= 3 || _halfMoveClock >= 100)
            {
                _isGameOver = true;
                return MoveResult.Draw;
            }

            if (_board.IsKingCheck(_currentTurn))
            {
                return MoveResult.Check;
            }

            return MoveResult.Success;
        }







        public MoveResult ToPromote(PromotionPiece promotionPiece)
        {
            if (!_promotionRequired) return MoveResult.IllegalMove;

            Vector2 pos = _pawnPromotionRequired.Position;
            PieceColor color = _pawnPromotionRequired.color;

            switch (promotionPiece)
            {
                case PromotionPiece.Queen:  _board.AddPiece(new Queen(pos, color, _board, false));  break;
                case PromotionPiece.Rook:   _board.AddPiece(new Rook(pos, color, _board, false));   break;
                case PromotionPiece.Bishop: _board.AddPiece(new Bishop(pos, color, _board, false)); break;
                case PromotionPiece.Knight: _board.AddPiece(new Knight(pos, color, _board, false)); break;
            }

            _promotionRequired = false;
            _pawnPromotionRequired = null;


            _board.UpdateAttackedSquares();

            // Передаем ход и проверяем шах, мат или пат
            // после изменения пешки передаём ход противнику
            return FinalizeTurn();
        }

        /*
        void DrawAttackedSquare(PieceColor color)
        {
            Console.WriteLine();
            for (int i = 7; i >= 0; i--)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (_board[j, i].IsAttacked(color))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write("•");
                    }

                }
                Console.Write("\n");
            }
        }
        */
        /*
        
                void DrawEnPassantTargetSquare()
                {
                    Console.WriteLine();
                    for (int i = 7; i >= 0; i--)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (_board[j, i].EnPassantTarget)
                            {
                                Console.Write("#");
                            }
                            else
                            {
                                Console.Write("•");
                            }

                        }
                        Console.Write("\n");
                    }
                }
        */

        public string SaveToFen()
        {
            return FenSerializer.Serialize(_board, _currentTurn, _halfMoveClock);
        }

        public void LoadFromFen(string fen)
        {
            FenSerializer.Deserialize(fen, out Chessboard loadedBoard, out PieceColor loadedTurn);
            _board = loadedBoard;
            _currentTurn = loadedTurn;
            _promotionRequired = false;
            _pawnPromotionRequired = null;
            _halfMoveClock = 0;

            string[] parts = fen.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 5 && int.TryParse(parts[4], out int parsedClock))
            {
                _halfMoveClock = parsedClock;
            }

            _positionHistory.Clear();
            RecordCurrentPosition();


            _isGameOver = _board.GetAllValidMoves(_currentTurn).Count == 0 || _halfMoveClock >= 100;


        }

        public void SaveToFile(string filePath)
        {
            System.IO.File.WriteAllText(filePath, SaveToFen());
        }

        public void LoadFromFile(string filePath)
        {
            string fen = System.IO.File.ReadAllText(filePath);
            LoadFromFen(fen);
        }


        // В Chess.Engine\ChessGame.cs
        internal Chessboard GetBoardClone()
        {
            return _board.Clone();
        }




        public SquareInfo[,] GetBoardData()
        {
            return _board.GetData();
        }


        private void ChangeCurrentTurn()
        {
            _currentTurn = GetOppositeColor(_currentTurn);
        }

        private PieceColor GetOppositeColor(PieceColor color)
        {
            if (color == PieceColor.White)
            {
                return PieceColor.Black;
            }
            else
            {
                return PieceColor.White;
            }
        }


        public Move GetLastMove()
        {
            // TODO: Сделать функцию которая возвращает полную информацию о ходе, и придумать ей нормальное название
            return new Move();
            
        }
        
    }
}