using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserPermission.Model;
using UserPermission.Bll;
using UserPermission.Utils;
using System.Data;

namespace UserPermission.Web.Pages.Init
{
    public partial class CompanyRelateManage : BasePage
    {
        #region 初始

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ControlHelper.ListContolDataBindFromEnum(ddlCompanyType, typeof(ShareEnum.CompanyType), "所有", "", "");
                BindData(0);
            }
        }

        private void BindData(int nPageIndex)
        {
            string strWhere = string.Empty;
            int nCount = 0;
            if (txtCompanyName.Text.Trim().Length > 0)
            {
                strWhere += string.Format(" AND COMPANYNAME LIKE '%{0}%' ", ValidatorHelper.SafeSql(txtCompanyName.Text));
            }
            if (ddlCompanyType.SelectedValue.Trim().Length > 0)
            {
                strWhere += string.Format(" AND COMPANYTYPE={0} ", ddlCompanyType.SelectedValue);
            }

            DataTable dt = CompanyBusiness.GetCompanyRelateList(nPageIndex, GlobalConsts.PageSize_Default, strWhere, out nCount);
            rptCompanyRelateInfo.DataSource = dt;
            rptCompanyRelateInfo.DataBind();
            PageBar1.PageIndex = nPageIndex;
            PageBar1.PageSize = GlobalConsts.PageSize_Default;
            PageBar1.RecordCount = nCount;
            PageBar1.Draw();
        }

        protected string GetOperateStauts(string strId, string strStatus, string strAdminId, string strCode)
        {
            string strResult = "<a href=\"###\" id=\"{0}{1}\" onclick=\"StopUse('{0}','{1}');\">{2}</a>";
            if (strStatus.Trim().Equals(ShareEnum.ProjectStatus.Normal.ToString("d")))
            {
                strResult = string.Format(strResult, strId, ShareEnum.CompanyRelateStatus.StopUse.ToString("d"), "停用");
            }
            if (strStatus.Trim().Equals(ShareEnum.ProjectStatus.StopUse.ToString("d")))
            {
                strResult = string.Format(strResult, strId, ShareEnum.CompanyRelateStatus.Normal.ToString("d"), "恢复使用");
            }
            return strResult;
        }

        protected string GetAdminStr(string strAdminId, string strAccount, string strCode)
        {
            string strResult = string.Format(" <a href=\"###\" title=\"{3}\" name=\"account\" id=\"{0},{1}\">{2}</a>", Enc.Encrypt(strAdminId, UrlEncKey),
               Enc.Encrypt(strCode, UrlEncKey), strAdminId.Equals("0") ? "未开通" : strAccount, strAdminId.Equals("0") ? "点击开通初始账号" : "点击修改账号信息");


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

        #region 停用和恢复使用

        protected void lnkStopUse_Click(object sender, EventArgs e)
        {
            if (hidCId.Value.Trim().Length > 0 && hidStatus.Value.Trim().Length > 0)
            {
                #region 日志记录

                USER_SHARE_LOGMODEL log = new USER_SHARE_LOGMODEL();
                log.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
                log.OPERATECONTENT = "更改公司关联表状态为:" + EnumPlus.GetEnumDescription(typeof(ShareEnum.CompanyRelateStatus), hidStatus.Value);
                log.OPERATECONTENT += ",公司关联表CId:" + hidCId.Value;
                log.OPERATEDATE = DateTime.Now;
                log.OPERATETYPE = int.Parse(ShareEnum.LogType.ChangeCompanyRelateStatus.ToString("d"));
                log.OPERATORID = AccountId;
                log.PROJECTID = ProjectId;

                #endregion

                #region 保存
                if (CompanyBusiness.UpdateCompanyRelateStatus(hidCId.Value, hidStatus.Value, log))
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