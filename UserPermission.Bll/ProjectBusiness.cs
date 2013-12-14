using System;
using System.Collections.Generic;
using System.Text;
using UserPermission.Model;
using UserPermission.Utils;
using System.Data;
using SAMURAI.Data;
using SAMURAI.Data.Parameter;
using SAMURAI.Data.Connection;

namespace UserPermission.Bll
{
    public class ProjectBusiness
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool AddProject(USER_SHARE_PROJECTMODEL model, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into USER_SHARE_PROJECT(");
            strSql.Append("PROJECTID,PROJECTNAME,APISERVICEKEY,CREATEDATE,PROJECTREMARK,STATUS)");
            strSql.Append(" values (");
            strSql.Append(":PROJECTID,:PROJECTNAME,:APISERVICEKEY,:CREATEDATE,:PROJECTREMARK,:STATUS)");
            ParamList param = new ParamList();
            param["PROJECTID"] = model.PROJECTID;
            param["PROJECTNAME"] = model.PROJECTNAME;
            param["APISERVICEKEY"] = model.APISERVICEKEY;
            param["CREATEDATE"] = model.CREATEDATE;
            param["PROJECTREMARK"] = model.PROJECTREMARK;
            param["STATUS"] = model.STATUS;

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
                    blSuccess = true;
                }
            }
            catch (Exception ex)
            {
                connection.RollbackTranscation();
                LogHelper.WriteErr("新增项目信息时发生错误，项目名称：" + model.PROJECTNAME, ex);
            }

            return blSuccess;
        }

        /// <summary>
        /// 更新项目信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static bool UpdateProject(USER_SHARE_PROJECTMODEL model, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update USER_SHARE_PROJECT set ");
            strSql.Append("PROJECTNAME=:PROJECTNAME,");
            strSql.Append("APISERVICEKEY=:APISERVICEKEY,");
            strSql.Append("CREATEDATE=:CREATEDATE,");
            strSql.Append("PROJECTREMARK=:PROJECTREMARK,");
            strSql.Append("STATUS=:STATUS");
            strSql.Append(" where PROJECTID=:PROJECTID ");
            ParamList param = new ParamList();
            param["PROJECTID"] = model.PROJECTID;
            param["PROJECTNAME"] = model.PROJECTNAME;
            param["APISERVICEKEY"] = model.APISERVICEKEY;
            param["CREATEDATE"] = model.CREATEDATE;
            param["PROJECTREMARK"] = model.PROJECTREMARK;
            param["STATUS"] = model.STATUS;

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
                    blSuccess = true;
                }
            }
            catch (Exception ex)
            {

                connection.RollbackTranscation();
                LogHelper.WriteErr("修改项目信息时发生错误:项目ID" + model.PROJECTID, ex);
            }

            return blSuccess;
        }

        /// <summary>
        /// 更新项目状态
        /// </summary>
        /// <param name="strProjectId"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public static bool UpdateProjectStatus(string strProjectId, string strStatus, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;
            StringBuilder strSql = new StringBuilder("UPDATE USER_SHARE_PROJECT SET STATUS=:STATUS WHERE PROJECTID=:PROJECTID");
            ParamList param = new ParamList();
            param["PROJECTID"] = strProjectId;
            param["STATUS"] = strStatus;
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
                LogHelper.WriteErr("更新项目状态时发生错误:" + ex.Message + ",项目Id:" + strProjectId, ex);
            }
            return blSuccess;
        }

        /// <summary>
        /// 得到一个对象实体
        /// <param name="strProjectId"></param>
        /// </summary>
        public static USER_SHARE_PROJECTMODEL GetProjectModel(int nProjectId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PROJECTID,PROJECTNAME,APISERVICEKEY,CREATEDATE,PROJECTREMARK,STATUS from USER_SHARE_PROJECT ");
            strSql.Append(" where PROJECTID=:PROJECTID ");
            USER_SHARE_PROJECTMODEL model = null;
            ParamList param = new ParamList();
            param["PROJECTID"] = nProjectId;
            DataTable dt = StaticConnectionProvider.ExecuteDataTable(strSql.ToString(), param);

            if (dt != null && dt.Rows.Count > 0)
            {
                model = ReaderBind(dt.Rows[0]);
            }

            return model;
        }

        /// <summary>
        /// 得到一个对象实体 根据密钥
        /// <param name="strProjectId"></param>
        /// </summary>
        public static USER_SHARE_PROJECTMODEL GetProjectModelByKey(string strAppKey)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PROJECTID,PROJECTNAME,APISERVICEKEY,CREATEDATE,PROJECTREMARK,STATUS from USER_SHARE_PROJECT ");
            strSql.Append(" where APISERVICEKEY=:APISERVICEKEY AND STATUS=" + ShareEnum.ProjectStatus.Normal.ToString("d"));
            USER_SHARE_PROJECTMODEL model = null;
            ParamList param = new ParamList();
            param["APISERVICEKEY"] = strAppKey;
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
        public static USER_SHARE_PROJECTMODEL ReaderBind(DataRow dataReader)
        {
            USER_SHARE_PROJECTMODEL model = new USER_SHARE_PROJECTMODEL();
            object ojb;
            ojb = dataReader["PROJECTID"];
            model.PROJECTID = ValidatorHelper.ToInt(ojb, 0);
            model.PROJECTNAME = CommonMethod.FinalString(dataReader["PROJECTNAME"]);
            model.APISERVICEKEY = CommonMethod.FinalString(dataReader["APISERVICEKEY"]);
            ojb = dataReader["CREATEDATE"];
            model.CREATEDATE = ValidatorHelper.ToDateTime(ojb, DateTime.Now);
            model.PROJECTREMARK = CommonMethod.FinalString(dataReader["PROJECTREMARK"]);
            ojb = dataReader["STATUS"];
            model.STATUS = ValidatorHelper.ToInt(ojb, 0);
            return model;
        }


        /// <summary>
        /// 获得数据列表 
        /// </summary>
        public static DataTable GetProjectList(int nPageIndex, int nPageSize, string strWhere, out int nCount)
        {
            int startIndex = nPageIndex * nPageSize;
            int endIndex = (nPageIndex + 1) * nPageSize;

            string strSqlCount = "Select COUNT(*) FROM USER_SHARE_PROJECT  WHERE 1=1 " + strWhere;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PROJECTID,PROJECTNAME,APISERVICEKEY,CREATEDATE,PROJECTREMARK,STATUS ");
            strSql.Append(" FROM USER_SHARE_PROJECT ");
            strSql.Append(" WHERE 1=1 " + strWhere+" ORDER BY PROJECTID DESC ");

            object objCount = StaticConnectionProvider.ExecuteScalar(strSqlCount);

            nCount = ValidatorHelper.ToInt(objCount, 0);

            string strFinalSql = string.Format("SELECT *FROM (SELECT R.*, ROWNUM RN FROM ({0})  R) WHERE RN>{1} AND RN<={2} ", strSql, startIndex, endIndex);
            return StaticConnectionProvider.ExecuteDataTable(strFinalSql);
        }



    }
}
