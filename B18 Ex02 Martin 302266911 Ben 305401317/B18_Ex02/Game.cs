using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex02
{
    public class Game
    {
        private enum e_PlayerIndicator
        {
            playerOne = 1,
            playerTwo = 2
        }

        private Player m_PlayerOne;

        private Player m_PlayerTwo;

        private Board m_Board;

        private int m_TurnCounter = 0;
   
        public void StartGame()
        {
            Console.WriteLine("Welcome to the game! ");
            this.initGame();
            string playerChoice = string.Empty;
            
            while (playerChoice != "Exit")
            {
                Ex02.ConsoleUtils.Screen.Clear(); // clearing the screen for a new round
                this.m_Board.PrintBoard();

                if (this.m_TurnCounter % 2 == 0)
                {// first players turn
                    Console.Write(this.m_PlayerOne.Name + "'s turn:");
                }
                else
                {// second players turn
                    Console.Write(this.m_PlayerTwo.Name + "'s turn:");
                }

                playerChoice = Console.ReadLine();
                bool isCurrent = true;
                Figure currentFigure = new Figure();
                Figure nextMoveFigure = new Figure();

                currentFigure.updateFigurePosAccordingToPlayerMove(playerChoice, isCurrent);
                nextMoveFigure.updateFigurePosAccordingToPlayerMove(playerChoice, !isCurrent);

                //player enters string : currentPos / nextPos
                //(has figure there)
                currentFigure = this.m_PlayerTwo.checkExistance(5, 4);

                if (currentFigure != null)
                {
                    if (this.m_Board.isEmptySpot(6, 5))
                    {
                        currentFigure.Row = 6;
                        currentFigure.Col = 5;
                        this.m_Board.updateBoard(5, 4, 6, 5, (int)e_PlayerIndicator.playerTwo);
                    }
                }

                this.m_TurnCounter++;
            }
        }

        public void initGame()
        {
            string playerChoice;

            this.AddNewPlayer();

            this.getBoardSizeFromUser();

            this.m_PlayerOne.figuresNum = this.m_Board.Size;
            this.m_PlayerOne.initFigures(0, this.m_Board.Size);

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
                this.AddNewPlayer();
                this.m_PlayerTwo.figuresNum = this.m_Board.Size;
                this.m_PlayerTwo.initFigures(this.m_PlayerOne.lastFigure.Row + 3, this.m_Board.Size);
                this.m_Board.initBoard(this.m_PlayerOne, this.m_PlayerTwo);
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

            if (this.m_TurnCounter == 0)
            {
                this.m_PlayerOne = new Player();
                this.m_PlayerOne.Shape = 'X';
                this.m_PlayerOne.Name = m_PlayerName;
            }
            else
            {
                this.m_PlayerTwo = new Player();
                this.m_PlayerTwo.Shape = 'O';
                this.m_PlayerTwo.Name = m_PlayerName;
            }

            this.m_TurnCounter++;
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

            this.m_Board = new Board(int.Parse(playerChoice));
        }
    }
}
