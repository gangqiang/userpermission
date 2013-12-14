using System;
namespace UserPermission.Model
{
	/// <summary>
	/// ������־��
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
		/// ��־Id
		/// </summary>
		public int LOGID
		{
			set{ _logid=value;}
			get{return _logid;}
		}
		/// <summary>
		/// �������
		/// </summary>
		public int OPERATETYPE
		{
			set{ _operatetype=value;}
			get{return _operatetype;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public int OPERATORID
		{
			set{ _operatorid=value;}
			get{return _operatorid;}
		}
		/// <summary>
		/// ��ĿId
		/// </summary>
		public int? PROJECTID
		{
			set{ _projectid=value;}
			get{return _projectid;}
		}
		/// <summary>
		/// ��˾Id
		/// </summary>
		public int? COMPANYID
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public string OPERATECONTENT
		{
			set{ _operatecontent=value;}
			get{return _operatecontent;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime OPERATEDATE
		{
			set{ _operatedate=value;}
			get{return _operatedate;}
		}
		#endregion Model

	}
}

