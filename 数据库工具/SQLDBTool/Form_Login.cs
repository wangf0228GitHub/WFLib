using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WFNetLib.ADO.DBToolGenerate;

namespace SQLDBTool
{
    public partial class Form_Login : Form
    {
        public Form_Login()
        {
            InitializeComponent();
            textBox1.Text = Properties.Settings.Default.strConn;
            if (Properties.Settings.Default.strType == "sql")
                radioButton2.Checked = true;
            else if (Properties.Settings.Default.strType == "access")
                radioButton1.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.strConn = textBox1.Text;
            Properties.Settings.Default.Save();
            if (radioButton1.Checked)
            {
                DataAccess.Type = "access";
                Properties.Settings.Default.strType = "access";
                Properties.Settings.Default.Save();
            }
            else if (radioButton2.Checked)
            {
                DataAccess.Type = "sql";
                Properties.Settings.Default.strType = "sql";
                Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("请选择数据库类型");
                return;
            }
            DataAccess.strConn = textBox1.Text;
            frmGenerator f = new frmGenerator();
            f.ShowDialog();
        }
    }
}
