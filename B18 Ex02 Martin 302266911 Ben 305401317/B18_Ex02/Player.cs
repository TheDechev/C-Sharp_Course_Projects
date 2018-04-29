using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace B18_Ex02
{
    public class Player
    {
        public enum e_PlayerType
        {
            none,
            playerOne,
            playerTwo,
            playerPC
        }

        private string m_Name;

        private e_PlayerType m_PlayerType;

        private int m_Score;

        private char m_Shape;

        private List<Figure> m_Figures;

        public Figure lastFigure
        {
            get
            {
                return this.m_Figures.Last();
            }
        }

        public int Score
        {
            get
            {
                return this.m_Score;
            }
            set
            {
                this.m_Score = value;
            }
        }

        public int figuresNum
        {
            get
            {
                return this.m_Figures.Count;
            }

            set
            {
                if(this.m_Figures == null)
                {
                    int figuresCount = ((value - 2) / 2) * (value / 2);
                    this.m_Figures = new List<Figure>(figuresCount);
                }
                else
                {
                    Console.WriteLine("Invalid size");
                }
            }
        }

        public e_PlayerType PlayerType
        {
            get
            {
                return this.m_PlayerType;
            }
            set
            {
                m_PlayerType = value;
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

        public void initFigures(Player.e_PlayerType currentPlayer, int i_BoardSize)
        {
            int m_figuresOnRowCounter = 0;
            int m_currentCol;
            int m_currentRow;

            if(currentPlayer == Player.e_PlayerType.playerOne)
            {
                m_currentCol = 0;
                m_currentRow = i_BoardSize / 2 + 1;
            }
            else
            {
                m_currentCol = 1;
                m_currentRow = 0;
            }

            for (int i = 0; i < this.m_Figures.Capacity; i++)
            {
                if(m_figuresOnRowCounter == i_BoardSize / 2)
                {
                    m_figuresOnRowCounter = 0;
                    m_currentRow++;
                    
                    if(m_currentCol == i_BoardSize)
                    {
                        m_currentCol = 1;
                    }
                    else
                    {
                        m_currentCol = 0;
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

        public List<Move> getObligatoryMoves(Board i_gameBoard)
        {
            List<Move> obligatoryMoves = new List<Move>();
            
            foreach (Figure currFigure in m_Figures)
            {
                // TODO: Move in boundries func in board
                    obligatoryMoves.AddRange(i_gameBoard.EliminationAvailable(currFigure.Row, currFigure.Col, this.m_PlayerType));
            }

            return obligatoryMoves;
        }

        public void deleteFigure(Figure i_FigureToDelete)
        {
            foreach (Figure currFigure in m_Figures)
            {
                if (i_FigureToDelete == currFigure)
                { 
                    m_Figures.Remove(currFigure);
                    break;
                }
            }
        }

        public void UpdateFigure(Move i_PlayerMove)
        {
            foreach(Figure currentFigure in m_Figures)
            {
                if(currentFigure.Equals(i_PlayerMove.FigureFrom))
                {
                    currentFigure.Col = i_PlayerMove.FigureTo.Col;
                    currentFigure.Row = i_PlayerMove.FigureTo.Row;
                    break;
                }
            }
        }

    }
}
