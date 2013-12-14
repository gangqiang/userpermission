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
	/// 数据访问类:USER_SHARE_ACCOUNT
	/// </summary>
	public partial class USER_SHARE_ACCOUNT
	{
		public USER_SHARE_ACCOUNT()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(decimal ACCOUNTID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from USER_SHARE_ACCOUNT where ACCOUNTID=@ACCOUNTID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "ACCOUNTID", DbType.String,ACCOUNTID);
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
		public void Add(UserPermission.Model.USER_SHARE_ACCOUNTMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into USER_SHARE_ACCOUNT(");
			strSql.Append("ACCOUNTID,ACCOUNTNAME,COMPANYID,ACCOUNTPWD,ORIGNALPWD,REALNAME,EMAIL,ROLEIDS,LINKPHONE,CREATEDATE,CREATORID,ISADMIN,STATUS)");

			strSql.Append(" values (");
			strSql.Append("@ACCOUNTID,@ACCOUNTNAME,@COMPANYID,@ACCOUNTPWD,@ORIGNALPWD,@REALNAME,@EMAIL,@ROLEIDS,@LINKPHONE,@CREATEDATE,@CREATORID,@ISADMIN,@STATUS)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "ACCOUNTID", DbType.String, model.ACCOUNTID);
			db.AddInParameter(dbCommand, "ACCOUNTNAME", DbType.String, model.ACCOUNTNAME);
			db.AddInParameter(dbCommand, "COMPANYID", DbType.String, model.COMPANYID);
			db.AddInParameter(dbCommand, "ACCOUNTPWD", DbType.String, model.ACCOUNTPWD);
			db.AddInParameter(dbCommand, "ORIGNALPWD", DbType.String, model.ORIGNALPWD);
			db.AddInParameter(dbCommand, "REALNAME", DbType.String, model.REALNAME);
			db.AddInParameter(dbCommand, "EMAIL", DbType.String, model.EMAIL);
			db.AddInParameter(dbCommand, "ROLEIDS", DbType.String, model.ROLEIDS);
			db.AddInParameter(dbCommand, "LINKPHONE", DbType.String, model.LINKPHONE);
			db.AddInParameter(dbCommand, "CREATEDATE", DbType.String, model.CREATEDATE);
			db.AddInParameter(dbCommand, "CREATORID", DbType.String, model.CREATORID);
			db.AddInParameter(dbCommand, "ISADMIN", DbType.String, model.ISADMIN);
			db.AddInParameter(dbCommand, "STATUS", DbType.String, model.STATUS);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(UserPermission.Model.USER_SHARE_ACCOUNTMODEL model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update USER_SHARE_ACCOUNT set ");
			strSql.Append("ACCOUNTNAME=@ACCOUNTNAME,");
			strSql.Append("COMPANYID=@COMPANYID,");
			strSql.Append("ACCOUNTPWD=@ACCOUNTPWD,");
			strSql.Append("ORIGNALPWD=@ORIGNALPWD,");
			strSql.Append("REALNAME=@REALNAME,");
			strSql.Append("EMAIL=@EMAIL,");
			strSql.Append("ROLEIDS=@ROLEIDS,");
			strSql.Append("LINKPHONE=@LINKPHONE,");
			strSql.Append("CREATEDATE=@CREATEDATE,");
			strSql.Append("CREATORID=@CREATORID,");
			strSql.Append("ISADMIN=@ISADMIN,");
			strSql.Append("STATUS=@STATUS");
			strSql.Append(" where ACCOUNTID=@ACCOUNTID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "ACCOUNTID", DbType.String, model.ACCOUNTID);
			db.AddInParameter(dbCommand, "ACCOUNTNAME", DbType.String, model.ACCOUNTNAME);
			db.AddInParameter(dbCommand, "COMPANYID", DbType.String, model.COMPANYID);
			db.AddInParameter(dbCommand, "ACCOUNTPWD", DbType.String, model.ACCOUNTPWD);
			db.AddInParameter(dbCommand, "ORIGNALPWD", DbType.String, model.ORIGNALPWD);
			db.AddInParameter(dbCommand, "REALNAME", DbType.String, model.REALNAME);
			db.AddInParameter(dbCommand, "EMAIL", DbType.String, model.EMAIL);
			db.AddInParameter(dbCommand, "ROLEIDS", DbType.String, model.ROLEIDS);
			db.AddInParameter(dbCommand, "LINKPHONE", DbType.String, model.LINKPHONE);
			db.AddInParameter(dbCommand, "CREATEDATE", DbType.String, model.CREATEDATE);
			db.AddInParameter(dbCommand, "CREATORID", DbType.String, model.CREATORID);
			db.AddInParameter(dbCommand, "ISADMIN", DbType.String, model.ISADMIN);
			db.AddInParameter(dbCommand, "STATUS", DbType.String, model.STATUS);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(decimal ACCOUNTID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from USER_SHARE_ACCOUNT ");
			strSql.Append(" where ACCOUNTID=@ACCOUNTID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "ACCOUNTID", DbType.String,ACCOUNTID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public UserPermission.Model.USER_SHARE_ACCOUNT GetModel(decimal ACCOUNTID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ACCOUNTID,ACCOUNTNAME,COMPANYID,ACCOUNTPWD,ORIGNALPWD,REALNAME,EMAIL,ROLEIDS,LINKPHONE,CREATEDATE,CREATORID,ISADMIN,STATUS from USER_SHARE_ACCOUNT ");
			strSql.Append(" where ACCOUNTID=@ACCOUNTID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "ACCOUNTID", DbType.String,ACCOUNTID);
			UserPermission.Model.USER_SHARE_ACCOUNT model=null;
			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				if(dataReader.Read())
				{
					model=ReaderBind(dataReader);
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ACCOUNTID,ACCOUNTNAME,COMPANYID,ACCOUNTPWD,ORIGNALPWD,REALNAME,EMAIL,ROLEIDS,LINKPHONE,CREATEDATE,CREATORID,ISADMIN,STATUS ");
			strSql.Append(" FROM USER_SHARE_ACCOUNT ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "USER_SHARE_ACCOUNT");
			db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "ACCOUNTID");
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
		public List<UserPermission.Model.USER_SHARE_ACCOUNT> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ACCOUNTID,ACCOUNTNAME,COMPANYID,ACCOUNTPWD,ORIGNALPWD,REALNAME,EMAIL,ROLEIDS,LINKPHONE,CREATEDATE,CREATORID,ISADMIN,STATUS ");
			strSql.Append(" FROM USER_SHARE_ACCOUNT ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<UserPermission.Model.USER_SHARE_ACCOUNT> list = new List<UserPermission.Model.USER_SHARE_ACCOUNT>();
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
		public UserPermission.Model.USER_SHARE_ACCOUNT ReaderBind(IDataReader dataReader)
		{
			UserPermission.Model.USER_SHARE_ACCOUNT model=new UserPermission.Model.USER_SHARE_ACCOUNT();
			object ojb; 
			ojb = dataReader["ACCOUNTID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ACCOUNTID=(decimal)ojb;
			}
			model.ACCOUNTNAME=dataReader["ACCOUNTNAME"].ToString();
			ojb = dataReader["COMPANYID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.COMPANYID=(decimal)ojb;
			}
			model.ACCOUNTPWD=dataReader["ACCOUNTPWD"].ToString();
			model.ORIGNALPWD=dataReader["ORIGNALPWD"].ToString();
			model.REALNAME=dataReader["REALNAME"].ToString();
			model.EMAIL=dataReader["EMAIL"].ToString();
			model.ROLEIDS=dataReader["ROLEIDS"].ToString();
			model.LINKPHONE=dataReader["LINKPHONE"].ToString();
			ojb = dataReader["CREATEDATE"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.CREATEDATE=(DateTime)ojb;
			}
			ojb = dataReader["CREATORID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.CREATORID=(decimal)ojb;
			}
			ojb = dataReader["ISADMIN"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ISADMIN=(decimal)ojb;
			}
			ojb = dataReader["STATUS"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.STATUS=(decimal)ojb;
			}
			return model;
		}

		#endregion  Method
	}
}

