using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SAMURAI.Data.Connection;
using UserPermission.Utils;
using UserPermission.Model;
using SAMURAI.Data.Parameter;

namespace UserPermission.Bll
{
    public class CompanyFunBusiness
    {
        /// <summary>
        /// 根据账号信息,返回账号拥有的功能菜单
        /// </summary>
        /// <param name="nAccountId">账户ID</param>
        /// <param name="nProjectId">项目ID</param>
        /// <param name="nCompanyId">公司ID</param>
        /// <returns></returns>
        public static DataTable GetAccountFunMenu(int nAccountId, int nIsAdmin, int nProjectId, int nCompanyId)
        {
            string strSql = string.Empty;
            if (!(nIsAdmin == 1))
            {
                strSql = string.Format(@" SELECT RTRIM(LTRIM(ROLEIDS,','),',') FROM USER_SHARE_ACCOUNT WHERE 
                                          ACCOUNTID={0} AND COMPANYID={1} AND STATUS={2} ", nAccountId, nCompanyId,
                                          ShareEnum.AccountStatus.Normal.ToString("d"));

                string roleids = CommonMethod.FinalString(StaticConnectionProvider.ExecuteScalar(strSql));
                if (roleids.Length > 0)
                {
                    strSql = string.Format(@"SELECT * FROM USER_SHARE_COMPANYFUN WHERE CFID IN(
                              SELECT DISTINCT(FUNID) FROM USER_SHARE_ROLEFUN WHERE ROLEID IN({0})
                              ) AND PROJECTID={1} AND COMPANYID={2} AND CFSTATUS={3} 
                              ORDER BY CFSORTNUM DESC ", roleids, nProjectId, nCompanyId,
                              ShareEnum.CompanyFunMenuStatus.Normal.ToString("d"));
                }
                else
                {
                    return null;
                }
            }
            else
            {
                strSql = string.Format(@"SELECT * FROM USER_SHARE_COMPANYFUN WHERE  COMPANYID={0} AND PROJECTID={1} 
                           AND CFSTATUS={2} ORDER BY CFSORTNUM DESC ", nCompanyId, nProjectId, ShareEnum.CompanyFunMenuStatus.Normal.ToString("d"));
            }
            return StaticConnectionProvider.ExecuteDataTable(strSql);

        }

        /// <summary>
        /// 获得数据列表 
        /// </summary>
        public static DataTable GetCompanyFunList(int nPageIndex, int nPageSize, string strWhere, out int nCount)
        {
            int startIndex = nPageIndex * nPageSize;
            int endIndex = (nPageIndex + 1) * nPageSize;

            string strSqlCount = "Select COUNT(*) FROM USER_SHARE_COMPANYFUN  WHERE CFSTATUS=" + ShareEnum.CompanyFunMenuStatus.Normal.ToString("d") + " " + strWhere;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  CFID,FMID,PROJECTID,COMPANYID,CFNAME,CFANOTHERNAME,CFPAGEURL,CFPARENTID,CFSORTNUM,CFSTEP,CFISLAST,CFDESC,CFSTATUS ");
            strSql.Append(" FROM USER_SHARE_COMPANYFUN ");
            strSql.Append(" WHERE CFSTATUS= " + ShareEnum.CompanyFunMenuStatus.Normal.ToString("d") + " " + strWhere);

            object objCount = StaticConnectionProvider.ExecuteScalar(strSqlCount);

            nCount = ValidatorHelper.ToInt(objCount, 0);

            string strFinalSql = string.Format("SELECT *FROM (SELECT R.*, ROWNUM RN FROM ({0})  R) WHERE RN>{1} AND RN<={2} ", strSql, startIndex, endIndex);
            return StaticConnectionProvider.ExecuteDataTable(strFinalSql);
        }

