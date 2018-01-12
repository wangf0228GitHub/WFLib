using System;
using Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;

namespace WFOffice2007
{
// 1.首先需要载入模板
// Report report =new Report();
// report.CreateNewDocument(TemPath); //模板路径
//  
// 2.插入一个值
// report.InsertValue("Bookmark_value","世界杯");//在书签“Bookmark_value”处插入值
//  
// 3.创建一个表格
// Table table =report.InsertTable("Bookmark_table", 2, 3, 0); //在书签“Bookmark_table”处插入2行3列行宽最大的表
//  
// 4.合并单元格
// report.MergeCell(table, 1, 1, 1, 3); //表名,开始行号,开始列号,结束行号,结束列号
//  
// 5.表格添加一行
// report.AddRow(table); //表名
//  
// 6.在单元格中插入值
// report.InsertCell(table, 2, 1,"R2C1");//表名,行号,列号,值
//  
// 7.设置表格中文字的对齐方式
// report.SetParagraph_Table(table, -1, 0);//水平方向左对齐，垂直方向居中对齐
//  
// 8.设置表格字体
// report.SetFont_Table(table,"宋体", 9);//宋体9磅
//  
// 9.给现有的表格添加一行
// report.AddRow(1);//给模板中第一个表格添加一行
//  
// 10.确定现有的表格是否使用边框
// report.UseBorder(1,true); //模板中第一个表格使用实线边框
//  
// 11.给现有的表格添加多行
// report.AddRow(1, 2);//给模板中第一个表格插入2行
//  
// 12.给现有的表格插入一行数据
// string[] values={"英超", "意甲", "德甲","西甲", "法甲" };
// report.InsertCell(1, 2, 5,values); //给模板中第一个表格的第二行的5列分别插入数据
//  
// 13.插入图片
// string picturePath = @"C:\Documents and Settings\Administrator\桌面\1.jpg";
// report.InsertPicture("Bookmark_picture",picturePath, 150, 150); //书签位置，图片路径，图片宽度，图片高度
//  
// 14.插入一段文字
// string text = "长期从事电脑操作者，应多吃一些新鲜的蔬菜和水果，同时增加维生素A、B1、C、E的摄入。为预防角膜干燥、眼干涩、视力下降、甚至出现夜盲等，电 脑操作者应多吃富含维生素A的食物，如豆制品、鱼、牛奶、核桃、青菜、大白菜、空心菜、西红柿及新鲜水果等。";
// report.InsertText("Bookmark_text",text);
//  
// 15.最后保存文档
// report.SaveDocument(RepPath); //文档路径

    public class WordReport
    {
        private _Application wordApp = null;
        private _Document wordDoc = null;
        public _Application Application
        {
            get
            {
                return wordApp;
            }
            set
            {
                wordApp = value;
            }
        }
        public _Document Document
        {
            get
            {
                return wordDoc;
            }
            set
            {
                wordDoc = value;
            }
        }

        //通过模板创建新文档
        public void CreateNewDocument(string filePath)
        {
            killWinWordProcess();
            wordApp = new Application();
            wordApp.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            wordApp.Visible = false;
            object missing = System.Reflection.Missing.Value;
            object templateName = filePath;
            wordDoc = wordApp.Documents.Open(ref templateName, ref missing,
              ref missing, ref missing, ref missing, ref missing, ref missing,
              ref missing, ref missing, ref missing, ref missing, ref missing,
              ref missing, ref missing, ref missing, ref missing);
        }

