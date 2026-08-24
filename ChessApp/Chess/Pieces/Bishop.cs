using System.Collections.Generic;



namespace Chess
{
    class Bishop : Piece
    {


        public Bishop(Vector2 position, PieceColor color, Chessboard board, bool firstMove = true) : base(position, color, board, PieceType.Bishop, firstMove)
        {

        }

        public override Piece Clone(Chessboard newBoard)
        {
            return new Bishop(_position, color, newBoard, _firstMove);
        }


        // TODO:
        public override List<Vector2> GetAllMoves()
        {
            return GetDiagonals(_position);
        }

        // TODO:
        public override List<Vector2> GetAttackedPositions()
        {
            return GetDiagonals(_position);
        }
    }
}