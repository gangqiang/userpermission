using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public delegate void PageChangeDelegate(object sender, PageChangeEventArgs e);

public partial class UC_PageBar : System.Web.UI.UserControl, IPostBackEventHandler
{
    /// <summary>
    /// 当页面改变时触发
    /// </summary>
    public event PageChangeDelegate PageChange;

    /// <summary>
    /// 实现IPostBackEventHandler.RaisePostBackEvent方法
    /// </summary>
    /// <param name="eventArgument"></param>
    void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
    {
        if (PageChange != null)
        {
            int pageIndex;
            if (!int.TryParse(eventArgument, out pageIndex))
                pageIndex = 0;

            PageIndex = pageIndex;

            PageChangeEventArgs pageEventArgs = new PageChangeEventArgs();
            pageEventArgs.PageIndex = pageIndex;
            PageChange(new object(), pageEventArgs);

            //Draw();
        }
    }

    private int m_PageSize = 10;
    /// <summary>
    /// 每页大小
    /// </summary>
    public int PageSize
    {
        get { return m_PageSize; }
        set { m_PageSize = value; }
    }


    private int m_RecordCount = 0;
    /// <summary>
    /// 总记录数
    /// </summary>
    public int RecordCount
    {
        get { return m_RecordCount; }
        set { m_RecordCount = value; }
    }


    /// <summary>
    /// 获取页面总数
    /// </summary>
    public int PageCount
    {
        get { return RecordCount % PageSize == 0 ? RecordCount / PageSize : (int)Math.Ceiling(RecordCount * 1.0M / PageSize); }
    }

    /// <summary>
    /// 当前页
    /// </summary>
    public int PageIndex
    {
        get
        {
            if (ViewState["PageIndex"] != null)
            {
                return (int)ViewState["PageIndex"];
            }
            else
            {
                return 0;
            }
        }
        set
        {
            ViewState["PageIndex"] = value;
        }
    }


    private string m_CmdPreviousText = "上一页";
    /// <summary>
    /// 上一页按钮文本
    /// </summary>
    public string CmdPreviousText
    {
        get { return m_CmdPreviousText; }
        set { m_CmdPreviousText = value; }
    }


    private string m_CmdNextText = "下一页";
    /// <summary>
    /// 下一页按钮文本
    /// </summary>
    public string CmdNextText
    {
        get { return m_CmdNextText; }
        set { m_CmdNextText = value; }
    }


    /// <summary>
    /// 创建客户端分页控件
    /// </summary>
    public void Draw()
    {
        int pageCount = PageCount;

        string pageStr = "";

        int pageStart = 0;
        if (PageIndex > 5)
            pageStart = PageIndex - 5;
        int pageEnd = pageStart + 11;

        if (PageIndex > 0)
            pageStr += "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, "0") + "\">首页</a> ";

        if (PageIndex > 0)
            pageStr += "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, (PageIndex - 1).ToString()) + "\">" + CmdPreviousText + "</a> ";

        if (pageStart > 0)
            pageStr += "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, (pageStart - 1).ToString()) + "\">...</a> ";

        for (int i = 0; i < pageCount; i++)
        {
            if (i >= pageStart && i < pageEnd)
            {
                if (PageIndex != i)
                    pageStr += "<a class=\"pagelistnormal\" title=\"转到第" + (i + 1) + "页\" href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, i.ToString()) + "\">" + (i + 1).ToString() + "</a> ";
                else
                    pageStr += "<span class=\"pagelistselected\">" + (i + 1).ToString() + "</span> ";
            }
        }

        if (pageEnd < pageCount)
            pageStr += "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, pageEnd.ToString()) + "\">...</a> ";

        if (PageIndex < pageCount - 1)
            pageStr += "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, (PageIndex + 1).ToString()) + "\">" + CmdNextText + "</a> ";

        if (PageIndex < pageCount - 1)
            pageStr += "<a href=\"javascript:" + Page.ClientScript.GetPostBackEventReference(this, (pageCount - 1).ToString()) + "\">末页</a> ";

        pageCtrl.InnerHtml = pageStr + "共" + PageCount.ToString() + "页" + RecordCount.ToString() + "条记录";
    }
}
