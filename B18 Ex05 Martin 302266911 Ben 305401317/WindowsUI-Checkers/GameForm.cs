using System.Windows.Forms;
using System.Drawing;
using System;
using Checkers_Logic;

namespace WindowsUI_Checkers
{
    public class GameForm : Form
    {
        private const int k_ButtonSize = 50;
        private bool m_GameWasCreated = false;
        private string m_CurrentMove = string.Empty;
        private CheckersGame.eRoundOptions m_RoundStatus;
        private Label labelPlayerOneName = new Label();
        private Label labelPlayerTwoName = new Label();
        private Label labelPlayerOneScore = new Label();
        private Label labelPlayerTwoScore = new Label();
        private Button buttonCurrentlyClicked;
        private SettingsForm formSettings = new SettingsForm();
        private CheckersGame m_Game = new CheckersGame();

        public GameForm()
        {
            this.FormClosing += this.GameForm_FormClosing;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.formSettings.ShowDialog();
            this.InitGameForm();
            base.OnLoad(e);
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.m_Game.CurrentPlayer != null && this.m_GameWasCreated)
            {
                if(!(this.m_Game.CurrentPlayer.PlayerType == this.m_Game.GetWeakPlayer()) && this.m_GameWasCreated)
                {
                    switch (MessageBox.Show("You are not the weak player, are you sure you want to quit?", "Checkers Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        case DialogResult.No:
                            e.Cancel = true;

                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    switch (MessageBox.Show("Game over. Would you like to play another round?", "Checkers Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        case DialogResult.Yes:
                            this.m_Game.CreateGameBoard(this.formSettings.BoardSize);
                            e.Cancel = true;
                            break;
                        default:
                            break;
                    }
                }
            } 
        }

        private void InitGameForm()
        {
            int boardSize = this.formSettings.BoardSize;
            this.m_Game.AddNewPlayer(this.formSettings.PlayerOneName, Square.eSquareType.playerOne);

            if (this.formSettings.IsComputer)
            {
                this.m_Game.AddNewPlayer(this.formSettings.PlayerTwoName, Square.eSquareType.playerPC);
            }
            else
            {
                this.m_Game.AddNewPlayer(this.formSettings.PlayerTwoName, Square.eSquareType.playerTwo);
            }

            this.m_Game.CreateGameBoard(boardSize);
            this.m_Game.Board.SquareUpdate += this.OnSquareUpdate;
            this.Size = new Size((boardSize * k_ButtonSize) + 35, (boardSize * k_ButtonSize) + (k_ButtonSize * 3));
            this.initButtons(boardSize, k_ButtonSize);
            this.initControls(boardSize, k_ButtonSize);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.BurlyWood;
            this.Text = "Checkers Game";
            this.labelPlayerOneScore.Text = this.m_Game.PlayerOne.BonusScore.ToString();
            this.labelPlayerTwoScore.Text = this.m_Game.PlayerTwo.BonusScore.ToString();
            this.m_GameWasCreated = true;
            this.Font = new Font(this.Font, FontStyle.Bold);
        }

        private void initControls(int i_BoardSize, int i_ButtonSize)
        {
            int sideMargin = (this.Width - this.ClientSize.Width) / 2;
            this.labelPlayerOneName.Text = this.formSettings.PlayerOneName;
            this.labelPlayerOneName.AutoSize = true;
            this.labelPlayerOneName.Location = new Point(sideMargin + i_ButtonSize, 20);

            this.labelPlayerOneScore.Text = "0";
            this.labelPlayerOneScore.AutoSize = true;
            this.labelPlayerOneScore.Location = new Point(this.labelPlayerOneName.Left + this.labelPlayerOneName.PreferredWidth + 7, this.labelPlayerOneName.Top);

            this.labelPlayerTwoScore.Text = "0";
            this.labelPlayerTwoScore.AutoSize = true;
            this.labelPlayerTwoScore.Location = new Point(this.ClientSize.Width - sideMargin - i_ButtonSize, 20);

            this.labelPlayerTwoName.Text = this.formSettings.PlayerTwoName;
            this.labelPlayerTwoName.AutoSize = true;
            this.labelPlayerTwoName.Location = new Point(this.labelPlayerTwoScore.Left - this.labelPlayerTwoName.PreferredWidth - 7, 20);

            this.Controls.AddRange(new Control[] { this.labelPlayerOneName, this.labelPlayerTwoName, this.labelPlayerOneScore, this.labelPlayerTwoScore });
        }

        private void initButtons(int i_BoardSize, int i_ButtonSize)
        {
            Square.eSquareType currentSquareType;
            char smallLetter, capitalLetter;

            Button currentButton;
            for (int i = 0; i < i_BoardSize; i++) 
            {
                smallLetter = Convert.ToChar(i + (int)'a');
                for (int j = 0; j < i_BoardSize; j++)
                {
                    capitalLetter = Convert.ToChar(j + (int)'A');
                    currentButton = new Button();
                    currentButton.Size = new Size(i_ButtonSize, i_ButtonSize);
                    currentButton.AutoSize = true;
                    currentButton.Name = new string(new char[] { capitalLetter, smallLetter });

                    currentButton.Location = new Point(10 + (i_ButtonSize * j), ((this.Height - this.ClientSize.Height) + 35) + (i_ButtonSize * i));
                    currentSquareType = this.m_Game.Board.GetSquareStatus(i, j);
                    currentButton.BackgroundImage = Resource.lightBackground;
                    currentButton.BackgroundImageLayout = ImageLayout.Stretch;
                    currentButton.FlatStyle = FlatStyle.Flat;
                    currentButton.Click += new EventHandler(this.button_Click);
                    if (currentSquareType == Square.eSquareType.invalid)
                    {
                        currentButton.BackgroundImage = Resource.darkBackground;
                        currentButton.Enabled = false;
                    }
                    else if (currentSquareType == Square.eSquareType.playerOne)
                    {
                        currentButton.Image = Resource.PlayerOne;
                    }
                    else if(currentSquareType == Square.eSquareType.playerTwo || currentSquareType == Square.eSquareType.playerPC)
                    {
                        currentButton.Image = Resource.PlayerTwo;
                    }

                    this.Controls.Add(currentButton);
                }
            }
        }

        private void button_Click(object obj, EventArgs ev)
        {
            Button clickedButton = obj as Button;
            this.m_CurrentMove += clickedButton.Name;
            if (this.m_CurrentMove.Length == 2)
            {
                if (clickedButton.Enabled == true && (this.m_Game.Board.GetSquareStatus(new Square(clickedButton.Name.ToString())) == this.m_Game.CurrentPlayer.PlayerType ||
                     this.m_Game.CurrentPlayer.IsMyKing(this.m_Game.Board.GetSquareStatus(new Square(clickedButton.Name.ToString())))))
                {
                    this.m_CurrentMove += ">";
                    this.buttonCurrentlyClicked = clickedButton;
                    clickedButton.BackgroundImage = Resource.SelectedBackground;
                }
                else
                {
                    this.m_CurrentMove = string.Empty;
                }
            }
            else
            {
                if (!(this.buttonCurrentlyClicked != null && this.m_Game.Board.GetSquareStatus(new Square(this.buttonCurrentlyClicked.Name.ToString())) == this.m_Game.Board.GetSquareStatus(new Square(clickedButton.Name.ToString()))))
                {
                    this.playRound();
                }

                this.buttonCurrentlyClicked.BackgroundImage = Resource.lightBackground;
                this.m_CurrentMove = string.Empty;
            }
        }

        private void handleRound()
        {
            this.buttonCurrentlyClicked.BackColor = Color.White;

            if (this.m_RoundStatus == CheckersGame.eRoundOptions.playerDidntEnterObligatoryMove)
            {
                MessageBox.Show("Invalid move, you must eliminate your opponnent!", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (this.m_RoundStatus == CheckersGame.eRoundOptions.currentPlayerHasAnotherRound)
            {
                if (!this.m_Game.CurrentPlayer.IsComputer)
                {
                    string warningMsg = string.Format("{0} has another turn.", this.m_Game.CurrentPlayer.Name);
                    MessageBox.Show(warningMsg, "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (this.m_RoundStatus == CheckersGame.eRoundOptions.playerOneWon || this.m_RoundStatus == CheckersGame.eRoundOptions.playerTwoWon)
            {
                string gameOverMsg = string.Format("{0} won the game! {1} Another round?.", this.m_Game.CurrentPlayer.Name, Environment.NewLine);
                DialogResult continueGame;
                continueGame  = MessageBox.Show(gameOverMsg, "Checkers Game", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (continueGame == DialogResult.Yes)
                {
                    this.m_Game.CreateGameBoard(this.formSettings.BoardSize);
                }
                else if (continueGame == DialogResult.No)
                {
                    this.m_GameWasCreated = false;
                    this.Close();
                }
            }
            else if (this.m_RoundStatus == CheckersGame.eRoundOptions.gameIsATie)
            {
                MessageBox.Show("It's a tie!", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.m_Game.CreateGameBoard(this.formSettings.BoardSize);
            }
            else if (this.m_RoundStatus == CheckersGame.eRoundOptions.playerEnteredInvalidMove)
            {
                MessageBox.Show("Invalid move! Try again.", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }   
        }

        private void OnSquareUpdate(int i_Row, int i_Col, Square.eSquareType i_SquareType)
        {
            if (this.m_GameWasCreated)
            {
                string ButtonToUpdate = string.Format("{0}{1}", Convert.ToChar(i_Col + (int)'A'), Convert.ToChar(i_Row + (int)'a'));
                Button currentButton = this.Controls[ButtonToUpdate] as Button;

                if (i_SquareType == Square.eSquareType.invalid)
                {
                    currentButton.BackgroundImage = Resource.darkBackground;
                }
                else
                {
                    currentButton.BackgroundImage = Resource.lightBackground;
                }

                if (i_SquareType == Square.eSquareType.playerOne)
                {
                    currentButton.Image = Resource.PlayerOne;
                }
                else if (i_SquareType == Square.eSquareType.playerTwo || i_SquareType == Square.eSquareType.playerPC)
                {
                    currentButton.Image = Resource.PlayerTwo;
                }
                else if (i_SquareType == Square.eSquareType.playerOneKing)
                {
                    currentButton.Image = Resource.PlayerOneKing;
                }
                else if (i_SquareType == Square.eSquareType.playerTwoKing)
                {
                    currentButton.Image = Resource.PlayerTwoKing;
                }
                else if (i_SquareType == Square.eSquareType.none)
                {
                    currentButton.Image = null;
                }
            }
        }

        private void playRound()
        {
            bool isComputerDone;
            this.m_RoundStatus = this.m_Game.NewRound(this.m_CurrentMove);

            do
            {
                this.handleRound();
                this.labelPlayerOneScore.Text = this.m_Game.PlayerOne.BonusScore.ToString();
                this.labelPlayerTwoScore.Text = this.m_Game.PlayerTwo.BonusScore.ToString();
                isComputerDone = true;

                if (this.m_Game.CurrentPlayer.IsComputer && m_GameWasCreated == true)
                {
                    this.m_RoundStatus = this.m_Game.NewRound(this.m_Game.CurrentPlayer.ComputerMove(this.m_Game.Board));
                    isComputerDone = false;
                }
            }
            while (!isComputerDone);
        }
    }
}
