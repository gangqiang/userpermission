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
	/// 数据访问类:USER_SHARE_ROLEFUN
	/// </summary>
	public partial class USER_SHARE_ROLEFUN
	{
		public USER_SHARE_ROLEFUN()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public void Add(UserPermission.Model.USER_SHARE_ROLEFUN model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into USER_SHARE_ROLEFUN(");
			strSql.Append("ROLEID,FUNID)");

			strSql.Append(" values (");
			strSql.Append("@ROLEID,@FUNID)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "ROLEID", DbType.String, model.ROLEID);
			db.AddInParameter(dbCommand, "FUNID", DbType.String, model.FUNID);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(UserPermission.Model.USER_SHARE_ROLEFUN model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update USER_SHARE_ROLEFUN set ");
			strSql.Append("ROLEID=@ROLEID,");
			strSql.Append("FUNID=@FUNID");
			strSql.Append(" where ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "ROLEID", DbType.String, model.ROLEID);
			db.AddInParameter(dbCommand, "FUNID", DbType.String, model.FUNID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from USER_SHARE_ROLEFUN ");
			strSql.Append(" where ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public UserPermission.Model.USER_SHARE_ROLEFUN GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ROLEID,FUNID from USER_SHARE_ROLEFUN ");
			strSql.Append(" where ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			UserPermission.Model.USER_SHARE_ROLEFUN model=null;
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
			strSql.Append("select ROLEID,FUNID ");
			strSql.Append(" FROM USER_SHARE_ROLEFUN ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "USER_SHARE_ROLEFUN");
			db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "CID");
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
		public List<UserPermission.Model.USER_SHARE_ROLEFUN> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ROLEID,FUNID ");
			strSql.Append(" FROM USER_SHARE_ROLEFUN ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<UserPermission.Model.USER_SHARE_ROLEFUN> list = new List<UserPermission.Model.USER_SHARE_ROLEFUN>();
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
		public UserPermission.Model.USER_SHARE_ROLEFUN ReaderBind(IDataReader dataReader)
		{
			UserPermission.Model.USER_SHARE_ROLEFUN model=new UserPermission.Model.USER_SHARE_ROLEFUN();
			object ojb; 
			ojb = dataReader["ROLEID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.ROLEID=(decimal)ojb;
			}
			ojb = dataReader["FUNID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.FUNID=(decimal)ojb;
			}
			return model;
		}

		#endregion  Method
	}
}

