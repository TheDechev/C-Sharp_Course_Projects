using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Checkers_Logic;

namespace WindowsUI_Checkers
{
    //class GameForm
    //{
    //    private const string k_AgainstAnothePlayerChar = "1";
    //    private const string k_AgainstPcChar = "2";
    //    private const string k_YesChar = "Y";
    //    private const string k_NoChar = "N";
    //    private const string k_ComputerNameString = "Computer";
    //    private const int k_MaxPlayerNameLength = 20;
    //    private const int k_MinPlayerNameLength = 1;
    //    private const int k_MoveLength = 5;








    //    public void CreateNewGame()
    //    {
    //        string firstPlayerName, secondPlayerName;
    //        int boardSize;
    //        int opponentChoice;
    //        CheckersGame newGame = new CheckersGame();

    //        //this.printWelcomeMsg();
    //        firstPlayerName = this.getPlayerName();
    //        newGame.AddNewPlayer(firstPlayerName, Square.eSquareType.playerOne);
    //        boardSize = this.getBoardSizeFromUser();
    //        opponentChoice = this.getOpponnetOptions();

    //        if (opponentChoice == int.Parse(k_AgainstAnothePlayerChar))
    //        {
    //            secondPlayerName = this.getPlayerName();
    //            newGame.AddNewPlayer(secondPlayerName, Square.eSquareType.playerTwo);
    //        }
    //        else
    //        {
    //            secondPlayerName = k_ComputerNameString;
    //            newGame.AddNewPlayer(string.Empty, Square.eSquareType.playerPC);
    //        }

    //        newGame.CreateGameBoard(boardSize);
    //        this.runGame(newGame, boardSize);
    //    }

    //    private void runGame(CheckersGame i_Game, int i_BoardSize)
    //    {
    //        string playerChoice = string.Empty;
    //        string previousMove = string.Empty;
    //        string userMove = string.Empty;

    //        CheckersGame.eRoundOptions gameStatus;
    //        CheckersGame.eRoundOptions currentRound = CheckersGame.eRoundOptions.passRound;

    //        while (currentRound != CheckersGame.eRoundOptions.gameOver)
    //        {
    //            //this.clearScreen();
    //            //this.printBoard(i_Game.Board);
    //            //this.printTurn(previousMove, i_Game.CurrentPlayer);

    //            if (i_Game.CurrentPlayer.PlayerType != Square.eSquareType.playerPC)
    //            {
    //                userMove = this.getUserMove(i_BoardSize);
    //            }
    //            else
    //            {
    //                userMove = i_Game.CurrentPlayer.ComputerMove(i_Game.Board);
    //                //Thread.Sleep(1200);
    //            }

    //            currentRound = i_Game.NewRound(userMove);
    //            gameStatus = i_Game.CheckGameStatus();

    //            if (gameStatus != CheckersGame.eRoundOptions.passRound)
    //            {
    //                currentRound = gameStatus;
    //            }

    //            this.handleRound(ref previousMove, ref userMove, ref currentRound, i_Game.CurrentPlayer, i_Game);

    //        }
    //    }

    //    private void handleRound(ref string io_PreviousMove, ref string io_UserMove, ref CheckersGame.eRoundOptions io_CurrentRound, Player i_CurrentPlayer, CheckersGame i_Game)
    //    {
    //        bool iswinnigPlayer = false;

    //        if (io_CurrentRound == CheckersGame.eRoundOptions.weakPlayerQuits)
    //        {
    //            this.endOfRoundScreen(i_Game, i_CurrentPlayer.PlayerType, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
    //        }
    //        else if (io_CurrentRound == CheckersGame.eRoundOptions.strongPlayerWantsToQuit)
    //        {
    //            // another round - Quit
    //            Console.WriteLine("You are not the weak player! Enter a valid move. . .");
    //            Thread.Sleep(800);
    //        }
    //        else if (io_CurrentRound == CheckersGame.eRoundOptions.playerDidntEnterObligatoryMove)
    //        {
    //            // another round - Wrong move
    //            Console.WriteLine("Invalid move, you must eliminate your opponnent!");
    //            Thread.Sleep(800);
    //        }
    //        else if (io_CurrentRound == CheckersGame.eRoundOptions.currentPlayerHasAnotherRound)
    //        {
    //            // another round 
    //            Console.WriteLine("{0} {1} has another turn", Environment.NewLine, i_CurrentPlayer.Name);
    //            Thread.Sleep(800);
    //        }
    //        else if (io_CurrentRound == CheckersGame.eRoundOptions.playerOneWon)
    //        {
    //            iswinnigPlayer = true;
    //            i_Game.endRoundScoreUpdate(Square.eSquareType.playerTwo);
    //            this.endOfRoundScreen(i_Game, Square.eSquareType.playerOne, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
    //        }
    //        else if (io_CurrentRound == CheckersGame.eRoundOptions.playerTwoWon)
    //        {
    //            iswinnigPlayer = true;
    //            i_Game.endRoundScoreUpdate(Square.eSquareType.playerOne);
    //            this.endOfRoundScreen(i_Game, Square.eSquareType.playerTwo, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
    //        }
    //        else if (io_CurrentRound == CheckersGame.eRoundOptions.gameIsATie)
    //        {
    //            this.endOfRoundScreen(i_Game, Square.eSquareType.none, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
    //        }
    //        else if (io_CurrentRound == CheckersGame.eRoundOptions.playerEnteredInvalidMove)
    //        {
    //            Console.WriteLine("Invalid move, try again . . .");
    //            Thread.Sleep(800);
    //        }
    //        else
    //        {
    //            string shape = i_Game.Board.SquareToString(i_Game.Board.GetSquareStatus(Move.Parse(io_UserMove).SquareTo));
    //            io_PreviousMove = i_CurrentPlayer.Name + "'s move was (" + shape + "): " + io_UserMove;
    //        }
    //    }

