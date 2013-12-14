using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using UserPermission.Bll;
using UserPermission.Utils;
using UserPermission.Model;

namespace UserPermission.Web.Pages.Service
{
    public partial class FunMenuManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //项目下拉框
                DataTable dt = CompanyBusiness.GetCompanyProjects(CompanyCode.ToString());
                ControlHelper.BindListControl(ddlProject, dt, "PROJECTNAME", "PROJECTID");
                ddlProject.Items.Remove(ddlProject.Items.FindByValue(CommonMethod.GetConfigValue("SYSPROJECTID")));
                if (ProjectId > 0)
                {
                    ControlHelper.SelectFlg(ddlProject, ProjectId.ToString());
                }
                if (Request.QueryString["pid"] != null)
                {
                    ControlHelper.SelectFlg(ddlProject, Enc.Decrypt(Request.QueryString["pid"], UrlEncKey));
                }
                if (ddlProject.Items.Count > 1 && ProjectId == 0) //不是自动登录并且开通项目大于1个
                {
                    ExecStartScript("$('#sProject').show();");
                }
                else
                {
                    lblTip.Visible = true;
                }

            }
        }
    }
}