using System;
using System.Collections.Generic;
using System.Web;
using UserPermission.Utils;
using UserPermission.Model;
using UserPermission.Bll;
using Newtonsoft.Json;
using Microsoft.JScript;
using System.Data;
using System.Text;
namespace UserPermission.Web.Handler
{
    /// <summary>
    /// Ajax 的摘要说明
    /// </summary>
    public class AjaxHandler : BasePage, IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string strContent = string.Empty;
            string strAction = CommonMethod.FinalString(context.Request.QueryString["action"]);
            switch (strAction) 
            {
                case "LoadCompany":
                    strContent = LoadCompany(context);
                    break;
                case "LoadGroup":
                    strContent = LoadGroup(context);
                    break;
                case "LoadProjectFunMenu":
                    strContent = LoadProjectFunMenu(context);
                    break;
                case "LoadCFunMenu":
                    strContent = LoadCFunMenu(context);
                    break;
                case "IfFunDel":
                    strContent = IfFunDel(context);
                    break;
                case "DelFunMenu":
                    strContent = DelFunMenu(context);
                    break;
                case "LoadFunMenu":
                    strContent = LoadFunMenu(context);
                    break;
                case "LoadCompanyFunMenu":
                    strContent = LoadCompanyFunMenu(context);
                    break;
                case "ValidateAccountName":
                    strContent = ValidateAccountName(context);
                    break;
                case "IfRoleUse":
                    strContent = IfRoleUse(context);
                    break;
                case "IfCFunDel":
                    strContent = IfCFunDel(context);
                    break;
                case "DelCFunMenu":
                    strContent = DelCFunMenu(context);
                    break;
                case "LoadCompanyGroup":
                    strContent = LoadCompanyGroup(context);
                    break;
                case "DelGroup":
                    strContent = DelGroup(context);
                    break;
                default: break;
            }
            context.Response.Write(strContent);
            context.Response.End();
        }



        /// <summary>
        /// 加载项目菜单
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string LoadCFunMenu(HttpContext context)
        {
            string strProjectId = context.Request.QueryString["projectid"];
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if (strProjectId.Length > 0)
            {
                DataTable terminate = new DataTable();
                terminate.Columns.Add("FMID");
                terminate.Columns.Add("CFPARENTID");
                terminate.Columns.Add("CFANOTHERNAME");
                terminate.Columns.Add("CFPAGEURL");
                terminate.Columns.Add("CFDESC");
                terminate.Columns.Add("CFSORTNUM");

                DataTable dtProjectFunMenu = CompanyFunBusiness.GetCompanyFunList(" AND PROJECTID=" + strProjectId + " AND COMPANYID=" + CompanyId);
                ReConstructionCompanyFunDataTable(dtProjectFunMenu, terminate, 0);

                DataRow row;
                DataRowCollection drc = terminate.Rows;
                int rowCount = drc.Count;

                if (rowCount > 0)
                {

                    for (int i = 0; i < rowCount; i++)
                    {
                        row = drc[i];
                        sb.Append("[");
                        sb.AppendFormat("'{0}','{1}',", row["FMID"], row["CFPARENTID"]);
                        sb.Append("[");
                        sb.AppendFormat("'{0}','{1}','{2}','{3}','{4}'", row["CFANOTHERNAME"], row["CFPAGEURL"], row["CFSORTNUM"], row["CFDESC"], Enc.Encrypt(row["FMID"].ToString(), UrlEncKey));
                        sb.Append("]");
                        if (i != rowCount - 1)
                        {
                            sb.Append("],");
                        }
                        else
                        {
                            sb.Append("]");
                        }
                    }

                    sb.Append("]");
                }

            }
            return sb.ToString();
        }



        /// <summary>
        /// 判断角色下是否有账号信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string IfRoleUse(HttpContext context)
        {
            string strResult = "";
            string roleid = context.Request.QueryString["RoleId"];
            if (roleid.Length > 0)
            {
                strResult = AccountBusiness.IsRoleUse(roleid) ? "1" : "0";
            }

            return strResult;

        }


        /// <summary>
        /// 判断账号名是否重复
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string ValidateAccountName(HttpContext context)
        {
            string strResult = string.Empty;
            string strAccountName = GlobalObject.unescape(context.Request.QueryString["AccountName"]);
            int nAccountId = ValidatorHelper.ToInt(context.Request.QueryString["AccountId"], 0);
            string strCompanyCode = context.Request.QueryString["CompanyCode"];
            USER_SHARE_ACCOUNTMODEL accountModel = AccountBusiness.GetAccountModel(strAccountName, strCompanyCode);
            if (accountModel != null)
            {
                // 新增时，存在证明重复，修改时 判断与自身之外的记录重复才算重复
                strResult = (nAccountId == 0 || (nAccountId > 0 && accountModel.ACCOUNTID != nAccountId)) ? "1" : "0";
            }
            else
            {
                strResult = "0";
            }
            return strResult;
        }


        /// <summary>
        /// 将功能菜单设置为无效
        /// </summary>
        /// <param name="context"></param>
        private string DelFunMenu(HttpContext context)
        {
            int nFmId = ValidatorHelper.ToInt(Enc.Decrypt(context.Request.QueryString["fmid"], UrlEncKey), 0);
            //日志记录
            USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
            logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
            logModel.OPERATEDATE = DateTime.Now;
            logModel.OPERATORID = AccountId;
            logModel.PROJECTID = ProjectId;
            logModel.COMPANYID = CompanyId;
            logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.ChangeProjectFunMenuStatus.ToString("d"));
            logModel.OPERATECONTENT = "设置项目功能菜单状态为" + EnumPlus.GetEnumDescription(typeof(ShareEnum.LogType), logModel.OPERATETYPE.ToString());
            return ProjectFunBusiness.SetFunStop(nFmId, logModel) ? "0" : "1";
        }


        /// <summary>
        /// 判断功能能否被删除
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string IfFunDel(HttpContext context)
        {
            int nFmId = ValidatorHelper.ToInt(Enc.Decrypt(context.Request.QueryString["fmid"], UrlEncKey), 0);
            return ProjectFunBusiness.ExistsChildFun(nFmId) ? "1" : "0";
        }

        /// <summary>
        /// 判断公司功能能否被删除
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string IfCFunDel(HttpContext context)
        {
            int nFmId = ValidatorHelper.ToInt(Enc.Decrypt(context.Request.QueryString["fmid"], UrlEncKey), 0);
            return CompanyFunBusiness.ExistsChildFun(nFmId,CompanyCode) ? "1" : "0";
        }

        /// <summary>
        /// 将功能菜单设置为无效
        /// </summary>
        /// <param name="context"></param>
        private string DelCFunMenu(HttpContext context)
        {
            int nFmId = ValidatorHelper.ToInt(Enc.Decrypt(context.Request.QueryString["fmid"], UrlEncKey), 0);
            //日志记录
            USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
            logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
            logModel.OPERATEDATE = DateTime.Now;
            logModel.OPERATORID = AccountId;
            logModel.PROJECTID = ProjectId;
            logModel.COMPANYID = CompanyId;
            logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.StopUseCompanyFun.ToString("d"));
            logModel.OPERATECONTENT = "设置公司功能菜单状态为" + EnumPlus.GetEnumDescription(typeof(ShareEnum.LogType), logModel.OPERATETYPE.ToString());
            return CompanyFunBusiness.SetFunStop(nFmId, CompanyId, logModel) ? "0" : "1";
        }

        /// <summary>
        /// 加载项目菜单
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string LoadProjectFunMenu(HttpContext context)
        {
            string strProjectId = Enc.Decrypt(context.Request.QueryString["projectid"], UrlEncKey);
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if (strProjectId.Length > 0)
            {
                DataTable terminate = new DataTable();
                terminate.Columns.Add("FMID");
                terminate.Columns.Add("FMPARENTID");
                terminate.Columns.Add("FMNAME");
                terminate.Columns.Add("FMPAGEURL");
                terminate.Columns.Add("FMDESC");
                terminate.Columns.Add("FMSORTNUM");

                DataTable dtProjectFunMenu = ProjectFunBusiness.GetFunMenuList(" AND PROJECTID=" + strProjectId);
                ReConstructionDataTable(dtProjectFunMenu, terminate, 0);

                DataRow row;
                DataRowCollection drc = terminate.Rows;
                int rowCount = drc.Count;

                if (rowCount > 0)
                {

                    for (int i = 0; i < rowCount; i++)
                    {
                        row = drc[i];
                        sb.Append("[");
                        sb.AppendFormat("{0},{1},", row["FMID"], row["FMPARENTID"]);
                        sb.Append("[");
                        sb.AppendFormat("'{0}','{1}','{2}','{3}','{4}'", row["FMNAME"], row["FMPAGEURL"], row["FMSORTNUM"], row["FMDESC"], Enc.Encrypt(row["FMID"].ToString(), UrlEncKey));
                        sb.Append("]");
                        if (i != rowCount - 1)
                        {
                            sb.Append("],");
                        }
                        else
                        {
                            sb.Append("]");
                        }
                    }

                    sb.Append("]");
                }

            }
            return sb.ToString();
        }



        /// <summary>
        /// 重新构建DataTable
        /// </summary>
        /// <param name="original"></param>
        /// <param name="terminate"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        private void ReConstructionDataTable(DataTable original, DataTable terminate, int parentId)
        {

            DataTable dt = original;
            DataRow[] rows = dt.Select(" FMPARENTID=" + parentId, " FMSORTNUM DESC  ");
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    object[] o = new object[] { row["FMID"], row["FMPARENTID"], row["FMNAME"], 
                      row["FMPAGEURL"],row["FMDESC"],row["FMSORTNUM"]};
                    terminate.Rows.Add(o);
                    ReConstructionDataTable(original, terminate, System.Convert.ToInt32(row["FMID"]));
                }
            }

        }

        /// <summary>
        /// 重新构建DataTable
        /// </summary>
        /// <param name="original"></param>
        /// <param name="terminate"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        private void ReConstructionCompanyFunDataTable(DataTable original, DataTable terminate, int parentId)
        {

            DataTable dt = original;
            DataRow[] rows = dt.Select(" CFPARENTID=" + parentId, " CFSORTNUM DESC  ");
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    object[] o = new object[] { row["FMID"], row["CFPARENTID"], row["CFANOTHERNAME"], 
                      row["CFPAGEURL"],row["CFDESC"],row["CFSORTNUM"]};
                    terminate.Rows.Add(o);
                    ReConstructionCompanyFunDataTable(original, terminate, System.Convert.ToInt32(row["FMID"]));
                }
            }

        }

        /// <summary>
        /// 加载项目的功能菜单
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string LoadFunMenu(HttpContext context)
        {
            StringBuilder sbContent = new StringBuilder();
            string strProjectId = CommonMethod.FinalString(context.Request.QueryString["pid"]);
            string strProductId = CommonMethod.FinalString(context.Request.QueryString["did"]);
            DataTable dtProjectFunMenu = ProjectFunBusiness.GetFunMenuList(" AND PROJECTID=" + strProjectId);
            DataRow[] drowCurFun = dtProjectFunMenu.Select(" FMPARENTID=0 ", "FMSORTNUM DESC");
            string strChecked = string.Empty;

            for (int i = 0; i < drowCurFun.Length; i++)
            {
                if (strProductId.Length > 0)//修改时判断是否选中
                {
                    strChecked = ProductBusiness.IsProductExistFun(strProductId, drowCurFun[i]["FMID"].ToString()) ? "checked='checked'" : "";
                }
                sbContent.Append("<table class=\"table\" style=\"float:left; width:33%; margin-left:3px;\">");
                sbContent.AppendFormat("<tr><td class='rhead' style=\"text-align:left;background-image: url('../../Resource/images/searchthead.gif');\" ><input type='checkbox' onclick='CheckAll({1});' name='{0}' id='{1}' value='{1}' {3} />{2}</td></tr>",
                    "fun", drowCurFun[i]["FMID"], drowCurFun[i]["FMNAME"], strChecked);

                //下面的子菜单
                ReConstructionDataRow(dtProjectFunMenu, System.Convert.ToInt32(drowCurFun[i]["FMID"]), sbContent, strProductId, drowCurFun[i]["FMID"].ToString());

                sbContent.Append("</table>");
                if ((i + 1) % 3 == 0)
                {
                    sbContent.Append("</br>");
                }
            }
            return sbContent.ToString();
        }

        /// <summary>
        /// 子功能获取
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>

        private void ReConstructionDataRow(DataTable dt, int parentId, StringBuilder sbContent, string strProductId, string strRootId)
        {
            string strPre = "&nbsp;&nbsp;";
            string strChecked = string.Empty;

            DataRow[] rows = dt.Select(" FMPARENTID=" + parentId, " FMSORTNUM DESC  ");

            if (rows != null && rows.Length > 0)
            {
                int nLastParentId = 0;
                for (int i = 0; i < rows.Length; i++)
                {
                    if (strProductId.Length > 0)//修改时判断是否选中
                    {
                        strChecked = ProductBusiness.IsProductExistFun(strProductId, rows[i]["FMID"].ToString()) ? "checked='checked'" : "";
                    }

                    sbContent.AppendFormat("<tr><td title=\"{5}\">" + strPre + "<input type='checkbox' class='{4}' name='{0}' id='{1}' value='{1}' {3} />{2}</td></tr>",
                        "fun", rows[i]["FMID"], rows[i]["FMNAME"], strChecked, strRootId, CommonMethod.FinalString(rows[i]["FMDESC"]));
                    nLastParentId = parentId;
                    ReConstructionDataRow(dt, System.Convert.ToInt32(rows[i]["FMID"]), sbContent, strProductId, strRootId);
                }
            }

        }

        /// <summary>
        /// 加载公司的功能菜单信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string LoadCompanyFunMenu(HttpContext context)
        {
            StringBuilder sbContent = new StringBuilder();
            string strRoleId = CommonMethod.FinalString(context.Request.QueryString["Roleid"]);
            string strProjectId = CommonMethod.FinalString(context.Request.QueryString["ProjectId"]);
            string strWhere = string.Format(" AND PROJECTID={0} AND COMPANYID={1} ", strProjectId, CompanyId);

            DataTable dtCompanyFunMenu = CompanyFunBusiness.GetCompanyFunList(strWhere);
            DataRow[] drowCurFun = dtCompanyFunMenu.Select(" CFPARENTID=0 ", "CFSORTNUM DESC");

            string strChecked = string.Empty;

            for (int i = 0; i < drowCurFun.Length; i++)
            {
                if (strRoleId.Length > 0)//修改时判断是否选中
                {
                    strChecked = CompanyFunBusiness.IsRoleExistFun(strRoleId, drowCurFun[i]["CFID"].ToString()) ? "checked='checked'" : "";
                }
                sbContent.Append("<table class=\"table\" style=\"float:left; width:33%; margin-left:3px;\">");
                sbContent.AppendFormat("<tr><td class='rhead' style=\"text-align:left;background-image: url('../../Resource/images/searchthead.gif');\" ><input type='checkbox' onclick='CheckAll({1});' name='{0}' id='{1}' value='{1}' {3} />{2}</td></tr>",
                    "fun", drowCurFun[i]["CFID"], drowCurFun[i]["CFANOTHERNAME"], strChecked);

                //下面的子菜单
                GetCompanyChildFun(dtCompanyFunMenu, System.Convert.ToInt32(drowCurFun[i]["FMID"]), sbContent, strRoleId, drowCurFun[i]["CFID"].ToString());

                sbContent.Append("</table>");
                if ((i + 1) % 3 == 0)
                {
                    sbContent.Append("</br>");
                }
            }
            return sbContent.ToString();
        }

        /// <summary>
        /// 公司子功能获取
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>

        private void GetCompanyChildFun(DataTable dt, int parentId, StringBuilder sbContent, string strRoleId, string strRootId)
        {
            string strPre = "&nbsp;&nbsp;";
            string strChecked = string.Empty;

            DataRow[] rows = dt.Select(" CFPARENTID=" + parentId, " CFSORTNUM DESC  ");

            if (rows != null && rows.Length > 0)
            {
                for (int i = 0; i < rows.Length; i++)
                {
                    if (strRoleId.Length > 0)//修改时判断是否选中
                    {
                        strChecked = CompanyFunBusiness.IsRoleExistFun(strRoleId, rows[i]["CFID"].ToString()) ? "checked='checked'" : "";
                    }

                    sbContent.AppendFormat("<tr><td >" + strPre + "<input type='checkbox' class='{4}' name='{0}' id='{1}' value='{1}' {3} />{2}</td></tr>",
                        "fun", rows[i]["CFID"], rows[i]["CFANOTHERNAME"], strChecked, strRootId);

                    GetCompanyChildFun(dt, System.Convert.ToInt32(rows[i]["FMID"]), sbContent, strRoleId, strRootId);
                }
            }

        }

        /// <summary>
        /// 加载公司分组
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string LoadGroup(HttpContext context)
        {
            string strcName = GlobalObject.unescape(context.Request.QueryString["cname"]);
            List<GroupJsonModel> lstGroupModel = PlatFormBusiness.GetGroupList(strcName);
            return JavaScriptConvert.SerializeObject(lstGroupModel);
        }

        /// <summary>
        /// 加载公司信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string LoadCompany(HttpContext context)
        {
            string strcName = GlobalObject.unescape(context.Request.QueryString["cname"]);
            int nType = ValidatorHelper.ToInt(context.Request.QueryString["ctype"], 0);
            List<CompanyJsonModel> lstCompanyModel = PlatFormBusiness.GetCompanyList(nType, strcName);
            return JavaScriptConvert.SerializeObject(lstCompanyModel);
        }

        /// <summary>
        /// 加载公司分组
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string LoadCompanyGroup(HttpContext context)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            DataTable terminate = new DataTable();
            terminate.Columns.Add("ID");
            terminate.Columns.Add("PARENTID");
            terminate.Columns.Add("GROUPNAME");
            terminate.Columns.Add("GROUPDESC");
            terminate.Columns.Add("GRADE");

            DataTable dtCompanyGroup = CompanyGroupBusiness.GetCompanyGroupList(" AND COMPANYCODE=" + CompanyCode);
            ReConstructionCompanyGroupDataTable(dtCompanyGroup, terminate, 0);

            DataRow row;
            DataRowCollection drc = terminate.Rows;
            int rowCount = drc.Count;

            if (rowCount > 0)
            {

                for (int i = 0; i < rowCount; i++)
                {
                    row = drc[i];
                    sb.Append("[");
                    sb.AppendFormat("{0},{1},", row["ID"], row["PARENTID"]);
                    sb.Append("[");
                    sb.AppendFormat("'{0}','{1}','{2}','{3}'", row["GROUPNAME"], row["GROUPDESC"], Enc.Encrypt(row["ID"].ToString(), UrlEncKey), row["GRADE"]);
                    sb.Append("]");
                    if (i != rowCount - 1)
                    {
                        sb.Append("],");
                    }
                    else
                    {
                        sb.Append("]");
                    }
                }

                sb.Append("]");
            }


            return sb.ToString();
        }

        /// <summary>
        /// 重新构建DataTable
        /// </summary>
        /// <param name="original"></param>
        /// <param name="terminate"></param>
        /// <param name="refId"></param>
        /// <returns></returns>
        private void ReConstructionCompanyGroupDataTable(DataTable original, DataTable terminate, int parentId)
        {

            DataTable dt = original;
            DataRow[] rows = dt.Select(" PARENTID=" + parentId, " ID DESC  ");
            if (rows.Length > 0)
            {
                foreach (DataRow row in rows)
                {
                    object[] o = new object[] { row["ID"], row["PARENTID"], row["GROUPNAME"], row["GROUPDESC"], row["GRADE"] };

                    terminate.Rows.Add(o);
                    ReConstructionCompanyGroupDataTable(original, terminate, System.Convert.ToInt32(row["ID"]));
                }
            }

        }

        /// <summary>
        /// 删除一个车辆分组
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string DelGroup(HttpContext context)
        {
            int nGroupId = ValidatorHelper.ToInt(Enc.Decrypt(context.Request.QueryString["gid"], UrlEncKey), 0);
            //日志记录
            USER_SHARE_LOGMODEL logModel = new USER_SHARE_LOGMODEL();
            logModel.LOGID = CommonBusiness.GetSeqID("S_USER_SHARE_LOG");
            logModel.OPERATEDATE = DateTime.Now;
            logModel.OPERATORID = AccountId;
            logModel.PROJECTID = ProjectId;
            logModel.COMPANYID = CompanyId;
            logModel.OPERATETYPE = int.Parse(ShareEnum.LogType.ChangeProjectFunMenuStatus.ToString("d"));
            logModel.OPERATECONTENT = "设置车辆分组状态为" + EnumPlus.GetEnumDescription(typeof(ShareEnum.LogType), logModel.OPERATETYPE.ToString());
            return CompanyGroupBusiness.SetGroupStop(nGroupId, logModel) ? "0" : "1";
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