using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex02
{
    class Game
    {
        private Player playerOne;
        private Player playerTwo;

        private readonly int boardSize;
        private const int requiredSpaceForFigure = 4;

        public Game(int i_BoardSize)
        {
            boardSize = i_BoardSize;
        }

        public void PrintGame()
        {
            char currentChar = 'a';

            for (int i = 0; i < boardSize * 2 + 1; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j <= boardSize* requiredSpaceForFigure + 1 ; j++)
                    {
                        Console.Write("=");
                    }
                }
                else
                {
                    Console.Write(currentChar++);

                    for (int j = 0; j <= boardSize; j++)
                    {
                        Console.Write("| ");
                        if (false) // the figures logic
                        {
                            Console.Write("X ");
                        }
                        else
                        {
                            Console.Write("  ");
                        }
                    }
                    
                }
                
                Console.WriteLine();
            }


            currentChar = 'a';
            Console.WriteLine();

        }

    }
}
