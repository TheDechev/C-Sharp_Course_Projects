using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace B18_Ex02
{
    public class Player
    {

        private List<Move> m_ObligatoryMoves;

        private string m_Name;

        private Figure.e_SquareType m_PlayerType;

        private int m_Score;

        private char m_Shape;

        private bool hasAvailableMoves = true;

        public bool hasAvailableMove
        {
            get
            {
                return hasAvailableMoves;
            }
        }

        private List<Figure> m_Figures;

        public int ObligatoryMovesCount
        {
            get
            {
                return this.m_ObligatoryMoves.Count;
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

        public Figure.e_SquareType PlayerType
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

        public void initFigures(Figure.e_SquareType currentPlayer, int i_BoardSize)
        {
            int m_figuresOnRowCounter = 0;
            int m_currentCol;
            int m_currentRow;

            if(currentPlayer == Figure.e_SquareType.playerOne)
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

        public void UpdateObligatoryMoves(Board i_GameBoard)
        {
            this.m_ObligatoryMoves = new List<Move>();
            
            foreach (Figure currFigure in m_Figures)
            {
                this.m_ObligatoryMoves.AddRange(i_GameBoard.EliminationAvailable(currFigure, this.m_PlayerType));
            }

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

        public void UpdateFigure(Move i_PlayerMove, int i_BoardSize)
        {
            foreach(Figure currentFigure in m_Figures)
            {
                if(currentFigure.Equals(i_PlayerMove.FigureFrom))
                {
                    currentFigure.Col = i_PlayerMove.FigureTo.Col;
                    currentFigure.Row = i_PlayerMove.FigureTo.Row;
                    if (PlayerType == Figure.e_SquareType.playerOne)
                    {
                        if (currentFigure.Row == 0)
                        {
                            currentFigure.IsKing = true;
                        }
                    }
                    else
                    {
                        if (currentFigure.Row == i_BoardSize - 1)
                        {
                            currentFigure.IsKing = true;
                        }
                    }
                    break;
                }
            }
        }

        public bool isMoveObligatory(Move i_UserInput)
        {
            foreach (Move optionaObligatorylMove in m_ObligatoryMoves)
            {
                if (optionaObligatorylMove.Equals(i_UserInput))
                {
                    return true;
                }
            }
            return false;
        }

        public void UpdateAvailableMovesIndicator(Board i_GameBoard)
        {
            Figure leftFig,rightFig;

            foreach(Figure currentFigure in m_Figures)
            {
                leftFig = i_GameBoard.GetSquareInDirection(currentFigure, PlayerType, Board.e_Direction.Left);
                rightFig = i_GameBoard.GetSquareInDirection(currentFigure, PlayerType, Board.e_Direction.Right);
                if (i_GameBoard.getSquareStatus(leftFig) == Figure.e_SquareType.none || i_GameBoard.getSquareStatus(rightFig) == Figure.e_SquareType.none)
                {
                    hasAvailableMoves = true;
                    return;
                }
            }

            hasAvailableMoves = false;

        }

    }
}
