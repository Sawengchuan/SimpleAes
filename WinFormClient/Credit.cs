namespace WinFormClient
{
    public partial class Credit : Form
    {
        struct CreditInfo
        {
            public string Description;
            public string Link;
        }

        List<CreditInfo> creditList = new List<CreditInfo>();
        public Credit()
        {
            InitializeComponent();

            creditList.Add(new CreditInfo { Description = "Lock Image", Link = "https://www.clipartmax.com" });
            creditList.Add(new CreditInfo { Description = "Application Icon", Link = "https://convertio.co/png-ico/" });
            creditList.Add(new CreditInfo { Description = "AES Encryption in C#", Link = "https://tomrucki.com/posts/aes-encryption-in-csharp/" });
            creditList.Add(new CreditInfo { Description = "Using CryptoStreams to encrypt and HMAC data", Link = "https://stackoverflow.com/questions/15125680/using-cryptostreams-to-encrypt-and-hmac-data" });
            creditList.Add(new CreditInfo { Description = "AES-Encrypt-then-MAC a large file with .NET", Link = "https://stackoverflow.com/questions/38623335/aes-encrypt-then-mac-a-large-file-with-net/" });
            creditList.Add(new CreditInfo { Description = "Misc", Link = "" });
        }

        private void Credit_Load(object sender, EventArgs e)
        {
            TableLayoutPanel tblPanel = new TableLayoutPanel();
            tblPanel.Dock = DockStyle.Fill;


            int rowcount = 1;
            tblPanel.ColumnCount = 1;
            tblPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));

            foreach (CreditInfo credit in creditList)
            {
                var c1 = new Label();
                c1.Text = credit.Description + Environment.NewLine + credit.Link;
                c1.AutoSize = true;

                tblPanel.Controls.Add(c1, 0, rowcount);
                tblPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
                rowcount++;
            }

            panel1.Controls.Add(tblPanel);

        }

    }
}
