using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WFNetLib.MyControls
{
    public partial class ImageComboBox : ComboBox
    {
        private ImageList imgs = new ImageList();//图片列表

        public ImageList Imgs
        {
            get { return imgs; }
            set { imgs = value; }
        }
        public ImageComboBox()
        {
            InitializeComponent();
            //设置绘制模式
            this.DrawMode = DrawMode.OwnerDrawFixed;
        }
        /// <summary>
        /// 重写绘制ITEM的过程
        /// </summary>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            if (e.Index < 0)
                e.Graphics.DrawString(this.Text, e.Font,
                 new SolidBrush(e.ForeColor), e.Bounds.Left + imgs.ImageSize.Width, e.Bounds.Top);
            else
            {
                //是否ImageComboBoxItem
                if (this.Items[e.Index].GetType() == typeof(wfComboBoxItem))
                {
                    wfComboBoxItem item = (wfComboBoxItem)this.Items[e.Index];

                    //获取颜色和字体
                    Color foreColor = (item.ForeColor != Color.FromKnownColor(KnownColor.Transparent)) ? item.ForeColor : e.ForeColor;
                    Font font = item.Bold ? (new Font(e.Font, FontStyle.Bold)) : e.Font;

                    // -1:没有图标
                    if (item.ImageIndex != -1)
                    {
                        //画图标和文本
                        this.Imgs.Draw(e.Graphics, e.Bounds.Left, e.Bounds.Top, item.ImageIndex);
                        e.Graphics.DrawString(item.ToString(), font, new SolidBrush(foreColor), e.Bounds.Left + imgs.ImageSize.Width, e.Bounds.Top);
                    }
                    else//画文本
                        e.Graphics.DrawString(item.ToString(), font, new SolidBrush(foreColor), e.Bounds.Left + imgs.ImageSize.Width, e.Bounds.Top);
                }
                else
                    e.Graphics.DrawString(this.Items[e.Index].ToString(), e.Font,
                     new SolidBrush(e.ForeColor), e.Bounds.Left + imgs.ImageSize.Width, e.Bounds.Top);
            }
            base.OnDrawItem(e);
        }
    }
}
