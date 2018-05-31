using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkers_Logic;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace WindowsUI_Checkers
{
    class GameForm: Form
    {
        private bool m_GameWasCreated = false;
        private const int k_ButtonSize = 50;
        private string m_CurrentMove = string.Empty;
        private CheckersGame.eRoundOptions m_RoundStatus;
        Label labelPlayerOneName = new Label();
        Label labelPlayerTwoName = new Label();
        Label labelPlayerOneScore = new Label();
        Label labelPlayerTwoScore = new Label();
       

        Button buttonCurrentlyClicked;
        SettingsForm formSettings = new SettingsForm();
        CheckersGame m_Game = new CheckersGame();

        public GameForm()
        {
            this.FormClosing += GameForm_FormClosing;
            this.Hide();
        }

        private void GameForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.m_Game.CurrentPlayer != null && this.m_GameWasCreated)
            {
                if(!(this.m_Game.CurrentPlayer.PlayerType == this.m_Game.GetWeakPlayer()) && m_GameWasCreated)
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
                            m_Game.CreateGameBoard(formSettings.BoardSize);
                            e.Cancel = true;
                            break;
                        default:
                            break;
                    }
                }
            } 
        }

        private void initControls(int i_BoardSize, int i_ButtonSize)
        {
            int sideMargin = (this.Width - this.ClientSize.Width) / 2;
            labelPlayerOneName.Text = this.formSettings.PlayerOneName;
            labelPlayerOneName.AutoSize = true;
            labelPlayerOneName.Location = new Point(sideMargin + i_ButtonSize, 20);

            labelPlayerOneScore.Text = "0";
            labelPlayerOneScore.AutoSize = true;
            labelPlayerOneScore.Location = new Point(labelPlayerOneName.Left + labelPlayerOneName.PreferredWidth + 7, labelPlayerOneName.Top);


            labelPlayerTwoScore.Text = "0";
            labelPlayerTwoScore.AutoSize = true;
            labelPlayerTwoScore.Location = new Point(this.ClientSize.Width - sideMargin - i_ButtonSize, 20);


            labelPlayerTwoName.Text = this.formSettings.PlayerTwoName;
            labelPlayerTwoName.AutoSize = true;
            labelPlayerTwoName.Location = new Point(labelPlayerTwoScore.Left - labelPlayerTwoName.PreferredWidth - 7, 20);


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
                    currentButton.BackgroundImage = Resource1.lightBackground;
                    currentButton.BackgroundImageLayout = ImageLayout.Stretch;
                    currentButton.FlatStyle = FlatStyle.Flat;
                    currentButton.Click += new EventHandler(this.button_Click);
                    if (currentSquareType == Square.eSquareType.invalid)
                    {
                        currentButton.BackgroundImage = Resource1.darkBackground;
                        currentButton.Enabled = false;
                    }
                    else if ( currentSquareType == Square.eSquareType.playerOne)
                    {
                        currentButton.Image = Resource1.PlayerOne;
                    }
                    else if(currentSquareType == Square.eSquareType.playerTwo || currentSquareType == Square.eSquareType.playerPC)
                    {
                        currentButton.Image = Resource1.PlayerTwo;
                    }
                    this.Controls.Add(currentButton);
                }
            }
        }

        private void InitGameForm()
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
            this.Size = new Size(boardSize * k_ButtonSize + 35, boardSize * k_ButtonSize + k_ButtonSize * 3);
            initButtons(boardSize, k_ButtonSize);
            initControls(boardSize, k_ButtonSize);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Checkers Game";
            this.labelPlayerOneScore.Text = m_Game.PlayerOne.BonusScore.ToString();
            this.labelPlayerTwoScore.Text = m_Game.PlayerTwo.BonusScore.ToString();
            m_GameWasCreated = true;
            this.Font = new Font(this.Font, FontStyle.Bold);
        }

        protected override void OnLoad(EventArgs e)
        {
            if (ensureSettingsValid())
            {
                base.OnLoad(e);
            }
            else
            {
                this.Opacity = 0;
                this.Close();
            }
        }

        private bool ensureSettingsValid()
        {
            bool isValid = false;
            if (formSettings.ShowDialog() == DialogResult.OK)
            {
                InitGameForm();
                isValid =  true;
            }

            return isValid;
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
                    buttonCurrentlyClicked = clickedButton;
                    clickedButton.BackgroundImage = Resource1.SelectedBackground;
                }
                else
                {
                    this.m_CurrentMove = string.Empty;
                }
            }
            else
            {
                if (!(this.buttonCurrentlyClicked != null && this.m_Game.Board.GetSquareStatus(new Square(buttonCurrentlyClicked.Name.ToString())) == this.m_Game.Board.GetSquareStatus(new Square(clickedButton.Name.ToString()))))
                {
                    playRound();
                }

                buttonCurrentlyClicked.BackgroundImage = Resource1.lightBackground;
                this.m_CurrentMove = string.Empty;
            }
        }

        private void handleRound()
        {
            buttonCurrentlyClicked.BackColor = Color.White;

            if (m_RoundStatus == CheckersGame.eRoundOptions.playerDidntEnterObligatoryMove)
            {
                MessageBox.Show("Invalid move, you must eliminate your opponnent!", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (m_RoundStatus == CheckersGame.eRoundOptions.currentPlayerHasAnotherRound)
            {
                if (!m_Game.CurrentPlayer.IsComputer)
                {
                    string warningMsg = string.Format("{0} has another turn.", m_Game.CurrentPlayer.Name);
                    MessageBox.Show(warningMsg, "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
            }
            else if (m_RoundStatus == CheckersGame.eRoundOptions.gameIsATie)
            {
                MessageBox.Show("It's a tie!", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (m_RoundStatus == CheckersGame.eRoundOptions.playerEnteredInvalidMove)
            {
                MessageBox.Show("Invalid move! Try again.", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }   
        }

        private void OnSquareUpdate(int i_Row, int i_Col, Square.eSquareType i_SquareType)
        {
            if (m_GameWasCreated)
            {

                string ButtonToUpdate = string.Format("{0}{1}", Convert.ToChar(i_Col + (int)'A'), Convert.ToChar(i_Row + (int)'a'));
                Button currentButton = (this.Controls[ButtonToUpdate] as Button);
                currentButton.BackgroundImage = Resource1.lightBackground;

                if (i_SquareType == Square.eSquareType.playerOne)
                {
                    currentButton.Image = Resource1.PlayerOne;
                }
                else if (i_SquareType == Square.eSquareType.playerTwo || i_SquareType == Square.eSquareType.playerPC)
                {
                    currentButton.Image = Resource1.PlayerTwo;
                }
                else if (i_SquareType == Square.eSquareType.playerOneKing)
                {
                    currentButton.Image = Resource1.PlayerOneKing;
                }
                else if (i_SquareType == Square.eSquareType.playerTwoKing)
                {
                    currentButton.Image = Resource1.PlayerTwoKing;
                }
                else
                {
                    currentButton.Image = null;
                }
            }
        }

        private void playRound()
        {
            bool isComputerDone;
            m_RoundStatus = m_Game.NewRound(this.m_CurrentMove);

            do
            {
                handleRound();
                this.labelPlayerOneScore.Text = m_Game.PlayerOne.BonusScore.ToString();
                this.labelPlayerTwoScore.Text = m_Game.PlayerTwo.BonusScore.ToString();
                isComputerDone = true;
                if (m_Game.CurrentPlayer.IsComputer)
                {
                    m_RoundStatus = m_Game.NewRound(m_Game.CurrentPlayer.ComputerMove(m_Game.Board));
                    isComputerDone = false;
                }

            } while (!isComputerDone);
        }
    }
}

