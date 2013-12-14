using System;
namespace UserPermission.Model
{
	/// <summary>
	/// �˺���Ϣ��
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
		/// �˺�Id
		/// </summary>
		public int ACCOUNTID
		{
			set{ _accountid=value;}
			get{return _accountid;}
		}
		/// <summary>
		/// �˺���
		/// </summary>
		public string ACCOUNTNAME
		{
			set{ _accountname=value;}
			get{return _accountname;}
		}
		/// <summary>
		/// ������˾����
		/// </summary>
		public int COMPANYID
		{
			set{ _companyid=value;}
			get{return _companyid;}
		}
		/// <summary>
		/// ��¼����
		/// </summary>
		public string ACCOUNTPWD
		{
			set{ _accountpwd=value;}
			get{return _accountpwd;}
		}
		/// <summary>
		/// ԭʼ����
		/// </summary>
		public string ORIGNALPWD
		{
			set{ _orignalpwd=value;}
			get{return _orignalpwd;}
		}
		/// <summary>
		/// ��ʵ����
		/// </summary>
		public string REALNAME
		{
			set{ _realname=value;}
			get{return _realname;}
		}
		/// <summary>
		/// ����
		/// </summary>
		public string EMAIL
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// ������ɫId,�����ɫ��Ӣ��״̬���Ÿ���
		/// </summary>
		public string ROLEIDS
		{
			set{ _roleids=value;}
			get{return _roleids;}
		}
		/// <summary>
		/// ��ϵ�绰
		/// </summary>
		public string LINKPHONE
		{
			set{ _linkphone=value;}
			get{return _linkphone;}
		}
		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime CREATEDATE
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// ������Id
		/// </summary>
		public int CREATORID
		{
			set{ _creatorid=value;}
			get{return _creatorid;}
		}
		/// <summary>
		/// �Ƿ����Ա
		/// </summary>
		public int ISADMIN
		{
			set{ _isadmin=value;}
			get{return _isadmin;}
		}
		/// <summary>
		/// �˺�״̬ (0������ 1����Ч 2����ɾ��)
		/// </summary>
		public int STATUS
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

	}
}

