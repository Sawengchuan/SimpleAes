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
    public partial class Decrypt : Form
    {
        public Decrypt()
        {
            InitializeComponent();
        }

        private void btnOpenFileDecrypt_Click(object sender, EventArgs e)
        {
            var result = openFileDialog1.ShowDialog(this);

            if (result == DialogResult.OK)
            {
                var FileName = openFileDialog1.FileName;

                lblSelectedFileDecrypt.Text = FileName;

            }
        }

        private async void btnDecrypt_Click(object sender, EventArgs e)
        {
            var FileName = lblSelectedFileDecrypt.Text;
            var Password = tbPasswordDecrypt.Text;

            if (!string.IsNullOrEmpty(FileName?.Trim()) && !string.IsNullOrEmpty(Password?.Trim()))
            {
                AesEncryptor aes = new();

                try
                {
                    tbResultDecrypt.Text = "";

                    var result = await aes.DecryptFile(FileName, Password);

                    if (result.Success)
                    {
                        tbResultDecrypt.Text = "Success" + Environment.NewLine;
                        tbResultDecrypt.Text += $"Encrypted file: {result.NewFIlePath}" + Environment.NewLine;
                        tbResultDecrypt.Text += $"Backup file: {result.OldFilePath}" + Environment.NewLine;
                    }

                }
                catch (Exception ex)
                {
                    tbResultDecrypt.Text = "Failed" + Environment.NewLine;
                    tbResultDecrypt.Text += ex.Message;
                }
            }
            else
            {
                tbResultDecrypt.Text = "File and password needed";
            }
        }
    }
}
