using System;
namespace UserPermission.Model
{
	/// <summary>
	/// 公司菜单表
	/// </summary>
	[Serializable]
	public partial class USER_SHARE_COMPANYFUNMODEL
	{
		public USER_SHARE_COMPANYFUNMODEL()
		{}

		#region Model
		private int _cfid;
		private int _fmid;
		private int _projectid=0;
		private int _companyid;
		private string _cfname;
		private string _cfanothername;
		private string _cfpageurl;
		private int? _cfparentid=0;
		private int? _cfsortnum=0;
		private string _cfstep;
		private int? _cfislast;
		private string _cfdesc;
		private int? _cfstatus=0;


		/// <summary>
		/// 主键ID
		/// </summary>
		public int CFID
		{
			set{ _cfid=value;}
			get{return _cfid;}
		}
		/// <summary>
		/// 功能菜单Id
		/// </summary>
		public int FMID
		{
			set{ _fmid=value;}
			get{return _fmid;}
		}
		/// <summary>
		/// 所属项目Id
		/// </summary>
		public int PROJECTID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// 公司编码
		/// </summary>
		public int COMPANYID
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// 功能菜单名称
		/// </summary>
		public string CFNAME
		{
			set{ _cfname=value;}
			get{return _cfname;}
		}
		/// <summary>
		/// 菜单别名
		/// </summary>
		public string CFANOTHERNAME
		{
			set{ _cfanothername=value;}
			get{return _cfanothername;}
		}
		/// <summary>
		/// 功能页面地址
		/// </summary>
		public string CFPAGEURL
		{
			set{ _cfpageurl=value;}
			get{return _cfpageurl;}
		}
		/// <summary>
		/// 父级功能Id
		/// </summary>
		public int? CFPARENTID
		{
			set{ _cfparentid=value;}
			get{return _cfparentid;}
		}
		/// <summary>
		/// 功能排序值(越大越靠前)
		/// </summary>
		public int? CFSORTNUM
		{
			set{ _cfsortnum=value;}
			get{return _cfsortnum;}
		}
		/// <summary>
		/// 菜单层级
		/// </summary>
		public string CFSTEP
		{
			set{ _cfstep=value;}
			get{return _cfstep;}
		}
		/// <summary>
		/// 是否末级菜单
		/// </summary>
		public int? CFISLAST
		{
			set{ _cfislast=value;}
			get{return _cfislast;}
		}
		/// <summary>
		/// 功能描述(功能的描述信息，可以用于菜单的鼠标提示)
		/// </summary>
		public string CFDESC
		{
			set{ _cfdesc=value;}
			get{return _cfdesc;}
		}
		/// <summary>
		/// 功能状态(各个项目自己定义数字)
		/// </summary>
		public int? CFSTATUS
		{
			set{ _cfstatus=value;}
			get{return _cfstatus;}
		}
		#endregion Model

	}
}

