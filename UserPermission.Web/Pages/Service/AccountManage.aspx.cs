using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserPermission.Model;
using UserPermission.Bll;
using UserPermission.Utils;
using System.Data;

namespace UserPermission.Web.Pages.Service
{
    public partial class AccountManage : BasePage
    {

        #region 初始

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //账号状态
                ControlHelper.ListContolDataBindFromEnum(ddlAccountStatus, typeof(ShareEnum.AccountStatus), "所有", "", "");
                ddlAccountStatus.Items.Remove(ddlAccountStatus.Items.FindByValue(ShareEnum.AccountStatus.Del.ToString("d")));
                BindData(0);
            }
        }

        #endregion

        #region 绑定

        private void BindData(int nPageIndex)
        {
            string strWhere = string.Format(" AND COMPANYID={0} ", CompanyId);
            int nCount = 0;

            //账号名
            if (txtAccountName.Text.Trim().Length > 0)
            {
                strWhere += string.Format(" AND ACCOUNTNAME ='{0}' ", ValidatorHelper.SafeSql(txtAccountName.Text));
            }

            // 姓名

            if (txtRealName.Text.Trim().Length > 0)
            {
                strWhere += string.Format(" AND REALNAME='{0}' ", ValidatorHelper.SafeSql(txtRealName.Text));
            }

            //状态
            if (ddlAccountStatus.SelectedValue.Trim().Length > 0)
            {
                strWhere += string.Format(" AND STATUS={0} ", ddlAccountStatus.SelectedValue);
            }

            DataTable dt = AccountBusiness.GetAccountList(nPageIndex, GlobalConsts.PageSize_Default, strWhere, out nCount);
            rptAccountInfo.DataSource = dt;
            rptAccountInfo.DataBind();
            PageBar1.PageIndex = nPageIndex;
            PageBar1.PageSize = GlobalConsts.PageSize_Default;
            PageBar1.RecordCount = nCount;
            PageBar1.Draw();
        }


        protected string GetOperateStauts(string strId, string strStatus, string strIsAdmin)
        {
            string strResult = string.Empty; ;
            if (!strIsAdmin.Equals("1"))
            {
                strResult = string.Format("<a href=\"###\" id=\"{0}{1}\" onclick=\"StopUse('{0}','{1}');\">{2}</a>", strId, ShareEnum.AccountStatus.Del.ToString("d"), "删除");
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
                log.OPERATECONTENT = "删除账号信息";
                log.OPERATECONTENT += ",账号Id:" + hidCId.Value;
                log.OPERATEDATE = DateTime.Now;
                log.OPERATETYPE = int.Parse(ShareEnum.LogType.DelAccount.ToString("d"));
                log.OPERATORID = AccountId;
                log.PROJECTID = ProjectId;

                #endregion

                #region 保存
                if (AccountBusiness.DelAccount(hidCId.Value, log))
                {
                    Alert("删除成功！");
                    BindData(0);
                }
                else
                {
                    Alert("删除失败，请重试！");
                }
                #endregion
            }
        }

        #endregion

    }
}