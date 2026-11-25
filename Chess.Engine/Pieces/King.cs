using System.Collections.Generic;



namespace Sslime0339.Chess.Pieces
{
    class King : Piece
    {

        public King(Vector2 position, PieceColor color, Chessboard board, bool firstMove = true) : base(position, color, board, PieceType.King, firstMove)
        {

        }

        public override Piece Clone(Chessboard newBoard)
        {
            return new King(_position, color, newBoard, _firstMove);
        }



        public override List<Vector2> GetAllMoves()
        {
            List<Vector2> result = GetAllKingMove();
            AddCastlingMoves(result);
            return result;
        }


        public override List<Vector2> GetAttackedPositions()
        {
            // поля рокировки НЕ считаются атакованными королем
            return GetAllKingMove();
        }


        public override void BeforeMove(Vector2 newPosition)
        {
            // Проверяем, является ли ход рокировкой (сдвиг по X на 2 клетки)
            int deltaX = newPosition.X - _position.X;
            if (Math.Abs(deltaX) == 2)
            {
                int y = _position.Y;
                if (deltaX > 0) // Короткая рокировка (вправо)
                {
                    MoveRookForCastling(new Vector2(7, y), new Vector2(5, y));
                }
                else // Длинная рокировка (влево)
                {
                    MoveRookForCastling(new Vector2(0, y), new Vector2(3, y));
                }
            }

            base.BeforeMove(newPosition);
        }


        private void MoveRookForCastling(Vector2 rookFrom, Vector2 rookTo)
        {
            Piece rook = _board[rookFrom].RemovePiece();
            _board[rookTo].AddPiece(rook);
            rook.AfterMove(rookTo); // Обновляет позицию ладьи и ставит ей _firstMove = false
        }

        private void AddCastlingMoves(List<Vector2> result)
        {
            // 1. Король не должен был ходить ранее
            if (!_firstMove)
                return;

            PieceColor opponentColor = color == PieceColor.White ? PieceColor.Black : PieceColor.White;

            // 2. Король не должен находиться под шахом
            if (_board[_position].IsAttacked(opponentColor))
                return;

            int y = _position.Y;

            // 3. Проверка короткой рокировки (ладья на x = 7)
            if (CanCastle(rookX: 7, direction: 1, y, opponentColor))
            {
                result.Add(new Vector2(_position.X + 2, y));
            }

            // 4. Проверка длинной рокировки (ладья на x = 0)
            if (CanCastle(rookX: 0, direction: -1, y, opponentColor))
            {
                result.Add(new Vector2(_position.X - 2, y));
            }
        }

        private bool CanCastle(int rookX, int direction, int y, PieceColor opponentColor)
        {
            // Проверяем наличие своей ладьи, которая еще не ходила
            Square rookSquare = _board[rookX, y];
            if (rookSquare.IsEmpty)
                return false;

            Piece rook = rookSquare.CurrentPiece;
            if (rook.type != PieceType.Rook || rook.color != color || !rook.FirstMove)
                return false;

            // Проверяем, что все клетки между королем и ладьей пусты
            for (int x = _position.X + direction; x != rookX; x += direction)
            {
                if (!_board[x, y].IsEmpty)
                    return false;
            }

            // Проверяем, что две клетки, через которые проходит и на которые встает король, не атакованы
            for (int step = 1; step <= 2; step++)
            {
                int checkX = _position.X + direction * step;
                if (_board[checkX, y].IsAttacked(opponentColor))
                    return false;
            }

            return true;
        }






        private List<Vector2> GetAllKingMove()
        {
            List<Vector2> result = new List<Vector2>();

            CheckEmptyOrEnemyAndAdd(_position + new Vector2(1, 1), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(1, 0), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(1, -1), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(0, -1), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(-1, -1), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(-1, 0), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(-1, 1), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(0, 1), result);

            return result;
        }
    }
}