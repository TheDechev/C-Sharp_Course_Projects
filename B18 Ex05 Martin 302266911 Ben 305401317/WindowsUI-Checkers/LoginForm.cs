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
        TextBox m_TextboxPlayerOne = new TextBox();
        TextBox m_TextboxPlayerTwo = new TextBox();
        Label m_LabelPlayerOne = new Label();
        Label m_LabelPlayerTwo = new Label();
        Label m_LabelBoardSize = new Label();
        Label m_LabelGameSize = new Label();
        Label m_LabelPlayers = new Label();

        public LoginForm()
        {
            this.Size = new Size(250, 200);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            initControls();
        }

        private void  initControls()
        {
            m_LabelPlayerOne.Text = "Player 1:";
            m_LabelPlayerOne.Location = new Point(10,50);
            m_LabelPlayerOne.Width = 50;

            m_LabelPlayerTwo.Text = "Player 2:";
            m_LabelPlayerTwo.Location = new Point(10, 90);
            m_LabelPlayerTwo.Width = 50;

            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.Location = new Point(10,10);
    
            m_TextboxPlayerOne.Location = new Point(m_LabelPlayerOne.Right + 8,
                50);

            m_TextboxPlayerTwo.Text = "[Computer]";
            m_TextboxPlayerTwo.Location = new Point(m_LabelPlayerTwo.Right + 8,
                90);

            this.Controls.AddRange(new Control[] { m_LabelPlayerOne, m_LabelPlayerTwo, m_LabelBoardSize, m_TextboxPlayerOne, m_TextboxPlayerTwo });
        }
    }
}
