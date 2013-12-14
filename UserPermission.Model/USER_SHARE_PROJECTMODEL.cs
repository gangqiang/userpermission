using System;
namespace UserPermission.Model
{
	/// <summary>
	/// 项目信息表
	/// </summary>
	[Serializable]
	public partial class USER_SHARE_PROJECTMODEL
	{
		public USER_SHARE_PROJECTMODEL()
		{}
		#region Model
		private int _projectid;
		private string _projectname="";
		private string _apiservicekey="";
		private DateTime _createdate= Convert.ToDateTime(DateTime.Now);
		private string _projectremark;
		private int? _status;
		/// <summary>
		/// 项目Id
		/// </summary>
		public int PROJECTID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 项目名称
		/// </summary>
		public string PROJECTNAME
		{
			set{ _projectname=value;}
			get{return _projectname;}
		}
		/// <summary>
		/// 接口访问密钥
		/// </summary>
		public string APISERVICEKEY
		{
			set{ _apiservicekey=value;}
			get{return _apiservicekey;}
		}
		/// <summary>
		/// 开通日期
		/// </summary>
		public DateTime CREATEDATE
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 项目备注
		/// </summary>
		public string PROJECTREMARK
		{
			set{ _projectremark=value;}
			get{return _projectremark;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public int? STATUS
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

