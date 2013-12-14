using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserPermission.Bll;
using UserPermission.Model;
using UserPermission.Utils;

namespace UserPermission.Web.Pages.Init
{
    public partial class ProjectAdd : BasePage
    {
        #region 初始
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //修改
                if (Request.QueryString["id"] != null)
                {
                    LoadProjectInfo(Enc.Decrypt(Request.QueryString["id"], UrlEncKey));
                }
                else  //新增
                {
                    lblProjectKey.Text = System.Guid.NewGuid().ToString().ToUpper();
                }
            }
        }

        private void LoadProjectInfo(string strId)
        {
            USER_SHARE_PROJECTMODEL projectModel = ProjectBusiness.GetProjectModel(ValidatorHelper.ToInt(strId, 0));
            if (projectModel != null)
            {
                txtProjectName.Text = projectModel.PROJECTNAME;
                lblProjectKey.Text = projectModel.APISERVICEKEY;
                txtProjectDesc.Text = projectModel.PROJECTREMARK;
            }
        }

        #endregion


        #region 保存

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool isEdit = Request.QueryString["id"] != null;

            //项目信息
            USER_SHARE_PROJECTMODEL projectModel = null;

            //日志记录
            USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
            logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
            logModel.OPERATEDATE = DateTime.Now;
            logModel.OPERATORID = AccountId;
            logModel.PROJECTID = ProjectId;
            logModel.COMPANYID = CompanyId;
            if (!isEdit)
            {
                projectModel = new USER_SHARE_PROJECTMODEL();
                projectModel.PROJECTID = CommonBusiness.GetSeqID("S_USER_SHARE_PROJECT");
                projectModel.PROJECTNAME = txtProjectName.Text.Trim();
                projectModel.APISERVICEKEY = lblProjectKey.Text.Trim();
                projectModel.PROJECTREMARK = txtProjectDesc.Text.Trim();
                projectModel.STATUS = int.Parse(ShareEnum.ProjectStatus.Normal.ToString("d"));
                projectModel.CREATEDATE = DateTime.Now;

                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.AddProject.ToString("d"));
                logModel.OPERATECONTENT = "注册新项目，Id:" + projectModel.PROJECTID + ",名称：" + projectModel.PROJECTNAME;
                if (ProjectBusiness.AddProject(projectModel, logModel))
                {
                    Alert("项目注册成功！");
                }
                else
                {
                    Alert("项目注册失败，请重试！");
                }
            }
            else
            {
                projectModel = ProjectBusiness.GetProjectModel(ValidatorHelper.ToInt(Enc.Decrypt(Request.QueryString["id"], UrlEncKey), 0));
                projectModel.PROJECTNAME = txtProjectName.Text.Trim();
                projectModel.PROJECTREMARK = txtProjectDesc.Text.Trim();
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.EditProject.ToString("d"));
                logModel.OPERATECONTENT = "修改项目信息，项目Id:" + projectModel.PROJECTID + ",修改后的名称：" + projectModel.PROJECTNAME;
                if (ProjectBusiness.UpdateProject(projectModel, logModel))
                {
                    Alert("项目信息修改成功！");
                }
                else
                {
                    Alert("项目信息修改失败，请重试！");
                }
            }

            ExecScript("parent.__doPostBack('ctl00$MainContent$btnSearch','');");

        }

        #endregion
    }
}