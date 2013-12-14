using System;
using System.Collections.Generic;
using System.Text;
using SAMURAI.Data.Parameter;
using SAMURAI.Data.Connection;
using UserPermission.Model;
using System.Data;
using UserPermission.Utils;

namespace UserPermission.Bll
{
    public class AccountBusiness
    {

        /// <summary>
        /// 获得数据列表 
        /// </summary>
        public static DataTable GetAccountList(int nPageIndex, int nPageSize, string strWhere, out int nCount)
        {
            int startIndex = nPageIndex * nPageSize;
            int endIndex = (nPageIndex + 1) * nPageSize;

            string strSqlCount = "Select COUNT(*) FROM USER_SHARE_ACCOUNT  WHERE STATUS!=" + ShareEnum.AccountStatus.Del.ToString("d") + " " + strWhere;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  ACCOUNTID,ACCOUNTNAME,COMPANYID,ACCOUNTPWD,ORIGNALPWD,REALNAME,EMAIL,ROLEIDS,LINKPHONE,CREATEDATE,CREATORID,ISADMIN,STATUS ");
            strSql.Append(" FROM USER_SHARE_ACCOUNT ");
            strSql.Append(" WHERE STATUS!= " + ShareEnum.AccountStatus.Del.ToString("d") + " " + strWhere);
            strSql.Append(" ORDER BY ACCOUNTID DESC ");
            object objCount = StaticConnectionProvider.ExecuteScalar(strSqlCount);

            nCount = ValidatorHelper.ToInt(objCount, 0);

            string strFinalSql = string.Format("SELECT *FROM (SELECT R.*, ROWNUM RN FROM ({0})  R) WHERE RN>{1} AND RN<={2} ", strSql, startIndex, endIndex);
            return StaticConnectionProvider.ExecuteDataTable(strFinalSql);
        }

        /// <summary>
        /// 获得数据列表 
        /// </summary>
        public static DataTable GetAccountList(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  ACCOUNTID,ACCOUNTID||'$'|| ROLEIDS AS ROLEACCOUNTS,ACCOUNTNAME,ACCOUNTNAME||'('|| REALNAME || ')&nbsp;&nbsp;&nbsp;&nbsp;' AS ARNAME,COMPANYID,ACCOUNTPWD,ORIGNALPWD,REALNAME,EMAIL,ROLEIDS,LINKPHONE,CREATEDATE,CREATORID,ISADMIN,STATUS ");
            strSql.Append(" FROM USER_SHARE_ACCOUNT ");
            strSql.Append(" WHERE STATUS= " + ShareEnum.AccountStatus.Normal.ToString("d") + " " + strWhere);
            strSql.Append(" ORDER BY ACCOUNTID DESC ");
            return StaticConnectionProvider.ExecuteDataTable(strSql.ToString());
        }

