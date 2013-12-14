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
	/// 数据访问类:USER_SHARE_PROJECT
	/// </summary>
	public partial class USER_SHARE_PROJECT
	{
		public USER_SHARE_PROJECT()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(decimal PROJECTID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from USER_SHARE_PROJECT where PROJECTID=@PROJECTID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PROJECTID", DbType.String,PROJECTID);
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
		public void Add(UserPermission.Model.USER_SHARE_PROJECT model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into USER_SHARE_PROJECT(");
			strSql.Append("PROJECTID,PROJECTNAME,APISERVICEKEY,CREATEDATE,PROJECTREMARK,STATUS)");

			strSql.Append(" values (");
			strSql.Append("@PROJECTID,@PROJECTNAME,@APISERVICEKEY,@CREATEDATE,@PROJECTREMARK,@STATUS)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PROJECTID", DbType.String, model.PROJECTID);
			db.AddInParameter(dbCommand, "PROJECTNAME", DbType.String, model.PROJECTNAME);
			db.AddInParameter(dbCommand, "APISERVICEKEY", DbType.String, model.APISERVICEKEY);
			db.AddInParameter(dbCommand, "CREATEDATE", DbType.String, model.CREATEDATE);
			db.AddInParameter(dbCommand, "PROJECTREMARK", DbType.String, model.PROJECTREMARK);
			db.AddInParameter(dbCommand, "STATUS", DbType.String, model.STATUS);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(UserPermission.Model.USER_SHARE_PROJECT model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update USER_SHARE_PROJECT set ");
			strSql.Append("PROJECTNAME=@PROJECTNAME,");
			strSql.Append("APISERVICEKEY=@APISERVICEKEY,");
			strSql.Append("CREATEDATE=@CREATEDATE,");
			strSql.Append("PROJECTREMARK=@PROJECTREMARK,");
			strSql.Append("STATUS=@STATUS");
			strSql.Append(" where PROJECTID=@PROJECTID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PROJECTID", DbType.String, model.PROJECTID);
			db.AddInParameter(dbCommand, "PROJECTNAME", DbType.String, model.PROJECTNAME);
			db.AddInParameter(dbCommand, "APISERVICEKEY", DbType.String, model.APISERVICEKEY);
			db.AddInParameter(dbCommand, "CREATEDATE", DbType.String, model.CREATEDATE);
			db.AddInParameter(dbCommand, "PROJECTREMARK", DbType.String, model.PROJECTREMARK);
			db.AddInParameter(dbCommand, "STATUS", DbType.String, model.STATUS);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(decimal PROJECTID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from USER_SHARE_PROJECT ");
			strSql.Append(" where PROJECTID=@PROJECTID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PROJECTID", DbType.String,PROJECTID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public UserPermission.Model.USER_SHARE_PROJECT GetModel(decimal PROJECTID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PROJECTID,PROJECTNAME,APISERVICEKEY,CREATEDATE,PROJECTREMARK,STATUS from USER_SHARE_PROJECT ");
			strSql.Append(" where PROJECTID=@PROJECTID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PROJECTID", DbType.String,PROJECTID);
			UserPermission.Model.USER_SHARE_PROJECT model=null;
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
			strSql.Append("select PROJECTID,PROJECTNAME,APISERVICEKEY,CREATEDATE,PROJECTREMARK,STATUS ");
			strSql.Append(" FROM USER_SHARE_PROJECT ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "USER_SHARE_PROJECT");
			db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "PROJECTID");
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
		public List<UserPermission.Model.USER_SHARE_PROJECT> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PROJECTID,PROJECTNAME,APISERVICEKEY,CREATEDATE,PROJECTREMARK,STATUS ");
			strSql.Append(" FROM USER_SHARE_PROJECT ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<UserPermission.Model.USER_SHARE_PROJECT> list = new List<UserPermission.Model.USER_SHARE_PROJECT>();
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
		public UserPermission.Model.USER_SHARE_PROJECT ReaderBind(IDataReader dataReader)
		{
			UserPermission.Model.USER_SHARE_PROJECT model=new UserPermission.Model.USER_SHARE_PROJECT();
			object ojb; 
			ojb = dataReader["PROJECTID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.PROJECTID=(decimal)ojb;
			}
			model.PROJECTNAME=dataReader["PROJECTNAME"].ToString();
			model.APISERVICEKEY=dataReader["APISERVICEKEY"].ToString();
			ojb = dataReader["CREATEDATE"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.CREATEDATE=(DateTime)ojb;
			}
			model.PROJECTREMARK=dataReader["PROJECTREMARK"].ToString();
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

