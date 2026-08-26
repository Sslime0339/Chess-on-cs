using System.Collections.Generic;



namespace Chess
{
    class Rook : Piece
    {

        public Rook(Vector2 position, PieceColor color, Chessboard board, bool firstMove = true) : base(position, color, board, PieceType.Rook, firstMove)
        {

        }

        public override Piece Clone(Chessboard newBoard)
        {
            return new Rook(_position, color, newBoard, _firstMove);
        }


        public override List<Vector2> GetAllMoves()
        {
            return GetOrthogonalLines(_position);
        }


        public override List<Vector2> GetAttackedPositions()
        {
            return GetOrthogonalLines(_position);
        }
    }
}