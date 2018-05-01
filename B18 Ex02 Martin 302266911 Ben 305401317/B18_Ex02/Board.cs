using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex02
{
    public class Board
    {
        private const int k_DefaultBoardSize = 8;
        private int m_BoardSize = k_DefaultBoardSize;
        private int[,] m_BoardGame;

        public enum e_Direction
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        public Board (int i_BoardSize)
        {
            this.m_BoardSize = i_BoardSize;
            this.m_BoardGame = new int[i_BoardSize, i_BoardSize];
            this.InitBoard();
        }

        public void PrintBoard()
        {
            ConsoleInterface.PrintBoard(this);
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

        public Figure.e_SquareType getSquareStatus(int i_Row, int i_Col)
        {
            Figure.e_SquareType resStatus = Figure.e_SquareType.invalid;

            if (isPositionValid(i_Row, i_Col))
            {
                resStatus = (Figure.e_SquareType)m_BoardGame[i_Row, i_Col];
            }

            return resStatus;
        }

        public void addPlayersToBoard(Player i_PlayerOne, Player i_PlayerTwo)
        {
            for (int i = 0; i < i_PlayerOne.figuresNum; i++)
            {
                this.m_BoardGame[i_PlayerOne.getFigure(i).Row, i_PlayerOne.getFigure(i).Col] = (int)i_PlayerOne.PlayerType;
            }

            for (int i = 0; i < i_PlayerTwo.figuresNum; i++)
            {    
                    this.m_BoardGame[i_PlayerTwo.getFigure(i).Row, i_PlayerTwo.getFigure(i).Col] = (int)i_PlayerTwo.PlayerType;
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

        public List<Move> EliminationAvailable(Figure i_FromFigure, Player i_CurrentPlayer)
        { 
            List<Move> eliminationList = new List<Move>();
            Figure.e_SquareType squareType;

            squareType = getSquareStatus(i_FromFigure);

            if (squareType == Figure.e_SquareType.playerOneKing || squareType == Figure.e_SquareType.playerTwoKing)
            {
                getEliminationInDirection(i_FromFigure, i_CurrentPlayer, e_Direction.TopRight, eliminationList);
                getEliminationInDirection(i_FromFigure, i_CurrentPlayer, e_Direction.TopLeft, eliminationList);
                getEliminationInDirection(i_FromFigure, i_CurrentPlayer, e_Direction.BottomRight, eliminationList);
                getEliminationInDirection(i_FromFigure, i_CurrentPlayer, e_Direction.BottomLeft, eliminationList);
            }
            else if(squareType == Figure.e_SquareType.playerOne)
            {
                getEliminationInDirection(i_FromFigure, i_CurrentPlayer, e_Direction.TopRight, eliminationList);
                getEliminationInDirection(i_FromFigure, i_CurrentPlayer, e_Direction.TopLeft, eliminationList);
            }
            else
            {
                getEliminationInDirection(i_FromFigure, i_CurrentPlayer, e_Direction.BottomRight, eliminationList);
                getEliminationInDirection(i_FromFigure, i_CurrentPlayer, e_Direction.BottomLeft, eliminationList);
            }

            return eliminationList;
        }

        public Figure GetSquareInDirection(Figure i_FromSquare, e_Direction i_PlayerDirection)
        {
            int offsetCol = 1;  //X
            int offsetRow = -1;  //Y
            int addition = 1;

            if (i_PlayerDirection == e_Direction.BottomLeft || i_PlayerDirection == e_Direction.BottomRight)
            {
                addition = -1;
            }

            if (i_PlayerDirection == e_Direction.TopLeft || i_PlayerDirection == e_Direction.BottomLeft)
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
            Figure.e_SquareType squareType = getSquareStatus(i_UserMove.FigureFrom);

            if (i_CurrentPlayer.isMyKing(squareType))
            {
                squareType = i_CurrentPlayer.PlayerType;
            }

            if (needToEliminate)
            {
                areaCheck++;
            }

            if( Math.Abs(i_UserMove.FigureFrom.Row - i_UserMove.FigureTo.Row) <= areaCheck &&
                Math.Abs(i_UserMove.FigureFrom.Col - i_UserMove.FigureTo.Col) <= areaCheck)
            {
                if (getSquareStatus(i_UserMove.FigureTo) == Figure.e_SquareType.none &&
                    squareType == i_CurrentPlayer.PlayerType)
                {
                    updateBoard(i_UserMove.FigureTo.Row, i_UserMove.FigureTo.Col, getSquareStatus(i_UserMove.FigureFrom));
                    updateBoard(i_UserMove.FigureFrom.Row, i_UserMove.FigureFrom.Col, Figure.e_SquareType.none);
                    i_CurrentPlayer.UpdateFigure(i_UserMove, m_BoardSize);

                    return true;
                }
            }

            return false;
        }

        public bool updateBoard(int i_Row, int i_Col, Figure.e_SquareType i_PlayerType)
        {
            bool updateRes = false;

            if (isPositionValid(i_Row, i_Col))
            {
                if (i_PlayerType == Figure.e_SquareType.playerOne)
                {
                    if (i_Row == 0)
                    {
                        this.m_BoardGame[i_Row, i_Col] = (int)Figure.e_SquareType.playerOneKing;
                        updateRes = true;
                    } else
                    {
                        this.m_BoardGame[i_Row, i_Col] = (int)i_PlayerType;
                        updateRes = true;
                    }
                }
                else if (i_PlayerType == Figure.e_SquareType.playerTwo || i_PlayerType == Figure.e_SquareType.playerPC)
                {
                    if (i_Row == m_BoardSize - 1)
                    {
                        this.m_BoardGame[i_Row, i_Col] = (int)Figure.e_SquareType.playerTwoKing;
                        updateRes = true;
                    } else
                    {
                        this.m_BoardGame[i_Row, i_Col] = (int)i_PlayerType;
                        updateRes = true;
                    }
                }
                else
                {
                    this.m_BoardGame[i_Row, i_Col] = (int)i_PlayerType;
                    updateRes = true;
                }
            }
            

            return updateRes;
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

        public void getEliminationInDirection(Figure i_FromFigure, Player i_CurrentPlayer, e_Direction i_Direction, 
            List<Move> i_EliminationList)
        {
            Figure currentFigure;
            Figure.e_SquareType squareType;
            Move resMove = null;

            currentFigure = GetSquareInDirection(i_FromFigure, i_Direction);
            squareType = getSquareStatus(currentFigure);

            if (i_CurrentPlayer.isMyKing(squareType))
            {
                squareType = i_CurrentPlayer.PlayerType;
            }

            //checks +1 place
            if (squareType != Figure.e_SquareType.invalid && squareType != i_CurrentPlayer.PlayerType && squareType != Figure.e_SquareType.none)
            {
                currentFigure = GetSquareInDirection(currentFigure, i_Direction);
                squareType = getSquareStatus(currentFigure);
                //checks +2 place
                if (squareType != Figure.e_SquareType.invalid && squareType == Figure.e_SquareType.none)
                {
                    resMove = new Move(i_FromFigure, currentFigure);
                }
            }

            if (!object.ReferenceEquals(resMove, null))
            {
                i_EliminationList.Add(resMove);
            }
        }

    }

}
