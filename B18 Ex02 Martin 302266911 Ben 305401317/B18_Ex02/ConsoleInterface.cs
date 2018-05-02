using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void runGame(CheckersGame i_Game,int i_BoardSize)
        {
            string playerChoice = string.Empty;
            string currentPlayName = i_Game.PlayerOne.Name, previousPlayerName = i_Game.PlayerTwo.Name;
            string previousMove = string.Empty;
            string userMove = string.Empty;
            CheckersGame.e_RoundOptions currentRound = CheckersGame.e_RoundOptions.passRound;

            while (currentRound != CheckersGame.e_RoundOptions.gameOver)
            {
                ClearScreen();
                PrintBoard(i_Game.Board);
                userMove = getUserMove(i_BoardSize);
                PrintTurn(previousMove, previousPlayerName);

                currentRound = i_Game.newRound(userMove);

                if(currentRound == CheckersGame.e_RoundOptions.weakPlayerQuits)
                {
                    if (!playerWantsAnotherRound())
                    {
                        printEndGame(null, null); //TODO: fix input parameters
                        break;
                    }
                    else // player wants to have another game
                    {
                        ClearScreen();
                        i_Game.createGameBoard(i_BoardSize);
                        PrintBoard(i_Game.Board);
                        PrintTurn(previousMove, previousPlayerName);
                        userMove = getUserMove(i_BoardSize);
                    }
                }
                else if (currentRound == CheckersGame.e_RoundOptions.strongPlayerWantsToQuit) // another round
                {
                    userMove = getMoveFromStrongPlayer(i_BoardSize);
                }
                else if (currentRound == CheckersGame.e_RoundOptions.playerDidntEnterObligatoryMove) // another round
                {
                    userMove = getObligatoryMove(i_BoardSize);
                }
                else if (currentRound == CheckersGame.e_RoundOptions.currentPlayerHasAnotherRound) // another round
                {
                    userMove = getAnotherMoveFromCurrentPlayer(i_BoardSize,currentPlayName);
                }
                else if (currentRound == CheckersGame.e_RoundOptions.playerOneWon)
                {
                    printEndGame(i_Game.PlayerOne);
                    currentRound = CheckersGame.e_RoundOptions.gameOver;// TODO: Split to 2 enums
                }
                else if (currentRound == CheckersGame.e_RoundOptions.playerTwoWon)
                {
                    printEndGame(i_Game.PlayerTwo);
                    currentRound = CheckersGame.e_RoundOptions.gameOver;// TODO: Split to 2 enums
                }
                else if (currentRound == CheckersGame.e_RoundOptions.gameIsATie)
                {
                    printEndGame(null);
                    currentRound = CheckersGame.e_RoundOptions.gameOver;// TODO: Split to 2 enums
                }

                
                    
                


            }
        }

        public  void printWelcomeMsg()
        {
            Console.WriteLine("Welcome to the game!" + Environment.NewLine);
        }

        public  void PrintBoard(Board i_Board)
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

        public  int getBoardSizeFromUser()
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

        public  void PrintScore(Player i_WinningPlayer)
        {
            Console.Write(i_WinningPlayer.Name + "won the game with a score of: " + i_WinningPlayer.Score);
        }

        public  void ClearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        public  int getOpponnetOptions()
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

        public  string getPlayerName()
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

        public  void PrintTurn(string i_PreviousMove, string i_CurrentPlayerName)
        {
            if (!i_PreviousMove.Equals(string.Empty))
            {
                Console.WriteLine(i_PreviousMove);
            }

            Console.Write(i_CurrentPlayerName + "'s turn:");
        }

        public string getUserMove(int i_BoardSize)
        {
            string playerInput = Console.ReadLine();

            if (playerInput != "Q")
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

        private  bool isUserInputMoveValid(string i_PlayerMove, int i_BoardSize)
        {
            return i_PlayerMove.Length == 5 && char.IsUpper(i_PlayerMove[0]) && i_PlayerMove[0] < ('A' + i_BoardSize) &&
                    char.IsLower(i_PlayerMove[1]) && i_PlayerMove[0] < ('a' + i_BoardSize) && i_PlayerMove[2] == '>' &&
                    char.IsUpper(i_PlayerMove[3]) && i_PlayerMove[3] < ('A' + i_BoardSize) &&
                    char.IsLower(i_PlayerMove[4]) && i_PlayerMove[4] < ('a' + i_BoardSize);
        }

        public  bool playerWantsAnotherRound()
        {
            string playerChoice;
            bool anotherRound = false;
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("Would you like to play another round? <Y/N>");
            playerChoice = Console.ReadLine();
            while (playerChoice != "Y" && playerChoice != "N")
            {
                Console.WriteLine("Invalid input, try again. . . ");
                playerChoice = Console.ReadLine();
            }

            Console.Write(Environment.NewLine);

            if (playerChoice != "Y")
            {
                anotherRound = true;
            }

            return anotherRound;     
        }

        public static bool printEndGame(Player i_winningPlayer)
        {

            if (i_winningPlayer != null)
            {
                Console.Write(i_winningPlayer.Name + "won the game with a score of: " + i_winningPlayer.Score);
            }
            else
            {
                Console.WriteLine("Its a tie!");
            }


            //bool isEnd = false;
            //if(i_PlayerOne.Score > i_PlayerTwo.Score)
            //{
            //    PrintScore(i_PlayerOne);
            //    isEnd = true;
            //}
            //else if (i_PlayerOne.Score < i_PlayerTwo.Score)
            //{ 
            //    PrintScore(i_PlayerTwo);
            //    isEnd = true;
            //} 
            //else if (!i_PlayerOne.hasAvailableMove && !i_PlayerTwo.hasAvailableMove)
            //{
            //    Console.WriteLine("Its a tie!");
            //    isEnd = true;
            //}

            return isEnd;
        }

        public string getMoveFromStrongPlayer(int i_BoardSize)
        {
            Console.WriteLine("You are not the weak player! Enter a valid move. . .");
            return getUserMove(i_BoardSize);
        }

        public string getAnotherMoveFromCurrentPlayer(int i_BoardSize, string i_PlayerName)
        {
            Console.WriteLine(i_PlayerName + " has another turn");
            return getUserMove(i_BoardSize);
        }

        public string getObligatoryMove(int i_BoardSize)
        {
            Console.WriteLine("Invalid move, you must eliminate your opponnent!");
            return getUserMove(i_BoardSize); 
        }

        public static void printInvalidMsg()
        {
            Console.WriteLine("Invalid move, try again . . .");
        }
    }
}
