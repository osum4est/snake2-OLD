namespace Snake2
{
    partial class frmOptions
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
            this.rb1Player = new System.Windows.Forms.RadioButton();
            this.rb2Player = new System.Windows.Forms.RadioButton();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rb3Player = new System.Windows.Forms.RadioButton();
            this.rb4Player = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rb1Player
            // 
            this.rb1Player.AutoSize = true;
            this.rb1Player.Location = new System.Drawing.Point(12, 12);
            this.rb1Player.Name = "rb1Player";
            this.rb1Player.Size = new System.Drawing.Size(63, 17);
            this.rb1Player.TabIndex = 0;
            this.rb1Player.TabStop = true;
            this.rb1Player.Text = "1 Player";
            this.rb1Player.UseVisualStyleBackColor = true;
            // 
            // rb2Player
            // 
            this.rb2Player.AutoSize = true;
            this.rb2Player.Location = new System.Drawing.Point(81, 12);
            this.rb2Player.Name = "rb2Player";
            this.rb2Player.Size = new System.Drawing.Size(63, 17);
            this.rb2Player.TabIndex = 1;
            this.rb2Player.TabStop = true;
            this.rb2Player.Text = "2 Player";
            this.rb2Player.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 227);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(132, 33);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(150, 227);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(129, 33);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rb3Player
            // 
            this.rb3Player.AutoSize = true;
            this.rb3Player.Location = new System.Drawing.Point(150, 12);
            this.rb3Player.Name = "rb3Player";
            this.rb3Player.Size = new System.Drawing.Size(63, 17);
            this.rb3Player.TabIndex = 4;
            this.rb3Player.TabStop = true;
            this.rb3Player.Text = "3 Player";
            this.rb3Player.UseVisualStyleBackColor = true;
            // 
            // rb4Player
            // 
            this.rb4Player.AutoSize = true;
            this.rb4Player.Location = new System.Drawing.Point(219, 12);
            this.rb4Player.Name = "rb4Player";
            this.rb4Player.Size = new System.Drawing.Size(63, 17);
            this.rb4Player.TabIndex = 5;
            this.rb4Player.TabStop = true;
            this.rb4Player.Text = "4 Player";
            this.rb4Player.UseVisualStyleBackColor = true;
            // 
            // frmOptions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 266);
            this.Controls.Add(this.rb4Player);
            this.Controls.Add(this.rb3Player);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.rb2Player);
            this.Controls.Add(this.rb1Player);
            this.Name = "frmOptions";
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rb1Player;
        private System.Windows.Forms.RadioButton rb2Player;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rb3Player;
        private System.Windows.Forms.RadioButton rb4Player;
    }
}