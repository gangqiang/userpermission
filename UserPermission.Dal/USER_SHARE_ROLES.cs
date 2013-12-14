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
	/// 数据访问类:USER_SHARE_ROLES
	/// </summary>
	public partial class USER_SHARE_ROLES
	{
		public USER_SHARE_ROLES()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(decimal ROLEID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from USER_SHARE_ROLES where ROLEID=@ROLEID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "ROLEID", DbType.String,ROLEID);
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
		public void Add(UserPermission.Model.USER_SHARE_ROLES model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into USER_SHARE_ROLES(");
			strSql.Append("ROLEID,ROLENAME,ROLEDESC,PROJECTID,COMPANYID,STATUS,CREATORID,CREATEDATE)");

			strSql.Append(" values (");
			strSql.Append("@ROLEID,@ROLENAME,@ROLEDESC,@PROJECTID,@COMPANYID,@STATUS,:CREATORID,:CREATEDATE)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "ROLEID", DbType.String, model.ROLEID);
			db.AddInParameter(dbCommand, "ROLENAME", DbType.String, model.ROLENAME);
			db.AddInParameter(dbCommand, "ROLEDESC", DbType.String, model.ROLEDESC);
			db.AddInParameter(dbCommand, "PROJECTID", DbType.String, model.PROJECTID);
			db.AddInParameter(dbCommand, "COMPANYID", DbType.String, model.COMPANYID);
			db.AddInParameter(dbCommand, "STATUS", DbType.String, model.STATUS);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(UserPermission.Model.USER_SHARE_ROLES model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update USER_SHARE_ROLES set ");
			strSql.Append("ROLENAME=@ROLENAME,");
			strSql.Append("ROLEDESC=@ROLEDESC,");
			strSql.Append("PROJECTID=@PROJECTID,");
			strSql.Append("COMPANYID=@COMPANYID,");
			strSql.Append("STATUS=@STATUS");
			strSql.Append(" where ROLEID=@ROLEID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "ROLEID", DbType.String, model.ROLEID);
			db.AddInParameter(dbCommand, "ROLENAME", DbType.String, model.ROLENAME);
			db.AddInParameter(dbCommand, "ROLEDESC", DbType.String, model.ROLEDESC);
			db.AddInParameter(dbCommand, "PROJECTID", DbType.String, model.PROJECTID);
			db.AddInParameter(dbCommand, "COMPANYID", DbType.String, model.COMPANYID);
			db.AddInParameter(dbCommand, "STATUS", DbType.String, model.STATUS);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(decimal ROLEID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from USER_SHARE_ROLES ");
			strSql.Append(" where ROLEID=@ROLEID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "ROLEID", DbType.String,ROLEID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public UserPermission.Model.USER_SHARE_ROLES GetModel(decimal ROLEID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ROLEID,ROLENAME,ROLEDESC,PROJECTID,COMPANYID,STATUS from USER_SHARE_ROLES ");
			strSql.Append(" where ROLEID=@ROLEID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "ROLEID", DbType.String,ROLEID);
			UserPermission.Model.USER_SHARE_ROLES model=null;
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
			strSql.Append("select ROLEID,ROLENAME,ROLEDESC,PROJECTID,COMPANYID,STATUS ");
			strSql.Append(" FROM USER_SHARE_ROLES ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "USER_SHARE_ROLES");
			db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "ROLEID");
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
		public List<UserPermission.Model.USER_SHARE_ROLES> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ROLEID,ROLENAME,ROLEDESC,PROJECTID,COMPANYID,STATUS ");
			strSql.Append(" FROM USER_SHARE_ROLES ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<UserPermission.Model.USER_SHARE_ROLES> list = new List<UserPermission.Model.USER_SHARE_ROLES>();
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
		public UserPermission.Model.USER_SHARE_ROLES ReaderBind(IDataReader dataReader)
		{
			UserPermission.Model.USER_SHARE_ROLES model=new UserPermission.Model.USER_SHARE_ROLES();
			object ojb; 
			ojb = dataReader["ROLEID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ROLEID=(decimal)ojb;
			}
			model.ROLENAME=dataReader["ROLENAME"].ToString();
			model.ROLEDESC=dataReader["ROLEDESC"].ToString();
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

