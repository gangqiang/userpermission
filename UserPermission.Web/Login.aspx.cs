using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserPermission.Bll;
using UserPermission.Model;
using UserPermission.Utils;
using System.Data;

namespace UserPermission.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie ck = HttpContext.Current.Request.Cookies["CompanyCode"];
                if (ck != null && ck.Values["CompanyCode"] != null)
                {
                    txtCompanyCode.Text = CommonMethod.FinalString(ck.Values["CompanyCode"]);
                }

                //自动登录  格式 ：'网址/Login.aspx?action=autologin&code=公司编码&param=Enc.Encrypt("appkey,userid",公司编码)'
                string strAction = CommonMethod.FinalString(Request.QueryString["action"]);
                if (strAction.Equals("autologin"))
                {
                    AutoLogin();
                }

            }

        }

        private void AutoLogin()
        {
            string strCompanyCode = CommonMethod.FinalString(Request.QueryString["code"]);
            if (strCompanyCode.Length > 0)
            {
                string strAuthKey = CommonMethod.FinalString(Request.QueryString["param"]);
                strAuthKey = Enc.Decrypt(strAuthKey, strCompanyCode.PadLeft(8, '0'));
                string[] param = strAuthKey.Split(',');
                if (param != null )
                {
                    SysLogin(ValidatorHelper.ToInt(param[1], 0), "", "", param[0], strCompanyCode);
                }
                else
                {
                    Alert("传入参数错误！");
                    return;
                }
            }
            else
            {
                Alert("传入参数错误！");
                return;
            }
        }

        //登录
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            #region 验证
            if (txtCompanyCode.Text.Trim().Length == 0)
            {
                Alert("请输入公司编码！");
                Select(txtCompanyCode);
                return;
            }

            if (txtUserName.Text.Trim().Length == 0)
            {
                Alert("请输入用户名！");
                Select(txtUserName);
                return;
            }

            if (txtPassWord.Text.Trim().Length == 0)
            {
                Alert("请输入密码！");
                Select(txtPassWord);
                return;
            }

            #endregion

            string strAccountPwd = Enc.Encrypt(txtPassWord.Text.Trim(), txtCompanyCode.Text.Trim().PadLeft(8, '0'));
            SysLogin(0, txtUserName.Text.Trim(), strAccountPwd, string.Empty, txtCompanyCode.Text.Trim());
        }

        private void SysLogin(int nAccountId, string strAccount, string strPwd, string strAppKey, string strCompanyCode)
        {
            #region 登录

            USER_SHARE_ACCOUNTMODEL account = null;
            if (nAccountId > 0)
            {
                account = AccountBusiness.GetAccountModel(nAccountId);
            }
            else
            {
                account = AccountBusiness.GetAccountModel(strCompanyCode, strAccount, strPwd);
            }
            if (account == null)
            {
                Alert(nAccountId > 0 ? "不存在此账号!" : "用户名密码不匹配！");
                return;
            }
            else
            {
                if (account.STATUS != int.Parse(ShareEnum.AccountStatus.Normal.ToString("d")))
                {
                    Alert("此账号已无效，请确认！");
                    return;
                }

                USER_SHARE_PROJECTMODEL project = null;
                if (strAppKey.Length > 0)
                {
                    project = ProjectBusiness.GetProjectModelByKey(strAppKey);
                    if (project == null)
                    {
                        Alert("不存在的项目密钥，请确认！");
                        return;
                    }
                    if (project.STATUS == int.Parse(ShareEnum.ProjectStatus.StopUse.ToString("d")))
                    {
                        Alert("此项目已停用，请确认！");
                        return;
                    }
                }

                USER_SHARE_COMPANYRELATEMODEL company = CompanyBusiness.GetModel(account.COMPANYID);

                if (company == null)
                {
                    Alert("不存在的公司信息，请确认！");
                    return;
                }
                if (company.STATUS.ToString() == ShareEnum.CompanyRelateStatus.StopUse.ToString("d"))
                {
                    Alert("此公司信息已无效，请确认！");
                    return;
                }
                if (company.COMPANYCODE.ToString() != strCompanyCode)
                {
                    Alert("公司信息和账号信息不匹配");
                    return;
                }

                int nProjectId = ValidatorHelper.ToInt(CommonMethod.GetConfigValue("SYSPROJECTID"), 0);

                HttpCookie ck = new HttpCookie("USP");
                ck.Values.Add("AccountId", account.ACCOUNTID.ToString());
                ck.Values.Add("RealName", Server.UrlEncode(account.REALNAME));
                ck.Values.Add("ProjectId", project == null ? "0" : project.PROJECTID.ToString());
                ck.Values.Add("CompanyId", account.COMPANYID.ToString());
                ck.Values.Add("GroupId", CommonMethod.FinalString(company.GROUPID));
                ck.Values.Add("CompanyName", Server.UrlEncode(company.COMPANYNAME));
                ck.Values.Add("CompanyCode", company.COMPANYCODE.ToString());
                Response.Cookies.Add(ck);

                //由登录页面进入
                if (strAppKey.Length == 0)
                {
                    //公司编码记录到Cookie
                    HttpCookie ckCode = new HttpCookie("CompanyCode");
                    ckCode.Values.Add("CompanyCode", txtCompanyCode.Text.Trim());
                    Response.Cookies.Add(ckCode);
                }

                //获取菜单
                DataTable dt = CompanyFunBusiness.GetAccountFunMenu(account.ACCOUNTID, account.ISADMIN, nProjectId, account.COMPANYID);
                if (dt != null)
                {
                    DataRow[] rows = dt.Select("PROJECTID=" + nProjectId, "");
                    if (rows != null && rows.Length > 0)
                    {
                        Response.Redirect(rows[0]["CFPAGEURL"].ToString());
                    }
                    else
                    {
                        Alert("您没有此系统权限！");
                        return;
                    }
                }
                else
                {
                    Alert("您没有此系统权限！");
                    return;
                }
            }
            #endregion
        }

        /// <summary>
        /// 执行客户端脚本
        /// </summary>
        /// <param name="script"></param>
        public void ExecScript(string script)
        {
            ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">" + script + "</script>");
        }

        public void Alert(string content)
        {
            ExecScript("alert(\"" + content + "\")");
        }

        /// <summary>
        /// 选中一个客户端控件
        /// </summary>
        /// <param name="id">控件ID</param>
        public void Select(string id)
        {
            ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "<script type=\"text/javascript\">try{document.getElementById('" + id + "').select()}catch(e){alert(e.description);}</script>");
        }

        /// <summary>
        /// 选中一个服务端控件
        /// </summary>
        /// <param name="c">控件ID</param>
        public void Select(System.Web.UI.Control c)
        {
            Select(c.ClientID);
        }

    }
}