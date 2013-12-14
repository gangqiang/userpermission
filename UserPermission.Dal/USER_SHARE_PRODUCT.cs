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
	/// 数据访问类:USER_SHARE_PRODUCT
	/// </summary>
	public partial class USER_SHARE_PRODUCT
	{
		public USER_SHARE_PRODUCT()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(decimal PRODUCTID)
		{
			Database db = DatabaseFactory.CreateDatabase();
			StringBuilder strSql = new StringBuilder();
			strSql.Append("select count(1) from USER_SHARE_PRODUCT where PRODUCTID=@PRODUCTID ");
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PRODUCTID", DbType.String,PRODUCTID);
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
		public void Add(UserPermission.Model.USER_SHARE_PRODUCT model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into USER_SHARE_PRODUCT(");
			strSql.Append("PRODUCTID,PROJECTID,PRODUCTNAME,PRODUCTDESC,PRODUCTFLAG)");

			strSql.Append(" values (");
			strSql.Append("@PRODUCTID,@PROJECTID,@PRODUCTNAME,@PRODUCTDESC,@PRODUCTFLAG)");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PRODUCTID", DbType.String, model.PRODUCTID);
			db.AddInParameter(dbCommand, "PROJECTID", DbType.String, model.PROJECTID);
			db.AddInParameter(dbCommand, "PRODUCTNAME", DbType.String, model.PRODUCTNAME);
			db.AddInParameter(dbCommand, "PRODUCTDESC", DbType.String, model.PRODUCTDESC);
			db.AddInParameter(dbCommand, "PRODUCTFLAG", DbType.String, model.PRODUCTFLAG);
			db.ExecuteNonQuery(dbCommand);
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public void Update(UserPermission.Model.USER_SHARE_PRODUCT model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update USER_SHARE_PRODUCT set ");
			strSql.Append("PROJECTID=@PROJECTID,");
			strSql.Append("PRODUCTNAME=@PRODUCTNAME,");
			strSql.Append("PRODUCTDESC=@PRODUCTDESC,");
			strSql.Append("PRODUCTFLAG=@PRODUCTFLAG");
			strSql.Append(" where PRODUCTID=@PRODUCTID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PRODUCTID", DbType.String, model.PRODUCTID);
			db.AddInParameter(dbCommand, "PROJECTID", DbType.String, model.PROJECTID);
			db.AddInParameter(dbCommand, "PRODUCTNAME", DbType.String, model.PRODUCTNAME);
			db.AddInParameter(dbCommand, "PRODUCTDESC", DbType.String, model.PRODUCTDESC);
			db.AddInParameter(dbCommand, "PRODUCTFLAG", DbType.String, model.PRODUCTFLAG);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public void Delete(decimal PRODUCTID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from USER_SHARE_PRODUCT ");
			strSql.Append(" where PRODUCTID=@PRODUCTID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PRODUCTID", DbType.String,PRODUCTID);
			db.ExecuteNonQuery(dbCommand);

		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public UserPermission.Model.USER_SHARE_PRODUCT GetModel(decimal PRODUCTID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PRODUCTID,PROJECTID,PRODUCTNAME,PRODUCTDESC,PRODUCTFLAG from USER_SHARE_PRODUCT ");
			strSql.Append(" where PRODUCTID=@PRODUCTID ");
			Database db = DatabaseFactory.CreateDatabase();
			DbCommand dbCommand = db.GetSqlStringCommand(strSql.ToString());
			db.AddInParameter(dbCommand, "PRODUCTID", DbType.String,PRODUCTID);
			UserPermission.Model.USER_SHARE_PRODUCT model=null;
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
			strSql.Append("select PRODUCTID,PROJECTID,PRODUCTNAME,PRODUCTDESC,PRODUCTFLAG ");
			strSql.Append(" FROM USER_SHARE_PRODUCT ");
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
			db.AddInParameter(dbCommand, "tblName", DbType.AnsiString, "USER_SHARE_PRODUCT");
			db.AddInParameter(dbCommand, "fldName", DbType.AnsiString, "PRODUCTID");
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
		public List<UserPermission.Model.USER_SHARE_PRODUCT> GetListArray(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select PRODUCTID,PROJECTID,PRODUCTNAME,PRODUCTDESC,PRODUCTFLAG ");
			strSql.Append(" FROM USER_SHARE_PRODUCT ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			List<UserPermission.Model.USER_SHARE_PRODUCT> list = new List<UserPermission.Model.USER_SHARE_PRODUCT>();
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
		public UserPermission.Model.USER_SHARE_PRODUCT ReaderBind(IDataReader dataReader)
		{
			UserPermission.Model.USER_SHARE_PRODUCT model=new UserPermission.Model.USER_SHARE_PRODUCT();
			object ojb; 
			ojb = dataReader["PRODUCTID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.PRODUCTID=(decimal)ojb;
			}
			ojb = dataReader["PROJECTID"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.PROJECTID=(decimal)ojb;
			}
			model.PRODUCTNAME=dataReader["PRODUCTNAME"].ToString();
			model.PRODUCTDESC=dataReader["PRODUCTDESC"].ToString();
			ojb = dataReader["PRODUCTFLAG"];
			if(ojb != null && ojb != DBNull.Value)
			{
				model.PRODUCTFLAG=(decimal)ojb;
			}
			return model;
		}

		#endregion  Method
	}
}

