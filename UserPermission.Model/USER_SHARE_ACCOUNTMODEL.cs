using System;
namespace UserPermission.Model
{
	/// <summary>
	/// 账号信息表
	/// </summary>
	[Serializable]
	public partial class USER_SHARE_ACCOUNTMODEL
	{
        public USER_SHARE_ACCOUNTMODEL()
		{}
		#region Model
		private int _accountid;
		private string _accountname;
		private int _companyid;
		private string _accountpwd;
		private string _orignalpwd;
		private string _realname;
		private string _email;
		private string _roleids;
		private string _linkphone;
		private DateTime _createdate= Convert.ToDateTime(DateTime.Now);
		private int _creatorid;
		private int _isadmin;
		private int _status;
		/// <summary>
		/// 账号Id
		/// </summary>
		public int ACCOUNTID
		{
			set{ _accountid=value;}
			get{return _accountid;}
		}
		/// <summary>
		/// 账号名
		/// </summary>
		public string ACCOUNTNAME
		{
			set{ _accountname=value;}
			get{return _accountname;}
		}
		/// <summary>
		/// 所属公司编码
		/// </summary>
		public int COMPANYID
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 登录密码
		/// </summary>
		public string ACCOUNTPWD
		{
			set{ _accountpwd=value;}
			get{return _accountpwd;}
		}
		/// <summary>
		/// 原始密码
		/// </summary>
		public string ORIGNALPWD
		{
			set{ _orignalpwd=value;}
			get{return _orignalpwd;}
		}
		/// <summary>
		/// 真实姓名
		/// </summary>
		public string REALNAME
		{
			set{ _realname=value;}
			get{return _realname;}
		}
		/// <summary>
		/// 邮箱
		/// </summary>
		public string EMAIL
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 所属角色Id,多个角色用英文状态逗号隔开
		/// </summary>
		public string ROLEIDS
		{
			set{ _roleids=value;}
			get{return _roleids;}
		}
		/// <summary>
		/// 联系电话
		/// </summary>
		public string LINKPHONE
		{
			set{ _linkphone=value;}
			get{return _linkphone;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CREATEDATE
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 创建人Id
		/// </summary>
		public int CREATORID
		{
			set{ _creatorid=value;}
			get{return _creatorid;}
		}
		/// <summary>
		/// 是否管理员
		/// </summary>
		public int ISADMIN
		{
			set{ _isadmin=value;}
			get{return _isadmin;}
		}
		/// <summary>
		/// 账号状态 (0：正常 1：无效 2：已删除)
		/// </summary>
		public int STATUS
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

