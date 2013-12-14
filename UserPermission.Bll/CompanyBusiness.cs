using System;
using System.Collections.Generic;
using System.Text;
using UserPermission.Model;
using UserPermission.Utils;
using SAMURAI.Data.Parameter;
using System.Data;
using SAMURAI.Data.Connection;
namespace UserPermission.Bll
{
    public class CompanyBusiness
    {


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool AddCompanyRelate(USER_SHARE_COMPANYRELATEMODEL model, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into USER_SHARE_COMPANYRELATE(");
            strSql.Append("CID,COMPANYTYPE,COMPANYNAME,COMPANYID,GROUPID,PRODUCTIDS,PROJECTIDS,COMPANYCODE,SHARECOMPANYID,ADMINID,CREATEDATE,STATUS,GROUPIDN)");

            strSql.Append(" values (");
            strSql.Append(":CID,:COMPANYTYPE,:COMPANYNAME,:COMPANYID,:GROUPID,:PRODUCTIDS,:PROJECTIDS,:COMPANYCODE,:SHARECOMPANYID,:ADMINID,:CREATEDATE,:STATUS,:GROUPIDN)");
            ParamList param = new ParamList();
            param["CID"] = model.CID;
            param["COMPANYTYPE"] = model.COMPANYTYPE;
            param["COMPANYNAME"] = model.COMPANYNAME;
            param["COMPANYID"] = model.COMPANYID;
            param["GROUPID"] = model.GROUPID;
            param["PRODUCTIDS"] = model.PRODUCTIDS;
            param["PROJECTIDS"] = model.PROJECTIDS;
            param["COMPANYCODE"] = model.COMPANYCODE;
            param["SHARECOMPANYID"] = model.SHARECOMPANYID;
            param["ADMINID"] = model.ADMINID;
            param["CREATEDATE"] = model.CREATEDATE;
            param["STATUS"] = model.STATUS;
            param["ADMINID"] = model.ADMINID;
            param["GROUPIDN"] = model.GROUPIDN;
            //应用系统注册
            if (model.COMPANYTYPE == int.Parse(ShareEnum.CompanyType.ShareCompany.ToString("d")))
            {
                //user_share_company插入
                int nShareCompanyId = CommonBusiness.GetSeqID("S_USER_SHARE_COMPANY");
                model.SHARECOMPANYID = nShareCompanyId;
                param["SHARECOMPANYID"] = nShareCompanyId;
            }

            string strSqlS = string.Format(@"SELECT M.* FROM USER_SHARE_PRODUCTFUN P INNER JOIN USER_SHARE_FUNMENU M 
                                             ON P.FUNID=M.FMID WHERE M.FMSTATUS={0} AND P.PRODUCTID IN ({1}) ",
                               ShareEnum.FunMenuStatus.Normal.ToString("d"), model.PRODUCTIDS.TrimStart(',').TrimEnd(','));

            DataTable dt = StaticConnectionProvider.ExecuteDataTable(strSqlS);
            int nCfId = 0;
            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();

            try
            {

                using (connection)
                {
                    connection.BeginTranscation();

                    //公司关联信息
                    connection.ExecuteNonQuery(strSql.ToString(), param);
                    param.Clear();

                    if (model.COMPANYTYPE == int.Parse(ShareEnum.CompanyType.ShareCompany.ToString("d")))
                    {
                        strSqlS = "INSERT INTO USER_SHARE_COMPANY(COMPANYID,COMPANYNAME) VALUES({0},'{1}')";
                        connection.ExecuteNonQuery(string.Format(strSqlS, model.SHARECOMPANYID, model.COMPANYNAME));
                    }

                    //公司菜单初始化
                    foreach (DataRow dr in dt.Rows)
                    {
                        nCfId = CommonBusiness.GetSeqID("S_USER_SHARE_COMPANYFUN");
                        strSqlS = string.Format(@"insert into USER_SHARE_COMPANYFUN(CFID,FMID,PROJECTID,COMPANYID,CFNAME,
                        CFANOTHERNAME,CFPAGEURL,CFPARENTID,CFSORTNUM,CFSTEP,CFISLAST,CFDESC,CFSTATUS) values (
                        :CFID,:FMID,:PROJECTID,:COMPANYID,:CFNAME,:CFANOTHERNAME,:CFPAGEURL,:CFPARENTID,:CFSORTNUM,
                        :CFSTEP,:CFISLAST,:CFDESC,:CFSTATUS)");
                        param["CFID"] = nCfId;
                        param["FMID"] = dr["FMID"];
                        param["PROJECTID"] = dr["PROJECTID"];
                        param["COMPANYID"] = model.COMPANYCODE;
                        param["CFNAME"] = dr["FMNAME"];
                        param["CFANOTHERNAME"] = dr["FMNAME"];
                        param["CFPAGEURL"] = CommonMethod.FinalString(dr["FMPAGEURL"]);
                        param["CFPARENTID"] = CommonMethod.FinalString(dr["FMPARENTID"]);
                        param["CFSORTNUM"] = dr["FMSORTNUM"];
                        param["CFSTEP"] = CommonMethod.FinalString(dr["FMSTEP"]);
                        param["CFISLAST"] = CommonMethod.FinalString(dr["FMISLAST"]);
                        param["CFDESC"] = CommonMethod.FinalString(dr["FMDESC"]);
                        param["CFSTATUS"] = ShareEnum.CompanyFunMenuStatus.Normal.ToString("d");
                        connection.ExecuteNonQuery(strSqlS, param);
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
                LogHelper.WriteErr("注册公司时发生错误:公司名称-" + model.COMPANYNAME, ex);
            }

            return blSuccess;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool UpdateCompanyRelate(USER_SHARE_COMPANYRELATEMODEL model, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update USER_SHARE_COMPANYRELATE set ");
            strSql.Append("COMPANYTYPE=:COMPANYTYPE,");
            strSql.Append("COMPANYNAME=:COMPANYNAME,");
            strSql.Append("COMPANYID=:COMPANYID,");
            strSql.Append("GROUPID=:GROUPID,");
            strSql.Append("PROJECTIDS=:PROJECTIDS,");
            strSql.Append("PRODUCTIDS=:PRODUCTIDS,");
            strSql.Append("COMPANYCODE=:COMPANYCODE,");
            strSql.Append("SHARECOMPANYID=:SHARECOMPANYID, ");
            strSql.Append("ADMINID=:ADMINID,");
            strSql.Append("GROUPIDN=:GROUPIDN ");
            strSql.Append(" where CID=:CID ");

            ParamList param = new ParamList();
            param["CID"] = model.CID;
            param["COMPANYTYPE"] = model.COMPANYTYPE;
            param["COMPANYNAME"] = model.COMPANYNAME;
            param["COMPANYID"] = model.COMPANYID;
            param["GROUPID"] = model.GROUPID;
            param["PROJECTIDS"] = model.PROJECTIDS;
            param["PRODUCTIDS"] = model.PRODUCTIDS;
            param["COMPANYCODE"] = model.COMPANYCODE;
            param["SHARECOMPANYID"] = model.SHARECOMPANYID;
            param["ADMINID"] = model.ADMINID;
            param["GROUPIDN"] = model.GROUPIDN;
            string strSqlS = string.Format(@"SELECT M.* FROM USER_SHARE_PRODUCTFUN P INNER JOIN USER_SHARE_FUNMENU M 
                                             ON P.FUNID=M.FMID WHERE M.FMSTATUS={0} AND P.PRODUCTID IN ({1}) AND P.FUNID
                                             NOT IN (SELECT FMID FROM USER_SHARE_COMPANYFUN WHERE COMPANYID={2}  )
                                             ", ShareEnum.FunMenuStatus.Normal.ToString("d"),
                                              model.PRODUCTIDS.TrimStart(',').TrimEnd(','),
                                              model.COMPANYCODE);

            DataTable dt = StaticConnectionProvider.ExecuteDataTable(strSqlS);
            //原来公司自己删除的菜单如果再次开通了要恢复
            strSqlS = string.Format(@"SELECT M.FMID FROM USER_SHARE_PRODUCTFUN P INNER JOIN USER_SHARE_FUNMENU M 
                                              ON P.FUNID=M.FMID WHERE M.FMSTATUS={0} AND P.PRODUCTID IN ({1}) AND P.FUNID
                                              IN (SELECT FMID FROM USER_SHARE_COMPANYFUN WHERE COMPANYID={2} AND CFSTATUS={3} )
                                             ", ShareEnum.FunMenuStatus.Normal.ToString("d"),
                                              model.PRODUCTIDS.TrimStart(',').TrimEnd(','),
                                              model.COMPANYCODE, ShareEnum.CompanyFunMenuStatus.StopUse.ToString("d"));
            DataTable dtStop = StaticConnectionProvider.ExecuteDataTable(strSqlS);
            int nCfId = 0;

            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();

            try
            {
                using (connection)
                {
                    connection.CommitTranscation();

                    //增加项目信息
                    connection.ExecuteNonQuery(strSql.ToString(), param);
                    param.Clear();

                    //公司菜单初始化
                    foreach (DataRow dr in dt.Rows)
                    {
                        nCfId = CommonBusiness.GetSeqID("S_USER_SHARE_COMPANYFUN");
                        strSqlS = string.Format(@"insert into USER_SHARE_COMPANYFUN(CFID,FMID,PROJECTID,COMPANYID,CFNAME,
                        CFANOTHERNAME,CFPAGEURL,CFPARENTID,CFSORTNUM,CFSTEP,CFISLAST,CFDESC,CFSTATUS) values (
                        :CFID,:FMID,:PROJECTID,:COMPANYID,:CFNAME,:CFANOTHERNAME,:CFPAGEURL,:CFPARENTID,:CFSORTNUM,
                        :CFSTEP,:CFISLAST,:CFDESC,:CFSTATUS)");
                        param["CFID"] = nCfId;
                        param["FMID"] = dr["FMID"];
                        param["PROJECTID"] = dr["PROJECTID"];
                        param["COMPANYID"] = model.COMPANYCODE;
                        param["CFNAME"] = dr["FMNAME"];
                        param["CFANOTHERNAME"] = dr["FMNAME"];
                        param["CFPAGEURL"] = CommonMethod.FinalString(dr["FMPAGEURL"]);
                        param["CFPARENTID"] = CommonMethod.FinalString(dr["FMPARENTID"]);
                        param["CFSORTNUM"] = dr["FMSORTNUM"];
                        param["CFSTEP"] = CommonMethod.FinalString(dr["FMSTEP"]);
                        param["CFISLAST"] = CommonMethod.FinalString(dr["FMISLAST"]);
                        param["CFDESC"] = CommonMethod.FinalString(dr["FMDESC"]);
                        param["CFSTATUS"] = ShareEnum.CompanyFunMenuStatus.Normal.ToString("d");
                        connection.ExecuteNonQuery(strSqlS, param);
                    }

                    //原来公司自己删除的菜单如果再次开通了要恢复
                    foreach (DataRow dr in dtStop.Rows)
                    {
                        strSqlS = "UPDATE USER_SHARE_COMPANYFUN SET CFSTATUS={0} WHERE COMPANYID={1} AND FMID={2} AND CFSTATUS={3}";
                        connection.ExecuteNonQuery(string.Format(strSqlS, ShareEnum.CompanyFunMenuStatus.Normal.ToString("d"),
                            model.COMPANYCODE, dr["FMID"], ShareEnum.CompanyFunMenuStatus.StopUse.ToString("d")));
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
                LogHelper.WriteErr("修改公司关联信息时发生错误:公司名称-" + model.COMPANYNAME, ex);
            }

            return blSuccess;

        }


        /// <summary>
        /// 更新项目状态
        /// </summary>
        /// <param name="strCId"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public static bool UpdateCompanyRelateStatus(string strCId, string strStatus, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;
            StringBuilder strSql = new StringBuilder("UPDATE USER_SHARE_COMPANYRELATE SET STATUS=:STATUS WHERE CID=:CID");
            ParamList param = new ParamList();
            param["CID"] = strCId;
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
                LogHelper.WriteErr("更新公司关联状态时发生错误:" + ex.Message + ",Id:" + strCId, ex);
            }
            return blSuccess;
        }

        // <summary>
        /// 获得数据列表 
        /// </summary>
        public static DataTable GetCompanyRelateList(int nPageIndex, int nPageSize, string strWhere, out int nCount)
        {
            int startIndex = nPageIndex * nPageSize;
            int endIndex = (nPageIndex + 1) * nPageSize;

            string strSqlCount = "Select COUNT(*) FROM USER_SHARE_COMPANYRELATE  WHERE 1=1 " + strWhere;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CID,COMPANYTYPE,COMPANYNAME,C.COMPANYID,GROUPID,PROJECTIDS,PRODUCTIDS,COMPANYCODE,SHARECOMPANYID,ADMINID,C.CREATEDATE,C.STATUS,A.ACCOUNTNAME ");
            strSql.Append(" FROM USER_SHARE_COMPANYRELATE C LEFT JOIN USER_SHARE_ACCOUNT A ON A.ACCOUNTID=C.ADMINID ");
            strSql.Append(" WHERE 1=1 " + strWhere + " ORDER BY CID DESC ");

            object objCount = StaticConnectionProvider.ExecuteScalar(strSqlCount);

            nCount = ValidatorHelper.ToInt(objCount, 0);

            string strFinalSql = string.Format("SELECT *FROM (SELECT R.*, ROWNUM RN FROM ({0})  R) WHERE RN>{1} AND RN<={2} ", strSql, startIndex, endIndex);
            return StaticConnectionProvider.ExecuteDataTable(strFinalSql);
        }


        /// <summary>
        /// 根据公司编码获取对象
        /// </summary>
        /// <param name="nCompanyId"></param>
        /// <returns></returns>
        public static USER_SHARE_COMPANYRELATEMODEL GetModel(int companyCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CID,COMPANYTYPE,COMPANYNAME,COMPANYID,GROUPID,PROJECTIDS,PRODUCTIDS,COMPANYCODE,SHARECOMPANYID,ADMINID,CREATEDATE,STATUS from USER_SHARE_COMPANYRELATE ");
            strSql.Append(" where  COMPANYCODE=:COMPANYCODE ");
            ParamList param = new ParamList();
            param["COMPANYCODE"] = companyCode;
            USER_SHARE_COMPANYRELATEMODEL model = null;
            DataTable dt = StaticConnectionProvider.ExecuteDataTable(strSql.ToString(), param);
            if (dt != null && dt.Rows.Count > 0)
            {
                model = ReaderBind(dt.Rows[0]);
            }

            return model;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="nCompanyId"></param>
        /// <returns></returns>
        public static USER_SHARE_COMPANYRELATEMODEL GetModelByCid(int nCId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CID,COMPANYTYPE,COMPANYNAME,COMPANYID,GROUPID,PROJECTIDS,PRODUCTIDS,COMPANYCODE,SHARECOMPANYID,ADMINID,CREATEDATE,STATUS from USER_SHARE_COMPANYRELATE ");
            strSql.Append(" where CID=:CID ");
            ParamList param = new ParamList();
            param["CID"] = nCId;
            USER_SHARE_COMPANYRELATEMODEL model = null;
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
        private static USER_SHARE_COMPANYRELATEMODEL ReaderBind(DataRow dataReader)
        {
            USER_SHARE_COMPANYRELATEMODEL model = new USER_SHARE_COMPANYRELATEMODEL();

            model.CID = ValidatorHelper.ToInt(dataReader["CID"], 0);
            model.COMPANYTYPE = ValidatorHelper.ToInt(dataReader["COMPANYTYPE"], 0);
            model.COMPANYNAME = CommonMethod.FinalString(dataReader["COMPANYNAME"]);
            model.COMPANYID = ValidatorHelper.ToInt(dataReader["COMPANYID"], 0);
            model.GROUPID = CommonMethod.FinalString(dataReader["GROUPID"]);
            model.PRODUCTIDS = CommonMethod.FinalString(dataReader["PRODUCTIDS"]);
            model.PROJECTIDS = CommonMethod.FinalString(dataReader["PROJECTIDS"]);
            model.COMPANYCODE = ValidatorHelper.ToInt(dataReader["COMPANYCODE"], 0);
            model.SHARECOMPANYID = ValidatorHelper.ToInt(dataReader["SHARECOMPANYID"], 0);
            model.ADMINID = ValidatorHelper.ToInt(dataReader["ADMINID"], 0);
            model.CREATEDATE = ValidatorHelper.ToDateTime(dataReader["CREATEDATE"], DateTime.Now);
            model.STATUS = ValidatorHelper.ToInt(dataReader["STATUS"], 0);
            return model;

        }


        /// <summary>
        /// 锁
        /// </summary>
        private static object oLock = new object();
        /// <summary>
        /// 获取公司编码
        /// </summary>
        /// <returns></returns>
        public static int GetCompanyCode()
        {
            int nCode = 0;
            lock (oLock)
            {
                string strSql = "SELECT NVL(MAX(COMPANYCODE),100)+1 FROM USER_SHARE_COMPANYRELATE ";
                object code = StaticConnectionProvider.ExecuteScalar(strSql);
                nCode = ValidatorHelper.ToInt(code, 100);

            }
            return nCode;
        }

        /// <summary>
        /// 验证是否存在公司编码
        /// </summary>
        /// <param name="CID"></param>
        /// <returns></returns>
        public static bool IsCompanyCodeExists(string strCompanyCode)
        {
            string strSql = "select count(1) from USER_SHARE_COMPANYRELATE where COMPANYCODE=:COMPANYCODE ";
            ParamList param = new ParamList();
            param["COMPANYCODE"] = strCompanyCode;
            return ValidatorHelper.ToInt(StaticConnectionProvider.ExecuteScalar(strSql, param), 0) > 0;
        }

        /// <summary>
        /// 判断某个公司是否已注册，此方法只在AutoRegister接口处使用，供危险品企业自动注册到权限系统，临时用
        /// </summary>
        /// <param name="companyname"></param>
        /// <param name="groupid"></param>
        /// <returns></returns>
        public static int IsCompanyExists(string companyname, string groupid, int companytype)
        {
            string strSql = "SELECT COMPANYCODE FROM USER_SHARE_COMPANYRELATE WHERE COMPANYNAME=:COMPANYNAME AND GROUPID=:GROUPID AND COMPANYTYPE=:COMPANYTYPE ";
            ParamList param = new ParamList();
            param["COMPANYNAME"] = companyname;
            param["GROUPID"] = groupid;
            param["COMPANYTYPE"] = companytype;
            return ValidatorHelper.ToInt(StaticConnectionProvider.ExecuteScalar(strSql, param), 0);
        }

        /// <summary>
        /// 根据公司取得公司开通的项目
        /// </summary>
        /// <param name="strCompanyCode"></param>
        /// <returns></returns>
        public static DataTable GetCompanyProjects(string strCompanyCode)
        {
            DataTable dt = null;
            string strSql = "SELECT PROJECTIDS FROM USER_SHARE_COMPANYRELATE WHERE COMPANYCODE=:COMPANYCODE ";
            ParamList param = new ParamList();
            param["COMPANYCODE"] = strCompanyCode;
            string strProjectIds = CommonMethod.FinalString(StaticConnectionProvider.ExecuteScalar(strSql, param));
            if (strProjectIds.Length > 0)
            {
                strProjectIds = strProjectIds.TrimStart(',').TrimEnd(',');
                strSql = "SELECT PROJECTID,PROJECTNAME FROM USER_SHARE_PROJECT WHERE PROJECTID IN(" + strProjectIds + ") AND STATUS=" + ShareEnum.ProjectStatus.Normal.ToString("d");

                dt = StaticConnectionProvider.ExecuteDataTable(strSql);
            }

            return dt;

        }

        /// <summary>
        /// 更新公司关联表的companyid
        /// </summary>
        /// <param name="strCompanyCode"></param>
        public static void UpdateRelateCompanyId(int companyid, int companycode)
        {
            string strSql = "UPDATE USER_SHARE_COMPANYRELATE SET COMPANYID=:COMPANYID WHERE COMPANYCODE=:COMPANYCODE ";
            ParamList param = new ParamList();
            param["COMPANYCODE"] = companycode;
            param["COMPANYID"] = companyid;
            StaticConnectionProvider.ExecuteNonQuery(strSql, param);
        }
    }
}
