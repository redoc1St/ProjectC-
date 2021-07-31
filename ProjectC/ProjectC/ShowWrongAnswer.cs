using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectC
{
    public partial class ShowWrongAnswer : Form
    {
        public ShowWrongAnswer(string wrong)
        {
            InitializeComponent();
            rtbWrong.Text = wrong;
        }

        

        private void ShowWrongAnswer_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            
        }
    }
}
