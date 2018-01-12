using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace WFWebLib
{
    public static partial class GridViewOperation
    {
        public static void GridRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                // PageSize 
                GridView grid = sender as GridView;

                // Format The Align
                Table tPager = e.Row.Cells[0].Controls[0] as Table;
                tPager.Height = new Unit(22);
                tPager.Rows[0].Height = new Unit(22);

                #region Change PageNumber Text
                bool blnFirst = false;
                foreach (TableCell tc in tPager.Rows[0].Cells)
                {
                    Control c = tc.Controls[0];
                    if (c is LinkButton)
                    {
                        int i = tPager.Rows[0].Cells.GetCellIndex(tc);
                        LinkButton lb = c as LinkButton;
                        if (lb.Text == "...")
                        {
                            lb.Font.Name = "webdings";
                            if (i == 1 && blnFirst)
                            {
                                lb.ToolTip = "Previous Page";
                                lb.Text = "7";
                                continue;
                            }
                            else
                            {
                                lb.ToolTip = "Next Page";
                                lb.Text = "8";
                                continue;
                            }
                        }

                        if (lb.Text == grid.PagerSettings.LastPageText)
                        {
                            lb.Font.Name = "webdings";
                            lb.ToolTip = "Last Page";
                            lb.Text = ":";
                            continue;
                        }
                        if (lb.Text == grid.PagerSettings.FirstPageText)
                        {
                            blnFirst = true;
                            lb.Font.Name = "webdings";
                            lb.ToolTip = "First Page";
                            lb.Text = "9";
                            continue;
                        }
                    }
                    if (c is Label)
                    {
                        (c as Label).Text = string.Format("[{0}]", (c as Label).Text);
                        (c as Label).ForeColor = System.Drawing.Color.Red;
                    }
                }
                #endregion

                #region Label Page Info
                int PageCount = grid.PageCount;
                int CurrentPageNumber = grid.PageIndex + 1;

                Label l = new Label();
                l.ForeColor = System.Drawing.Color.Black;
                if (grid.DataSource != null)
                {
                    int iCount = 0;
                    if (grid.DataSource is DataView)
                        iCount = (grid.DataSource as DataView).Count;
                    if (grid.DataSource is DataTable)
                        iCount = (grid.DataSource as DataTable).Rows.Count;

                    string strTemp = "总计{2}条记录，当前页数：{0}/{1}，每页{3}条记录";
                    l.Text = string.Format(strTemp, CurrentPageNumber, PageCount, iCount, grid.PageSize);
                    grid.Attributes["my_Paper_00x"] = l.Text;
                }
                else
                    l.Text = grid.Attributes["my_Paper_00x"];

                TableCell tdInfo = new TableCell();
                tPager.Rows[0].Cells.AddAt(0, tdInfo);
                tdInfo.Width = new Unit("100%");
                tdInfo.Controls.AddAt(0, l);
                #endregion
            }
        }

        /// <summary>
        /// 获得排序字符串。
        /// </summary>
        /// <param name="grid">GridView 对象。</param>
        /// <param name="sortExpression">要排序的字段名称。e.SortExpression 属性值。</param>
        /// <param name="bindType"></param>
        /// <returns>返回拼装好的排序字符串。</returns>
        public static string GetSortExpression(GridView grid, string sortExpression, BindPagingGridViewType bindType)
        {
            string m_sortExpression = "SortExpression";

            // strTemp 不为空表示上次排序就是按照这个字段排序的。
            string strTemp = grid.Attributes[m_sortExpression];
            if (string.IsNullOrEmpty(strTemp))
                strTemp = string.Empty;

            switch (bindType)
            {
                case BindPagingGridViewType.Sorting:
                    if (string.Compare(strTemp, sortExpression, true) == 0 || strTemp.ToLower().IndexOf(sortExpression) > -1)
                    {
                        // 相同一列点击第二次。
                        if (strTemp.ToLower().IndexOf("desc") > -1)
                            sortExpression = strTemp.ToLower().Replace(" desc", string.Empty);
                        else
                            sortExpression = string.Format("{0} desc", sortExpression);
                    }
                    break;
                case BindPagingGridViewType.Paging:
                    if (!string.IsNullOrEmpty(strTemp))
                        sortExpression = strTemp;
                    break;
                case BindPagingGridViewType.Searching:
                    break;
            }

            grid.Attributes[m_sortExpression] = sortExpression;
            return sortExpression;
        }

        /// <summary>
        /// 绑定 GridView 数据，并且导向到指定页码。
        /// </summary>
        /// <param name="grid">GridView 控件。</param>
        /// <param name="dt">DataTable 数据源。</param>
        /// <returns></returns>
        public static bool BindPagingGridView(GridView grid, DataTable dt)
        {
            return BindPagingGridView(grid, dt.DefaultView);
        }

        /// <summary>
        /// 绑定 GridView 数据，并且导向到指定页码。
        /// </summary>
        /// <param name="grid">GridView 控件。</param>
        /// <param name="dv">DataView 数据源。</param>
        /// <returns></returns>
        public static bool BindPagingGridView(GridView grid, DataView dv)
        {
            return BindPagingGridView(grid, dv, 0, string.Empty);
        }

        /// <summary>
        /// 绑定 GridView 数据，并且导向到指定页码。
        /// </summary>
        /// <param name="grid">GridView 控件。</param>
        /// <param name="dv">DataView 数据源。</param>
        /// <param name="pageIndex">默认指定页码。</param>
        /// <returns></returns>
        public static bool BindPagingGridView(GridView grid, DataView dv, int pageIndex)
        {
            return BindPagingGridView(grid, dv, pageIndex, string.Empty);
        }

        /// <summary>
        /// 绑定 GridView 数据，并且导向到指定页码。
        /// </summary>
        /// <param name="grid">GridView 控件。</param>
        /// <param name="dv">DataView 数据源。</param>
        /// <param name="pageIndex">默认指定页码。</param>	
        /// <param name="sortExpression">排序字段。</param>
        /// <returns></returns>
        public static bool BindPagingGridView(GridView grid, DataView dv, int pageIndex, string sortExpression)
        {
            return BindPagingGridView(grid, dv, pageIndex, sortExpression, true);
        }
        /// <summary>
        /// 绑定 GridView 数据，并且导向到指定页码。
        /// </summary>
        /// <param name="grid">GridView 控件。</param>
        /// <param name="dv">DataView 数据源。</param>
        /// <param name="pageIndex">默认指定页码。</param>	
        /// <param name="sortExpression">排序字段。</param>
        /// <param name="supportSortViewState">是否支持表头排序双击。（第一次正序，第二次倒序。）</param>
        /// <returns></returns>
        public static bool BindPagingGridView(GridView grid, DataView dv, int pageIndex, string sortExpression, bool supportSortViewState)
        {
            return BindPagingGridView(grid, dv, pageIndex, sortExpression, supportSortViewState, BindPagingGridViewType.Paging);
        }
        /// <summary>
        /// 绑定 GridView 数据调用方式。
        /// </summary>
        public enum BindPagingGridViewType
        {
            /// <summary>
            /// 第一次查找时调用。
            /// </summary>
            Searching,
            /// <summary>
            /// 分页时调用。
            /// </summary>
            Paging,
            /// <summary>
            /// 排序时调用。
            /// </summary>
            Sorting,
        }
        /// <summary>
        /// 绑定 GridView 数据，并且导向到指定页码。
        /// </summary>
        /// <param name="grid">GridView 控件。</param>
        /// <param name="dv">DataView 数据源。</param>
        /// <param name="pageIndex">默认指定页码。</param>	
        /// <param name="sortExpression">排序字段。</param>
        /// <param name="supportSortViewState">是否支持记录上一次排序表达式。（第一次正序，第二次倒序。）</param>
        /// <param name="bindType">枚举类型参数，指定此次调用是翻页调用还是排序调用。</param>
        /// <returns></returns>
        /// <remarks>如果支持记录上次排序表达式，当指定调用方式为排序掉用是，就支持了表头双击了。如果指定调用方式翻页调用则翻页时能够保持排序。</remarks>
        public static bool BindPagingGridView(GridView grid, DataView dv, int pageIndex, string sortExpression, bool supportSortViewState, BindPagingGridViewType bindType)
        {
            if (grid == null)
                return false;

            bool blnRet = false;
            try
            {
                if (grid.AllowPaging)
                {
                    if (dv.Count > 0)
                        grid.PageIndex = Math.Min(pageIndex, (int)Math.Ceiling(dv.Count / (double)grid.PageSize) - 1);
                    else
                        grid.PageIndex = 0;
                }

                if (pageIndex < 0)
                    pageIndex = 0;

                if (supportSortViewState)
                    dv.Sort = GetSortExpression(grid, sortExpression, bindType);
                else if (!string.IsNullOrEmpty(sortExpression))
                    dv.Sort = sortExpression;

                grid.DataSource = dv;
                grid.DataBind();
                blnRet = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return blnRet;
        }
        public static void AllCheckGridView(GridView grid, CheckBox allcb)
        {
            foreach (GridViewRow gv in grid.Rows)
            {
                CheckBox cd = (CheckBox)gv.FindControl("GridView_SingleCheck");
                cd.Checked = allcb.Checked;
            }           
        }
        public static string CheckedKeysGridView(GridView grid, int Column)
        {
            StringBuilder sb = new StringBuilder();
            foreach (GridViewRow gv in grid.Rows)
            {
                CheckBox cd = (CheckBox)gv.FindControl("GridView_SingleCheck");
                if(cd.Checked)
                {
                   sb.Append(gv.Cells[Column].Text);
                   sb.Append(",");
                }
            }
            if (sb.Length == 0)
                return "";
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
        public static void AddConfirmGridView(GridViewRowEventArgs e,int cellid,string txt)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((ImageButton)e.Row.Cells[cellid].Controls[0]).Attributes.Add("onclick", "if(!window.confirm('"+txt+"')) return;");               
            }  

        }
    }
}
