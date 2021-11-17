using EncryptionLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormClient
{
    internal class TableDrawerHelper
    {
        public static void Draw(Control owner, TableLayoutPanel tablePanel, Result result)
        {
            if (owner == null)
                return;

            if (result == null)
                return;

            if (tablePanel == null)
                return;

            tablePanel.Controls.Clear();

            List<string> list = new List<string>();
            list.Add(result.Success ? "Success" : "Failed");
            list.Add(result.CryptoOp == CryptoOp.Encrypt ? "Encrypted file:" : "Decrypted file:");
            list.Add(result.NewFIlePath);
            list.Add("Backup file:");
            list.Add(result.OldFilePath);

            Draw(owner, tablePanel, list);

            var btn = new Button();
            btn.Text = "Open Folder";
            btn.AutoSize = true;
            btn.ForeColor = Color.LawnGreen;
            btn.BackColor = Color.Black;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.MouseOverBackColor = Color.DarkGreen;

            FileInfo fi = new FileInfo(result.OldFilePath);
            FileInfo fiNew = new FileInfo(result.NewFIlePath);

            if (fi.Exists || fiNew.Exists)
            {
                var initialDir = string.Empty;

                if (fi.Exists)
                    initialDir = fi.DirectoryName;

                if (fiNew.Exists)
                    initialDir = fiNew.DirectoryName;

                OpenFileDialog dialog = new OpenFileDialog();
                dialog.InitialDirectory = initialDir;

                btn.Click += (sender, e) => { dialog.ShowDialog(); };
            }

            tablePanel.Controls.Add(btn, 1, list.Count);

        }

        public static void Draw(Control owner, TableLayoutPanel tablePanel, Exception ex)
        {
            if (owner == null)
                return;

            if (tablePanel == null)
                return;

            if (ex == null)
                return;

            tablePanel.Controls.Clear();

            List<string> list = new List<string>();
            list.Add("Failed");
            list.Add(ex.Message);

            Draw(owner, tablePanel, list);

        }
        public static void Draw(Control owner, TableLayoutPanel tablePanel, List<string> message)
        {
            if (owner == null)
                return;

            if (tablePanel == null)
                return;

            if (message == null)
                return;

            tablePanel.Controls.Clear();

            int rowcount = 0;

            foreach (var msg in message)
            {

                Label lbl = new Label();
                lbl.Text = msg;
                lbl.AutoSize = true;

                tablePanel.Controls.Add(lbl, 1, rowcount);

                rowcount++;
            }
        }

    }
}
