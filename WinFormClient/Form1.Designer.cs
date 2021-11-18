namespace WinFormClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnGoCredit = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.btnGoDecrypt = new System.Windows.Forms.Button();
            this.btnGoEncrypt = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnGoCredit);
            this.splitContainer1.Panel1.Controls.Add(this.btnAbout);
            this.splitContainer1.Panel1.Controls.Add(this.btnGoDecrypt);
            this.splitContainer1.Panel1.Controls.Add(this.btnGoEncrypt);
            this.splitContainer1.Panel1.Cursor = System.Windows.Forms.Cursors.Hand;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlContent);
            this.splitContainer1.Panel2.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.Size = new System.Drawing.Size(884, 441);
            this.splitContainer1.SplitterDistance = 120;
            this.splitContainer1.TabIndex = 5;
            // 
            // btnGoCredit
            // 
            this.btnGoCredit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnGoCredit.FlatAppearance.BorderSize = 2;
            this.btnGoCredit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGreen;
            this.btnGoCredit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoCredit.Location = new System.Drawing.Point(12, 354);
            this.btnGoCredit.Name = "btnGoCredit";
            this.btnGoCredit.Size = new System.Drawing.Size(95, 34);
            this.btnGoCredit.TabIndex = 3;
            this.btnGoCredit.Text = "Credit";
            this.btnGoCredit.UseVisualStyleBackColor = true;
            this.btnGoCredit.Click += new System.EventHandler(this.btnGoCredit_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAbout.FlatAppearance.BorderSize = 2;
            this.btnAbout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGreen;
            this.btnAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbout.Location = new System.Drawing.Point(12, 394);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(95, 34);
            this.btnAbout.TabIndex = 2;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnGoDecrypt
            // 
            this.btnGoDecrypt.FlatAppearance.BorderSize = 2;
            this.btnGoDecrypt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGreen;
            this.btnGoDecrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoDecrypt.Location = new System.Drawing.Point(12, 52);
            this.btnGoDecrypt.Name = "btnGoDecrypt";
            this.btnGoDecrypt.Size = new System.Drawing.Size(95, 34);
            this.btnGoDecrypt.TabIndex = 1;
            this.btnGoDecrypt.Text = "Decrypt";
            this.btnGoDecrypt.UseVisualStyleBackColor = true;
            this.btnGoDecrypt.Click += new System.EventHandler(this.btnGoDecrypt_Click);
            // 
            // btnGoEncrypt
            // 
            this.btnGoEncrypt.FlatAppearance.BorderSize = 2;
            this.btnGoEncrypt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGreen;
            this.btnGoEncrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGoEncrypt.Location = new System.Drawing.Point(12, 12);
            this.btnGoEncrypt.Name = "btnGoEncrypt";
            this.btnGoEncrypt.Size = new System.Drawing.Size(95, 34);
            this.btnGoEncrypt.TabIndex = 0;
            this.btnGoEncrypt.Text = "Encrypt";
            this.btnGoEncrypt.UseVisualStyleBackColor = true;
            this.btnGoEncrypt.Click += new System.EventHandler(this.btnGoEncrypt_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.RosyBrown;
            this.pnlContent.Location = new System.Drawing.Point(43, 68);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(623, 221);
            this.pnlContent.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(884, 441);
            this.Controls.Add(this.splitContainer1);
            this.ForeColor = System.Drawing.Color.LawnGreen;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(750, 480);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simple AES";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResizeEnd += new System.EventHandler(this.Form1_ResizeEnd);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private SplitContainer splitContainer1;
        private Button btnGoDecrypt;
        private Button btnGoEncrypt;
        private Button btnAbout;
        private Panel pnlContent;
        private Button btnGoCredit;
    }
}