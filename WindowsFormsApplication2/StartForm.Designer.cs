﻿namespace SimonSays
{
    partial class StartForm
    {
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
            if (disposing && (components != null))
            {
                components.Dispose();
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
            this.startGame = new System.Windows.Forms.Button();
            this.restartGame = new System.Windows.Forms.Button();
            this.colorsListBox = new System.Windows.Forms.ListBox();
            this.showFailButton = new System.Windows.Forms.Button();
            this.highestLevelLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startGame
            // 
            this.startGame.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.startGame.ForeColor = System.Drawing.SystemColors.ControlText;
            this.startGame.Location = new System.Drawing.Point(12, 187);
            this.startGame.Name = "startGame";
            this.startGame.Size = new System.Drawing.Size(105, 62);
            this.startGame.TabIndex = 0;
            this.startGame.Text = "Start Game";
            this.startGame.UseVisualStyleBackColor = false;
            this.startGame.Click += new System.EventHandler(this.startGame_Click);
            // 
            // restartGame
            // 
            this.restartGame.Location = new System.Drawing.Point(170, 187);
            this.restartGame.Name = "restartGame";
            this.restartGame.Size = new System.Drawing.Size(102, 62);
            this.restartGame.TabIndex = 1;
            this.restartGame.Text = "Restart Game";
            this.restartGame.UseVisualStyleBackColor = true;
            this.restartGame.Click += new System.EventHandler(this.restartGame_Click);
            // 
            // colorsListBox
            // 
            this.colorsListBox.FormattingEnabled = true;
            this.colorsListBox.Items.AddRange(new object[] {
            "Blue",
            "Green",
            "HotPink",
            "Orange",
            "Pink",
            "Purple",
            "White",
            "Yellow"});
            this.colorsListBox.Location = new System.Drawing.Point(0, 0);
            this.colorsListBox.Name = "colorsListBox";
            this.colorsListBox.Size = new System.Drawing.Size(120, 95);
            this.colorsListBox.Sorted = true;
            this.colorsListBox.TabIndex = 2;
            this.colorsListBox.SelectedIndexChanged += new System.EventHandler(this.colorsListBox_SelectedIndexChanged);
            // 
            // showFailButton
            // 
            this.showFailButton.Location = new System.Drawing.Point(197, 158);
            this.showFailButton.Name = "showFailButton";
            this.showFailButton.Size = new System.Drawing.Size(75, 23);
            this.showFailButton.TabIndex = 3;
            this.showFailButton.Text = "Show Fail";
            this.showFailButton.UseVisualStyleBackColor = true;
            this.showFailButton.Visible = false;
            this.showFailButton.Click += new System.EventHandler(this.showFailButton_Click);
            // 
            // highestLevelLabel
            // 
            this.highestLevelLabel.AutoSize = true;
            this.highestLevelLabel.Location = new System.Drawing.Point(127, 13);
            this.highestLevelLabel.Name = "highestLevelLabel";
            this.highestLevelLabel.Size = new System.Drawing.Size(104, 13);
            this.highestLevelLabel.TabIndex = 4;
            this.highestLevelLabel.Text = "Your highest level is:";
            // 
            // StartForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.highestLevelLabel);
            this.Controls.Add(this.showFailButton);
            this.Controls.Add(this.colorsListBox);
            this.Controls.Add(this.restartGame);
            this.Controls.Add(this.startGame);
            this.Name = "StartForm";
            this.Text = "Simon Says";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button startGame;
        private System.Windows.Forms.Button restartGame;
        private System.Windows.Forms.ListBox colorsListBox;
        private System.Windows.Forms.Button showFailButton;
        private System.Windows.Forms.Label highestLevelLabel;
    }

}

