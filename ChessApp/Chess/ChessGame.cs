using System.Collections.Generic;


using System;

namespace Chess
{
    class ChessGame
    {
        private Chessboard _board;

        private PieceColor _currentTurn;
        public PieceColor CurrentTurn => _currentTurn;

        private bool _isGameOver;
        public bool IsGameOver => _isGameOver;


        public ChessGame()
        {
            _board = new Chessboard();
            _currentTurn = PieceColor.White;
            _isGameOver = false;
        }


        public MoveResult Move(Vector2 from, Vector2 to)
        {
            if (_board[from].IsEmpty)
                return MoveResult.IllegalMove;

            Piece piece = _board[from].CurrentPiece;

            if (piece.color != _currentTurn)
                return MoveResult.IllegalMove;

            List<Vector2> validMoves = piece.GetValidMoves();

            if (validMoves.Contains(to))
            {
                _board.Move(from, to);
                ChangeCurrentTurn();
                // Console.WriteLine(_board.IsKingCheck(PieceColor.White));
                // Console.WriteLine(_board.IsKingCheck(PieceColor.Black));

                // DrawAttackedSquare(PieceColor.White);
                // DrawAttackedSquare(PieceColor.Black);

                return MoveResult.Success;
            }

            if (validMoves.Count == 0)
            {
                _board.GetAllMoves();
            }


            return MoveResult.IllegalMove;
            //return MoveResult.PromotionRequired;
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


        public SquareInfo[,] GetBoardData()
        {
            return _board.GetData();
        }


        private void ChangeCurrentTurn()
        {
            if (_currentTurn == PieceColor.White)
            {
                _currentTurn = PieceColor.Black;
            }
            else
            {
                _currentTurn = PieceColor.White;
            }
        }
    }
}