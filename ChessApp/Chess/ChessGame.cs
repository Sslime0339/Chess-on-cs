using System.Collections.Generic;


using System;

namespace Chess
{
    class ChessGame
    {
        private Chessboard _board;

        private PieceColor _currentTurn;
        public PieceColor CurrentTurn => _currentTurn;
        //public PieceColor OppositeTurn => GetOppositeColor(_currentTurn);

        private bool _isGameOver;
        public bool IsGameOver => _isGameOver;

        private bool _promotionRequired;
        public bool PromotionRequired => _promotionRequired;



        public ChessGame()
        {
            _board = new Chessboard();
            _currentTurn = PieceColor.White;
            _isGameOver = false;
        }


        public MoveResult Move(Vector2 from, Vector2 to)
        {
            if (_isGameOver || _promotionRequired)
            {
                // TODO: 
            }

            if (_board[from].IsEmpty)
                return MoveResult.IllegalMove;

            Piece piece = _board[from].CurrentPiece;

            if (piece.color != _currentTurn)
                return MoveResult.IllegalMove;

            List<Vector2> validMoves = piece.GetValidMoves();

            if (validMoves.Contains(to))
            {
                //DrawEnPassantTargetSquare();
                _board.Move(from, to);



                //DrawEnPassantTargetSquare();
                ChangeCurrentTurn();


                // TODO: сделать прверку на шах и шахимат тут
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
                
                if (_board.IsKingCheck(_currentTurn))
                {
                    return MoveResult.Check;
                }


                // Console.WriteLine(_board.IsKingCheck(PieceColor.White));
                // Console.WriteLine(_board.IsKingCheck(PieceColor.Black));

                // DrawAttackedSquare(PieceColor.White);
                // DrawAttackedSquare(PieceColor.Black);

                return MoveResult.Success;
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
    }
}