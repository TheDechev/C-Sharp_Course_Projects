using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex02
{
    public class Board
    {

        public enum e_Direction
        {
            Left,
            Right 
        }

        private const int k_RequiredSpaceForFigure = 4;

        private const int k_DefaultBoardSize = 8;

        private int m_BoardSize = k_DefaultBoardSize;

        private int[,] m_BoardGame;

        public Board (int i_BoardSize)
        {
            this.m_BoardSize = i_BoardSize;
            this.m_BoardGame = new int[i_BoardSize, i_BoardSize];
            this.InitBoard();
        }

        public void PrintBoard()
        {
            char currentChar = 'A';
            int m_BoardRow = 0;

            Console.Write(" ");

            for (int j = 0; j < this.m_BoardSize; j++)
            {
                Console.Write("  " +currentChar+++" ");
            }

            Console.WriteLine();
            currentChar = 'a';

            for (int i = 0; i < this.m_BoardSize * 2 + 1; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j <= this.m_BoardSize * k_RequiredSpaceForFigure + 1; j++)
                    {
                        Console.Write("=");
                    }
                }
                else
                {
                    Console.Write(currentChar++);

                    for (int j = 0; j < this.m_BoardSize; j++)
                    {
                        Console.Write("| ");
                        //// the figures logic

                        if (this.m_BoardGame[m_BoardRow, j] == (int) Figure.e_SquareType.playerOne) 
                        {
                            Console.Write("X ");
                        }
                        else if (this.m_BoardGame[m_BoardRow, j] == (int)Figure.e_SquareType.playerTwo)
                        {
                            Console.Write("O ");
                        }
                        else
                        {
                            Console.Write("  ");
                        }
                    }

                    Console.Write("|");
                    m_BoardRow++;
                }

                Console.WriteLine();
            }

            currentChar = 'a';
            Console.WriteLine();
        }

        public int Size
        {
            get
            {
                return this.m_BoardSize;
            }

            set
            {
                this.m_BoardSize = value;   
            }
        }

        public Figure.e_SquareType getSquareStatus(Figure i_Figure)
        {
            Figure.e_SquareType resStatus = Figure.e_SquareType.invalid;

            if(!object.ReferenceEquals(i_Figure,null))
            {
                if (isPositionValid(i_Figure.Row, i_Figure.Col))
                { 
                   resStatus = (Figure.e_SquareType)m_BoardGame[i_Figure.Row, i_Figure.Col];
                }
            }
            

            return resStatus;
        }

        public void addPlayersToBoard(Player i_PlayerOne, Player i_PlayerTwo)
        {
            for (int i = 0; i < i_PlayerOne.figuresNum; i++)
            {
                this.m_BoardGame[i_PlayerOne.getFigure(i).Row, i_PlayerOne.getFigure(i).Col] = (int)Figure.e_SquareType.playerOne;
            }

            if(!(i_PlayerTwo == null))
            {
                for (int i = 0; i < i_PlayerTwo.figuresNum; i++)
                {
                    this.m_BoardGame[i_PlayerTwo.getFigure(i).Row, i_PlayerTwo.getFigure(i).Col] = (int)Figure.e_SquareType.playerTwo;
                }
            }
        }

        public void InitBoard()
        {
            int currentCol = 0;
            for (int currentRow=0; currentRow < m_BoardSize; currentRow++)
            {
                if(currentRow % 2 != 0)
                {
                    currentCol++;
                }
                while(currentCol < m_BoardSize)
                {
                    m_BoardGame[currentRow, currentCol] = (int)Figure.e_SquareType.invalid;
                    currentCol += 2;
                }
                currentCol = 0;
            }
        }

        public List<Move> EliminationAvailable(Figure i_FromFigure, Figure.e_SquareType i_currentPlayer)
        { 
            List<Move> eliminationList = new List<Move>();
            Figure currentFigure;
            Figure.e_SquareType squareType;
            

            //check right pos
            currentFigure = GetSquareInDirection(i_FromFigure, i_currentPlayer, e_Direction.Right);
            squareType = getSquareStatus(currentFigure);

            if (squareType != Figure.e_SquareType.invalid && squareType != i_currentPlayer && squareType != Figure.e_SquareType.none)
            {
                currentFigure = GetSquareInDirection(currentFigure, i_currentPlayer, e_Direction.Right);
                squareType = getSquareStatus(currentFigure);

                if (squareType != Figure.e_SquareType.invalid && squareType == Figure.e_SquareType.none)
                {
                    Move moveRight = new Move(i_FromFigure, currentFigure);
                    eliminationList.Add(moveRight);
                }
            }

            //check left pos
            currentFigure = GetSquareInDirection(i_FromFigure, i_currentPlayer, e_Direction.Left);
            squareType = getSquareStatus(currentFigure);

            if (squareType != Figure.e_SquareType.invalid && squareType != i_currentPlayer && squareType != Figure.e_SquareType.none)
            {
                currentFigure = GetSquareInDirection(currentFigure, i_currentPlayer, e_Direction.Left);
                squareType = getSquareStatus(currentFigure);

                if (squareType != Figure.e_SquareType.invalid && squareType == Figure.e_SquareType.none)
                {
                    Move moveLeft = new Move(i_FromFigure, currentFigure);
                    eliminationList.Add(moveLeft);
                }
            }

            return eliminationList;
        }

        public Figure GetSquareInDirection(Figure i_FromSquare, Figure.e_SquareType i_PlayerType, e_Direction i_PlayerDirection)
        {
            int offsetCol = 1;  //X
            int offsetRow = -1;  //Y
            int addition = 1;

            if (!(i_PlayerType == Figure.e_SquareType.playerOne))
            {
                addition = -1;
            }

            //check right pos
            if (i_PlayerDirection == e_Direction.Left)
            {
                offsetCol = -1;
                offsetRow = -1;
            }

            Figure resSquare = new Figure(i_FromSquare.Row + offsetRow * addition, i_FromSquare.Col + offsetCol);

            if(getSquareStatus(resSquare) != Figure.e_SquareType.invalid)
            {
                return resSquare;
            }
            else
            {
                return null;
            }
           
        }

        public bool updateBoardAfterMove(Move i_UserMove, Player i_CurrentPlayer, bool needToEliminate)
        {
            int areaCheck = 1;

            if (needToEliminate)
            {
                areaCheck++;
            }

            if( Math.Abs(i_UserMove.FigureFrom.Row - i_UserMove.FigureTo.Row) <= areaCheck &&
                Math.Abs(i_UserMove.FigureFrom.Col - i_UserMove.FigureTo.Col) <= areaCheck)
            {
                if (getSquareStatus(i_UserMove.FigureTo) == Figure.e_SquareType.none &&
    getSquareStatus(i_UserMove.FigureFrom) == i_CurrentPlayer.PlayerType)
                {
                    updateBoard(i_UserMove.FigureFrom.Row, i_UserMove.FigureFrom.Col, Figure.e_SquareType.none);
                    updateBoard(i_UserMove.FigureTo.Row, i_UserMove.FigureTo.Col, i_CurrentPlayer.PlayerType);
                    i_CurrentPlayer.UpdateFigure(i_UserMove, m_BoardSize);
                    return true;
                }
            }

            return false;
        }

        public bool updateBoard(int i_Row, int i_Col, Figure.e_SquareType i_Value )
        {
            if (isPositionValid(i_Row, i_Col))
            {
                this.m_BoardGame[i_Row, i_Col] = (int)i_Value;
                return true;
            }
            return false;
        }

        public bool isPositionValid(int i_Row, int i_Col)
        {
            bool isPosValid = false;

            if(i_Row < m_BoardSize && i_Col < m_BoardSize && i_Row >= 0 && i_Col >= 0) //inside board 
            {
                isPosValid = m_BoardGame[i_Row, i_Col ] != (int)Figure.e_SquareType.invalid;
            }
            return isPosValid;
        }


    }

}
