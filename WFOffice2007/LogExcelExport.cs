using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace WFOffice
{
    public class LogExcelExport
    {
        private DataGridView dgv;
        private ExcelExport ExcelEx;
        public LogExcelExport(DataGridView d)
        {
            dgv = d;
            ExcelEx = new ExcelExport(dgv.Rows.Count);
            ExcelEx.ExcelWorkbookCallbackProc = new ExcelExport.ExcelWorkbookCallback(ExcelWorkbookCallbackProc);
        }
        public void Proc()
        {
            if (dgv.Rows.Count > 65530)
            {
                MessageBox.Show("条目过多，无法导出，请重新选择索引条件");
                return;
            }
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("当前没有显示任何内容，无法导出");
                return;
            }
            ExcelEx.ExcelExportProc();
        }
        private bool ExcelWorkbookCallbackProc(Workbook wBook, int sheetIndex, int itemIndex)
        {
            Worksheet wSheet;
            wSheet = (Worksheet)wBook.Worksheets[1];
            Range dr;
            if(itemIndex==-1)
            {                
                for (int i = 0; i < wBook.Worksheets.Count - 1; i++)
                {
                    wSheet = (Worksheet)wBook.Worksheets[i + 1];
                    wSheet.Delete();
                }
                wSheet = (Worksheet)wBook.Worksheets[1];
                wSheet.Name = "系统日志";
                wSheet.Cells[1, 1] = "编号";
                wSheet.Cells[1, 2] = "类型";
                wSheet.Cells[1, 3] = "内容";
                wSheet.Cells[1, 4] = "备注";
                wSheet.Cells[1, 5] = "操作员";
                wSheet.Cells[1, 6] = "时间";                
                dr = wSheet.get_Range("A1", "F1");
                dr.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.DarkOrange);
                dr.Interior.Pattern = XlPattern.xlPatternSolid;
            }
            else if(itemIndex==int.MaxValue)
            {
                dr = wSheet.get_Range("A1", "F" + (dgv.Rows.Count + 1).ToString());
                dr.Columns.AutoFit();
                dr.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                dr.Borders.LineStyle = XlLineStyle.xlContinuous;
            }
            else
            {
//                 SystemLogData log = SystemLogDataFactory.Construct(dgv, index);
//                 wSheet.Cells[2 + index, 1] = log.ID.ToString();
//                 wSheet.Cells[2 + index, 2] = log.LogType;
//                 wSheet.Cells[2 + index, 3] = log.LogContent;
//                 wSheet.Cells[2 + index, 4] = log.LogRemark;
//                 wSheet.Cells[2 + index, 5] = log.Operator;
//                 wSheet.Cells[2 + index, 6] = log.AddTime.ToString();
//                 if (index % 2 == 1)
//                 {
//                     dr = wSheet.get_Range("A" + (2 + index).ToString(), "F" + (2 + index).ToString());
//                     dr.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
//                     dr.Interior.Pattern = XlPattern.xlPatternSolid;
//                 }
            }
            return true;
        }
    }
}
