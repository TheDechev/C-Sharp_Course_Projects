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
            this.InitializeComponent();
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxPlayer2.Enabled)
            {
                this.textBoxPlayer2.Enabled = true;
            }
        }

        public int BoardSize
        {
            get
            {
                return (int)this.m_BoardSize;
            }
        }

        public bool IsComputer
        {
            get
            {
                return !this.checkBoxPlayer2.Checked;
            }
        }

        private void radioButtonSmallBoard_CheckedChanged(object sender, EventArgs e)
        {
            this.m_BoardSize = Board.eBoardSize.Small;
        }

        private void radioButtonMediumBoard_CheckedChanged(object sender, EventArgs e)
        {
            this.m_BoardSize = Board.eBoardSize.Medium;
        }

        private void radioButtonLargeBoard_CheckedChanged(object sender, EventArgs e)
        {
            this.m_BoardSize = Board.eBoardSize.Large;
        }

        public string PlayerOneName
        {
            get
            {
                return string.Format("{0}:", this.textBoxPlayer1.Text);
            }
        }
   
        public string PlayerTwoName
        {
            get
            {
               return this.textBoxPlayer2.Text;
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            if (this.textBoxPlayer1.Text.Length > 10)
            {
                this.textBoxPlayer1.Text = this.textBoxPlayer1.Text.Substring(0, 9);
            }

            if (this.textBoxPlayer2.Text.Length > 10)
            {
                this.textBoxPlayer2.Text = this.textBoxPlayer2.Text.Substring(0, 9);
            }

            if (this.textBoxPlayer1.Text == string.Empty || this.textBoxPlayer2.Text == string.Empty)
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
