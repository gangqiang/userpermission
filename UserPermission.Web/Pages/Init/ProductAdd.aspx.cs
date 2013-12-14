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
    public partial class ProductAdd : BasePage
    {
        #region 属性

        public int ProductId
        {
            get { return Request.QueryString["id"] == null ? 0 : ValidatorHelper.ToInt(Enc.Decrypt(Request.QueryString["id"], UrlEncKey), 0); }
        }

        #endregion

        #region 初始

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                #region 项目绑定

                int nCount;
                DataTable dt = ProjectBusiness.GetProjectList(0, int.MaxValue, " AND STATUS=" + ShareEnum.ProjectStatus.Normal.ToString("d"), out nCount);
                ControlHelper.BindListControl(ddlProjects, dt, "ProjectName", "ProjectId");

                #endregion

                //修改
                if (ProductId > 0)
                {
                    hidId.Value = ProductId.ToString();
                    LoadProductInfo();
                }
            }
        }

        //加载原有信息
        private void LoadProductInfo()
        {
            USER_SHARE_PRODUCTMODEL uspModel = ProductBusiness.GetProductModel(ProductId);
            if (uspModel != null)
            {
                txtProductName.Text = uspModel.PRODUCTNAME;
                txtProductDesc.Text = uspModel.PRODUCTDESC;
                ControlHelper.SelectFlg(ddlProjects, uspModel.PROJECTID.ToString());
            }
            else
            {
                Response.Write("不存在此产品信息！");
                Response.End();
            }
        }

        #endregion

        #region 保存

        protected void btnSave_Click(object sender, EventArgs e)
        {

            #region 验证

            if (txtProductName.Text.Trim().Length == 0)
            {
                Alert("请填写产品名称！");
                Select(txtProductName);
                return;
            }

            if (CommonMethod.FinalString(Request.Form["fun"]).Length == 0)
            {
                Alert("请选择产品拥有的功能！");
                return;
            }

            #endregion

            #region 基础信息和日志记录

            USER_SHARE_PRODUCTMODEL uspModel = null;

            //日志记录
            USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
            logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
            logModel.OPERATEDATE = DateTime.Now;
            logModel.OPERATORID = AccountId;
            logModel.PROJECTID = ProjectId;
            logModel.COMPANYID = CompanyId;

            //修改
            if (ProductId > 0)
            {
                uspModel = ProductBusiness.GetProductModel(ProductId);
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.EditProduct.ToString("d"));
                logModel.OPERATECONTENT = string.Format("修改产品信息,产品Id:{0},修改后的名称：{1}", ProductId, txtProductName.Text.Trim());
            }

            //新增
            else
            {
                uspModel = new USER_SHARE_PRODUCTMODEL();
                uspModel.PRODUCTID = CommonBusiness.GetSeqID("S_USER_SHARE_PRODUCT");
                uspModel.PRODUCTFLAG = int.Parse(ShareEnum.ProductFlag.Normal.ToString("d"));
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.AddProduct.ToString("d"));
                logModel.OPERATECONTENT = string.Format("新增产品信息,产品Id:{0}, 名称：{1}", uspModel.PRODUCTID, txtProductName.Text.Trim());
            }

            uspModel.PRODUCTNAME = txtProductName.Text.Trim();
            uspModel.PROJECTID = ValidatorHelper.ToInt(ddlProjects.SelectedValue, 0);
            uspModel.PRODUCTDESC = txtProductDesc.Text.Trim();

            #endregion

            #region 产品功能

            string strFunIds = CommonMethod.FinalString(Request.Form["fun"]);
            List<USER_SHARE_PRODUCTFUNMODEL> lstModels = new List<USER_SHARE_PRODUCTFUNMODEL>();
            if (strFunIds.Length > 0)
            {

                USER_SHARE_PRODUCTFUNMODEL funModel = null;
                string[] funs = strFunIds.Split(',');
                foreach (string funid in funs)
                {
                    funModel = new USER_SHARE_PRODUCTFUNMODEL();
                    funModel.PROCUTID = uspModel.PRODUCTID;
                    funModel.FUNID = ValidatorHelper.ToInt(funid, 0);
                    lstModels.Add(funModel);
                }
            }
            else
            {
                Alert("请先选择此产品包含的功能！");
                return;
            }

            #endregion

            #region 保存

            bool isSuccess = false;
            string strTip = ProductId > 0 ? "修改" : "新增";

            if (ProductId > 0)
            {
                isSuccess = ProductBusiness.EditProduct(uspModel, lstModels, logModel);
            }
            else
            {
                isSuccess = ProductBusiness.AddProduct(uspModel, lstModels, logModel);
            }

            if (isSuccess)
            {
                Alert(strTip + "产品成功！");
                ExecScript("parent.__doPostBack('ctl00$MainContent$btnSearch','');");
            }
            else
            {
                Alert(strTip + "产品失败，请重试！");
            }

            #endregion

        }

        #endregion


    }
}