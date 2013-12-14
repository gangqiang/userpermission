using System;
using System.Collections.Generic;
using System.Text;
using UserPermission.Utils;
using System.Data;
using SAMURAI.Data.Connection;
using UserPermission.Model;
using SAMURAI.Data.Parameter;

namespace UserPermission.Bll
{
    public class RoleBusiness
    {

        /// 获得数据列表 
        /// </summary>
        public static DataTable GetRoleList(int nPageIndex, int nPageSize, string strWhere, out int nCount)
        {
            int startIndex = nPageIndex * nPageSize;
            int endIndex = (nPageIndex + 1) * nPageSize;

            string strSqlCount = "Select COUNT(*) FROM USER_SHARE_ROLES R WHERE R.STATUS= " + ShareEnum.RoleStatus.Normal.ToString("d") + " " + strWhere;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ROLEID,ROLENAME,ROLEDESC,R.PROJECTID,R.COMPANYID, A.REALNAME,R.CREATEDATE,R.STATUS ");
            strSql.Append(" FROM USER_SHARE_ROLES R INNER JOIN USER_SHARE_ACCOUNT A ON R.CREATORID=A.ACCOUNTID ");
            strSql.Append(" WHERE R.STATUS= " + ShareEnum.RoleStatus.Normal.ToString("d") + " " + strWhere);
            strSql.Append(" ORDER BY ROLEID DESC ");
            object objCount = StaticConnectionProvider.ExecuteScalar(strSqlCount);

            nCount = ValidatorHelper.ToInt(objCount, 0);

            string strFinalSql = string.Format("SELECT *FROM (SELECT R.*, ROWNUM RN FROM ({0})  R) WHERE RN>{1} AND RN<={2} ", strSql, startIndex, endIndex);
            return StaticConnectionProvider.ExecuteDataTable(strFinalSql);
        }


        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="roleModel"></param>
        /// <returns></returns>
        public static bool AddARole(USER_SHARE_ROLESMODEL roleModel, string strFunIds, string strGroupIds, List<RoleAccountModel> lstRaModel, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into USER_SHARE_ROLES(");
            strSql.Append("ROLEID,ROLENAME,ROLEDESC,PROJECTID,COMPANYID,STATUS,CREATORID,CREATEDATE)");
            strSql.Append(" values (");
            strSql.Append(":ROLEID,:ROLENAME,:ROLEDESC,:PROJECTID,:COMPANYID,:STATUS,:CREATORID,:CREATEDATE)");
            ParamList param = new ParamList();
            param["ROLEID"] = roleModel.ROLEID;
            param["ROLENAME"] = roleModel.ROLENAME;
            param["ROLEDESC"] = roleModel.ROLEDESC;
            param["PROJECTID"] = roleModel.PROJECTID;
            param["COMPANYID"] = roleModel.COMPANYID;
            param["STATUS"] = roleModel.STATUS;
            param["CREATORID"] = roleModel.CreatorId;
            param["CREATEDATE"] = roleModel.CreateDate;

            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();
            try
            {

                using (connection)
                {
                    connection.BeginTranscation();
                    //增加角色信息
                    connection.ExecuteNonQuery(strSql.ToString(), param);
                    param.Clear();

                    string strSqlFun = string.Empty;

                    //角色功能
                    if (strFunIds.Trim().Length > 0)
                    {
                        string[] funId = strFunIds.Split(',');

                        foreach (string str in funId)
                        {
                            if (str.Trim().Length > 0)
                            {
                                strSqlFun = string.Format("INSERT INTO USER_SHARE_ROLEFUN(ROLEID,FUNID) VALUES({0},{1}) ", roleModel.ROLEID, str);
                                connection.ExecuteNonQuery(strSqlFun);
                            }
                        }
                    }

                    //角色下的账号更新
                    foreach (RoleAccountModel raModel in lstRaModel)
                    {
                        //新增时选中的账号，角色更新，未选中的不用做处理
                        if (raModel.IsChecked)
                        {
                            strSqlFun = string.Format("UPDATE USER_SHARE_ACCOUNT SET ROLEIDS=(ROLEIDS||(CASE WHEN ROLEIDS IS NULL THEN ',{0},' ELSE '{0},' END )) WHERE ACCOUNTID={1} ", roleModel.ROLEID, raModel.AccountId);
                            connection.ExecuteNonQuery(strSqlFun);
                        }
                    }

                    //拥有权限的车辆分组
                    if (strGroupIds.Trim().Length > 0)
                    {
                        string[] funId = strGroupIds.Split(',');

                        foreach (string str in funId)
                        {
                            if (str.Trim().Length > 0)
                            {
                                strSqlFun = string.Format("INSERT INTO USER_SHARE_ROLE_GROUP(ROLEID,SHAREGROUPID) VALUES({0},{1}) ", roleModel.ROLEID, str);
                                connection.ExecuteNonQuery(strSqlFun);
                            }
                        }
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
                LogHelper.WriteErr("新增角色信息时发生错误，角色名称：" + roleModel.ROLENAME, ex);
            }

            return blSuccess;

        }

        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="roleModel"></param>
        /// <returns></returns>
        public static bool EditRole(USER_SHARE_ROLESMODEL roleModel, string strFunIds, string strGroupIds, List<RoleAccountModel> lstRaModel, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update USER_SHARE_ROLES set ");
            strSql.Append("ROLENAME=:ROLENAME,");
            strSql.Append("ROLEDESC=:ROLEDESC,");
            strSql.Append("PROJECTID=:PROJECTID,");
            strSql.Append("COMPANYID=:COMPANYID,");
            strSql.Append("STATUS=:STATUS");
            strSql.Append(" where ROLEID=:ROLEID ");

            ParamList param = new ParamList();
            param["ROLEID"] = roleModel.ROLEID;
            param["ROLENAME"] = roleModel.ROLENAME;
            param["ROLEDESC"] = roleModel.ROLEDESC;
            param["PROJECTID"] = roleModel.PROJECTID;
            param["COMPANYID"] = roleModel.COMPANYID;
            param["STATUS"] = roleModel.STATUS;

            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();
            try
            {

                using (connection)
                {
                    connection.BeginTranscation();
                    //修改账号信息
                    connection.ExecuteNonQuery(strSql.ToString(), param);
                    param.Clear();

                    //角色功能

                    //删除旧的信息
                    string strSqlFun = "DELETE FROM USER_SHARE_ROLEFUN WHERE ROLEID=" + roleModel.ROLEID;
                    connection.ExecuteNonQuery(strSqlFun);

                    //新的信息插入
                    if (strFunIds.Trim().Length > 0)
                    {
                        string[] funId = strFunIds.Split(',');

                        foreach (string str in funId)
                        {
                            if (str.Trim().Length > 0)
                            {
                                strSqlFun = string.Format("INSERT INTO USER_SHARE_ROLEFUN(ROLEID,FUNID) VALUES({0},{1}) ", roleModel.ROLEID, str);
                                connection.ExecuteNonQuery(strSqlFun);
                            }
                        }
                    }

                    //角色下的账号更新
                    foreach (RoleAccountModel raModel in lstRaModel)
                    {
                        //修改时账号角色更新
                        if (raModel.IsChecked)
                        {
                            strSqlFun = string.Format(@"UPDATE USER_SHARE_ACCOUNT SET ROLEIDS=(CASE WHEN REPLACE(ROLEIDS,',{0},',',')=',' OR ROLEIDS  IS NULL 
                                                        THEN ',{0},' ELSE REPLACE(ROLEIDS,',{0},',',')||'{0},'  end) WHERE ACCOUNTID={1} ", roleModel.ROLEID, raModel.AccountId);

                        }
                        else
                        {
                            strSqlFun = string.Format(@"UPDATE USER_SHARE_ACCOUNT SET ROLEIDS=(CASE WHEN REPLACE(ROLEIDS,',{0},',',')=',' OR ROLEIDS  IS NULL 
                                                        THEN '' ELSE REPLACE(ROLEIDS,',{0},',',')  end) WHERE ACCOUNTID={1} ", roleModel.ROLEID, raModel.AccountId);
                        }

                        connection.ExecuteNonQuery(strSqlFun);
                    }


                    //车辆分组

                    //删除旧的信息
                    strSqlFun = "DELETE FROM USER_SHARE_ROLE_GROUP WHERE ROLEID=" + roleModel.ROLEID;
                    connection.ExecuteNonQuery(strSqlFun);

                    //新的信息插入
                    if (strGroupIds.Trim().Length > 0)
                    {
                        string[] funId = strGroupIds.Split(',');

                        foreach (string str in funId)
                        {
                            if (str.Trim().Length > 0)
                            {
                                strSqlFun = string.Format("INSERT INTO USER_SHARE_ROLE_GROUP(ROLEID,SHAREGROUPID) VALUES({0},{1}) ", roleModel.ROLEID, str);
                                connection.ExecuteNonQuery(strSqlFun);
                            }
                        }
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
                LogHelper.WriteErr("修改角色信息时发生错误，角色Id：" + roleModel.ROLEID, ex);
            }

            return blSuccess;

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static USER_SHARE_ROLESMODEL GetRoleModel(int ROLEID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ROLEID,ROLENAME,ROLEDESC,PROJECTID,COMPANYID,STATUS from USER_SHARE_ROLES ");
            strSql.Append(" where ROLEID=:ROLEID AND STATUS=:STATUS ");
            ParamList param = new ParamList();
            param["ROLEID"] = ROLEID;
            param["STATUS"] = ShareEnum.RoleStatus.Normal.ToString("d");
            USER_SHARE_ROLESMODEL model = null;
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
        private static USER_SHARE_ROLESMODEL ReaderBind(DataRow dataReader)
        {
            USER_SHARE_ROLESMODEL model = new UserPermission.Model.USER_SHARE_ROLESMODEL();

            model.ROLEID = ValidatorHelper.ToInt(dataReader["ROLEID"], 0);
            model.ROLENAME = CommonMethod.FinalString(dataReader["ROLENAME"]);
            model.ROLEDESC = CommonMethod.FinalString(dataReader["ROLEDESC"]);
            model.PROJECTID = ValidatorHelper.ToInt(dataReader["PROJECTID"], 0);
            model.COMPANYID = ValidatorHelper.ToInt(dataReader["COMPANYID"], 0);
            model.STATUS = ValidatorHelper.ToInt(dataReader["STATUS"], 0);
            return model;
        }

        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="strAccountId"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public static bool DelRole(string strRoleId, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;
            StringBuilder strSql = new StringBuilder("UPDATE USER_SHARE_ROLES SET STATUS=:STATUS WHERE ROLEID=:ROLEID");
            ParamList param = new ParamList();
            param["ROLEID"] = strRoleId;
            param["STATUS"] = ShareEnum.RoleStatus.Del.ToString("d");
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
                LogHelper.WriteErr("删除角色信息时发生错误:" + ex.Message + ",角色Id:" + strRoleId, ex);
            }
            return blSuccess;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataTable GetAccountRoleList(string strWhere)
        {
            string strSql = @"SELECT R.ROLEID,R.ROLENAME,R.PROJECTID FROM USER_SHARE_ROLES R   
                              INNER JOIN USER_SHARE_PROJECT P ON P.PROJECTID=R.PROJECTID
                              WHERE R.STATUS={0} AND P.STATUS={1}  ";

            if (strWhere.Trim().Length > 0)
            {
                strSql += strWhere;
            }

            strSql = string.Format(strSql, ShareEnum.RoleStatus.Normal.ToString("d"), ShareEnum.ProjectStatus.Normal.ToString("d"));
            strSql += " ORDER BY P.CREATEDATE ASC, R.CREATEDATE ASC ";
            return StaticConnectionProvider.ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 根据角色查询公司拥有的项目信息
        /// </summary>
        /// <param name="nCompanyId"></param>
        /// <returns></returns>
        public static DataTable GetCompanyProjects(int nCompanyId)
        {
            string strSql = @"SELECT DISTINCT(R.PROJECTID),P.PROJECTNAME 
                             FROM USER_SHARE_ROLES R
                             INNER JOIN USER_SHARE_PROJECT P
                             ON P.PROJECTID = R.PROJECTID WHERE R.COMPANYID={0}
                             AND R.STATUS={1} AND P.STATUS={2} 
                             ORDER BY R.PROJECTID ASC  ";
            strSql = string.Format(strSql, nCompanyId, ShareEnum.RoleStatus.Normal.ToString("d"), ShareEnum.ProjectStatus.Normal.ToString("d"));
            return StaticConnectionProvider.ExecuteDataTable(strSql);
        }


    }
}
