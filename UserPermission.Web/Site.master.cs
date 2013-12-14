using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using UserPermission.Utils;
using UserPermission.Model;
using UserPermission.Bll;

public partial class SiteMaster : System.Web.UI.MasterPage
{

    //初始化页面控制
    protected void Page_Load(object sender, EventArgs e)
    {
        LoadNavigation();
    }


    private void LoadNavigation()
    {
        #region 导航信息

        NavigationMenu.Items.Clear();

        int nCompanyId = ((BasePage)this.Page).CompanyId;
        int nAccountId = ((BasePage)this.Page).AccountId;
        int nSysProjectId = ValidatorHelper.ToInt(CommonMethod.GetConfigValue("SYSPROJECTID"), 1);
        int nSysCompanyId = ValidatorHelper.ToInt(CommonMethod.GetConfigValue("SYSCOMPANYID"), 1);


        USER_SHARE_ACCOUNTMODEL account = AccountBusiness.GetAccountModel(nAccountId);

        DataTable dt = CompanyFunBusiness.GetAccountFunMenu(nAccountId, account.ISADMIN, nSysProjectId, nCompanyId);
        if (dt != null)
        {
            DataRow[] rows = dt.Select("PROJECTID=" + nSysProjectId, "");
            divNavigation.Visible = nAccountId > 0 && rows.Length > 0;

            MenuItem menu = null;
            foreach (DataRow dr in rows)
            {
                menu = new MenuItem();
                menu.Text = CommonMethod.FinalString(dr["CFANOTHERNAME"]);
                menu.NavigateUrl = ResolveUrl("~/" + CommonMethod.FinalString(dr["CFPAGEURL"]));
                menu.ToolTip = CommonMethod.FinalString(dr["CFDESC"]);
                NavigationMenu.Items.Add(menu);
            }
        }
        else
        {
            ((BasePage)this.Page).Alert("您没有此系统权限！");
            Response.Redirect(ResolveUrl("~/Login.aspx"));
        }


        #endregion

        #region 欢迎信息

        if (this.Page is BasePage && ((BasePage)this.Page).AccountId > 0)
        {

            lblCompany.Text = ((BasePage)this.Page).CompanyName;
            lblWelcome.Text = string.Format("欢迎您，<span class=\"bold\">{0}</span>&nbsp;&nbsp;", ((BasePage)this.Page).RealName);

            lblWelcome.Visible = true;
            lnkLogOut.Visible = true;
        }
        else
        {
            lblWelcome.Visible = false;
            lnkLogOut.Visible = false;
        }

        #endregion
    }

    //注销退出
    protected void lnkLogOut_Click(object sender, EventArgs e)
    {
        HttpCookie ckOut = Request.Cookies["USP"];
        if (ckOut != null)
        {
            ckOut.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(ckOut);
            Response.Redirect(ResolveUrl("~/Login.aspx"));
        }
    }
}
