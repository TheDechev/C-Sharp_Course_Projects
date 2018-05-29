using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers_Logic;
using System.Windows.Forms;
using System.Drawing;
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
    class GameForm: Form
    {
        private const int k_ButtonSize = 40;
        Label labelPlayerOneName = new Label();
        Label labelPlayerTwoName = new Label();
        Label labelPlayerOneScore = new Label();
        Label labelPlayerTwoScore = new Label();
        CheckersGame m_Game = new CheckersGame();
        SettingsForm formSettings = new SettingsForm();
        public GameForm()
        {
            this.Hide();
        }

        private void initControls(int i_BoardSize, int i_ButtonSize)
        {
            labelPlayerOneName.Text = "Martin:"; // TODO: add real name
            labelPlayerOneName.Width = labelPlayerOneName.Text.Length * 6;
            labelPlayerOneName.Location = new Point((this.Width - this.ClientSize.Width) / 2 + i_ButtonSize, 20);


            labelPlayerOneScore.Text = "0";
            labelPlayerOneScore.Width = labelPlayerOneScore.Text.Length * 10;
            labelPlayerOneScore.Location = new Point(labelPlayerOneName.Right, labelPlayerOneName.Top);


            labelPlayerTwoScore.Text = "0";
            labelPlayerTwoScore.Width = labelPlayerTwoScore.Text.Length * 10;
            labelPlayerTwoScore.Location = new Point(this.ClientSize.Width - 20 - i_ButtonSize, 20);

            labelPlayerTwoName.Text = "Ben:";
            labelPlayerTwoName.Width = labelPlayerTwoName.Text.Length * 7;
            labelPlayerTwoName.Location = new Point(labelPlayerTwoScore.Left - labelPlayerTwoName.Width, 20);


            this.Controls.AddRange(new Control[] { labelPlayerOneName, labelPlayerTwoName, labelPlayerOneScore, labelPlayerTwoScore});
        }

        private void initButtons(int i_BoardSize, int i_ButtonSize)
        {
            Square.eSquareType currentSquareType;
            char smallLetter, capitalLetter;

            Button currentButton;
            for (int i = 0; i < i_BoardSize ; i++) 
            {
                capitalLetter = Convert.ToChar(i + (int)'A');
                for (int j = 0; j < i_BoardSize; j++)
                {
                    smallLetter = Convert.ToChar(j + (int)'a');
                    currentButton = new Button();
                    currentButton.Size = new Size(i_ButtonSize, i_ButtonSize);
                    currentButton.AutoSize = true;
                    currentButton.Name = new string(new char[]{capitalLetter,smallLetter});
                    currentButton.Location = new Point( 10 + i_ButtonSize * j , this.Height - this.ClientSize.Height + 35 + i_ButtonSize * i);
                    currentSquareType = m_Game.Board.GetSquareStatus(i, j);
                    currentButton.BackColor = Color.White;
                    if (currentSquareType == Square.eSquareType.invalid)
                    {
                        currentButton.BackColor = Color.Gray;
                        currentButton.Enabled = false;
                    }
                    string shapeText = m_Game.Board.SquareToString(currentSquareType);
                    currentButton.Text = shapeText;
                    this.Controls.Add(currentButton);
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            if (ensureSettingsValid())
            {
                base.OnShown(e);
            }
        }

        private bool ensureSettingsValid()
        {
            bool isValid = false;
            if (formSettings.ShowDialog() == DialogResult.OK)
            {
                int boardSize = formSettings.BoardSize;
                m_Game.AddNewPlayer(formSettings.PlayerOneName, Square.eSquareType.playerOne);
                if (formSettings.IsComputer)
                {
                    m_Game.AddNewPlayer(formSettings.PlayerTwoName, Square.eSquareType.playerPC);
                }
                else
                {
                    m_Game.AddNewPlayer(formSettings.PlayerTwoName, Square.eSquareType.playerTwo);
                }
                m_Game.CreateGameBoard(boardSize);
                this.Size = new Size(boardSize * k_ButtonSize + 35, boardSize * k_ButtonSize + k_ButtonSize * 3);
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Text = "Checkers Game";
                initButtons(boardSize, k_ButtonSize);
                initControls(boardSize, k_ButtonSize);
                this.Font = new Font(this.Font, FontStyle.Bold);
                isValid =  true;
                this.Show();
            }

            return isValid;

        }
    }
}

