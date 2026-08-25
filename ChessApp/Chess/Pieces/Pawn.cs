using System.Collections.Generic;
using System;


namespace Chess
{
    class Pawn : Piece
    {


        public Pawn(Vector2 position, PieceColor color, Chessboard board, bool firstMove = true) : base(position, color, board, PieceType.Pawn, firstMove)
        {

        }

        public override void BeforeMove(Vector2 newPosition)
        {
            if (_board[newPosition].EnPassantTarget)
            {
                Piece pieceToRemove = _board[newPosition].EnPassantTargetPiece;
                _board[pieceToRemove.Position].RemovePiece();
            }

            base.BeforeMove(newPosition);
        }

        public override void AfterMove(Vector2 newPosition)
        {
            if (_firstMove && IsTwoSquarePawnMove(_position, newPosition))
            {
                Vector2 forward = GetForward();


                _board[_position + forward].SetEnPassantTarget(this);
            }


            base.AfterMove(newPosition);
        }

        public bool IsTwoSquarePawnMove(Vector2 startPosition, Vector2 endPosition)
        {
            if (Math.Abs(startPosition.Y - endPosition.Y) == 2)
            {
                return true;
            }
            return false;
        }

        public override Piece Clone(Chessboard newBoard)
        {
            return new Pawn(_position, color, newBoard, _firstMove);
        }

        // TODO:
        public override List<Vector2> GetAllMoves()
        {
            List<Vector2> result = new List<Vector2>();

            Vector2 forward = GetForward();

            if (CheckEmptyAndAdd(_position + forward, result))
            {

                // проверено две клетки перед пешкой
                if (_firstMove && CheckEmpty(_position + forward * 2))
                {
                    result.Add(_position + forward * 2);
                }
            }

            CheckEnemyOrEnPassantTarget(_position + forward + new Vector2(1, 0), result);

            CheckEnemyOrEnPassantTarget(_position + forward + new Vector2(-1, 0), result);


            return result;
        }

        // TODO:
        public override List<Vector2> GetAttackedPositions()
        {
            List<Vector2> result = new List<Vector2>();

            Vector2 forward = GetForward(); 

            OnBoardAndAdd(_position + forward + new Vector2(1, 0), result);

            OnBoardAndAdd(_position + forward + new Vector2(-1, 0), result);


            return result;
        }

        public Vector2 GetForward()
        {
            Vector2 forward;
            if (color == PieceColor.White)
                forward = new Vector2(0, 1);
            else
                forward = new Vector2(0, -1);
            return forward;
        }

        public bool CheckEnemyOrEnPassantTarget(Vector2 position, List<Vector2> list)
        {
            if (CheckEnemyAndAdd(position, list))
                return true;
            if (OnBoard(position) && _board[position].EnPassantTarget)
            {
                list.Add(position);
                return true;
            }
            return false;

        }


    }
}