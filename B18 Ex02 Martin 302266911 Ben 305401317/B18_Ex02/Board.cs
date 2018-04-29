using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex02
{
    public class Board
    {
        private const int k_RequiredSpaceForFigure = 4;

        private const int k_DefaultBoardSize = 8;

        private int m_BoardSize = k_DefaultBoardSize;

        private int[,] m_BoardGame;

        public Board (int i_BoardSize)
        {
            this.m_BoardSize = i_BoardSize;
            this.m_BoardGame = new int[i_BoardSize, i_BoardSize];
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

                        if (this.m_BoardGame[m_BoardRow, j] == (int) Player.e_PlayerType.playerOne) 
                        {
                            Console.Write("X ");
                        }
                        else if (this.m_BoardGame[m_BoardRow, j] == (int)Player.e_PlayerType.playerTwo)
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

        public void initBoard(Player i_PlayerOne, Player i_PlayerTwo)
        {
            for (int i = 0; i < i_PlayerOne.figuresNum; i++)
            {
                this.m_BoardGame[i_PlayerOne.getFigure(i).Row, i_PlayerOne.getFigure(i).Col] = (int)Player.e_PlayerType.playerOne;
            }

            if(!(i_PlayerTwo == null))
            {
                for (int i = 0; i < i_PlayerTwo.figuresNum; i++)
                {
                    this.m_BoardGame[i_PlayerTwo.getFigure(i).Row, i_PlayerTwo.getFigure(i).Col] = (int)Player.e_PlayerType.playerTwo;
                }
            }
        }

        public List<Move> EliminationAvailable(int i_Row, int i_Col, Player.e_PlayerType i_currentPlayer)
        {
            int offsetCol = 1;  //X
            int offsetRow = -1;  //Y
            int addition = 1;
            int currentPos;
            bool isPosValid;
            List<Move> eliminationList = new List<Move>();
            Figure currentFigure = new Figure(i_Row, i_Col);

            //check right pos

            if (!(i_currentPlayer == Player.e_PlayerType.playerOne))
            {
                addition = -1;
            }

            isPosValid = isPositionValid(i_Row + offsetRow * (addition + 1), i_Col + offsetCol + 1);
            if (isPosValid)
            {
                currentPos = m_BoardGame[i_Row + offsetRow * addition, i_Col + offsetCol];
                if (currentPos != (int)i_currentPlayer && currentPos != (int)Player.e_PlayerType.none)
                {
                    offsetCol++;
                    offsetRow--;
                    currentPos = m_BoardGame[i_Row + offsetRow*addition, i_Col + offsetCol];

                    if (currentPos == (int)Player.e_PlayerType.none)
                    {
                        Move moveRight = new Move(currentFigure, new Figure(i_Row + offsetRow * addition, i_Col + offsetCol));
                        eliminationList.Add(moveRight);
                    }
                }
            }

            offsetCol = -1;
            offsetRow = -1;
            //check left pos
            isPosValid = isPositionValid(i_Row + offsetRow* (addition + 1), i_Col + offsetCol - 1);
            if (isPosValid)
            {
                currentPos = m_BoardGame[i_Row + offsetRow * addition, i_Col + offsetCol];
                if (currentPos != (int)i_currentPlayer && currentPos != (int)Player.e_PlayerType.none)
                {
                    offsetCol--;
                    offsetRow--;
                    currentPos = m_BoardGame[i_Row + offsetRow * addition, i_Col + offsetCol];
                    if (currentPos == (int)Player.e_PlayerType.none)
                    {

                        Move moveLeft = new Move(currentFigure, new Figure(i_Row + offsetRow * addition, i_Col + offsetCol));
                        eliminationList.Add(moveLeft);
                    }
                }
            }

            return eliminationList;
        }

        public bool updateBoardAfterMove(Move i_UserMove, Player.e_PlayerType i_WhichPlayer)
        {
            bool fromUpdate, toUpdate;
            
            fromUpdate = updateBoard(i_UserMove.FigureFrom.Row, i_UserMove.FigureFrom.Col, Player.e_PlayerType.none);
            toUpdate = updateBoard(i_UserMove.FigureTo.Row, i_UserMove.FigureTo.Col, i_WhichPlayer);

            return fromUpdate && toUpdate;
        }

        public bool updateBoard(int i_Row, int i_Col, Player.e_PlayerType i_Value )
        {
            if (isPositionValid(i_Row, i_Col))
            {
                this.m_BoardGame[i_Row, i_Col] = (int)i_Value;
                return true;
            }
            return false;
        }

        private bool isPositionValid(int i_Row, int i_Col)
        {
            return (i_Row < m_BoardSize && i_Col < m_BoardSize && i_Row >= 0 && i_Col >= 0);
        }
    }
}
