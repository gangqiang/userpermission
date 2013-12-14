using System;
using System.Collections.Generic;
using System.Web;
using UserPermission.Bll;
using UserPermission.Model;
using UserPermission.Utils;
using System.Text;
using System.IO;
using System.Xml;
using System.Data;

namespace UserPermission.ApiService
{
    /// <summary>
    /// PermissioinService 的摘要说明
    /// </summary>
    public class PermissioinService : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                //输出格式设置
                context.Response.ContentType = "text/xml";
                context.Response.ContentEncoding = Encoding.GetEncoding("GBK");
                context.Response.Charset = "gbk";

                ServiceResponse sResponse = new ServiceResponse();
                //请求方式验证 
                if (context.Request.HttpMethod != "POST")
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.HttpMethodErr;
                    context.Response.Write(sResponse.GetXML());
                    return;
                }
                //请求内容验证 
                if (context.Request.InputStream.Length == 0)
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.RequireContentErr;
                    context.Response.Write(sResponse.GetXML());
                    return;
                }
                //编码格式验证 
                if (context.Request.ContentEncoding != Encoding.GetEncoding("GBK"))
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.CodeTypeErr;
                    sResponse.ErrorDesc = "请使用gbk编码格式";
                    context.Response.Write(sResponse.GetXML());
                    return;
                }

                //action 及节点传入参数的验证
                string requestXML = string.Empty;
                using (StreamReader reader = new StreamReader(context.Request.InputStream, Encoding.GetEncoding("GBK")))
                {
                    requestXML = reader.ReadToEnd();
                    reader.Close();
                }

                XmlDocument xmlDoc = new XmlDocument();

                try
                {
                    //加载请求的XML格式内容 
                    xmlDoc.LoadXml(requestXML);
                }
                catch (Exception ex)
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.XMLFormatErr;
                    sResponse.ErrorDesc = ex.Message;
                    context.Response.Write(sResponse.GetXML());
                    return;
                }

                XmlNode xnAppKey = xmlDoc.SelectSingleNode("request/appkey");
                XmlNode xnAction = xmlDoc.SelectSingleNode("request/action");

                if (xnAppKey == null)
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.KeyErr;
                    sResponse.ErrorDesc = "缺少请求参数appkey";
                    context.Response.Write(sResponse.GetXML());
                    return;
                }

                if (xnAction == null)
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.ActionErr;
                    sResponse.ErrorDesc = "缺少请求的action";
                    context.Response.Write(sResponse.GetXML());
                    return;
                }

                if (!Enum.IsDefined(typeof(ShareEnum.ServiceAction), xnAction.InnerText.Trim()))
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.ActionErr;
                    sResponse.ErrorDesc = "不存在的action,请确认";
                    context.Response.Write(sResponse.GetXML());
                    return;
                }

                sResponse.ActionType = (ShareEnum.ServiceAction)Enum.Parse(typeof(ShareEnum.ServiceAction), xnAction.InnerText.Trim());

                //针对不同的Action进行针对性验证 
                switch (sResponse.ActionType)
                {
                    case ShareEnum.ServiceAction.AccountLogin://登录
                        AccountLogin(context, xmlDoc, sResponse);
                        break;
                    case ShareEnum.ServiceAction.EditAccountPwd://修改密码
                        EditAccountPwd(context, xmlDoc, sResponse);
                        break;
                    case ShareEnum.ServiceAction.AddAccount://注册账号
                        AddAccount(context, xmlDoc, sResponse);
                        break;
                    case ShareEnum.ServiceAction.AutoRegister://企业和账号自动注册，危险品和运管平台使用
                        AutoRegister(context, xmlDoc, sResponse);
                        break;
                    case ShareEnum.ServiceAction.GetAccounts://TMS获取账号信息
                        GetAccounts(context, xmlDoc, sResponse);
                        break;
                    default: break;
                }

                string strResponseXml = sResponse.GetXML();
                LogHelper.WriteInfo(string.Format("请求 内容:{0},返回:{1}", requestXML, strResponseXml));

                context.Response.Write(strResponseXml);
                context.Response.Flush();
            }

            catch (Exception ex)
            {
                LogHelper.WriteErr("权限接口内部错误", ex);
                ServiceResponse sResponse = new ServiceResponse();
                sResponse.ErrorType = ShareEnum.ApiResultStatus.ExceptionErr;
                sResponse.ErrorDesc = ex.Message;
                context.Response.Write(sResponse.GetXML());
                context.Response.End();
            }

        }

        /// <summary>
        /// 新开通账号
        /// </summary>
        /// <param name="context"></param>
        /// <param name="xmlDoc"></param>
        /// <param name="sResponse"></param>
        private void AddAccount(HttpContext context, XmlDocument xmlDoc, ServiceResponse sResponse)
        {
            XmlNode xnKey = xmlDoc.SelectSingleNode("request/appkey");
            XmlNode xnCompanyCode = xmlDoc.SelectSingleNode("request/companycode");
            XmlNode xnAccountName = xmlDoc.SelectSingleNode("request/accountname");
            XmlNode xnAccountPwd = xmlDoc.SelectSingleNode("request/accountpwd");
            XmlNode xnRealName = xmlDoc.SelectSingleNode("request/realname");
            XmlNode xnEmail = xmlDoc.SelectSingleNode("request/email");
            XmlNode xnCreatorId = xmlDoc.SelectSingleNode("request/creatorid");

            USER_SHARE_PROJECTMODEL projectModel = null;

            //appkey参数验证
            if (xnKey.InnerText.Trim().Length > 0)
            {
                projectModel = ProjectBusiness.GetProjectModelByKey(xnKey.InnerText.Trim());
                if (projectModel == null)
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.KeyErr;
                    sResponse.ErrorDesc = "不存在的密钥KEY";
                    context.Response.Write(sResponse.GetXML());
                    return;
                }
                else
                {
                    //companycode参数验证
                    if (xnCompanyCode == null || xnCompanyCode.InnerText.Trim().Length == 0)
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                        sResponse.ErrorDesc = "缺少传入参数companycode";
                        return;
                    }

                    if (!CompanyBusiness.IsCompanyCodeExists(xnCompanyCode.InnerText.Trim()))
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.UnValidCompanyCode;
                        sResponse.ErrorDesc = "不存在的公司编码信息";
                        return;
                    }

                    //accountpwd参数验证
                    if (xnAccountPwd == null || xnAccountPwd.InnerText.Trim().Length == 0)
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                        sResponse.ErrorDesc = "缺少传入参数accountpwd";
                        return;
                    }

                    //accountname参数验证
                    if (xnAccountName == null || xnAccountName.InnerText.Trim().Length == 0)
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                        sResponse.ErrorDesc = "缺少传入参数accountname";
                        return;
                    }

                    //判断账号是否重复
                    USER_SHARE_ACCOUNTMODEL accountModel = AccountBusiness.GetAccountModel(xnAccountName.InnerText.Trim(), xnCompanyCode.InnerText.Trim());
                    if (accountModel != null)
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.AccountNameExists;
                        sResponse.ErrorDesc = "已经存在此账号，请确认！";
                        return;
                    }

                    //realname参数验证
                    if (xnRealName == null || xnRealName.InnerText.Trim().Length == 0)
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                        sResponse.ErrorDesc = "缺少传入参数realname";
                        return;
                    }

                    //email参数验证
                    if (xnEmail == null || xnEmail.InnerText.Trim().Length == 0)
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                        sResponse.ErrorDesc = "缺少传入参数email";
                        return;
                    }

                    //creatorid参数验证
                    if (xnCreatorId == null || xnCreatorId.InnerText.Trim().Length == 0 || ValidatorHelper.ToInt(xnCreatorId.InnerText.Trim(), 0) == 0)
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                        sResponse.ErrorDesc = "缺少传入参数creatorid";
                        return;
                    }

                    //账号信息
                    accountModel = new USER_SHARE_ACCOUNTMODEL();
                    accountModel.ACCOUNTID = CommonBusiness.GetSeqID("S_USER_SHARE_ACCOUNT");
                    accountModel.ACCOUNTNAME = xnAccountName.InnerText.Trim();
                    accountModel.ACCOUNTPWD = xnAccountPwd.InnerText.Trim();
                    accountModel.COMPANYID = Convert.ToInt32(xnCompanyCode.InnerText.Trim());
                    accountModel.ORIGNALPWD = Enc.Decrypt(accountModel.ACCOUNTPWD, accountModel.COMPANYID.ToString().PadLeft(8, '0'));
                    accountModel.CREATEDATE = DateTime.Now;
                    accountModel.REALNAME = xnRealName.InnerText.Trim();
                    accountModel.EMAIL = xnEmail.InnerText.Trim();
                    accountModel.STATUS = int.Parse(ShareEnum.AccountStatus.Normal.ToString("d"));
                    accountModel.ISADMIN = 0;
                    accountModel.CREATORID = Convert.ToInt32(xnCreatorId.InnerText.Trim());

                    //日志信息
                    USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
                    logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
                    logModel.OPERATEDATE = DateTime.Now;
                    logModel.OPERATORID = accountModel.CREATORID;
                    logModel.PROJECTID = projectModel.PROJECTID;
                    logModel.COMPANYID = accountModel.COMPANYID;
                    logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.AddAccount.ToString("d"));
                    logModel.OPERATECONTENT = string.Format("通过接口新增账号信息，账号名称：{0}，公司ID：{1} ", xnAccountName.InnerText.Trim(), accountModel.COMPANYID);

                    if (AccountBusiness.AddAccount(accountModel, logModel))
                    {
                        sResponse.Result = string.Format("<accountid>{0}</accountid>", accountModel.ACCOUNTID);
                    }

                }
            }
        }

        /// <summary>
        /// 账号登陆
        /// </summary>
        /// <param name="context"></param>
        /// <param name="xmlDoc"></param>
        /// <param name="sResponse"></param>
        private void AccountLogin(HttpContext context, XmlDocument xmlDoc, ServiceResponse sResponse)
        {
            XmlNode xnKey = xmlDoc.SelectSingleNode("request/appkey");
            XmlNode xnCompanyCode = xmlDoc.SelectSingleNode("request/companycode");
            XmlNode xnAccountName = xmlDoc.SelectSingleNode("request/accountname");
            XmlNode xnAccountPwd = xmlDoc.SelectSingleNode("request/accountpwd");

            USER_SHARE_PROJECTMODEL projectModel = null;

            //appkey参数验证
            if (xnKey.InnerText.Trim().Length > 0)
            {
                projectModel = ProjectBusiness.GetProjectModelByKey(xnKey.InnerText.Trim());
                if (projectModel == null)
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.KeyErr;
                    sResponse.ErrorDesc = "不存在的密钥KEY";
                    context.Response.Write(sResponse.GetXML());
                    return;
                }
                else
                {
                    //companycode参数验证
                    if (xnCompanyCode == null || xnCompanyCode.InnerText.Trim().Length == 0)
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                        sResponse.ErrorDesc = "缺少传入参数companycode";
                        return;
                    }

                    if (!CompanyBusiness.IsCompanyCodeExists(xnCompanyCode.InnerText.Trim()))
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.UnValidCompanyCode;
                        sResponse.ErrorDesc = "不存在的公司编码信息";
                        return;
                    }

                    //accountpwd参数验证
                    if (xnAccountPwd == null || xnAccountPwd.InnerText.Trim().Length == 0)
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                        sResponse.ErrorDesc = "缺少传入参数accountpwd";
                        return;
                    }

                    //accountname参数验证
                    if (xnAccountName == null || xnAccountName.InnerText.Trim().Length == 0)
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                        sResponse.ErrorDesc = "缺少传入参数accountname";
                        return;
                    }

                    string strAppKey = xnKey.InnerText.Trim();
                    string strCompanyCode = xnCompanyCode.InnerText.Trim();
                    string strAccountName = xnAccountName.InnerText.Trim();

                    string strPwd = xnAccountPwd.InnerText.Trim();

                    USER_SHARE_ACCOUNTMODEL accountModel = AccountBusiness.GetAccountModel(strCompanyCode, strAccountName, strPwd);

                    if (accountModel == null)
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.UnValidUser;
                        sResponse.ErrorDesc = "用户名和密码不匹配";
                        return;
                    }
                    if (accountModel.STATUS != int.Parse(ShareEnum.AccountStatus.Normal.ToString("d")))
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.UnValidUser;
                        sResponse.ErrorDesc = "此用户已停用";
                        return;
                    }

                    USER_SHARE_COMPANYRELATEMODEL companyModel = CompanyBusiness.GetModel(accountModel.COMPANYID);

                    //不存在公司信息或 公司编码和传入的编码不一致
                    if (companyModel == null || companyModel.COMPANYCODE.ToString() != strCompanyCode)
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.CompanyNotMatchAccount;
                        sResponse.ErrorDesc = "公司信息和账号信息不匹配";
                        return;
                    }
                    if (companyModel.STATUS != int.Parse(ShareEnum.CompanyRelateStatus.Normal.ToString("d")))
                    {
                        sResponse.ErrorType = ShareEnum.ApiResultStatus.CompanyStopUse;
                        sResponse.ErrorDesc = "此公司已停用";
                        return;
                    }
                    StringBuilder sbContent = new StringBuilder();
                    sbContent.AppendFormat("<accountid>{0}</accountid>", accountModel.ACCOUNTID);
                    sbContent.AppendFormat("<truename>{0}</truename>", accountModel.REALNAME);
                    sbContent.AppendFormat("<companyid>{0}</companyid>", companyModel.COMPANYID);
                    sbContent.AppendFormat("<companyname>{0}</companyname>", companyModel.COMPANYNAME);
                    sbContent.AppendFormat("<sharecompanyid>{0}</sharecompanyid>", companyModel.SHARECOMPANYID);
                    sbContent.AppendFormat("<sharecompanyname>{0}</sharecompanyname>", companyModel.COMPANYNAME);
                    sbContent.AppendFormat("<productids>{0}</productids>", companyModel.PRODUCTIDS);
                    sbContent.AppendFormat("<groupid>{0}</groupid>", companyModel.GROUPID);
                    sbContent.AppendFormat("<groupname>{0}</groupname>", companyModel.COMPANYNAME);
                    sbContent.AppendFormat("<groupidn>{0}</groupidn>", companyModel.GROUPIDN);
                    sbContent.Append("<accountmenu>");
                    //账户拥有的菜单返回
                    DataTable dt = CompanyFunBusiness.GetAccountFunMenu(accountModel.ACCOUNTID, accountModel.ISADMIN, projectModel.PROJECTID, accountModel.COMPANYID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            sbContent.AppendFormat(@"<menuitem><menuid>{0}</menuid><name>{1}</name>
                                                 <linkurl>{2}</linkurl><parentid>{3}</parentid><sortnum>{4}</sortnum>
                                                <desc>{5}</desc></menuitem>", dr["FMID"], dr["CFANOTHERNAME"],
                                                    dr["CFPAGEURL"], dr["CFPARENTID"], dr["CFSORTNUM"], dr["CFDESC"]);
                        }
                    }

                    sbContent.Append("</accountmenu>");
                    sResponse.Result = sbContent.ToString();

                }
            }


        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="context"></param>
        /// <param name="xmlDoc"></param>
        /// <param name="sResponse"></param>
        private void EditAccountPwd(HttpContext context, XmlDocument xmlDoc, ServiceResponse sResponse)
        {
            XmlNode xnKey = xmlDoc.SelectSingleNode("request/appkey");
            XmlNode xnAccountId = xmlDoc.SelectSingleNode("request/accountid");
            XmlNode xnOldPwd = xmlDoc.SelectSingleNode("request/oldpwd");
            XmlNode xnNewPwd = xmlDoc.SelectSingleNode("request/newpwd");


            USER_SHARE_PROJECTMODEL projectModel = ProjectBusiness.GetProjectModelByKey(xnKey.InnerText.Trim());
            if (projectModel == null)
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.KeyErr;
                sResponse.ErrorDesc = "不存在的密钥KEY";
                return;
            }

            else
            {
                //oldpwd参数验证
                if (xnOldPwd == null || xnOldPwd.InnerText.Trim().Length == 0)
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                    sResponse.ErrorDesc = "缺少传入参数oldpwd";
                    return;
                }

                //newpwd参数验证
                if (xnNewPwd == null || xnNewPwd.InnerText.Trim().Length == 0)
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                    sResponse.ErrorDesc = "缺少传入参数oldpwd";
                    return;
                }

                //账号Id
                int nAccountId = ValidatorHelper.ToInt(xnAccountId.InnerText.Trim(), 0);

                //accountid参数验证
                if (xnAccountId == null || nAccountId <= 0)
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                    sResponse.ErrorDesc = "缺少传入参数accountid";
                    return;
                }

                string strAppKey = xnKey.InnerText.Trim();

                string strPwd = xnNewPwd.InnerText.Trim();


                USER_SHARE_ACCOUNTMODEL accountModel = AccountBusiness.GetAccountModel(nAccountId);
                if (accountModel == null || accountModel.ACCOUNTPWD != xnOldPwd.InnerText.Trim())
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.OldPassUnCorrect;
                    sResponse.ErrorDesc = "旧密码不正确";
                    return;
                }

                USER_SHARE_COMPANYRELATEMODEL companyModel = CompanyBusiness.GetModel(accountModel.COMPANYID);
                if (companyModel == null)
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.CompanyNotMatchAccount;
                    sResponse.ErrorDesc = "公司信息和账号信息不匹配";
                    return;
                }

                //得到原始密码
                string strOrignalPwd = Enc.Decrypt(strPwd, companyModel.COMPANYCODE.ToString().PadLeft(8, '0'));

                //更新账号密码
                if (!AccountBusiness.UpdatePwd(accountModel.ACCOUNTID, strPwd, strOrignalPwd))
                {
                    sResponse.ErrorType = ShareEnum.ApiResultStatus.ExceptionErr;
                    sResponse.ErrorDesc = "更改密码时出现并发错误，请重试！";
                    return;
                }

            }
        }

        /// <summary>
        /// 危险品，运管项目公司和账号自动注册接口(临时用)
        /// </summary>
        /// <param name="context"></param>
        /// <param name="xmlDoc"></param>
        /// <param name="sResponse"></param>
        private void AutoRegister(HttpContext context, XmlDocument xmlDoc, ServiceResponse sResponse)
        {
            //传入参数验证
            XmlNode xnCompanyType = xmlDoc.SelectSingleNode("request/companytype");
            XmlNode xnCompanyName = xmlDoc.SelectSingleNode("request/companyname");
            XmlNode xnGroupId = xmlDoc.SelectSingleNode("request/groupid");
            XmlNode xnGroupIdn = xmlDoc.SelectSingleNode("request/groupidn");
            XmlNode xnAccountName = xmlDoc.SelectSingleNode("request/accountname");
            XmlNode xnAccountPwd = xmlDoc.SelectSingleNode("request/accountpwd");
            XmlNode xnRealName = xmlDoc.SelectSingleNode("request/realname");

            if (xnCompanyType == null || xnCompanyType.InnerText.Trim().Length == 0)
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                sResponse.ErrorDesc = "缺少传入参数companytype";
                return;
            }
            if (xnCompanyName == null || xnCompanyName.InnerText.Trim().Length == 0)
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                sResponse.ErrorDesc = "缺少传入参数companyname";
                return;
            }
            if (xnGroupId == null || xnGroupId.InnerText.Trim().Length == 0)
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                sResponse.ErrorDesc = "缺少传入参数groupid";
                return;
            }
            if (xnGroupIdn == null || xnGroupIdn.InnerText.Trim().Length == 0)
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                sResponse.ErrorDesc = "缺少传入参数groupidn";
                return;
            }
            if (xnAccountName == null || xnAccountName.InnerText.Trim().Length == 0)
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                sResponse.ErrorDesc = "缺少传入参数accountname";
                return;
            }
            if (xnAccountPwd == null || xnAccountPwd.InnerText.Trim().Length == 0)
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                sResponse.ErrorDesc = "缺少传入参数accountpwd";
                return;
            }
            if (xnRealName == null || xnRealName.InnerText.Trim().Length == 0)
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                sResponse.ErrorDesc = "缺少传入参数realname";
                return;
            }

            int ntype = 0;
            string projectids = "";
            string productids = "";
            switch (xnCompanyType.InnerText.Trim())
            {
                //危险品运输
                case "0":
                    ntype = int.Parse(ShareEnum.CompanyType.PlatCompany.ToString("d"));
                    projectids = ",1,21,";//开通项目，危险品整合 和权限系统  
                    productids = ",15,2,";//权限系统维护和危险品运输 
                    break;
                //运管平台
                case "2":
                    ntype = int.Parse(ShareEnum.CompanyType.YgCompany.ToString("d"));
                    projectids = ",1,";//开通项目，危险品整合 和权限系统  
                    productids = ",15,";//权限系统维护 
                    break;
                default:
                    return;
            }

            int ncode = CompanyBusiness.IsCompanyExists(xnCompanyName.InnerText.Trim(), xnGroupId.InnerText.Trim(), ntype);
            int companyid = PlatFormBusiness.GetYgCompanyId(xnCompanyName.InnerText.Trim());
            if (ncode > 0)
            {

                //判断账号是否存在
                USER_SHARE_ACCOUNTMODEL accountModel1 = AccountBusiness.GetAccountModel(xnAccountName.InnerText.Trim(), ncode.ToString());
                if (accountModel1 != null)
                {
                    //已存在的账号，判断密码是否改动，如果改动，更新密码

                    if (accountModel1.ORIGNALPWD.Trim() != xnAccountPwd.InnerText.Trim())
                    {
                        accountModel1.ORIGNALPWD = xnAccountPwd.InnerText.Trim();
                        accountModel1.ACCOUNTPWD = Enc.Encrypt(xnAccountPwd.InnerText.Trim(), ncode.ToString().PadLeft(8, '0'));
                        //日志信息
                        USER_SHARE_LOGMODEL logModele = new USER_SHARE_LOGMODEL();
                        logModele.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
                        logModele.OPERATEDATE = DateTime.Now;
                        logModele.OPERATORID = 0;
                        logModele.PROJECTID = 21;
                        logModele.COMPANYID = ncode;
                        logModele.OPERATETYPE = int.Parse(ShareEnum.LogType.EditAccount.ToString("d"));
                        logModele.OPERATECONTENT = string.Format("通过自动注册接口更改账号密码，账号名称：{0}，公司ID：{1} ", xnAccountName.InnerText.Trim(), accountModel1.COMPANYID);
                        AccountBusiness.EditAccount(accountModel1, logModele);
                    }

                    //之前遗漏了公司Id ,已经注册过的，公司 Id更新进去
                    if (companyid > 0)
                    {
                        CompanyBusiness.UpdateRelateCompanyId(companyid, ncode);
                    }

                    return;
                }
            }
            else
            {
                //公司注册  产品开通

                USER_SHARE_COMPANYRELATEMODEL uscrModel = new USER_SHARE_COMPANYRELATEMODEL();
                ncode = CompanyBusiness.GetCompanyCode();
                uscrModel.CID = CommonBusiness.GetSeqID("S_USER_SHARE_COMPANYRELATE");
                uscrModel.COMPANYID = companyid;
                uscrModel.COMPANYTYPE = ntype;
                uscrModel.COMPANYNAME = xnCompanyName.InnerText.Trim();
                uscrModel.STATUS = int.Parse(ShareEnum.CompanyRelateStatus.Normal.ToString("d"));
                uscrModel.GROUPID = xnGroupId.InnerText.Trim();
                uscrModel.GROUPIDN = xnGroupIdn.InnerText.Trim();
                uscrModel.PROJECTIDS = projectids;//开通项目 
                uscrModel.PRODUCTIDS = productids;//开通产品
                uscrModel.CREATEDATE = DateTime.Now;
                uscrModel.COMPANYCODE = ncode;

                //日志记录
                USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
                logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
                logModel.OPERATEDATE = DateTime.Now;
                logModel.OPERATORID = 0;
                logModel.PROJECTID = 21;
                logModel.COMPANYID = ncode;
                logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.AddCompanyRelate.ToString("d"));
                logModel.OPERATECONTENT = "通过接口新增公司注册信息,公司名称:" + uscrModel.COMPANYNAME;
                CompanyBusiness.AddCompanyRelate(uscrModel, logModel);
            }

            //账号信息
            USER_SHARE_ACCOUNTMODEL accountModel = new USER_SHARE_ACCOUNTMODEL();
            accountModel.ACCOUNTID = CommonBusiness.GetSeqID("S_USER_SHARE_ACCOUNT");
            accountModel.ACCOUNTNAME = xnAccountName.InnerText.Trim();
            accountModel.ACCOUNTPWD = Enc.Encrypt(xnAccountPwd.InnerText.Trim(), ncode.ToString().PadLeft(8, '0'));
            accountModel.COMPANYID = ncode;
            accountModel.ORIGNALPWD = xnAccountPwd.InnerText.Trim();
            accountModel.CREATEDATE = DateTime.Now;
            accountModel.REALNAME = xnRealName.InnerText.Trim();
            accountModel.EMAIL = "";
            accountModel.STATUS = int.Parse(ShareEnum.AccountStatus.Normal.ToString("d"));
            accountModel.ISADMIN = 0;
            accountModel.CREATORID = 0;

            //日志信息
            USER_SHARE_LOGMODEL logModel2 = new USER_SHARE_LOGMODEL();
            logModel2.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
            logModel2.OPERATEDATE = DateTime.Now;
            logModel2.OPERATORID = 0;
            logModel2.PROJECTID = 21;
            logModel2.COMPANYID = ncode;
            logModel2.OPERATETYPE = int.Parse(ShareEnum.LogType.AddAccount.ToString("d"));
            logModel2.OPERATECONTENT = string.Format("通过公司自动注册接口注册公司后新增账号信息，账号名称：{0}，公司ID：{1} ", xnAccountName.InnerText.Trim(), accountModel.COMPANYID);
            AccountBusiness.AddAccount(accountModel, logModel2);
        }
        /// <summary>
        /// 获取TMS项目的公司账号信息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="xmlDoc"></param>
        /// <param name="sResponse"></param>
        private void GetAccounts(HttpContext context, XmlDocument xmlDoc, ServiceResponse sResponse)
        {
            XmlNode xnAccountName = xmlDoc.SelectSingleNode("request/accountname");
            XmlNode xnAccountPwd = xmlDoc.SelectSingleNode("request/accountpwd");
            XmlNode xnCompanyCode = xmlDoc.SelectSingleNode("request/companyid");
            if (CommonMethod.FinalString(xnAccountName.InnerText).Length == 0)
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                sResponse.ErrorDesc = "缺少传入参数accountname";
                return;
            }
            if (CommonMethod.FinalString(xnAccountPwd.InnerText).Length == 0)
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                sResponse.ErrorDesc = "缺少传入参数accountpwd";
                return;
            }
            if (CommonMethod.FinalString(xnCompanyCode.InnerText).Length == 0)
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.InputParaErr;
                sResponse.ErrorDesc = "缺少传入参数companyid";
                return;
            }

            USER_SHARE_ACCOUNTMODEL accountModel = AccountBusiness.GetAccountModel(xnCompanyCode.InnerText.Trim(), xnAccountName.InnerText.Trim(), xnAccountPwd.InnerText.Trim());

            if (accountModel == null)
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.UnValidUser;
                sResponse.ErrorDesc = "用户名和密码不匹配";
                return;
            }
            if (accountModel.STATUS != int.Parse(ShareEnum.AccountStatus.Normal.ToString("d")))
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.UnValidUser;
                sResponse.ErrorDesc = "此用户已停用";
                return;
            }

            USER_SHARE_COMPANYRELATEMODEL companyModel = CompanyBusiness.GetModel(accountModel.COMPANYID);

            //不存在公司信息或 公司编码和传入的编码不一致
            if (companyModel == null || companyModel.COMPANYCODE.ToString() != xnCompanyCode.InnerText.Trim())
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.CompanyNotMatchAccount;
                sResponse.ErrorDesc = "公司信息和账号信息不匹配";
                return;
            }
            if (companyModel.STATUS != int.Parse(ShareEnum.CompanyRelateStatus.Normal.ToString("d")))
            {
                sResponse.ErrorType = ShareEnum.ApiResultStatus.CompanyStopUse;
                sResponse.ErrorDesc = "此公司已停用";
                return;
            }
            //获取账号信息
            string strWhere = string.Format(" AND COMPANYID={0} ", xnCompanyCode.InnerText.Trim());
            strWhere += string.Format(" AND STATUS={0} ", ShareEnum.AccountStatus.Normal.ToString("d"));
            int count = 0;
            DataTable dt = AccountBusiness.GetAccountList(0, int.MaxValue, strWhere, out count);
            StringBuilder sbContent = new StringBuilder("<accounts>");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    sbContent.AppendFormat(@"<accountitem><accountname>{0}</accountname><accountpwd>{1}</accountpwd>
                                                 <truename>{2}</truename><isadmin>{3}</isadmin></accountitem> ",
                                        dr["ACCOUNTNAME"], dr["ACCOUNTPWD"], dr["REALNAME"], dr["ISADMIN"]);
                }
            }

            sbContent.Append("</accounts>");
            sResponse.Result = sbContent.ToString();

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}