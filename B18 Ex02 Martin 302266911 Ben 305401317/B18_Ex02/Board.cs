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

        public Board(int i_BoardSize)
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

        public Square.e_SquareType getSquareStatus(Square i_Square)
        {
            Square.e_SquareType resStatus = Square.e_SquareType.invalid;

            if(!object.ReferenceEquals(i_Square, null))
            {
                if (this.isPositionValid(i_Square.Row, i_Square.Col))
                { 
                   resStatus = (Square.e_SquareType)this.m_BoardGame[i_Square.Row, i_Square.Col];
                }
            }

            return resStatus;
        }

        public Square.e_SquareType getSquareStatus(int i_Row, int i_Col)
        {
            Square.e_SquareType resStatus = Square.e_SquareType.invalid;

            if (this.isPositionValid(i_Row, i_Col))
            {
                resStatus = (Square.e_SquareType)this.m_BoardGame[i_Row, i_Col];
            }

            return resStatus;
        }

        public void addPlayersToBoard(Player i_PlayerOne, Player i_PlayerTwo)
        {
            for (int i = 0; i < i_PlayerOne.squaresNum; i++)
            {
                this.m_BoardGame[i_PlayerOne.getSquare(i).Row, i_PlayerOne.getSquare(i).Col] = (int)i_PlayerOne.PlayerType;
            }

            for (int i = 0; i < i_PlayerTwo.squaresNum; i++)
            {    
                    this.m_BoardGame[i_PlayerTwo.getSquare(i).Row, i_PlayerTwo.getSquare(i).Col] = (int)i_PlayerTwo.PlayerType;
            }
        }

        public void InitBoard()
        {
            int currentCol = 0;
            for (int currentRow = 0; currentRow < this.m_BoardSize; currentRow++)
            {
                if(currentRow % 2 != 0)
                {
                    currentCol++;
                }

                while(currentCol < this.m_BoardSize)
                {
                    this.m_BoardGame[currentRow, currentCol] = (int)Square.e_SquareType.invalid;
                    currentCol += 2;
                }

                currentCol = 0;
            }
        }

        public List<Move> EliminationAvailable(Square i_FromSquare, Player i_CurrentPlayer)
        { 
            List<Move> eliminationList = new List<Move>();
            Square.e_SquareType squareType;

            squareType = this.getSquareStatus(i_FromSquare);

            if (squareType == Square.e_SquareType.playerOneKing || squareType == Square.e_SquareType.playerTwoKing)
            {
               this.getEliminationInDirection(i_FromSquare, i_CurrentPlayer, e_Direction.TopRight, eliminationList);
               this.getEliminationInDirection(i_FromSquare, i_CurrentPlayer, e_Direction.TopLeft, eliminationList);
               this.getEliminationInDirection(i_FromSquare, i_CurrentPlayer, e_Direction.BottomRight, eliminationList);
               this.getEliminationInDirection(i_FromSquare, i_CurrentPlayer, e_Direction.BottomLeft, eliminationList);
            }
            else if(squareType == Square.e_SquareType.playerOne)
            {
                this.getEliminationInDirection(i_FromSquare, i_CurrentPlayer, e_Direction.TopRight, eliminationList);
                this.getEliminationInDirection(i_FromSquare, i_CurrentPlayer, e_Direction.TopLeft, eliminationList);
            }
            else
            {
                this.getEliminationInDirection(i_FromSquare, i_CurrentPlayer, e_Direction.BottomRight, eliminationList);
                this.getEliminationInDirection(i_FromSquare, i_CurrentPlayer, e_Direction.BottomLeft, eliminationList);
            }

            return eliminationList;
        }

        public Square GetSquareInDirection(Square i_FromSquare, e_Direction i_PlayerDirection)
        {
            int offsetCol = 1;
            int offsetRow = -1; 
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

            Square resSquare = new Square(i_FromSquare.Row + (offsetRow * addition), i_FromSquare.Col + offsetCol);

            if(this.getSquareStatus(resSquare) != Square.e_SquareType.invalid)
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
            Square.e_SquareType squareType = this.getSquareStatus(i_UserMove.SquareFrom);

            if (i_CurrentPlayer.isMyKing(squareType))
            {
                squareType = i_CurrentPlayer.PlayerType;
            }

            if (needToEliminate)
            {
                areaCheck++;
            }

            if(Math.Abs(i_UserMove.SquareFrom.Row - i_UserMove.SquareTo.Row) <= areaCheck &&
               Math.Abs(i_UserMove.SquareFrom.Col - i_UserMove.SquareTo.Col) <= areaCheck)
            {
                if (this.getSquareStatus(i_UserMove.SquareTo) == Square.e_SquareType.none &&
                    squareType == i_CurrentPlayer.PlayerType)
                {
                    this.updateBoard(i_UserMove.SquareTo.Row, i_UserMove.SquareTo.Col, this.getSquareStatus(i_UserMove.SquareFrom));
                    this.updateBoard(i_UserMove.SquareFrom.Row, i_UserMove.SquareFrom.Col, Square.e_SquareType.none);
                    i_CurrentPlayer.UpdateSquare(i_UserMove, this.m_BoardSize);

                    return true;
                }
            }

            return false;
        }

        public bool updateBoard(int i_Row, int i_Col, Square.e_SquareType i_PlayerType)
        {
            bool updateRes = false;

            if (this.isPositionValid(i_Row, i_Col))
            {
                if (i_PlayerType == Square.e_SquareType.playerOne)
                {
                    if (i_Row == 0)
                    {
                        this.m_BoardGame[i_Row, i_Col] = (int)Square.e_SquareType.playerOneKing;
                        updateRes = true;
                    }
                    else
                    {
                        this.m_BoardGame[i_Row, i_Col] = (int)i_PlayerType;
                        updateRes = true;
                    }
                }
                else if (i_PlayerType == Square.e_SquareType.playerTwo || i_PlayerType == Square.e_SquareType.playerPC)
                {
                    if (i_Row == this.m_BoardSize - 1)
                    {
                        this.m_BoardGame[i_Row, i_Col] = (int)Square.e_SquareType.playerTwoKing;
                        updateRes = true;
                    }
                    else
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
            //inside board 
            if (i_Row < this.m_BoardSize && i_Col < this.m_BoardSize && i_Row >= 0 && i_Col >= 0)
            {
                isPosValid = this.m_BoardGame[i_Row, i_Col ] != (int)Square.e_SquareType.invalid;
            }

            return isPosValid;
        }

        public void getEliminationInDirection(Square i_FromSquare, Player i_CurrentPlayer, e_Direction i_Direction, List<Move> i_EliminationList)
        {
            Square currentSquare;
            Square.e_SquareType squareType;
            Move resMove = null;

            currentSquare = this.GetSquareInDirection(i_FromSquare, i_Direction);
            squareType = this.getSquareStatus(currentSquare);

            if (i_CurrentPlayer.isMyKing(squareType))
            {
                squareType = i_CurrentPlayer.PlayerType;
            }

            //checks +1 place
            if (squareType != Square.e_SquareType.invalid && squareType != i_CurrentPlayer.PlayerType && squareType != Square.e_SquareType.none)
            {
                currentSquare = this.GetSquareInDirection(currentSquare, i_Direction);
                squareType = this.getSquareStatus(currentSquare);
                //checks +2 place
                if (squareType != Square.e_SquareType.invalid && squareType == Square.e_SquareType.none)
                {
                    resMove = new Move(i_FromSquare, currentSquare);
                }
            }

            if (!object.ReferenceEquals(resMove, null))
            {
                i_EliminationList.Add(resMove);
            }
        }
    }
}
