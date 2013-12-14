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
    public class ProjectFunBusiness
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool AddProjectFun(USER_SHARE_FUNMENUMODEL model, USER_SHARE_LOGMODEL log)
        {
            bool blResult = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into USER_SHARE_FUNMENU(");
            strSql.Append("FMID,PROJECTID,FMNAME,FMPAGEURL,FMPARENTID,FMSORTNUM,FMSTEP,FMISLAST,FMDESC,FMSTATUS)");
            strSql.Append(" values (");
            strSql.Append(":FMID,:PROJECTID,:FMNAME,:FMPAGEURL,:FMPARENTID,:FMSORTNUM,:FMSTEP,:FMISLAST,:FMDESC,:FMSTATUS)");

            ParamList param = new ParamList();
            param["FMID"] = model.FMID;
            param["PROJECTID"] = model.PROJECTID;
            param["FMNAME"] = model.FMNAME;
            param["FMPAGEURL"] = model.FMPAGEURL;
            param["FMPARENTID"] = model.FMPARENTID;
            param["FMSORTNUM"] = model.FMSORTNUM;
            param["FMSTEP"] = model.FMSTEP;
            param["FMISLAST"] = model.FMISLAST;
            param["FMDESC"] = model.FMDESC;
            param["FMSTATUS"] = model.FMSTATUS;

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
                LogHelper.WriteErr("新增项目功能菜单时出现异常，项目Id:" + model.PROJECTID, ex);
            }

            return blResult;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool UpdateProjectFun(USER_SHARE_FUNMENUMODEL model, USER_SHARE_LOGMODEL log)
        {
            bool blResult = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update USER_SHARE_FUNMENU set ");
            strSql.Append("PROJECTID=:PROJECTID,");
            strSql.Append("FMNAME=:FMNAME,");
            strSql.Append("FMPAGEURL=:FMPAGEURL,");
            strSql.Append("FMPARENTID=:FMPARENTID,");
            strSql.Append("FMSORTNUM=:FMSORTNUM,");
            strSql.Append("FMSTEP=:FMSTEP,");
            strSql.Append("FMISLAST=:FMISLAST,");
            strSql.Append("FMDESC=:FMDESC,");
            strSql.Append("FMSTATUS=:FMSTATUS");
            strSql.Append(" where FMID=:FMID ");

            ParamList param = new ParamList();
            param["FMID"] = model.FMID;
            param["PROJECTID"] = model.PROJECTID;
            param["FMNAME"] = model.FMNAME;
            param["FMPAGEURL"] = model.FMPAGEURL;
            param["FMPARENTID"] = model.FMPARENTID;
            param["FMSORTNUM"] = model.FMSORTNUM;
            param["FMSTEP"] = model.FMSTEP;
            param["FMISLAST"] = model.FMISLAST;
            param["FMDESC"] = model.FMDESC;
            param["FMSTATUS"] = model.FMSTATUS;

            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();
            try
            {

                using (connection)
                {
                    connection.BeginTranscation();

                    //增加项目信息
                    connection.ExecuteNonQuery(strSql.ToString(), param);
                    param.Clear();
                    //链接地址改动，更新公司菜单表的链接地址
                    strSql = new StringBuilder("UPDATE USER_SHARE_COMPANYFUN SET CFPAGEURL=:CFPAGEURL WHERE CFPAGEURL!=:CFPAGEURL AND  FMID=:FMID  ");
                    param["FMID"] = model.FMID;
                    param["CFPAGEURL"] = model.FMPAGEURL;
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
                LogHelper.WriteErr("修改项目功能菜单时出现异常，Id:" + model.FMID, ex);
            }

            return blResult;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static USER_SHARE_FUNMENUMODEL GetModel(int FMID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FMID,PROJECTID,FMNAME,FMPAGEURL,FMPARENTID,FMSORTNUM,FMSTEP,FMISLAST,FMDESC,FMSTATUS from USER_SHARE_FUNMENU ");
            strSql.Append(" where FMID=:FMID ");
            ParamList param = new ParamList();
            param["FMID"] = FMID;

            USER_SHARE_FUNMENUMODEL model = null;
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
        private static USER_SHARE_FUNMENUMODEL ReaderBind(DataRow dataReader)
        {
            UserPermission.Model.USER_SHARE_FUNMENUMODEL model = new UserPermission.Model.USER_SHARE_FUNMENUMODEL();
            model.FMID = ValidatorHelper.ToInt(dataReader["FMID"], 0);
            model.PROJECTID = ValidatorHelper.ToInt(dataReader["PROJECTID"], 0);
            model.FMNAME = dataReader["FMNAME"].ToString();
            model.FMPAGEURL = CommonMethod.FinalString(dataReader["FMPAGEURL"]);
            model.FMPARENTID = ValidatorHelper.ToInt(dataReader["FMPARENTID"], 0);
            model.FMSORTNUM = ValidatorHelper.ToInt(dataReader["FMSORTNUM"], 0);
            model.FMSTEP = CommonMethod.FinalString(dataReader["FMSTEP"]);
            model.FMISLAST = ValidatorHelper.ToInt(dataReader["FMISLAST"], 0);
            model.FMDESC = CommonMethod.FinalString(dataReader["FMDESC"]);
            model.FMSTATUS = ValidatorHelper.ToInt(dataReader["FMSTATUS"], 0);
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public static DataTable GetFunMenuList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FMID,PROJECTID,FMNAME,FMPAGEURL,FMPARENTID,FMSORTNUM,FMSTEP,FMISLAST,FMDESC,FMSTATUS ");
            strSql.Append(" FROM USER_SHARE_FUNMENU ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where FMSTATUS= " + ShareEnum.FunMenuStatus.Normal.ToString("d") + " " + strWhere);
            }
            strSql.Append(" ORDER BY FMSORTNUM DESC ");
            return StaticConnectionProvider.ExecuteDataTable(strSql.ToString());
        }


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public static bool ExistsChildFun(int FMID)
        {
            string strSql = "select count(1) from USER_SHARE_FUNMENU where FMPARENTID= " + FMID + " AND FMSTATUS=" + ShareEnum.FunMenuStatus.Normal.ToString("d");
            object obj = StaticConnectionProvider.ExecuteScalar(strSql);
            return ValidatorHelper.ToInt(obj, 0) > 0;
        }

        /// <summary>
        ///  停用功能菜单
        /// </summary>
        /// <param name="nFmId"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public static bool SetFunStop(int nFmId, USER_SHARE_LOGMODEL log)
        {
            bool blResult = false;

            string strSqls = "UPDATE USER_SHARE_FUNMENU SET FMSTATUS=:FMSTATUS WHERE FMID=:FMID";

            ParamList param = new ParamList();
            param["FMID"] = nFmId;
            param["FMSTATUS"] = ShareEnum.FunMenuStatus.StopUse.ToString("d");

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
                LogHelper.WriteErr("将项目功能菜单设置为无效时出现异常，Id:" + nFmId, ex);
            }
            return blResult;
        }

    }
}
