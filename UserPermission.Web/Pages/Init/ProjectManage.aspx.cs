using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserPermission.Utils;
using UserPermission.Model;
using UserPermission.Bll;
using System.Data;
namespace UserPermission.Web.Pages
{
    public partial class ProjectManage : BasePage
    {
        #region 初始
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //项目状态绑定
                ControlHelper.ListContolDataBindFromEnum(ddlStauts, typeof(ShareEnum.ProjectStatus), "所有", "", "");
                BindData(0);
            }
        }
        #endregion

        #region 搜索

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData(0);
        }

        private void BindData(int nPageIndex)
        {
            string strWhere = string.Empty;
            int nCount = 0;

            if (txtProjectName.Text.Trim().Length > 0)
            {
                strWhere += string.Format(" AND PROJECTNAME LIKE '%{0}%' ", ValidatorHelper.SafeSql(txtProjectName.Text));
            }
            if (ddlStauts.SelectedValue.Trim().Length > 0)
            {
                strWhere += string.Format(" AND STATUS={0} ", ddlStauts.SelectedValue);
            }

            DataTable dt = ProjectBusiness.GetProjectList(nPageIndex, GlobalConsts.PageSize_Default, strWhere, out nCount);
            rptProjectInfo.DataSource = dt;
            rptProjectInfo.DataBind();
            PageBar1.RecordCount = nCount;
            PageBar1.PageIndex = nPageIndex;
            PageBar1.PageSize = GlobalConsts.PageSize_Default;
            PageBar1.Draw();
        }

        protected string GetOperateStr(string strId, string strStatus)
        {
            string strResult = "<a href=\"###\" id=\"{0}{1}\" onclick=\"StopUse('{0}','{1}');\">{2}</a>";
            if (strStatus.Trim().Equals(ShareEnum.ProjectStatus.Normal.ToString("d")))
            {
                strResult = string.Format(strResult, strId, ShareEnum.ProjectStatus.StopUse.ToString("d"), "停用");
            }
            if (strStatus.Trim().Equals(ShareEnum.ProjectStatus.StopUse.ToString("d")))
            {
                strResult = string.Format(strResult, strId, ShareEnum.ProjectStatus.Normal.ToString("d"), "恢复使用");
            }
            return strResult;
        }

        #endregion

        #region 分页事件

        protected void PageBar_PageChange(object sender, PageChangeEventArgs e)
        {
            BindData(e.PageIndex);
        }

        #endregion

        #region 恢复和停止使用操作

        protected void lnkStopUse_Click(object sender, EventArgs e)
        {
            if (hidProjectId.Value.Trim().Length > 0 && hidStatus.Value.Trim().Length > 0)
            {
                #region 日志记录
                USER_SHARE_LOGMODEL log = new USER_SHARE_LOGMODEL();
                log.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
                log.OPERATECONTENT = "更改项目状态为:" + EnumPlus.GetEnumDescription(typeof(ShareEnum.ProjectStatus), hidStatus.Value);
                log.OPERATECONTENT += ",项目Id:" + hidProjectId.Value;
                log.OPERATEDATE = DateTime.Now;
                log.OPERATETYPE = int.Parse(ShareEnum.LogType.ChangeStatus.ToString("d"));
                log.OPERATORID = AccountId;
                log.PROJECTID = ProjectId;
                #endregion

                #region 保存
                if (ProjectBusiness.UpdateProjectStatus(hidProjectId.Value, hidStatus.Value, log))
                {
                    Alert("操作成功！");
                    BindData(0);
                }
                else
                {
                    Alert("操作失败，请重试！");
                }
                #endregion
            }
        }

        #endregion
    }
}