using ProjectC.Entity;
using ProjectC.ProcessHandling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ProjectC
{
    public partial class Form1 : Form
    {
        string wrong = null;
        public Form1()
        {
            InitializeComponent();


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog2.ShowDialog() != DialogResult.Cancel)
                this.txtProgram.Text = this.folderBrowserDialog2.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.folderBrowserDialog1.ShowDialog() != DialogResult.Cancel)
                this.txtTestCase.Text = this.folderBrowserDialog1.SelectedPath;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.folderBrowserDialog2.SelectedPath = Application.ExecutablePath;
            this.folderBrowserDialog1.SelectedPath = Application.ExecutablePath;
            this.pictureBox1.Image = Image.FromFile(@"..\..\Image\average.jpg");
           // this.pictureBox1.Image = Image.FromFile(@"C:\Users\ACER\OneDrive\Desktop\ProjectC\ProjectC\ProjectC\average.jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.Image = Image.FromFile(@"..\..\Image\Exit.png");
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            linkLabel1.Hide();
            //this.Resiz = false;
            //linkLabel1.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtProgram.Text.Trim() == "" || txtTestCase.Text.Trim() == "")
            {
                MessageBox.Show("txtProgram/txtTestCase is empty!!! Lets input", "Warning");
            }
            else
            {
                int ques, numberOfRight = 0; ;
                List<TestCase> listTestCase = new List<TestCase>();

                for (ques = 1; ques < 6; ques++)
                {
                    listTestCase = GradeHandle.loadTestCase(this.txtTestCase.Text + @"\Q" + ques + " Testcases");
                    string programPath = this.txtProgram.Text;
                    float num = 0;
                    foreach (TestCase current in listTestCase)
                    {
                        Answer answer = GradeHandle.Grade(programPath, current, this.txtTestCase.Text, ques, ref wrong);
                        num += answer.Mark;

                    }

                    if (num == 2)
                    {
                        /*lblNoti.Text = "Pass";
                        lblNoti.ForeColor = Color.Green;*/
                        //Thread.Sleep(1000);
                        numberOfRight++;
                    }
                }

                lblNoti.Text = "DONE";
                lblNoti.ForeColor = Color.Green;
                lblCorrect.Text = "Number of correct answer: " + numberOfRight + "/5";
                lblCorrect.Font = new Font(lblCorrect.Font.FontFamily, 15, FontStyle.Bold);
                lblGrade.Text = "" + (numberOfRight * 2);
                if (numberOfRight == 5)
                {
                    /*                    wrong = "Nothing Wrong!!!Congratulation";
                    */
                }
                else
                {
                    linkLabel1.Show();
                }
            }
        }

        private void txtProgram_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //this.Hide();
            ShowWrongAnswer show = new ShowWrongAnswer(wrong);

            show.ShowDialog();
            //this.Close();
        }
    }
}
