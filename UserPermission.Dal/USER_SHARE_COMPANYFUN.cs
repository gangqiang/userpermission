using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;

namespace UserPermission.DAL
{
    /// <summary>
    /// 数据访问类:USER_SHARE_COMPANYFUN
    /// </summary>
    public partial class USER_SHARE_COMPANYFUN
    {
        public USER_SHARE_COMPANYFUN()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(decimal CFID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from USER_SHARE_COMPANYFUN where CFID=@CFID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CFID", DbType.String, CFID);
            int cmdresult;
            object obj = db.ExecuteScalar(dbCommand);
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public void Add(UserPermission.Model.USER_SHARE_COMPANYFUNMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into USER_SHARE_COMPANYFUN(");
            strSql.Append("CFID,FMID,PROJECTID,COMPANYID,CFNAME,CFANOTHERNAME,CFPAGEURL,CFPARENTID,CFSORTNUM,CFSTEP,CFISLAST,CFDESC,CFSTATUS)");

            strSql.Append(" values (");
            strSql.Append("@CFID,@FMID,@PROJECTID,@COMPANYID,@CFNAME,@CFANOTHERNAME,@CFPAGEURL,@CFPARENTID,@CFSORTNUM,@CFSTEP,@CFISLAST,@CFDESC,@CFSTATUS)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CFID", DbType.String, model.CFID);
            db.AddInParameter(dbCommand, "FMID", DbType.String, model.FMID);
            db.AddInParameter(dbCommand, "PROJECTID", DbType.String, model.PROJECTID);
            db.AddInParameter(dbCommand, "COMPANYID", DbType.String, model.COMPANYID);
            db.AddInParameter(dbCommand, "CFNAME", DbType.String, model.CFNAME);
            db.AddInParameter(dbCommand, "CFANOTHERNAME", DbType.String, model.CFANOTHERNAME);
            db.AddInParameter(dbCommand, "CFPAGEURL", DbType.String, model.CFPAGEURL);
            db.AddInParameter(dbCommand, "CFPARENTID", DbType.String, model.CFPARENTID);
            db.AddInParameter(dbCommand, "CFSORTNUM", DbType.String, model.CFSORTNUM);
            db.AddInParameter(dbCommand, "CFSTEP", DbType.String, model.CFSTEP);
            db.AddInParameter(dbCommand, "CFISLAST", DbType.String, model.CFISLAST);
            db.AddInParameter(dbCommand, "CFDESC", DbType.String, model.CFDESC);
            db.AddInParameter(dbCommand, "CFSTATUS", DbType.String, model.CFSTATUS);
            db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(UserPermission.Model.USER_SHARE_COMPANYFUNMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update USER_SHARE_COMPANYFUN set ");
            strSql.Append("FMID=@FMID,");
            strSql.Append("PROJECTID=@PROJECTID,");
            strSql.Append("COMPANYID=@COMPANYID,");
            strSql.Append("CFNAME=@CFNAME,");
            strSql.Append("CFANOTHERNAME=@CFANOTHERNAME,");
            strSql.Append("CFPAGEURL=@CFPAGEURL,");
            strSql.Append("CFPARENTID=@CFPARENTID,");
            strSql.Append("CFSORTNUM=@CFSORTNUM,");
            strSql.Append("CFSTEP=@CFSTEP,");
            strSql.Append("CFISLAST=@CFISLAST,");
            strSql.Append("CFDESC=@CFDESC,");
            strSql.Append("CFSTATUS=@CFSTATUS");
            strSql.Append(" where CFID=@CFID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CFID", DbType.String, model.CFID);
            db.AddInParameter(dbCommand, "FMID", DbType.String, model.FMID);
            db.AddInParameter(dbCommand, "PROJECTID", DbType.String, model.PROJECTID);
            db.AddInParameter(dbCommand, "COMPANYID", DbType.String, model.COMPANYID);
            db.AddInParameter(dbCommand, "CFNAME", DbType.String, model.CFNAME);
            db.AddInParameter(dbCommand, "CFANOTHERNAME", DbType.String, model.CFANOTHERNAME);
            db.AddInParameter(dbCommand, "CFPAGEURL", DbType.String, model.CFPAGEURL);
            db.AddInParameter(dbCommand, "CFPARENTID", DbType.String, model.CFPARENTID);
            db.AddInParameter(dbCommand, "CFSORTNUM", DbType.String, model.CFSORTNUM);
            db.AddInParameter(dbCommand, "CFSTEP", DbType.String, model.CFSTEP);
            db.AddInParameter(dbCommand, "CFISLAST", DbType.String, model.CFISLAST);
            db.AddInParameter(dbCommand, "CFDESC", DbType.String, model.CFDESC);
            db.AddInParameter(dbCommand, "CFSTATUS", DbType.String, model.CFSTATUS);
            return db.ExecuteNonQuery(dbCommand) > 0;

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(decimal CFID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from USER_SHARE_COMPANYFUN ");
            strSql.Append(" where CFID=@CFID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CFID", DbType.String, CFID);
            db.ExecuteNonQuery(dbCommand) > 0;

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserPermission.Model.USER_SHARE_COMPANYFUNMODEL GetModel(decimal CFID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CFID,FMID,PROJECTID,COMPANYID,CFNAME,CFANOTHERNAME,CFPAGEURL,CFPARENTID,CFSORTNUM,CFSTEP,CFISLAST,CFDESC,CFSTATUS from USER_SHARE_COMPANYFUN ");
            strSql.Append(" where CFID=@CFID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CFID", DbType.String, CFID);
            UserPermission.Model.USER_SHARE_COMPANYFUNMODEL model = null;
            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
            {
                if (dataReader.Read())
                {
                    model = ReaderBind(dataReader);
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CFID,FMID,PROJECTID,COMPANYID,CFNAME,CFANOTHERNAME,CFPAGEURL,CFPARENTID,CFSORTNUM,CFSTEP,CFISLAST,CFDESC,CFSTATUS ");
            strSql.Append(" FROM USER_SHARE_COMPANYFUN ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetStoredProcCommand("UP_GetRecordByPage");
            db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "USER_SHARE_COMPANYFUN");
            db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "CFID");
            db.AddInParameter(dbCommand, "PageSize", DbType.Int32, PageSize);
            db.AddInParameter(dbCommand, "PageIndex", DbType.Int32, PageIndex);
            db.AddInParameter(dbCommand, "IsReCount", DbType.Boolean, 0);
            db.AddInParameter(dbCommand, "OrderType", DbType.Boolean, 0);
            db.AddInParameter(dbCommand, "strWhere", DbType.AnsiString, strWhere);
            return db.ExecuteDataSet(dbCommand);
        }*/

        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<UserPermission.Model.USER_SHARE_COMPANYFUNMODEL> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CFID,FMID,PROJECTID,COMPANYID,CFNAME,CFANOTHERNAME,CFPAGEURL,CFPARENTID,CFSORTNUM,CFSTEP,CFISLAST,CFDESC,CFSTATUS ");
            strSql.Append(" FROM USER_SHARE_COMPANYFUN ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<UserPermission.Model.USER_SHARE_COMPANYFUNMODEL> list = new List<UserPermission.Model.USER_SHARE_COMPANYFUNMODEL>();
            Database db = DatabaseFactory.CreateDatabase();
            using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, strSql.ToString()))
            {
                while (dataReader.Read())
                {
                    list.Add(ReaderBind(dataReader));
                }
            }
            return list;
        }


        /// <summary>
        /// 对象实体绑定数据
        /// </summary>
        public UserPermission.Model.USER_SHARE_COMPANYFUNMODEL ReaderBind(IDataReader dataReader)
        {
            UserPermission.Model.USER_SHARE_COMPANYFUN model = new UserPermission.Model.USER_SHARE_COMPANYFUN();
            object ojb;
            ojb = dataReader["CFID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CFID = (decimal)ojb;
            }
            ojb = dataReader["FMID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FMID = (decimal)ojb;
            }
            ojb = dataReader["PROJECTID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.PROJECTID = (decimal)ojb;
            }
            ojb = dataReader["COMPANYID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.COMPANYID = (decimal)ojb;
            }
            model.CFNAME = dataReader["CFNAME"].ToString();
            model.CFANOTHERNAME = dataReader["CFANOTHERNAME"].ToString();
            model.CFPAGEURL = dataReader["CFPAGEURL"].ToString();
            ojb = dataReader["CFPARENTID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CFPARENTID = (decimal)ojb;
            }
            ojb = dataReader["CFSORTNUM"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CFSORTNUM = (decimal)ojb;
            }
            model.CFSTEP = dataReader["CFSTEP"].ToString();
            ojb = dataReader["CFISLAST"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CFISLAST = (decimal)ojb;
            }
            model.CFDESC = dataReader["CFDESC"].ToString();
            ojb = dataReader["CFSTATUS"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CFSTATUS = (decimal)ojb;
            }
            return model;
        }

        #endregion  Method
    }
}

