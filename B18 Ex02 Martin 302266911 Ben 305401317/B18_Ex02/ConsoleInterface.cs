using System;
using System.Threading;

namespace B18_Ex02
{
    public class ConsoleInterface
    {
        private const string k_AgainstAnothePlayerChar = "1";
        private const string k_AgainstPcChar = "2";
        private const string k_YesChar = "Y";
        private const string k_NoChar = "N";
        private const string k_ComputerNameString = "Computer";
        private const int k_MaxPlayerNameLength = 20; 
        private const int k_MinPlayerNameLength = 1;
        private const int k_MoveLength = 5;

        public void CreateNewGame()
        {
            string firstPlayerName, secondPlayerName;
            int boardSize;
            int opponentChoice;
            CheckersGame newGame = new CheckersGame();

            this.printWelcomeMsg();
            firstPlayerName = this.getPlayerName();
            newGame.AddNewPlayer(firstPlayerName, Square.eSquareType.playerOne); 
            boardSize = this.getBoardSizeFromUser();
            opponentChoice = this.getOpponnetOptions();

            if (opponentChoice == int.Parse(k_AgainstAnothePlayerChar)) 
            {
                secondPlayerName = this.getPlayerName();
                newGame.AddNewPlayer(secondPlayerName, Square.eSquareType.playerTwo);
            }
            else
            {
                secondPlayerName = k_ComputerNameString;
                newGame.AddNewPlayer(string.Empty, Square.eSquareType.playerPC);
            }

            newGame.CreateGameBoard(boardSize);
            this.runGame(newGame, boardSize);
        }

        private void runGame(CheckersGame i_Game, int i_BoardSize)
        {
            string playerChoice = string.Empty;
            Player currentPlayer = i_Game.PlayerOne;
            string previousMove = string.Empty;
            string userMove = string.Empty;

            CheckersGame.eRoundOptions gameStatus;
            CheckersGame.eRoundOptions currentRound = CheckersGame.eRoundOptions.passRound;

            while (currentRound != CheckersGame.eRoundOptions.gameOver)
            {
                this.clearScreen();
                this.printBoard(i_Game.Board);
                this.printTurn(previousMove, currentPlayer);

                if (currentPlayer.PlayerType != Square.eSquareType.playerPC)
                {
                    userMove = this.getUserMove(i_BoardSize);
                }
                else
                {
                    userMove = currentPlayer.ComputerMove(i_Game.Board);
                    Thread.Sleep(1200);
                }

                currentRound = i_Game.NewRound(userMove);
 
                gameStatus = i_Game.CheckGameStatus();

                if (gameStatus != CheckersGame.eRoundOptions.passRound)
                {
                    currentRound = gameStatus;
                }

                this.handleRound(ref previousMove, ref userMove, ref currentRound, currentPlayer, i_Game);
                currentPlayer = i_Game.GetCurrentPlayer();
            }
        }

