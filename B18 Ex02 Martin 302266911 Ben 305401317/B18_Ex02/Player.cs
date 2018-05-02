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
        private Square.e_SquareType m_PlayerType;
        private int m_Score;
        private bool hasAvailableMoves = true;
        private List<Square> m_Squares;
        private int m_BonusScore = 0;

        public int BonusScore{
            get
            {
                return m_BonusScore;
            }
            set
            {
                m_BonusScore += value;
            }
        }

        public bool hasAvailableMove
        {
            get
            {
                return this.hasAvailableMoves;
            }
        }

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

        public int squaresNum
        {
            get
            {
                return this.m_Squares.Count;
            }

            set
            {
                int squaresCount = ((value - 2) / 2) * (value / 2);
                this.m_Squares = new List<Square>(squaresCount);
             }
        }

        public Square.e_SquareType PlayerType
        {
            get
            {
                return this.m_PlayerType;
            }

            set
            {
                this.m_PlayerType = value;
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

        public void initSquares(int i_BoardSize)
        {
            int m_squaresOnRowCounter = 0;
            int m_currentCol = 0;
            int m_currentRow;

            if(this.m_PlayerType == Square.e_SquareType.playerOne)
            {
                m_currentRow = (i_BoardSize / 2) + 1;

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

            for (int i = 0; i < this.m_Squares.Capacity; i++)
            {
                if(m_squaresOnRowCounter == i_BoardSize / 2)
                {
                    m_squaresOnRowCounter = 0;
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
                
                m_squaresOnRowCounter++;
                this.m_Squares.Add(new Square(m_currentRow, m_currentCol));
                m_currentCol += 2;
            }
        }

        public Square getSquare(int i_Index)
        {
            return this.m_Squares[i_Index];
        }

        public Square checkExistance(int i_CurrentRow, int i_CurrentCol)
        {
            Square checkSquare = new Square(i_CurrentRow, i_CurrentCol);
            
            foreach(Square currentFig in this.m_Squares)
            {
                if (currentFig.Equals(checkSquare))
                {
                    return currentFig;
                }
            }

            return null;  
        }

        public void UpdateObligatoryMoves(Board i_GameBoard)
        {
            this.m_ObligatoryMoves = new List<Move>();
            
            foreach (Square currSquare in this.m_Squares)
            {
                this.m_ObligatoryMoves.AddRange(i_GameBoard.EliminationAvailable(currSquare, this));
            }
        }

        public void deleteSquare(Square i_SquareToDelete)
        {
            foreach (Square currSquare in this.m_Squares)
            {
                if (i_SquareToDelete == currSquare)
                { 
                    this.m_Squares.Remove(currSquare);
                    break;
                }
            }
        }

        public void UpdateSquare(Move i_PlayerMove, int i_BoardSize)
        {
            foreach(Square currentSquare in this.m_Squares)
            {
                if(currentSquare.Equals(i_PlayerMove.SquareFrom))
                {
                    currentSquare.Col = i_PlayerMove.SquareTo.Col;
                    currentSquare.Row = i_PlayerMove.SquareTo.Row;
                    break;
                }
            }
        }

        public bool isMoveObligatory(Move i_UserInput)
        {
            foreach (Move optionaObligatorylMove in this.m_ObligatoryMoves)
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
            Square availableSquare;

            foreach(Square currentSquare in this.m_Squares)
            {
                availableSquare = this.AvailableMove(currentSquare, i_GameBoard);
                if (!object.ReferenceEquals(availableSquare, null))
                {
                    this.hasAvailableMoves = true;
                    return;
                }    
            }

            this.hasAvailableMoves = false;
        }

        public bool isMyKing(Square.e_SquareType i_SquareType)
        {
            bool isMyKingRes = false; 
            if (i_SquareType == Square.e_SquareType.playerOneKing && this.m_PlayerType == Square.e_SquareType.playerOne)
            {
                isMyKingRes = true;
            }
            else if (i_SquareType == Square.e_SquareType.playerTwoKing && (this.m_PlayerType == Square.e_SquareType.playerTwo || this.m_PlayerType == Square.e_SquareType.playerPC) )
            {
                isMyKingRes = true;
            }

            return isMyKingRes;
        }

        public Square AvailableMove(Square i_CurrentSquare, Board i_GameBoard)
        {
            Square leftFig, rightFig;
            Square.e_SquareType squareType;
            squareType = i_GameBoard.getSquareStatus(i_CurrentSquare);

            if (squareType == Square.e_SquareType.playerOneKing || squareType == Square.e_SquareType.playerTwoKing)
            {
                leftFig = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.e_Direction.TopLeft);
                rightFig = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.e_Direction.TopRight);
                if (i_GameBoard.getSquareStatus(leftFig) != Square.e_SquareType.none && i_GameBoard.getSquareStatus(rightFig) != Square.e_SquareType.none)
                {
                    leftFig = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.e_Direction.BottomLeft);
                    rightFig = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.e_Direction.BottomRight);
                }
            }
            else if (squareType == Square.e_SquareType.playerOne)
            {
                leftFig = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.e_Direction.TopLeft);
                rightFig = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.e_Direction.TopRight);
            }
            else
            {
                leftFig = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.e_Direction.BottomLeft);
                rightFig = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.e_Direction.BottomRight);
            }

            if (i_GameBoard.getSquareStatus(leftFig) == Square.e_SquareType.none)
            {
                return leftFig;
            }
            else if (i_GameBoard.getSquareStatus(rightFig) == Square.e_SquareType.none)
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
            int randIndx = rand.Next(this.m_Squares.Count); // a num between 0 - squareCount - 1
            Square nextSquare = this.AvailableMove(this.m_Squares[randIndx], i_GameBoard);
            Move computerMove = null;
            Square fromSquare;

            if (this.hasAvailableMove)
            {
                while (object.ReferenceEquals(nextSquare, null))
                {
                    randIndx++;
                    if (randIndx >= this.m_Squares.Count)
                    {
                        randIndx = 0;
                    }

                    nextSquare = this.AvailableMove(this.m_Squares[randIndx], i_GameBoard);
                }

                fromSquare = new Square(this.m_Squares[randIndx].Row, this.m_Squares[randIndx].Col);
                computerMove = new Move(fromSquare, nextSquare);
            }

            return computerMove;  
        }

        public Move RandomObligatoryMove()
        {
            Random pcRandomObligatoryMove = new Random();
            int randomIndex = pcRandomObligatoryMove.Next(this.m_ObligatoryMoves.Count);
            Square squareFrom = new Square(this.m_ObligatoryMoves[randomIndex].SquareFrom.Row, this.m_ObligatoryMoves[randomIndex].SquareFrom.Col);
            Square squareTo = new Square(this.m_ObligatoryMoves[randomIndex].SquareTo.Row, this.m_ObligatoryMoves[randomIndex].SquareTo.Col);
            Move randomObligatoryMove = new Move(squareFrom, squareTo);

            return randomObligatoryMove;
        }

        public void initPlayer(int i_BoardSize)
        {
            this.squaresNum = i_BoardSize;
            this.initSquares(i_BoardSize);
            this.Score = this.squaresNum + this.m_BonusScore;
        }
    }
}
