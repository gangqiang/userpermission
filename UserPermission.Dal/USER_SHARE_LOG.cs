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
	/// 数据访问类:USER_SHARE_LOG
	/// </summary>
	public partial class USER_SHARE_LOG
	{
		public USER_SHARE_LOG()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(decimal LOGID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from USER_SHARE_LOG where LOGID=@LOGID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "LOGID", DbType.String,LOGID);
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
		public void Add(UserPermission.Model.USER_SHARE_LOG model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into USER_SHARE_LOG(");
			strSql.Append("LOGID,OPERATETYPE,OPERATORID,PROJECTID,COMPANYID,OPERATECONTENT,OPERATEDATE)");

			strSql.Append(" values (");
			strSql.Append("@LOGID,@OPERATETYPE,@OPERATORID,@PROJECTID,@COMPANYID,@OPERATECONTENT,@OPERATEDATE)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "LOGID", DbType.String, model.LOGID);
			db.AddInParameter(dbCommand, "OPERATETYPE", DbType.String, model.OPERATETYPE);
			db.AddInParameter(dbCommand, "OPERATORID", DbType.String, model.OPERATORID);
			db.AddInParameter(dbCommand, "PROJECTID", DbType.String, model.PROJECTID);
			db.AddInParameter(dbCommand, "COMPANYID", DbType.String, model.COMPANYID);
			db.AddInParameter(dbCommand, "OPERATECONTENT", DbType.String, model.OPERATECONTENT);
			db.AddInParameter(dbCommand, "OPERATEDATE", DbType.String, model.OPERATEDATE);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(UserPermission.Model.USER_SHARE_LOG model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update USER_SHARE_LOG set ");
			strSql.Append("OPERATETYPE=@OPERATETYPE,");
			strSql.Append("OPERATORID=@OPERATORID,");
			strSql.Append("PROJECTID=@PROJECTID,");
			strSql.Append("COMPANYID=@COMPANYID,");
			strSql.Append("OPERATECONTENT=@OPERATECONTENT,");
			strSql.Append("OPERATEDATE=@OPERATEDATE");
			strSql.Append(" where LOGID=@LOGID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "LOGID", DbType.String, model.LOGID);
			db.AddInParameter(dbCommand, "OPERATETYPE", DbType.String, model.OPERATETYPE);
			db.AddInParameter(dbCommand, "OPERATORID", DbType.String, model.OPERATORID);
			db.AddInParameter(dbCommand, "PROJECTID", DbType.String, model.PROJECTID);
			db.AddInParameter(dbCommand, "COMPANYID", DbType.String, model.COMPANYID);
			db.AddInParameter(dbCommand, "OPERATECONTENT", DbType.String, model.OPERATECONTENT);
			db.AddInParameter(dbCommand, "OPERATEDATE", DbType.String, model.OPERATEDATE);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(decimal LOGID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from USER_SHARE_LOG ");
			strSql.Append(" where LOGID=@LOGID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "LOGID", DbType.String,LOGID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public UserPermission.Model.USER_SHARE_LOG GetModel(decimal LOGID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select LOGID,OPERATETYPE,OPERATORID,PROJECTID,COMPANYID,OPERATECONTENT,OPERATEDATE from USER_SHARE_LOG ");
			strSql.Append(" where LOGID=@LOGID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "LOGID", DbType.String,LOGID);
			UserPermission.Model.USER_SHARE_LOG model=null;
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
			strSql.Append("select LOGID,OPERATETYPE,OPERATORID,PROJECTID,COMPANYID,OPERATECONTENT,OPERATEDATE ");
			strSql.Append(" FROM USER_SHARE_LOG ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "USER_SHARE_LOG");
			db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "LOGID");
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
		public List<UserPermission.Model.USER_SHARE_LOG> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select LOGID,OPERATETYPE,OPERATORID,PROJECTID,COMPANYID,OPERATECONTENT,OPERATEDATE ");
			strSql.Append(" FROM USER_SHARE_LOG ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<UserPermission.Model.USER_SHARE_LOG> list = new List<UserPermission.Model.USER_SHARE_LOG>();
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
		public UserPermission.Model.USER_SHARE_LOG ReaderBind(IDataReader dataReader)
		{
			UserPermission.Model.USER_SHARE_LOG model=new UserPermission.Model.USER_SHARE_LOG();
			object ojb; 
			ojb = dataReader["LOGID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.LOGID=(decimal)ojb;
			}
			ojb = dataReader["OPERATETYPE"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.OPERATETYPE=(decimal)ojb;
			}
			ojb = dataReader["OPERATORID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.OPERATORID=(decimal)ojb;
			}
			ojb = dataReader["PROJECTID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.PROJECTID=(decimal)ojb;
			}
			ojb = dataReader["COMPANYID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.COMPANYID=(decimal)ojb;
			}
			model.OPERATECONTENT=dataReader["OPERATECONTENT"].ToString();
			ojb = dataReader["OPERATEDATE"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.OPERATEDATE=(DateTime)ojb;
			}
			return model;
		}

		#endregion  Method
	}
}

