using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex02
{
    class Game
    {
        private enum e_PlayerIndicator { playerOne = 1, playerTwo = 2 }
        private Player m_PlayerOne;
        private Player m_PlayerTwo;
        private Board m_Board;

        private int m_TurnCounter = 0;
   
        public void StartGame()
        {
            int currentPosCol;
            int currentPosRow;
            Figure currentFigure;
            Console.WriteLine("Welcome to the game! ");
            initGame();
            string playerChoice = "";
            
            while (playerChoice != "Exit")
            {
                Ex02.ConsoleUtils.Screen.Clear(); // clearing the screen for a new round
                m_Board.PrintBoard();

                if (m_TurnCounter % 2 == 0) // first players turn
                {
                    Console.Write(m_PlayerOne.Name + "'s turn:");
                }
                else // second players turn
                {
                    Console.Write(m_PlayerTwo.Name + "'s turn:");
                }

                playerChoice = Console.ReadLine();

                //player enters string : currentPos / nextPos
                //(has figure there)
                currentFigure = m_PlayerTwo.checkExistance(5, 4);

                if (currentFigure != null)
                {
                    if (m_Board.isEmptySpot(6, 5))
                    {
                        currentFigure.Row = 6;
                        currentFigure.Col = 5;
                        m_Board.updateBoard(5, 4, 6, 5, (int)e_PlayerIndicator.playerTwo);

                    }
                }

                m_TurnCounter++;
            }

        }

        public void initGame()
        {
            string playerChoice;

            AddNewPlayer();
            
            getBoardSizeFromUser();

            m_PlayerOne.figuresNum = m_Board.Size;
            m_PlayerOne.initFigures(0, m_Board.Size);

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
                AddNewPlayer();
                m_PlayerTwo.figuresNum = m_Board.Size;
                m_PlayerTwo.initFigures(m_PlayerOne.lastFigure.Row + 3, m_Board.Size);
                m_Board.initBoard(m_PlayerOne, m_PlayerTwo);
            }
            else
            {
                //TODO: implement computer playing;
                Console.WriteLine("The Computer is not ready to play againsnt you yet!");
                return;
            }


        }

        public void AddNewPlayer()
        {
            string m_PlayerName;

            Console.WriteLine("Please enter your name: ");
            m_PlayerName = Console.ReadLine();

            while (m_PlayerName.Length > 20 || m_PlayerName.Length == 0)
            {
                Console.WriteLine("Invalid name size, please enter your name again...");
                m_PlayerName = Console.ReadLine();
            }

            if (m_TurnCounter == 0)
            {
                m_PlayerOne = new Player();
                m_PlayerOne.Shape = 'X';
                m_PlayerOne.Name = m_PlayerName;
               
            }
            else
            {
                m_PlayerTwo = new Player();
                m_PlayerTwo.Shape = 'O';
                m_PlayerTwo.Name = m_PlayerName;
            }

            m_TurnCounter++;

        }

        public void getBoardSizeFromUser()
        {
            string playerChoice;

            Console.WriteLine("Enter the board size: (6/8/10) ");
            playerChoice = Console.ReadLine();
            while (playerChoice != "6" && playerChoice != "8" && playerChoice != "10")
            {
                Console.WriteLine("Invalid board size, please enter the size again...");
                playerChoice = Console.ReadLine();
            }

            m_Board = new Board(int.Parse(playerChoice));
        }
    }
}
