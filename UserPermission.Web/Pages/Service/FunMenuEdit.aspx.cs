using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserPermission.Utils;
using UserPermission.Model;
using UserPermission.Bll;

namespace UserPermission.Web.Pages.Service
{
    public partial class FunMenuEdit : BasePage
    {
        #region 属性

        public int FmId
        {
            get { return Request.QueryString["fmid"] == null ? 0 : ValidatorHelper.ToInt(Enc.Decrypt(Request.QueryString["fmid"], UrlEncKey), 0); }
        }

        #endregion

        #region 初始

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (FmId > 0)
                {
                    LoadInfo(FmId);
                }
            }

        }

        private void LoadInfo(int nFmId)
        {
            USER_SHARE_COMPANYFUNMODEL usfModel = CompanyFunBusiness.GetCompanyFunModel(nFmId, CompanyId);
            if (usfModel != null)
            {
                txtFMName.Text = usfModel.CFANOTHERNAME;
                txtFMDesc.Text = CommonMethod.FinalString(usfModel.CFDESC);
                //txtFMPageUrl.Text = CommonMethod.FinalString(usfModel.CFPAGEURL);
                txtFMSortNum.Text = usfModel.CFSORTNUM.ToString();
            }
        }

        #endregion

        #region 保存

        protected void btnSave_Click(object sender, EventArgs e)
        {

            USER_SHARE_COMPANYFUNMODEL usfModel = null;
            bool isEdit = FmId > 0;

            //日志记录
            USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
            logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
            logModel.OPERATEDATE = DateTime.Now;
            logModel.OPERATORID = AccountId;
            logModel.PROJECTID = ProjectId;
            logModel.COMPANYID = CompanyId;


            usfModel = CompanyFunBusiness.GetCompanyFunModel(FmId, CompanyId);
            usfModel.CFANOTHERNAME = txtFMName.Text.Trim();
            //usfModel.CFPAGEURL = txtFMPageUrl.Text.Trim();
            usfModel.CFSORTNUM = ValidatorHelper.ToInt(txtFMSortNum.Text, 0);
            usfModel.CFDESC = txtFMDesc.Text.Trim();

            logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.EditCompanyFun.ToString("d"));
            logModel.OPERATECONTENT = "修改公司功能菜单，菜单ID：" + usfModel.CFID + "，修改后菜单名称:" + txtFMName.Text.Trim();


            bool isSuccess = CompanyFunBusiness.UpdateCompanyFun(usfModel, logModel);
            Alert("修改功能菜单" + (isSuccess ? "成功" : "失败，请重试") + "！");

            //刷新父页面
            ExecStartScript(string.Format("parent.location='FunMenuManage.aspx?pid={0}&s={1}';", Enc.Encrypt(usfModel.PROJECTID.ToString(), UrlEncKey), new Random(10000).Next()));
        }

        #endregion
    }
}