        private void handleRound(ref string io_PreviousMove, ref string io_UserMove, ref CheckersGame.eRoundOptions io_CurrentRound, Player i_CurrentPlayer, CheckersGame i_Game)
        {
            bool iswinnigPlayer = false;

            if (io_CurrentRound == CheckersGame.eRoundOptions.weakPlayerQuits)
            {
                this.endOfRoundScreen(i_Game, i_CurrentPlayer.PlayerType, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
            }
            else if (io_CurrentRound == CheckersGame.eRoundOptions.strongPlayerWantsToQuit) 
            {
                // another round - Quit
                Console.WriteLine("You are not the weak player! Enter a valid move. . .");
                Thread.Sleep(800);
            }
            else if (io_CurrentRound == CheckersGame.eRoundOptions.playerDidntEnterObligatoryMove) 
            {
                // another round - Wrong move
                Console.WriteLine("Invalid move, you must eliminate your opponnent!");
                Thread.Sleep(800);
            }
            else if (io_CurrentRound == CheckersGame.eRoundOptions.currentPlayerHasAnotherRound) 
            {
                // another round 
                Console.WriteLine("{0} {1} has another turn", Environment.NewLine, i_CurrentPlayer.Name);
                Thread.Sleep(800);
            }
            else if (io_CurrentRound == CheckersGame.eRoundOptions.playerOneWon)
            {
                iswinnigPlayer = true;
                i_Game.endRoundScoreUpdate(Square.eSquareType.playerTwo);
                this.endOfRoundScreen(i_Game, Square.eSquareType.playerOne, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
            }
            else if (io_CurrentRound == CheckersGame.eRoundOptions.playerTwoWon)
            {
                iswinnigPlayer = true;
                i_Game.endRoundScoreUpdate(Square.eSquareType.playerOne);
                this.endOfRoundScreen(i_Game, Square.eSquareType.playerTwo, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
            }
            else if (io_CurrentRound == CheckersGame.eRoundOptions.gameIsATie)
            {
                this.endOfRoundScreen(i_Game, Square.eSquareType.none, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
            }
            else if (io_CurrentRound == CheckersGame.eRoundOptions.playerEnteredInvalidMove)
            {
                Console.WriteLine("Invalid move, try again . . .");
                Thread.Sleep(800);
            }
            else
            {
                string shape = i_Game.Board.SquareToString(i_Game.Board.GetSquareStatus(Move.Parse(io_UserMove).SquareTo));
                io_PreviousMove = i_CurrentPlayer.Name + "'s move was (" + shape + "): " + io_UserMove;
            }
        }

        private void printWelcomeMsg()
        {
            Console.WriteLine("Welcome to the game! {0}", Environment.NewLine);
        }

        private void printBoard(Board i_Board)
        {
            int requiredSpaceForSquare = 4;
            char currentChar = 'A';
            int boardRow = 0;

            Console.Write(" ");

            for (int j = 0; j < i_Board.Size; j++)
            {
                Console.Write("  {0} ", currentChar++);
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
                        Console.Write("|");

                        string shape = i_Board.SquareToString(i_Board.GetSquareStatus(boardRow, j));
                        Console.Write(shape);
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
            int playerChoice = 0;

            Console.WriteLine("Enter the board size: < 6 / 8 / 10 > ");
            int.TryParse(Console.ReadLine().Trim(), out playerChoice);

            while (playerChoice != (int)Board.eBoardSize.Small && playerChoice != (int)Board.eBoardSize.Medium && playerChoice != (int)Board.eBoardSize.Large)
            {
                Console.WriteLine("Invalid board size, please enter the size again...");
                int.TryParse(Console.ReadLine().Trim(), out playerChoice);
            }

            Console.Write(Environment.NewLine);

            return playerChoice;
        }

        private void clearScreen()
        {
            Ex02.ConsoleUtils.Screen.Clear();
        }

        private int getOpponnetOptions()
        {
            Console.WriteLine("Choose your opponent: ");
            Console.WriteLine("1. Another player ");
            Console.WriteLine("2. The PC ");

            string playerChoice = Console.ReadLine();
            playerChoice = playerChoice.Trim();

            while (playerChoice != k_AgainstAnothePlayerChar && playerChoice != k_AgainstPcChar)
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

            while (playerName.Length > k_MaxPlayerNameLength || playerName.Length < k_MinPlayerNameLength)
            {
                Console.WriteLine("Invalid name size, please enter your name again...");
                playerName = Console.ReadLine();
            }
   
            Console.Write(Environment.NewLine);

            return playerName;
        }

        private void printTurn(string i_PreviousMove, Player i_CurrentPlayerName)
        {
            if (!i_PreviousMove.Equals(string.Empty))
            {
                Console.WriteLine(i_PreviousMove);
            }

            Console.Write("{0}'s turn:", i_CurrentPlayerName.Name);
        }

        private string getUserMove(int i_BoardSize)
        {
            string playerInput = Console.ReadLine();
            playerInput = playerInput.Replace(" ", string.Empty);

            if (playerInput.ToUpper() != CheckersGame.k_QuitGameChar)
            {
                while (!this.isUserInputMoveValid(playerInput, i_BoardSize))
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
            return i_PlayerMove.Length == k_MoveLength && char.IsUpper(i_PlayerMove[0]) && i_PlayerMove[0] < ('A' + i_BoardSize) &&
                    char.IsLower(i_PlayerMove[1]) && i_PlayerMove[0] < ('a' + i_BoardSize) && i_PlayerMove[2] == '>' &&
                    char.IsUpper(i_PlayerMove[3]) && i_PlayerMove[3] < ('A' + i_BoardSize) &&
                    char.IsLower(i_PlayerMove[4]) && i_PlayerMove[4] < ('a' + i_BoardSize);
        }

        private bool playerWantsAnotherRound(CheckersGame i_Game, Square.eSquareType i_Player)
        {
            string playerChoice;
            bool anotherRound = false;

            Console.WriteLine("{0}Would you like to play another round? <Y/N>", Environment.NewLine);
            playerChoice = Console.ReadLine().ToUpper();

            while (playerChoice != k_YesChar && playerChoice != k_NoChar)
            {
                Console.WriteLine("Invalid input, try again. . . ");
                playerChoice = Console.ReadLine();
            }

            Console.Write(Environment.NewLine);

            if (playerChoice != k_NoChar)
            {
                anotherRound = true;
            }

            return anotherRound;     
        }

        private void printEndGame(CheckersGame i_Game, Square.eSquareType i_PlayerType, bool i_WinningPlayer)
        {
            if(i_PlayerType != Square.eSquareType.none)
            {
                if (i_WinningPlayer)
                {
                    if (i_PlayerType == Square.eSquareType.playerOne)
                    {
                        Console.Write("{0}{1} won the game!", Environment.NewLine, i_Game.PlayerOne.Name);
                    }
                    else
                    {
                        Console.Write("{0}{1} won the game!", Environment.NewLine, i_Game.PlayerTwo.Name);
                    }
                }
                else
                {
                    if (i_PlayerType == Square.eSquareType.playerOne)
                    {
                        Console.Write("{0}{1} won the game!", Environment.NewLine, i_Game.PlayerTwo.Name);
                    }
                    else
                    {
                        Console.Write("{0}{1} won the game!", Environment.NewLine, i_Game.PlayerOne.Name);
                    }
                }
            }
            else
            {
                Console.WriteLine("Its a tie!");
            }

            Console.Write(Environment.NewLine);
            Console.WriteLine("{0}'s score is: {1}", i_Game.PlayerOne.Name, i_Game.PlayerOne.Score);
            Console.WriteLine("{0}'s score is: {1}", i_Game.PlayerTwo.Name, i_Game.PlayerTwo.Score);
        }

        private string getAnotherMoveFromCurrentPlayer(int i_BoardSize, string i_PlayerName)
        {
            return this.getUserMove(i_BoardSize);
        }

        private void endOfRoundScreen(CheckersGame i_Game, Square.eSquareType i_PlayerType, bool i_WinningPlayer, ref CheckersGame.eRoundOptions io_CurrentRound, ref string io_PreviousMove)
        {
            this.clearScreen();
            this.printEndGame(i_Game, i_PlayerType, i_WinningPlayer);

            if (!this.playerWantsAnotherRound(i_Game, i_PlayerType))
            {
                Console.WriteLine("Goodbye! :)");
                io_CurrentRound = CheckersGame.eRoundOptions.gameOver;
            }
            else 
            {
                // player wants to play another game
                i_Game.CreateGameBoard(i_Game.Board.Size);
                io_PreviousMove = string.Empty;
            }
        }
    }
}
