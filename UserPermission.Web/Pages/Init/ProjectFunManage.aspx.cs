using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using UserPermission.Bll;
using UserPermission.Utils;
using UserPermission.Model;

namespace UserPermission.Web.Pages.Init
{
    public partial class ProjectFunManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int nCount;
                DataTable dt = ProjectBusiness.GetProjectList(0, int.MaxValue, " AND STATUS=" + ShareEnum.ProjectStatus.Normal.ToString("d"), out nCount);
                ControlHelper.BindListControl(ddlProject, dt, "ProjectName", "ProjectId");
                foreach (ListItem item in ddlProject.Items)
                {
                    item.Value = Enc.Encrypt(item.Value, UrlEncKey);
                }
                if (CommonMethod.FinalString(Request.QueryString["pid"]).Length > 0)
                {
                    ControlHelper.SelectFlg(ddlProject, Request.QueryString["pid"]);
                }
            }
        }
    }
}