        /// <summary>
        /// 新增账号信息
        /// </summary>
        /// <param name="AccountModel"></param>
        /// <returns></returns>
        public static bool AddAccount(USER_SHARE_ACCOUNTMODEL AccountModel, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into USER_SHARE_ACCOUNT(");
            strSql.Append("ACCOUNTID,ACCOUNTNAME,COMPANYID,ACCOUNTPWD,ORIGNALPWD,REALNAME,EMAIL,ROLEIDS,LINKPHONE,CREATEDATE,CREATORID,ISADMIN,STATUS)");
            strSql.Append(" values (");
            strSql.Append(":ACCOUNTID,:ACCOUNTNAME,:COMPANYID,:ACCOUNTPWD,:ORIGNALPWD,:REALNAME,:EMAIL,:ROLEIDS,:LINKPHONE,:CREATEDATE,:CREATORID,:ISADMIN,:STATUS)");
            ParamList param = new ParamList();
            param["ACCOUNTID"] = AccountModel.ACCOUNTID;
            param["ACCOUNTNAME"] = AccountModel.ACCOUNTNAME;
            param["COMPANYID"] = AccountModel.COMPANYID;
            param["ACCOUNTPWD"] = AccountModel.ACCOUNTPWD;
            param["ORIGNALPWD"] = AccountModel.ORIGNALPWD;
            param["REALNAME"] = AccountModel.REALNAME;
            param["EMAIL"] = AccountModel.EMAIL;
            param["ROLEIDS"] = AccountModel.ROLEIDS;
            param["LINKPHONE"] = AccountModel.LINKPHONE;
            param["CREATEDATE"] = AccountModel.CREATEDATE;
            param["CREATORID"] = AccountModel.CREATORID;
            param["ISADMIN"] = AccountModel.ISADMIN;
            param["STATUS"] = AccountModel.STATUS;

            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();
            try
            {

                using (connection)
                {
                    connection.BeginTranscation();
                    //增加账号信息
                    connection.ExecuteNonQuery(strSql.ToString(), param);
                    param.Clear();

                    if (AccountModel.ISADMIN == 1)//初始账号时更新公司关联信息表
                    {
                        string strUpSql = string.Format("UPDATE USER_SHARE_COMPANYRELATE SET ADMINID={0} WHERE COMPANYCODE={1} ",
                                          AccountModel.ACCOUNTID, AccountModel.COMPANYID);
                        connection.ExecuteNonQuery(strUpSql);
                    }

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
                    blSuccess = true;
                }
            }
            catch (Exception ex)
            {
                connection.RollbackTranscation();
                LogHelper.WriteErr("新增账号信息时发生错误，账号名称：" + AccountModel.ACCOUNTNAME, ex);
            }

            return blSuccess;

        }

        /// <summary>
        /// 修改账号信息
        /// </summary>
        /// <param name="AccountModel"></param>
        /// <returns></returns>
        public static bool EditAccount(USER_SHARE_ACCOUNTMODEL AccountModel, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update USER_SHARE_ACCOUNT set ");
            strSql.Append("ACCOUNTNAME=:ACCOUNTNAME,");
            strSql.Append("COMPANYID=:COMPANYID,");
            strSql.Append("ACCOUNTPWD=:ACCOUNTPWD,");
            strSql.Append("ORIGNALPWD=:ORIGNALPWD,");
            strSql.Append("REALNAME=:REALNAME,");
            strSql.Append("EMAIL=:EMAIL,");
            strSql.Append("ROLEIDS=:ROLEIDS,");
            strSql.Append("LINKPHONE=:LINKPHONE,");
            strSql.Append("CREATEDATE=:CREATEDATE,");
            strSql.Append("CREATORID=:CREATORID,");
            strSql.Append("ISADMIN=:ISADMIN,");
            strSql.Append("STATUS=:STATUS");
            strSql.Append(" where ACCOUNTID=:ACCOUNTID ");
            ParamList param = new ParamList();
            param["ACCOUNTID"] = AccountModel.ACCOUNTID;
            param["ACCOUNTNAME"] = AccountModel.ACCOUNTNAME;
            param["COMPANYID"] = AccountModel.COMPANYID;
            param["ACCOUNTPWD"] = AccountModel.ACCOUNTPWD;
            param["ORIGNALPWD"] = AccountModel.ORIGNALPWD;
            param["REALNAME"] = AccountModel.REALNAME;
            param["EMAIL"] = AccountModel.EMAIL;
            param["ROLEIDS"] = AccountModel.ROLEIDS;
            param["LINKPHONE"] = AccountModel.LINKPHONE;
            param["CREATEDATE"] = AccountModel.CREATEDATE;
            param["CREATORID"] = AccountModel.CREATORID;
            param["ISADMIN"] = AccountModel.ISADMIN;
            param["STATUS"] = AccountModel.STATUS;

            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();
            try
            {

                using (connection)
                {
                    connection.BeginTranscation();
                    //修改账号信息
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
                    blSuccess = true;
                }
            }
            catch (Exception ex)
            {
                connection.RollbackTranscation();
                LogHelper.WriteErr("修改账号信息时发生错误，账号名称：" + AccountModel.ACCOUNTNAME, ex);
            }

            return blSuccess;

        }

        /// <summary>
        /// 删除账号信息
        /// </summary>
        /// <param name="strAccountId"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public static bool DelAccount(string strAccountId, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;
            StringBuilder strSql = new StringBuilder("UPDATE USER_SHARE_ACCOUNT SET STATUS=:STATUS WHERE ACCOUNTID=:ACCOUNTID");
            ParamList param = new ParamList();
            param["ACCOUNTID"] = strAccountId;
            param["STATUS"] = ShareEnum.AccountStatus.Del.ToString("d");
            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();
            try
            {
                using (connection)
                {
                    connection.BeginTranscation();

                    //更新项目状态
                    connection.ExecuteNonQuery(strSql.ToString(), param);

                    //日志记录
                    strSql = new StringBuilder();
                    strSql.Append("insert into USER_SHARE_LOG(");
                    strSql.Append("LOGID,OPERATETYPE,OPERATORID,PROJECTID,COMPANYID,OPERATECONTENT,OPERATEDATE)");
                    strSql.Append(" values (");
                    strSql.Append(":LOGID,:OPERATETYPE,:OPERATORID,:PROJECTID,:COMPANYID,:OPERATECONTENT,:OPERATEDATE)");

                    param.Clear();
                    param["LOGID"] = log.LOGID;
                    param["OPERATETYPE"] = log.OPERATETYPE;
                    param["OPERATORID"] = log.OPERATORID;
                    param["PROJECTID"] = log.PROJECTID;
                    param["COMPANYID"] = log.COMPANYID;
                    param["OPERATECONTENT"] = log.OPERATECONTENT;
                    param["OPERATEDATE"] = log.OPERATEDATE;
                    connection.ExecuteNonQuery(strSql.ToString(), param);

                    connection.CommitTranscation();
                    blSuccess = true;
                }
            }
            catch (Exception ex)
            {
                connection.RollbackTranscation();
                LogHelper.WriteErr("删除账号时发生错误:" + ex.Message + ",账号Id:" + strAccountId, ex);
            }
            return blSuccess;
        }

