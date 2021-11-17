namespace WinFormClient
{
    partial class Decrypt
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
            this.pnlDecrypt = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbPasswordDecrypt = new System.Windows.Forms.TextBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.btnOpenFileDecrypt = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSelectedFileDecrypt = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.pnlDecrypt.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlDecrypt
            // 
            this.pnlDecrypt.Controls.Add(this.btnClear);
            this.pnlDecrypt.Controls.Add(this.btnCopy);
            this.pnlDecrypt.Controls.Add(this.groupBox2);
            this.pnlDecrypt.Controls.Add(this.label8);
            this.pnlDecrypt.Controls.Add(this.tbPasswordDecrypt);
            this.pnlDecrypt.Controls.Add(this.btnDecrypt);
            this.pnlDecrypt.Controls.Add(this.btnOpenFileDecrypt);
            this.pnlDecrypt.Controls.Add(this.label9);
            this.pnlDecrypt.Controls.Add(this.lblSelectedFileDecrypt);
            this.pnlDecrypt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDecrypt.ForeColor = System.Drawing.Color.LawnGreen;
            this.pnlDecrypt.Location = new System.Drawing.Point(0, 0);
            this.pnlDecrypt.Name = "pnlDecrypt";
            this.pnlDecrypt.Size = new System.Drawing.Size(720, 475);
            this.pnlDecrypt.TabIndex = 6;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.ForeColor = System.Drawing.Color.LawnGreen;
            this.groupBox2.Location = new System.Drawing.Point(19, 161);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(684, 299);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 15);
            this.label8.TabIndex = 11;
            this.label8.Text = "Password :";
            // 
            // tbPasswordDecrypt
            // 
            this.tbPasswordDecrypt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbPasswordDecrypt.BackColor = System.Drawing.Color.Black;
            this.tbPasswordDecrypt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbPasswordDecrypt.ForeColor = System.Drawing.Color.LawnGreen;
            this.tbPasswordDecrypt.Location = new System.Drawing.Point(93, 20);
            this.tbPasswordDecrypt.Name = "tbPasswordDecrypt";
            this.tbPasswordDecrypt.Size = new System.Drawing.Size(615, 23);
            this.tbPasswordDecrypt.TabIndex = 10;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.FlatAppearance.BorderSize = 2;
            this.btnDecrypt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGreen;
            this.btnDecrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecrypt.Location = new System.Drawing.Point(19, 113);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(98, 31);
            this.btnDecrypt.TabIndex = 9;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // btnOpenFileDecrypt
            // 
            this.btnOpenFileDecrypt.BackColor = System.Drawing.Color.Black;
            this.btnOpenFileDecrypt.FlatAppearance.BorderSize = 2;
            this.btnOpenFileDecrypt.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGreen;
            this.btnOpenFileDecrypt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFileDecrypt.Location = new System.Drawing.Point(19, 61);
            this.btnOpenFileDecrypt.Name = "btnOpenFileDecrypt";
            this.btnOpenFileDecrypt.Size = new System.Drawing.Size(98, 28);
            this.btnOpenFileDecrypt.TabIndex = 6;
            this.btnOpenFileDecrypt.Text = "Select File";
            this.btnOpenFileDecrypt.UseVisualStyleBackColor = false;
            this.btnOpenFileDecrypt.Click += new System.EventHandler(this.btnOpenFileDecrypt_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(125, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(31, 15);
            this.label9.TabIndex = 7;
            this.label9.Text = "File :";
            // 
            // lblSelectedFileDecrypt
            // 
            this.lblSelectedFileDecrypt.AutoSize = true;
            this.lblSelectedFileDecrypt.Location = new System.Drawing.Point(162, 68);
            this.lblSelectedFileDecrypt.Name = "lblSelectedFileDecrypt";
            this.lblSelectedFileDecrypt.Size = new System.Drawing.Size(0, 15);
            this.lblSelectedFileDecrypt.TabIndex = 8;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnClear
            // 
            this.btnClear.FlatAppearance.BorderSize = 2;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGreen;
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Location = new System.Drawing.Point(181, 116);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(50, 28);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.FlatAppearance.BorderSize = 2;
            this.btnCopy.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGreen;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Location = new System.Drawing.Point(125, 116);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(50, 28);
            this.btnCopy.TabIndex = 16;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // Decrypt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(720, 475);
            this.Controls.Add(this.pnlDecrypt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Decrypt";
            this.Text = "Decrypt";
            this.pnlDecrypt.ResumeLayout(false);
            this.pnlDecrypt.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel pnlDecrypt;
        private GroupBox groupBox2;
        private Label label8;
        private TextBox tbPasswordDecrypt;
        private Button btnDecrypt;
        private Button btnOpenFileDecrypt;
        private Label label9;
        private Label lblSelectedFileDecrypt;
        private OpenFileDialog openFileDialog1;
        private Button btnClear;
        private Button btnCopy;
    }
}