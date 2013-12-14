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
    public partial class ProjectFunAdd : BasePage
    {
        #region 属性

        public int FmId
        {
            get { return Request.QueryString["fmid"] == null ? 0 : ValidatorHelper.ToInt(Enc.Decrypt(Request.QueryString["fmid"], UrlEncKey), 0); }
        }

        public string ActionType
        {
            get { return CommonMethod.FinalString(Request.QueryString["type"]); }
        }

        #endregion

        #region 初始

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (FmId > 0)
                {
                    //修改菜单
                    if (ActionType.Equals("edit"))
                    {
                        LoadInfo(FmId);
                    }

                }

            }

        }

        private void LoadInfo(int nFmId)
        {
            USER_SHARE_FUNMENUMODEL usfModel = ProjectFunBusiness.GetModel(nFmId);
            if (usfModel != null)
            {
                txtFMName.Text = usfModel.FMNAME;
                txtFMDesc.Text = CommonMethod.FinalString(usfModel.FMDESC);
                txtFMPageUrl.Text = CommonMethod.FinalString(usfModel.FMPAGEURL);
                txtFMSortNum.Text = usfModel.FMSORTNUM.ToString();
            }
        }

        #endregion

        #region 保存

        protected void btnSave_Click(object sender, EventArgs e)
        {
            USER_SHARE_FUNMENUMODEL usfModel = null;
            bool isEdit = FmId > 0 && ActionType.Equals("edit");
            bool isSuccess = false;
            //日志记录
            USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
            logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
            logModel.OPERATEDATE = DateTime.Now;
            logModel.OPERATORID = AccountId;
            logModel.PROJECTID = ProjectId;
            logModel.COMPANYID = CompanyId;

            int nProjectId = ValidatorHelper.ToInt(Enc.Decrypt(Request.QueryString["projectid"], UrlEncKey), 0);

            //新增
            if (!isEdit)
            {
                usfModel = new USER_SHARE_FUNMENUMODEL();
                usfModel.FMID = CommonBusiness.GetSeqID("S_USER_SHARE_FUNMENU");
                usfModel.PROJECTID = nProjectId;
                usfModel.FMPARENTID = FmId > 0 ? FmId : 0;
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.AddProjectFunMenu.ToString("d"));
                logModel.OPERATECONTENT = "新增项目功能菜单，项目ID：" + usfModel.PROJECTID + "，菜单名称:" + txtFMName.Text.Trim();
            }
            //修改
            else
            {
                usfModel = ProjectFunBusiness.GetModel(FmId);
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.EditProjectFunMenu.ToString("d"));
                logModel.OPERATECONTENT = "修改项目功能菜单，菜单ID：" + FmId + "，修改后菜单名称:" + txtFMName.Text.Trim();
            }

            usfModel.FMNAME = txtFMName.Text.Trim();
            usfModel.FMPAGEURL = txtFMPageUrl.Text.Trim();
            usfModel.FMSORTNUM = ValidatorHelper.ToInt(txtFMSortNum.Text, 0);
            usfModel.FMDESC = txtFMDesc.Text.Trim();

            if (!isEdit)
            {
                isSuccess = ProjectFunBusiness.AddProjectFun(usfModel, logModel);
            }
            else
            {
                isSuccess = ProjectFunBusiness.UpdateProjectFun(usfModel, logModel);
            }


            Alert((isEdit ? "修改" : "新增") + "功能菜单" + (isSuccess ? "成功" : "失败，请重试") + "！");

            //刷新父页面
            ExecStartScript(string.Format("parent.location='ProjectFunManage.aspx?pid={0}&s={1}';", Request.QueryString["projectid"], new Random(10000).Next()));
        }

        #endregion
    }
}