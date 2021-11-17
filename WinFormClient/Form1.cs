namespace WinFormClient
{
    public partial class Form1 : Form
    {
        About about = new About() { TopLevel = false };
        Home home = new Home() { TopLevel = false };
        Encrypt encrypt = new Encrypt() { TopLevel = false };
        Decrypt decrypt = new Decrypt() { TopLevel = false };
        Credit credit = new Credit() { TopLevel = false };

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            //this.Paint += new PaintEventHandler(set_background);

            this.Width = 800;
            this.Height = 600;
        }

        void LoadForm<T>(Control owner, T form) where T : Form
        {
            owner.Controls.Clear();
            owner.Controls.Add(form);
            form.Dock = DockStyle.Fill;
            form.Show();
        }

        //private void set_background(object? sender, PaintEventArgs e)
        //{
        //    Graphics graphics = e.Graphics;

        //    //the rectangle, the same size as our Form
        //    Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);

        //    //define gradient's properties
        //    Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(0, 0, 0), Color.FromArgb(57, 128, 227), 65f);
        //    b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(192, 255, 255), Color.FromArgb(255, 255, 255), 65f);

        //    //apply gradient         
        //    graphics.FillRectangle(b, gradient_rectangle);

        //}



        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            this.Invalidate();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            pnlContent.Visible = true;
            pnlContent.Dock = DockStyle.Fill;

            LoadForm(pnlContent, home);
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            LoadForm(pnlContent, about);
        }

        private void btnGoEncrypt_Click(object sender, EventArgs e)
        {
            LoadForm(pnlContent, encrypt);
        }

        private void btnGoDecrypt_Click(object sender, EventArgs e)
        {
            LoadForm(pnlContent, decrypt);
        }

        private void btnGoCredit_Click(object sender, EventArgs e)
        {
            LoadForm(pnlContent, credit);
        }
    }
}