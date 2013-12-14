using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.HtmlControls;
using UserPermission.Utils;


/// <summary>
///BasePage 的摘要说明
/// </summary>
public class BasePage : System.Web.UI.Page
{
    public BasePage()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }


    private int _accountid = 0;
    /// <summary>
    /// 当前登录人ID
    /// </summary>
    public int AccountId
    {
        get
        {
            HttpCookie ck = HttpContext.Current.Request.Cookies["USP"];
            if (ck != null && ck.Values["AccountId"] != null)
            {
                _accountid = ValidatorHelper.ToInt(ck.Values["AccountId"], 0);
            }
            return _accountid;

        }
        set
        {
            _accountid = value;
        }
    }

    /// <summary>
    /// 真实姓名
    /// </summary>
    private string _realname = "";
    public string RealName
    {
        get
        {
            HttpCookie ck = HttpContext.Current.Request.Cookies["USP"];
            if (ck != null && ck.Values["RealName"] != null)
            {
                _realname = Server.UrlDecode(ValidatorHelper.FinalString(ck.Values["RealName"]));
            }
            return _realname;

        }
        set
        {
            _realname = value;
        }
    }


    private int _projectid = 0;

    /// <summary>
    /// 项目ID
    /// </summary>
    public int ProjectId
    {
        get
        {
            HttpCookie ck = HttpContext.Current.Request.Cookies["USP"];
            if (ck != null && ck.Values["ProjectId"] != null)
            {
                _projectid = ValidatorHelper.ToInt(ck.Values["ProjectId"], 0);
            }
            return _projectid;

        }

    }


    private int _companyid = 0;

    /// <summary>
    /// 公司ID
    /// </summary>
    public int CompanyId
    {
        get
        {
            HttpCookie ck = HttpContext.Current.Request.Cookies["USP"];
            if (ck != null && ck.Values["CompanyId"] != null)
            {
                _companyid = ValidatorHelper.ToInt(ck.Values["CompanyId"], 0);
            }
            return _companyid;

        }
        set
        {
            _companyid = value;
        }
    }

    /// <summary>
    /// 公司名称
    /// </summary>
    private string _companyname = "";
    public string CompanyName
    {
        get
        {
            HttpCookie ck = HttpContext.Current.Request.Cookies["USP"];
            if (ck != null && ck.Values["CompanyName"] != null)
            {
                _companyname = Server.UrlDecode(ValidatorHelper.FinalString(ck.Values["CompanyName"]));
            }
            return _companyname;

        }
        set
        {
            _companyname = value;
        }
    }

    /// <summary>
    /// 公司名称
    /// </summary>
    private string _groupid = "";
    public string GroupId
    {
        get
        {
            HttpCookie ck = HttpContext.Current.Request.Cookies["USP"];
            if (ck != null && ck.Values["GroupId"] != null)
            {
                _groupid = Server.UrlDecode(ValidatorHelper.FinalString(ck.Values["GroupId"]));
            }
            return _groupid;

        }
        set
        {
            _groupid = value;
        }
    }

    /// <summary>
    ///公司编码
    /// </summary>
    private int _companycode = 0;
    public int CompanyCode
    {
        get
        {
            HttpCookie ck = HttpContext.Current.Request.Cookies["USP"];
            if (ck != null && ck.Values["CompanyCode"] != null)
            {
                _companycode = ValidatorHelper.ToInt(ck.Values["CompanyCode"], 0);
            }
            return _companycode;

        }
        set
        {
            _companycode = value;
        }
    }



    public string UrlEncKey
    {
        get { return CommonMethod.GetConfigValue("URLENCKEY"); }
    }


    #region 页面加载之前执行

    #region Page_PreInit方法

    protected virtual void Page_PreInit()
    {
        //验证用户登录
        if (!(AccountId > 0))
        {
            string script =
                     "<script type=\"text/javascript\">" +
                     "var target;" +
                     "try {" +
                     "   target = parent.location.host == window.location.host ? parent : window;" +
                     "} catch (ex) {" +
                     "   target = window;" +
                     "}" +
                     "target.location='" + ResolveClientUrl("~/Login.aspx") + "?source=' + encodeURIComponent(target.location);" +
                     "</script>";
            Response.Write(script);
            Response.End();
        }
    }
    #endregion

    protected virtual void Page_PreLoad()
    {
        Page.Title = Page.Title + "--" + CompanyName + "权限管理平台";

        if (this.Master == null) //如果页面套用母版页，不需要再加载样式文件
        {
            HtmlGenericControl meta = new HtmlGenericControl("meta");
            meta.Attributes["http-equiv"] = "X-UA-Compatible";
            meta.Attributes["content"] = "IE=EmulateIE7";
            Header.Controls.AddAt(0, meta);
            GenerateCss("Resource/Styles/Site.css", 1);
            GenerateCss("Resource/Scripts/My97DatePicker/skin/WdatePicker.css", 2);
            GenerateScript("Resource/Scripts/jquery-1.7.1.min.js", 3);
            GenerateScript("Resource/Scripts/My97DatePicker/WdatePicker.js", 4);
            GenerateScript("Resource/Scripts/TZWidget.js", 5);
            GenerateCss("Resource/Styles/validator.css", 6);
            GenerateScript("Resource/Scripts/formValidator_min.js", 7);
            GenerateScript("Resource/Scripts/formValidatorRegex.js", 8);

            HtmlGenericControl css = new HtmlGenericControl("style");
            css.Attributes["type"] = "text/css";
            css.InnerText = "body{ background: #FFF;}";
            Header.Controls.AddAt(9, css);
        }
    }

    #region Meta,Script,Css 标签

    private void GenerateScript(string src, int index)
    {
        HtmlGenericControl script = new HtmlGenericControl("script");
        script.Attributes["type"] = "text/javascript";
        script.Attributes["language"] = "javascript";
        script.Attributes["src"] = src.IndexOf("http") >= 0 ? src : ResolveUrl("~/" + src);
        Header.Controls.AddAt(index, script);
    }

    private void GenerateCss(string src, int index)
    {
        HtmlLink css = new HtmlLink();
        css.Attributes["type"] = "text/css";
        css.Attributes["href"] = ResolveUrl("~/" + src);
        css.Attributes["rel"] = "stylesheet";
        Header.Controls.AddAt(index, css);
    }

    #endregion

    #endregion

    /// <summary>
    /// 执行客户端脚本
    /// </summary>
    /// <param name="script"></param>
    public void ExecScript(string script)
    {
        ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">" + script + "</script>");
    }

    public void ExecStartScript(string script)
    {
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">" + script + "</script>");
    }

    public void Alert(string content)
    {
        ExecScript("alert(\"" + content + "\")");
    }

    public void ClosePopWin()
    {
        ExecStartScript("setTimeout('Tw.Win.Close()',1000)");
    }

    public void Refresh()
    {
        ExecScript("parent.location=parent.location;");
    }

    /// <summary>
    /// 选中一个客户端控件
    /// </summary>
    /// <param name="id">控件ID</param>
    public void Select(string id)
    {
        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">try{document.getElementById('" + id + "').select()}catch(e){alert(e.description);}</script>");
    }

    /// <summary>
    /// 选中一个服务端控件
    /// </summary>
    /// <param name="c">控件ID</param>
    public void Select(System.Web.UI.Control c)
    {
        Select(c.ClientID);
    }

}