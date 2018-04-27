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
        private int boardSize = 8;
        private const int requiredSpaceForFigure = 4;
        private int turnCounter = 0;

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
        public void StartGame()
        {
            string playerChoice;
            playerOne = new Player();
            string playerName;

            Console.WriteLine("Welcome to the game!" + Environment.NewLine + "Please enter your name: ");
            playerName = Console.ReadLine();

            while (playerName.Length > 20 || playerName.Length == 0)
            {
                Console.WriteLine("Invalid name size, please enter your name again...");
                playerName = Console.ReadLine();
            }

            playerOne.Name = playerName;

            Console.WriteLine("Enter the board size: (6/8/10) ");
            while(!(int.TryParse(Console.ReadLine(),out boardSize)) || (boardSize!=6 && boardSize !=8 && boardSize != 10))
            {
                Console.WriteLine("Invalid board size, please enter the size again...");
            }
            PrintGame();

            Console.WriteLine("Choose your opponent: ");
            Console.WriteLine("1. Another player ");
            Console.WriteLine("2. The PC ");

            playerChoice = Console.ReadLine();
            while (playerChoice != "1" && playerChoice != "2")
            {
                playerChoice = Console.ReadLine();
            }

            if (playerChoice == "1")
            {
                playerTwo = new Player();
                Console.WriteLine("Please enter the second player's name: ");
                playerName = Console.ReadLine();

                while (playerName.Length > 20 || playerName.Length == 0)
                {
                    Console.WriteLine("Invalid name size, please enter your name again...");
                    playerName = Console.ReadLine();
                }

                playerTwo.Name = playerName;

            }
            else
            {
                //TODO: implement computer playing;
                Console.WriteLine("The Computer is not ready to play againsnt you yet!");
                return;
            }

            while (playerChoice != "Exit")
            {
                Ex02.ConsoleUtils.Screen.Clear(); // clearing the screen for a new round
                turnCounter++;
                PrintGame();
                if (turnCounter % 2 == 0) // first players turn
                {
                    Console.Write(playerOne.Name + "'s turn:");
                }
                else // second players turn
                {
                    Console.Write(playerTwo.Name + "'s turn:");
                }

                playerChoice = Console.ReadLine();
            }


        }

    }
}
