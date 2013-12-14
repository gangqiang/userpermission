using System;
namespace UserPermission.Model
{
	/// <summary>
	/// 操作日志表
	/// </summary>
	[Serializable]
	public partial class USER_SHARE_LOGMODEL
	{
		public USER_SHARE_LOGMODEL()
		{}
		#region Model
		private int _logid;
		private int _operatetype;
		private int _operatorid;
		private int? _projectid;
		private int? _companyid;
		private string _operatecontent;
		private DateTime _operatedate= Convert.ToDateTime(DateTime.Now);
		/// <summary>
		/// 日志Id
		/// </summary>
		public int LOGID
		{
			set{ _logid=value;}
			get{return _logid;}
		}
		/// <summary>
		/// 操作类别
		/// </summary>
		public int OPERATETYPE
		{
			set{ _operatetype=value;}
			get{return _operatetype;}
		}
		/// <summary>
		/// 操作人
		/// </summary>
		public int OPERATORID
		{
			set{ _operatorid=value;}
			get{return _operatorid;}
		}
		/// <summary>
		/// 项目Id
		/// </summary>
		public int? PROJECTID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 公司Id
		/// </summary>
		public int? COMPANYID
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 操作内容
		/// </summary>
		public string OPERATECONTENT
		{
			set{ _operatecontent=value;}
			get{return _operatecontent;}
		}
		/// <summary>
		/// 操作时间
		/// </summary>
		public DateTime OPERATEDATE
		{
			set{ _operatedate=value;}
			get{return _operatedate;}
		}
		#endregion Model

	}
}

