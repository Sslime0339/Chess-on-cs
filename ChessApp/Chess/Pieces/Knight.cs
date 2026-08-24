using System.Collections.Generic;



namespace Chess
{
    class Knight : Piece
    {


        public Knight(Vector2 position, PieceColor color, Chessboard board, bool firstMove = true) : base(position, color, board, PieceType.Knight, firstMove)
        {

        }

        public override Piece Clone(Chessboard newBoard)
        {
            return new Knight(_position, color, newBoard, _firstMove);
        }


        // TODO:
        public override List<Vector2> GetAllMoves()
        {
            return GetAllKnightMove();
        }

        // TODO:
        public override List<Vector2> GetAttackedPositions()
        {
            return GetAllKnightMove();
        }

        private List<Vector2> GetAllKnightMove()
        {
            List<Vector2> result = new List<Vector2>();

            CheckEmptyOrEnemyAndAdd(_position + new Vector2(-1, 2), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(1, 2), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(2, 1), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(2, -1), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(1, -2), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(-1, -2), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(-2, -1), result);
            CheckEmptyOrEnemyAndAdd(_position + new Vector2(-2, 1), result);

            return result;
        }
    }
}