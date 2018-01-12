using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WFNetLib.Forms
{
    public partial class InputBox : Form
    {
        public InputBox(string _txtData)
        {
            InitializeComponent();
            txtData.Text = _txtData;
        }
        //对键盘进行响应

        private void txtData_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {

                this.Close();

            }

            else if (e.KeyCode == Keys.Escape)
            {

                txtData.Text = string.Empty;

                this.Close();

            }

        }

        //显示InputBox
        public static string ShowInputBox(string Title,string _txtData)
        {
            return ShowInputBox(Title, "", _txtData);
        }
        public static string ShowInputBox(string Title, string keyInfo,string _txtData)
        {

            InputBox inputbox = new InputBox(_txtData);

            inputbox.Text = Title;

            if (keyInfo.Trim() != string.Empty)

                inputbox.lblInfo.Text = keyInfo;

            inputbox.ShowDialog();

            return inputbox.txtData.Text;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
    }
}
