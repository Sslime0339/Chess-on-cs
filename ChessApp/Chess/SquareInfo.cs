using System;


namespace Chess
{

    struct SquareInfo
    {
        // эта главная переменная
        public bool IsEmpty { get; }

        private readonly PieceType? _piece;
        public PieceType? Piece => IsEmpty ? null : _piece;

        private readonly PieceColor? _color;
        public PieceColor? Color => IsEmpty ? null : _color;

        private readonly bool? _firstMove;
        public bool? FirstMove => IsEmpty ? null : _firstMove;
        

        public SquareInfo(PieceType piece, PieceColor color, bool firstMove)
        {
            this.IsEmpty = false;
            this._piece = piece;
            this._color = color;
            this._firstMove = firstMove;
        }

        //чтобы получить нужно использовать SquareInfo.EmptySquare()
        private SquareInfo(bool isEmpty)
        {
            this.IsEmpty = true;
            this._piece = null;
            this._color = null;
            this._firstMove = null;
        }

        public static SquareInfo EmptySquare()
        {
            return new SquareInfo(true);
        }

    }
}