using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;

namespace WFNetLib.MyControls
{
    public class TextBoxOnlyNumber : TextBox
    {
        private bool bFloat = false;

        public bool BFloat
        {
            get { return bFloat; }
            set { bFloat = value; }
        }
        public TextBoxOnlyNumber() 
        {

        }
        public TextBoxOnlyNumber(bool bfloat)
        {
            bFloat = bfloat;
        }
        protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
        {
            e.Handled = true;
            if (Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8 )
            {
                e.Handled = false;
            }
            else if (bFloat)
            {
                if (e.KeyChar == '.' && this.Text.IndexOf('.')==-1)
                    e.Handled = false;
            }
        } 
      
    }
}

