using System.Collections.Generic;



namespace Chess
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
            return GetAllKingMove();
        }


        public override List<Vector2> GetAttackedPositions()
        {
            return GetAllKingMove();
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