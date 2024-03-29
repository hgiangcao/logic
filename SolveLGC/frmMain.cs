using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Text.RegularExpressions ;
using System.Threading;
using System.Diagnostics;


namespace Logic
{
    public partial class SolveLGC : Form

    {
        const string path = "Inferencer.dll";
        [DllImport(path)]
        static extern string inferencer(string str);
        string[] arraystr;
        int step = 0;
       
        bool isclick = false;
        bool iserror = false;
        int charStart, charFinish, lineStart, lineFinish;
        string inputt;
        string outputt;
 //       Thread inference;
        Options frmOpt;
        Waiting frmWaiting;
        private void prove(bool isShowAll)
        {
            Output.Font = label4.Font;
            try
            {

                
                inputt = convert2Normal(Input.Text);
                string dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                File.WriteAllText(dir + "\\solveLogic.txt", inputt);
                frmWaiting = new Waiting( frmOpt.Maxtime);
                //this.Opacity = 0.9f;
                frmWaiting.Left = Left + (Output.Left + Output.Right - frmWaiting.Width) / 2;
                frmWaiting.Top = Top + Output.Top + Output.Height / 4 ;
                frmWaiting.ShowDialog();
                outputt = File.ReadAllText(dir + "\\solveLogic.txt");
                

            }
            catch
            {
                outputt = "Cannot SOLVE...";
                
            }
            finally
            {
            //    MessageBox.Show(outputt.ToString());
                //this.Opacity = 1.0f;
                if (isShowAll)
                    showAll();
                else
                    ShowStep();
  //              button2.Enabled = true;
            }
        }
        public SolveLGC()
        {            
            InitializeComponent();
            label4.Font = Output.Font;
             frmOpt = new Options();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            btnProveAll.Enabled = false;
            btnStep.Enabled = false;
            if (Input.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập đề toán!!!");
            else
            {
               
                prove(true);

                isclick = false;
                arraystr = null;
                step = 0;
                save.Enabled = true;
            }
        }
        #region abc
        private void showAll()
        {
            float maxlength = 0;
            int t = 0;
            if (outputt.StartsWith("Sai"))
            {
                Output.Text = "Bai toan sai ngu nghia!!!";   
                return;
            }
            else if (outputt == "Cannot SOLVE...")
            {
                Output.Text = ("Không giải được !!!");  
                Output.Font = label3.Font;
                return;
            }
            
            else if (outputt.StartsWith("Unexpected"))
            //{
            //    iserror = true;
            {
                Output.Text = convert2Lgc(outputt);
                return;
            }
            //    string[] instr = Regex.Split(outputt, "CharStart:");
            //    string[] cStart = Regex.Split(instr[1], "\tCharFinish:");
            //    charStart = Int32.Parse((string)cStart[0]);
            //    string[] cFinish = Regex.Split(cStart[1], "\tLineStart:");
            //    charFinish = Int32.Parse((string)cFinish[0]);
            //    string[] lStart = Regex.Split(cFinish[1], "\tLineFinish:");
            //    lineStart = Int32.Parse((string)lStart[0]);
            //    lineFinish = Int32.Parse((string)lStart[1]);
            //    if (lineFinish == 1)
            //    {
            //        string in_text = Input.Text;
            //        char[] intext = in_text.ToCharArray();
            //        for (int v = 0; v < charStart - 1; v++)
            //        {
            //            if ((intext[v]).ToString() == "→" || (intext[v]).ToString() == "┠" || (intext[v]).ToString() == "∀" || (intext[v]).ToString() == "∃")
            //            {
            //                charStart--;
            //                charFinish--;
            //            }
            //            if ((intext[v]).ToString() == "⊥")
            //            {
            //                charStart -= 2;
            //                charFinish -= 2;
            //            }
            //        }
            //        Input.SelectionStart = charStart - 1;
            //        Input.SelectionLength = charFinish - charStart + 1;
            //        Input.SelectionBackColor = Color.SeaGreen;
            //        Input.SelectionColor = Color.Red;
            //    }
            //    else
            //    {

            //        int x;
            //        string in_text = Input.Text;
            //        char[] intext = in_text.ToCharArray();
            //        for (int v = 0; v < in_text.Length; v++)
            //        {
            //            if ((intext[v]).ToString() == "\n")
            //                lineFinish--;

            //            if (lineFinish == 1)
            //            {
            //                x = v;
            //                for (int u = 0; u < charStart - 1; u++)
            //                {
            //                    if ((intext[v + 1]).ToString() == "→" || (intext[v + 1]).ToString() == "┠" || (intext[v + 1]).ToString() == "∀" || (intext[v + 1]).ToString() == "∃")
            //                    {
            //                        charStart--;
            //                        charFinish--;
            //                    }
            //                    if ((intext[v + 1]).ToString() == "⊥")
            //                    {
            //                        charStart -= 2;
            //                        charFinish -= 2;
            //                    }
            //                    v++;
            //                }
            //                if (charStart == 1)
            //                {
            //                    Input.SelectionStart = x + charStart - 1;
            //                    Input.SelectionLength = charFinish - charStart + 2;
            //                }
            //                else
            //                {
            //                    Input.SelectionStart = x + charStart;
            //                    Input.SelectionLength = charFinish - charStart + 1;
            //                }
            //                Input.SelectionBackColor = Color.SeaGreen;
            //                Input.SelectionColor = Color.Red;
            //                break;
            //            }
            //        }
            //    }
            //    iserror = false;
            //    return;
            //}

            else
            {
                string pattern = "\n";
                string[] substrings = Regex.Split(outputt, pattern);    // Split on hyphens
                string[] result = new string[substrings.Length];
                string[] tail = new string[substrings.Length];
                foreach (string match in substrings)
                {
                    //condition processing
                    if (match.Contains("nif"))
                    {
                        conditions.Add(temps[temps.Count - 1]);
                        temps.RemoveAt(temps.Count - 1);
                        conditions.Add(t);
                    }
                    else if (match.Contains("if"))
                    {
                        temps.Add(t);
                    }
                    //
                    string[] substr = Regex.Split(match, "#");
                    if (substr.Length > 1)
                    {
                        result[t] = convert2Lgc(substr[0]);
                        tail[t] = convert2Lgc(substr[1]);
                    }
                    t++;
                }

                Graphics g = CreateGraphics();

                for (int i = 0; i < result.Length; i++)
                {
                    SizeF size = g.MeasureString(result[i], Output.Font);
                    if (maxlength < size.Width)
                        maxlength = size.Width;
                }
                for (int i = 0; i < result.Length; i++)
                {
                    int num_space = 0;
                    SizeF size = g.MeasureString(result[i], Output.Font);
                    if (size.Width <= maxlength)
                    {
                        num_space = (int)((maxlength - size.Width) / 7) + 8;
                        result[i] = result[i] + add_space(num_space) + "\t";
                    }
                }
                Output.Text = result[0] + tail[0];
                for (int i = 1; i < result.Length - 1; i++)
                {
                    Output.Text = Output.Text + "\n" + result[i] + tail[i];
                }
                Output.Select(Output.Text.Length, 0);
                Output.ScrollToCaret();
            }
            btnStep.Enabled = true;
        }
        string add_space(int i)
        {
            string s = "";
            for (int t = 0; t < i; t++)
            {
                s += " ";
            }
            return s;
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        bool isColored = false;
        private void Input_TextChange(object sender, EventArgs e)
        {
            try
            {
                if (isColored)
                {
                    int se = Input.SelectionStart;
                    Input.SelectAll();
                    Input.SelectionBackColor = Input.BackColor;
                    Input.SelectionColor = Input.ForeColor;
                    isColored = false;
                    iserror = false;
                    Input.SelectionStart = se;
                    //string s1 = convert2Lgc(Input.Text.Substring(0, Input.SelectionStart));
                    //string s2 = Input.Text.Substring(Input.SelectionStart, Input.Text.Length - Input.SelectionStart);
                    //Input.Text = convert2Lgc(s1 + s2);
                    //Input.SelectionStart = s1.Length;

                }
                if (!iserror)
                {
                    
                    string s1 = convert2Lgc(Input.Text.Substring(0, Input.SelectionStart));
                    string s2 = Input.Text.Substring(Input.SelectionStart, Input.Text.Length - Input.SelectionStart);
                    Input.Text = convert2Lgc(s1 + s2);
                    Input.SelectionStart = s1.Length;
                    isclick = false;
                    arraystr = null;
                    step = 0;
                    btnProveAll.Enabled = true;
                    btnStep.Enabled = true;
                    Output.Clear();
                    
                }
                save.Enabled = false;

                //if (!outputt.StartsWith("Unexpected"))
                   
            }
            catch
            {
            }
        }

        string convert2Lgc(string text)
        {
            text = text.Replace("<->", "↔");
            text = text.Replace("_|_", "⊥");
            text = text.Replace("&", "∧");
            text = text.Replace(" AND ", "∧");
            text = text.Replace(" and ", "∧");
 //           text = text.Replace("v", "∨");
            text = text.Replace(" OR ", "∨");
            text = text.Replace(" or ", "∨");
            text = text.Replace("|","∨");
            text = text.Replace("!", "¬");
            text = text.Replace("~", "¬");
            text = text.Replace(" NOT ", "¬");
            text = text.Replace("->", "→");
            text = text.Replace("∨-", "┠");
            text = text.Replace("|-", "┠");
            text = text.Replace("all ","∀");
            text = text.Replace("exists ","∃");
            text = text.Replace("-]","∃");
            text = text.Replace("\\-","∀");            
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
            text = text.Replace("∀","\\-");
            text = text.Replace("∃","-]");
            text = text.Replace("⊥", "_|_");
            return text;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Logic File Format(*.lff)|*.lff|Text files(*.txt)|*.txt";
            if (openFile.ShowDialog() != DialogResult.OK)
                return;
            try
            {
                Input.LoadFile(openFile.FileName);
            }
            catch
            {
                Input.LoadFile(openFile.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            iserror = false;
            if (Clipboard.ContainsText())
            {
                Input.Text = Clipboard.GetText();
                //Input.Text += xotring;
                richTextBox1_TextChanged(null, null);
            }
            else
            {
                pasteToolStripMenuItem.Enabled = false;
            }

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Input.Clear();
            Output.Clear();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RichTextBox dummy = new RichTextBox();
            dummy.AcceptsTab = true;
            dummy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dummy.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dummy.Cursor = System.Windows.Forms.Cursors.Default;
            dummy.Font = new System.Drawing.Font("Lucida Sans Unicode", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dummy.HideSelection = false;
            dummy.Location = new System.Drawing.Point(36, 240);
            dummy.Margin = new System.Windows.Forms.Padding(4);
            dummy.Name = "Output";
            dummy.ReadOnly = true;
            dummy.Size = new System.Drawing.Size(1019, 288);
            dummy.TabIndex = 1;
            //dummy.Text = "";
            //dummy.Validated += new System.EventHandler(dummy_Validated);
            //ummy.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            //dummy.Click += new System.EventHandler(this.clickoutput);
            dummy.Text = Input.Text + "\n Bai giai:\n" + Output.Text;
            //dummy.Visible = false;
            this.Controls.Add(dummy);
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "RichTextFormat(*.rtf)|*.rtf";
            if (saveFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            dummy.SaveFile(saveFile.FileName, RichTextBoxStreamType.RichText);
            dummy = null;
            //ResumeLayout();
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SolveLGC_Load(object sender, EventArgs e)
        {

        }
        #endregion
        private void proveStep()
        {
            Output.Font = label4.Font;
            Output.Text = "Đang giải...";
            try
            {
                inputt = convert2Normal(Input.Text);
                outputt = inferencer(inputt);
            }
            catch 
            {                
                outputt = "Cannot SOLVE...";
            }
            finally
            {               
               ShowStep();              
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            btnProveAll.Enabled = false;
            btnStep.Enabled = false;
            if (Input.Text.Trim().Length == 0)
                MessageBox.Show("Vui lòng nhập đề toán!!!");
            else if (!isclick)
            {
                isclick = true;
                //inference = new Thread(new ThreadStart(proveStep));
                //inference.Start();
                prove(false);               
            }
            else  
            {
                if (arraystr != null)
                {
                    if (step < arraystr.Length-1)
                    {
                        Output.Text += arraystr[step] + "\n";
                        save.Enabled = false;
                        step++;
                    }
                    if (step >= (arraystr.Length - 1))
                    {
                        btnProveAll.Enabled = true;
                        save.Enabled = true;
                    }
                    btnProveAll.Enabled = true;
                }
                Output.Select(Output.Text.Length, 0);
                Output.ScrollToCaret();
                btnStep.Enabled = true;                
            }            
        }
        private void ShowStep()
        {
            Output.Text = "";
            float maxlength = 0;
            int t = 0;
            if (outputt == "Cannot SOLVE..." )
            {
                Output.Text = ("Không giải được !!!"); 
                //Output.SelectAll();
                Output.Font = label3.Font;
               // Output.SelectAll();
               // Output.Update();
                return;
            }
            else if (outputt.StartsWith("Sai"))
            {
                Output.Text = "Bai toan sai ngu nghia!!!";
                return;
            }
            else if (outputt.StartsWith("Unexpected"))
            {
                
               // iserror = true;
                Output.Text = convert2Lgc(outputt);
                //string[] instr = Regex.Split(outputt, "CharStart:");
                //string[] cStart = Regex.Split(instr[1], "\tCharFinish:");
                //charStart = Int32.Parse((string)cStart[0]);
                //string[] cFinish = Regex.Split(cStart[1], "\tLineStart:");
                //charFinish = Int32.Parse((string)cFinish[0]);
                //string[] lStart = Regex.Split(cFinish[1], "\tLineFinish:");
                //lineStart = Int32.Parse((string)lStart[0]);
                //lineFinish = Int32.Parse((string)lStart[1]);
                //if (lineFinish == 1)
                //{
                //    string in_text = Input.Text;
                //    char[] intext = in_text.ToCharArray();
                //    for (int v = 0; v < charStart - 1; v++)
                //    {
                //        if ((intext[v]).ToString() == "→" || (intext[v]).ToString() == "┠" || (intext[v]).ToString() == "∀" || (intext[v]).ToString() == "∃")
                //        {
                //            charStart--;
                //            charFinish--;
                //        }
                //        if ((intext[v]).ToString() == "⊥")
                //        {
                //            charStart -= 2;
                //            charFinish -= 2;
                //        }
                //    }
                //    Input.SelectionStart = charStart - 1;
                //    Input.SelectionLength = charFinish - charStart + 1;
                //    Input.SelectionBackColor = Color.SeaGreen;
                //    Input.SelectionColor = Color.Red;
                //}
                //else
                //{
                //    int x;
                //    string in_text = Input.Text;
                //    char[] intext = in_text.ToCharArray();
                //    for (int v = 0; v < in_text.Length; v++)
                //    {
                //        if ((intext[v]).ToString() == "\n")
                //            lineFinish--;

                //        if (lineFinish == 1)
                //        {
                //            x = v;
                //            for (int u = 0; u < charStart - 1; u++)
                //            {
                //                if ((intext[v + 1]).ToString() == "→" || (intext[v + 1]).ToString() == "┠" || (intext[v + 1]).ToString() == "∀" || (intext[v + 1]).ToString() == "∃")
                //                {
                //                    charStart--;
                //                    charFinish--;
                //                }
                //                if ((intext[v + 1]).ToString() == "⊥")
                //                {
                //                    charStart -= 2;
                //                    charFinish -= 2;
                //                }
                //                v++;
                //            -=

                //            if (charStart == 1)
                //            {
                //                Input.SelectionStart = x + charStart - 1;
                //                Input.SelectionLength = charFinish - charStart + 2;
                //            }
                //            else
                //            {
                //                Input.SelectionStart = x + charStart;
                //                Input.SelectionLength = charFinish - charStart + 1;
                //            }
                //            Input.SelectionBackColor = Color.SeaGreen;
                //            Input.SelectionColor = Color.Red;
                //            break;
                //        }
                //    }
                //}
                //iserror = false;
                return;
            }
            else
            {
                string pattern = "\n";
                string[] substrings = Regex.Split(outputt, pattern);    // Split on hyphens
                string[] result = new string[substrings.Length];
                arraystr = new string[substrings.Length];
                string[] tail = new string[substrings.Length];
                foreach (string match in substrings)
                {
                   
                    string[] substr = Regex.Split(match, "#");
                    if (substr.Length > 1)
                    {
                        result[t] = convert2Lgc(substr[0]);
                        tail[t] = convert2Lgc(substr[1]);
                    }
                    t++;
                }

                Graphics g = CreateGraphics();
                for (int i = 0; i < result.Length; i++)
                {
                    SizeF size = g.MeasureString(result[i], Output.Font);
                    if (maxlength < size.Width)
                        maxlength = size.Width;
                }
                for (int i = 0; i < result.Length; i++)
                {
                    int num_space = 0;
                    SizeF size = g.MeasureString(result[i], Output.Font);
                    if (size.Width <= maxlength)
                    {
                        num_space = (int)((maxlength - size.Width) / 7) + 8;
                        result[i] = result[i] + add_space(num_space) + "\t";
                    }
                }
                for (int i = 0; i < result.Length; i++)
                {
                    arraystr[i] = result[i] + tail[i];
                }
                if (arraystr != null)
                {
                    Output.Text += arraystr[step] + "\n";
                    step++;
                }
                btnProveAll.Enabled = true;
            }
            isclick = true;
            btnStep.Enabled = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            int select = Input.SelectionStart;
            Input.Text = Input.Text.Remove(select, Input.SelectionLength);
            Input.Text = Input.Text.Insert(select, "∀");
            Input.Select(); Input.SelectionStart = select + 1;
        }
        //           ∧  ¬   →      ∨      ┠      ∃   ∀  ⊥
        private void button3_Click(object sender, EventArgs e)
        {
            int select = Input.SelectionStart;
            Input.Text = Input.Text.Remove(select, Input.SelectionLength);
            Input.Text = Input.Text.Insert(select, "∧");
            Input.Select(); Input.SelectionStart = select + 1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int select = Input.SelectionStart;
            Input.Text = Input.Text.Remove(select, Input.SelectionLength);
            Input.Text = Input.Text.Insert(select, "∨");
            Input.Select(); Input.SelectionStart = select + 1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int select = Input.SelectionStart;
            Input.Text = Input.Text.Remove(select, Input.SelectionLength);
            Input.Text = Input.Text.Insert(select, "¬");
            Input.Select(); Input.SelectionStart = select + 1;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int select = Input.SelectionStart;
            Input.Text = Input.Text.Remove(select, Input.SelectionLength);
            Input.Text = Input.Text.Insert(select, "→");
             //= Input.Text.Replace(c, "→");
            Input.Select();
            Input.SelectionStart = select + 1;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int select = Input.SelectionStart;
            Input.Text = Input.Text.Remove(select, Input.SelectionLength);
            Input.Text = Input.Text.Insert(select, "∃");
            Input.Select(); Input.SelectionStart = select + 1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int select = Input.SelectionStart;
            Input.Text = Input.Text.Remove(select, Input.SelectionLength);
            Input.Text = Input.Text.Insert(select, "┠");
            Input.Select(); Input.SelectionStart = select + 1;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int select = Input.SelectionStart;
            Input.Text = Input.Text.Remove(select, Input.SelectionLength);
            Input.Text = Input.Text.Insert(select, "⊥");
            Input.Select(); Input.SelectionStart = select+1;
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmInstruction about = new frmInstruction();
            about.ShowDialog();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEquiv about = new frmEquiv();
            about.ShowDialog();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show(conditions.Count.ToString());
            drawLine(conditions[0], conditions[1]);
        }

        List<int>conditions = new List<int>();
        List<int> temps = new List<int>();
 
        void drawLine(int first, int second)
        {      
                Graphics g = Output.CreateGraphics();
                float height = g.MeasureString("gh", Output.Font).Height;
                float y1 = (first + 1) * height;
                float y2 = (second + 1) * height;
                g.DrawLine(Pens.Red, new PointF(10, y1), new PointF(10, y2));

        }

        private void Output_Validated(object sender, EventArgs e)
        {
            
        }

        
        
        private void clickoutput(object sender, EventArgs e)
        {
            //Input.TextChanged -= new EventHandler(Input_TextChange);
            if (outputt != null && outputt.StartsWith("Unexpected"))
            {
                iserror = true;
                isColored = false;
                Output.Text = convert2Lgc(outputt);
                string[] instr = Regex.Split(outputt, "CharStart:");
                string[] cStart = Regex.Split(instr[1], "\tCharFinish:");
                charStart = Int32.Parse((string)cStart[0]);
                string[] cFinish = Regex.Split(cStart[1], "\tLineStart:");
                charFinish = Int32.Parse((string)cFinish[0]);
                string[] lStart = Regex.Split(cFinish[1], "\tLineFinish:");
                lineStart = Int32.Parse((string)lStart[0]);
                lineFinish = Int32.Parse((string)lStart[1]);
                if (lineFinish == 1)
                {
                    string in_text = Input.Text;
                    char[] intext = in_text.ToCharArray();
                    for (int v = 0; v < charStart - 1; v++)
                    {
                        if ((intext[v]).ToString() == "→" || (intext[v]).ToString() == "┠" || (intext[v]).ToString() == "∀" || (intext[v]).ToString() == "∃")
                        {
                            charStart--;
                            charFinish--;
                        }
                        if ((intext[v]).ToString() == "⊥")
                        {
                            charStart -= 2;
                            charFinish -= 2;
                        }
                    }
                    Input.ScrollToCaret();
                    Input.SelectionStart = charStart - 1;
                    Input.SelectionLength = charFinish - charStart + 1;
                    Input.SelectionBackColor = Color.SeaGreen;
                    Input.SelectionColor = Color.Red;
                }
                else
                {

                    int x;
                    string in_text = Input.Text;
                    char[] intext = in_text.ToCharArray();
                    for (int v = 0; v < in_text.Length; v++)
                    {
                        if ((intext[v]).ToString() == "\n")
                            lineFinish--;

                        if (lineFinish == 1)
                        {
                            x = v;
                            for (int u = 0; u < charStart - 1; u++)
                            {
                                if ((intext[v + 1]).ToString() == "→" || (intext[v + 1]).ToString() == "┠" || (intext[v + 1]).ToString() == "∀" || (intext[v + 1]).ToString() == "∃")
                                {
                                    charStart--;
                                    charFinish--;
                                }
                                if ((intext[v + 1]).ToString() == "⊥")
                                {
                                    charStart -= 2;
                                    charFinish -= 2;
                                }
                                v++;
                            }
                            if (charStart == 1)
                            {
                                Input.SelectionStart = x + charStart - 1;
                                Input.SelectionLength = charFinish - charStart + 2;
                            }
                            else
                            {
                                Input.SelectionStart = x + charStart;
                                Input.SelectionLength = charFinish - charStart + 1;
                            }
                            Input.ScrollToCaret();
                            Input.SelectionBackColor = Color.SeaGreen;
                            Input.SelectionColor = Color.Red;

                            break;
                        }
                    }
                }
                iserror = false;
                isColored = true;
                Output.SelectAll();
                //Input.TextChanged += new EventHandler(Input_TextChange);
                return;
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            RichTextBox dummy = new RichTextBox();
            dummy.AcceptsTab = true;
            dummy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            dummy.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            dummy.Cursor = System.Windows.Forms.Cursors.Default;
            dummy.Font = new System.Drawing.Font("Lucida Sans Unicode", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dummy.HideSelection = false;
            dummy.Location = new System.Drawing.Point(36, 240);
            dummy.Margin = new System.Windows.Forms.Padding(4);
            dummy.Name = "Output";
            dummy.ReadOnly = true;
            dummy.Size = new System.Drawing.Size(1019, 288);
            dummy.TabIndex = 1;
            //dummy.Text = "";
            //dummy.Validated += new System.EventHandler(dummy_Validated);
            //ummy.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            //dummy.Click += new System.EventHandler(this.clickoutput);
            dummy.Text = Input.Text + "\n Bai giai:\n" + Output.Text;
            //dummy.Visible = false;
            this.Controls.Add(dummy);
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "RichTextFormat(*.rtf)|*.rtf";
            if(saveFile.ShowDialog()!= DialogResult.OK)
            {
                return;
            }
            dummy.SaveFile(saveFile.FileName, RichTextBoxStreamType.RichText);
            dummy = null;
           //ResumeLayout();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tùyChọnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOpt.ShowDialog();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Logic File Format(*.lff)|*.lff";
            if (saveFile.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Input.SaveFile(saveFile.FileName, RichTextBoxStreamType.RichText);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Input.SelectAll();
        }

        
    }
}