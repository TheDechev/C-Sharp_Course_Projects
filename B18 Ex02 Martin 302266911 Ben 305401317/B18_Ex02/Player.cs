using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace B18_Ex02
{
    class Player
    {
        private string m_Name;
        private int m_FigureCount;
        private int m_Score;
        private bool m_IsComputer = false;
        private char m_Shape;
        private List<Figure> m_Figures;

        public Figure lastFigure
        {
            get
            {
                return m_Figures.Last();
            }
        }

        public int figuresNum
        {

            set
            {
                if(m_FigureCount == 0)
                {
                    m_FigureCount = ((value - 2) / 2) * (value / 2);
                    m_Figures = new List<Figure>(m_FigureCount);
                }
                else
                {
                    Console.WriteLine("Invalid size");
                }
            }
            get
            {
                return m_FigureCount;
            }
        }

        public string Name
        {
            set
            {
                m_Name = value;
            }

            get
            {
                return m_Name;
            }
        }

        public char Shape
        {
            set
            {
                m_Shape = value;
            }
            get
            {
                return m_Shape;
            }
        }

        public void initFigures(int i_StartLine, int i_BoardSize)
        {
            int m_figuresOnRowCounter = 0;
            int m_currentCol;
            int m_currentRow = i_StartLine;

            //second player
            if(i_StartLine == 0)
            {
                m_currentCol = 1;
            }
            //first player
            else
            {
                m_currentCol = 0;
            }

            for (int i = 0; i < m_FigureCount; i++)
            {
                if(m_figuresOnRowCounter == i_BoardSize / 2)
                {
                    m_figuresOnRowCounter = 0;
                    m_currentRow++;
                    
                    if(m_currentCol == i_BoardSize + 1)
                    {
                        m_currentCol = 0;
                        
                    } else
                    {
                        m_currentCol = 1;
                    }
                }
                
                m_figuresOnRowCounter++;
                
                Console.WriteLine("Row:" + m_currentRow +  " Col:" + m_currentCol);
                m_Figures.Add(new Figure(m_currentRow, m_currentCol));
                m_currentCol += 2;

            }

        }

        public Figure getFigure(int i_Index)
        {
           
            return m_Figures[i_Index];
        }

        public Figure checkExistance(int i_CurrentRow, int i_CurrentCol)
        {
            Figure checkFigure = new Figure(i_CurrentRow, i_CurrentCol);
            
            foreach(Figure currentFig in m_Figures)
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
