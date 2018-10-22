using System;
using System.Windows.Forms;

using System.Data.OleDb;
using System.Data;
using Microsoft.Office.Interop.Excel;
using WFNetLib;

namespace WFOffice2007
{
    public class ExcelExport
    {        
        public int Count = 0;        
        WaitingProc wp;
        public int SheetIndex;
        public int SheetCount;
        public delegate bool ExcelWorkbookCallback(Workbook wBook,int sheetIndex,int itemIndex);
        public ExcelWorkbookCallback ExcelWorkbookCallbackProc;
        public ExcelExport(int count,int sheetCount)
        {
            Count = count;
            SheetCount = sheetCount;
        }
        public ExcelExport(int count)
        {
            Count = count;
            SheetCount = 1;
        }
        public ExcelExport()
        {
            Count = -1;
            SheetCount = 1;
        }
        public void ExcelExportProc()
        {
            if (Count > 65530)
            {
                MessageBox.Show("条目过多，无法导出");
                return;
            }
            if (Count == 0)
            {
                MessageBox.Show("没有任何内容，无法导出");
                return;
            }
            WaitingProcFunc wpf = new WaitingProcFunc(Proc);
            wp = new WFNetLib.WaitingProc();
            string strTitle = "数据导出到Excel中";
            SheetIndex = 1;
            wp.Execute(wpf, strTitle, WFNetLib.WaitingType.With_ConfirmCancel, "确定要取消么？");
        }
        private void Proc(object LockWatingThread)
        {            
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            try
            {
                app.Visible = false;
                Workbook wBook = app.Workbooks.Add(true);
                Worksheet wSheet;
                for (int i = 0; i < wBook.Worksheets.Count - 1; i++)
                {
                    wSheet = (Worksheet)wBook.Worksheets[i + 1];
                    wSheet.Delete();
                }
                for (int i = 0; i < SheetCount - 1; i++)
                {
                    wBook.Worksheets.Add();
                }
                wp.SetCursorStyle(Cursors.Default);
                int percent = 0;
                wp.SetProcessBar(percent);
                while(SheetIndex<=SheetCount)
                {                 
                    ExcelWorkbookCallbackProc(wBook, SheetIndex, -1);//用于回调函数执行列标题定义操作                     
                    if (Count == -1)
                    {
                        int itemIndex=0;
                        while (true)
                        {
                            percent++;
                            if (percent == 10000)
                                percent = 0;
                            wp.SetProcessBar(percent / 100);
                            lock (LockWatingThread)
                            {
                                if (!ExcelWorkbookCallbackProc(wBook, SheetIndex, itemIndex++))
                                {                                    
                                    break;
                                }
                                if (itemIndex > 65530)
                                {
//                                     MessageBox.Show("条目过多，只导出前65530条");
//                                     break;
                                    break;
                                }
                                
                            }
                            if (wp.HasBeenCancelled())
                            {
                                app.DisplayAlerts = false;
                                app.Quit();
                                return;
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < Count; i++)
                        {
                            percent++;
                            wp.SetProcessBar(percent * 100 / Count/SheetCount);
                            lock (LockWatingThread)
                            {
                                if (!ExcelWorkbookCallbackProc(wBook, SheetIndex, i))
                                {
                                    break;
                                }
                            }
                            if (wp.HasBeenCancelled())
                            {
                                app.DisplayAlerts = false;
                                app.Quit();
                                return;
                            }
                        }
                    }                    
                    ExcelWorkbookCallbackProc(wBook,SheetIndex,int.MaxValue); //全部导出完成，用于回调函数执行整体界面设定
                    SheetIndex++;
                }
                wp.SetProcessBar(100);
                app.Visible = true;
                app.WindowState = XlWindowState.xlMaximized;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("导出Excel出错！错误原因" + ex.Message);
                app.DisplayAlerts = false;
                app.Quit();
                return;
            }
        }
    }
    public class ExcelImport
    {
        bool bSelfOpen = true;
        //WaitingProc wp;
        OpenFileDialog openFileDialog;
        string SheetName;
        public ExcelImport(bool _bSelfopen,string sheetName)
        {
            bSelfOpen = _bSelfopen;
            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xls;*.xlsx)|*.xls;*.xlsx";
            openFileDialog.ReadOnlyChecked = true;
            openFileDialog.Title = "指定要导入的Excel文件";
            SheetName = sheetName;
        }
        public System.Data.DataTable ExcelImportProc_OleDB(bool hasTitle = false)//true:第一行为标题，不导入;false:第一行也导入
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string strConn;
                    //bool IS_EXCEL_2007 = false;
                    if(hasTitle)
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog.FileName + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1'";
                    else
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + openFileDialog.FileName + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1'";
                    if (System.IO.Path.GetExtension(openFileDialog.FileName).ToUpper() == ".XLSX")
                    {
                        if (hasTitle)
                            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + openFileDialog.FileName + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
                        else
                            strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + openFileDialog.FileName + ";Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1\"";
                        //IS_EXCEL_2007 = true;
                    }  
                    OleDbConnection OleConn = new OleDbConnection(strConn);
                    OleConn.Open();
                    OleDbCommand myOleDbCommand = new OleDbCommand("select * from [" + SheetName + "$]", OleConn);
                    OleDbDataAdapter dataAda = new OleDbDataAdapter(myOleDbCommand);                    
                    DataSet dsExcel = new DataSet();
                    dataAda.Fill(dsExcel, "[" + SheetName + "$]");
                    System.Data.DataTable dt = dsExcel.Tables[0];
                    OleConn.Close();
                    OleConn.Dispose();
                    return dt;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show("数据绑定Excel失败!失败原因：" + Ex.Message, "提示信息",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return null;
                } 
            }
            else
                return null;
        }
    }
}