        //保存新文件
        public void SaveDocument(string filePath)
        {
            object fileName = filePath;
            object format = WdSaveFormat.wdFormatDocument;//保存格式
            object miss = System.Reflection.Missing.Value;
            while(true)
            {
                try
                {
                    wordDoc.SaveAs(ref fileName, ref format, ref miss,
                                  ref miss, ref miss, ref miss, ref miss,
                                  ref miss, ref miss, ref miss, ref miss,
                                  ref miss, ref miss, ref miss, ref miss,
                                  ref miss);
                    break;
                }
                catch (COMException ex)
                {
                    if (System.Windows.Forms.MessageBox.Show("发生错误，错误码为:" + ex.ErrorCode.ToString() + "\r\n请检查" + fileName + "是否已经打开\r\n是否重试", "发生错误", System.Windows.Forms.MessageBoxButtons.RetryCancel) == System.Windows.Forms.DialogResult.Cancel)
                        break;
                }
            }
            //关闭wordDoc，wordApp对象
            object SaveChanges = WdSaveOptions.wdSaveChanges;
            object OriginalFormat = WdOriginalFormat.wdOriginalDocumentFormat;
            object RouteDocument = false;
            wordDoc.Close(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
            wordApp.Quit(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
        }

        //在书签处插入值
        public bool InsertValue(string bookmark, string value)
        {
            object bkObj = bookmark;
            if (wordApp.ActiveDocument.Bookmarks.Exists(bookmark))
            {
                wordApp.ActiveDocument.Bookmarks.get_Item(ref bkObj).Select();
                wordApp.Selection.TypeText(value);
                return true;
            }
            return false;
        }

        //插入表格,bookmark书签
        public Table InsertTable(string bookmark, int rows, int columns, float width)
        {
            object miss = System.Reflection.Missing.Value;
            object oStart = bookmark;
            Range range = wordDoc.Bookmarks.get_Item(ref oStart).Range;//表格插入位置
            Table newTable = wordDoc.Tables.Add(range, rows, columns, ref miss, ref miss);
            //设置表的格式
            newTable.Borders.Enable = 1; //允许有边框，默认没有边框(为0时报错，1为实线边框，2、3为虚线边框，以后的数字没试过)
            newTable.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;//边框宽度
            if (width != 0)
            {
                newTable.PreferredWidth = width;//表格宽度
            }
            newTable.AllowPageBreaks = false;
            return newTable;
        }

        //合并单元格 表名,开始行号,开始列号,结束行号,结束列号
        public void MergeCell(Microsoft.Office.Interop.Word.Table table, int row1, int column1, int row2, int column2)
        {
            table.Cell(row1, column1).Merge(table.Cell(row2, column2));
        }

        //设置表格内容对齐方式Align水平方向，Vertical垂直方向(左对齐，居中对齐，右对齐分别对应Align和Vertical的值为-1,0,1)
        public void SetParagraph_Table(Microsoft.Office.Interop.Word.Table table, int Align, int Vertical)
        {
            switch (Align)
            {
                case -1: table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft; break;//左对齐
                case 0: table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter; break;//水平居中
                case 1: table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight; break;//右对齐
            }
            switch (Vertical)
            {
                case -1: table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalTop; break;//顶端对齐
                case 0: table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter; break;//垂直居中
                case 1: table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalBottom; break;//底端对齐
            }
        }

        //设置表格字体
        public void SetFont_Table(Microsoft.Office.Interop.Word.Table table, string fontName, double size)
        {
            if (size != 0)
            {
                table.Range.Font.Size = Convert.ToSingle(size);
            }
            if (fontName != "")
            {
                table.Range.Font.Name = fontName;
            }
        }

        //是否使用边框,n表格的序号,use是或否
        public void UseBorder(int n, bool use)
        {
            if (use)
            {
                wordDoc.Content.Tables[n].Borders.Enable = 1; //允许有边框，默认没有边框(为0时无边框，1为实线边框，2、3为虚线边框，以后的数字没试过)
            }
            else
            {
                wordDoc.Content.Tables[n].Borders.Enable = 2; //允许有边框，默认没有边框(为0时无边框，1为实线边框，2、3为虚线边框，以后的数字没试过)
            }
        }

        //给表格插入一行,n表格的序号从1开始记
        public void AddRow(int n)
        {
            object miss = System.Reflection.Missing.Value;
            wordDoc.Content.Tables[n].Rows.Add(ref miss);
        }

        //给表格添加一行
        public void AddRow(Microsoft.Office.Interop.Word.Table table)
        {
            object miss = System.Reflection.Missing.Value;
            table.Rows.Add(ref miss);
        }

        //给表格插入rows行,n为表格的序号
        public void AddRow(int n, int rows)
        {
            object miss = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.Table table = wordDoc.Content.Tables[n];
            for (int i = 0; i < rows; i++)
            {
                table.Rows.Add(ref miss);
            }
        }

        //给表格中单元格插入元素，table所在表格，row行号，column列号，value插入的元素
        public void InsertCell(Microsoft.Office.Interop.Word.Table table, int row, int column, string value)
        {
            table.Cell(row, column).Range.Text = value;
        }

        //给表格中单元格插入元素，n表格的序号从1开始记，row行号，column列号，value插入的元素
        public void InsertCell(int n, int row, int column, string value)
        {
            wordDoc.Content.Tables[n].Cell(row, column).Range.Text = value;
        }

        //给表格插入一行数据，n为表格的序号，row行号，columns列数，values插入的值
        public void InsertCell(int n, int row, int columns, string[] values)
        {
            Microsoft.Office.Interop.Word.Table table = wordDoc.Content.Tables[n];
            for (int i = 0; i < columns; i++)
            {
                table.Cell(row, i + 1).Range.Text = values[i];
            }
        }

        //插入图片
        public void InsertPicture(string bookmark, string picturePath, float width, float hight)
        {
            object miss = System.Reflection.Missing.Value;
            object oStart = bookmark;
            object linkToFile = false;    //图片是否为外部链接
            object saveWithDocument = true; //图片是否随文档一起保存
            object range = wordDoc.Bookmarks.get_Item(ref oStart).Range;//图片插入位置
            InlineShape p=wordDoc.InlineShapes.AddPicture(picturePath, ref linkToFile, ref saveWithDocument, ref range);
            p.Width = width;
            p.Height = hight;
//             wordDoc.Application.ActiveDocument.InlineShapes[1].Width = width; //设置图片宽度
//             wordDoc.Application.ActiveDocument.InlineShapes[1].Height = hight; //设置图片高度
        }

        //插入一段文字,text为文字内容
        public void InsertText(string bookmark, string text)
        {
            object oStart = bookmark;
            object range = wordDoc.Bookmarks.get_Item(ref oStart).Range;
            Paragraph wp = wordDoc.Content.Paragraphs.Add(ref range);
            wp.Format.SpaceBefore = 6;
            wp.Range.Text = text;
            wp.Format.SpaceAfter = 24;
            wp.Range.InsertParagraphAfter();
            wordDoc.Paragraphs.Last.Range.Text = "\n";
        }

        //杀掉winword.exe进程
        public void killWinWordProcess()
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName("WINWORD");
            foreach (System.Diagnostics.Process process in processes)
            {
                bool b = process.MainWindowTitle == "";
                if (process.MainWindowTitle == "")
                {
                    process.Kill();
                }
            }
        }
    }
}
