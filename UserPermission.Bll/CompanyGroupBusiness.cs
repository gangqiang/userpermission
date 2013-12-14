using System;
using System.Collections.Generic;
using System.Text;
using UserPermission.Model;
using SAMURAI.Data.Parameter;
using SAMURAI.Data.Connection;
using UserPermission.Utils;
using System.Data;

namespace UserPermission.Bll
{
    public class CompanyGroupBusiness
    {
        /// <summary>
        /// 新增公司分组信息
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        public static bool AddCompanyGroup(USER_SHARE_GROUPMODEL groupModel, List<USER_SHARE_VEHICLE_GROUPMODEL> lstVgModel, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into USER_SHARE_GROUP(");
            strSql.Append("ID,COMPANYCODE,GROUPNAME,PARENTID,STATE,GRADE,VEHICLENUM,GROUPDESC)");
            strSql.Append(" values (");
            strSql.Append(":ID,:COMPANYCODE,:GROUPNAME,:PARENTID,:STATE,:GRADE,:VEHICLENUM,:GROUPDESC)");
            ParamList param = new ParamList();
            param["ID"] = groupModel.ID;
            param["COMPANYCODE"] = groupModel.COMPANYCODE;
            param["GROUPNAME"] = groupModel.GROUPNAME;
            param["PARENTID"] = groupModel.PARENTID;
            param["STATE"] = groupModel.STATE;
            param["GRADE"] = groupModel.GRADE;
            param["VEHICLENUM"] = groupModel.VEHICLENUM;
            param["GROUPDESC"] = groupModel.GROUPDESC;
            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();
            try
            {

                using (connection)
                {
                    connection.BeginTranscation();
                    //增加分组信息
                    connection.ExecuteNonQuery(strSql.ToString(), param);
                    param.Clear();

                    string strSqlFun = string.Empty;

                    //分组车辆 

                    foreach (USER_SHARE_VEHICLE_GROUPMODEL vgModel in lstVgModel)
                    {
                        strSqlFun = string.Format("insert into USER_SHARE_VEHICLE_GROUP(SHAREGROUPID,TARGETID,MACID)values(:SHAREGROUPID,:TARGETID,:MACID)");
                        param.Clear();
                        param["SHAREGROUPID"] = groupModel.ID;
                        param["TARGETID"] = vgModel.TARGETID;
                        param["MACID"] = vgModel.MACID;
                        connection.ExecuteNonQuery(strSqlFun, param);
                    }

                    //操作日志
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
                LogHelper.WriteErr("新增公司车辆分组信息时发生错误，分组名称：" + groupModel.GROUPNAME, ex);
            }

            return blSuccess;

        }

        /// <summary>
        /// 修改公司分组信息
        /// </summary>
        /// <param name="groupModel"></param>
        /// <returns></returns>
        public static bool EditCompanyGroup(USER_SHARE_GROUPMODEL groupModel, List<USER_SHARE_VEHICLE_GROUPMODEL> lstVgModel, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update USER_SHARE_GROUP set ");
            strSql.Append("COMPANYCODE=:COMPANYCODE,");
            strSql.Append("GROUPNAME=:GROUPNAME,");
            strSql.Append("PARENTID=:PARENTID,");
            strSql.Append("STATE=:STATE,");
            strSql.Append("GRADE=:GRADE,");
            strSql.Append("VEHICLENUM=:VEHICLENUM,");
            strSql.Append("GROUPDESC=:GROUPDESC");
            strSql.Append(" where ID=:ID ");

            ParamList param = new ParamList();
            param["ID"] = groupModel.ID;
            param["COMPANYCODE"] = groupModel.COMPANYCODE;
            param["GROUPNAME"] = groupModel.GROUPNAME;
            param["PARENTID"] = groupModel.PARENTID;
            param["STATE"] = groupModel.STATE;
            param["GRADE"] = groupModel.GRADE;
            param["VEHICLENUM"] = groupModel.VEHICLENUM;
            param["GROUPDESC"] = groupModel.GROUPDESC;

            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();
            try
            {

                using (connection)
                {
                    connection.BeginTranscation();
                    //修改公司分组信息
                    connection.ExecuteNonQuery(strSql.ToString(), param);
                    param.Clear();

                    //删除原来分组下的车辆信息
                    string strSqlFun = "DELETE FROM USER_SHARE_VEHICLE_GROUP WHERE SHAREGROUPID=" + groupModel.ID;
                    StaticConnectionProvider.ExecuteNonQuery(strSqlFun);

                    //分组车辆信息添加
                    foreach (USER_SHARE_VEHICLE_GROUPMODEL vgModel in lstVgModel)
                    {
                        strSqlFun = @"insert into USER_SHARE_VEHICLE_GROUP(SHAREGROUPID,TARGETID,MACID)
                                       values (:SHAREGROUPID,:TARGETID,:MACID)";
                        param.Clear();
                        param["SHAREGROUPID"] = groupModel.ID;
                        param["TARGETID"] = vgModel.TARGETID;
                        param["MACID"] = vgModel.MACID;
                        connection.ExecuteNonQuery(strSqlFun, param);
                    }

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
                LogHelper.WriteErr("修改公司分组信息时发生错误，分组Id：" + groupModel.ID, ex);
            }

            return blSuccess;

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static USER_SHARE_GROUPMODEL GetGroupModel(int groupid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,COMPANYCODE,GROUPNAME,PARENTID,STATE,GRADE,GROUPDESC FROM USER_SHARE_GROUP ");
            strSql.Append(" where ID=:ID AND STATE=:STATE");
            ParamList param = new ParamList();
            param["ID"] = groupid;
            param["STATE"] = ShareEnum.CompanyGroupStatus.Normal.ToString("d");
            USER_SHARE_GROUPMODEL model = null;
            DataTable dt = StaticConnectionProvider.ExecuteDataTable(strSql.ToString(), param);
            if (dt != null && dt.Rows.Count > 0)
            {
                model = ReaderBind(dt.Rows[0]);
            }

            return model;
        }

        /// <summary>
        ///  停用车辆分组
        /// </summary>
        /// <param name="nFmId"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static bool SetGroupStop(int groupid, USER_SHARE_LOGMODEL log)
        {
            bool blResult = false;

            string strSqls = "UPDATE USER_SHARE_GROUP SET STATE=:STATE WHERE  ID=:ID";

            ParamList param = new ParamList();
            param["ID"] = groupid;
            param["STATE"] = ShareEnum.CompanyGroupStatus.StopUse.ToString("d");

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
                LogHelper.WriteErr("将车辆分组置为无效时出现异常，Id:" + groupid, ex);
            }
            return blResult;
        }

        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        private static USER_SHARE_GROUPMODEL ReaderBind(DataRow dataReader)
        {
            USER_SHARE_GROUPMODEL model = new UserPermission.Model.USER_SHARE_GROUPMODEL();

            model.ID = ValidatorHelper.ToInt(dataReader["ID"], 0);
            model.COMPANYCODE = ValidatorHelper.ToInt(dataReader["COMPANYCODE"], 0);
            model.GROUPNAME = CommonMethod.FinalString(dataReader["GROUPNAME"]);
            model.PARENTID = ValidatorHelper.ToInt(dataReader["PARENTID"], 0);
            model.STATE = ValidatorHelper.ToInt(dataReader["STATE"], 0);
            model.GRADE = CommonMethod.FinalString(dataReader["GRADE"]);
            model.GROUPDESC = CommonMethod.FinalString(dataReader["GROUPDESC"]);
            return model;
        }

        /// <summary>
        /// 获得数据列表 
        /// </summary>
        public static DataTable GetCompanyGroupList(string strWhere)
        {
            string strSql = @"select ID,COMPANYCODE,GROUPNAME,PARENTID,STATE,GRADE,GROUPDESC FROM USER_SHARE_GROUP WHERE STATE={0} " + strWhere;
            strSql += " ORDER BY ID ASC ";
            return StaticConnectionProvider.ExecuteDataTable(string.Format(strSql, ShareEnum.CompanyGroupStatus.Normal.ToString("d")));
        }

        /// <summary>
        /// 判断分组下是否包含某辆车
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="macid"></param>
        /// <returns></returns>
        public static bool IsGroupContainVehicel(int groupid, string macid)
        {
            string strSql = "SELECT COUNT(*) FROM USER_SHARE_VEHICLE_GROUP WHERE SHAREGROUPID=" + groupid + " AND MACID='" + macid + "'";
            return ValidatorHelper.ToInt(StaticConnectionProvider.ExecuteScalar(strSql), 0) > 0;
        }

        /// <summary>
        /// 判断角色下是否包含某分组
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="macid"></param>
        /// <returns></returns>
        public static bool IsRoleContainGroup(int roleid, string groupid)
        {
            string strSql = "SELECT COUNT(*) FROM USER_SHARE_ROLE_GROUP WHERE SHAREGROUPID=" + groupid + " AND ROLEID=" + roleid + "";
            return ValidatorHelper.ToInt(StaticConnectionProvider.ExecuteScalar(strSql), 0) > 0;
        }

        /// <summary>
        /// 得到分组的层级
        /// </summary>
        /// <param name="nParentId"></param>
        /// <returns></returns>
        public static string GetGroupGrade(int nCompanyCode, int nParentId)
        {
            string strGrade = string.Empty;
            int lastId = nParentId;
            DataTable dt = GetCompanyGroupList(" AND COMPANYCODE=" + nCompanyCode);
            DataRow[] dr = null;

            while (nParentId > 0)
            {
                dr = dt.Select("ID=" + nParentId, "");
                nParentId = ValidatorHelper.ToInt(dr[0]["PARENTID"], 0);
                strGrade += nParentId + "|";
            }

            strGrade = strGrade.TrimEnd('|');

            if (lastId > 0)
            {
                strGrade += "|" + lastId;
            }
            return strGrade;
        }

        /// <summary>
        /// 根据账号Id得到可以查看的车辆信息
        /// </summary>
        /// <param name="nAccountId"></param>
        /// <returns></returns>
        public static DataTable GetAccountVehicel(USER_SHARE_ACCOUNTMODEL account)
        {
            string strSql = string.Empty;
            if (!(account.ISADMIN == 1))
            {
                strSql = string.Format(@" SELECT RTRIM(LTRIM(ROLEIDS,','),',') FROM USER_SHARE_ACCOUNT WHERE 
                                          ACCOUNTID={0} AND COMPANYID={1} AND STATUS={2} ", account.ACCOUNTID, account.COMPANYID,
                                          ShareEnum.AccountStatus.Normal.ToString("d"));

                string roleids = CommonMethod.FinalString(StaticConnectionProvider.ExecuteScalar(strSql));
                if (roleids.Length > 0)
                {
                    strSql = string.Format(@"SELECT * FROM USER_SHARE_VEHICLE_GROUP WHERE SHAREGROUPID IN(
                                             SELECT DISTINCT(SHAREGROUPID) FROM USER_SHARE_ROLE_GROUP WHERE ROLEID IN({0})
                                             )", roleids);
                }
                else
                {
                    return null;
                }
            }

            else
            {
                strSql = string.Format(@"SELECT * FROM USER_SHARE_VEHICLE_GROUP WHERE SHAREGROUPID IN(
                                         SELECT ID FROM USER_SHARE_GROUP WHERE COMPANYCODE={0}
                                         AND STATE={1} )", account.COMPANYID, ShareEnum.CompanyGroupStatus.Normal.ToString("d"));
            }

            return StaticConnectionProvider.ExecuteDataTable(strSql);
        }

    }
}
