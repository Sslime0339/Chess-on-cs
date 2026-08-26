using System.Collections.Generic;
using System;


// TODO: сделать сохранение игровой доски
namespace Chess
{
    class Chessboard
    {
        private Square[,] _board;


        public Square this[int x, int y]
        {
            get
            {
                return _board[x, y];
            }
        }


        public Square this[Vector2 vec]
        {
            get
            {
                return this[vec.X, vec.Y];
            }
        }



        public Chessboard()
        {
            _board = new Square[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _board[i, j] = new Square();
                }
            }


            for (int i = 0; i < 8; i++)
                AddPiece(new Pawn(new Vector2(i, 1), PieceColor.White, this));

            for (int i = 0; i < 8; i++)
                AddPiece(new Pawn(new Vector2(i, 6), PieceColor.Black, this));

            // тест
            // AddPiece(new Pawn(new Vector2(3, 2), PieceColor.Black, this));

            AddPiece(new Rook(new Vector2(0, 0), PieceColor.White, this));
            AddPiece(new Rook(new Vector2(7, 0), PieceColor.White, this));
            AddPiece(new Knight(new Vector2(1, 0), PieceColor.White, this));
            AddPiece(new Knight(new Vector2(6, 0), PieceColor.White, this));
            AddPiece(new Bishop(new Vector2(2, 0), PieceColor.White, this));
            AddPiece(new Bishop(new Vector2(5, 0), PieceColor.White, this));
            AddPiece(new Queen(new Vector2(3, 0), PieceColor.White, this));
            AddPiece(new King(new Vector2(4, 0), PieceColor.White, this));

            AddPiece(new Rook(new Vector2(0, 7), PieceColor.Black, this));
            AddPiece(new Rook(new Vector2(7, 7), PieceColor.Black, this));
            AddPiece(new Knight(new Vector2(1, 7), PieceColor.Black, this));
            AddPiece(new Knight(new Vector2(6, 7), PieceColor.Black, this));
            AddPiece(new Bishop(new Vector2(2, 7), PieceColor.Black, this));
            AddPiece(new Bishop(new Vector2(5, 7), PieceColor.Black, this));
            AddPiece(new Queen(new Vector2(3, 7), PieceColor.Black, this));
            AddPiece(new King(new Vector2(4, 7), PieceColor.Black, this));



            UpdateAttackedSquares();
        }


        private Chessboard(Chessboard boardToCopy)
        {
            _board = new Square[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    _board[i, j] = boardToCopy[i, j].Clone(this);
                }
            }
        }



        public Chessboard Clone()
        {
            return new Chessboard(this);
        }


        public void AddPiece(Piece piece)
        {
            _board[piece.Position.X, piece.Position.Y].AddPiece(piece);
        }


        public void Move(Vector2 from, Vector2 to)
        {
            this[from].CurrentPiece.BeforeMove(to);


            // обновляем карту до того как пешка после перемещения добавит особую клетку для взятия на проходе
            UpdateEnPassantTargetSquares();

            this[to].AddPiece(this[from].RemovePiece());

            this[to].CurrentPiece.AfterMove(to);

            UpdateAttackedSquares();
        }


        public List<Piece> GetPieces(PieceColor color)
        {
            List<Piece> result = new List<Piece>();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (!this[i, j].IsEmpty && this[i, j].CurrentPiece.color == color)
                        result.Add(this[i, j].CurrentPiece);
                }
            }
            return result;
        }

        
        public bool TryGetPawnRequiringPromotion(PieceColor color, out Pawn pawnRequiringPromotion)
        {
            foreach (Piece piece in GetPieces(color))
            {
                if (piece is Pawn pawn && pawn.CanPromote())
                {
                    pawnRequiringPromotion = pawn;
                    return true;
                }
            }
            pawnRequiringPromotion = null;
            return false;
        }


        public List<Vector2> GetAllValidMoves(PieceColor color)
        {
            List<Vector2> result = new List<Vector2>();

            foreach (Piece piece in GetPieces(color))
            {
                result.AddRange(piece.GetValidMoves());
            }

            return result;
        }


        public Piece GetKing(PieceColor color)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (!this[i, j].IsEmpty && this[i, j].CurrentPiece.color == color && this[i, j].CurrentPiece.type == PieceType.King)
                        return this[i, j].CurrentPiece;
                }
            }
            return null;
        }


        public void UpdateAttackedSquares()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this[i, j].ResetAttacked();
                }
            }


            foreach (PieceColor color in new[] { PieceColor.White, PieceColor.Black })
            {
                foreach (Piece piese in GetPieces(color))
                {
                    foreach (Vector2 attackedPosition in piese.GetAttackedPositions())
                    {
                        this[attackedPosition].SetAttacked(color);
                    }
                }
            }
        }


        public void UpdateEnPassantTargetSquares()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {

                    _board[i, j].ResetEnPassantTarget();
                }
            }
        }


        public bool IsKingCheck(PieceColor kingColor)
        {
            PieceColor opponentColor = kingColor == PieceColor.White ? PieceColor.Black : PieceColor.White;
            Piece king = GetKing(kingColor);

            return this[king.Position].IsAttacked(opponentColor);

            /*
            foreach (Piece piece in GetPiece(opponentColor))
            {
              if (piece.GetAttackedPositions().Contains(king.Position))
                return true;
            }
            return false;
            */
        }


        public bool WouldLeaveKingInCheck(Vector2 from, Vector2 to, PieceColor color)
        {
            Chessboard newBoard = this.Clone();
            newBoard.Move(from, to);
            return newBoard.IsKingCheck(color);
        }


        public SquareInfo[,] GetData()
        {
            SquareInfo[,] data = new SquareInfo[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    data[i, j] = this[i, j].GetSquareInfo();
                }
            }
            return data;
        }

    }
}