using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B18_Ex02
{
    public class Move
    {
        private Square m_squareFrom;
        private Square m_squareTo;

        public Move(Square from, Square to)
        {
            this.m_squareFrom = from;
            this.m_squareTo = to;
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
    }
}
