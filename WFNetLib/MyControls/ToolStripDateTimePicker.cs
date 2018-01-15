using System;
using System.Collections.Generic;

using System.Text;
using System.Windows.Forms;

/************************************************************************/
/* 使用方法                                                             */
/************************************************************************/
//         ToolStripDateTimePicker startTime;
//         ToolStripDateTimePicker endTime;
//         private void Form4_Load(object sender, EventArgs e)
//         {
//             startTime = new ToolStripDateTimePicker();
//             endTime = new ToolStripDateTimePicker();
//             toolStrip1.Items.Insert(4, startTime);
//             toolStrip1.Items.Insert(7, endTime);
//             startTime.Control.Format = DateTimePickerFormat.Custom;
//             startTime.Control.CustomFormat = "yyyy-MM-dd HH:mm:ss";
//             endTime.Control.Format = DateTimePickerFormat.Custom;
//             endTime.Control.CustomFormat = "yyyy-MM-dd HH:mm:ss";
//         }
            //DateTime st = startTime.Value;
            //DateTime et = endTime.Value;
/************************************************************************/
/*                                                                      */
/************************************************************************/
namespace WFNetLib.MyControls
{
    public class ToolStripDateTimePicker : ToolStripControlHost
    {
        public event EventHandler ValueChanged;
        public ToolStripDateTimePicker() 
            : base(new DateTimePicker())
        {

        }
        public ToolStripDateTimePicker(DateTime dt)
            : base(new DateTimePicker())
        {
            Control.Value = dt;
        }
        public new DateTimePicker Control 
        { 
            get 
            { 
                return (DateTimePicker)base.Control; 
            } 
        }
        //create a striong typed Value property
        public DateTime Value
        {
            get 
            { 
                return Control.Value; 
            }
        }

        protected override void OnSubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            ((DateTimePicker)control).ValueChanged += new EventHandler(HandleValueChanged);
        }
    
        protected override void OnUnsubscribeControlEvents(Control control)
        {
            base.OnSubscribeControlEvents(control);
            ((DateTimePicker)control).ValueChanged -= new EventHandler(HandleValueChanged);
        }
        private void HandleValueChanged (object sender, EventArgs e)
        {
            if (ValueChanged != null)
                ValueChanged(this, e);
        }
    }
}

