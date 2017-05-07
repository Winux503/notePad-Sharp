using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace Notepad_Sharp
{
    public partial class NotepadSharp : Form
    {
        public RichTextBox rBox = new RichTextBox();
        string tabTitle = "untitled.txt";
        
        public NotepadSharp()
        {
            InitializeComponent();
           
            if (File.Exists("config.bin"))
            {
                try
                {
                    using (BinaryReader br =
                        new BinaryReader(File.Open(binFile, FileMode.Open)))
                    {
                        this.Size = new Size(br.ReadInt32(), br.ReadInt32());
                        this.Top = br.ReadInt32();
                        this.Left = br.ReadInt32();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error 1 - Cannot Write Config./n Are you admin?");
                }
            }
            Updater.Start();
            rBox.Size = new System.Drawing.Size(1109, 652);
            rBox.Location = new System.Drawing.Point(6, 6);
            rBox.Dock = DockStyle.Fill;
            rBox.Margin = new System.Windows.Forms.Padding(3,3,3,3);
            TabPage tbPage = new TabPage(tabTitle);
            tbPage.Size = new System.Drawing.Size(1121, 664);
            Tabs.TabPages.Add(tbPage);
            tbPage.Controls.Add(rBox);
            Tabs.SelectedTab = tbPage;
            for (int i = 0; i < 1; i++)
            {
                Control Current = Tabs.TabPages[i];
                if (Current is TabPage)
                {
                    if (tabTitle != null)
                    {
                        Tabs.TabPages[i].Text = tabTitle;
                    }
                }
            }
            SetChildColour(this, Color.FromArgb(255, 255, 255), Color.FromArgb(0, 0, 0));
            html = System.IO.File.ReadAllLines(path);
            HTMLAtrib = System.IO.File.ReadAllLines(path2);
            HighlighterColourMain = Color.Blue;
            HighlighterColourSecond = Color.HotPink;
            Update.Interval = 5000;
            Update.Start();
            Form3 CodeBin = new Form3();
            CodeBin.Show();

            cm.MenuItems.Add("Copy", new EventHandler(copyToolStripMenuItem_Click));
            cm.MenuItems.Add("Paste", new EventHandler(pasteToolStripMenuItem_Click));

            rBox.ContextMenu = cm;
            Tabs.SelectedTab.Controls[0].ContextMenu = cm;
        }

        string[] fileNameGlobals = new string[50000];
        string binFile = Path.Combine(Environment.CurrentDirectory,"config.bin");
        ContextMenu cm = new ContextMenu();
        
        //Programming Language Files
        string[] html = new string[2000];
        string path = Environment.CurrentDirectory + @"/ProgrammingLanguages/HTMLTags.txt";
        string[] HTMLAtrib = new string[2000];
        string path2 = Environment.CurrentDirectory + @"/ProgrammingLanguages/attributes.txt";

        private Color HighlighterColourMain;
        private Color HighlighterColourSecond;

        private void NotepadSharp_Load(object sender, EventArgs e)
        {

        }

        private void languageToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fullscreenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            PageMaker();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            DialogResult result = FileOpener.ShowDialog();
            if (result == DialogResult.OK)
            {
                PageMaker();
                rBox.Text = File.ReadAllText(FileOpener.FileName);
                Tabs.SelectedTab.Text = FileOpener.FileName;
                fileNameGlobals[Tabs.SelectedIndex] = FileOpener.FileName;
                SetChildColour(this, Color.FromArgb(255, 255, 255), Color.FromArgb(0, 0, 0));
                HighlighterColourMain = Color.Blue;
                HighlighterColourSecond = Color.HotPink;
            }
            else
            {
                FileOpener.Reset();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Tabs.TabPages[Tabs.SelectedIndex].Controls[0].Text != "")
            {
                File.WriteAllText(fileNameGlobals[Tabs.SelectedIndex], Tabs.TabPages[Tabs.SelectedIndex].Controls[0].Text.Replace("\n", Environment.NewLine), Encoding.Unicode);
            }
            else
            {
                MessageBox.Show("There is nothing to save...");
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
           using (BinaryWriter bw =
                new BinaryWriter(File.Open(binFile, FileMode.Create)))
            {
                bw.Write(this.Width);
                bw.Write(this.Height);
                bw.Write(this.Top);
                bw.Write(this.Left);
            }
            if (MessageBox.Show("Really Quit?", "Exit",
                    MessageBoxButtons.OKCancel,
                        MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageMaker();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = FileOpener.ShowDialog();
            if (result == DialogResult.OK)
            {
                PageMaker();
                rBox.Text = File.ReadAllText(FileOpener.FileName);
                Tabs.SelectedTab.Text = FileOpener.FileName;
                fileNameGlobals[Tabs.SelectedIndex] = FileOpener.FileName;
                SetChildColour(this, Color.FromArgb(255, 255, 255), Color.FromArgb(0, 0, 0));
                HighlighterColourMain = Color.Blue;
                HighlighterColourSecond = Color.HotPink;
            }
            else
            {
                FileOpener.Reset();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(fileNameGlobals[Tabs.SelectedIndex], Tabs.TabPages[Tabs.SelectedIndex].Controls[0].Text.Replace("\n", Environment.NewLine), Encoding.Unicode);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = FileSaver.ShowDialog();
            if (result == DialogResult.OK)
            {
                File.WriteAllText(FileSaver.FileName, rBox.Text.Replace("\n", Environment.NewLine), Encoding.Unicode);
                Tabs.SelectedTab.Text = FileSaver.FileName;
                fileNameGlobals[Tabs.SelectedIndex] = FileSaver.FileName;
            }
            else
            {
                FileSaver.Reset();
            }
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Tabs.TabPages.Count; i++)
            {
                File.WriteAllText(fileNameGlobals[i], Tabs.TabPages[i].Controls[0].Text.Replace("\n", Environment.NewLine), Encoding.Unicode);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Tabs.TabPages.Remove(Tabs.SelectedTab);
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            foreach(TabPage c in Tabs.TabPages)
            {
                for (int i = 1; i < Tabs.TabPages.Count; i++)
                {
                    if (c != Tabs.TabPages[0])
                    {
                        Tabs.TabPages.RemoveAt(i);
                    }
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (BinaryWriter bw =
                new BinaryWriter(File.Open(binFile, FileMode.Create)))
            {
                bw.Write(this.Width);
                bw.Write(this.Height);
                bw.Write(this.Top);
                bw.Write(this.Left);
            }
            if (MessageBox.Show("Really Quit?", "Exit",
                       MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question) == DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void Updater_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = DateTime.Now.ToLongTimeString();
            if (Tabs.SelectedTab == Tabs.TabPages[0])
            {
                toolStripStatusLabel2.Text = "Character Count: " + Convert.ToString(rBox.TextLength);
            }
        }

        private void multiLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = FontChooser.ShowDialog();
            if (result == DialogResult.OK)
            {
                Tabs.TabPages[Tabs.SelectedIndex].Controls[0].Font = new Font(FontChooser.Font.FontFamily, FontChooser.Font.Size, FontChooser.Font.Style);
            }
            else
            {
                FontChooser.Reset();
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            DialogResult result = FontChooser.ShowDialog();
            if (result == DialogResult.OK)
            {
                Tabs.TabPages[Tabs.SelectedIndex].Controls[0].Font = new Font(FontChooser.Font.FontFamily, FontChooser.Font.Size, FontChooser.Font.Style);
            }
            else
            {
                FontChooser.Reset();
            }
        }

        private void PageMaker()
        {
            rBox = new RichTextBox();
            rBox.Size = new System.Drawing.Size(Tabs.TabPages[Tabs.SelectedIndex].Width - 6, Tabs.TabPages[Tabs.SelectedIndex].Width - 6);
            rBox.Location = new System.Drawing.Point(6, 6);
            rBox.Dock = DockStyle.Fill;
            rBox.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            TabPage newPage = new TabPage(tabTitle);
            newPage.Size = new System.Drawing.Size(this.Width - 6, this.Height - 6);
            Tabs.TabPages.Add(newPage);
            newPage.Controls.Add(rBox);
            Tabs.SelectedTab = newPage;
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(rBox.SelectedText, TextDataFormat.Rtf);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rBox.AppendText(Clipboard.GetText(TextDataFormat.Rtf));
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripTextBox1.Focus();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (rBox.Text.Contains(toolStripTextBox1.Text))
            {
                rBox.SelectionStart = rBox.Text.IndexOf(toolStripTextBox1.Text);
                rBox.SelectionLength = toolStripTextBox1.Text.Length;
                rBox.Focus();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 AboutPage = new Form2();
            AboutPage.ShowDialog();
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetChildColour(this, Color.FromArgb(58, 58, 58), Color.FromArgb(255, 255, 255));
            HighlighterColourMain = Color.LightBlue;
            HighlighterColourSecond = Color.LightPink;
        }

        private void whiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetChildColour(this, Color.FromArgb(255, 255, 255), Color.FromArgb(0, 0, 0));
            HighlighterColourMain = Color.Blue;
            HighlighterColourSecond = Color.HotPink;
        }
        
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColourPicker.ShowDialog();
            SetChildColour(this, Color.FromArgb(ColourPicker.Color.R, ColourPicker.Color.G, ColourPicker.Color.B), Color.FromArgb(0, 0, 0));
            if (this.BackColor.R <= 128)
            {
                if (this.BackColor.G <= 128)
                {
                    if (this.BackColor.B <= 128)
                    {
                        SetChildColour(this, Color.FromArgb(ColourPicker.Color.R, ColourPicker.Color.G, ColourPicker.Color.B), Color.FromArgb(255, 255, 255));
                    }
                    else
                    {
                        SetChildColour(this, Color.FromArgb(ColourPicker.Color.R, ColourPicker.Color.G, ColourPicker.Color.B), Color.FromArgb(0, 0, 0));
                    }
                }
            }
        }

        public void SetChildColour(Control Parent, Color BackColor, Color ForeColor)
        {
            this.BackColor = BackColor;
            this.ForeColor = ForeColor;
            if (Parent == null) return;
            foreach (Control c in Parent.Controls)
            {
                c.BackColor = BackColor;
                c.ForeColor = ForeColor;
                SetChildColour(c, BackColor, ForeColor);
                if (c is RichTextBox || c is TextBox)
                {
                    if (c.BackColor.R <= 128)
                    {
                        if (c.BackColor.G <= 128)
                        {
                            if (c.BackColor.B <= 128)
                            {
                                c.BackColor = Color.FromArgb(BackColor.R + 20, BackColor.G + 20, BackColor.B + 20);
                            }
                        }
                    }
                    else
                    {
                        c.BackColor = Color.FromArgb(BackColor.R - 20, BackColor.G - 20, BackColor.B - 20);
                    }
                }
            }
        }
        
        public void Highlighter()
        {

            int start = rBox.SelectionStart;
            int len = rBox.SelectionLength;

            for (int o = 0; o < HTMLAtrib.Length; o++)
            {
                if (Tabs.TabPages[Tabs.SelectedIndex].Controls[0].Text.Contains(HTMLAtrib[o]) && Tabs.TabPages[Tabs.SelectedIndex].Controls[0].ForeColor != Color.Blue)
                {
                    rBox.SelectionStart = rBox.Text.IndexOf(HTMLAtrib[o]);
                    rBox.SelectionLength = HTMLAtrib[o].Length;
                    rBox.SelectionColor = HighlighterColourSecond;
                    if (rBox.SelectedText != HTMLAtrib[o])
                    {
                        rBox.SelectionColor = Color.Black;
                    }
                }
            }
            for (int i = 0; i < html.Length; i++)
            {
                if (Tabs.TabPages[Tabs.SelectedIndex].Controls[0].Text.Contains(html[i]))
                {
                    rBox.SelectionStart = rBox.Text.IndexOf(html[i]);
                    rBox.SelectionLength = html[i].Length;
                    rBox.SelectionColor = HighlighterColourMain;
                    if (rBox.SelectedText != html[i])
                    {
                        rBox.SelectionColor = Color.Black;
                    }   
                }
            }
            
            rBox.SelectionStart = start;
            rBox.SelectionLength = len;
            //rBox.Select();
        }

        private void Update_Tick(object sender, EventArgs e)
        {
            Highlighter();
            rBox.ContextMenu = cm;
            Tabs.SelectedTab.Controls[0].ContextMenu = cm;
        }

        private void fullscreenToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (this.TopMost != true)
            {
                this.TopMost = true;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.TopMost = false;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
            
        }
    }
}