    //    private void printBoard(Board i_Board)
    //    {
    //        int requiredSpaceForSquare = 4;
    //        char currentChar = 'A';
    //        int boardRow = 0;

    //        Console.Write(" ");

    //        for (int j = 0; j < i_Board.Size; j++)
    //        {
    //            Console.Write("  {0} ", currentChar++);
    //        }

    //        Console.WriteLine();
    //        currentChar = 'a';

    //        for (int i = 0; i < (i_Board.Size * 2) + 1; i++)
    //        {
    //            if (i % 2 == 0)
    //            {
    //                for (int j = 0; j <= (i_Board.Size * requiredSpaceForSquare) + 1; j++)
    //                {
    //                    Console.Write("=");
    //                }
    //            }
    //            else
    //            {
    //                Console.Write(currentChar++);

    //                for (int j = 0; j < i_Board.Size; j++)
    //                {
    //                    Console.Write("|");

    //                    string shape = i_Board.SquareToString(i_Board.GetSquareStatus(boardRow, j));
    //                    Console.Write(shape);
    //                }

    //                Console.Write("|");
    //                boardRow++;
    //            }

    //            Console.WriteLine();
    //        }

    //        currentChar = 'a';
    //        Console.WriteLine();
    //    }

    //    private int getBoardSizeFromUser()
    //    {
    //        int playerChoice = 0;

    //        Console.WriteLine("Enter the board size: < 6 / 8 / 10 > ");
    //        int.TryParse(Console.ReadLine().Trim(), out playerChoice);

    //        while (playerChoice != (int)Board.eBoardSize.Small && playerChoice != (int)Board.eBoardSize.Medium && playerChoice != (int)Board.eBoardSize.Large)
    //        {
    //            Console.WriteLine("Invalid board size, please enter the size again...");
    //            int.TryParse(Console.ReadLine().Trim(), out playerChoice);
    //        }

    //        Console.Write(Environment.NewLine);

    //        return playerChoice;
    //    }

    //    private void clearScreen()
    //    {
    //        Ex02.ConsoleUtils.Screen.Clear();
    //    }

    //    private int getOpponnetOptions()
    //    {
    //        Console.WriteLine("Choose your opponent: ");
    //        Console.WriteLine("1. Another player ");
    //        Console.WriteLine("2. The PC ");

    //        string playerChoice = Console.ReadLine();
    //        playerChoice = playerChoice.Trim();

    //        while (playerChoice != k_AgainstAnothePlayerChar && playerChoice != k_AgainstPcChar)
    //        {
    //            playerChoice = Console.ReadLine();
    //        }

    //        Console.WriteLine();

    //        return int.Parse(playerChoice);
    //    }

    //    private string getPlayerName()
    //    {
    //        Console.WriteLine("Please enter your name: ");
    //        string playerName = Console.ReadLine();

    //        while (playerName.Length > k_MaxPlayerNameLength || playerName.Length < k_MinPlayerNameLength)
    //        {
    //            Console.WriteLine("Invalid name size, please enter your name again...");
    //            playerName = Console.ReadLine();
    //        }

    //        Console.Write(Environment.NewLine);

    //        return playerName;
    //    }

    //    private string getUserMove(int i_BoardSize)
    //    {
    //        string playerInput = Console.ReadLine();
    //        playerInput = playerInput.Replace(" ", string.Empty);

    //        if (playerInput.ToUpper() != "Q")
    //        {
    //            while (!this.isUserInputMoveValid(playerInput, i_BoardSize))
    //            {
    //                Console.WriteLine("Invalid input, try again. . .");
    //                playerInput = Console.ReadLine();
    //                playerInput = playerInput.Replace(" ", string.Empty);
    //            }
    //        }

