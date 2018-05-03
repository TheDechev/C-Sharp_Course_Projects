using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace B18_Ex02
{
    public class ConsoleInterface
    {
        public void createNewGame()
        {
            string firstPlayerName, secondPlayerName;
            int boardSize;
            int opponentChoice;
            CheckersGame newGame = new CheckersGame();

            printWelcomeMsg();
            firstPlayerName = getPlayerName(); //get player one name
            newGame.AddNewPlayer(firstPlayerName, Square.e_SquareType.playerOne); 
            boardSize = getBoardSizeFromUser();
            opponentChoice = getOpponnetOptions();

            if(opponentChoice == 1)
            {
                secondPlayerName = getPlayerName();
                newGame.AddNewPlayer(secondPlayerName, Square.e_SquareType.playerTwo);
            }
            else
            {
                secondPlayerName = "Computer";
                newGame.AddNewPlayer(string.Empty,Square.e_SquareType.playerPC);
            }

            newGame.createGameBoard(boardSize);
            runGame(newGame, boardSize);
        }

        private void runGame(CheckersGame i_Game,int i_BoardSize)
        {
            string playerChoice = string.Empty;
            Player currentPlayer = i_Game.PlayerOne;
            string previousMove = string.Empty;
            string userMove = string.Empty;

            CheckersGame.e_RoundOptions gameStatus;
            CheckersGame.e_RoundOptions currentRound = CheckersGame.e_RoundOptions.passRound;

            while (currentRound != CheckersGame.e_RoundOptions.gameOver)
            {
                ClearScreen();
                PrintBoard(i_Game.Board);
                PrintTurn(previousMove, currentPlayer);

                if (currentPlayer.PlayerType != Square.e_SquareType.playerPC)
                {
                    userMove = getUserMove(i_BoardSize);
                }
                else
                {
                    userMove = currentPlayer.ComputerMove(i_Game.Board);
                    Thread.Sleep(1200);
                }

                currentRound = i_Game.newRound(userMove);
 
                gameStatus = i_Game.checkGameStatus();

                if (gameStatus != CheckersGame.e_RoundOptions.passRound)
                {
                    currentRound = gameStatus;
                }

                handleRound(ref previousMove,ref userMove, ref currentRound, currentPlayer, i_Game);

                currentPlayer = i_Game.getCurrentPlayer();

            }
        }

        private void handleRound(ref string io_PreviousMove, ref string io_UserMove, ref CheckersGame.e_RoundOptions io_CurrentRound, Player i_CurrentPlayer, CheckersGame i_Game)
        {
            if (io_CurrentRound == CheckersGame.e_RoundOptions.weakPlayerQuits)
            {
                if (!playerWantsAnotherRound())
                {
                    if (i_CurrentPlayer.PlayerType == Square.e_SquareType.playerOne)
                    {
                        printEndGame(i_Game, Square.e_SquareType.playerOne);
                    }
                    else
                    {
                        printEndGame(i_Game, Square.e_SquareType.playerTwo);

                    }
                    io_CurrentRound = CheckersGame.e_RoundOptions.gameOver;
                }
                else // player wants to have another game
                {
                    i_Game.createGameBoard(i_Game.Board.Size);
                    io_PreviousMove = string.Empty;
                }
            }
            else if (io_CurrentRound == CheckersGame.e_RoundOptions.strongPlayerWantsToQuit) // another round
            {
                Console.WriteLine("You are not the weak player! Enter a valid move. . .");
                Thread.Sleep(800);
            }
            else if (io_CurrentRound == CheckersGame.e_RoundOptions.playerDidntEnterObligatoryMove) // another round
            {
                Console.WriteLine("Invalid move, you must eliminate your opponnent!");
                Thread.Sleep(800);
            }
            else if (io_CurrentRound == CheckersGame.e_RoundOptions.currentPlayerHasAnotherRound) // another round
            {
                Console.WriteLine(Environment.NewLine + i_CurrentPlayer.Name + " has another turn");
                Thread.Sleep(800);
            }
            else if (io_CurrentRound == CheckersGame.e_RoundOptions.playerOneWon)
            {
                ClearScreen();
                PrintBoard(i_Game.Board);
                printEndGame(i_Game, Square.e_SquareType.playerOne);
                io_CurrentRound = CheckersGame.e_RoundOptions.gameOver;
            }
            else if (io_CurrentRound == CheckersGame.e_RoundOptions.playerTwoWon)
            {
                ClearScreen();
                PrintBoard(i_Game.Board);
                printEndGame(i_Game, Square.e_SquareType.playerTwo);
                io_CurrentRound = CheckersGame.e_RoundOptions.gameOver;
            }
            else if (io_CurrentRound == CheckersGame.e_RoundOptions.gameIsATie)
            {
                ClearScreen();
                PrintBoard(i_Game.Board);
                printEndGame(i_Game, Square.e_SquareType.none);
                io_CurrentRound = CheckersGame.e_RoundOptions.gameOver;
            }
            else if (io_CurrentRound == CheckersGame.e_RoundOptions.playerEnteredInvalidMove)
            {
                printInvalidMsg();
                Thread.Sleep(800);
            }
            else
            {
                io_PreviousMove = i_CurrentPlayer.Name + "'s move was (" + i_CurrentPlayer.Shape + "): " + io_UserMove;
            }
        }

        private void printWelcomeMsg()
        {
            Console.WriteLine("Welcome to the game!" + Environment.NewLine);
        }

        private void PrintBoard(Board i_Board)
        {
            int requiredSpaceForSquare = 4;
            char currentChar = 'A';
            int boardRow = 0;

            Console.Write(" ");

            for (int j = 0; j < i_Board.Size; j++)
            {
                Console.Write("  " + currentChar++ + " ");
            }

            Console.WriteLine();
            currentChar = 'a';

            for (int i = 0; i < (i_Board.Size * 2) + 1; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j <= (i_Board.Size * requiredSpaceForSquare) + 1; j++)
                    {
                        Console.Write("=");
                    }
                }
                else
                {
                    Console.Write(currentChar++);

                    for (int j = 0; j < i_Board.Size; j++)
                    {
                        Console.Write("| ");
                        //// the squares logic

                        if (i_Board.getSquareStatus(boardRow, j) == Square.e_SquareType.playerOne)
                        {
                            Console.Write("X ");
                        }
                        else if (i_Board.getSquareStatus(boardRow, j) == Square.e_SquareType.playerTwo || i_Board.getSquareStatus(boardRow, j) == Square.e_SquareType.playerPC)
                        {
                            Console.Write("O ");
                        }
                        else if (i_Board.getSquareStatus(boardRow, j) == Square.e_SquareType.playerOneKing)
                        {
                            Console.Write("K ");
                        }
                        else if (i_Board.getSquareStatus(boardRow, j) == Square.e_SquareType.playerTwoKing)
                        {
                            Console.Write("U ");
                        }
                        else
                        {
                            Console.Write("  ");
                        }
                    }

                    Console.Write("|");
                    boardRow++;
                }

                Console.WriteLine();
            }

            currentChar = 'a';
            Console.WriteLine();
        }

        private int getBoardSizeFromUser()
        {
            string playerChoice;

            Console.WriteLine("Enter the board size: < 6 / 8 / 10 > ");
            playerChoice = Console.ReadLine();
            while (playerChoice != "6" && playerChoice != "8" && playerChoice != "10")
            {
                Console.WriteLine("Invalid board size, please enter the size again...");
                playerChoice = Console.ReadLine();
            }

            Console.Write(Environment.NewLine);

            return int.Parse(playerChoice);
        }

        private void PrintScore(Player i_WinningPlayer)
        {
            Console.Write(i_WinningPlayer.Name + "won the game with a score of: " + i_WinningPlayer.Score);
        }

        private void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private int getOpponnetOptions()
        {
            Console.WriteLine("Choose your opponent: ");
            Console.WriteLine("1. Another player ");
            Console.WriteLine("2. The PC ");

            string playerChoice = Console.ReadLine();

            while (playerChoice != "1" && playerChoice != "2")
            {
                playerChoice = Console.ReadLine();
            }

            Console.WriteLine();

            return int.Parse(playerChoice);
        }
        
        private string getPlayerName()
        {   
            Console.WriteLine("Please enter your name: ");
            string playerName = Console.ReadLine();

            while (playerName.Length > 20 || playerName.Length == 0)
            {
                Console.WriteLine("Invalid name size, please enter your name again...");
                playerName = Console.ReadLine();
            }
   
            Console.Write(Environment.NewLine);

            return playerName;
        }

        private void PrintTurn(string i_PreviousMove, Player i_CurrentPlayerName)
        {
            if (!i_PreviousMove.Equals(string.Empty))
            {
                Console.WriteLine(i_PreviousMove);
            }

            Console.Write(i_CurrentPlayerName.Name + "'s turn:");
        }

        private string getUserMove(int i_BoardSize)
        {
            string playerInput = Console.ReadLine();

            if (playerInput != "Q" && playerInput != "q")
            {
                playerInput = playerInput.Replace(" ", string.Empty);

                while (!isUserInputMoveValid(playerInput, i_BoardSize))
                {
                    Console.WriteLine("Invalid input, try again. . .");
                    playerInput = Console.ReadLine();
                    playerInput = playerInput.Replace(" ", string.Empty);
                }
            }

            return playerInput;
        }

        private bool isUserInputMoveValid(string i_PlayerMove, int i_BoardSize)
        {
            return i_PlayerMove.Length == 5 && char.IsUpper(i_PlayerMove[0]) && i_PlayerMove[0] < ('A' + i_BoardSize) &&
                    char.IsLower(i_PlayerMove[1]) && i_PlayerMove[0] < ('a' + i_BoardSize) && i_PlayerMove[2] == '>' &&
                    char.IsUpper(i_PlayerMove[3]) && i_PlayerMove[3] < ('A' + i_BoardSize) &&
                    char.IsLower(i_PlayerMove[4]) && i_PlayerMove[4] < ('a' + i_BoardSize);
        }

        private bool playerWantsAnotherRound()
        {
            string playerChoice;
            bool anotherRound = false;
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("Would you like to play another round? <Y/N>");
            playerChoice = Console.ReadLine();
            while (playerChoice != "Y" && playerChoice != "N" && playerChoice != "y" && playerChoice != "n")
            {
                Console.WriteLine("Invalid input, try again. . . ");
                playerChoice = Console.ReadLine();
            }

            Console.Write(Environment.NewLine);

            if (playerChoice != "n" && playerChoice !="N")
            {
                anotherRound = true;
            }

            return anotherRound;     
        }

        private void printEndGame(CheckersGame i_Game, Square.e_SquareType i_Winner)
        {

            if(i_Winner == Square.e_SquareType.playerOne)
            {
                Console.Write(Environment.NewLine + i_Game.PlayerOne.Name + " won the game!");

            }
            else if (i_Winner == Square.e_SquareType.playerTwo)
            {
                Console.Write(Environment.NewLine + i_Game.PlayerTwo.Name + " won the game!");
            }
            else
            {
                Console.WriteLine("Its a tie!");
            }

            Console.Write(Environment.NewLine);
            Console.WriteLine(i_Game.PlayerOne.Name + "'s score is: " + i_Game.PlayerOne.Score);
            Console.WriteLine(i_Game.PlayerTwo.Name + "'s score is: " + i_Game.PlayerOne.Score);


        }

        private string getAnotherMoveFromCurrentPlayer(int i_BoardSize, string i_PlayerName)
        {

            return getUserMove(i_BoardSize);
        }

        private static void printInvalidMsg()
        {
            Console.WriteLine("Invalid move, try again . . .");
        }
    }
}
