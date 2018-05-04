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
        private Square.eSquareType m_PlayerType;
        private int m_Score;
        private bool m_HasAvailableMoves = true;
        private List<Square> m_Squares;
        private int m_BonusScore;

        public int BonusScore
        {
            get
            {
                return this.m_BonusScore;
            }

            set
            {
                this.m_BonusScore = value;
            }
        }

        public bool HasAvailableMove
        {
            get
            {
                return this.m_HasAvailableMoves;
            }
        }

        public int ObligatoryMovesCount
        {
            get
            {
                return this.m_ObligatoryMoves.Count;
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

        public int SquaresNum
        {
            get
            {
                return this.m_Squares.Count;
            }

            set
            {
                int squaresCount = ((value - 2) / 2) * (value / 2);     // Calculate the player deafult checkers num
                this.m_Squares = new List<Square>(squaresCount);
             }
        }

        public void InitPlayer(int i_BoardSize)
        {
            this.SquaresNum = i_BoardSize;
            this.InitSquares(i_BoardSize);
            this.Score = this.SquaresNum;
        }

        public Square.eSquareType PlayerType
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

        public Square GetSquare(int i_Index)
        {
            return this.m_Squares[i_Index];
        }

        public void InitSquares(int i_BoardSize)
        {
            int m_squaresOnRowCounter = 0;
            int m_currentCol = 0;
            int m_currentRow;

            if (this.m_PlayerType == Square.eSquareType.playerOne)
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
                if (m_squaresOnRowCounter == i_BoardSize / 2)
                {
                    m_squaresOnRowCounter = 0;
                    m_currentRow++;

                    if (m_currentCol == i_BoardSize)
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

        public void DeleteSquare(Square i_SquareToDelete)
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
            foreach (Square currentSquare in this.m_Squares)
            {
                if (currentSquare.Equals(i_PlayerMove.SquareFrom))
                {
                    currentSquare.Col = i_PlayerMove.SquareTo.Col;
                    currentSquare.Row = i_PlayerMove.SquareTo.Row;
                    break;
                }
            }
        }

        public Square AvailableMove(Square i_CurrentSquare, Board i_GameBoard)
        {
            Square leftSqr, rightSqr;
            Square.eSquareType squareType;
            squareType = i_GameBoard.GetSquareStatus(i_CurrentSquare);

            if (squareType == Square.eSquareType.playerOneKing || squareType == Square.eSquareType.playerTwoKing)
            {
                leftSqr = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.eDirection.TopLeft);
                rightSqr = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.eDirection.TopRight);
                if (i_GameBoard.GetSquareStatus(leftSqr) != Square.eSquareType.none && i_GameBoard.GetSquareStatus(rightSqr) != Square.eSquareType.none)
                {
                    leftSqr = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.eDirection.BottomLeft);
                    rightSqr = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.eDirection.BottomRight);
                }
            }
            else if (squareType == Square.eSquareType.playerOne)
            {
                leftSqr = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.eDirection.TopLeft);
                rightSqr = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.eDirection.TopRight);
            }
            else
            {
                leftSqr = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.eDirection.BottomLeft);
                rightSqr = i_GameBoard.GetSquareInDirection(i_CurrentSquare, Board.eDirection.BottomRight);
            }

            if (i_GameBoard.GetSquareStatus(leftSqr) == Square.eSquareType.none)
            {
                return leftSqr;
            }
            else if (i_GameBoard.GetSquareStatus(rightSqr) == Square.eSquareType.none)
            {
                return rightSqr;
            }
            else
            {
                return null;
            }
        }

        public void UpdateAvailableMovesIndicator(Board i_GameBoard)
        {
            Square availableSquare;

            foreach (Square currentSquare in this.m_Squares)
            {
                availableSquare = this.AvailableMove(currentSquare, i_GameBoard);
                if (!object.ReferenceEquals(availableSquare, null))
                {
                    this.m_HasAvailableMoves = true;
                    return;
                }
            }

            this.m_HasAvailableMoves = false;
        }

        public void UpdateObligatoryMoves(Board i_GameBoard)
        {
            this.m_ObligatoryMoves = new List<Move>();
            
            foreach (Square currSquare in this.m_Squares)
            {
                this.m_ObligatoryMoves.AddRange(i_GameBoard.EliminationAvailable(currSquare, this));
            }
        }

        public bool IsMoveObligatory(Move i_UserInput)
        {
            bool isMoveObligatory = false;

            foreach (Move optionaObligatorylMove in this.m_ObligatoryMoves)
            {
                if (optionaObligatorylMove.Equals(i_UserInput))
                {
                    isMoveObligatory = true;
                    break;
                }
            }

            return isMoveObligatory;
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

        public bool IsMyKing(Square.eSquareType i_SquareType)
        {
            bool isMyKingRes = false; 
            if (i_SquareType == Square.eSquareType.playerOneKing && this.m_PlayerType == Square.eSquareType.playerOne)
            {
                isMyKingRes = true;
            }
            else if (i_SquareType == Square.eSquareType.playerTwoKing && (this.m_PlayerType == Square.eSquareType.playerTwo || this.m_PlayerType == Square.eSquareType.playerPC) )
            {
                isMyKingRes = true;
            }

            return isMyKingRes;
        }

        public string ComputerMove(Board i_GameBoard)
        {
            Random rand = new Random();
            int randIndx = rand.Next(this.m_Squares.Count); 
            Square nextSquare = this.AvailableMove(this.m_Squares[randIndx], i_GameBoard);
            string computerMove = string.Empty;
            Square fromSquare;

            if (this.m_HasAvailableMoves)
            {
                this.UpdateObligatoryMoves(i_GameBoard);
                if(this.ObligatoryMovesCount > Move.k_ZeroObligatoryMoves)
                {
                    computerMove = this.RandomObligatoryMove().ToString();
                }
                else
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
                    computerMove = new Move(fromSquare, nextSquare).ToString();
                }   
            }

            return computerMove;  
        }
    }
}
