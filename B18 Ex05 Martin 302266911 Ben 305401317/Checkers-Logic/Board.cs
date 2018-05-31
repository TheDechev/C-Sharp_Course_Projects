using System;
using System.Collections.Generic;

namespace Checkers_Logic
{
    public delegate void SquareUpdateDelegate(int Row, int Col, Square.eSquareType i_SquareType);
    public class Board
    {
        private int m_BoardSize = (int)eBoardSize.Medium;
        private int[,] m_BoardGame;
        private event SquareUpdateDelegate m_SquareUpdate;
        public enum eDirection
        {
            TopLeft,
            TopRight,
            BottomLeft,
            BottomRight
        }

        public enum eBoardSize
        {
            Small = 6,                
            Medium = 8,
            Large = 10,
        }

        public SquareUpdateDelegate SquareUpdate
        {
            get
            {
                return m_SquareUpdate;
            }
            set
            {
                m_SquareUpdate = value;
            }
        }

        public Board(int i_BoardSize)
        {
            this.m_BoardSize = i_BoardSize;
            this.m_BoardGame = new int[i_BoardSize, i_BoardSize];
            this.InitBoard();
        }

        public void ClearBoard()
        {
            m_BoardGame = new int[Convert.ToInt16(Math.Sqrt(m_BoardGame.Length)), Convert.ToInt16(Math.Sqrt(m_BoardGame.Length))];
            this.InitBoard();
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

        public Square.eSquareType GetSquareStatus(Square i_Square)
        {
            Square.eSquareType resStatus = Square.eSquareType.invalid;

            if(!object.ReferenceEquals(i_Square, null))
            {
                if (this.IsPositionValid(i_Square.Row, i_Square.Col))
                { 
                   resStatus = (Square.eSquareType)this.m_BoardGame[i_Square.Row, i_Square.Col];
                }
            }
            return resStatus;
        }

        public Square.eSquareType GetSquareStatus(int i_Row, int i_Col)
        {
            Square.eSquareType resStatus = Square.eSquareType.invalid;

            if (this.IsPositionValid(i_Row, i_Col))
            {
                resStatus = (Square.eSquareType)this.m_BoardGame[i_Row, i_Col];
            }

            return resStatus;
        }

        public void AddPlayersToBoard(Player i_PlayerOne, Player i_PlayerTwo)
        {
            for (int i = 0; i < i_PlayerOne.SquaresNum; i++)
            {
                this.m_BoardGame[i_PlayerOne.GetSquare(i).Row, i_PlayerOne.GetSquare(i).Col] = (int)i_PlayerOne.PlayerType;
                OnSquareUpdate(i_PlayerOne.GetSquare(i).Row, i_PlayerOne.GetSquare(i).Col, GetSquareStatus(i_PlayerOne.GetSquare(i).Row, i_PlayerOne.GetSquare(i).Col));
            }

            for (int i = 0; i < i_PlayerTwo.SquaresNum; i++)
            {    
                this.m_BoardGame[i_PlayerTwo.GetSquare(i).Row, i_PlayerTwo.GetSquare(i).Col] = (int)i_PlayerTwo.PlayerType;
                OnSquareUpdate(i_PlayerTwo.GetSquare(i).Row, i_PlayerTwo.GetSquare(i).Col, GetSquareStatus(i_PlayerTwo.GetSquare(i).Row, i_PlayerTwo.GetSquare(i).Col));
            }
        }

        public void InitBoard()
        {
            for (int currentRow = 0; currentRow < this.m_BoardSize; currentRow++)
            {
                for(int currentCol = 0; currentCol < this.m_BoardSize; currentCol++)
                {
                    if ((currentRow % 2 != 0 && currentCol % 2 != 0) || (currentRow % 2 == 0 && currentCol % 2 == 0))
                    {
                        this.m_BoardGame[currentRow, currentCol] = (int)Square.eSquareType.invalid;
                    }
                    else
                    {
                        this.m_BoardGame[currentRow, currentCol] = (int)Square.eSquareType.none;
                    }
                    OnSquareUpdate(currentRow, currentCol, GetSquareStatus(currentRow, currentCol));
                }
            }
        }

        public List<Move> EliminationAvailable(Square i_FromSquare, Player i_CurrentPlayer)
        { 
            List<Move> eliminationList = new List<Move>();
            Square.eSquareType squareType;

            squareType = this.GetSquareStatus(i_FromSquare);

            if (squareType == Square.eSquareType.playerOneKing || squareType == Square.eSquareType.playerTwoKing)
            {
               this.GetEliminationInDirection(i_FromSquare, i_CurrentPlayer, eDirection.TopRight, eliminationList);
               this.GetEliminationInDirection(i_FromSquare, i_CurrentPlayer, eDirection.TopLeft, eliminationList);
               this.GetEliminationInDirection(i_FromSquare, i_CurrentPlayer, eDirection.BottomRight, eliminationList);
               this.GetEliminationInDirection(i_FromSquare, i_CurrentPlayer, eDirection.BottomLeft, eliminationList);
            }
            else if(squareType == Square.eSquareType.playerOne)
            {
                this.GetEliminationInDirection(i_FromSquare, i_CurrentPlayer, eDirection.TopRight, eliminationList);
                this.GetEliminationInDirection(i_FromSquare, i_CurrentPlayer, eDirection.TopLeft, eliminationList);
            }
            else
            {
                this.GetEliminationInDirection(i_FromSquare, i_CurrentPlayer, eDirection.BottomRight, eliminationList);
                this.GetEliminationInDirection(i_FromSquare, i_CurrentPlayer, eDirection.BottomLeft, eliminationList);
            }

            return eliminationList;
        }

        public Square GetSquareInDirection(Square i_FromSquare, eDirection i_PlayerDirection)
        {
            int offsetCol = 1;
            int offsetRow = -1; 
            int addition = 1;

            if (i_PlayerDirection == eDirection.BottomLeft || i_PlayerDirection == eDirection.BottomRight)
            {
                addition = -1;
            }

            if (i_PlayerDirection == eDirection.TopLeft || i_PlayerDirection == eDirection.BottomLeft)
            {
                offsetCol = -1;
                offsetRow = -1;
            }

            Square resSquare = new Square(i_FromSquare.Row + (offsetRow * addition), i_FromSquare.Col + offsetCol);

            if(this.GetSquareStatus(resSquare) != Square.eSquareType.invalid)
            {
                return resSquare;
            }
            else
            {
                return null;
            }
        }

        public bool UpdateBoardAfterMove(Move i_UserMove, Player i_CurrentPlayer, bool i_NeedToEliminate)
        {
            int areaCheck = 1;
            bool isUpdate = false;
            Square.eSquareType squareType = this.GetSquareStatus(i_UserMove.SquareFrom);

            if (i_CurrentPlayer.IsMyKing(squareType))
            {
                squareType = i_CurrentPlayer.PlayerType;
            }

            if (i_NeedToEliminate)
            {
                areaCheck++;
            }

            if(Math.Abs(i_UserMove.SquareFrom.Row - i_UserMove.SquareTo.Row) <= areaCheck &&
               Math.Abs(i_UserMove.SquareFrom.Col - i_UserMove.SquareTo.Col) <= areaCheck)
            {
                if (this.GetSquareStatus(i_UserMove.SquareTo) == Square.eSquareType.none && squareType == i_CurrentPlayer.PlayerType &&
                    isDestinationValid(i_UserMove,this.GetSquareStatus(i_UserMove.SquareFrom)))
                {
                    this.UpdateBoard(i_UserMove.SquareTo.Row, i_UserMove.SquareTo.Col, this.GetSquareStatus(i_UserMove.SquareFrom), i_CurrentPlayer);
                    this.UpdateBoard(i_UserMove.SquareFrom.Row, i_UserMove.SquareFrom.Col, Square.eSquareType.none, i_CurrentPlayer);
                    i_CurrentPlayer.UpdateSquare(i_UserMove, this.m_BoardSize);

                    isUpdate = true;
                }
            }

            return isUpdate;
        }

        public bool UpdateBoard(int i_Row, int i_Col, Square.eSquareType i_UpdateSquare, Player i_CurrentPlayer)
        {
            bool updateRes = false;

            if (this.IsPositionValid(i_Row, i_Col))
            {
                if (i_UpdateSquare == Square.eSquareType.playerOne)
                {
                    if (i_Row == 0)
                    {
                        i_CurrentPlayer.Score += 3; // King score addition
                        this.m_BoardGame[i_Row, i_Col] = (int)Square.eSquareType.playerOneKing;
                        updateRes = true;
                    }
                    else
                    {
                        this.m_BoardGame[i_Row, i_Col] = (int)i_UpdateSquare;
                        updateRes = true;
                    }
                }
                else if (i_UpdateSquare == Square.eSquareType.playerTwo || i_UpdateSquare == Square.eSquareType.playerPC)
                {
                    if (i_Row == this.m_BoardSize - 1)
                    {
                        i_CurrentPlayer.Score += 3; // King score addition
                        this.m_BoardGame[i_Row, i_Col] = (int)Square.eSquareType.playerTwoKing;
                        updateRes = true;
                    }
                    else
                    {
                        this.m_BoardGame[i_Row, i_Col] = (int)i_UpdateSquare;
                        updateRes = true;
                    }
                }
                else
                {
                    this.m_BoardGame[i_Row, i_Col] = (int)i_UpdateSquare;
                    updateRes = true;
                }

                OnSquareUpdate(i_Row,i_Col,GetSquareStatus(i_Row,i_Col));
            }

            return updateRes;
        }

        public bool IsPositionValid(int i_Row, int i_Col)
        {
            bool isPosValid = false;

            if (i_Row < this.m_BoardSize && i_Col < this.m_BoardSize && i_Row >= 0 && i_Col >= 0)
            {
                isPosValid = this.m_BoardGame[i_Row, i_Col] != (int)Square.eSquareType.invalid;
            }

            return isPosValid;
        }

        public void GetEliminationInDirection(Square i_FromSquare, Player i_CurrentPlayer, eDirection i_Direction, List<Move> i_EliminationList)
        {
            Square currentSquare;
            Square.eSquareType squareType;
            Move resMove = null;

            currentSquare = this.GetSquareInDirection(i_FromSquare, i_Direction);
            squareType = this.GetSquareStatus(currentSquare);

            if (i_CurrentPlayer.IsMyKing(squareType))
            {
                squareType = i_CurrentPlayer.PlayerType;
            }

            if (squareType != Square.eSquareType.invalid && squareType != i_CurrentPlayer.PlayerType && squareType != Square.eSquareType.none)
            {   
                // Checks 1 square raduis
                currentSquare = this.GetSquareInDirection(currentSquare, i_Direction);
                squareType = this.GetSquareStatus(currentSquare);
                
                if (squareType != Square.eSquareType.invalid && squareType == Square.eSquareType.none)
                {   
                    // Checks 2 square raduis
                    resMove = new Move(i_FromSquare, currentSquare);
                }
            }

            if (!object.ReferenceEquals(resMove, null))
            {
                i_EliminationList.Add(resMove);
            }
        }

        public string SquareToString(Square.eSquareType i_CurrentSquare)
        {
            string resString = string.Empty;

            if (i_CurrentSquare == Square.eSquareType.playerOne)
            {
                resString = " X ";
            }
            else if (i_CurrentSquare == Square.eSquareType.playerTwo || i_CurrentSquare == Square.eSquareType.playerPC)
            {
                resString = " O ";
            }
            else if (i_CurrentSquare == Square.eSquareType.playerOneKing)
            {
                resString = " K ";
            }
            else if (i_CurrentSquare == Square.eSquareType.playerTwoKing)
            {
                resString = " U ";
            }

            return resString;
        }

        private bool isDestinationValid(Move i_UserMove, Square.eSquareType i_SquareType)
        {
            bool isValid = true;

            if(i_SquareType == Square.eSquareType.playerOne)
            {
                if(i_UserMove.SquareTo.Row - i_UserMove.SquareFrom.Row > 0)
                {
                    isValid = false;
                }
            }
            else if (i_SquareType == Square.eSquareType.playerTwo)
            {
                if (i_UserMove.SquareTo.Row - i_UserMove.SquareFrom.Row < 0)
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        private void OnSquareUpdate(int i_Row, int i_Col , Square.eSquareType i_SquareType)
        {
            if(this.m_SquareUpdate != null)
            {
                this.m_SquareUpdate.Invoke(i_Row, i_Col, i_SquareType);
            }
        }
    }
}
