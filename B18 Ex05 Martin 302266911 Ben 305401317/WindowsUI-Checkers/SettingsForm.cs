using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Checkers_Logic;

namespace WindowsUI_Checkers
{
    public partial class SettingsForm : Form
    {
        private Board.eBoardSize m_BoardSize = Board.eBoardSize.Small;

        public SettingsForm()
        {
            this.DialogResult = DialogResult.No;
            InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPlayer2.Enabled)
            {
                textBoxPlayer2.Enabled = true;
            }
        }

        public int BoardSize
        {
            get { return (int)m_BoardSize; }
        }

        public bool IsComputer
        {
            get { return !checkBoxPlayer2.Enabled; }
        }

        private void radioButtonSmallBoard_CheckedChanged(object sender, EventArgs e)
        {
            m_BoardSize = Board.eBoardSize.Small;
        }

        private void radioButtonMediumBoard_CheckedChanged(object sender, EventArgs e)
        {
            m_BoardSize = Board.eBoardSize.Medium;
        }

        private void radioButtonLargeBoard_CheckedChanged(object sender, EventArgs e)
        {
            m_BoardSize = Board.eBoardSize.Large;
        }

        public string PlayerOneName
        {
            get { return textBoxPlayer1.Text; }
        }
   
        public string PlayerTwoName
        {
            get { return textBoxPlayer2.Text; }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (textBoxPlayer1.Text == string.Empty)
            {
                MessageBox.Show("Please fill all the fields", "Checkers Game", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}
