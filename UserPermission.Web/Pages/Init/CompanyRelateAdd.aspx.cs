using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserPermission.Bll;
using UserPermission.Model;
using UserPermission.Utils;
using System.Data;

namespace UserPermission.Web.Pages.Init
{
    public partial class CompanyRelateAdd : BasePage
    {
        #region  初始

        public string Cid
        {
            get { return Request.QueryString["cid"] == null ? string.Empty : Enc.Decrypt(Request.QueryString["cid"], UrlEncKey); }
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //公司类型
                ControlHelper.ListContolDataBindFromEnum(ddlCompanyType, typeof(ShareEnum.CompanyType), "", "", "");

                //修改时预先加载原先信息
                if (Cid.Length > 0)
                {
                    LoadOriInfo(Cid);
                }

                //项目和产品
                BindProduct();

            }
        }

        private void LoadOriInfo(string strCid)
        {
            if (strCid.Length > 0)
            {
                USER_SHARE_COMPANYRELATEMODEL uscrModel = CompanyBusiness.GetModelByCid(ValidatorHelper.ToInt(strCid, 0));
                if (uscrModel != null)
                {
                    txtCompanyName.Text = uscrModel.COMPANYNAME;
                    ControlHelper.SelectFlg(ddlCompanyType, uscrModel.COMPANYTYPE.ToString());
                    hidCompanyId.Value = uscrModel.COMPANYID > 0 ? uscrModel.COMPANYID.ToString() : uscrModel.SHARECOMPANYID.ToString();
                    hidGroupId.Value = CommonMethod.FinalString(uscrModel.GROUPID);
                    hidGroupIdn.Value = CommonMethod.FinalString(uscrModel.GROUPIDN);
                    hidProjects.Value = CommonMethod.FinalString(uscrModel.PROJECTIDS);
                    hidProducts.Value = CommonMethod.FinalString(uscrModel.PRODUCTIDS);
                }
                else
                {
                    Response.Write("不存在此公司信息！");
                    Response.End();
                }
            }
        }

        #region 项目和产品
        private void BindProduct()
        {
            int nCount = 0;
            DataTable dt = ProjectBusiness.GetProjectList(0, int.MaxValue, " AND STATUS=" + ShareEnum.ProjectStatus.Normal.ToString("d"), out nCount);
            if (dt != null)
            {
                dlProject.DataSource = dt;
                dlProject.DataBind();
                dlProject.RepeatColumns = Convert.ToInt32(dt.Rows.Count / 5);
            }
        }

        protected void dlProject_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater rptProduct = (Repeater)e.Item.FindControl("rptProduct");
                HiddenField hidProjectId = (HiddenField)e.Item.FindControl("hidProjectId");
                if (rptProduct != null)
                {
                    int nCount = 0;
                    rptProduct.DataSource = ProductBusiness.GetProductList(0, int.MaxValue, " AND D.PROJECTID=" + hidProjectId.Value + " AND PRODUCTFLAG=" + ShareEnum.ProductFlag.Normal.ToString("d"), out nCount);
                    rptProduct.DataBind();
                }
            }
        }

        protected string GetChecked(string strProjectId, string strProductId)
        {
            string strIds = (strProductId.Length > 0 ? hidProducts.Value : hidProjects.Value);
            string strId = (strProductId.Length > 0 ? strProductId : strProjectId);
            string strResult = string.Empty;
            if (Cid.Length > 0)//修改
            {
                strResult = strIds.IndexOf("," + strId + ",") >= 0 ? "checked=\"checked\"" : "";
                //if (strProductId.Length > 0 && hidProjects.Value.IndexOf("," + strProjectId + ",") >= 0)
                //{
                //    strResult += "disabled = \"disabled\"";
                //}
            }

            return strResult;
        }

        #endregion

        #endregion


        #region 保存

        protected void btnSave_Click(object sender, EventArgs e)
        {

            #region 基本信息

            USER_SHARE_COMPANYRELATEMODEL uscrModel = null;

            //日志记录
            USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
            logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
            logModel.OPERATEDATE = DateTime.Now;
            logModel.OPERATORID = AccountId;
            logModel.PROJECTID = ProjectId;
            logModel.COMPANYID = CompanyId;



            #region 产品

            string strProjectIds = CommonMethod.FinalString(Request.Form["project"]);
            string strProductIds = CommonMethod.FinalString(Request.Form["ppfun"]);

            if (strProjectIds.Length > 0)
            {
                strProjectIds = "," + strProjectIds + ",";
            }

            if (strProductIds.Length > 0)
            {
                strProductIds = "," + strProductIds + ",";
            }

            if (strProjectIds.Length == 0 || strProductIds.Length == 0)
            {
                Alert("请选择公司开通的项目和产品！");
                return;
            }

            #endregion

            //新增

            if (Cid.Length == 0)
            {
                uscrModel = new USER_SHARE_COMPANYRELATEMODEL();
                uscrModel.CID = CommonBusiness.GetSeqID("S_USER_SHARE_COMPANYRELATE");
                uscrModel.STATUS = int.Parse(ShareEnum.CompanyRelateStatus.Normal.ToString("d"));
                uscrModel.CREATEDATE = DateTime.Now;
                uscrModel.COMPANYCODE = CompanyBusiness.GetCompanyCode();
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.AddCompanyRelate.ToString("d"));
                logModel.OPERATECONTENT = "新增公司注册关联信息,公司名称:" + txtCompanyName.Text.Trim();
            }

            //修改

            else
            {
                uscrModel = CompanyBusiness.GetModelByCid(ValidatorHelper.ToInt(Cid, 0));
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.EditCompanyRelate.ToString("d"));
                logModel.OPERATECONTENT = "修改公司注册关联信息,公司名称:" + txtCompanyName.Text.Trim();
            }

            uscrModel.COMPANYNAME = txtCompanyName.Text.Trim();
            uscrModel.COMPANYTYPE = int.Parse(ddlCompanyType.SelectedValue);
            if (uscrModel.COMPANYTYPE != int.Parse(ShareEnum.CompanyType.ShareCompany.ToString("d")))
            {
                uscrModel.COMPANYID = ValidatorHelper.ToInt(hidCompanyId.Value, 0);
                uscrModel.SHARECOMPANYID = 0;
            }
            else
            {
                uscrModel.SHARECOMPANYID = ValidatorHelper.ToInt(hidCompanyId.Value, 0);
                uscrModel.COMPANYID = 0;
            }

            uscrModel.GROUPID = hidGroupId.Value;
            uscrModel.GROUPIDN = hidGroupIdn.Value;
            uscrModel.PROJECTIDS = strProjectIds;
            uscrModel.PRODUCTIDS = strProductIds;

            if (Cid.Length == 0)
            {
                if (CompanyBusiness.AddCompanyRelate(uscrModel, logModel))
                {
                    Alert("注册成功！");
                }
                else
                {
                    Alert("注册失败，请重试！");
                }
            }
            else
            {
                if (CompanyBusiness.UpdateCompanyRelate(uscrModel, logModel))
                {
                    Alert("修改成功！");
                }
                else
                {
                    Alert("操作失败，请重试！");
                }
            }

            ExecScript("parent.__doPostBack('ctl00$MainContent$btnSearch','');");

            #endregion


        }

        #endregion
    }
}