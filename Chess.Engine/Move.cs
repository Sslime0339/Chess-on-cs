using Sslime0339.Chess.Pieces;

namespace Sslime0339.Chess
{

    public class Move
    {
        // TODO: Сделать класс Move который хранит ВСЮ информацию о ходе

        public MoveResult moveResult;

        public Vector2 start;

        public Vector2 end;

        public bool eatPiece;

        public bool isPromotion;

        public PieceType toPiecePromoting;
    }
}