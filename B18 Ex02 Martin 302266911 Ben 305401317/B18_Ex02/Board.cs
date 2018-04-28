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

        private enum e_PlayerIndicator
        {
            playerOne = 1,
            playerTwo = 2
        }

        private int m_BoardSize = k_DefaultBoardSize;

        private int[,] m_BoardGame;

        public Board (int i_BoardSize)
        {
            this.m_BoardSize = i_BoardSize;
            this.m_BoardGame = new int[i_BoardSize, i_BoardSize];
        }

        public void PrintBoard()
        {
            char currentChar = 'a';
            int m_BoardRow = 0;

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

                        if (this.m_BoardGame[m_BoardRow, j] == (int) e_PlayerIndicator.playerOne) 
                        {
                            Console.Write("O ");
                        }
                        else if (this.m_BoardGame[m_BoardRow, j] == (int)e_PlayerIndicator.playerTwo)
                        {
                            Console.Write("X ");
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
                this.m_BoardGame[i_PlayerOne.getFigure(i).Row, i_PlayerOne.getFigure(i).Col] = (int)e_PlayerIndicator.playerOne;
            }

            if(!(i_PlayerTwo == null))
            {
                for (int i = 0; i < i_PlayerTwo.figuresNum; i++)
                {
                    this.m_BoardGame[i_PlayerTwo.getFigure(i).Row, i_PlayerTwo.getFigure(i).Col] = (int)e_PlayerIndicator.playerTwo;
                }
            }
        }

        public bool isEmptySpot(int i_Row, int i_Col)
        {
            if(this.m_BoardGame[i_Row, i_Col] == 0)
            {
                return true;
            }

            return false;
        }

        public void updateBoard(int i_PrevRow, int i_PrevCol, int i_NextRow, int i_nextCol, int i_WhichPlayer)
        {
            this.m_BoardGame[i_PrevRow, i_PrevCol] = 0;
            this.m_BoardGame[i_NextRow, i_nextCol] = i_WhichPlayer;
        }
    }
}
