using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace B18_Ex02
{
    public class Player
    {
        private string m_Name;
        private int m_FigureCount;
        ////private int m_Score;
        ////private bool m_IsComputer = false;
        private char m_Shape;
        private List<Figure> m_Figures;

        public Figure lastFigure
        {
            get
            {
                return this.m_Figures.Last();
            }
        }

        public int figuresNum
        {
            get
            {
                return this.m_FigureCount;
            }

            set
            {
                if(this.m_FigureCount == 0)
                {
                    this.m_FigureCount = ((value - 2) / 2) * (value / 2);
                    this.m_Figures = new List<Figure>(this.m_FigureCount);
                }
                else
                {
                    Console.WriteLine("Invalid size");
                }
            }
        }

        public string Name
        {
            get
            {
                return this.m_Name;
            }

            set
            {
                this.m_Name = value;
            }
        }

        public char Shape
        {
            get
            {
                return this.m_Shape;
            }

            set
            {
                this.m_Shape = value;
            }
        }

        public void initFigures(int i_StartLine, int i_BoardSize)
        {
            int m_figuresOnRowCounter = 0;
            int m_currentCol;
            int m_currentRow = i_StartLine;

            if(i_StartLine == 0)
            {////second player
                m_currentCol = 1;
            }
            else
            {////first player
                m_currentCol = 0;
            }

            for (int i = 0; i < this.m_FigureCount; i++)
            {
                if(m_figuresOnRowCounter == i_BoardSize / 2)
                {
                    m_figuresOnRowCounter = 0;
                    m_currentRow++;
                    
                    if(m_currentCol == i_BoardSize + 1)
                    {
                        m_currentCol = 0;
                    }
                    else
                    {
                        m_currentCol = 1;
                    }
                }
                
                m_figuresOnRowCounter++;
                Console.WriteLine("Row:" + m_currentRow + " Col:" + m_currentCol);
                this.m_Figures.Add(new Figure(m_currentRow, m_currentCol));
                m_currentCol += 2;
            }
        }

        public Figure getFigure(int i_Index)
        {
            return this.m_Figures[i_Index];
        }

        public Figure checkExistance(int i_CurrentRow, int i_CurrentCol)
        {
            Figure checkFigure = new Figure(i_CurrentRow, i_CurrentCol);
            
            foreach(Figure currentFig in this.m_Figures)
            {
                if (currentFig.Equals(checkFigure))
                {
                    return currentFig;
                }
            }

            return null;  
        }
    }
}
