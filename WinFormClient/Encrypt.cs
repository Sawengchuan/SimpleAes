using EncryptionLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormClient
{
    public partial class Encrypt : Form
    {
        public Encrypt()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var FileName = openFileDialog1.FileName;

                lblSelectedFile.Text = FileName;

            }
        }

        private async void btnEncrypt_Click(object sender, EventArgs e)
        {
            var FileName = lblSelectedFile.Text;
            var Password = tbPassword.Text;

            if (!string.IsNullOrEmpty(FileName?.Trim()) && !string.IsNullOrEmpty(Password?.Trim()))
            {
                AesEncryptor aes = new();

                try
                {
                    tbResult.Text = "";

                    var result = await aes.EncryptFile(FileName, Password);

                    if (result.Success)
                    {
                        tbResult.Text = "Success" + Environment.NewLine;
                        tbResult.Text += $"Encrypted file: {result.NewFIlePath}" + Environment.NewLine;
                        tbResult.Text += $"Backup file: {result.OldFilePath}" + Environment.NewLine;
                    }

                }
                catch (Exception ex)
                {
                    tbResult.Text = "Failed" + Environment.NewLine;
                    tbResult.Text += ex.Message;
                }
            }
            else
            {
                tbResult.Text = "File and password needed";
            }
        }
    }
}
