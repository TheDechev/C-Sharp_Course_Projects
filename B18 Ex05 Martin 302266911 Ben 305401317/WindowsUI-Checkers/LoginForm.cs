using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
namespace WindowsUI_Checkers
{
    class LoginForm : Form
    {
        Label m_LabelBoardSize = new Label();
        RadioButton m_RadioButtonSmallSize = new RadioButton();
        RadioButton m_RadioButtonMediumSize = new RadioButton();
        RadioButton m_RadioButtonLargeSize = new RadioButton();

        TextBox m_TextboxPlayerOne = new TextBox();
        TextBox m_TextboxPlayerTwo = new TextBox();
        CheckBox m_CheckBoxPlayerTwo = new CheckBox();
        Label m_LabelPlayers = new Label();
        Label m_LabelPlayerOne = new Label();

        Label m_LabelGameSize = new Label();

        Button m_ButtonDone = new Button();

        

        public LoginForm()
        {
            this.Size = new Size(250, 200);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            initControls();

            m_CheckBoxPlayerTwo.Click += new EventHandler(m_CheckBoxPlayerTwo_Click);
        }

        private void  initControls()
        {
            
            // Board size lable
            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.Location = new Point(10, 10);
            m_LabelBoardSize.Height = 15;

            // Players label
            m_LabelPlayers.Text = "Players:";
            m_LabelPlayers.Location = new Point(m_LabelBoardSize.Left, m_LabelBoardSize.Top * 5);
            m_LabelPlayers.Width = 50;
            m_LabelPlayers.Height = 15;

            // Board size radio buttons
            m_RadioButtonSmallSize.Location = new Point(m_LabelBoardSize.Left + 10 , (m_LabelPlayers.Top - m_LabelBoardSize.Top)/2);
            m_RadioButtonSmallSize.Height = 30;
            m_RadioButtonSmallSize.Width = 50;
            m_RadioButtonSmallSize.Text = "6 x 6";

            m_RadioButtonMediumSize.Location = new Point(m_RadioButtonSmallSize.Left * 4, (m_LabelPlayers.Top - m_LabelBoardSize.Top) / 2);
            m_RadioButtonMediumSize.Height = 30;
            m_RadioButtonMediumSize.Width = 50;
            m_RadioButtonMediumSize.Text = "8 x 8";

            m_RadioButtonLargeSize.Location = new Point(m_RadioButtonSmallSize.Left * 7, (m_LabelPlayers.Top - m_LabelBoardSize.Top) / 2);
            m_RadioButtonLargeSize.Height = 30;
            m_RadioButtonLargeSize.Width = 60;
            m_RadioButtonLargeSize.Text = "10 x 10";

            // Player 1 - label + text
            m_LabelPlayerOne.Text = "Player 1:";
            m_LabelPlayerOne.Location = new Point(m_RadioButtonSmallSize.Left, m_LabelBoardSize.Top * 7);
            m_LabelPlayerOne.Width = 75;
            m_LabelPlayerOne.Height = 15;

            m_TextboxPlayerOne.Location = new Point(m_LabelPlayerOne.Right + 8, m_LabelPlayerOne.Top - 2);

            // Player 2 - CheckBox + Label + Text
            m_CheckBoxPlayerTwo.Location = new Point(m_LabelPlayerOne.Left, m_LabelBoardSize.Top * 9);
            m_CheckBoxPlayerTwo.Text = "Player 2:";

            m_TextboxPlayerTwo.Text = "[Computer]";
            m_TextboxPlayerTwo.Enabled = false;
            m_TextboxPlayerTwo.Location = new Point(m_TextboxPlayerOne.Left , m_CheckBoxPlayerTwo.Top + 2);

            // Done button
            m_ButtonDone.Location = new Point(m_RadioButtonLargeSize.Left  , m_RadioButtonLargeSize.Top * 6);
            m_ButtonDone.Width = 63;
            m_ButtonDone.Text = "Done";
            
            this.Controls.AddRange(new Control[] { m_LabelPlayerOne, m_LabelBoardSize, m_TextboxPlayerOne, m_TextboxPlayerTwo, m_CheckBoxPlayerTwo
                                    , m_RadioButtonSmallSize, m_RadioButtonMediumSize, m_RadioButtonLargeSize, m_LabelPlayers, m_ButtonDone});
        }

        private void m_CheckBoxPlayerTwo_Click(object sender, EventArgs e)
        {
            if (m_CheckBoxPlayerTwo.Enabled)
            {
                m_TextboxPlayerTwo.Enabled = true;
            }
        }

    }
}
