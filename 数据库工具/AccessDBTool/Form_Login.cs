using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AccessDBTool
{
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.strConn;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.strConn = textBox1.Text;
            Properties.Settings.Default.Save();
            DataAccess.strConn = textBox1.Text;
            frmGenerator f=new frmGenerator();
            f.ShowDialog();
        }
    }
}
