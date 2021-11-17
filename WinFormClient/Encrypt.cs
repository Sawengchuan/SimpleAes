using EncryptionLib;

namespace WinFormClient
{
    public partial class Encrypt : Form
    {
        TableLayoutPanel tblTable = new TableLayoutPanel();

        public Encrypt()
        {
            InitializeComponent();

            openFileDialog1.Title = "Choose file to encrypt";
            openFileDialog1.FileName = String.Empty;

            tblTable.Dock = DockStyle.Fill;
            groupBox1.Controls.Add(tblTable);
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
                    var result = await aes.EncryptFile(FileName, Password);

                    TableDrawerHelper.Draw(groupBox1, tblTable, result);

                }
                catch (Exception ex)
                {
                    TableDrawerHelper.Draw(groupBox1, tblTable, ex);
                }
            }
            else
            {
                List<string> list = new List<string>() { "File and password needed" };

                TableDrawerHelper.Draw(groupBox1, tblTable, list);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tblTable.Controls.Clear();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            var result = string.Empty;
            foreach(var ctrl in tblTable.Controls)
            {
                if(ctrl is Label)
                {
                    result += ((Label)ctrl).Text + Environment.NewLine;
                }
            }

            if (!string.IsNullOrEmpty(result?.Trim()))
                Clipboard.SetText(result);
        }
    }
}
