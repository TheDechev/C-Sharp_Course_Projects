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
        public const string k_DefaultComputerName = "[Computer]";
        public SettingsForm()
        {
            this.DialogResult = DialogResult.No;
            this.InitializeComponent();
            this.textBoxPlayer2.Text = k_DefaultComputerName;

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
                return string.Format("{0}", this.textBoxPlayer1.Text);
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
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            if (this.PlayerOneName == string.Empty)
            {
                this.textBoxPlayer1.Text = "Player1";
            }

            if (this.PlayerTwoName == k_DefaultComputerName)
            {
                this.textBoxPlayer2.Text = "Computer";
            }

            base.OnClosed(e);
        }

        private void textBoxPlayer2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
