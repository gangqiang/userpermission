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
    /// 数据访问类:USER_SHARE_COMPANYRELATE
    /// </summary>
    public partial class USER_SHARE_COMPANYRELATE
    {
        public USER_SHARE_COMPANYRELATE()
        { }
        #region  Method

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(decimal CID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from USER_SHARE_COMPANYRELATE where CID=@CID ");
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CID", DbType.String, CID);
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
        public bool Add(UserPermission.Model.USER_SHARE_COMPANYRELATEMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into USER_SHARE_COMPANYRELATE(");
            strSql.Append("CID,COMPANYTYPE,COMPANYNAME,COMPANYID,GROUPID,PRODUCTIDS,COMPANYCODE,SHARECOMPANYID,STATUS)");

            strSql.Append(" values (");
            strSql.Append("@CID,@COMPANYTYPE,@COMPANYNAME,@COMPANYID,@GROUPID,@PRODUCTIDS,@COMPANYCODE,@SHARECOMPANYID,@STATUS)");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CID", DbType.String, model.CID);
            db.AddInParameter(dbCommand, "COMPANYTYPE", DbType.String, model.COMPANYTYPE);
            db.AddInParameter(dbCommand, "COMPANYNAME", DbType.String, model.COMPANYNAME);
            db.AddInParameter(dbCommand, "COMPANYID", DbType.String, model.COMPANYID);
            db.AddInParameter(dbCommand, "GROUPID", DbType.String, model.GROUPID);
            db.AddInParameter(dbCommand, "PRODUCTIDS", DbType.String, model.PRODUCTIDS);
            db.AddInParameter(dbCommand, "COMPANYCODE", DbType.String, model.COMPANYCODE);
            db.AddInParameter(dbCommand, "SHARECOMPANYID", DbType.String, model.SHARECOMPANYID);
            db.AddInParameter(dbCommand, "STATUS", DbType.String, model.STATUS);
            db.ExecuteNonQuery(dbCommand) > 0;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public void Update(UserPermission.Model.USER_SHARE_COMPANYRELATEMODEL model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update USER_SHARE_COMPANYRELATE set ");
            strSql.Append("COMPANYTYPE=@COMPANYTYPE,");
            strSql.Append("COMPANYNAME=@COMPANYNAME,");
            strSql.Append("COMPANYID=@COMPANYID,");
            strSql.Append("GROUPID=@GROUPID,");
            strSql.Append("PRODUCTIDS=@PRODUCTIDS,");
            strSql.Append("COMPANYCODE=@COMPANYCODE,");
            strSql.Append("SHARECOMPANYID=@SHARECOMPANYID,");
            strSql.Append("STATUS=@STATUS");
            strSql.Append(" where CID=@CID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CID", DbType.String, model.CID);
            db.AddInParameter(dbCommand, "COMPANYTYPE", DbType.String, model.COMPANYTYPE);
            db.AddInParameter(dbCommand, "COMPANYNAME", DbType.String, model.COMPANYNAME);
            db.AddInParameter(dbCommand, "COMPANYID", DbType.String, model.COMPANYID);
            db.AddInParameter(dbCommand, "GROUPID", DbType.String, model.GROUPID);
            db.AddInParameter(dbCommand, "PRODUCTIDS", DbType.String, model.PRODUCTIDS);
            db.AddInParameter(dbCommand, "COMPANYCODE", DbType.String, model.COMPANYCODE);
            db.AddInParameter(dbCommand, "SHARECOMPANYID", DbType.String, model.SHARECOMPANYID);
            db.AddInParameter(dbCommand, "STATUS", DbType.String, model.STATUS);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public void Delete(decimal CID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from USER_SHARE_COMPANYRELATE ");
            strSql.Append(" where CID=@CID ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CID", DbType.String, CID);
            db.ExecuteNonQuery(dbCommand);

        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public UserPermission.Model.USER_SHARE_COMPANYRELATEMODEL GetModel(decimal CID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CID,COMPANYTYPE,COMPANYNAME,COMPANYID,GROUPID,PRODUCTIDS,COMPANYCODE,SHARECOMPANYID,STATUS from USER_SHARE_COMPANYRELATE ");
            strSql.Append(" where (COMPANYID=:COMPANYID) ");
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
            db.AddInParameter(dbCommand, "CID", DbType.String, CID);
            UserPermission.Model.USER_SHARE_COMPANYRELATEMODEL model = null;
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
            strSql.Append("select CID,COMPANYTYPE,COMPANYNAME,COMPANYID,GROUPID,PRODUCTIDS,COMPANYCODE,SHARECOMPANYID,STATUS ");
            strSql.Append(" FROM USER_SHARE_COMPANYRELATE ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            Database db = DatabaseFactory.CreateDatabase();
            return db.ExecuteDataSet(CommandType.Text, strSql.ToString());
        }



        /// <summary>
        /// 获得数据列表（比DataSet效率高，推荐使用）
        /// </summary>
        public List<UserPermission.Model.USER_SHARE_COMPANYRELATEMODEL> GetListArray(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CID,COMPANYTYPE,COMPANYNAME,COMPANYID,GROUPID,PRODUCTIDS,COMPANYCODE,SHARECOMPANYID,STATUS ");
            strSql.Append(" FROM USER_SHARE_COMPANYRELATE ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            List<UserPermission.Model.USER_SHARE_COMPANYRELATEMODEL> list = new List<UserPermission.Model.USER_SHARE_COMPANYRELATEMODEL>();
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
        public UserPermission.Model.USER_SHARE_COMPANYRELATEMODEL ReaderBind(IDataReader dataReader)
        {
            UserPermission.Model.USER_SHARE_COMPANYRELATEMODEL model = new UserPermission.Model.USER_SHARE_COMPANYRELATEMODEL();
            object ojb;
            ojb = dataReader["CID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.CID = (int)ojb;
            }
            ojb = dataReader["COMPANYTYPE"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.COMPANYTYPE = (int)ojb;
            }
            model.COMPANYNAME = dataReader["COMPANYNAME"].ToString();
            ojb = dataReader["COMPANYID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.COMPANYID = (int)ojb;
            }
            ojb = dataReader["GROUPID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.GROUPID = (int)ojb;
            }
            model.PRODUCTIDS = dataReader["PRODUCTIDS"].ToString();
            ojb = dataReader["COMPANYCODE"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.COMPANYCODE = (int)ojb;
            }
            ojb = dataReader["SHARECOMPANYID"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.SHARECOMPANYID = (int)ojb;
            }
            ojb = dataReader["STATUS"];
            if (ojb != null && ojb != DBNull.Value)
            {
                model.STATUS = (int)ojb;
            }
            return model;
        }

        #endregion  Method
    }
}

