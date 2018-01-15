using System;
using System.Collections.Generic;

using System.Text;
using System.Drawing;

namespace WFNetLib.MyControls
{
    public class wfComboBoxItem
    {
        private string _Text = "";
        private Color foreColor = Color.FromKnownColor(KnownColor.Transparent);       
        private bool bold = false;        
        private object _Value;
        private int imageIndex = -1;

        public int ImageIndex
        {
            get { return imageIndex; }
            set { imageIndex = value; }
        }

        public object Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
        public wfComboBoxItem(string _text, object _value)
        {
            _Text = _text;
            _Value = _value;
        }
        public wfComboBoxItem(string _text, object _value,int _imageIndex)
        {
            _Text = _text;
            _Value = _value;
            ImageIndex = _imageIndex;
        }

        public override string ToString()
        {
            return _Text;
        }
        public bool Bold
        {
            get { return bold; }
            set { bold = value; }
        }
        public Color ForeColor
        {
            get { return foreColor; }
            set { foreColor = value; }
        }
    }
}