        /// <summary>
        /// 获得数据列表 
        /// </summary>
        public static DataTable GetCompanyFunList(string strWhere)
        {
            string strSql = @"select CFID,FMID,PROJECTID,COMPANYID,CFNAME,CFANOTHERNAME,CFPAGEURL,CFPARENTID,CFSORTNUM,CFSTEP,CFISLAST,CFDESC,CFSTATUS 
                              FROM USER_SHARE_COMPANYFUN  WHERE CFSTATUS={0} " + strWhere;
            return StaticConnectionProvider.ExecuteDataTable(string.Format(strSql, ShareEnum.CompanyFunMenuStatus.Normal.ToString("d")));
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool UpdateCompanyFun(USER_SHARE_COMPANYFUNMODEL model, USER_SHARE_LOGMODEL log)
        {
            bool blResult = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update USER_SHARE_COMPANYFUN set ");
            strSql.Append("FMID=:FMID,");
            strSql.Append("PROJECTID=:PROJECTID,");
            strSql.Append("COMPANYID=:COMPANYID,");
            strSql.Append("CFNAME=:CFNAME,");
            strSql.Append("CFANOTHERNAME=:CFANOTHERNAME,");
            strSql.Append("CFPAGEURL=:CFPAGEURL,");
            strSql.Append("CFPARENTID=:CFPARENTID,");
            strSql.Append("CFSORTNUM=:CFSORTNUM,");
            strSql.Append("CFSTEP=:CFSTEP,");
            strSql.Append("CFISLAST=:CFISLAST,");
            strSql.Append("CFDESC=:CFDESC,");
            strSql.Append("CFSTATUS=:CFSTATUS");
            strSql.Append(" where CFID=:CFID ");

            ParamList param = new ParamList();
            param["CFID"] = model.CFID;
            param["FMID"] = model.FMID;
            param["PROJECTID"] = model.PROJECTID;
            param["COMPANYID"] = model.COMPANYID;
            param["CFNAME"] = model.CFNAME;
            param["CFANOTHERNAME"] = model.CFANOTHERNAME;
            param["CFPAGEURL"] = model.CFPAGEURL;
            param["CFPARENTID"] = model.CFPARENTID;
            param["CFSORTNUM"] = model.CFSORTNUM;
            param["CFSTEP"] = model.CFSTEP;
            param["CFISLAST"] = model.CFISLAST;
            param["CFDESC"] = model.CFDESC;
            param["CFSTATUS"] = model.CFSTATUS;

            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();
            try
            {

                using (connection)
                {
                    connection.BeginTranscation();

                    //增加项目信息
                    connection.ExecuteNonQuery(strSql.ToString(), param);
                    param.Clear();
                    //操作日志
                    strSql = new StringBuilder();
                    strSql.Append("insert into USER_SHARE_LOG(");
                    strSql.Append("LOGID,OPERATETYPE,OPERATORID,PROJECTID,COMPANYID,OPERATECONTENT,OPERATEDATE)");
                    strSql.Append(" values (");
                    strSql.Append(":LOGID,:OPERATETYPE,:OPERATORID,:PROJECTID,:COMPANYID,:OPERATECONTENT,:OPERATEDATE)");

                    param["LOGID"] = log.LOGID;
                    param["OPERATETYPE"] = log.OPERATETYPE;
                    param["OPERATORID"] = log.OPERATORID;
                    param["PROJECTID"] = log.PROJECTID;
                    param["COMPANYID"] = log.COMPANYID;
                    param["OPERATECONTENT"] = log.OPERATECONTENT;
                    param["OPERATEDATE"] = log.OPERATEDATE;
                    connection.ExecuteNonQuery(strSql.ToString(), param);

                    connection.CommitTranscation();
                    blResult = true;
                }
            }
            catch (Exception ex)
            {
                connection.RollbackTranscation();
                LogHelper.WriteErr("修改公司功能菜单时出现异常，Id:" + model.FMID, ex);
            }

            return blResult;
        }

        /// <summary>
        /// 判断某个角色下是否有某个功能
        /// </summary>
        /// <param name="strRoleId"></param>
        /// <param name="strFunId"></param>
        /// <returns></returns>
        public static bool IsRoleExistFun(string strRoleId, string strFunId)
        {
            string strSql = "SELECT COUNT(*) FROM USER_SHARE_ROLEFUN WHERE ROLEID=" + strRoleId + " AND FUNID=" + strFunId;
            return ValidatorHelper.ToInt(StaticConnectionProvider.ExecuteScalar(strSql), 0) > 0;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static USER_SHARE_COMPANYFUNMODEL GetCompanyFunModel(int FmID, int nCompanyId)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CFID,FMID,PROJECTID,COMPANYID,CFNAME,CFANOTHERNAME,CFPAGEURL,CFPARENTID,CFSORTNUM,CFSTEP,CFISLAST,CFDESC,CFSTATUS from USER_SHARE_COMPANYFUN ");
            strSql.Append(" where FMID=:FMID AND COMPANYID=:COMPANYID ");
            ParamList param = new ParamList();
            param["FMID"] = FmID;
            param["COMPANYID"] = nCompanyId;
            DataTable dt = StaticConnectionProvider.ExecuteDataTable(strSql.ToString(), param);

            USER_SHARE_COMPANYFUNMODEL model = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                model = ReaderBind(dt.Rows[0]);
            }

            return model;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        private static USER_SHARE_COMPANYFUNMODEL ReaderBind(DataRow dataReader)
        {
            USER_SHARE_COMPANYFUNMODEL model = new USER_SHARE_COMPANYFUNMODEL();

            model.CFID = ValidatorHelper.ToInt(dataReader["CFID"], 0);
            model.FMID = ValidatorHelper.ToInt(dataReader["FMID"], 0);
            model.PROJECTID = ValidatorHelper.ToInt(dataReader["PROJECTID"], 0);
            model.COMPANYID = ValidatorHelper.ToInt(dataReader["COMPANYID"], 0);
            model.CFNAME = CommonMethod.FinalString(dataReader["CFNAME"]);
            model.CFANOTHERNAME = dataReader["CFANOTHERNAME"].ToString();
            model.CFPAGEURL = dataReader["CFPAGEURL"].ToString();
            model.CFPARENTID = ValidatorHelper.ToInt(dataReader["CFPARENTID"], 0);
            model.CFSORTNUM = ValidatorHelper.ToInt(dataReader["CFSORTNUM"], 0);
            model.CFSTEP = CommonMethod.FinalString(dataReader["CFSTEP"]);
            model.CFISLAST = ValidatorHelper.ToInt(dataReader["CFISLAST"], 0);
            model.CFDESC = CommonMethod.FinalString(dataReader["CFDESC"]);
            model.CFSTATUS = ValidatorHelper.ToInt(dataReader["CFSTATUS"], 0);

            return model;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool ExistsChildFun(int FMID, int CompanyCode)
        {
            string strSql = "select count(1) from USER_SHARE_COMPANYFUN WHERE CFPARENTID= " + FMID + " AND COMPANYID=" + CompanyCode + " AND CFSTATUS=" + ShareEnum.CompanyFunMenuStatus.Normal.ToString("d");
            object obj = StaticConnectionProvider.ExecuteScalar(strSql);
            return ValidatorHelper.ToInt(obj, 0) > 0;
        }

        /// <summary>
        ///  停用功能菜单
        /// </summary>
        /// <param name="nFmId"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static bool SetFunStop(int nFmId, int nCompanyId, USER_SHARE_LOGMODEL log)
        {
            bool blResult = false;

            string strSqls = "UPDATE USER_SHARE_COMPANYFUN SET CFSTATUS=:CFSTATUS WHERE FMID=:FMID AND COMPANYID=:COMPANYID ";

            ParamList param = new ParamList();
            param["FMID"] = nFmId;
            param["COMPANYID"] = nCompanyId;
            param["CFSTATUS"] = ShareEnum.FunMenuStatus.StopUse.ToString("d");

            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();
            try
            {

                using (connection)
                {
                    connection.BeginTranscation();

                    //更改状态
                    connection.ExecuteNonQuery(strSqls, param);
                    param.Clear();
                    //操作日志
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into USER_SHARE_LOG(");
                    strSql.Append("LOGID,OPERATETYPE,OPERATORID,PROJECTID,COMPANYID,OPERATECONTENT,OPERATEDATE)");
                    strSql.Append(" values (");
                    strSql.Append(":LOGID,:OPERATETYPE,:OPERATORID,:PROJECTID,:COMPANYID,:OPERATECONTENT,:OPERATEDATE)");

                    param["LOGID"] = log.LOGID;
                    param["OPERATETYPE"] = log.OPERATETYPE;
                    param["OPERATORID"] = log.OPERATORID;
                    param["PROJECTID"] = log.PROJECTID;
                    param["COMPANYID"] = log.COMPANYID;
                    param["OPERATECONTENT"] = log.OPERATECONTENT;
                    param["OPERATEDATE"] = log.OPERATEDATE;
                    connection.ExecuteNonQuery(strSql.ToString(), param);

                    connection.CommitTranscation();
                    blResult = true;
                }
            }
            catch (Exception ex)
            {
                connection.RollbackTranscation();
                LogHelper.WriteErr("将公司功能菜单设置为无效时出现异常，Id:" + nFmId, ex);
            }
            return blResult;
        }
    }
}
