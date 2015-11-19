using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ANSI2UNICODE
{


    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EncodingInfo[] encoodingList = Encoding.GetEncodings();
            foreach (EncodingInfo encoding in encoodingList)
            {
                ComboboxItem item = new ComboboxItem();
                item.Text = encoding.Name +" - "+ encoding.DisplayName ;
                item.Value = encoding.CodePage;
                cboEncoding.Items.Add(item);
            }

        }
        byte[] temp = null;

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtFilePath.Text = ofd.FileName;
                if (File.Exists(ofd.FileName))
                {
                    temp = File.ReadAllBytes(ofd.FileName);
                    txtOriginal.Text = File.ReadAllText(ofd.FileName);
                }
            }
        }

        private void cboEncoding_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboboxItem item = cboEncoding.SelectedItem as ComboboxItem;
            if (item != null)
            {
                // temp  = Encoding.ASCII.GetBytes(txtOriginal.Text);
                Encoding c1256 = System.Text.Encoding.GetEncoding(int.Parse( item.Value.ToString()));
                char[] chars = c1256.GetChars(temp);
                StringBuilder sb = new StringBuilder();
                sb.Append(chars);
                txtConverted.Text = sb.ToString();
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            SaveFileDialog svd = new SaveFileDialog();
            if (svd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamWriter sr = File.CreateText(svd.FileName);
                sr.Write(txtConverted.Text.ToCharArray());
                sr.Close();
            }
        }


    }
}
