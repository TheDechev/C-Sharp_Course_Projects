using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B18_Ex02
{
    public class Game
    {
        private Player m_PlayerOne;

        private Player m_PlayerTwo;

        private Board m_Board;

        private int m_TurnCounter = 0;

        public void StartGame()
        {
            Console.WriteLine("Welcome to the game!" + Environment.NewLine);
            this.initGame();
            string playerChoice = string.Empty;
            bool movedSuccesfully = true;
            Player currentPlayer;
            Move inputMove = new Move();

            while (!inputMove.Equals(null))
            {
                Ex02.ConsoleUtils.Screen.Clear(); // clearing the screen for a new round
                printScore();
                this.m_Board.PrintBoard();
                currentPlayer = whichTurn();

                Console.Write(currentPlayer.Name + "'s turn:");
                currentPlayer.UpdateObligatoryMoves(m_Board);
                currentPlayer.UpdateAvailableMovesIndicator(m_Board);
                inputMove = getUserInput();
            
                if (currentPlayer.ObligatoryMovesCount > 0)
                {
                    playObligatoryMove(currentPlayer, inputMove);
                }
                else if(currentPlayer.hasAvailableMove)
                {
                    movedSuccesfully = m_Board.updateBoardAfterMove(inputMove, currentPlayer, false);
                    while (!movedSuccesfully)
                    {
                        Console.WriteLine("Invalid move, try again . . .");
                        inputMove = getUserInput();
                        movedSuccesfully = m_Board.updateBoardAfterMove(inputMove, currentPlayer, false);
                    }

                } else
                {
                    Console.WriteLine(currentPlayer.Name + " lost.");
                    return;
                }

                this.m_TurnCounter++;
            }
        }

        public Player whichTurn()
        {
            Player whichPlayer;

            // first players turn
            if (this.m_TurnCounter % 2 == 0)
            {
                whichPlayer = m_PlayerOne;
            }
            // second players turn
            else
            {
                whichPlayer = m_PlayerTwo;
            }

            return whichPlayer;
        }

        public void initGame()
        {
            string playerChoice;

            this.AddNewPlayer(Figure.e_SquareType.playerOne);

            this.getBoardSizeFromUser();

            this.m_PlayerOne.figuresNum = this.m_Board.Size;
            this.m_PlayerOne.initFigures(Figure.e_SquareType.playerOne, this.m_Board.Size);

            Console.WriteLine("Choose your opponent: ");
            Console.WriteLine("1. Another player ");
            Console.WriteLine("2. The PC ");

            playerChoice = Console.ReadLine();

            while (playerChoice != "1" && playerChoice != "2")
            {
                playerChoice = Console.ReadLine();
            }
            Console.WriteLine();
            if (playerChoice == "1")
            {
                this.AddNewPlayer(Figure.e_SquareType.playerTwo);
                this.m_PlayerTwo.figuresNum = this.m_Board.Size;
                this.m_PlayerTwo.initFigures(Figure.e_SquareType.playerTwo, this.m_Board.Size);
                this.m_Board.addPlayersToBoard(this.m_PlayerOne, this.m_PlayerTwo);
            }
            else
            {
                //TODO: implement computer playing;
                Console.WriteLine("The Computer is not ready to play againsnt you yet!");
                return;
            }
        }

        public void AddNewPlayer(Figure.e_SquareType playerType)
        {
            string m_PlayerName = string.Empty;

            if (playerType != Figure.e_SquareType.playerPC)
            {
                Console.WriteLine("Please enter your name: ");
                m_PlayerName = Console.ReadLine();

                while (m_PlayerName.Length > 20 || m_PlayerName.Length == 0)
                {
                    Console.WriteLine("Invalid name size, please enter your name again...");
                    m_PlayerName = Console.ReadLine();
                }
            }
            Console.WriteLine();
            if (playerType == Figure.e_SquareType.playerOne)
            {
                this.m_PlayerOne = new Player();
                this.m_PlayerOne.Shape = 'X';
                this.m_PlayerOne.Name = m_PlayerName;
                this.m_PlayerOne.PlayerType = Figure.e_SquareType.playerOne;
            }
            else if (playerType == Figure.e_SquareType.playerTwo)
            {
                this.m_PlayerTwo = new Player();
                this.m_PlayerTwo.Shape = 'O';
                this.m_PlayerTwo.Name = m_PlayerName;
                this.m_PlayerTwo.PlayerType = Figure.e_SquareType.playerTwo;
            }
            else
            {//// TODO: PC implemantion 
            }
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
            Console.WriteLine();

            this.m_Board = new Board(int.Parse(playerChoice));
        }

        public Move getUserInput()
        {
            string playerInput = Console.ReadLine();
            Move inputMove;

            if(playerInput == "Q")
            {
                return null;
            }
            else
            {
                Figure currentFigure = new Figure();
                Figure nextMoveFigure = new Figure();

                playerInput = playerInput.Replace(" ", string.Empty);


                while (!isUserMoveValid(playerInput))
                {
                    playerInput = Console.ReadLine();
                    playerInput = playerInput.Replace(" ", string.Empty);
                }

                currentFigure.updateFigureWithString(playerInput.Substring(0, 2));
                nextMoveFigure.updateFigureWithString(playerInput.Substring(3, 2));

                inputMove =  new Move(currentFigure, nextMoveFigure);
            }

            return inputMove;
        }

        public bool isUserMoveValid(string i_playerMov)
        {


            return i_playerMov.Length == 5 && char.IsUpper(i_playerMov[0]) && i_playerMov[0] < ('A' + m_Board.Size) &&
                    char.IsLower(i_playerMov[1]) && i_playerMov[0] < ('a' + m_Board.Size) && i_playerMov[2] == '>' &&
                    char.IsUpper(i_playerMov[3]) && i_playerMov[3] < ('A' + m_Board.Size) &&
                    char.IsLower(i_playerMov[4]) && i_playerMov[4] < ('a' + m_Board.Size);


        }

        public void eliminateOpponent(Move i_UserInput, Player i_CurrentPlayer)
        {
            bool movedSuccesfully;
            // TODO: Move to a function
            movedSuccesfully = m_Board.updateBoardAfterMove(i_UserInput, i_CurrentPlayer, true);

            while (!movedSuccesfully)
            {
                Console.WriteLine("Invalid move, try again . . .");
                i_UserInput = getUserInput();
                movedSuccesfully = m_Board.updateBoardAfterMove(i_UserInput, i_CurrentPlayer, true);
            }

            int opponnentCol = ((i_UserInput.FigureTo.Col - i_UserInput.FigureFrom.Col) / 2) + i_UserInput.FigureFrom.Col;
            int opponnentRow = ((i_UserInput.FigureTo.Row - i_UserInput.FigureFrom.Row) / 2) + +i_UserInput.FigureFrom.Row;

            m_Board.updateBoard(opponnentRow, opponnentCol, Figure.e_SquareType.none);

            if (i_CurrentPlayer.PlayerType == Figure.e_SquareType.playerOne)
            {
                m_PlayerTwo.deleteFigure(new Figure(opponnentRow, opponnentCol));
            }
            else
            {
                m_PlayerOne.deleteFigure(new Figure(opponnentRow, opponnentCol));
            }




        }

        public void playObligatoryMove(Player i_CurrentPlayer, Move i_InputMove)
        {
            while (i_CurrentPlayer.ObligatoryMovesCount != 0)  // Player Must Kill the rival 
            {
                if (i_CurrentPlayer.isMoveObligatory(i_InputMove)) // Move was one of the obligatory options
                {
                    eliminateOpponent(i_InputMove, i_CurrentPlayer);
                    i_CurrentPlayer.Score++;
                    i_CurrentPlayer.UpdateObligatoryMoves(m_Board);
                    if (i_CurrentPlayer.ObligatoryMovesCount > 0)
                    {
                        Console.WriteLine(i_CurrentPlayer.Name + " has another turn");
                        i_InputMove = getUserInput();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid move, you must eliminate your opponnent!");
                    i_InputMove = getUserInput();
                }
            }
        }

        public void printScore()
        {
            Console.WriteLine(string.Concat(Enumerable.Repeat("==", m_Board.Size * 2 + 2)) + Environment.NewLine);
            Console.WriteLine(m_PlayerOne.Name + "'s Score is: " + m_PlayerOne.Score);
            Console.WriteLine(m_PlayerTwo.Name + "'s Score is: " + m_PlayerTwo.Score + Environment.NewLine);
            Console.WriteLine(string.Concat(Enumerable.Repeat("==", m_Board.Size*2 + 2)) + Environment.NewLine + Environment.NewLine);
        }

    }
}