        /// <summary>
        /// 获取账号信息
        /// </summary>
        /// <param name="nAccountId"></param>
        /// <returns></returns>
        public static USER_SHARE_ACCOUNTMODEL GetAccountModel(int nAccountId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ACCOUNTID,ACCOUNTNAME,COMPANYID,ACCOUNTPWD,ORIGNALPWD,REALNAME,EMAIL,ROLEIDS,LINKPHONE,CREATEDATE,CREATORID,ISADMIN,STATUS from USER_SHARE_ACCOUNT ");
            strSql.Append(" where ACCOUNTID=:ACCOUNTID AND STATUS!=:STATUS ");
            ParamList param = new ParamList();
            param["ACCOUNTID"] = nAccountId;
            param["STATUS"] = ShareEnum.AccountStatus.Del.ToString("d");
            USER_SHARE_ACCOUNTMODEL model = null;
            DataTable dt = StaticConnectionProvider.ExecuteDataTable(strSql.ToString(), param);
            if (dt != null && dt.Rows.Count > 0)
            {
                model = ReaderBind(dt.Rows[0]);
            }

            return model;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        private static USER_SHARE_ACCOUNTMODEL ReaderBind(DataRow dataReader)
        {
            USER_SHARE_ACCOUNTMODEL model = new USER_SHARE_ACCOUNTMODEL();
            model.ACCOUNTID = ValidatorHelper.ToInt(dataReader["ACCOUNTID"], 0);
            model.ACCOUNTNAME = ValidatorHelper.FinalString(dataReader["ACCOUNTNAME"]);
            model.COMPANYID = ValidatorHelper.ToInt(dataReader["COMPANYID"], 0);
            model.ACCOUNTPWD = ValidatorHelper.FinalString(dataReader["ACCOUNTPWD"]);
            model.ORIGNALPWD = ValidatorHelper.FinalString(dataReader["ORIGNALPWD"]);
            model.REALNAME = ValidatorHelper.FinalString(dataReader["REALNAME"]);
            model.EMAIL = ValidatorHelper.FinalString(dataReader["EMAIL"]);
            model.ROLEIDS = ValidatorHelper.FinalString(dataReader["ROLEIDS"]);
            model.LINKPHONE = ValidatorHelper.FinalString(dataReader["LINKPHONE"]);
            model.CREATEDATE = ValidatorHelper.ToDateTime(dataReader["CREATEDATE"], DateTime.Now);
            model.CREATORID = ValidatorHelper.ToInt(dataReader["CREATORID"], 0);
            model.ISADMIN = ValidatorHelper.ToInt(dataReader["ISADMIN"], 0);
            model.STATUS = ValidatorHelper.ToInt(dataReader["Status"], 0);
            return model;
        }

        /// <summary>
        /// 更改账号密码
        /// </summary>
        /// <param name="accountid"></param>
        /// <param name="newPwd"></param>
        /// <returns></returns>
        public static bool UpdatePwd(int accountid, string newPwd, string orignalpwd)
        {
            string strSql = @"UPDATE USER_SHARE_ACCOUNT SET ACCOUNTPWD=:ACCOUNTPWD,
                           ORIGNALPWD=:ORIGNALPWD WHERE ACCOUNTID=:ACCOUNTID ";
            ParamList param = new ParamList();
            param["ACCOUNTID"] = accountid;
            param["ACCOUNTPWD"] = newPwd;
            param["ORIGNALPWD"] = orignalpwd;

            return StaticConnectionProvider.ExecuteNonQuery(strSql, param) > 0;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static USER_SHARE_ACCOUNTMODEL GetAccountModel(string strCompanyCode, string strAccountName, string strPwd)
        {
            string strSql = "select * from USER_SHARE_ACCOUNT where ACCOUNTNAME=:ACCOUNTNAME AND COMPANYID=:COMPANYID AND ACCOUNTPWD=:ACCOUNTPWD  ";
            ParamList param = new ParamList();
            param["ACCOUNTNAME"] = strAccountName;
            param["ACCOUNTPWD"] = strPwd;
            param["COMPANYID"] = strCompanyCode;

            USER_SHARE_ACCOUNTMODEL model = null;

            DataTable dt = StaticConnectionProvider.ExecuteDataTable(strSql.ToString(), param);

            if (dt != null && dt.Rows.Count > 0)
            {
                model = ReaderBind(dt.Rows[0]);
            }

            return model;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static USER_SHARE_ACCOUNTMODEL GetAccountModel(string strAccountName, string strCompanyCode)
        {
            string strSql = "select * from USER_SHARE_ACCOUNT where ACCOUNTNAME=:ACCOUNTNAME AND COMPANYID=:COMPANYID AND STATUS!=:STATUS ";
            ParamList param = new ParamList();
            param["ACCOUNTNAME"] = strAccountName;
            param["COMPANYID"] = strCompanyCode;
            param["STATUS"] = ShareEnum.AccountStatus.Del.ToString("d");
            USER_SHARE_ACCOUNTMODEL model = null;
            DataTable dt = StaticConnectionProvider.ExecuteDataTable(strSql.ToString(), param);

            if (dt != null && dt.Rows.Count > 0)
            {
                model = ReaderBind(dt.Rows[0]);
            }

            return model;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool IsRoleUse(string roleid)
        {
            string strSql = "select COUNT(*) from USER_SHARE_ACCOUNT where ROLEIDS LIKE :ROLEIDS AND STATUS!=:STATUS ";
            ParamList param = new ParamList();
            param["ROLEIDS"] = "%" + "," + roleid + "," + "%";
            param["STATUS"] = ShareEnum.AccountStatus.Del.ToString("d");

            return ValidatorHelper.ToInt(StaticConnectionProvider.ExecuteScalar(strSql, param), 0) > 0;
        }

    }
}
