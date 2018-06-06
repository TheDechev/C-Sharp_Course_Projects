using System;

namespace Checkers_Logic
{
    public class Move
    {
        public const int k_ZeroObligatoryMoves = 0;

        private Square m_squareFrom;
        private Square m_squareTo;

        public static Move Parse(string i_Move)
        {
            Move parseMove = null;

            if (i_Move != string.Empty && i_Move.Length == 5)
            {
                Square currentSquare = new Square();
                Square nextMoveSquare = new Square();

                currentSquare.updateSquareWithString(i_Move.Substring(0, 2));
                nextMoveSquare.updateSquareWithString(i_Move.Substring(3, 2));

                parseMove = new Move(currentSquare, nextMoveSquare);
            }

            return parseMove;
        }

        public Move(Square i_From, Square i_To)
        {
            this.m_squareFrom = i_From;
            this.m_squareTo = i_To;
        }

        public Move()
        {
            this.m_squareFrom = null;
            this.m_squareTo = null;
        }

        public Square SquareFrom
        {
            get
            {
                return this.m_squareFrom;
            }
        }

        public Square SquareTo
        {
            get
            {
                return this.m_squareTo;
            }
        }

        public static bool operator ==(Move i_MoveOne, Move i_MoveTwo)
        {
            return i_MoveOne.m_squareFrom == i_MoveTwo.m_squareFrom && i_MoveOne.m_squareTo == i_MoveTwo.m_squareTo;
        }

        public static bool operator !=(Move i_MoveOne, Move i_MoveTwo)
        {
            return i_MoveOne.m_squareFrom != i_MoveTwo.m_squareFrom || i_MoveOne.m_squareTo != i_MoveTwo.m_squareTo;
        }

        public override bool Equals(object i_Move)
        {
            if (i_Move == null || i_Move.GetType() != GetType() )
            {
                return false;
            }

            Move movObj = (Move)i_Move;
            return this == movObj;
        }

        public override string ToString()
        {
            string moveString = string.Empty;

            moveString += (char)(this.m_squareFrom.Col + 'A');
            moveString += (char)(this.m_squareFrom.Row + 'a');

            moveString += ">";
            moveString += (char)(this.m_squareTo.Col + 'A');
            moveString += (char)(this.m_squareTo.Row + 'a');

            return moveString;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode(); 
        }
    }
}
