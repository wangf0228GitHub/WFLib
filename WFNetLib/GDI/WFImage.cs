using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WFNetLib.GDI
{
    public class WFImage
    {
        /// <summary>
        /// 图片转为Byte字节数组
        /// </summary>
        /// <param name="FilePath">路径</param>
        /// <returns>字节数组</returns>
        public static byte[] imageToByteArray(string FilePath)
        {
            using (MemoryStream ms = new MemoryStream())
            {

                using (Image imageIn = Image.FromFile(FilePath))
                {

                    using (Bitmap bmp = new Bitmap(imageIn))
                    {
                        bmp.Save(ms, imageIn.RawFormat);
                    }

                }
                return ms.ToArray();
            }
        }
        /// <summary>
        /// 字节数组生成图片
        /// </summary>
        /// <param name="Bytes">字节数组</param>
        /// <returns>图片</returns>
        public static Image byteArrayToImage(byte[] Bytes)
        {
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                Image outputImg = Image.FromStream(ms);
                return outputImg;
            }
        }
        public static void Image_FillRect(Bitmap SourceBmp, Bitmap TargetBmp, FillMode _FillMode)
        {
            try
            {
                switch (_FillMode)
                {
                    case FillMode.Title://平铺
                        using (TextureBrush Txbrus = new TextureBrush(SourceBmp))
                        {
                            Txbrus.WrapMode = WrapMode.Tile;
                            using (Graphics G = Graphics.FromImage(TargetBmp))
                            {
                                G.FillRectangle(Txbrus, new Rectangle(0, 0, TargetBmp.Width - 1, TargetBmp.Height - 1));
                            }
                        }

                        break;
                    case FillMode.Center://居中
                        using (Graphics G = Graphics.FromImage(TargetBmp))
                        {
                            int xx = (TargetBmp.Width - SourceBmp.Width) / 2;
                            int yy = (TargetBmp.Height - SourceBmp.Height) / 2;
                            G.DrawImage(SourceBmp, new Rectangle(xx, yy, SourceBmp.Width, SourceBmp.Height), new Rectangle(0, 0, SourceBmp.Width, SourceBmp.Height), GraphicsUnit.Pixel);
                        }

                        break;
                    case FillMode.Struk://拉伸
                        using (Graphics G = Graphics.FromImage(TargetBmp))
                        {
                            G.DrawImage(SourceBmp, new Rectangle(0, 0, TargetBmp.Width, TargetBmp.Height), new Rectangle(0, 0, SourceBmp.Width, SourceBmp.Height), GraphicsUnit.Pixel);
                        }

                        break;
                    case FillMode.Zoom://缩放
                        double tm = 0.0;
                        int W = SourceBmp.Width;
                        int H = SourceBmp.Height;
                        if (W > TargetBmp.Width)
                        {
                            tm = TargetBmp.Width / SourceBmp.Width;
                            W = W * (int)tm;
                            H = H * (int)tm;
                        }
                        if (H > TargetBmp.Height)
                        {
                            tm = TargetBmp.Height / H;
                            W = W * (int)tm;
                            H = H * (int)tm;
                        }
                        using (Bitmap tmpBP = new Bitmap(W, H))
                        {
                            using (Graphics G2 = Graphics.FromImage(tmpBP))
                            {
                                G2.DrawImage(SourceBmp, new Rectangle(0, 0, W, H), new Rectangle(0, 0, SourceBmp.Width, SourceBmp.Height), GraphicsUnit.Pixel);
                                using (Graphics G = Graphics.FromImage(TargetBmp))
                                {
                                    int xx = (TargetBmp.Width - W) / 2;
                                    int yy = (TargetBmp.Height - H) / 2;
                                    G.DrawImage(tmpBP, new Rectangle(xx, yy, W, H), new Rectangle(0, 0, W, H), GraphicsUnit.Pixel);
                                }
                            }
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public enum FillMode
    {
        /// <summary>
        /// 平铺
        /// </summary>
        /// <remarks></remarks>
        Title = 0,
        /// <summary>
        /// 居中
        /// </summary>
        /// <remarks></remarks>
        Center = 1,
        /// <summary>
        /// 拉伸
        /// </summary>
        /// <remarks></remarks>
        Struk = 2,
        /// <summary>
        /// 缩放
        /// </summary>
        /// <remarks></remarks>
        Zoom = 3
    }
}
