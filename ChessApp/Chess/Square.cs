



namespace Chess
{
    class Square
    {
        private Piece? _piece;
        public Piece? CurrentPiece => _piece;

        public bool IsEmpty => _piece == null;

        public bool IsAttackedWhite { get; set; }
        public bool IsAttackedBlack { get; set; }

        public bool EnPassantTarget { get; set; }

        public Piece? EnPassantTargetPiece { get; set; }



        public void ResetEnPassantTarget()
        {
            EnPassantTarget = false;
            EnPassantTargetPiece = null;
        }

        public void SetEnPassantTarget(Piece piece)
        {
            EnPassantTarget = true;
            EnPassantTargetPiece = piece;
        }


        public bool IsAttacked(PieceColor attackedColor)
        {
            if (attackedColor == PieceColor.White)
                return IsAttackedWhite;
            else
                return IsAttackedBlack;
        }

        public void SetAttacked(PieceColor attackedColor)
        {
            if (attackedColor == PieceColor.White)
                IsAttackedWhite = true;
            else
                IsAttackedBlack = true;
        }

        public void ResetAttacked()
        {
            IsAttackedWhite = false;
            IsAttackedBlack = false;
        }

        // хз вроде не нужен
        //public readonly Vector2 position;

        public void AddPiece(Piece piece)
        {
            _piece = piece;
        }

        public Piece RemovePiece()
        {
            Piece piece = _piece;
            _piece = null;
            return piece;
        }


        public Square Clone(Chessboard newBoard)
        {
            Square newSquare = new Square();
            if (!IsEmpty)
                newSquare.AddPiece(_piece.Clone(newBoard));
            newSquare.IsAttackedWhite = IsAttackedWhite;
            newSquare.IsAttackedBlack = IsAttackedBlack;

            return newSquare;
        }



        public SquareInfo GetSquareInfo()
        {
            if (_piece == null)
                return SquareInfo.EmptySquare();
            else
                return new SquareInfo(_piece.type, _piece.color, _piece.FirstMove);

        }
    }
}