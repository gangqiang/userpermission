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
    public partial class RoleManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //项目下拉框
                DataTable dt = CompanyBusiness.GetCompanyProjects(CompanyCode.ToString());
                ControlHelper.BindListControl(ddlProject, dt, "PROJECTNAME", "PROJECTID");
                if (ProjectId > 0)
                {
                    ControlHelper.SelectFlg(ddlProject, ProjectId.ToString());
                }

                lblProject.Visible = ddlProject.Visible = (dt != null && dt.Rows.Count > 0 && ProjectId == 0);
                BindData(0);
            }
        }


        #region 绑定

        private void BindData(int nPageIndex)
        {

            string strWhere = string.Format("  AND R.COMPANYID={0} ", CompanyId);

            strWhere += string.Format(" AND R.PROJECTID={0} ", ddlProject.SelectedValue);


            int nCount = 0;

            //角色名
            if (txtRoleName.Text.Trim().Length > 0)
            {
                strWhere += string.Format(" AND R.ROLENAME ='{0}' ", ValidatorHelper.SafeSql(txtRoleName.Text));
            }

            DataTable dt = RoleBusiness.GetRoleList(nPageIndex, GlobalConsts.PageSize_Default, strWhere, out nCount);
            rptRoleInfo.DataSource = dt;
            rptRoleInfo.DataBind();
            PageBar1.PageIndex = nPageIndex;
            PageBar1.PageSize = GlobalConsts.PageSize_Default;
            PageBar1.RecordCount = nCount;
            PageBar1.Draw();
        }


        protected string GetOperateStauts(string strId, string strStatus)
        {
            string strResult = string.Empty; ;
            strResult = string.Format("<a href=\"###\" id=\"{0}{1}\" onclick=\"StopUse('{0}','{1}');\">{2}</a>", strId, ShareEnum.RoleStatus.Del.ToString("d"), "删除");
            return strResult;
        }

        #endregion

        #region 分页事件

        protected void PageBar_PageChange(object sender, PageChangeEventArgs e)
        {
            BindData(e.PageIndex);
        }

        #endregion

        #region 搜索

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(0);
        }

        #endregion

        #region  操作

        protected void lnkStopUse_Click(object sender, EventArgs e)
        {
            if (hidCId.Value.Trim().Length > 0 && hidStatus.Value.Trim().Length > 0)
            {
                #region 日志记录

                USER_SHARE_LOGMODEL log = new USER_SHARE_LOGMODEL();
                log.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
                log.OPERATECONTENT = "删除角色信息";
                log.OPERATECONTENT += ",角色Id:" + hidCId.Value;
                log.OPERATEDATE = DateTime.Now;
                log.OPERATETYPE = int.Parse(ShareEnum.LogType.DelRole.ToString("d"));
                log.OPERATORID = AccountId;
                log.PROJECTID = ProjectId;

                #endregion

                #region 保存
                if (RoleBusiness.DelRole(hidCId.Value, log))
                {
                    Alert("角色删除成功！");
                    BindData(0);
                }
                else
                {
                    Alert("角色删除失败，请重试！");
                }
                #endregion
            }
        }

        #endregion
    }
}