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
    /// 数据访问类:USER_SHARE_FUNMENU
    /// </summary>
    public partial class USER_SHARE_FUNMENU
    {
        public USER_SHARE_FUNMENU()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(decimal FMID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from USER_SHARE_FUNMENU where FMID=@FMID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FMID", DbType.String, FMID);
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
        public void Add(UserPermission.Model.USER_SHARE_FUNMENU model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into USER_SHARE_FUNMENU(");
            strSql.Append("FMID,PROJECTID,FMNAME,FMPAGEURL,FMPARENTID,FMSORTNUM,FMSTEP,FMISLAST,FMDESC,FMSTATUS)");

            strSql.Append(" values (");
            strSql.Append("@FMID,@PROJECTID,@FMNAME,@FMPAGEURL,@FMPARENTID,@FMSORTNUM,@FMSTEP,@FMISLAST,@FMDESC,@FMSTATUS)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FMID", DbType.String, model.FMID);
            db.AddInParameter(dbCommand, "PROJECTID", DbType.String, model.PROJECTID);
            db.AddInParameter(dbCommand, "FMNAME", DbType.String, model.FMNAME);
            db.AddInParameter(dbCommand, "FMPAGEURL", DbType.String, model.FMPAGEURL);
            db.AddInParameter(dbCommand, "FMPARENTID", DbType.String, model.FMPARENTID);
            db.AddInParameter(dbCommand, "FMSORTNUM", DbType.String, model.FMSORTNUM);
            db.AddInParameter(dbCommand, "FMSTEP", DbType.String, model.FMSTEP);
            db.AddInParameter(dbCommand, "FMISLAST", DbType.String, model.FMISLAST);
            db.AddInParameter(dbCommand, "FMDESC", DbType.String, model.FMDESC);
            db.AddInParameter(dbCommand, "FMSTATUS", DbType.String, model.FMSTATUS);
            db.ExecuteNonQuery(dbCommand);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(UserPermission.Model.USER_SHARE_FUNMENU model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update USER_SHARE_FUNMENU set ");
            strSql.Append("PROJECTID=@PROJECTID,");
            strSql.Append("FMNAME=@FMNAME,");
            strSql.Append("FMPAGEURL=@FMPAGEURL,");
            strSql.Append("FMPARENTID=@FMPARENTID,");
            strSql.Append("FMSORTNUM=@FMSORTNUM,");
            strSql.Append("FMSTEP=@FMSTEP,");
            strSql.Append("FMISLAST=@FMISLAST,");
            strSql.Append("FMDESC=@FMDESC,");
            strSql.Append("FMSTATUS=@FMSTATUS");
            strSql.Append(" where FMID=@FMID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FMID", DbType.String, model.FMID);
            db.AddInParameter(dbCommand, "PROJECTID", DbType.String, model.PROJECTID);
            db.AddInParameter(dbCommand, "FMNAME", DbType.String, model.FMNAME);
            db.AddInParameter(dbCommand, "FMPAGEURL", DbType.String, model.FMPAGEURL);
            db.AddInParameter(dbCommand, "FMPARENTID", DbType.String, model.FMPARENTID);
            db.AddInParameter(dbCommand, "FMSORTNUM", DbType.String, model.FMSORTNUM);
            db.AddInParameter(dbCommand, "FMSTEP", DbType.String, model.FMSTEP);
            db.AddInParameter(dbCommand, "FMISLAST", DbType.String, model.FMISLAST);
            db.AddInParameter(dbCommand, "FMDESC", DbType.String, model.FMDESC);
            db.AddInParameter(dbCommand, "FMSTATUS", DbType.String, model.FMSTATUS);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserPermission.Model.USER_SHARE_FUNMENU GetModel(decimal FMID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FMID,PROJECTID,FMNAME,FMPAGEURL,FMPARENTID,FMSORTNUM,FMSTEP,FMISLAST,FMDESC,FMSTATUS from USER_SHARE_FUNMENU ");
            strSql.Append(" where FMID=@FMID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "FMID", DbType.String, FMID);
            UserPermission.Model.USER_SHARE_FUNMENU model = null;
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
            strSql.Append("select FMID,PROJECTID,FMNAME,FMPAGEURL,FMPARENTID,FMSORTNUM,FMSTEP,FMISLAST,FMDESC,FMSTATUS ");
            strSql.Append(" FROM USER_SHARE_FUNMENU ");
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
            db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "USER_SHARE_FUNMENU");
            db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "FMID");
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
        public List<UserPermission.Model.USER_SHARE_FUNMENU> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select FMID,PROJECTID,FMNAME,FMPAGEURL,FMPARENTID,FMSORTNUM,FMSTEP,FMISLAST,FMDESC,FMSTATUS ");
            strSql.Append(" FROM USER_SHARE_FUNMENU ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<UserPermission.Model.USER_SHARE_FUNMENU> list = new List<UserPermission.Model.USER_SHARE_FUNMENU>();
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
        public UserPermission.Model.USER_SHARE_FUNMENU ReaderBind(IDataReader dataReader)
        {
            UserPermission.Model.USER_SHARE_FUNMENU model = new UserPermission.Model.USER_SHARE_FUNMENU();
            object ojb;
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
            model.FMNAME = dataReader["FMNAME"].ToString();
            model.FMPAGEURL = dataReader["FMPAGEURL"].ToString();
            ojb = dataReader["FMPARENTID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FMPARENTID = (decimal)ojb;
            }
            ojb = dataReader["FMSORTNUM"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FMSORTNUM = (decimal)ojb;
            }
            model.FMSTEP = dataReader["FMSTEP"].ToString();
            ojb = dataReader["FMISLAST"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FMISLAST = (decimal)ojb;
            }
            model.FMDESC = dataReader["FMDESC"].ToString();
            ojb = dataReader["FMSTATUS"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.FMSTATUS = (decimal)ojb;
            }
            return model;
        }

        #endregion  Method
    }
}

