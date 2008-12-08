using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;


namespace Logic
{
    public partial class SolveLGC : Form

    {
        const string path = "Inferencer.dll";
        [DllImport(path)]
        static extern string inferencer(string str);
        public SolveLGC()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Output.Text = convert2Lgc(inferencer(convert2Normal(Input.Text)));
          
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Input_TextChange(object sender, EventArgs e)
        {
            Input.Text = convert2Lgc(Input.Text);
            Input.SelectionStart = Input.Text.Length ;
        }
        string convert2Lgc(string text)
        {   
            text = text.Replace("&", "∧");
            text = text.Replace(" AND ", "∧");
            text = text.Replace(" and ", "∧");
            text = text.Replace("v", "∨");
            text = text.Replace(" OR ", "∨");
            text = text.Replace(" or ", "∨");
            text = text.Replace(" | ","∨");
            text = text.Replace("!", "¬");
            text = text.Replace("~", "¬");
            text = text.Replace(" NOT ", "¬");
            text = text.Replace("->", "→");
            text = text.Replace("|-", "┠");
            text = text.Replace(" all ","∀ ");
            text = text.Replace(" exists ","∃");
            text = text.Replace("-]","∃");
            text = text.Replace("V-","∀");
            text = text.Replace("_|_", "⊥");
            //           ¬   →   ∧    ∨      ┠      ∃   ∀ 
            return text;
        }
 
        string convert2Normal(string text)
        {
            text = text.Replace("∧","&" );
            text = text.Replace("∨","|" );
            text = text.Replace("¬","!" );
            text = text.Replace("→","->");
            text = text.Replace("┠","|-");
            text = text.Replace("∀"," all ");
            text = text.Replace("∃"," exists ");
            text = text.Replace("⊥", "_|_");
            return text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() != DialogResult.OK)
                return;
            Input.Text = File.ReadAllText(openFile.FileName);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
                Input.Text = Clipboard.GetText();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input.Clear();
            Output.Clear();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.ShowDialog();    
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input.Copy();
            Output.Copy();
            Input.Text = Clipboard.GetText();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input.Cut();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Input.IsAccessible)
            {
                Input.SelectAll();           
            }
            else if (Output.IsAccessible)
                Output.SelectAll();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void SolveLGC_Load(object sender, EventArgs e)
        {

        }
    }
}