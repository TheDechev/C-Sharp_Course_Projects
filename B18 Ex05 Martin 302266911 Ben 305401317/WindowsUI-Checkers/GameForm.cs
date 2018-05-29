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
    class GameForm: Form
    {
        private bool m_GameWasCreated = false;
        private const int k_ButtonSize = 40;
        private string m_CurrentMove = string.Empty;
        private CheckersGame.eRoundOptions m_RoundStatus;
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
            labelPlayerOneName.Text = this.formSettings.PlayerOneName;
            labelPlayerOneName.Width = labelPlayerOneName.Text.Length * 6;
            labelPlayerOneName.Location = new Point((this.Width - this.ClientSize.Width) / 2 + i_ButtonSize, 20);


            labelPlayerOneScore.Text = "0";
            labelPlayerOneScore.Width = labelPlayerOneScore.Text.Length * 10;
            labelPlayerOneScore.Location = new Point(labelPlayerOneName.Right, labelPlayerOneName.Top);


            labelPlayerTwoScore.Text = "0";
            labelPlayerTwoScore.Width = labelPlayerTwoScore.Text.Length * 10;
            labelPlayerTwoScore.Location = new Point(this.ClientSize.Width - 20 - i_ButtonSize, 20);

            labelPlayerTwoName.Text = this.formSettings.PlayerTwoName;
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
                smallLetter = Convert.ToChar(i + (int)'a');
                for (int j = 0; j < i_BoardSize; j++)
                {
                    capitalLetter = Convert.ToChar(j + (int)'A');
                    currentButton = new Button();
                    currentButton.Size = new Size(i_ButtonSize, i_ButtonSize);
                    currentButton.AutoSize = true;
                    currentButton.Name = new string(new char[]{capitalLetter,smallLetter});
                    currentButton.Location = new Point( 10 + i_ButtonSize * j , this.Height - this.ClientSize.Height + 35 + i_ButtonSize * i);
                    currentSquareType = m_Game.Board.GetSquareStatus(i, j);
                    currentButton.BackColor = Color.White;
                    currentButton.Click += new EventHandler(this.button_Click);
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

        protected override void OnLoad(EventArgs e)
        {
            if (ensureSettingsValid())
            {
                base.OnLoad(e);
            }
            else
            {
                this.Close();
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
                m_Game.Board.SquareUpdate += OnSquareUpdate;
                initButtons(boardSize, k_ButtonSize);
                initControls(boardSize, k_ButtonSize);
                this.Size = new Size(boardSize * k_ButtonSize + 35, boardSize * k_ButtonSize + k_ButtonSize * 3);
                this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Text = "Checkers Game";
                m_GameWasCreated = true;
                this.Font = new Font(this.Font, FontStyle.Bold);
                isValid =  true;
            }

            return isValid;
        }


        private void OnSquareUpdate(int i_Row, int i_Col, string i_SquareType)
        {
            if (m_GameWasCreated)
            {
                string ButtonToUpdate = string.Format("{0}{1}", Convert.ToChar(i_Col + (int)'A'), Convert.ToChar(i_Row + (int)'a'));
                this.Controls[ButtonToUpdate].BackColor = Color.White;
                this.Controls[ButtonToUpdate].Text = i_SquareType;
            }
        }

       private void button_Click(object obj, EventArgs ev)
        {
            Button clickedButton = obj as Button;
            this.m_CurrentMove += clickedButton.Name;
            if (this.m_CurrentMove.Length == 2)
            {
                this.m_CurrentMove += ">";
                clickedButton.BackColor = Color.CadetBlue;
            }
            else
            {
                m_RoundStatus = m_Game.NewRound(this.m_CurrentMove);
                handleRound();
                this.m_CurrentMove = string.Empty;
            }
        }

        private void handleRound()
        {

            if (m_RoundStatus == CheckersGame.eRoundOptions.weakPlayerQuits)
            {
                //this.endOfRoundScreen(i_Game, i_CurrentPlayer.PlayerType, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
            }
            else if (m_RoundStatus == CheckersGame.eRoundOptions.strongPlayerWantsToQuit)
            {
                // another round - Quit
                MessageBox.Show("You are not the weak player! Enter a valid move.", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (m_RoundStatus == CheckersGame.eRoundOptions.playerDidntEnterObligatoryMove)
            {
                // another round - Wrong move
                MessageBox.Show("Invalid move, you must eliminate your opponnent!", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else if (m_RoundStatus == CheckersGame.eRoundOptions.currentPlayerHasAnotherRound)
            {
                string warningMsg = string.Format("{0} has another turn.", m_Game.CurrentPlayer.Name);
                // another round 
                MessageBox.Show(warningMsg, "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (m_RoundStatus == CheckersGame.eRoundOptions.playerOneWon)
            {
                string gameOverMsg = string.Format("{0} won the game! {0} Another round?.", m_Game.PlayerOne.Name,Environment.NewLine);
                DialogResult continueGame;

                continueGame  = MessageBox.Show(gameOverMsg, "Checkers Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (continueGame == DialogResult.Yes)
                {
                    m_Game.CreateGameBoard(formSettings.BoardSize);
                }
                else if (continueGame == DialogResult.No)
                {
                    this.Close();
                }
                //i_Game.endRoundScoreUpdate(Square.eSquareType.playerTwo);
                //this.endOfRoundScreen(i_Game, Square.eSquareType.playerOne, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
            }
            else if (m_RoundStatus == CheckersGame.eRoundOptions.playerTwoWon)
            {
                string gameOverMsg = string.Format("{0} won the game! {0} Another round?.", m_Game.PlayerTwo.Name, Environment.NewLine);
                DialogResult continueGame;

                continueGame = MessageBox.Show(gameOverMsg, "Checkers Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (continueGame == DialogResult.Yes)
                {
                    m_Game.CreateGameBoard(formSettings.BoardSize);
                }
                else if (continueGame == DialogResult.No)
                {
                    this.Close();
                }

                //i_Game.endRoundScoreUpdate(Square.eSquareType.playerOne);
                //this.endOfRoundScreen(i_Game, Square.eSquareType.playerTwo, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
            }
            else if (m_RoundStatus == CheckersGame.eRoundOptions.gameIsATie)
            {
                //this.endOfRoundScreen(i_Game, Square.eSquareType.none, iswinnigPlayer, ref io_CurrentRound, ref io_PreviousMove);
            }
            else if (m_RoundStatus == CheckersGame.eRoundOptions.playerEnteredInvalidMove)
            {
                MessageBox.Show("Invalid move! Try again.", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

