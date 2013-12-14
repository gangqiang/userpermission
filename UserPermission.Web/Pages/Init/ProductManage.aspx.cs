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
    public partial class ProductManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 项目绑定
                int nCount;
                DataTable dt = ProjectBusiness.GetProjectList(0, int.MaxValue, " AND STATUS=" + ShareEnum.ProjectStatus.Normal.ToString("d"), out nCount);
                ControlHelper.BindListControl(ddlProject, dt, "ProjectName", "ProjectId");
                ddlProject.Items.Insert(0, new ListItem("不限", ""));
                #endregion

                //产品状态绑定
                ControlHelper.ListContolDataBindFromEnum(ddlStauts, typeof(ShareEnum.ProductFlag), "所有", "", "");

                BindData(0);
            }
        }

        #region

        private void BindData(int nPageIndex)
        {
            string strWhere = string.Empty;
            int nCount = 0;

            if (ddlProject.SelectedValue.Trim().Length > 0)
            {
                strWhere += string.Format(" AND D.PROJECTID={0} ", ddlProject.SelectedValue);
            }
            if (txtProductName.Text.Trim().Length > 0)
            {
                strWhere += string.Format(" AND D.PRODUCTNAME='{0}' ", ValidatorHelper.SafeSql(txtProductName.Text.Trim()));
            }
            if (ddlStauts.SelectedValue.Trim().Length > 0)
            {
                strWhere += string.Format(" AND D.PRODUCTFLAG={0} ", ddlStauts.SelectedValue);
            }

            DataTable dt = ProductBusiness.GetProductList(nPageIndex, GlobalConsts.PageSize_Default, strWhere, out nCount);
            rptProductInfo.DataSource = dt;
            rptProductInfo.DataBind();
            PageBar1.PageSize = GlobalConsts.PageSize_Default;
            PageBar1.PageIndex = nPageIndex;
            PageBar1.RecordCount = nCount;
            PageBar1.Draw();
        }

        protected string GetOperateStr(string strId, string strStatus)
        {
            string strResult = "<a href=\"###\" id=\"{0}{1}\" onclick=\"StopUse('{0}','{1}');\">{2}</a>";
            if (strStatus.Trim().Equals(ShareEnum.ProductFlag.Normal.ToString("d")))
            {
                strResult = string.Format(strResult, strId, ShareEnum.ProductFlag.StopUse.ToString("d"), "停用");
            }
            if (strStatus.Trim().Equals(ShareEnum.ProductFlag.StopUse.ToString("d")))
            {
                strResult = string.Format(strResult, strId, ShareEnum.ProductFlag.Normal.ToString("d"), "恢复使用");
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

        #region 恢复和停止使用操作

        protected void lnkStopUse_Click(object sender, EventArgs e)
        {
            if (hidProductId.Value.Trim().Length > 0 && hidStatus.Value.Trim().Length > 0)
            {
                #region 日志记录

                USER_SHARE_LOGMODEL log = new USER_SHARE_LOGMODEL();
                log.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
                log.OPERATECONTENT = "更改产品状态为:" + EnumPlus.GetEnumDescription(typeof(ShareEnum.ProductFlag), hidStatus.Value);
                log.OPERATECONTENT += ",产品Id:" + hidProductId.Value;
                log.OPERATEDATE = DateTime.Now;
                log.OPERATETYPE = int.Parse(ShareEnum.LogType.ChangeProductStatus.ToString("d"));
                log.OPERATORID = AccountId;
                log.PROJECTID = ProjectId;

                #endregion

                #region 保存
                if (ProductBusiness.UpdateProductStatus(hidProductId.Value, hidStatus.Value, log))
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