using System;
using System.Collections.Generic;
using System.Text;
using UserPermission.Model;
using UserPermission.Utils;
using System.Data;
using SAMURAI.Data.Connection;
using SAMURAI.Data.Parameter;

namespace UserPermission.Bll
{
    public class ProductBusiness
    {

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static bool AddProduct(USER_SHARE_PRODUCTMODEL model, List<USER_SHARE_PRODUCTFUNMODEL> lstFunModel, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into USER_SHARE_PRODUCT(");
            strSql.Append("PRODUCTID,PROJECTID,PRODUCTNAME,PRODUCTDESC,PRODUCTFLAG)");
            strSql.Append(" values (");
            strSql.Append(":PRODUCTID,:PROJECTID,:PRODUCTNAME,:PRODUCTDESC,:PRODUCTFLAG)");
            ParamList param = new ParamList();
            param["PRODUCTID"] = model.PRODUCTID;
            param["PROJECTID"] = model.PROJECTID;
            param["PRODUCTNAME"] = model.PRODUCTNAME;
            param["PRODUCTDESC"] = model.PRODUCTDESC;
            param["PRODUCTFLAG"] = model.PRODUCTFLAG;

            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();
            try
            {

                using (connection)
                {
                    connection.BeginTranscation();

                    //增加产品基础信息
                    connection.ExecuteNonQuery(strSql.ToString(), param);
                    param.Clear();

                    //产品功能信息

                    foreach (USER_SHARE_PRODUCTFUNMODEL funmodel in lstFunModel)
                    {
                        strSql = new StringBuilder();
                        strSql.Append("insert into USER_SHARE_PRODUCTFUN(");
                        strSql.Append("PRODUCTID,FUNID)");
                        strSql.Append(" values (");
                        strSql.Append(":PRODUCTID,:FUNID)");

                        param["PRODUCTID"] = funmodel.PROCUTID;
                        param["FUNID"] = funmodel.FUNID;

                        connection.ExecuteNonQuery(strSql.ToString(), param);
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
                LogHelper.WriteErr("新增产品信息时发生错误，产品名称：" + model.PRODUCTNAME, ex);
            }

            return blSuccess;
        }


        /// <summary>
        /// 修改一条数据
        /// </summary>
        public static bool EditProduct(USER_SHARE_PRODUCTMODEL model, List<USER_SHARE_PRODUCTFUNMODEL> lstFunModel, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("update USER_SHARE_PRODUCT set ");
            strSql.Append("PROJECTID=:PROJECTID,");
            strSql.Append("PRODUCTNAME=:PRODUCTNAME,");
            strSql.Append("PRODUCTDESC=:PRODUCTDESC");
            strSql.Append(" where PRODUCTID=:PRODUCTID ");
            ParamList param = new ParamList();
            param["PRODUCTID"] = model.PRODUCTID;
            param["PROJECTID"] = model.PROJECTID;
            param["PRODUCTNAME"] = model.PRODUCTNAME;
            param["PRODUCTDESC"] = model.PRODUCTDESC;


            IConnectionProvider connection = ConnectionProviderBuilder.CreateConnectionProvider();
            try
            {

                using (connection)
                {
                    connection.BeginTranscation();

                    //增加产品基础信息
                    connection.ExecuteNonQuery(strSql.ToString(), param);
                    param.Clear();

                    //产品功能信息

                    //删除原来的
                    connection.ExecuteNonQuery("DELETE FROM USER_SHARE_PRODUCTFUN WHERE PRODUCTID=" + model.PRODUCTID);
                    //增加现在的
                    foreach (USER_SHARE_PRODUCTFUNMODEL funmodel in lstFunModel)
                    {
                        strSql = new StringBuilder();
                        strSql.Append("insert into USER_SHARE_PRODUCTFUN(");
                        strSql.Append("PRODUCTID,FUNID)");
                        strSql.Append(" values (");
                        strSql.Append(":PRODUCTID,:FUNID)");

                        param["PRODUCTID"] = funmodel.PROCUTID;
                        param["FUNID"] = funmodel.FUNID;

                        connection.ExecuteNonQuery(strSql.ToString(), param);
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
                LogHelper.WriteErr("修改产品信息时发生错误，产品名称：" + model.PRODUCTNAME, ex);
            }

            return blSuccess;
        }

        /// <summary>
        /// 获得数据列表 
        /// </summary>
        public static DataTable GetProductList(int nPageIndex, int nPageSize, string strWhere, out int nCount)
        {
            int startIndex = nPageIndex * nPageSize;
            int endIndex = (nPageIndex + 1) * nPageSize;

            string strSqlCount = "Select COUNT(*) FROM USER_SHARE_PRODUCT D INNER JOIN USER_SHARE_PROJECT J ON D.PROJECTID=J.PROJECTID  WHERE 1=1 " + strWhere;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PRODUCTID,D.PROJECTID,PRODUCTNAME,PRODUCTDESC,PRODUCTFLAG,PROJECTNAME  ");
            strSql.Append(" FROM USER_SHARE_PRODUCT D INNER JOIN USER_SHARE_PROJECT J ON D.PROJECTID=J.PROJECTID ");
            strSql.Append(" WHERE 1=1 " + strWhere+" ORDER BY PRODUCTID DESC ");

            object objCount = StaticConnectionProvider.ExecuteScalar(strSqlCount);

            nCount = ValidatorHelper.ToInt(objCount, 0);

            string strFinalSql = string.Format("SELECT *FROM (SELECT R.*, ROWNUM RN FROM ({0})  R) WHERE RN>{1} AND RN<={2} ", strSql, startIndex, endIndex);
            return StaticConnectionProvider.ExecuteDataTable(strFinalSql);
        }


        /// <summary>
        /// 更新产品状态
        /// </summary>
        /// <param name="strProductId"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public static bool UpdateProductStatus(string strProductId, string strStatus, USER_SHARE_LOGMODEL log)
        {
            bool blSuccess = false;
            StringBuilder strSql = new StringBuilder("UPDATE USER_SHARE_PRODUCT SET PRODUCTFLAG=:PRODUCTFLAG WHERE PRODUCTID=:PRODUCTID");
            ParamList param = new ParamList();
            param["PRODUCTID"] = strProductId;
            param["PRODUCTFLAG"] = strStatus;
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
                LogHelper.WriteErr("更新产品状态时发生错误:" + ex.Message + ",产品Id:" + strProductId, ex);
            }
            return blSuccess;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public static USER_SHARE_PRODUCTMODEL GetProductModel(int PRODUCTID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select PRODUCTID,PROJECTID,PRODUCTNAME,PRODUCTDESC,PRODUCTFLAG from USER_SHARE_PRODUCT ");
            strSql.Append(" where PRODUCTID=:PRODUCTID ");
            ParamList param = new ParamList();
            param["PRODUCTID"] = PRODUCTID;
            USER_SHARE_PRODUCTMODEL model = null;
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
        private static USER_SHARE_PRODUCTMODEL ReaderBind(DataRow dataReader)
        {
            USER_SHARE_PRODUCTMODEL model = new USER_SHARE_PRODUCTMODEL();

            model.PRODUCTID = ValidatorHelper.ToInt(dataReader["PRODUCTID"], 0);
            model.PROJECTID = ValidatorHelper.ToInt(dataReader["PROJECTID"], 0);
            model.PRODUCTNAME = CommonMethod.FinalString(dataReader["PRODUCTNAME"]);
            model.PRODUCTDESC = CommonMethod.FinalString(dataReader["PRODUCTDESC"]);
            model.PRODUCTFLAG = ValidatorHelper.ToInt(dataReader["PRODUCTFLAG"], 0);
            return model;
        }

        /// <summary>
        /// 判断某个产品下是否有某个功能
        /// </summary>
        /// <param name="strProductId"></param>
        /// <param name="strFunId"></param>
        /// <returns></returns>
        public static bool IsProductExistFun(string strProductId, string strFunId)
        {
            string strSql = "SELECT COUNT(*) FROM USER_SHARE_PRODUCTFUN WHERE PRODUCTID=" + strProductId + " AND FUNID=" + strFunId;
            return ValidatorHelper.ToInt(StaticConnectionProvider.ExecuteScalar(strSql), 0) > 0;
        }

    }
}