    //        return playerInput;
    //    }

    //    private bool isUserInputMoveValid(string i_PlayerMove, int i_BoardSize)
    //    {
    //        return i_PlayerMove.Length == k_MoveLength && char.IsUpper(i_PlayerMove[0]) && i_PlayerMove[0] < ('A' + i_BoardSize) &&
    //                char.IsLower(i_PlayerMove[1]) && i_PlayerMove[0] < ('a' + i_BoardSize) && i_PlayerMove[2] == '>' &&
    //                char.IsUpper(i_PlayerMove[3]) && i_PlayerMove[3] < ('A' + i_BoardSize) &&
    //                char.IsLower(i_PlayerMove[4]) && i_PlayerMove[4] < ('a' + i_BoardSize);
    //    }

    //    private bool playerWantsAnotherRound(CheckersGame i_Game, Square.eSquareType i_Player)
    //    {
    //        string playerChoice;
    //        bool anotherRound = false;

    //        Console.WriteLine("{0}Would you like to play another round? <Y/N>", Environment.NewLine);
    //        playerChoice = Console.ReadLine().ToUpper();

    //        while (playerChoice != k_YesChar && playerChoice != k_NoChar)
    //        {
    //            Console.WriteLine("Invalid input, try again. . . ");
    //            playerChoice = Console.ReadLine();
    //        }

    //        Console.Write(Environment.NewLine);

    //        if (playerChoice != k_NoChar)
    //        {
    //            anotherRound = true;
    //        }

    //        return anotherRound;
    //    }

    //}

    class GameForm : Form
    {
        List<Button> m_ButtonsPlayerOne = new List<Button>();
        List<Button> m_ButtonsPlayerTwo = new List<Button>();
        Label m_LevelPlayerOneName = new Label();
        Label m_LevelPlayerTwoName = new Label();
        Label m_LevelPlayerOneScore = new Label();
        Label m_LevelPlayerTwoScore = new Label();

        public GameForm()
        {
            this.Size = new Size(6*60, 6*60);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Checkers Game";
            initControls();
            initButtons(6);
        }

        private void initControls()
        {
            m_LevelPlayerOneName.Text = "Player 1:"; // add real name
            m_LevelPlayerOneName.Location = new Point(this.Width / 5, 20);
            m_LevelPlayerOneName.Width = m_LevelPlayerOneName.Text.Length * 6;

            m_LevelPlayerTwoName.Text = "Player 2:";
            m_LevelPlayerTwoName.Width = m_LevelPlayerTwoName.Text.Length * 6;
            m_LevelPlayerTwoName.Location = new Point(this.Width / 3 + m_LevelPlayerOneName.Left + m_LevelPlayerOneName.Width, 20);

            m_LevelPlayerOneScore.Text = "0";
            m_LevelPlayerOneScore.Location = new Point(m_LevelPlayerOneName.Left + m_LevelPlayerOneName.Width, 20);

            m_LevelPlayerTwoScore.Text = "0";
            m_LevelPlayerTwoScore.Location = new Point(m_LevelPlayerTwoName.Left + m_LevelPlayerTwoName.Width, 20);

            this.Controls.AddRange(new Control[] { m_LevelPlayerOneName, m_LevelPlayerTwoName, m_LevelPlayerOneScore, m_LevelPlayerTwoScore});
        }

        private void initButtons(int i_BoardSize)
        {
            i_BoardSize = 6; // TODO: DELETE
            int fromTop = 0;
            const int buttonSize = 40;
            Button currentButton;
            for (int i = 0; i < i_BoardSize * i_BoardSize; i++) 
            {
                currentButton = new Button();
                currentButton.Size = new Size(buttonSize, buttonSize);
                currentButton.Location = new Point((buttonSize/2) + buttonSize * (i % i_BoardSize), this.Top + 60 + fromTop*buttonSize);
                this.Controls.AddRange(new Control[] { currentButton });

                if (i % i_BoardSize == 0 && i != 0)
                {
                    fromTop++;
                }

                if(fromTop % 2 != 0)
                {
                    currentButton.BackColor = Color.Gray;
                    if (i % 2 == 0)
                    {
                        currentButton.BackColor = Color.White;
                    }
                }
                else
                {
                    currentButton.BackColor = Color.White;
                    if (i % 2 == 0)
                    {
                        currentButton.BackColor = Color.Gray;
                    }
                }

                if(i<i_BoardSize * (i_BoardSize / 2))
                {
                    m_ButtonsPlayerOne.Add(currentButton);
                }
                else
                {
                    m_ButtonsPlayerTwo.Add(currentButton);

                }
            }

        }
    }
}

