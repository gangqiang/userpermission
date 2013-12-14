using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;
using System.IO;
using UserPermission.Bll;


namespace CRM.Business
{
    public sealed class BaseBusiness
    {
        private const string SERVICE_URL = "http://demo.gpssz.com/SpService/PermissioinService.ashx";
        //private const string SERVICE_URL = "http://localhost:1216/PermissioinService.ashx";
        //private const string SERVICE_URL = "http://demo.gpssz.com/OilApiService1/Service.ashx";
        private const string APPKEY = "925C90E4-C258-4745-B198-1ACDDDB50778";
        private const string EncryptKey = "00000117";
        public BaseBusiness()
        {

        }

        #region 用户登录
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="accountName"></param>
        /// <param name="accountPwd"></param>
        /// <param name="companyCode"></param>
        /// <returns></returns>
        public static ActionResult AccountLogin(string accountName, string accountPwd, string companyCode)
        {
            string response = string.Empty;
            ActionResult result = new ActionResult();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                #region 构造请求的XML
                XmlDeclaration declare = xmlDoc.CreateXmlDeclaration("1.0", "gbk", null);
                xmlDoc.AppendChild(declare);
                XmlElement xeRoot = xmlDoc.CreateElement("request");
                XmlElement xeAppKey = xmlDoc.CreateElement("appkey");
                xeAppKey.InnerText = APPKEY;
                XmlElement xeAction = xmlDoc.CreateElement("action");
                xeAction.InnerText = "AccountLogin";
                XmlElement xeAccountname = xmlDoc.CreateElement("accountname");
                xeAccountname.InnerText = accountName;
                XmlElement xeAccountpwd = xmlDoc.CreateElement("accountpwd");
                xeAccountpwd.InnerText = Enc.Encrypt(accountPwd, EncryptKey);
                XmlElement xeCompanyCode = xmlDoc.CreateElement("companycode");
                xeCompanyCode.InnerText = companyCode;
                xeRoot.AppendChild(xeAppKey);
                xeRoot.AppendChild(xeAction);
                xeRoot.AppendChild(xeAccountname);
                xeRoot.AppendChild(xeAccountpwd);
                xeRoot.AppendChild(xeCompanyCode);
                xmlDoc.AppendChild(xeRoot);
                #endregion
                HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(SERVICE_URL);
                //相应请求的参数
                byte[] data = Encoding.GetEncoding("GBK").GetBytes(xmlDoc.OuterXml);

                httpRequest.Method = "POST";
                httpRequest.ContentType = "text/xml;charset=gbk";
                httpRequest.ContentLength = data.Length;
                httpRequest.Timeout = 100000;
                httpRequest.ReadWriteTimeout = 100000;
                httpRequest.ServicePoint.Expect100Continue = false;
                httpRequest.ServicePoint.UseNagleAlgorithm = false;

                //请求流
                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                //响应流
                HttpWebResponse m_Response = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = m_Response.GetResponseStream();
                using (StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("GBK")))
                {
                    //获取返回的信息
                    response = streamReader.ReadToEnd();
                    xmlDoc.LoadXml(response);
                    streamReader.Close();
                    XmlNode codeNode = xmlDoc.SelectSingleNode("response/result/code");
                    if (codeNode != null)
                        result.code = codeNode.InnerText;
                    XmlNode descNode = xmlDoc.SelectSingleNode("response/result/desc");
                    if (descNode != null)
                        result.desc = descNode.InnerText;
                    if (result.code == "0")
                    {
                        //账户id
                        XmlNode accountIdNode = xmlDoc.SelectSingleNode("response/result/accountid");
                        if (accountIdNode != null)
                        {
                            result.accountId = accountIdNode.InnerText;
                        }
                        //真实姓名
                        XmlNode trueNameNode = xmlDoc.SelectSingleNode("response/result/truename");
                        if (trueNameNode != null)
                        {
                            result.trueName = trueNameNode.InnerText;
                        }
                        //所属公司companyid
                        XmlNode companyIdNode = xmlDoc.SelectSingleNode("response/result/companyid");
                        if (companyIdNode != null)
                        {
                            result.companyId = companyIdNode.InnerText;
                        }
                        //所属公司companyname
                        XmlNode companyNameNode = xmlDoc.SelectSingleNode("response/result/companyname");
                        if (companyNameNode != null)
                        {
                            result.companyName = companyNameNode.InnerText;
                        }
                        //所属公司shareCompanyId
                        XmlNode shareCompanyIdNode = xmlDoc.SelectSingleNode("response/result/sharecompanyid");
                        if (shareCompanyIdNode != null)
                        {
                            result.shareCompanyId = shareCompanyIdNode.InnerText;
                        }
                        //所属公司shareCompanyName
                        XmlNode shareCompanyNameNode = xmlDoc.SelectSingleNode("response/result/sharecompanyname");
                        if (shareCompanyNameNode != null)
                        {
                            result.shareCompanyName = shareCompanyNameNode.InnerText;
                        }
                        //所属公司groupid
                        XmlNode groupIdNode = xmlDoc.SelectSingleNode("response/result/groupid");
                        if (groupIdNode != null)
                        {
                            result.groupId = groupIdNode.InnerText;
                        }
                        //所属公司groupName
                        XmlNode groupNameNode = xmlDoc.SelectSingleNode("response/result/groupname");
                        if (groupNameNode != null)
                        {
                            result.groupName = groupNameNode.InnerText;
                        }
                        MenuItem item;
                        List<MenuItem> items = new List<MenuItem>();
                        XmlNodeList xmlMenuItem = xmlDoc.SelectNodes("response/result/accountmenu/menuitem");
                        foreach (XmlNode xn in xmlMenuItem)
                        {
                            item = new MenuItem();
                            item.menuId = xn.SelectSingleNode("menuid").InnerText.Trim();
                            item.name = xn.SelectSingleNode("name").InnerText.Trim();
                            item.linkUrl = xn.SelectSingleNode("linkurl").InnerText.Trim();
                            item.parentId = xn.SelectSingleNode("parentid").InnerText.Trim();
                            item.menuDesc = xn.SelectSingleNode("menudesc").InnerText.Trim();
                            items.Add(item);

                        }
                    }


                }
            }
            catch (Exception ex)
            {
                result.code = "105";
                result.desc = ex.Message;
            }
            return result;
        }
        #endregion

        #region 修改密码
        public static ActionPwdResult EditPwd(string accountId, string oldPwd, string newPwd)
        {
            string response = string.Empty;
            ActionPwdResult result = new ActionPwdResult();
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                #region 构造请求的XML
                XmlDeclaration declare = xmlDoc.CreateXmlDeclaration("1.0", "gbk", null);
                xmlDoc.AppendChild(declare);
                XmlElement xeRoot = xmlDoc.CreateElement("request");
                XmlElement xeAppKey = xmlDoc.CreateElement("appkey");
                xeAppKey.InnerText = APPKEY;
                XmlElement xeAction = xmlDoc.CreateElement("action");
                xeAction.InnerText = "EditAccountPwd";
                XmlElement xeAccountId = xmlDoc.CreateElement("accountid");
                xeAccountId.InnerText = accountId;
                XmlElement xeOldPwd = xmlDoc.CreateElement("oldpwd");
                xeOldPwd.InnerText = Enc.Encrypt(oldPwd, EncryptKey);
                XmlElement xeNewPwd = xmlDoc.CreateElement("newpwd");
                xeNewPwd.InnerText = Enc.Encrypt(newPwd, EncryptKey);

                xeRoot.AppendChild(xeAppKey);
                xeRoot.AppendChild(xeAction);
                xeRoot.AppendChild(xeAccountId);
                xeRoot.AppendChild(xeOldPwd);
                xeRoot.AppendChild(xeNewPwd);
                xmlDoc.AppendChild(xeRoot);
                #endregion
                HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(SERVICE_URL);
                //相应请求的参数
                byte[] data = Encoding.GetEncoding("GBK").GetBytes(xmlDoc.OuterXml);

                httpRequest.Method = "POST";
                httpRequest.ContentType = "text/xml;charset=gbk";
                httpRequest.ContentLength = data.Length;
                httpRequest.Timeout = 100000;
                httpRequest.ReadWriteTimeout = 100000;
                httpRequest.ServicePoint.Expect100Continue = false;
                httpRequest.ServicePoint.UseNagleAlgorithm = false;

                //请求流
                Stream requestStream = httpRequest.GetRequestStream();
                requestStream.Write(data, 0, data.Length);
                requestStream.Close();

                //响应流
                HttpWebResponse m_Response = (HttpWebResponse)httpRequest.GetResponse();
                Stream responseStream = m_Response.GetResponseStream();
                using (StreamReader streamReader = new StreamReader(responseStream, Encoding.GetEncoding("GBK")))
                {
                    //获取返回的信息
                    response = streamReader.ReadToEnd();
                    xmlDoc.LoadXml(response);
                    streamReader.Close();
                    XmlNode codeNode = xmlDoc.SelectSingleNode("response/result/code");
                    if (codeNode != null)
                        result.code = codeNode.InnerText;
                    XmlNode descNode = xmlDoc.SelectSingleNode("response/result/desc");
                    if (descNode != null)
                        result.desc = descNode.InnerText;
                }
            }
            catch (Exception ex)
            {
                result.code = "105";
                result.desc = ex.Message;
            }
            return result;
        }
        #endregion
    }
    /// <summary>
    /// 修改密码返回结果
    /// </summary>
    public sealed class ActionPwdResult
    {
        /// <summary>
        /// 是否发生错误
        /// </summary>
        public string code = "0";
        /// <summary>
        /// 错误描述
        /// </summary>
        public string desc = string.Empty;
    }
    /// <summary>
    /// 登录操作结果
    /// </summary>
    public sealed class ActionResult
    {
        /// <summary>
        /// 是否发生错误
        /// </summary>
        public string code = "0";
        /// <summary>
        /// 错误描述
        /// </summary>
        public string desc = string.Empty;
        /// <summary>
        /// 账户id
        /// </summary>
        public string accountId = string.Empty;
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string trueName = string.Empty;
        /// <summary>
        /// 所属公司companyid
        /// </summary>
        public string companyId = string.Empty;
        /// <summary>
        /// 所属公司companyname
        /// </summary>
        public string companyName = string.Empty;
        /// <summary>
        /// 所属公司shareCompanyId
        /// </summary>
        public string shareCompanyId = string.Empty;
        /// <summary>
        /// 所属公司sharecompanyname
        /// </summary>
        public string shareCompanyName = string.Empty;
        /// <summary>
        /// 所属公司groupid
        /// </summary>
        public string groupId = string.Empty;
        /// <summary>
        /// 所属公司groupName
        /// </summary>
        public string groupName = string.Empty;
        /// <summary>
        /// 菜单集合
        /// </summary>
        public MenuItem menuItem = null;

        public List<MenuItem> menuItems = null;
    }

    public class MenuItem
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public string menuId = string.Empty;
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string name = string.Empty;
        /// <summary>
        /// 链接地址
        /// </summary>
        public string linkUrl = string.Empty;
        /// <summary>
        /// 上级菜单id
        /// </summary>
        public string parentId = string.Empty;
        /// <summary>
        /// 菜单功能描述
        /// </summary>
        public string menuDesc = string.Empty;


    }
}
