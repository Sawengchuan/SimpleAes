using EncryptionLib;

namespace WinFormClient
{
    public partial class Decrypt : Form
    {
        TableLayoutPanel tblTable = new TableLayoutPanel();

        EncryptionLib.Encryptor.ISymmetricEncryptor aes;

        public Decrypt()
        {
            InitializeComponent();

            openFileDialog1.Title = "Choose file to decrypt";
            openFileDialog1.FileName = String.Empty;

            tblTable.Dock = DockStyle.Fill;
            groupBox2.Controls.Add(tblTable);

            aes = new EncryptionLib.Encryptor.AesEncryptor();
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
                try
                {

                    var result = await aes.DecryptFile(FileName, Password);

                    TableDrawerHelper.Draw(groupBox2, tblTable, result);

                }
                catch (Exception ex)
                {
                    TableDrawerHelper.Draw(groupBox2, tblTable, ex);
                }
            }
            else
            {
                List<string> list = new List<string>() { "File and password needed" };

                TableDrawerHelper.Draw(groupBox2, tblTable, list);

            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            var result = string.Empty;
            foreach (var ctrl in tblTable.Controls)
            {
                if (ctrl is Label)
                {
                    result += ((Label)ctrl).Text + Environment.NewLine;
                }
            }

            if (!string.IsNullOrEmpty(result?.Trim()))
                Clipboard.SetText(result);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tblTable.Controls.Clear();
        }
    }
}
