using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace B18_Ex02
{
    public class Figure
    {
        private int m_Row;

        private int m_Col;

        private bool m_IsKing = false;

        public Figure(int i_Row, int i_Col)
        {
            this.m_Row = i_Row;
            this.m_Col = i_Col;
        }

        public Figure()
        {
            this.m_Row = -1;
            this.m_Col = -1;
        }

        public bool IsKing
        {
            set
            {
                this.m_IsKing = value;
            }
            get
            {
                return this.m_IsKing;
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

        public void updateFigurePosAccordingToPlayerMove(string i_playerInput, bool isCurrent)
        {
            int i = 0;

            if(!isCurrent)
            {
                i = 3;
            }

            i_playerInput = i_playerInput.Replace(" ", string.Empty);
            this.m_Col = i_playerInput[i] - 'A';
            this.m_Row = i_playerInput[++i] - 'a';      
        }

        public static bool operator ==(Figure i_FigureOne, Figure i_FigureTwo)
        {
            return (i_FigureOne.Row == i_FigureTwo.Row && i_FigureOne.Col == i_FigureTwo.Col);
        }

        public static bool operator !=(Figure i_FigureOne, Figure i_FigureTwo)
        {
            return (i_FigureOne.Row != i_FigureTwo.Row || i_FigureOne.Col != i_FigureTwo.Col);
        }

        public override bool Equals(object i_Figure)
        {
            if (i_Figure == null || i_Figure.GetType() != GetType())
            {
                return false;
            }

            Figure movObj = (Figure)i_Figure;
            return this == movObj;
        }
    }
}
