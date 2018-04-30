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
        
        public Move RandomObligatoryMove
        {
            get
            {
                Random pcRandomObligatoryMove = new Random();
                return m_ObligatoryMoves[pcRandomObligatoryMove.Next(m_ObligatoryMoves.Count)];
            }
        }

        private Figure.e_SquareType m_PlayerType;

        private int m_Score;

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

                int figuresCount = ((value - 2) / 2) * (value / 2);
                this.m_Figures = new List<Figure>(figuresCount);
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

        public void initFigures(int i_BoardSize)
        {
            int m_figuresOnRowCounter = 0;
            int m_currentCol = 0;
            int m_currentRow;

            if(m_PlayerType == Figure.e_SquareType.playerOne)
            {
                m_currentRow = i_BoardSize / 2 + 1;

                if ((i_BoardSize / 2) % 2 != 0)
                {
                    m_currentCol = 1;
                }
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
                this.m_ObligatoryMoves.AddRange(i_GameBoard.EliminationAvailable(currFigure, this));
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
            Figure availableFigure;

            foreach(Figure currentFigure in m_Figures)
            {

                availableFigure = AvailableMove(currentFigure, i_GameBoard);
                if (!object.ReferenceEquals(availableFigure, null))
                {
                    hasAvailableMoves = true;
                    return;
                }    
            }

            hasAvailableMoves = false;
        }

        public bool isMyKing(Figure.e_SquareType i_SquareType)
        {
            bool isMyKingRes = false; 
            if (i_SquareType == Figure.e_SquareType.playerOneKing && m_PlayerType == Figure.e_SquareType.playerOne)
            {
                isMyKingRes = true;
            }
            else if (i_SquareType == Figure.e_SquareType.playerTwoKing && m_PlayerType == Figure.e_SquareType.playerTwo)
            {
                isMyKingRes = true;
            }

            return isMyKingRes;
        }

        public Figure AvailableMove(Figure i_CurrentFigure, Board i_GameBoard)
        {
            Figure leftFig, rightFig;
            Figure.e_SquareType squareType;
            squareType = i_GameBoard.getSquareStatus(i_CurrentFigure);

            if (squareType == Figure.e_SquareType.playerOneKing || squareType == Figure.e_SquareType.playerTwoKing)
            {
                leftFig = i_GameBoard.GetSquareInDirection(i_CurrentFigure, Board.e_Direction.TopLeft);
                rightFig = i_GameBoard.GetSquareInDirection(i_CurrentFigure, Board.e_Direction.TopRight);
                if (i_GameBoard.getSquareStatus(leftFig) != Figure.e_SquareType.none && i_GameBoard.getSquareStatus(rightFig) != Figure.e_SquareType.none)
                {
                    leftFig = i_GameBoard.GetSquareInDirection(i_CurrentFigure, Board.e_Direction.BottomLeft);
                    rightFig = i_GameBoard.GetSquareInDirection(i_CurrentFigure, Board.e_Direction.BottomRight);
                }

            }
            else if (squareType == Figure.e_SquareType.playerOne)
            {
                leftFig = i_GameBoard.GetSquareInDirection(i_CurrentFigure, Board.e_Direction.TopLeft);
                rightFig = i_GameBoard.GetSquareInDirection(i_CurrentFigure, Board.e_Direction.TopRight);
            }
            else
            {
                leftFig = i_GameBoard.GetSquareInDirection(i_CurrentFigure, Board.e_Direction.BottomLeft);
                rightFig = i_GameBoard.GetSquareInDirection(i_CurrentFigure, Board.e_Direction.BottomRight);
            }

            if (i_GameBoard.getSquareStatus(leftFig) == Figure.e_SquareType.none)
            {
                return leftFig;
            }
            else if (i_GameBoard.getSquareStatus(rightFig) == Figure.e_SquareType.none)
            {
                return rightFig;
            }
            else
            {
                return null;
            }
        }

        public Move ComputerMove(Board i_GameBoard)
        {
            Random rand = new Random();
            int randIndx = rand.Next(m_Figures.Count); // a num between 0 - figureCount - 1
            Figure nextFigure = AvailableMove(this.m_Figures[randIndx], i_GameBoard);
            Move computerMove = null;

            if (this.hasAvailableMove)
            {
                while (object.ReferenceEquals(nextFigure, null))
                {
                    randIndx++;
                    if (randIndx >= m_Figures.Count)
                    {
                        randIndx = 0;
                    }
                    nextFigure = AvailableMove(this.m_Figures[randIndx], i_GameBoard);
                }
                computerMove = new Move(this.m_Figures[randIndx], nextFigure);
            }

            return computerMove;
            
        }
    }
}
