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

namespace Notepad_Sharp
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            if (File.Exists("codeBin.bin"))
            {
                try
                {
                    using (BinaryReader br =
                        new BinaryReader(File.Open(binFile, FileMode.Open)))
                    {
                        txtBin.Text = br.ReadString();  
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 1 - Cannot Write Config./n Are you admin?");
                }
            }
        }
        string binFile = Path.Combine(Environment.CurrentDirectory,"codeBin.bin");
        private void txtBin_TextChanged(object sender, EventArgs e)
        {
            using (BinaryWriter bw =
               new BinaryWriter(File.Open(binFile, FileMode.Create)))
            {
                bw.Write(txtBin.Text);
                
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBin.Text = "";
        }

        private void txtBinFind_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnFind_Click(object sender, EventArgs e)
        {

        }
    }
}
