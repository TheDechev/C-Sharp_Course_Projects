using System.Windows.Forms;

namespace WindowsUI_Checkers
{
    public partial class SettingsForm
    {
        private Label labelPlayers;
        private RadioButton radioButtonSmallBoard;
        private RadioButton radioButtonMediumBoard;
        private RadioButton radioButtonLargeBoard;
        private Label labelPlayer1;
        private CheckBox checkBoxPlayer2;
        private TextBox textBoxPlayer1;
        private TextBox textBoxPlayer2;
        private Button buttonDone;
        private Label labelBoardSize;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.radioButtonSmallBoard = new System.Windows.Forms.RadioButton();
            this.radioButtonMediumBoard = new System.Windows.Forms.RadioButton();
            this.radioButtonLargeBoard = new System.Windows.Forms.RadioButton();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.buttonDone = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBoardSize.Location = new System.Drawing.Point(12, 9);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(72, 13);
            this.labelBoardSize.TabIndex = 0;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayers.Location = new System.Drawing.Point(12, 61);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(52, 13);
            this.labelPlayers.TabIndex = 1;
            this.labelPlayers.Text = "Players:";
            // 
            // radioButtonSmallBoard
            // 
            this.radioButtonSmallBoard.AutoSize = true;
            this.radioButtonSmallBoard.Location = new System.Drawing.Point(34, 32);
            this.radioButtonSmallBoard.Name = "radioButtonSmallBoard";
            this.radioButtonSmallBoard.Size = new System.Drawing.Size(48, 17);
            this.radioButtonSmallBoard.TabIndex = 2;
            this.radioButtonSmallBoard.TabStop = true;
            this.radioButtonSmallBoard.Text = "6 x 6";
            this.radioButtonSmallBoard.UseVisualStyleBackColor = true;
            this.radioButtonSmallBoard.CheckedChanged += new System.EventHandler(this.radioButtonSmallBoard_CheckedChanged);
            // 
            // radioButtonMediumBoard
            // 
            this.radioButtonMediumBoard.AutoSize = true;
            this.radioButtonMediumBoard.Location = new System.Drawing.Point(88, 32);
            this.radioButtonMediumBoard.Name = "radioButtonMediumBoard";
            this.radioButtonMediumBoard.Size = new System.Drawing.Size(48, 17);
            this.radioButtonMediumBoard.TabIndex = 3;
            this.radioButtonMediumBoard.TabStop = true;
            this.radioButtonMediumBoard.Text = "8 x 8";
            this.radioButtonMediumBoard.UseVisualStyleBackColor = true;
            this.radioButtonMediumBoard.CheckedChanged += new System.EventHandler(this.radioButtonMediumBoard_CheckedChanged);
            // 
            // radioButtonLargeBoard
            // 
            this.radioButtonLargeBoard.AutoSize = true;
            this.radioButtonLargeBoard.Location = new System.Drawing.Point(151, 32);
            this.radioButtonLargeBoard.Name = "radioButtonLargeBoard";
            this.radioButtonLargeBoard.Size = new System.Drawing.Size(60, 17);
            this.radioButtonLargeBoard.TabIndex = 4;
            this.radioButtonLargeBoard.TabStop = true;
            this.radioButtonLargeBoard.Text = "10 x 10";
            this.radioButtonLargeBoard.UseVisualStyleBackColor = true;
            this.radioButtonLargeBoard.CheckedChanged += new System.EventHandler(this.radioButtonLargeBoard_CheckedChanged);
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlayer1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.labelPlayer1.Location = new System.Drawing.Point(34, 85);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(48, 13);
            this.labelPlayer1.TabIndex = 5;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPlayer2.Location = new System.Drawing.Point(37, 111);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(67, 17);
            this.checkBoxPlayer2.TabIndex = 6;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Location = new System.Drawing.Point(119, 82);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(124, 20);
            this.textBoxPlayer1.TabIndex = 7;
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.Location = new System.Drawing.Point(119, 109);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(124, 20);
            this.textBoxPlayer2.TabIndex = 8;
            this.textBoxPlayer2.TextChanged += new System.EventHandler(this.textBoxPlayer2_TextChanged);
            // 
            // buttonDone
            // 
            this.buttonDone.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDone.Location = new System.Drawing.Point(168, 146);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(75, 23);
            this.buttonDone.TabIndex = 9;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // SettingsForm
            // 
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(257, 179);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.radioButtonLargeBoard);
            this.Controls.Add(this.radioButtonMediumBoard);
            this.Controls.Add(this.radioButtonSmallBoard);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.labelBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}