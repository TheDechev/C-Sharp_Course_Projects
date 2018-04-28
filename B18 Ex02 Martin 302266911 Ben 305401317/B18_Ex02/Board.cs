using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex02
{
    class Board
    {
        private enum e_PlayerIndicator { playerOne = 1, playerTwo = 2}
        private int m_BoardSize = m_DefaultBoardSize;
        private int[,] m_BoardGame;
        private const int m_RequiredSpaceForFigure = 4;
        private const int m_DefaultBoardSize = 8;

        public Board (int i_BoardSize)
        {
            m_BoardSize = i_BoardSize;
            m_BoardGame = new int[i_BoardSize, i_BoardSize];
        }

        public void PrintBoard()
        {
            char currentChar = 'a';
            int m_BoardRow = 0;

            for (int i = 0; i < m_BoardSize * 2 + 1; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j <= m_BoardSize * m_RequiredSpaceForFigure + 1; j++)
                    {
                        Console.Write("=");
                    }
                }
                else
                {
                    Console.Write(currentChar++);

                    for (int j = 0; j < m_BoardSize; j++)
                    {
                        Console.Write("| ");
                        // the figures logic
                        if (m_BoardGame[m_BoardRow, j] == (int) e_PlayerIndicator.playerOne) 
                        {
                            Console.Write("O ");
                        }
                        else if (m_BoardGame[m_BoardRow, j] == (int)e_PlayerIndicator.playerTwo)
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
            set
            {
                m_BoardSize = value;   
            }

            get
            {
                return m_BoardSize;
            }
        }

        public void initBoard(Player i_PlayerOne, Player i_PlayerTwo)
        {
            for (int i = 0; i < i_PlayerOne.figuresNum; i++)
            {
                m_BoardGame[i_PlayerOne.getFigure(i).Row, i_PlayerOne.getFigure(i).Col] = (int)e_PlayerIndicator.playerOne;
            }

            if(!(i_PlayerTwo == null))
            {
                for (int i = 0; i < i_PlayerTwo.figuresNum; i++)
                {
                    m_BoardGame[i_PlayerTwo.getFigure(i).Row, i_PlayerTwo.getFigure(i).Col] = (int)e_PlayerIndicator.playerTwo;
                }
            }


            
        }

        public bool isEmptySpot(int i_Row, int i_Col)
        {
            if(m_BoardGame[i_Row,i_Col] == 0)
            {
                return true;
            }
            return false;
        }

        public void updateBoard(int i_PrevRow, int i_PrevCol, int i_NextRow, int i_nextCol , int i_WhichPlayer)
        {
            m_BoardGame[i_PrevRow, i_PrevCol] = 0;
            m_BoardGame[i_NextRow, i_nextCol] = i_WhichPlayer;
        }

    }
}
