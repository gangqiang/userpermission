using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserPermission.Utils;
using UserPermission.Model;
using UserPermission.Bll;
using System.Data;
using System.Text;

namespace UserPermission.Web.Pages.Service
{
    public partial class AccountAdd : BasePage
    {
        #region 属性
        public int PageAccountId
        {
            get { return Request.QueryString["id"] == null ? 0 : ValidatorHelper.ToInt(Enc.Decrypt(Request.QueryString["id"], UrlEncKey), 0); }
        }

        /// <summary>
        /// 从公司初始账号处进入
        /// </summary>
        public bool IsInit
        {
            get { return CommonMethod.FinalString(Request.QueryString["type"]).Equals("init"); }
        }

        #endregion

        #region 初始
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitPage();
            }
        }


        private void InitPage()
        {
            //账号状态
            ControlHelper.ListContolDataBindFromEnum(rbtAccountStatus, typeof(ShareEnum.AccountStatus), "", "", ShareEnum.AccountStatus.Normal.ToString("d"));
            rbtAccountStatus.Items.Remove(rbtAccountStatus.Items.FindByValue(ShareEnum.AccountStatus.Del.ToString("d")));

            //账号Id
            hidAccountId.Value = PageAccountId.ToString();

            string strRoleIds = string.Empty;

            hidCompanyCode.Value = CompanyCode.ToString();

            if (PageAccountId > 0)//修改页面
            {
                USER_SHARE_ACCOUNTMODEL accountModel = AccountBusiness.GetAccountModel(PageAccountId);
                if (accountModel != null)
                {
                    txtAccountName.Text = accountModel.ACCOUNTNAME;
                    txtRealName.Text = accountModel.REALNAME;
                    txtPwd.Attributes["value"] = accountModel.ORIGNALPWD;
                    txtPwd2.Attributes["value"] = accountModel.ORIGNALPWD;
                    txtLinkPhone.Text = CommonMethod.FinalString(accountModel.LINKPHONE);
                    hidEmail.Value = CommonMethod.FinalString(accountModel.EMAIL);
                    hidCompanyCode.Value = accountModel.COMPANYID.ToString();
                    ControlHelper.SelectFlg(rbtAccountStatus, accountModel.STATUS.ToString());
                    strRoleIds = CommonMethod.FinalString(accountModel.ROLEIDS);
                }

                else
                {
                    Response.Write("不存在的账号信息！");
                    Response.End();
                }
            }


            #region 角色

            if (!IsInit)
            {
                DataTable dtProjects = CompanyBusiness.GetCompanyProjects(CompanyCode.ToString());
                DataTable dtRoles = RoleBusiness.GetAccountRoleList(" AND R.COMPANYID=" + CompanyId);
                if (dtProjects != null && dtProjects.Rows.Count > 0 && dtRoles != null)
                {
                    StringBuilder sbContent = new StringBuilder("");
                    foreach (DataRow dr in dtProjects.Rows)
                    {
                        sbContent.Append("<table class=\"table\" style=\"float:left; width:33%; margin-left:3px;\">");
                        sbContent.AppendFormat("<tr><td class='rhead' style=\"text-align:left;background-image: url('../../Resource/images/searchthead.gif');\" >{0}</td></tr>", dr["ProjectName"]);

                        DataRow[] drRoles = dtRoles.Select("PROJECTID=" + dr["PROJECTID"], " ROLEID ASC ");
                        foreach (DataRow drRole in drRoles)
                        {
                            sbContent.AppendFormat("<tr><td><input type='checkbox'   name='role' id='{0}' value='{0}' {2} />{1}</td></tr>",
                                  drRole["ROLEID"], drRole["ROLENAME"], strRoleIds.IndexOf("," + drRole["ROLEID"] + ",") >= 0 ? "checked='checked'" : "");

                        }

                        sbContent.Append("</table>");
                    }

                    tdRoles.InnerHtml = sbContent.ToString();
                }
            }
            else
            {
                trRoles.Visible = false;
                hidCompanyCode.Value = Enc.Decrypt(CommonMethod.FinalString(Request.QueryString["code"]), UrlEncKey);
            }

            #endregion

        }


        #endregion

        #region 保存

        protected void btnSave_Click(object sender, EventArgs e)
        {
            #region 服务端验证

            if (txtAccountName.Text.Trim().Length == 0)
            {
                Alert("请输入账号名称！");
                Select(txtAccountName);
                return;
            }

            if (txtRealName.Text.Trim().Length == 0)
            {
                Alert("请输入真实姓名！");
                Select(txtRealName);
                return;
            }

            if (txtPwd.Text.Trim().Length == 0)
            {
                Alert("请输入登录密码！");
                Select(txtPwd);
                return;
            }

            if (txtPwd2.Text.Trim().Length == 0)
            {
                Alert("请确认登录密码！");
                Select(txtPwd2);
                return;
            }
            if (!txtPwd.Text.Trim().Equals(txtPwd2.Text.Trim()))
            {
                Alert("两次输入密码不一致！");
                Select(txtPwd2);
                return;
            }
            if (txtEmail.Text.Trim().Length == 0)
            {
                Alert("请输入邮箱！");
                Select(txtEmail);
                return;
            }

            string strRoles = CommonMethod.FinalString(Request.Form["role"]);
            if (strRoles.Length > 0)
            {
                strRoles = "," + strRoles + ",";
            }

            #endregion

            #region 账号信息保存

            USER_SHARE_ACCOUNTMODEL accountModel = null;

            int nCompanyCode = IsInit ? ValidatorHelper.ToInt(Enc.Decrypt(Request.QueryString["code"], UrlEncKey), 0) : CompanyCode;

            //日志信息
            USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
            logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
            logModel.OPERATEDATE = DateTime.Now;
            logModel.OPERATORID = AccountId;
            logModel.PROJECTID = ProjectId;
            logModel.COMPANYID = CompanyId;

            if (PageAccountId > 0)
            {
                accountModel = AccountBusiness.GetAccountModel(PageAccountId);
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.EditAccount.ToString("d"));
                logModel.OPERATECONTENT = string.Format("修改账号信息，修改后账号名称：{0}，公司编码：{1}，账号Id:{2} ", txtAccountName.Text.Trim(), CompanyCode, PageAccountId);
            }
            else
            {
                accountModel = new USER_SHARE_ACCOUNTMODEL();
                accountModel.ACCOUNTID = CommonBusiness.GetSeqID("S_USER_SHARE_ACCOUNT");
                accountModel.COMPANYID = nCompanyCode;
                accountModel.CREATORID = AccountId;
                if (IsInit)
                {
                    accountModel.ISADMIN = 1;
                }
                accountModel.CREATEDATE = DateTime.Now;
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.AddAccount.ToString("d"));
                logModel.OPERATECONTENT = string.Format("新增账号信息，账号名称：{0}，公司ID：{1} ", txtAccountName.Text.Trim(), accountModel.COMPANYID);
            }

            accountModel.ACCOUNTNAME = txtAccountName.Text.Trim();
            accountModel.REALNAME = txtRealName.Text.Trim();
            accountModel.ORIGNALPWD = txtPwd.Text.Trim();
            accountModel.ACCOUNTPWD = Enc.Encrypt(txtPwd.Text.Trim(), nCompanyCode.ToString().PadLeft(8, '0'));
            accountModel.LINKPHONE = txtLinkPhone.Text.Trim();
            accountModel.EMAIL = txtEmail.Text.Trim();
            accountModel.ROLEIDS = strRoles;
            accountModel.STATUS = ValidatorHelper.ToInt(rbtAccountStatus.SelectedValue, 0);

            bool blSuccess = false;

            if (PageAccountId == 0)
            {
                blSuccess = AccountBusiness.AddAccount(accountModel, logModel);
            }
            else
            {
                blSuccess = AccountBusiness.EditAccount(accountModel, logModel);
            }

            Alert((PageAccountId > 0 ? "修改" : "新增") + "账号" + (blSuccess ? "成功" : "失败，请重试！"));

            ExecScript("parent.__doPostBack('ctl00$MainContent$btnSearch','');");


            #endregion
        }

        #endregion


    }
}