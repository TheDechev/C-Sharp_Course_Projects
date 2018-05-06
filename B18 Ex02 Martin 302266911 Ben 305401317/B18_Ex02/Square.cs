using System;

namespace B18_Ex02
{
    public class Square
    {
        private int m_Row;
        private int m_Col;
        private bool m_IsKing = false;

        public enum eSquareType
        {
            invalid = -1,
            none,
            playerOne,
            playerTwo,
            playerPC,
            playerOneKing,
            playerTwoKing
        }

        public static bool operator ==(Square i_SquareOne, Square i_SquareTwo)
        {
            return i_SquareOne.Row == i_SquareTwo.Row && i_SquareOne.Col == i_SquareTwo.Col;
        }

        public static bool operator !=(Square i_SquareOne, Square i_SquareTwo)
        {
            return i_SquareOne.Row != i_SquareTwo.Row || i_SquareOne.Col != i_SquareTwo.Col;
        }

        public Square(int i_Row, int i_Col)
        {
            this.m_Row = i_Row;
            this.m_Col = i_Col;
        }

        public Square()
        {
            this.m_Row = -1;
            this.m_Col = -1;
        }

        public bool IsKing
        {
            get
            {
                return this.m_IsKing;
            }

            set
            {
                this.m_IsKing = value;
            }
        }

        public int Row
        {
            get
            {
                return this.m_Row;
            }

            set
            {
                this.m_Row = value;
            }
        }

        public int Col
        {
            get
            {
                return this.m_Col;
            }

            set
            {
                this.m_Col = value;
            }
        }

        public void updateSquareWithString(string i_PlayerInput)
        {
            int i = 0;
            this.m_Col = i_PlayerInput[i++] - 'A';
            this.m_Row = i_PlayerInput[i] - 'a';      
        }

        public override bool Equals(object i_Square)
        {
            if (i_Square == null || i_Square.GetType() != GetType())
            {
                return false;
            }

            Square movObj = (Square)i_Square;
            return this == movObj;
        }
    }
}
